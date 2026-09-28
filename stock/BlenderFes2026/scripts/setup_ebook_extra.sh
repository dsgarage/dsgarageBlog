#!/bin/bash
# Blender Fes Extra「3D衣装の作り方」の統合記事 1 本を Re:VIEW の単巻にする（#14）
#
# Day1 の setup_ebook.sh（#5）と同じ流儀:
#   雛形展開 → 記事を book_outline.md の章立てで分割 → md2review.py で章変換 → 画像コピー
#   → catalog.yml / config.yml 書き換え → 画像参照の検証
# 再実行可能。既存の出力ディレクトリには上書きで同期する（ディレクトリごとの削除はしない）。
#
# 使い方:
#   bash stock/BlenderFes2026/scripts/setup_ebook_extra.sh
#   cd stock/BlenderFes2026/ebook/blenderfes-extra-3dcostume
#   rake prepare && review-pdfmaker _2.5.0_ config.yml   # ローカル（Re:VIEW 2.5.0 gem が必要）
#
# 環境変数で上書きできるパス:
#   STOCK_DIR     記事の置き場（既定: このスクリプトの 1 つ上 = stock/BlenderFes2026）
#   TEMPLATE_DIR  Re:VIEW Starter 雛形
#   REFBOOK_DIR   CLAUDE.md / .claude/rules / STYLEGUIDE.md / ビルド手順のコピー元（GDC2026-Vol2）
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
STOCK_DIR="${STOCK_DIR:-$(cd "$SCRIPT_DIR/.." && pwd)}"
TEMPLATE_DIR="${TEMPLATE_DIR:-/Users/daisuketsukada/Documents/dsgarageBooks/Game/ReVIEWStarterTemplate}"
REFBOOK_DIR="${REFBOOK_DIR:-/Users/daisuketsukada/Documents/dsgarageBooks/Game/GDC2026-Vol2}"
CONVERTER="$SCRIPT_DIR/md2review.py"
SPLITTER="$SCRIPT_DIR/split_article_by_outline.py"
SRC_EXTRA="$SCRIPT_DIR/ebook_src_extra"

BOOKNAME="blenderfes-extra-3dcostume"
BOOKTITLE="Blender Fes Extra 3D衣装の作り方 講座レポート"
SUBTITLE="モデリングから Unity・lilToon まで"
PUBDATE="2026-09-28"
BOOK_DIR="$STOCK_DIR/ebook/$BOOKNAME"
# 分割した中間 Markdown の置き場（書籍ディレクトリの外。ebook/ ごと gitignore 済み）
SPLIT_DIR="$STOCK_DIR/ebook/_split-$BOOKNAME"

SESSION_DIR="$STOCK_DIR/17_Extra_3DCostume"
ARTICLE="$SESSION_DIR/20260928_BlenderFesExtra_3D衣装の作り方.md"
OUTLINE="$SESSION_DIR/book_outline.md"

# 付録（ebook_src_extra/ の手書き原稿）
APPENDICES=(91-glossary 92-where-to-open 93-todo-figures)

echo "=== Setting up $BOOKNAME ==="
echo "  article:  $ARTICLE"
echo "  template: $TEMPLATE_DIR"
echo "  output:   $BOOK_DIR"

[ -f "$ARTICLE" ] || { echo "ERROR: 記事がありません: $ARTICLE"; exit 1; }
[ -f "$OUTLINE" ] || { echo "ERROR: 章立てがありません: $OUTLINE"; exit 1; }

# 1. 雛形を展開（生成物・サンプル章・git 管理情報は除く）
mkdir -p "$BOOK_DIR"
rsync -a \
  --exclude '.git' \
  --exclude '.DS_Store' \
  --exclude 'node_modules/' \
  --exclude '*-pdf/' \
  --exclude '/*.pdf' \
  --exclude '/*.re' \
  --exclude '/contents/' \
  --exclude '/images/*/' \
  --exclude '/catalog.yml' \
  --exclude '/config.yml' \
  --exclude '/.claude/' \
  --exclude '/.secrets' \
  "$TEMPLATE_DIR/" "$BOOK_DIR/"
mkdir -p "$BOOK_DIR/contents" "$BOOK_DIR/images"

# Re:VIEW 2.5 は hook を直接実行するため実行権限が要る（雛形では 644 になっている）
chmod +x "$BOOK_DIR/lib/hooks/beforetexcompile.rb"

# 2. 執筆規約とビルド手順を GDC2026-Vol2 からコピー
for f in CLAUDE.md STYLEGUIDE.md local-ci.sh docker-compose.yml; do
  if [ -f "$REFBOOK_DIR/$f" ]; then
    cp "$REFBOOK_DIR/$f" "$BOOK_DIR/$f"
  fi
done
if [ -d "$REFBOOK_DIR/.claude/rules" ]; then
  mkdir -p "$BOOK_DIR/.claude/rules"
  rsync -a "$REFBOOK_DIR/.claude/rules/" "$BOOK_DIR/.claude/rules/"
fi

# 3. config.yml（雛形から毎回作り直す）
CONFIG="$BOOK_DIR/config.yml"
cp "$TEMPLATE_DIR/config.yml" "$CONFIG"
python3 - "$CONFIG" "$BOOKNAME" "$BOOKTITLE" "$SUBTITLE" "$PUBDATE" <<'PYEOF'
import re, sys
path, bookname, title, subtitle, date = sys.argv[1:]
s = open(path, encoding='utf-8').read()
def sub(pattern, repl):
    global s
    s, n = re.subn(pattern, lambda m: repl, s, count=1, flags=re.M)
    if n != 1:
        sys.exit(f"config.yml: pattern not found: {pattern}")
# Starter のレイアウト（layouts/layout.tex.erb, sty/）は Re:VIEW 2.5 向け。GDC2026-Vol2 と同じく 2.0 互換でビルドする
sub(r'^review_version: .*$', 'review_version: 2.0')
sub(r'^bookname: .*$', f'bookname: {bookname}')
sub(r'^booktitle: \|-\n  .*$', f'booktitle: "{title}"')
sub(r'^subtitle: \|-\n  .*$', f'subtitle: "{subtitle}"')
sub(r'^date: .*$', f'date: {date}')
sub(r'^    - \d{4}-\d{2}-\d{2} ver 1\.0 .*$', f'    - {date} ver 1.0')
sub(r'^pubevent_name: .*$', '#pubevent_name:')
sub(r'^rights: .*$', f'rights: (C) {date[:4]} dsgarage Games')
# 雛形の ext: review-ext.rb は Re:VIEW 5 では「原稿の拡張子」と解釈され、章が catalog と一致せず本文が空になる。
# GDC2026-Vol2 と同じく外す（review-ext.rb は基準ディレクトリにあれば自動で読み込まれる）
sub(r'^ext: review-ext\.rb.*$', '#ext: review-ext.rb  # Re:VIEW 5 では原稿の拡張子扱いになるため無効化')
# aut は雛形のまま（Day1・GDC2026-Vol2 と同じ dsgarage Games / ディーズガレージゲームス）
if 'name:    dsgarage Games' not in s:
    sys.exit('config.yml: aut が想定と異なる')
open(path, 'w', encoding='utf-8').write(s)
PYEOF

# 4. 記事を book_outline.md の章立てで分割する（記事本文は変えず、章の境目で切って見出しの階層だけ合わせる）
mkdir -p "$SPLIT_DIR"
echo "=== Splitting article by book_outline.md ==="
split_log="$SPLIT_DIR/split.log"
python3 "$SPLITTER" "$ARTICLE" "$OUTLINE" "$SPLIT_DIR" > "$split_log"
grep -v '^IMAGE' "$split_log" | grep -v '^    ' || true

# 5. 各章を変換し、その章で使う画像だけを images/<章名>/ にコピーする（source/ は含めない）
catalog_chaps=""
for md in "$SPLIT_DIR"/[0-9][0-9]-*.md; do
  chap="$(basename "$md" .md)"
  echo "  Converting: $chap.md -> contents/$chap.re"
  python3 "$CONVERTER" "$md" "$BOOK_DIR/contents/$chap.re" ""

  mkdir -p "$BOOK_DIR/images/$chap"
  includes=()
  while IFS=$'\t' read -r _ c img; do
    [ "$c" = "$chap" ] || continue
    src="$SESSION_DIR/$img"
    [ -f "$src" ] || { echo "ERROR: 画像がありません: $src"; exit 1; }
    includes+=(--include "/$(basename "$img")")
  done < <(grep '^IMAGE' "$split_log")
  # 章ディレクトリ内だけ同期削除する（参照されなくなった画像を残さない）
  rsync -a --delete --delete-excluded ${includes[@]+"${includes[@]}"} --exclude '*' \
    "$SESSION_DIR/images/" "$BOOK_DIR/images/$chap/"

  catalog_chaps="$catalog_chaps  - $chap.re"$'\n'
done

# 6. まえがき・あとがき・付録（ebook_src_extra/ の手書き原稿）
for f in 00-preface 99-postface "${APPENDICES[@]}"; do
  if [ -f "$SRC_EXTRA/$f.re" ]; then
    cp "$SRC_EXTRA/$f.re" "$BOOK_DIR/contents/$f.re"
  else
    echo "ERROR: $SRC_EXTRA/$f.re がありません"
    exit 1
  fi
done
appendix_chaps=""
for f in "${APPENDICES[@]}"; do
  appendix_chaps="$appendix_chaps  - $f.re"$'\n'
done

# 7. catalog.yml
catalog_chaps="${catalog_chaps%$'\n'}"
appendix_chaps="${appendix_chaps%$'\n'}"
cat > "$BOOK_DIR/catalog.yml" <<CATEOF
# -*- coding: utf-8 -*-
PREDEF:
  - 00-preface.re
CHAPS:
${catalog_chaps}
APPENDIX:
${appendix_chaps}
POSTDEF:
  - 99-postface.re
CATEOF

# 8. 画像参照の検証（//image[id] が images/<章名>/id.* に実在するか、記事の画像をすべて使っているか）
echo "=== Verifying image references ==="
missing=0
total=0
for re_file in "$BOOK_DIR"/contents/[0-9][0-9]-*.re; do
  chap="$(basename "$re_file" .re)"
  while IFS= read -r img_id; do
    total=$((total + 1))
    if ! ls "$BOOK_DIR/images/$chap/$img_id".* >/dev/null 2>&1; then
      echo "  MISSING: $chap: $img_id"
      missing=$((missing + 1))
    fi
  done < <(grep -oE '^//image\[[^]]+\]' "$re_file" | sed -E 's|^//image\[||; s|\]$||')
done
article_imgs=$(grep -cE '^!\[[^]]*\]\(images/' "$ARTICLE" || true)
echo "  image refs: $total (article: $article_imgs), missing: $missing"
if [ "$missing" -ne 0 ] || [ "$total" -ne "$article_imgs" ]; then
  echo "ERROR: 画像参照の検証に失敗しました"
  exit 1
fi

echo ""
echo "=== Complete: $BOOK_DIR ==="
echo "Build: cd $BOOK_DIR && rake prepare && review-pdfmaker _2.5.0_ config.yml"
