---
description: 執筆済み記事をQiita/Zenn/note/PDFに一括変換し、デプロイ・X投稿まで行う公開ワークフロー。
---

# ブログ記事公開スキル

## 使い方

ユーザーが「公開して」「デプロイして」「配信して」等と言ったらこのスキルを使う。
対象記事のファイル名を確認してから実行する。

## 手順

### Phase 1: ビルド（PDF + 図解）

```bash
cd TechBlog
./build-diagram.sh          # .dot → .png
./build-pdf.sh <記事.md>    # Markdown → PDF
```

生成物は `TechBlog/PDF/` に出力される。

### Phase 2: プラットフォーム変換

各プラットフォーム向けの中間ファイルを生成:

- **Qiita**: `TechBlog/Qiita/` → `TechBlog/qiita_deploy/` にコピー
- **Zenn**: `TechBlog/Zenn/` → `TechBlog/zenn_deploy/` にコピー
- **note**: `./convert-to-mt.sh <記事.md>` → `TechBlog/note_import/` に MT 形式出力

### Phase 3: デプロイ

```bash
# Qiita (GitHub push で自動公開)
cd TechBlog/qiita_deploy && git add -A && git commit -m "記事公開: <タイトル>" && git push

# Zenn (GitHub push で自動公開)
cd TechBlog/zenn_deploy && git add -A && git commit -m "記事公開: <タイトル>" && git push

# note は note_import/ の MT ファイルを手動インポート（ユーザーに案内）
```

### Phase 4: PR 動画 + X 投稿

```bash
cd TechBlog
./build-video.sh <記事.md>   # Remotion で PR 動画生成 → out/
./post-video.sh              # X に動画 + テキスト投稿
```

## 注意事項

- X 投稿は `.env` に API キーが必要（キーをコミットしない）
- note へのインポートは手動操作が必要 — ユーザーに手順を案内する
- 各ステップの成功を確認してから次に進む
- 製品名表記: **UniMCP4CC** が正式名称
