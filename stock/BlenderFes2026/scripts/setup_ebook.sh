#!/bin/bash
# Blender Fes 2026 AW Day1 の記事 8 本を Re:VIEW の 1 冊にまとめる（#5）
#
# stock/GDC2026/setup_ebooks_v2.sh と同じ流儀:
#   雛形展開 → md2review.py で章変換 → 画像コピー → catalog.yml / config.yml 書き換え
# 再実行可能。既存の出力ディレクトリには上書きで同期する（rm -rf は使わない）。
#
# 使い方:
#   bash stock/BlenderFes2026/scripts/setup_ebook.sh
#   cd stock/BlenderFes2026/ebook/blenderfes2026-day1
#   rake prepare && review-pdfmaker _2.5.0_ config.yml   # ローカル（Re:VIEW 2.5.0 gem が必要）
#   ./local-ci.sh build                                  # Docker（kauplan/review2.5）
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

BOOKNAME="blenderfes2026-day1"
BOOKTITLE="Blender Fes 2026 AW セッションレポート Day1"
SUBTITLE="Blender で描く、動かす、盛る"
PUBDATE="2026-09-27"
BOOK_DIR="$STOCK_DIR/ebook/$BOOKNAME"

# セッションディレクトリ:章名
SESSIONS=(
  "01_KaguyahimeCGBackground_QoonPlant:01-kaguyahime"
  "02_CharacterShading_Eight:02-charshading"
  "03_MoruReal2_JennyKaori:03-moru"
  "04_AnimeLook_SuminekoBebe:04-animelook"
  "05_KireAction_Tatsumura:05-kireaction"
  "06_Effects_Gyunyubin:06-effects"
  "07_Rigging_minusT:07-rigging"
  "08_Addons_3Dnin:08-addons"
)

echo "=== Setting up $BOOKNAME ==="
echo "  stock:    $STOCK_DIR"
echo "  template: $TEMPLATE_DIR"
echo "  output:   $BOOK_DIR"

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
# aut は雛形のまま（GDC2026-Vol2 と同じ dsgarage Games / ディーズガレージゲームス）
if 'name:    dsgarage Games' not in s:
    sys.exit('config.yml: aut が想定と異なる')
open(path, 'w', encoding='utf-8').write(s)
PYEOF

# 4. 各セッションを変換
catalog_chaps=""
for entry in "${SESSIONS[@]}"; do
  session_dir="${entry%%:*}"
  re_name="${entry##*:}"
  src_dir="$STOCK_DIR/$session_dir"

  md_file=$(ls "$src_dir"/20260926_BlenderFes2026AW_S*.md 2>/dev/null | head -1 || true)
  if [ -z "$md_file" ]; then
    echo "  WARNING: 記事が見つからないため除外: $session_dir"
    continue
  fi

  echo "  Converting: $session_dir -> contents/$re_name.re"
  python3 "$CONVERTER" "$md_file" "$BOOK_DIR/contents/$re_name.re" ""

  # 画像は記事用の images/ のみ（source/ は含めない）。章ディレクトリ内だけ同期削除する
  mkdir -p "$BOOK_DIR/images/$re_name"
  rsync -a --delete --exclude '.gitkeep' --exclude '.DS_Store' \
    "$src_dir/images/" "$BOOK_DIR/images/$re_name/"

  catalog_chaps="$catalog_chaps  - $re_name.re"$'\n'
done

# 5. まえがき・あとがき（stock 側に原稿がある場合のみ使う。無ければ既存を残す）
for f in 00-preface.re 99-postface.re; do
  if [ -f "$SCRIPT_DIR/ebook_src/$f" ]; then
    cp "$SCRIPT_DIR/ebook_src/$f" "$BOOK_DIR/contents/$f"
  elif [ ! -f "$BOOK_DIR/contents/$f" ]; then
    echo "  WARNING: $f がありません"
  fi
done

# 6. catalog.yml
catalog_chaps="${catalog_chaps%$'\n'}"
cat > "$BOOK_DIR/catalog.yml" <<CATEOF
# -*- coding: utf-8 -*-
PREDEF:
  - 00-preface.re
CHAPS:
${catalog_chaps}
APPENDIX:
POSTDEF:
  - 99-postface.re
CATEOF

# 7. 画像参照の検証（//image[id] が images/<章名>/id.* に実在するか）
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
echo "  image refs: $total, missing: $missing"
if [ "$missing" -ne 0 ]; then
  exit 1
fi

echo ""
echo "=== Complete: $BOOK_DIR ==="
echo "Build: cd $BOOK_DIR && rake prepare && review-pdfmaker _2.5.0_ config.yml"
