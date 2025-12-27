#!/bin/bash
# Graphviz図をPNGに変換（共通スタイル適用）
# 使い方: ./build-diagram.sh images/diagram_name.dot

set -e

if [ $# -eq 0 ]; then
    echo "Usage: $0 <dot-file> [output-format]"
    echo "Example: $0 images/my_diagram.dot"
    echo "         $0 images/my_diagram.dot svg"
    exit 1
fi

DOT_FILE="$1"
FORMAT="${2:-png}"

if [ ! -f "$DOT_FILE" ]; then
    echo "Error: File not found: $DOT_FILE"
    exit 1
fi

# 出力ファイル名
OUTPUT_FILE="${DOT_FILE%.*}.${FORMAT}"

# 高品質PNG生成オプション
DPI=150

echo "Converting: $DOT_FILE -> $OUTPUT_FILE"

# Graphviz変換
if [ "$FORMAT" = "png" ]; then
    dot -Tpng -Gdpi=$DPI "$DOT_FILE" -o "$OUTPUT_FILE"
elif [ "$FORMAT" = "svg" ]; then
    dot -Tsvg "$DOT_FILE" -o "$OUTPUT_FILE"
else
    dot -T"$FORMAT" "$DOT_FILE" -o "$OUTPUT_FILE"
fi

echo "Done: $OUTPUT_FILE"

# ファイルサイズ表示
ls -lh "$OUTPUT_FILE"
