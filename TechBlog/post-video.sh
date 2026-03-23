#!/bin/bash
# X (Twitter) 投稿スクリプト
# Usage: ./post-video.sh <記事.md> <ブログURL> [--dry-run]
#
# 1つ目のツイート: PR動画 + タイトル + 要点
# 2つ目のツイート: 返信でブログURL + コメント
#
# 事前に build-video.sh で動画を生成しておくこと
# 環境変数: X_API_KEY, X_API_SECRET, X_ACCESS_TOKEN, X_ACCESS_TOKEN_SECRET
#   (.env ファイルに記載可)

set -e

if [ -z "$1" ] || [ -z "$2" ]; then
    echo "Usage: $0 <markdown-file> <blog-url> [--dry-run]"
    echo ""
    echo "例: $0 20251226_UnityでMCPが必要な理由.md https://zenn.dev/dsgarage/articles/unity-mcp-why-you-need-it"
    echo ""
    echo "事前に ./build-video.sh で動画を生成してください"
    exit 1
fi

MD_FILE="$1"
BLOG_URL="$2"
BASENAME=$(basename "$MD_FILE" .md)
SCRIPT_DIR=$(cd "$(dirname "$0")" && pwd)
OUT_DIR="${SCRIPT_DIR}/out"
PROPS_FILE="${OUT_DIR}/${BASENAME}_props.json"
VIDEO_FILE="${OUT_DIR}/${BASENAME}.mp4"
DRY_RUN=""

if [[ "$*" == *"--dry-run"* ]]; then
    DRY_RUN="--dry-run"
fi

# ファイル存在チェック
if [ ! -f "$VIDEO_FILE" ]; then
    echo "エラー: 動画ファイルが見つかりません: $VIDEO_FILE"
    echo "先に ./build-video.sh $MD_FILE を実行してください"
    exit 1
fi
if [ ! -f "$PROPS_FILE" ]; then
    echo "エラー: Props ファイルが見つかりません: $PROPS_FILE"
    echo "先に ./build-video.sh $MD_FILE を実行してください"
    exit 1
fi

# .env ファイルの読み込み
if [ -f "${SCRIPT_DIR}/.env" ]; then
    echo ".env ファイルを読み込み中..."
    set -a
    source "${SCRIPT_DIR}/.env"
    set +a
fi

# ツイート本文を組み立て
TITLE=$(node -e "console.log(JSON.parse(require('fs').readFileSync('${PROPS_FILE}','utf-8')).title)")
KEY_POINTS=$(node -e "
  const p = JSON.parse(require('fs').readFileSync('${PROPS_FILE}','utf-8'));
  p.keyPoints.forEach((k,i) => console.log((i+1) + '. ' + k));
")

TWEET="📝 新着ブログ: ${TITLE}

${KEY_POINTS}

#ClaudeCode #AI"

REPLY="📖 記事はこちら 👇
${BLOG_URL}"

echo "=== X 投稿 ==="
echo "動画: $VIDEO_FILE"
echo ""
echo "--- メインツイート ---"
echo "$TWEET"
echo ""
echo "--- 返信ツイート ---"
echo "$REPLY"
echo "---"

if [ -n "$DRY_RUN" ]; then
    echo ""
    echo "(ドライラン: 実際の投稿はスキップ)"
else
    (cd "${SCRIPT_DIR}/scripts" && npx tsx post-to-x.ts "$VIDEO_FILE" "$TWEET" --reply "$REPLY")
    echo ""
    echo "投稿完了!"
fi
