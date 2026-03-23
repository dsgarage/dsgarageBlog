/**
 * X (Twitter) にテキストのみのツイート（スレッド対応）を投稿する
 *
 * 使い方:
 *   npx tsx scripts/post-text.ts "ツイート本文"
 *   npx tsx scripts/post-text.ts "ツイート1" --reply "リプライ"
 *
 * 環境変数:
 *   X_API_KEY, X_API_SECRET, X_ACCESS_TOKEN, X_ACCESS_TOKEN_SECRET
 */

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

async function main() {
  const args = process.argv.slice(2);
  const replyIndex = args.indexOf("--reply");

  let replyText: string | null = null;
  if (replyIndex !== -1) {
    replyText = args[replyIndex + 1] || null;
  }

  const positional = args.filter(
    (_, i) => i !== replyIndex && i !== replyIndex + 1,
  );

  const text = positional[0];
  if (!text) {
    console.error(
      '使い方: npx tsx scripts/post-text.ts "本文" [--reply "リプライ"]',
    );
    process.exit(1);
  }

  console.log("--- ツイート内容 ---");
  console.log(text);
  if (replyText) {
    console.log("--- リプライ ---");
    console.log(replyText);
  }
  console.log("---");
  console.log("投稿中...");

  const client = getClient();
  const tweet = await client.v2.tweet({ text });
  console.log(`ツイート投稿完了: ${tweet.data.id}`);

  if (replyText) {
    console.log("リプライ投稿中...");
    const reply = await client.v2.tweet({
      text: replyText,
      reply: { in_reply_to_tweet_id: tweet.data.id },
    });
    console.log(`リプライ投稿完了: ${reply.data.id}`);
  }
}

main().catch((err) => {
  console.error("投稿エラー:", err);
  process.exit(1);
});
