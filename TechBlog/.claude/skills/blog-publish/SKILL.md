---
name: blog-publish
description: 原本Markdownをマルチプラットフォーム（Qiita/Zenn/note/PDF）に変換し、GitHub経由でデプロイし、X（Twitter）にPR動画付きで投稿する。記事の公開、デプロイ、変換について言及された場合に使用する。
allowed-tools:
  - Read
  - Write
  - Edit
  - Bash
  - Glob
argument-hint: "<記事ファイル名.md>"
---

# blog-publish スキル

記事「$ARGUMENTS」をビルド・デプロイ・X投稿する一貫フロー。

## Phase 2: ビルド（マルチプラットフォーム変換）

### 2-1. 原本の読み込み
- ルートの `YYYYMMDD_タイトル.md` を読む
- frontmatter（title, subtitle, author, date）を解析

### 2-2. 変換（並列実行可能なものは並列で）

#### Qiita
- `:::note info` 記法に変換
- frontmatter を除去、`# H1` タイトルのみ
- `Qiita/YYYYMMDD_タイトル_qiita.md` に出力

#### Zenn
- `:::message` 記法に変換
- Zenn用 frontmatter を生成（title, emoji, type, topics, published: false）
- topics は最大5つ
- `Zenn/YYYYMMDD_タイトル_zenn.md` に出力

#### note
- `./convert-to-mt.sh` を実行
- `note_import/YYYYMMDD_タイトル.txt` に出力

#### PDF
- `./build-pdf.sh YYYYMMDD_タイトル.md` を実行

### 2-3. 生成確認
- 生成されたファイルの一覧を表示

**注意**: PR動画は Phase 1（/blog-write）で生成済みのはず。未生成の場合は `./build-video.sh` を実行する。

## Phase 3: デプロイ（GitHub 経由で公開）

### 3-1. スラッグを決める
- 記事の主題を英語 kebab-case で表現（日付なし）
- 例: `anthropic-skill-creator`

### 3-2. Qiita デプロイ
1. Qiita 用 frontmatter に書き換えて `qiita_deploy/public/<slug>.md` に出力
   ```yaml
   ---
   title: 記事タイトル
   tags:
     - Tag1
     - Tag2
   private: false
   updated_at: ''
   id: null
   organization_url_name: null
   slide: false
   ignorePublish: false
   ---
   ```
2. コミット & プッシュ:
   ```bash
   cd qiita_deploy
   git add public/<slug>.md
   git commit -m "記事タイトル を追加"
   git push origin main
   ```
3. GitHub Actions が自動で Qiita に公開

### 3-3. Zenn デプロイ
1. Zenn 用 frontmatter で `zenn_deploy/articles/<slug>.md` に出力
   - published: true に設定
2. コミット & プッシュ:
   ```bash
   cd zenn_deploy
   git add articles/<slug>.md
   git commit -m "記事タイトル を追加"
   git push origin main
   ```
3. GitHub 連携で自動的に Zenn に公開

### 3-4. 公開 URL の確認
- Qiita: push 後に `qiita-cli` が ID を付与 → URL 確定
- Zenn: `https://zenn.dev/dsgarage/articles/<slug>` で即アクセス可能

## Phase 4: X（Twitter）投稿

Qiita/Zenn の公開 URL が確定した後に実行する。

### 4-1. ユーザーに記事 URL を確認
- Qiita or Zenn の公開 URL をリプライツイートに使用
- Zenn は `https://zenn.dev/dsgarage/articles/<slug>` で確定
- Qiita は push 後に ID が付与されるため、ユーザーに確認

### 4-2. ドライラン（必須）
```bash
./post-video.sh YYYYMMDD_タイトル.md <記事URL> --dry-run
```
- 出力されるツイート内容をユーザーに提示

### 4-3. 本投稿（ユーザー承認後のみ）
```bash
./post-video.sh YYYYMMDD_タイトル.md <記事URL>
```

**ユーザーの承認なしに本投稿してはならない。**
