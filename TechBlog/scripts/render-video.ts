/**
 * Remotion レンダリング実行
 *
 * 使い方:
 *   npx tsx render-video.ts <propsファイル.json> [出力パス]
 *
 * propsファイルは parse-article.ts の出力 JSON
 */

import { execSync } from "node:child_process";
import { resolve, dirname } from "node:path";
import { existsSync, mkdirSync } from "node:fs";
import { fileURLToPath } from "node:url";

const __dirname = dirname(fileURLToPath(import.meta.url));
const videoDir = resolve(__dirname, "../video");

function renderVideo(propsPath: string, outputPath: string): void {
  const absPropsPath = resolve(propsPath);
  const absOutputPath = resolve(outputPath);

  // 出力ディレクトリの作成
  const outDir = dirname(absOutputPath);
  if (!existsSync(outDir)) {
    mkdirSync(outDir, { recursive: true });
  }

  console.log(`レンダリング開始...`);
  console.log(`  Props: ${absPropsPath}`);
  console.log(`  Output: ${absOutputPath}`);

  const cmd = [
    "npx remotion render",
    "src/index.ts",
    "BlogVideo",
    absOutputPath,
    `--props="${absPropsPath}"`,
    "--codec=h264",
  ].join(" ");

  execSync(cmd, {
    cwd: videoDir,
    stdio: "inherit",
    env: { ...process.env },
  });

  console.log(`レンダリング完了: ${absOutputPath}`);
}

// CLI 実行
const propsPath = process.argv[2];
const outputPath = process.argv[3] || resolve(__dirname, "../out/video.mp4");

if (!propsPath) {
  console.error("使い方: npx tsx render-video.ts <props.json> [出力パス]");
  process.exit(1);
}

renderVideo(propsPath, outputPath);
