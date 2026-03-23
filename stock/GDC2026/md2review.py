#!/usr/bin/env python3
"""Markdown to Re:VIEW converter for GDC 2026 ebook."""
import re
import sys
import os


def convert_md_to_review(md_text, image_dir_prefix=""):
    """Convert Markdown text to Re:VIEW format."""
    lines = md_text.split('\n')
    result = []
    in_frontmatter = False
    in_code_block = False
    code_lang = ""
    in_table = False
    table_lines = []
    table_caption = ""
    table_id_counter = [0]
    in_quote = False
    quote_lines = []
    image_counter = [0]
    list_counter = [0]

    def flush_quote():
        nonlocal in_quote, quote_lines
        if in_quote and quote_lines:
            result.append('//quote{')
            for ql in quote_lines:
                result.append(ql)
            result.append('//}')
            result.append('')
        in_quote = False
        quote_lines = []

    def flush_table():
        nonlocal in_table, table_lines, table_caption
        if in_table and table_lines:
            table_id_counter[0] += 1
            tid = f"tbl{table_id_counter[0]}"
            # Sanitize caption: remove Re:VIEW commands, heading markers, brackets, etc.
            safe_caption = table_caption
            safe_caption = re.sub(r'^=+\s*', '', safe_caption)  # Remove heading markers
            safe_caption = re.sub(r'//image\[[^\]]*\]\[[^\]]*\]', '', safe_caption)  # Remove image refs
            safe_caption = re.sub(r'//[a-z]+\{?', '', safe_caption)  # Remove Re:VIEW commands
            safe_caption = re.sub(r'//\}', '', safe_caption)  # Remove closing braces
            safe_caption = re.sub(r'\[', '（', safe_caption)  # Replace brackets
            safe_caption = re.sub(r'\]', '）', safe_caption)  # Replace brackets
            safe_caption = safe_caption.strip()
            if not safe_caption:
                safe_caption = f"表{table_id_counter[0]}"
            result.append(f'//table[{tid}][{safe_caption}]{{')
            header_done = False
            for tl in table_lines:
                cells = [c.strip() for c in tl.strip('|').split('|')]
                if all(re.match(r'^[-:]+$', c) for c in cells):
                    result.append('----')
                    header_done = True
                    continue
                # Remove markdown bold from table cells
                cleaned_cells = [re.sub(r'\*\*(.+?)\*\*', r'\1', c) for c in cells]
                result.append('\t'.join(cleaned_cells))
            result.append('//}')
            result.append('')
        in_table = False
        table_lines = []
        table_caption = ""

    def convert_inline(text):
        """Convert inline Markdown to Re:VIEW inline commands."""
        # Bold+italic ***text*** or ___text___
        text = re.sub(r'\*\*\*(.+?)\*\*\*', r'@<strong>{\1}', text)
        # Bold **text**
        text = re.sub(r'\*\*(.+?)\*\*', r'@<b>{\1}', text)
        # Italic *text* (but not inside already converted)
        text = re.sub(r'(?<![<@])(?<!\w)\*([^*]+?)\*(?!\w)', r'@<em>{\1}', text)
        # Inline code `text`
        text = re.sub(r'`([^`]+?)`', r'@<code>{\1}', text)
        # Links [text](url)
        text = re.sub(r'\[([^\]]+)\]\(([^)]+)\)', r'@<href>{\2, \1}', text)
        # Escape Re:VIEW special chars in braces (nested braces issue)
        return text

    i = 0
    while i < len(lines):
        line = lines[i]

        # Frontmatter
        if i == 0 and line.strip() == '---':
            in_frontmatter = True
            i += 1
            continue
        if in_frontmatter:
            if line.strip() == '---':
                in_frontmatter = False
            i += 1
            continue

        # Code blocks
        if line.strip().startswith('```'):
            if not in_code_block:
                in_code_block = True
                code_lang = line.strip().lstrip('`').strip()
                list_counter[0] += 1
                if code_lang:
                    result.append(f'//list[list{list_counter[0]}][{code_lang}]{{')
                else:
                    result.append(f'//list[list{list_counter[0]}][]{{')
            else:
                in_code_block = False
                result.append('//}')
                result.append('')
            i += 1
            continue
        if in_code_block:
            result.append(line)
            i += 1
            continue

        # Horizontal rule
        if re.match(r'^---+\s*$', line):
            flush_quote()
            flush_table()
            i += 1
            continue

        # Empty line
        if line.strip() == '':
            flush_quote()
            flush_table()
            result.append('')
            i += 1
            continue

        # Table
        if '|' in line and line.strip().startswith('|'):
            if not in_table:
                in_table = True
                table_lines = []
                # Try to find caption from previous non-empty line
                # Skip lines that are Re:VIEW commands (//image, //quote, etc.)
                for j in range(len(result) - 1, -1, -1):
                    if result[j].strip():
                        prev = result[j].strip()
                        if prev.startswith('//') or prev.startswith('='):
                            # Don't use Re:VIEW commands or headings as caption
                            table_caption = ""
                            break
                        table_caption = prev
                        result.pop(j)
                        break
            table_lines.append(line)
            i += 1
            continue

        # Quote
        if line.strip().startswith('> ') or line.strip().startswith('>'):
            if not in_quote:
                flush_table()
                in_quote = True
                quote_lines = []
            qtext = re.sub(r'^>\s*', '', line)
            quote_lines.append(convert_inline(qtext))
            i += 1
            continue

        # If we were in quote but this line doesn't start with >, flush
        flush_quote()
        flush_table()

        # Headings
        m = re.match(r'^(#{1,6})\s+(.+)$', line)
        if m:
            level = len(m.group(1))
            title = m.group(2).strip()
            # Remove inline formatting from heading for cleanliness
            title_clean = re.sub(r'\*\*(.+?)\*\*', r'\1', title)
            title_clean = re.sub(r'\*(.+?)\*', r'\1', title_clean)
            title_clean = re.sub(r'`(.+?)`', r'\1', title_clean)
            equals = '=' * level
            result.append(f'{equals} {title_clean}')
            result.append('')
            i += 1
            continue

        # Image
        m = re.match(r'^!\[([^\]]*)\]\(([^)]+)\)\s*$', line)
        if m:
            alt = m.group(1)
            path = m.group(2)
            image_counter[0] += 1
            # Extract filename without extension for Re:VIEW image id
            basename = os.path.splitext(os.path.basename(path))[0]
            img_id = f"{image_dir_prefix}{basename}" if image_dir_prefix else basename
            result.append(f'//image[{img_id}][{alt}]')
            result.append('')
            i += 1
            continue

        # Unordered list
        m = re.match(r'^(\s*)-\s+(.+)$', line)
        if m:
            indent = len(m.group(1))
            depth = indent // 2 + 1
            text = convert_inline(m.group(2))
            stars = ' ' + '*' * depth
            result.append(f'{stars} {text}')
            i += 1
            continue

        # Ordered list
        m = re.match(r'^(\s*)\d+\.\s+(.+)$', line)
        if m:
            text = convert_inline(m.group(2))
            result.append(f' 1. {text}')
            i += 1
            continue

        # Definition list (: term)
        # Not standard markdown, skip

        # Normal paragraph
        result.append(convert_inline(line))
        i += 1

    flush_quote()
    flush_table()

    return '\n'.join(result)


def extract_title(md_text):
    """Extract title from frontmatter."""
    m = re.search(r'^title:\s*"?(.+?)"?\s*$', md_text, re.MULTILINE)
    if m:
        return m.group(1)
    return ""


def main():
    if len(sys.argv) < 3:
        print("Usage: md2review.py input.md output.re [image_prefix]")
        sys.exit(1)

    input_file = sys.argv[1]
    output_file = sys.argv[2]
    image_prefix = sys.argv[3] if len(sys.argv) > 3 else ""

    with open(input_file, 'r', encoding='utf-8') as f:
        md_text = f.read()

    title = extract_title(md_text)
    review_text = convert_md_to_review(md_text, image_prefix)

    with open(output_file, 'w', encoding='utf-8') as f:
        f.write(review_text)

    print(f"Converted: {input_file} -> {output_file} ({title})")


if __name__ == '__main__':
    main()
