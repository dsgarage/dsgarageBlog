---
title: "Ralph Technique：自己改善ループでAIに開発させる"
emoji: "🔄"
type: "tech"
topics: ["ClaudeCode", "AI", "自動化", "RalphTechnique", "開発手法"]
published: false
---

# Ralph Technique：自己改善ループでAIに開発させる

**ストック記事** - 公開日未定

---

## はじめに

「AIに指示を出して、完成するまで放置したい」

この夢を実現する手法が、**Ralph Technique**です。

Anthropic公式のClaude Codeプラグインとしてリリースされたこの手法は、**AIが自分の出力を読み込み、改善を繰り返す**という自己参照ループを実現します。

---

## Ralph Techniqueとは

### 概要

**Ralph Technique**は、Geoffrey Huntley氏が提唱した開発手法で、Claude Codeに**同じプロンプトを繰り返し与える**ことで、自動的に改善を重ねさせます。

### 名前の由来

シンプソンズのキャラクター「Ralph Wiggum」から。
困難に直面しても**しぶとく続ける**哲学を表しています。

### 基本原理

```
┌─────────────────────────────────────────────────────┐
│                Ralph Loopの仕組み                   │
│                                                     │
│  ┌─────────┐                                       │
│  │ユーザー │                                       │
│  │「タスク」│                                       │
│  └────┬────┘                                       │
│       │                                            │
│       ▼                                            │
│  ┌─────────┐      ┌─────────┐      ┌─────────┐   │
│  │ Claude  │ ──→ │ファイル │ ──→ │ Claude  │   │
│  │ (1回目) │      │に保存   │      │ (2回目) │   │
│  └─────────┘      └─────────┘      └────┬────┘   │
│                                         │         │
│       ┌─────────────────────────────────┘         │
│       │                                            │
│       ▼                                            │
│  ┌─────────┐      ┌─────────┐      ┌─────────┐   │
│  │ファイル │ ──→ │ Claude  │ ──→ │  完成   │   │
│  │更新     │      │ (N回目) │      │         │   │
│  └─────────┘      └─────────┘      └─────────┘   │
│                                                     │
│  ※ 完了条件を満たすまで自動的に繰り返す           │
└─────────────────────────────────────────────────────┘
```

### シンプルな定義

> **Ralph = Bashループ**
> `while true` ループでAIエージェントに同じプロンプトを繰り返し与える

---

## 公式プラグイン：ralph-wiggum

### インストール

```bash
# Claude Codeプラグインとして追加
# https://github.com/anthropics/claude-plugins-official/tree/main/plugins/ralph-wiggum
```

### 主要コマンド

#### `/ralph-loop`

自己参照ループを開始します。

```bash
/ralph-loop "<プロンプト>" --max-iterations <回数> --completion-promise "<完了フレーズ>"
```

**オプション:**

| オプション | 説明 |
|:---|:---|
| `--max-iterations <n>` | N回の反復後に停止（デフォルト: 無限） |
| `--completion-promise <text>` | 完了を示すフレーズ |

#### `/cancel-ralph`

実行中のRalphループをキャンセルします。

```bash
/cancel-ralph
```

---

## 実践例

### 例1: REST APIの構築

```bash
/ralph-loop "REST API for todos を構築してください。

要件:
- すべてのCRUD操作（Create, Read, Update, Delete）
- 入力検証
- ユニットテスト（カバレッジ80%以上）
- README with API ドキュメント

各反復で:
1. 現在の状態を確認
2. 未実装の機能を実装
3. テストを実行
4. 失敗があれば修正

すべての要件を満たしたら:
<promise>COMPLETE</promise>
を出力してください。" \
  --completion-promise "COMPLETE" \
  --max-iterations 50
```

### 例2: TDD（テスト駆動開発）

```bash
/ralph-loop "TDDに従って電卓アプリを実装してください。

サイクル:
1. 失敗するテストを書く
2. テストが通る最小限のコードを書く
3. テストを実行
4. 失敗したらデバッグして修正
5. 必要ならリファクタリング
6. 次の機能へ

必須機能:
- 四則演算（+, -, *, /）
- 括弧のサポート
- エラーハンドリング（0除算など）

すべてのテストがグリーンになったら:
<promise>COMPLETE</promise>" \
  --completion-promise "COMPLETE" \
  --max-iterations 30
```

### 例3: 段階的なゴール設定

```bash
/ralph-loop "eコマースAPIを段階的に構築してください。

Phase 1: ユーザー認証
- JWT認証
- 登録/ログイン
- テスト

Phase 2: 商品カタログ
- 商品リスト/検索
- カテゴリ管理
- テスト

Phase 3: ショッピングカート
- カートに追加/削除
- 数量変更
- テスト

各フェーズ完了後、次のフェーズへ進む。
すべてのフェーズが完了したら:
<promise>COMPLETE</promise>" \
  --completion-promise "COMPLETE" \
  --max-iterations 100
```

---

## プロンプト設計のベストプラクティス

### 1. 明確な完了基準を設定する

```markdown
❌ 悪い例:
「良いtodo APIを作成して」

✅ 良い例:
「todo APIを作成して。

完了条件:
- GET /todos: 一覧取得
- POST /todos: 新規作成
- PUT /todos/:id: 更新
- DELETE /todos/:id: 削除
- すべてのエンドポイントにテスト
- テスト成功率100%

完了時: <promise>COMPLETE</promise>」
```

### 2. 自己検証を含める

```markdown
「各反復で以下を実行:
1. npm test を実行
2. 結果を確認
3. 失敗があれば修正
4. 成功したら次の機能へ」
```

### 3. 安全装置を設定する

```bash
# 必ず --max-iterations を設定
/ralph-loop "タスク" --max-iterations 20
```

プロンプト内にも逃げ口を含める:

```markdown
「15回反復後も完了しない場合:
- 進捗を阻むものをドキュメント
- 試したことを列挙
- 代替案を提案
- <promise>STUCK</promise> を出力」
```

### 4. 小さなゴールに分割する

```markdown
❌ 悪い例:
「完全なeコマースプラットフォームを作成」

✅ 良い例:
「Phase 1: ユーザー認証のみを実装
  完了したら <promise>PHASE1_COMPLETE</promise>」
```

---

## Ralph Techniqueの4つの原則

| 原則 | 説明 |
|:---|:---|
| **反復 > 完璧** | 最初から完璧を目指さない。ループが洗練する |
| **失敗はデータ** | 予測可能な失敗はプロンプト改善の情報源 |
| **操作者のスキル重要** | モデル性能より、良いプロンプト作成が勝負 |
| **粘り強さが勝つ** | ループが自動的に再試行してくれる |

---

## 向いている場面・向いていない場面

### ✅ 向いている

| 場面 | 理由 |
|:---|:---|
| **明確な成功基準がある** | テスト合格、ビルド成功など |
| **反復と改善が必要** | TDD、リファクタリング |
| **一度実行して放置できる** | 人間の介入が不要 |
| **自動検証が可能** | テスト、リンター、型チェック |

### ❌ 向いていない

| 場面 | 理由 |
|:---|:---|
| **人間の判断が必要** | デザイン、UX |
| **ワンショット操作** | ファイル変換など |
| **成功基準が曖昧** | 「良い感じにして」 |
| **本番環境のデバッグ** | リスクが高い |

---

## 実績

Ralph Techniqueを使った実績:

| 案件 | 結果 |
|:---|:---|
| **Y Combinator ハッカソン** | 一晩で6つのリポジトリを生成 |
| **契約開発案件** | $50,000の案件を$297のAPIコストで完了 |
| **言語開発** | 新しいプログラミング言語を3ヶ月で実装 |

---

## Unityでの活用アイデア

### 例1: テストの自動修正

```bash
/ralph-loop "Unity Test Runnerのテストを全て通してください。

手順:
1. テストを実行
2. 失敗したテストを確認
3. コードを修正
4. 再度テストを実行
5. 全て通るまで繰り返す

全テスト成功時: <promise>ALL_TESTS_PASS</promise>" \
  --completion-promise "ALL_TESTS_PASS" \
  --max-iterations 30
```

### 例2: コードスタイルの統一

```bash
/ralph-loop "プロジェクト内のC#コードをスタイルガイドに準拠させてください。

ルール:
- private変数は _camelCase
- public変数は camelCase
- クラス名は PascalCase
- 1ファイル1クラス

各反復で1ファイルずつ修正。
全ファイル完了時: <promise>STYLE_COMPLETE</promise>" \
  --completion-promise "STYLE_COMPLETE" \
  --max-iterations 100
```

### 例3: Prefabの自動整理

```bash
/ralph-loop "Hierarchyの整理とPrefab化を行ってください。

対象:
- 重複オブジェクトのPrefab化
- 命名規則の統一
- 空のGameObjectの削除

各反復で:
1. 問題を検出
2. 1件修正
3. 状態を確認
4. 次の問題へ

全ての問題が解決したら: <promise>CLEANUP_DONE</promise>" \
  --completion-promise "CLEANUP_DONE" \
  --max-iterations 50
```

---

## 注意点

### 1. コスト管理

長時間のループはAPIコストがかかります。

```bash
# 必ず上限を設定
--max-iterations 50
```

### 2. 無限ループ防止

完了条件が曖昧だと無限ループになります。

```markdown
# 必ず明確な完了条件を設定
「以下の3つの条件をすべて満たしたらCOMPLETE:
1. テスト成功率100%
2. ビルドエラーなし
3. リンターエラーなし」
```

### 3. 監視

バックグラウンドで実行する場合も、定期的に確認を。

```bash
# キャンセルが必要な場合
/cancel-ralph
```

---

## まとめ

### Ralph Techniqueとは

- **自己参照ループでAIに自動改善させる手法**
- 同じプロンプトを繰り返し与える
- 前回の出力を読み込んで改善

### 使い方

```bash
/ralph-loop "<プロンプト>" \
  --completion-promise "<完了フレーズ>" \
  --max-iterations <上限>
```

### 成功の鍵

1. **明確な完了基準**を設定
2. **自動検証**を含める
3. **安全装置**を忘れない
4. **小さなゴール**に分割

---

## 参考リンク

- [Ralph Wiggum Plugin (公式)](https://github.com/anthropics/claude-plugins-official/tree/main/plugins/ralph-wiggum)
- [Original Ralph Technique by Geoffrey Huntley](https://ghuntley.com/ralph/)
- [Ralph Orchestrator](https://github.com/mikeyobrien/ralph-orchestrator)

---
