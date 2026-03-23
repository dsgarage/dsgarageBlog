# GDC2026 Stock Rules

`stock/GDC2026/` では、各セッションディレクトリの中で「元資料」と「ブログ制作の補完素材」を分けて管理する。

## Directory Rules

各セッションは以下の構成を基本とする。

```text
<session>/
├── source/
│   ├── audio/         # 元音声、録音ファイル
│   ├── images/        # 撮影したスライド写真、会場写真
│   ├── docs/          # PDF、配布資料、原本ドキュメント
│   └── transcripts/   # Talksribe 等の生文字起こし
├── images/            # ブログ記事向けに作成した補完画像
├── transcripts/       # ブログ用に整形した transcript Markdown
├── README.md          # 進行管理、作業メモ
├── SESSION_INFO.md    # セッション情報
├── summary.md         # 要約
└── <session>.md       # 記事用ドラフト
```

## Separation Policy

- `source/`
  - イベント現地や録音から得た元資料を置く。
  - 命名は元ファイル名を優先し、無理にリネームしない。
- `images/`
  - 記事本文で直接使うために作った図、清書画像、切り出し画像を置く。
- `transcripts/`
  - 読みやすく整えた Markdown transcript、話者別 transcript を置く。
- セッション直下
  - 記事本文、要約、進行管理ファイルだけを置く。

## Naming Rules

- 元資料は元の命名を維持する。
- ブログ補完素材は用途がわかる名前にする。
  - 例: `images/community_safety_framework.png`
  - 例: `transcripts/transcript_john_ryan.md`

## Existing Content Migration Rule

- 既存の `img/` や `slides/` は `source/images/` に統一する。
- 生文字起こし `.txt` は `source/transcripts/` に移動する。
- transcript 系の `.md` は `transcripts/` に移動する。
- 記事本文中で元スライド写真を参照する場合は `source/images/...` を使う。
