# dsgarageBlog Repository Layout

このリポジトリでは、原稿ソース、公開物、素材ストックを明確に分けて扱う。

## Top Level Rules

- `TechBlog/`
  - 記事原稿、執筆ガイド、画像、変換スクリプトを置く。
  - ブログ執筆の Single Source of Truth。
- `docs/`
  - 公開用の静的ファイル置き場。
  - 手編集しない。公開物または生成物のみを置く。
- `stock/`
  - リポジトリ全体で使う下書き、調査メモ、未整理素材を置く。
  - 特定の記事シリーズに紐づく素材は `TechBlog/stock/` に寄せる。

## Detail Rules

- `stock/` の詳細ルールは `stock/README.md` を参照。
- `TechBlog/stock/` の詳細ルールは `TechBlog/stock/README.md` を参照。
- `TechBlog/stock/GDC2026/` のセッション素材ルールは `TechBlog/stock/GDC2026/README.md` を参照。

## Editing Rules

- 新規記事の原稿は `TechBlog/` 直下に置く。
- 記事本文で使う画像は `TechBlog/images/` に置く。
- イベントや連載ごとの調査素材は `TechBlog/stock/<topic>/` に置く。
- `docs/` に直接元資料を置かない。

## Intent

- `stock/` はリポジトリ横断で再利用する素材置き場。
- `TechBlog/stock/` は記事やイベント単位で編集途中の調査素材を置く場所。
- `TechBlog/images/` は記事本文でそのまま使う画像だけを置く。
