---
name: blog-post-x
description: テックブログ記事のPR動画をX（Twitter）に投稿する。必ずドライランで確認後に本投稿。
allowed-tools:
  - Read
  - Bash
  - Glob
argument-hint: "<記事ファイル名.md> <ブログURL>"
---

# blog-post-x スキル

記事「$ARGUMENTS」のPR動画をXに投稿する。

## 前提条件
- PR動画が `out/` に生成済みであること（なければ `./build-video.sh` を先に実行）
- `.env` に X API キーが設定済みであること

## 手順

### 1. 動画の存在確認
- `out/YYYYMMDD_タイトル.mp4` の存在を確認
- なければ `./build-video.sh` の実行を提案

### 2. ツイート内容の組み立て
- `out/YYYYMMDD_タイトル_props.json` から title, keyPoints を読む
- メインツイートを組み立て:
  ```
  新着ブログ: {タイトル}

  1. {要点1}
  2. {要点2}
  3. {要点3}

  #Unity #AI #ClaudeCode #GameDev
  ```
- リプライツイート: ブログURL

### 3. 文字数チェック（重要）
- X の文字数上限: 280文字
- 日本語は1文字=2カウント（実質約140文字）
- 上限を超える場合は要点を短縮・ハッシュタグを削減

### 4. ドライラン（必須）
```bash
./post-video.sh YYYYMMDD_タイトル.md <ブログURL> --dry-run
```
- 出力されるツイート内容をユーザーに提示
- **ユーザーの承認なしに本投稿してはならない**

### 5. 本投稿（ユーザー承認後）
```bash
./post-video.sh YYYYMMDD_タイトル.md <ブログURL>
```
