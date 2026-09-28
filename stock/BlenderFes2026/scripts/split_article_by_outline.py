#!/usr/bin/env python3
"""統合記事 1 本を book_outline.md の章立てに従って章ごとの Markdown に分割する(#14)

記事本文は変更しない。行の並びはそのままに、章の境目で切り、見出しの階層だけを章に合わせる。

  - 章タイトル(Re:VIEW の = 見出しになる # 行)は book_outline.md の章立て表「タイトル案」列から取る
  - 章の開始位置は、outline の「記事との対応」列に対応する記事の見出し(CHAPTERS の start)
  - 記事の H1 とフロントマターは捨てる(書名と重複するため)
  - 章の開始見出しが H2 のときは、その H2 行を章タイトルに置き換える(本文は章の導入として残す)
  - 置き換えた H2 の配下の H3 は H2 に繰り上げる。それ以外の H2(講座概要、まとめ等)はそのまま H2
  - 記事の冒頭(リード・講座概要)は第 1 章の頭に入れる

使い方:
  python3 split_article_by_outline.py <article.md> <book_outline.md> <out_dir>
  → <out_dir>/<章名>.md を書き出し、章名と画像ファイル名の対応を標準出力に出す
"""
import os
import re
import sys

# (章番号, 章名 = .re のファイル名, 開始見出しの行頭。None は記事の先頭から)
CHAPTERS = [
    (1, "01-overview", None),
    (2, "02-design", "## 1. 企画・型紙的発想とモデリング"),
    (3, "03-modeling", "### 前半 29:18〜"),
    (4, "04-uv", "### 前半 65:20〜"),
    (5, "05-sculpt", "## 2. ハイモデル"),
    (6, "06-bake", "## 3. ベイクのためのパーツ分け"),
    (7, "07-texture", "## 4. テクスチャ"),
    (8, "08-bone-weight", "## 5. ボーンとウェイト"),
    (9, "09-unity-liltoon", "## 6. Unity と lilToon"),
    (10, "10-workflow", "## 7. 講師のワークフロー観"),
]

HEADING_RE = re.compile(r"^(#{1,6})\s+(.+)$")
IMAGE_RE = re.compile(r"^!\[[^\]]*\]\(([^)]+)\)\s*$")


def read_outline_titles(outline_path):
    """book_outline.md の章立て表から {章番号: タイトル案} を返す"""
    titles = {}
    for line in open(outline_path, encoding="utf-8"):
        cells = [c.strip() for c in line.strip().strip("|").split("|")]
        if len(cells) >= 2 and re.fullmatch(r"\d+", cells[0]):
            titles[int(cells[0])] = cells[1]
    return titles


def strip_frontmatter(lines):
    if lines and lines[0].strip() == "---":
        for i in range(1, len(lines)):
            if lines[i].strip() == "---":
                return lines[i + 1:]
        sys.exit("frontmatter が閉じていません")
    return lines


def split(article_path, outline_path, out_dir):
    titles = read_outline_titles(outline_path)
    for num, _, _ in CHAPTERS:
        if num not in titles:
            sys.exit(f"book_outline.md に第 {num} 章のタイトルがありません")

    raw = open(article_path, encoding="utf-8").read().split("\n")
    lines = strip_frontmatter(raw)
    fm_offset = len(raw) - len(lines)  # 報告の行番号を記事ファイルの行番号にそろえる

    # 各章の開始行を探す(コードブロック内は見出しとみなさない)
    starts = {}
    in_code = False
    for idx, line in enumerate(lines):
        if line.strip().startswith("```"):
            in_code = not in_code
            continue
        if in_code:
            continue
        for num, name, marker in CHAPTERS:
            if marker and line.startswith(marker):
                if name in starts:
                    sys.exit(f"開始見出しが 2 回見つかりました: {marker}")
                starts[name] = idx
    for num, name, marker in CHAPTERS:
        if marker and name not in starts:
            sys.exit(f"開始見出しが見つかりません: {marker}")
    starts[CHAPTERS[0][1]] = 0

    order = [name for _, name, _ in CHAPTERS]
    positions = [starts[n] for n in order]
    if positions != sorted(positions):
        sys.exit("章の開始位置が記事の順序と一致しません")

    os.makedirs(out_dir, exist_ok=True)
    report = []
    total_images = []
    for k, (num, name, marker) in enumerate(CHAPTERS):
        begin = positions[k]
        end = positions[k + 1] if k + 1 < len(positions) else len(lines)
        body = lines[begin:end]

        out = [f"# {titles[num]}", ""]
        # H2 が章の開始なら、その H2 を章タイトルに置き換える。
        # 第 3・4 章は「## 1.」の配下の H3 から始まるので、章の頭から H3 を繰り上げる
        promote_h3 = marker is not None
        in_code = False
        mapped = []  # (元の見出し, 章内の見出し)
        for j, line in enumerate(body):
            if line.strip().startswith("```"):
                in_code = not in_code
                out.append(line)
                continue
            m = None if in_code else HEADING_RE.match(line)
            if not m:
                out.append(line)
                continue
            level, text = len(m.group(1)), m.group(2)
            if level == 1:
                continue  # 記事の H1 は捨てる(書名と重複)
            if j == 0 and marker is not None and level == 2:
                mapped.append((line, f"# {titles[num]}"))
                continue  # 章タイトルに置き換え済み
            if level == 2:
                promote_h3 = False
                new = line
            elif level >= 3 and promote_h3:
                new = "#" * (level - 1) + " " + text
            else:
                new = line
            mapped.append((line, new))
            out.append(new)

        # 章頭の余分な空行と、章末の区切り線・空行を落とす(区切り線は md2review でも捨てられる)
        while len(out) > 2 and out[2].strip() == "":
            del out[2]
        while out and (out[-1].strip() == "" or re.match(r"^---+\s*$", out[-1])):
            out.pop()
        out.append("")

        images = [IMAGE_RE.match(l).group(1) for l in out if IMAGE_RE.match(l)]
        total_images.extend(images)
        path = os.path.join(out_dir, f"{name}.md")
        with open(path, "w", encoding="utf-8") as f:
            f.write("\n".join(out))
        report.append((name, titles[num], begin, end, mapped, images))

    for name, title, begin, end, mapped, images in report:
        print(f"{name}\t{title}\tlines {begin + 1 + fm_offset}-{end + fm_offset}\timages {len(images)}")
        for src, dst in mapped:
            print(f"    {src}  ->  {dst}")
        for img in images:
            print(f"IMAGE\t{name}\t{img}")
    print(f"TOTAL_IMAGES\t{len(total_images)}")


if __name__ == "__main__":
    if len(sys.argv) != 4:
        print(__doc__)
        sys.exit(1)
    split(*sys.argv[1:])
