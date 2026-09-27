#!/usr/bin/env python3
"""whisper-cli の S{n}.srt を Talksribe 形式のテキストに変換する(標準ライブラリのみ)。

壁時計 = 配信開始(2026-09-26 09:45:01) + 切り出しオフセット + srt の開始時刻

出力:
  <stock>/<NN_slug>/source/transcripts/20260926_S{n}_<slug>.txt   (Talksribe 形式)
  <stock>/<NN_slug>/source/transcripts/S{n}.srt / .vtt / .txt     (元ファイルのコピー)

Talksribe 形式:
  1 行目: # Talksribe 録音 YYYY-MM-DD HH:MM:SS   (切り出し開始の壁時計)
  空行
  [HH:MM:SS] 本文

使い方:
  python3 srt_to_talksribe.py --src ~/Downloads/vimeo_event_6144090/sessions \
      --stock stock/BlenderFes2026 --sessions 1-8

  # Day2(配信開始 2026-09-27 09:45:01、S1〜S8 はディレクトリ 09_〜16_、出力名は 20260927_S{n}_<slug>.txt)
  python3 srt_to_talksribe.py --src ~/Downloads/vimeo_event_6213809/sessions \
      --stock stock/BlenderFes2026 --sessions 1-8 \
      --stream-start "2026-09-27 09:45:01" --dir-offset 8
"""
from __future__ import annotations

import argparse
import re
import shutil
import sys
from datetime import datetime, timedelta
from pathlib import Path

DEFAULT_STREAM_START = "2026-09-26 09:45:01"
DEFAULT_OFFSETS = "1:599,2:5099,3:9599,4:14099,5:18599,6:23099,7:27599,8:32099"

TIME_RE = re.compile(r"(\d+):(\d{2}):(\d{2})[,.](\d{3})\s*-->")


def parse_offsets(text: str) -> dict[int, float]:
    """"1:599,2:5099" 形式のオフセット表を dict にする。"""
    table: dict[int, float] = {}
    for item in text.split(","):
        item = item.strip()
        if not item:
            continue
        n, sec = item.split(":", 1)
        table[int(n)] = float(sec)
    return table


def parse_sessions(text: str) -> list[int]:
    """"1-8" や "1,3,5" を番号リストにする。"""
    result: list[int] = []
    for part in text.split(","):
        part = part.strip()
        if "-" in part:
            a, b = part.split("-", 1)
            result.extend(range(int(a), int(b) + 1))
        elif part:
            result.append(int(part))
    return result


def parse_srt(path: Path) -> list[tuple[float, str]]:
    """srt を (開始秒, 本文) のリストにする。本文の複数行は空白で連結する。"""
    cues: list[tuple[float, str]] = []
    blocks = re.split(r"\n\s*\n", path.read_text(encoding="utf-8").replace("\r\n", "\n"))
    for block in blocks:
        lines = [ln for ln in block.strip().split("\n") if ln.strip()]
        for i, ln in enumerate(lines):
            m = TIME_RE.match(ln.strip())
            if m:
                h, mi, s, ms = (int(x) for x in m.groups())
                text = " ".join(x.strip() for x in lines[i + 1:]).strip()
                if text:
                    cues.append((h * 3600 + mi * 60 + s + ms / 1000, text))
                break
    return cues


def find_session_dir(stock: Path, n: int) -> Path:
    matches = sorted(p for p in stock.glob(f"{n:02d}_*") if p.is_dir())
    if len(matches) != 1:
        raise SystemExit(f"セッション {n:02d} のディレクトリが一意に見つかりません: {matches}")
    return matches[0]


def convert(n: int, offset: float, stream_start: datetime, src: Path, stock: Path,
            dir_offset: int = 0) -> Path | None:
    srt = src / f"S{n}.srt"
    if not srt.exists():
        print(f"[skip] S{n}: {srt} がありません", file=sys.stderr)
        return None
    sdir = find_session_dir(stock, n + dir_offset)
    slug = sdir.name.split("_", 1)[1]
    out_dir = sdir / "source" / "transcripts"
    out_dir.mkdir(parents=True, exist_ok=True)

    clip_start = stream_start + timedelta(seconds=offset)
    cues = parse_srt(srt)
    lines = [f"# Talksribe 録音 {clip_start:%Y-%m-%d %H:%M:%S}", ""]
    for sec, text in cues:
        wall = clip_start + timedelta(seconds=sec)
        lines.append(f"[{wall:%H:%M:%S}] {text}")
    out = out_dir / f"{clip_start:%Y%m%d}_S{n}_{slug}.txt"
    out.write_text("\n".join(lines) + "\n", encoding="utf-8")

    for ext in ("srt", "vtt", "txt"):
        f = src / f"S{n}.{ext}"
        if f.exists():
            shutil.copy2(f, out_dir / f.name)
    print(f"[ok] S{n}: {len(cues)} cues -> {out}")
    return out


def main() -> int:
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("--src", type=Path, required=True, help="S{n}.srt があるディレクトリ")
    ap.add_argument("--stock", type=Path, required=True, help="stock/BlenderFes2026 のパス")
    ap.add_argument("--sessions", default="1-8", help="対象セッション番号(例: 1-8, 1,3)")
    ap.add_argument("--offsets", default=DEFAULT_OFFSETS, help="番号:配信内オフセット秒 のカンマ区切り")
    ap.add_argument("--stream-start", default=DEFAULT_STREAM_START, help="配信開始の壁時計")
    ap.add_argument("--dir-offset", type=int, default=0,
                    help="セッション番号に足してディレクトリ番号にする値(Day2 は 8 で S1→09_)")
    args = ap.parse_args()

    stream_start = datetime.strptime(args.stream_start, "%Y-%m-%d %H:%M:%S")
    offsets = parse_offsets(args.offsets)
    missing = 0
    for n in parse_sessions(args.sessions):
        if n not in offsets:
            raise SystemExit(f"S{n} のオフセットがありません")
        if convert(n, offsets[n], stream_start, args.src.expanduser(), args.stock,
                   args.dir_offset) is None:
            missing += 1
    return 1 if missing else 0


if __name__ == "__main__":
    sys.exit(main())
