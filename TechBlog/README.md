# Blog PDF Generator

Markdown から美しい PDF を生成するためのテンプレートとビルドスクリプト。

## 特徴

- シンプルな Markdown で記事を書く
- 表紙・目次を自動生成
- テック系モダンなデザイン
- コードブロックのシンタックスハイライト

## 必要環境

- **Python 3.10+** (weasyprint用)
- **pandoc** (Markdown → HTML変換)
- **weasyprint** (HTML → PDF変換)

## インストール

### 1. Homebrew (macOS)

```bash
brew install pandoc
```

### 2. Python環境

```bash
# pyenvを使用している場合
pyenv install 3.10.7
pyenv global 3.10.7

# weasyprintをインストール
pip install weasyprint
```

### 3. 動作確認

```bash
pandoc --version
python -m weasyprint --version
```

## 使い方

### 1. テンプレートをコピー

```bash
cp _template.md YYYYMMDD_タイトル.md
```

### 2. 記事を編集

```markdown
---
title: "記事タイトル"
subtitle: "サブタイトル"
author: "著者名"
date: "2025-12-26"
---

# 記事タイトル

## セクション

本文...
```

### 3. PDFを生成

```bash
./build-pdf.sh YYYYMMDD_タイトル.md
```

## ファイル構成

```
Blog/
├── YYYYMMDD_タイトル.md   # 原本（ルートに配置）
├── README.md              # このファイル
├── _template.md           # 記事テンプレート
├── style.css              # PDFスタイル
├── build-pdf.sh           # PDF生成スクリプト
├── build-diagram.sh       # 図生成スクリプト
├── .gitignore             # 記事とPDFを除外
├── images/                # 図・画像
│   └── _style.dot         # Graphviz共通スタイル
├── PDF/                   # 生成済みPDF
├── Qiita/                 # Qiita向け記事
└── Zenn/                  # Zenn向け記事
```

## 記事の管理

- **ルート**: 原本のMarkdownファイル
- **PDF/**: 生成済みPDFファイル
- **Qiita/**: Qiita向けにフォーマットした記事
- **Zenn/**: Zenn向けにフォーマットした記事（frontmatter付き）

## 図の作成（Graphviz）

### 1. DOTファイルを作成

`images/` に `.dot` ファイルを作成：

```dot
digraph MyDiagram {
    // _style.dot のスタイルをコピー or 参照
    graph [rankdir=LR bgcolor="white" fontname="Helvetica" fontsize=12]
    node [fontname="Helvetica" fontsize=11 style="filled,rounded" shape=box]
    edge [fontname="Helvetica" fontsize=10]

    // カラーパレット
    // Primary (青): #2563eb, Light: #dbeafe, BG: #eff6ff
    // Danger (赤): #dc2626, Light: #fecaca, BG: #fef2f2
    // Warning (黄): #d97706, Fill: #fbbf24

    A [label="ノードA" fillcolor="#dbeafe" color="#2563eb"]
    B [label="ノードB" fillcolor="#fecaca" color="#dc2626"]
    A -> B [label="関係"]
}
```

### 2. PNGに変換

```bash
./build-diagram.sh images/my_diagram.dot
```

### 3. Markdownで参照

```markdown
![図の説明](./images/my_diagram.png)
```

## カスタマイズ

### スタイル変更

`style.css` を編集：

- `--primary`: メインカラー（見出し、リンク）
- `--accent`: アクセントカラー
- `.cover`: 表紙デザイン

### 表紙の色を変更

```css
.cover {
  background: #1a1a2e;  /* 背景色 */
}

.cover h1::after {
  background: #e94560;  /* アクセントライン */
}
```

## トラブルシューティング

### weasyprint が見つからない

pyenv使用時は、正しいPythonバージョンにインストールされているか確認：

```bash
~/.pyenv/versions/3.10.7/bin/python -m weasyprint --version
```

`build-pdf.sh` のパスを環境に合わせて修正してください。

### 日本語が文字化けする

システムに日本語フォントがインストールされていることを確認：
- macOS: ヒラギノ角ゴ（標準搭載）
- Linux: `apt install fonts-noto-cjk`

## ライセンス

MIT
