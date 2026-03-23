#!/usr/bin/env python3
"""
GDC2026 記事を Note 有料記事フォーマットに変換するスクリプト。

構成:
- 無料エリア: タイトル、リード文、セッション概要テーブル、目次（キーポイント）
- ===== 有料ライン =====
- 有料エリア: 詳細本文（リード・セッション概要を除いた残り全体）

Note の仕様:
- 画像は Note エディタで別途アップロード
- 有料ラインは Note エディタ上で設定（このスクリプトではマーカーを挿入）
"""

import os
import re
import glob
import yaml

BASE_DIR = "/Users/daisuketsukada/Documents/dsgarageBlog/stock/GDC2026"
OUT_DIR = os.path.join(BASE_DIR, "note")


def parse_frontmatter(text: str):
    """YAML frontmatter をパースして (metadata_dict, body) を返す"""
    if text.startswith("---"):
        parts = text.split("---", 2)
        if len(parts) >= 3:
            meta = yaml.safe_load(parts[1])
            body = parts[2].strip()
            return meta or {}, body
    return {}, text.strip()


def split_sections(body: str):
    """本文を ## 単位のセクションに分割して [(heading, content)] を返す。
    最初の ## の前は heading="" の intro セクション。"""
    sections = []
    current_heading = ""
    current_lines = []

    for line in body.split("\n"):
        if line.startswith("## "):
            # 前のセクションを保存
            sections.append((current_heading, "\n".join(current_lines)))
            current_heading = line
            current_lines = []
        else:
            current_lines.append(line)

    sections.append((current_heading, "\n".join(current_lines)))
    return sections


def is_session_info_section(heading: str) -> bool:
    """セッション概要/情報セクションかどうか"""
    return bool(re.match(r"##\s*セッション(?:概要|情報)", heading))


def is_speaker_section(heading: str) -> bool:
    """スピーカー紹介セクションかどうか"""
    return bool(re.match(r"##\s*スピーカー(?:紹介)?", heading))


def remove_images(text: str) -> str:
    """画像参照とそのキャプション行を除去"""
    lines = text.split("\n")
    result = []
    skip_next_caption = False
    for line in lines:
        if re.match(r"^!\[.*?\]\(.*?\)", line):
            skip_next_caption = True
            continue
        if skip_next_caption and line.strip().startswith("*") and line.strip().endswith("*"):
            skip_next_caption = False
            continue
        skip_next_caption = False
        result.append(line)
    return "\n".join(result)


def strip_title_line(text: str) -> str:
    """先頭の # タイトル行と空行を除去"""
    lines = text.split("\n")
    start = 0
    for i, line in enumerate(lines):
        stripped = line.strip()
        if stripped.startswith("# ") and not stripped.startswith("## "):
            start = i + 1
            continue
        if stripped == "" and i == start:
            start = i + 1
            continue
        break
    return "\n".join(lines[start:])


def count_images(body: str) -> int:
    return len(re.findall(r"!\[.*?\]\(.*?\)", body))


def extract_headings_for_toc(sections) -> list:
    """目次用の見出しリスト（セッション概要・スピーカー紹介・おわりに除外）"""
    skip_keywords = ["セッション概要", "セッション情報", "スピーカー紹介", "おわりに"]
    headings = []
    for heading, _ in sections:
        if not heading:
            continue
        text = heading.lstrip("# ").strip()
        if any(kw in text for kw in skip_keywords):
            continue
        headings.append(text)
    return headings


def convert_article(md_path: str, session_name: str) -> str:
    """単一記事を Note フォーマットに変換"""
    with open(md_path, "r", encoding="utf-8") as f:
        text = f.read()

    meta, body = parse_frontmatter(text)
    title = meta.get("title", "")

    sections = split_sections(body)
    img_count = count_images(body)
    word_count = len(body)

    # セクション分類
    intro_text = ""  # 最初の ## の前（リード文）
    session_info_text = ""  # セッション概要
    paid_sections = []  # 有料エリアに入るセクション

    for heading, content in sections:
        if heading == "":
            # intro（# タイトル行 + リード文）
            intro_text = strip_title_line(content).strip()
        elif is_session_info_section(heading):
            session_info_text = (heading + "\n" + content).strip()
        elif is_speaker_section(heading):
            # スピーカー紹介は有料エリアの先頭に
            paid_sections.insert(0, (heading, content))
        else:
            paid_sections.append((heading, content))

    # 目次用見出し
    toc_headings = extract_headings_for_toc(sections)

    # ====== Note 記事の組み立て ======
    parts = []

    # ── 無料エリア ──
    parts.append(f"# {title}\n")

    # リード文（画像除去）
    if intro_text:
        free_lead = remove_images(intro_text)
        parts.append(free_lead)
        parts.append("")

    # セッション概要テーブル
    if session_info_text:
        parts.append(session_info_text)
        parts.append("")

    # 目次
    if toc_headings:
        parts.append("### この記事で読めること\n")
        for h in toc_headings[:7]:
            clean = re.sub(r"^\d+\.\s*", "", h)
            # --- 区切りや装飾を除去
            clean = clean.split(" -- ")[0].split(" — ")[0].strip()
            parts.append(f"- **{clean}**")
        if len(toc_headings) > 7:
            parts.append(f"- ...ほか全{len(toc_headings)}セクション")
        parts.append("")

    # ボリューム表示
    parts.append(f"> **本記事のボリューム**: 約{word_count:,}文字 / スライド画像{img_count}枚")
    parts.append("> スピーカーのトランスクリプト（発言の文字起こし）を原文・日本語訳つきで完全収録しています。")
    parts.append("")

    # ── 有料ライン ──
    parts.append("---")
    parts.append("")
    parts.append("<!-- ===== ここから有料エリア（Note エディタで有料ラインを設定） ===== -->")
    parts.append("")

    # ── 有料エリア ──
    for heading, content in paid_sections:
        parts.append(heading)
        parts.append(content)

    # フッター
    parts.append("\n---\n")
    parts.append("## おわりに")
    parts.append("")
    parts.append("最後までお読みいただきありがとうございます。GDC 2026 の他のセッションレポートも順次公開していますので、ぜひフォローしてお待ちください。")
    parts.append("")
    parts.append("**dsgarage Games** | GDC 2026 現地レポート")
    parts.append("")

    return "\n".join(parts)


def main():
    os.makedirs(OUT_DIR, exist_ok=True)

    session_dirs = sorted(glob.glob(os.path.join(BASE_DIR, "*/")))
    converted = []

    for session_dir in session_dirs:
        name = os.path.basename(session_dir.rstrip("/"))
        if name in ("ebook", "note"):
            continue

        md_files = glob.glob(os.path.join(session_dir, "20260*.md"))
        if not md_files:
            print(f"  SKIP: {name} (no article found)")
            continue

        md_path = md_files[0]
        print(f"  Converting: {name}")

        try:
            note_text = convert_article(md_path, name)
            out_path = os.path.join(OUT_DIR, f"note_{name}.md")
            with open(out_path, "w", encoding="utf-8") as f:
                f.write(note_text)

            # 文字数と画像数を記録
            with open(md_path, "r", encoding="utf-8") as f:
                orig = f.read()
            wc = len(orig)
            ic = count_images(orig)
            converted.append((name, out_path, wc, ic))
        except Exception as e:
            print(f"  ERROR: {name} - {e}")
            import traceback
            traceback.print_exc()

    # インデックスファイル生成
    index_lines = [
        "# GDC 2026 Note 有料記事一覧\n",
        f"変換済み: {len(converted)} 記事\n",
        "## 公開チェックリスト\n",
        "各記事について以下を確認してから Note にペーストしてください:\n",
        "1. サムネイル画像を設定（各セッションの `source/images/` から選択）",
        "2. 有料ラインの位置を `<!-- ===== ここから有料エリア -->` の箇所に設定",
        "3. 本文中の `![alt](path)` 画像を Note エディタ上で挿入",
        "4. 価格を設定（目安: 短い記事 300円 / 標準 500円 / 大ボリューム 800円）",
        "5. タグを設定（推奨: `GDC2026` `ゲーム開発` `ゲームデザイン` `GDC`）\n",
        "## 記事一覧\n",
        "| # | セッション | 文字数 | 画像 | ファイル |",
        "|:--|:--|--:|--:|:--|",
    ]
    for i, (name, path, wc, ic) in enumerate(converted, 1):
        index_lines.append(
            f"| {i} | {name} | {wc:,} | {ic} | `note_{name}.md` |"
        )

    index_path = os.path.join(OUT_DIR, "README.md")
    with open(index_path, "w", encoding="utf-8") as f:
        f.write("\n".join(index_lines) + "\n")

    print(f"\n=== Done: {len(converted)} articles converted ===")
    print(f"Output: {OUT_DIR}/")


if __name__ == "__main__":
    main()
