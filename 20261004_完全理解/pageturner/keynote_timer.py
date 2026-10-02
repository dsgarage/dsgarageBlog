#!/usr/bin/env python3
"""Keynote を時間表どおりに自動でめくり、めくる前に ntfy で通知を送る。

使い方:
    python3 keynote_timer.py schedule.json            # スライドショーを開始して自動めくり
    python3 keynote_timer.py schedule.json --no-start # 既に再生中のスライドショーを使う
    python3 keynote_timer.py schedule.json --dry-run --speed 20
    python3 keynote_timer.py schedule.json --test-notify

標準ライブラリのみで動く(pip 依存なし)。詳細は README.md。
"""

import argparse
import base64
import json
import os
import select
import subprocess
import sys
import threading
import time
import urllib.error
import urllib.request

POLL_INTERVAL = 0.5      # Keynote の現在スライドを問い合わせる間隔(実時間・秒)
TICK = 0.1               # メインループの刻み(実時間・秒)
NOTIFY_TIMEOUT = 2.0     # 通知の HTTP タイムアウト(秒)
OSASCRIPT_TIMEOUT = 5.0  # osascript 1 回のタイムアウト(秒)
LAST_MINUTE_SEC = 60     # 「残り1分」を送る全体の残り秒


# ---------------------------------------------------------------- 表示

class Console:
    """イベントログと、1 行で上書きするステータス行を扱う。"""

    def __init__(self):
        self.is_tty = sys.stdout.isatty()
        self._status_len = 0

    def log(self, msg):
        self._clear_status()
        print(msg, flush=True)

    def status(self, text):
        if not self.is_tty:
            return
        pad = max(0, self._status_len - _display_width(text))
        sys.stdout.write("\r" + text + " " * pad)
        sys.stdout.flush()
        self._status_len = _display_width(text)

    def _clear_status(self):
        if self.is_tty and self._status_len:
            sys.stdout.write("\r" + " " * self._status_len + "\r")
            sys.stdout.flush()
            self._status_len = 0


def _display_width(s):
    # 全角文字は 2 桁として数える(上書き時の消し残し防止)
    return sum(2 if ord(c) > 0x2E7F else 1 for c in s)


def fmt(sec):
    sec = max(0, int(round(sec)))
    return f"{sec // 60}:{sec % 60:02d}"


# ---------------------------------------------------------------- 通知

class Notifier:
    """ntfy へ POST する。送信は別スレッドで行い、めくりを遅らせない。"""

    def __init__(self, server, topic, enabled, console):
        self.url = server.rstrip("/") + "/" + topic
        self.enabled = enabled
        self.console = console
        self._threads = []

    @staticmethod
    def _encode_header(value):
        # ntfy は RFC 2047 でエンコードしたヘッダを受け付ける(docs.ntfy.sh/publish)。
        # urllib は非 ASCII ヘッダを送れないので、日本語タイトルは Base64 で包む。
        try:
            value.encode("ascii")
            return value
        except UnicodeEncodeError:
            b64 = base64.b64encode(value.encode("utf-8")).decode("ascii")
            return f"=?UTF-8?B?{b64}?="

    def send_sync(self, title, message, priority="default", tags=""):
        """同期送信。HTTP ステータスを返す(失敗時は None)。"""
        headers = {
            "Title": self._encode_header(title),
            "Priority": priority,
            "Content-Type": "text/plain; charset=utf-8",
        }
        if tags:
            headers["Tags"] = tags
        req = urllib.request.Request(
            self.url, data=message.encode("utf-8"), headers=headers, method="POST")
        try:
            with urllib.request.urlopen(req, timeout=NOTIFY_TIMEOUT) as res:
                return res.status
        except urllib.error.HTTPError as e:
            self.console.log(f"[通知失敗] HTTP {e.code}: {title}")
            return e.code
        except Exception as e:  # ネットワーク断・タイムアウトはログだけ
            self.console.log(f"[通知失敗] {type(e).__name__}: {e}: {title}")
            return None

    def send(self, title, message, priority="default", tags=""):
        if not self.enabled:
            return
        t = threading.Thread(
            target=self.send_sync, args=(title, message, priority, tags), daemon=True)
        t.start()
        self._threads.append(t)

    def wait(self, timeout=NOTIFY_TIMEOUT + 1):
        end = time.monotonic() + timeout
        for t in self._threads:
            t.join(max(0, end - time.monotonic()))


# ---------------------------------------------------------------- Keynote

class KeynoteError(Exception):
    pass


def osascript(script):
    try:
        r = subprocess.run(["osascript", "-e", script], capture_output=True,
                           text=True, timeout=OSASCRIPT_TIMEOUT)
    except subprocess.TimeoutExpired:
        raise KeynoteError("osascript が応答しません(権限ダイアログが出ていないか確認してください)")
    if r.returncode != 0:
        raise KeynoteError(r.stderr.strip() or f"osascript 終了コード {r.returncode}")
    return r.stdout.strip()


class Keynote:
    def is_running(self):
        return osascript('application "Keynote" is running') == "true"

    def document_count(self):
        return int(osascript('tell application "Keynote" to count documents'))

    def document_name(self):
        return osascript('tell application "Keynote" to name of front document')

    def slide_count(self):
        return int(osascript('tell application "Keynote" to count slides of front document'))

    def start(self):
        osascript('tell application "Keynote" to start front document '
                  'from slide 1 of front document')

    def show_next(self):
        osascript('tell application "Keynote" to show next')

    def state(self):
        """(再生中か, 現在スライド番号 1 始まり) を返す。"""
        out = osascript(
            'tell application "Keynote"\n'
            '  if playing then\n'
            '    set n to slide number of current slide of front document\n'
            '    return "1|" & n\n'
            '  else\n'
            '    return "0|0"\n'
            '  end if\n'
            'end tell')
        playing, num = out.split("|")
        return playing == "1", int(num)


# ---------------------------------------------------------------- キー入力

class KeyReader:
    """ターミナルがフォーカスのときだけ 1 文字ずつ読む(termios の cbreak)。"""

    def __init__(self):
        self.enabled = sys.stdin.isatty()
        self._old = None

    def __enter__(self):
        if self.enabled:
            import termios
            import tty
            fd = sys.stdin.fileno()
            self._old = termios.tcgetattr(fd)
            tty.setcbreak(fd)
        return self

    def __exit__(self, *exc):
        if self._old is not None:
            import termios
            termios.tcsetattr(sys.stdin.fileno(), termios.TCSADRAIN, self._old)

    def read(self):
        if not self.enabled:
            return None
        r, _, _ = select.select([sys.stdin], [], [], 0)
        if r:
            return os.read(sys.stdin.fileno(), 1).decode(errors="ignore")
        return None


# ---------------------------------------------------------------- 本体

def load_schedule(path):
    with open(path, encoding="utf-8") as f:
        s = json.load(f)
    slides = s.get("slides") or []
    if not slides:
        raise SystemExit("schedule.json の slides が空です")
    for i, sl in enumerate(slides, 1):
        if "title" not in sl or "sec" not in sl or float(sl["sec"]) <= 0:
            raise SystemExit(f"slides[{i}] に title と正の sec が必要です: {sl}")
    if not s.get("ntfy_topic"):
        raise SystemExit("schedule.json に ntfy_topic がありません")
    s.setdefault("ntfy_server", "https://ntfy.sh")
    s.setdefault("warn_before_sec", 10)
    return s


def confirm(prompt):
    if not sys.stdin.isatty():
        return False
    try:
        return input(prompt).strip().lower() in ("y", "yes")
    except EOFError:
        return False


def preflight(kn, n_schedule, no_start):
    """開始前チェック。問題があれば分かるメッセージで止める。"""
    if not kn.is_running():
        raise SystemExit("Keynote が起動していません。発表用の書類を開いてから実行してください。")
    if kn.document_count() == 0:
        raise SystemExit("Keynote で書類が開かれていません。発表用の書類を開いてから実行してください。")
    name = kn.document_name()
    n = kn.slide_count()
    print(f"Keynote 書類: {name}(スライド {n} 枚 / 時間表 {n_schedule} 枚)")
    if n != n_schedule:
        print(f"警告: スライド枚数({n})と時間表の枚数({n_schedule})が一致しません。")
        if not confirm("このまま続行しますか? [y/N] "):
            raise SystemExit("中止しました。")
    if no_start:
        playing, _ = kn.state()
        if not playing:
            raise SystemExit("--no-start が指定されていますが、スライドショーが再生されていません。")


def run(args):
    sched = load_schedule(args.schedule)
    slides = sched["slides"]
    n = len(slides)
    total = sum(float(s["sec"]) for s in slides)
    warn = float(sched["warn_before_sec"])
    con = Console()

    notify_enabled = (not args.dry_run) or args.notify
    notifier = Notifier(sched["ntfy_server"], sched["ntfy_topic"], notify_enabled, con)

    if args.test_notify:
        st = notifier.send_sync("テスト通知", "ページめくりタイマーのテスト通知です。Watch に届けば準備完了。",
                                priority="high", tags="bell")
        print(f"送信先: {notifier.url}\nHTTP ステータス: {st}")
        return 0 if st == 200 else 1

    kn = None if args.dry_run else Keynote()
    if kn:
        try:
            preflight(kn, n, args.no_start)
        except KeynoteError as e:
            raise SystemExit(f"Keynote の操作に失敗しました: {e}")

    speed = args.speed
    mode = "ドライラン" if args.dry_run else "本番"
    print(f"[{mode}] スライド {n} 枚 / 合計 {fmt(total)} / {speed:g} 倍速 / "
          f"通知 {'あり' if notify_enabled else 'なし'}(topic: {sched['ntfy_topic']})")
    if sys.stdin.isatty():
        print("操作: p = 一時停止/再開, q = 終了(このターミナルにフォーカスがあるときだけ効きます)")

    for c in (3, 2, 1):
        print(f"開始まで {c}...", flush=True)
        time.sleep(1)

    if kn and not args.no_start:
        try:
            kn.start()
            deadline = time.monotonic() + 5
            while time.monotonic() < deadline:
                playing, _ = kn.state()
                if playing:
                    break
                time.sleep(0.2)
            else:
                raise SystemExit("スライドショーが開始されませんでした。")
        except KeynoteError as e:
            raise SystemExit(f"スライドショーの開始に失敗しました: {e}")

    # 仮想時計: 一時停止中は進まず、--speed 倍で進む
    sim = 0.0
    idx = 0                 # 現在スライド(0 始まり)
    slide_start = 0.0       # 現在スライドが表示された仮想時刻
    warned = False          # このスライド表示中に「あと N 秒」を送ったか
    last_minute_sent = False
    paused = False
    last_real = time.monotonic()
    next_poll = 0.0
    # 自分で show next した直後は Keynote の反映が遅れて古い番号が返ることがある。
    # その間は「手動で戻した」と誤判定しないよう、期待する番号と猶予期限を持つ。
    expect_num = None
    expect_deadline = 0.0

    if kn:
        try:
            _, num = kn.state()
            idx = min(max(num, 1), n) - 1
        except KeynoteError as e:
            con.log(f"[警告] 現在スライドの取得に失敗: {e}")

    con.log(f"[{fmt(sim)}] 開始: {idx + 1}/{n} {slides[idx]['title']}")

    def enter_slide(new_idx, reason, start=None):
        # 自動めくりでは予定時刻(start)から数え、ループの刻みによる遅れを積み上げない。
        # 手動で移動したときは検出した時刻から数え直す。
        nonlocal idx, slide_start, warned
        idx = new_idx
        slide_start = sim if start is None else start
        warned = False
        con.log(f"[{fmt(sim)}] {reason}: {idx + 1}/{n} {slides[idx]['title']}"
                f"({fmt(float(slides[idx]['sec']))})")

    with KeyReader() as keys:
        while True:
            now = time.monotonic()
            dt = now - last_real
            last_real = now
            if not paused:
                sim += dt * speed

            # キー入力
            k = keys.read()
            if k in ("q", "Q"):
                con.log(f"[{fmt(sim)}] q で終了しました")
                break
            if k in ("p", "P"):
                paused = not paused
                con.log(f"[{fmt(sim)}] {'一時停止' if paused else '再開'}")

            # Keynote の実際のスライド番号に追従(一時停止中も続ける)
            if kn and now >= next_poll:
                next_poll = now + POLL_INTERVAL
                try:
                    playing, num = kn.state()
                except KeynoteError as e:
                    con.log(f"[警告] Keynote の状態取得に失敗: {e}")
                    playing, num = True, idx + 1
                if not playing:
                    con.log(f"[{fmt(sim)}] スライドショーが終了したので停止します")
                    break
                if expect_num is not None:
                    if num == expect_num or now >= expect_deadline:
                        expect_num = None
                    elif num == expect_num - 1:
                        num = expect_num  # まだ反映前。古い番号は無視する
                if 1 <= num <= n and num - 1 != idx:
                    expect_num = None
                    enter_slide(num - 1, "手動で移動")
                elif num > n:
                    con.log(f"[警告] 時間表にないスライド {num} が表示されています")

            elapsed = sim - slide_start
            sec = float(slides[idx]["sec"])
            remain_slide = sec - elapsed
            remain_total = total - sim
            is_last = idx == n - 1

            if not paused:
                # 全体の残り 1 分
                if not last_minute_sent and remain_total <= LAST_MINUTE_SEC:
                    last_minute_sent = True
                    con.log(f"[{fmt(sim)}] 通知: 残り1分")
                    notifier.send("残り1分", f"全体の残り1分です(現在 {idx + 1}/{n} {slides[idx]['title']})",
                                  priority="high", tags="hourglass")

                # めくる warn 秒前
                if not is_last and not warned and remain_slide <= warn:
                    warned = True
                    nxt = slides[idx + 1]["title"]
                    con.log(f"[{fmt(sim)}] 通知: あと{int(warn)}秒 → 次: {nxt}")
                    notifier.send(f"あと{int(warn)}秒", f"あと{int(warn)}秒 → 次: {nxt}",
                                  priority="high", tags="arrow_right")

                # 時間到達
                if remain_slide <= 0:
                    if is_last:
                        con.log(f"[{fmt(sim)}] 通知: 終了時間です")
                        notifier.send("終了時間です", f"終了時間です(合計 {fmt(total)})",
                                      priority="urgent", tags="rotating_light")
                        break
                    if kn:
                        try:
                            kn.show_next()
                            expect_num = idx + 2
                            expect_deadline = time.monotonic() + 2.0
                        except KeynoteError as e:
                            con.log(f"[警告] めくりに失敗: {e}")
                    enter_slide(idx + 1, "めくり", start=slide_start + sec)
                    remain_slide = float(slides[idx]["sec"]) - (sim - slide_start)

            state = "一時停止中 " if paused else ""
            con.status(f"{state}{idx + 1}/{n} {slides[idx]['title']} | "
                       f"このスライド残り {fmt(remain_slide)} | "
                       f"全体 {fmt(sim)} / 残り {fmt(remain_total)}")
            time.sleep(TICK)

    notifier.wait()
    return 0


def main():
    ap = argparse.ArgumentParser(description="Keynote を時間表どおりにめくり、ntfy で通知する")
    ap.add_argument("schedule", help="時間表 JSON(schedule.json)")
    ap.add_argument("--no-start", action="store_true", help="既に再生中のスライドショーを使う")
    ap.add_argument("--dry-run", action="store_true", help="Keynote を操作せず流れだけログに出す")
    ap.add_argument("--notify", action="store_true", help="--dry-run でも通知を送る")
    ap.add_argument("--speed", type=float, default=1.0, help="時間を N 倍速で進める(リハーサル用)")
    ap.add_argument("--test-notify", action="store_true", help="通知を 1 件送って終了")
    args = ap.parse_args()
    if args.speed <= 0:
        ap.error("--speed は正の数にしてください")
    try:
        sys.exit(run(args))
    except KeyboardInterrupt:
        print("\n中断しました")
        sys.exit(130)


if __name__ == "__main__":
    main()
