---
paths:
  - TechBlog/**
---

# TechBlog プロジェクト

dsgarage テックブログの原稿管理・マルチプラットフォーム配信プロジェクト。

## ワークフロー概要

```
Phase 1: 執筆      /blog-write <テーマ>       → 原本 .md + 図解 + PR動画
Phase 2: ビルド    /blog-publish <記事.md>     → Qiita/Zenn/note/PDF 一括生成
Phase 3: デプロイ  （/blog-publish の続き）    → GitHub push で自動公開
Phase 4: X 投稿    （/blog-publish の続き）    → PR動画付きツイート
```

詳細は `.claude/rules/` を参照:
- **publishing-workflow.md** — Phase 1-4 の全手順・変換ルール・デプロイ手順
- **writing-guideline.md** — 執筆品質基準・リード文の3要素・表記ルール
- **gdc-session-report.md** — GDC セッションレポートの構成・スライド書き起こしルール

## ディレクトリ構成

```
TechBlog/
├── YYYYMMDD_タイトル.md      # 原本（Single Source of Truth）
├── _template.md               # 新規記事テンプレート
├── BLOG_WRITING_GUIDELINE.md  # 執筆ガイドライン（詳細版）
├── style.css                  # PDF 用 CSS
├── images/                    # 記事本文で使う確定画像（.dot + .png）
│   └── _style.dot             # Graphviz 共通カラーパレット
├── PDF/                       # 生成済み PDF
├── Qiita/                     # Qiita 向け中間ファイル
├── Zenn/                      # Zenn 向け中間ファイル
├── note_import/               # note 向け MT 形式テキスト
├── qiita_deploy/              # Qiita デプロイ用（dsgarage/Qiita）
├── zenn_deploy/               # Zenn デプロイ用（dsgarage/Zenn）
├── out/                       # 生成動画 + props JSON
├── Reference/                 # 参考資料
├── scripts/                   # Node.js ツール（parse, post）
├── video/                     # Remotion PR 動画プロジェクト
├── build-pdf.sh               # Markdown → PDF
├── build-diagram.sh           # .dot → .png
├── build-video.sh             # 記事 → PR 動画
├── convert-to-mt.sh           # Markdown → MT 形式（note用）
└── post-video.sh              # X に動画+テキスト投稿
```

## 技術スタック

| ツール | 用途 | 備考 |
|:---|:---|:---|
| pandoc + weasyprint | PDF 生成 | Python 3.11（`~/.pyenv/versions/3.11.0/`） |
| draw.io MCP / Figma MCP / Graphviz | 図解生成 | `./build-diagram.sh` で .dot → .png |
| Remotion 4.x | PR 動画 | React 18（`video/`） |
| gray-matter | frontmatter 解析 | `scripts/parse-article.ts` |
| twitter-api-v2 | X 投稿 | `scripts/post-to-x.ts`、`.env` に API キー |

## 製品名の表記

- **UniMCP4CC** が正式名称（CC2UniMCP, UniMCP2CC は不可）
- GitHub: `dsgarage/UniMCP4CC`
- 記事末尾に UniMCP4CC への誘導を含める（BLOG_WRITING_GUIDELINE.md 参照）
