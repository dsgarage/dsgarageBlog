# TechBlog

dsgarage テックブログの原稿管理・マルチプラットフォーム配信プロジェクト。

## 公開ワークフロー

```
┌──────────────────────────────────────────────────────────────────┐
│  Phase 1: 執筆                /blog-write <テーマ>                │
│    ├── 原本 Markdown 作成                                        │
│    ├── 挿絵・ダイアグラム作成（draw.io MCP / Figma MCP）          │
│    └── X 用告知動画 生成（Remotion → .mp4）                      │
├──────────────────────────────────────────────────────────────────┤
│  Phase 2: ビルド              /blog-publish <記事.md>             │
│    └── Qiita / Zenn / note / PDF を一括生成                      │
├──────────────────────────────────────────────────────────────────┤
│  Phase 3: デプロイ            /blog-publish の続き                │
│    ├── Qiita: qiita_deploy/public/<slug>.md → git push           │
│    │         → GitHub Actions で自動公開                          │
│    └── Zenn:  zenn_deploy/articles/<slug>.md → git push           │
│              → GitHub 連携で自動公開                              │
├──────────────────────────────────────────────────────────────────┤
│  Phase 4: X 投稿              /blog-publish の続き                │
│    ├── メインツイート: PR動画 + タイトル + 要点 + ハッシュタグ      │
│    └── リプライツイート: Qiita/Zenn の記事 URL                    │
└──────────────────────────────────────────────────────────────────┘
```

### クイックスタート

```bash
# Phase 1: 執筆（スキルで自動生成）
#   /blog-write <テーマ>
#   → 原本 .md + 図解 + PR動画 が生成される

# Phase 2-4: ビルド・デプロイ・X投稿
#   /blog-publish <記事.md>
#   → Qiita/Zenn/note/PDF 変換
#   → GitHub push でデプロイ
#   → X にPR動画付き投稿
```

## ディレクトリ構成

```
TechBlog/
├── YYYYMMDD_タイトル.md        # 原本 Markdown（Single Source of Truth）
├── _template.md                 # 新規記事テンプレート
├── CLAUDE.md                    # Claude Code プロジェクト設定
├── BLOG_WRITING_GUIDELINE.md    # 執筆ガイドライン（詳細版）
├── .claude/                     # Claude Code 設定
│   ├── rules/                   #   ルール（自動適用）
│   │   ├── writing-guideline.md
│   │   └── publishing-workflow.md
│   └── skills/                  #   スキル（スラッシュコマンド）
│       ├── blog-write/          #     /blog-write — 執筆+図解+動画
│       ├── blog-publish/        #     /blog-publish — 変換+デプロイ+X投稿
│       └── blog-post-x/         #     /blog-post-x — X投稿のみ
├── style.css                    # PDF 用 CSS
├── images/                      # 記事本文で使う確定画像
│   └── _style.dot               #   Graphviz 共通カラーパレット
├── PDF/                         # 生成済み PDF
├── Qiita/                       # Qiita 向け中間ファイル
├── Zenn/                        # Zenn 向け中間ファイル
├── note_import/                 # note 向け MT 形式テキスト
├── qiita_deploy/                # Qiita デプロイ用（dsgarage/Qiita）
│   └── public/<slug>.md         #   Qiita frontmatter 付き記事
├── zenn_deploy/                 # Zenn デプロイ用（dsgarage/Zenn）
│   └── articles/<slug>.md       #   Zenn frontmatter 付き記事
├── out/                         # 生成動画 + props JSON
├── stock/                       # 記事用の元資料・調査素材
├── Reference/                   # 参考資料
├── scripts/                     # Node.js ツール
├── video/                       # Remotion PR 動画プロジェクト
├── build-pdf.sh                 # Markdown → PDF
├── build-diagram.sh             # .dot → .png
├── build-video.sh               # 記事 → PR 動画
├── convert-to-mt.sh             # Markdown → MT 形式
├── post-video.sh                # X に動画+テキスト投稿
└── .env                         # X API キー（gitignore 対象）
```

## デプロイ先リポジトリ

| プラットフォーム | リポジトリ | デプロイ方式 | ファイル配置先 |
|:---|:---|:---|:---|
| **Qiita** | `dsgarage/Qiita` | GitHub Actions（qiita-cli） | `public/<slug>.md` |
| **Zenn** | `dsgarage/Zenn` | GitHub 連携（自動） | `articles/<slug>.md` |

### スラッグ命名規則

デプロイ用リポジトリのファイル名は **kebab-case のスラッグ**（日付なし）。

```
原本:     20260306_Anthropic公式skill-creatorでスキルを自作する.md
Qiita用:  qiita_deploy/public/anthropic-skill-creator.md
Zenn用:   zenn_deploy/articles/anthropic-skill-creator.md
```

## 図解ツール

| ツール | 用途 | 備考 |
|:---|:---|:---|
| **draw.io MCP** | フロー図・構成図・状態遷移図 | 技術ダイアグラムの第一選択 |
| **Figma MCP** | FigJam ダイアグラム・デザインモック | `generate_diagram` / `generate_figma_design` |
| **Graphviz** | シンプルなノード接続図 | `./build-diagram.sh`、`_style.dot` でカラー統一 |

## 技術スタック

| ツール | 用途 |
|:---|:---|
| pandoc + weasyprint | PDF 生成（Python 3.11） |
| Remotion 4.x | PR 動画（React 18） |
| gray-matter | frontmatter 解析 |
| twitter-api-v2 | X 投稿 |

## Claude Code スキル

| スキル | コマンド | Phase | 説明 |
|:---|:---|:---|:---|
| blog-write | `/blog-write <テーマ>` | 1 | 執筆 + 図解 + 動画生成 |
| blog-publish | `/blog-publish <記事.md>` | 2-4 | 変換 + デプロイ + X投稿 |
| blog-post-x | `/blog-post-x <記事.md> <URL>` | 4 | X 投稿のみ（単独実行用） |

## 製品名の表記

- **UniMCP4CC** が正式名称（CC2UniMCP, UniMCP2CC は不可）
- GitHub: `dsgarage/UniMCP4CC`

## 素材の置き分け

- 記事本文の原稿は `TechBlog/` 直下に置く。
- 記事本文で使う確定画像は `TechBlog/images/` に置く。
- イベントや連載の元資料は `TechBlog/stock/<topic>/` に置く。
- topic 固有ルールは `TechBlog/stock/<topic>/README.md` に置く。
- `TechBlog/stock/` 全体のルールは `TechBlog/stock/README.md` を参照。
