/**
 * Markdown 記事から VideoProps JSON を抽出する
 *
 * 使い方:
 *   npx tsx parse-article.ts <記事ファイル.md>
 *
 * 出力: VideoProps JSON を stdout に出力
 */

import { readFileSync } from "node:fs";
import { resolve } from "node:path";
import matter from "gray-matter";

// 要点抽出時に除外する H2 見出し
const EXCLUDED_HEADINGS = new Set([
  "概要",
  "まとめ",
  "参考リンク",
  "次のステップ",
  "参考",
  "参考文献",
  "おわりに",
  "はじめに",
]);

interface VideoProps {
  title: string;
  subtitle: string;
  keyPoints: string[];
  author: string;
  date: string;
}

function parseArticle(filePath: string): VideoProps {
  const raw = readFileSync(filePath, "utf-8");
  const { data: frontmatter, content } = matter(raw);

  // frontmatter からメタ情報を取得
  const title = (frontmatter.title as string) || "無題";
  const subtitle = (frontmatter.subtitle as string) || "";
  const author = (frontmatter.author as string) || "dsgarage";
  const date = (frontmatter.date as string) || "";

  // H2 見出しから要点を抽出
  const h2Regex = /^## (.+)$/gm;
  const headings: string[] = [];
  let match: RegExpExecArray | null;

  while ((match = h2Regex.exec(content)) !== null) {
    const heading = match[1].trim();
    if (!EXCLUDED_HEADINGS.has(heading)) {
      headings.push(heading);
    }
  }

  // 最大3つ選択（先頭から）
  const keyPoints = headings.slice(0, 3);

  // 要点が3つ未満の場合、サブタイトルで補完
  while (keyPoints.length < 3 && subtitle) {
    keyPoints.push(subtitle);
    break;
  }

  return { title, subtitle, keyPoints, author, date };
}

// CLI 実行
const articlePath = process.argv[2];
if (!articlePath) {
  console.error("使い方: npx tsx parse-article.ts <記事ファイル.md>");
  process.exit(1);
}

const resolved = resolve(articlePath);
const props = parseArticle(resolved);
console.log(JSON.stringify(props, null, 2));
