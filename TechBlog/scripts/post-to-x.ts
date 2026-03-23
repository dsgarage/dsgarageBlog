/**
 * X (Twitter) に動画付きツイート + 返信リンクを投稿する
 *
 * 使い方:
 *   npx tsx post-to-x.ts <動画パス> <ツイート本文> [--reply <返信テキスト>] [--dry-run]
 *
 * 環境変数:
 *   X_API_KEY, X_API_SECRET, X_ACCESS_TOKEN, X_ACCESS_TOKEN_SECRET
 */

import { readFileSync } from "node:fs";
import { resolve } from "node:path";
import { TwitterApi } from "twitter-api-v2";

function getClient(): TwitterApi {
  const apiKey = process.env.X_API_KEY;
  const apiSecret = process.env.X_API_SECRET;
  const accessToken = process.env.X_ACCESS_TOKEN;
  const accessTokenSecret = process.env.X_ACCESS_TOKEN_SECRET;

  if (!apiKey || !apiSecret || !accessToken || !accessTokenSecret) {
    throw new Error(
      "環境変数が不足しています: X_API_KEY, X_API_SECRET, X_ACCESS_TOKEN, X_ACCESS_TOKEN_SECRET",
    );
  }

  return new TwitterApi({
    appKey: apiKey,
    appSecret: apiSecret,
    accessToken,
    accessSecret: accessTokenSecret,
  });
}

async function postToX(
  videoPath: string,
  text: string,
  replyText: string | null,
  dryRun: boolean,
): Promise<void> {
  if (dryRun) {
    console.log("=== ドライラン ===");
    console.log(`動画: ${videoPath}`);
    console.log(`本文: ${text}`);
    if (replyText) {
      console.log(`返信: ${replyText}`);
    }
    console.log("(実際の投稿はスキップ)");
    return;
  }

  const client = getClient();

  // 1. 動画アップロード
  console.log("動画アップロード中...");
  const mediaBuffer = readFileSync(resolve(videoPath));
  const mediaId = await client.v1.uploadMedia(mediaBuffer, {
    mimeType: "video/mp4",
  });
  console.log(`動画アップロード完了: mediaId=${mediaId}`);

  // 2. メディア処理完了を待つ（動画はサーバー側エンコードが必要）
  console.log("メディア処理の完了を待機中...");
  const maxAttempts = 30;
  for (let i = 0; i < maxAttempts; i++) {
    const status = await client.v1.mediaInfo(mediaId);
    const state = status.processing_info?.state;

    if (!state || state === "succeeded") {
      console.log("メディア処理完了");
      break;
    }
    if (state === "failed") {
      throw new Error(
        `メディア処理失敗: ${JSON.stringify(status.processing_info)}`,
      );
    }
    // in_progress or pending — wait
    const waitSec = status.processing_info?.check_after_secs ?? 5;
    console.log(
      `  処理中... ${waitSec}秒後に再確認 (${i + 1}/${maxAttempts})`,
    );
    await new Promise((r) => setTimeout(r, waitSec * 1000));
  }

  // 3. メインツイート投稿
  console.log("ツイート投稿中...");
  const tweet = await client.v2.tweet({
    text,
    media: { media_ids: [mediaId] },
  });
  console.log(`ツイート投稿完了: ${tweet.data.id}`);

  // 4. 返信ツイート（URL付き）
  if (replyText) {
    console.log("返信ツイート投稿中...");
    const reply = await client.v2.tweet({
      text: replyText,
      reply: { in_reply_to_tweet_id: tweet.data.id },
    });
    console.log(`返信投稿完了: ${reply.data.id}`);
  }
}

// CLI 引数パース
const args = process.argv.slice(2);
const dryRun = args.includes("--dry-run");
const replyIndex = args.indexOf("--reply");

let replyText: string | null = null;
if (replyIndex !== -1) {
  replyText = args[replyIndex + 1] || null;
}

// --dry-run と --reply を除いた位置引数
const positional = args.filter(
  (_, i) => i !== replyIndex && i !== replyIndex + 1 && args[i] !== "--dry-run",
);

const videoPath = positional[0];
const tweetText = positional[1];

if (!videoPath || !tweetText) {
  console.error(
    '使い方: npx tsx post-to-x.ts <動画パス> "本文" [--reply "返信テキスト"] [--dry-run]',
  );
  process.exit(1);
}

postToX(videoPath, tweetText, replyText, dryRun).catch((err) => {
  console.error("投稿エラー:", err);
  process.exit(1);
});
