#!/bin/bash
# Markdown to Movable Type format converter for note.com
# Usage: ./convert-to-mt.sh input.md > output.txt

set -e

if [ -z "$1" ]; then
    echo "Usage: $0 <markdown-file>" >&2
    exit 1
fi

MD_FILE="$1"

# Extract title from first H1 or filename
TITLE=$(grep -m1 "^# " "$MD_FILE" | sed 's/^# //' || basename "$MD_FILE" .md)

# Extract date from filename (YYYYMMDD_title.md format)
FILENAME=$(basename "$MD_FILE")
if [[ $FILENAME =~ ^([0-9]{4})([0-9]{2})([0-9]{2})_ ]]; then
    YEAR="${BASH_REMATCH[1]}"
    MONTH="${BASH_REMATCH[2]}"
    DAY="${BASH_REMATCH[3]}"
    DATE="$MONTH/$DAY/$YEAR 12:00:00"
else
    DATE=$(date "+%m/%d/%Y %H:%M:%S")
fi

# Get body content (skip YAML frontmatter if exists, skip first H1)
BODY=$(awk '
    BEGIN { in_frontmatter=0; skip_first_h1=1 }
    /^---$/ && NR==1 { in_frontmatter=1; next }
    /^---$/ && in_frontmatter { in_frontmatter=0; next }
    in_frontmatter { next }
    /^# / && skip_first_h1 { skip_first_h1=0; next }
    { print }
' "$MD_FILE")

# Output Movable Type format
cat << EOF
TITLE: $TITLE
DATE: $DATE
STATUS: Draft
ALLOW COMMENTS: 1
CONVERT BREAKS: markdown
-----
BODY:
$BODY
-----
EOF
