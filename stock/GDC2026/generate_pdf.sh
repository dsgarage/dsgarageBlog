#!/bin/bash
# GDC2026 セッション記事 → PDF 生成スクリプト
# Usage: ./generate_pdf.sh <session_dir>
# pandoc + weasyprint で画像埋め込みPDFを生成

set -e

BASE_DIR="/Users/daisuketsukada/Documents/dsgarageBlog/stock/GDC2026"
CSS_FILE="/Users/daisuketsukada/Documents/dsgarageBlog/TechBlog/style.css"

SESSION_DIR="$1"
if [ -z "$SESSION_DIR" ]; then
  echo "Usage: $0 <session_directory_name>"
  exit 1
fi

FULL_DIR="$BASE_DIR/$SESSION_DIR"
if [ ! -d "$FULL_DIR" ]; then
  echo "ERROR: Directory not found: $FULL_DIR"
  exit 1
fi

# Find the main article MD (日本語タイトルの2026*.md or GDC2026*.md)
MD_FILE=$(find "$FULL_DIR" -maxdepth 1 -name "2026*.md" | head -1)
if [ -z "$MD_FILE" ]; then
  MD_FILE=$(find "$FULL_DIR" -maxdepth 1 -name "GDC2026*.md" | head -1)
fi
if [ -z "$MD_FILE" ]; then
  echo "ERROR: No article MD found in $FULL_DIR"
  exit 1
fi

MD_BASENAME=$(basename "$MD_FILE" .md)
HTML_FILE="$FULL_DIR/${MD_BASENAME}.html"
PDF_FILE="$FULL_DIR/${MD_BASENAME}.pdf"

echo "=== Processing: $SESSION_DIR ==="
echo "  MD: $MD_FILE"

# Extract metadata from frontmatter
TITLE=$(grep -m1 '^title:' "$MD_FILE" | sed 's/^title:[[:space:]]*//' | sed 's/^"//;s/"$//')
SUBTITLE=$(grep -m1 '^subtitle:' "$MD_FILE" | sed 's/^subtitle:[[:space:]]*//' | sed 's/^"//;s/"$//')
DATE=$(grep -m1 '^date:' "$MD_FILE" | sed 's/^date:\s*"\?\(.*\)"\?\s*$/\1/' | sed 's/"$//')
AUTHOR=$(grep -m1 '^author:' "$MD_FILE" | sed 's/^author:\s*"\?\(.*\)"\?\s*$/\1/' | sed 's/"$//')

[ -z "$TITLE" ] && TITLE="$MD_BASENAME"
[ -z "$DATE" ] && DATE="2026-03-12"
[ -z "$AUTHOR" ] && AUTHOR="dsgarage"

echo "  Title: $TITLE"

# Generate HTML with pandoc (run from session directory for relative image paths)
cd "$FULL_DIR"

# Create template file
TEMPLATE_FILE="$FULL_DIR/_template.html5"
cat > "$TEMPLATE_FILE" <<'TMPLEOF'
<!DOCTYPE html>
<html lang="ja">
<head>
    <meta charset="UTF-8">
    <title>$title$</title>
    <link rel="stylesheet" href="CSSPLACEHOLDER">
</head>
<body>
<div class="cover">
    <h1>$title$</h1>
    $if(subtitle)$<p class="subtitle">$subtitle$</p>$endif$
    <div class="meta">
        $if(date)$<p>$date$</p>$endif$
        $if(author)$<p>$author$</p>$endif$
    </div>
</div>
<div class="toc">
    <h2>目次</h2>
$toc$
</div>
<div class="content" data-date="$date$">
$body$
</div>
</body>
</html>
TMPLEOF

# Fix CSS path in template
sed -i '' "s|CSSPLACEHOLDER|$CSS_FILE|g" "$TEMPLATE_FILE"

pandoc "$MD_FILE" \
  --from=markdown \
  --to=html5 \
  --standalone \
  --toc \
  --toc-depth=3 \
  --metadata title="$TITLE" \
  --template="$TEMPLATE_FILE" \
  -o "$HTML_FILE"

# Clean up template
rm -f "$TEMPLATE_FILE"

echo "  HTML generated: $HTML_FILE"

# Generate PDF with weasyprint
weasyprint "$HTML_FILE" "$PDF_FILE" 2>&1 | tail -5

echo "  PDF generated: $PDF_FILE"
echo "  Size: $(du -h "$PDF_FILE" | cut -f1)"
echo "=== Done: $SESSION_DIR ==="
