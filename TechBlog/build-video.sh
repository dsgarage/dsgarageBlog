#!/bin/bash
# Blog PR動画 Build Script
# Usage: ./build-video.sh 20251226_タイトル.md
#
# 記事のfrontmatter + H2見出しから30秒PR動画を自動生成
# 出力先: out/<記事名>.mp4

set -e

if [ -z "$1" ]; then
    echo "Usage: $0 <markdown-file>"
    exit 1
fi

MD_FILE="$1"
BASENAME=$(basename "$MD_FILE" .md)
SCRIPT_DIR=$(cd "$(dirname "$0")" && pwd)
OUT_DIR="${SCRIPT_DIR}/out"
PROPS_FILE="${OUT_DIR}/${BASENAME}_props.json"
VIDEO_FILE="${OUT_DIR}/${BASENAME}.mp4"

mkdir -p "$OUT_DIR"

# 依存関係チェック
if [ ! -d "${SCRIPT_DIR}/scripts/node_modules" ]; then
    echo "scripts/ の依存関係をインストール中..."
    (cd "${SCRIPT_DIR}/scripts" && npm install)
fi
if [ ! -d "${SCRIPT_DIR}/video/node_modules" ]; then
    echo "video/ の依存関係をインストール中..."
    (cd "${SCRIPT_DIR}/video" && npm install)
fi

# Step 1: 記事パース
echo "=== PR動画ビルド: $MD_FILE ==="
echo ""
echo "[1/2] 記事をパース中..."
(cd "${SCRIPT_DIR}/scripts" && npx tsx parse-article.ts "${SCRIPT_DIR}/${MD_FILE}") > "$PROPS_FILE"

TITLE=$(node -e "console.log(JSON.parse(require('fs').readFileSync('${PROPS_FILE}','utf-8')).title)")
echo "  タイトル: $TITLE"
echo "  Props: $PROPS_FILE"

# Step 2: 動画レンダリング
echo ""
echo "[2/2] 動画をレンダリング中..."
(cd "${SCRIPT_DIR}/video" && npx remotion render src/index.ts BlogVideo "$VIDEO_FILE" --props="$PROPS_FILE" --codec=h264)

echo ""
echo "Done: $VIDEO_FILE"

# 動画を開く
open -a "QuickTime Player" "$VIDEO_FILE"
