# Repository Stock Rules

`/stock` は `dsgarageBlog` 全体のネタ・素材・下書きを一元管理する場所。
TechBlog 内には投稿済み記事のみを置き、ストックはすべてここに集約する。

## カテゴリ構成

```text
stock/
├── ClaudeCode/   # Claude Code・AI ツール関連のネタ・下書き
├── GDC2026/      # GDC セッション素材（transcript, slides, 下書き）
├── Paper/        # 論文読解・解説記事の素材
├── Unity/        # Unity 関連のネタ・下書き
├── TechBook/     # 技術書・電子書籍の素材
└── README.md
```

## Put Here

- 記事の構想・下書き
- 調査メモ・比較メモ
- 元画像・元 PDF・transcript
- 複数記事で使い回す図表の原本

## Do Not Put Here

- `TechBlog/` 直下に置くべき投稿済み記事原稿
- Qiita/Zenn/note/PDF の出力物
- `docs/` に置くべき公開物

## Escalation Rule

- 記事が完成し投稿されたら、原稿を `TechBlog/YYYYMMDD_タイトル.md` に移動する
- 記事に採用確定した画像は `TechBlog/images/` へ移す
