# dsgarageBlog

dsgarage のテックブログ原稿管理・マルチプラットフォーム配信リポジトリ。

## ディレクトリ構成

```
dsgarageBlog/
├── TechBlog/          # 記事原稿・執筆ガイド・画像・変換スクリプト（Single Source of Truth）
│   ├── .claude/       #   Claude Code 設定（rules/, skills/）
│   ├── images/        #   記事本文で使う確定画像のみ
│   └── ...            #   詳細は TechBlog/CLAUDE.md を参照
├── stock/             # 下書き・調査メモ・未整理素材（リポジトリ横断）
│   ├── GDC2026/       #   GDC セッション素材（トランスクリプト・スライド写真）
│   ├── ClaudeCode/    #   Claude Code 関連の調査素材
│   └── ...            #   詳細は stock/README.md を参照
└── docs/              # 公開用静的ファイル（手編集しない・生成物のみ）
```

## ファイル配置ルール

- 新規記事の原稿は `TechBlog/` 直下に `YYYYMMDD_タイトル.md` で置く
- 記事本文でそのまま使う画像は `TechBlog/images/` に置く
- イベント・連載の調査素材は `stock/<topic>/` に置く
- `docs/` に元資料を置かない

## 製品名の表記

- **UniMCP4CC** が正式名称（CC2UniMCP, UniMCP2CC は不可）
- GitHub: `dsgarage/UniMCP4CC`
