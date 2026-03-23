# ストック: ローカルLLM（Qwen3.5 + LM Studio）記事

## ステータス: 構想中

## きっかけ

- 元ページ: [Qwen3.5-9B — LM Studio](https://lmstudio.ai/models/qwen/qwen3.5-9b)
  - Alibaba（Qwenチーム）の最新ローカルLLM
  - LM Studio経由で無料・ローカル実行可能

## Qwen3.5-9B の要点

### スペック
- パラメータ数: 90億（9B）Dense
- コンテキスト長: 262,144トークン（約26万）
- アーキテクチャ: 密集型（MoEではない）
- 動作環境: macOS / Windows / Linux（LM Studio経由）
- 価格: 無料

### 技術的特徴
- マルチモーダル学習の統合
- アーキテクチャ効率の向上
- 強化学習スケーリング
- 26万トークンのコンテキストは書籍1冊分程度

## 記事の方向性（案）

### 切り口: Unity開発者のためのローカルLLM活用
- Claude Code（クラウドAPI）vs ローカルLLM の使い分け
- オフライン環境でのコード補完・レビュー
- LM Studio + Qwen3.5 のセットアップ手順
- ローカルLLMの限界（パラメータ数の壁、MCP非対応）
- ハイブリッド運用: 日常はローカル、本格作業はClaude Code

### 関連トピック
- LM Studio: ローカルLLMの実行プラットフォーム（GUI、API互換）
- Qwenファミリー: 0.6B〜235B（MoE）まで幅広いラインナップ
- ollama との比較（CUIベース vs GUIベース）
- ローカルLLMのVRAM要件（9Bモデルは16GB推奨）

## 参考リンク

- [LM Studio](https://lmstudio.ai/)
- [Qwen3.5-9B — LM Studio](https://lmstudio.ai/models/qwen/qwen3.5-9b)
- [Qwen公式（GitHub）](https://github.com/QwenLM/Qwen)
