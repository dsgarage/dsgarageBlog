# Claude Code新スキル /simplify と /batch — Unity開発のPRワークフローとコードマイグレーションを自動化する

## はじめに

「実装は終わった。でもPRに出す前に、もう一度コード全体を見直さないと……」

Unity開発でClaude Codeを使っていると、長時間のセッションの末にコードが冗長になっていたり、同じパターンが複数ファイルに散らばっていたりすることがあります。手動でのレビューと整理に30分。機能は動くのに、PRに出せる品質にするための作業が重い。

さらに厄介なのが大規模マイグレーション。Unity 6へのAPI更新、非推奨メソッドの置き換え——数十ファイルに散らばる同じパターンの修正を、1つずつ手で直していく。丸一日かかることも珍しくありません。

Claude Codeに追加された2つの新スキル **`/simplify`** と **`/batch`** は、まさにこの2つの苦痛を自動化します。本記事では、それぞれの仕組みと、Unity開発での具体的な活用方法を解説します。

## 対象読者

- Unity開発者（C#スクリプトを日常的に書く方）
- Claude Codeを使ってAIコーディングをしている方
- PRレビューや大規模リファクタリングの効率化に関心がある方

:::note info
**Unity開発者の方へ**
Claude Codeは強力なツールですが、Unity Editorを直接操作することはできません。
この問題を解決するのが**UniMCP4CC**（Unity MCP Server for Claude Code）です。
GitHub: [dsgarage/UniMCP4CC](https://github.com/dsgarage/UniMCP4CC)
:::

---

## /simplify — PRに出す前の「仕上げ」を自動化

### 何をするスキルか

`/simplify` は、セッション中に変更したコードを3つの観点で自動レビュー・修正するスキルです。

| 観点 | チェック内容 | 例 |
|:---|:---|:---|
| **再利用性** | 重複コードの検出・統合 | 同じ `GetComponent<T>()` が3箇所 → フィールドキャッシュに統合 |
| **品質** | 可読性・保守性の改善 | ネストした三項演算子 → switch文に変換 |
| **効率性** | 不要な処理の削除 | Update()内の毎フレーム `FindObjectOfType()` を検出 |

**最大の特徴は、機能を一切変えないこと**です。関数の戻り値、エッジケース処理、副作用はすべて保持されます。「何をするか」は不変で、「どう書くか」だけが改善される。つまり、既存のテストはそのまま全パスします。

### 使い方

セッション内で `/simplify` と入力するだけです。

```
> /simplify
```

複数の並列エージェントが起動し、変更ファイルをそれぞれの観点でチェック。問題を検出したら自動修正します。

### 推奨ワークフロー

```
Step 1: 機能を実装する
├── Claude Codeで実装
└── 動作確認

Step 2: 実装コミット
├── git add & git commit
└── "feat: プレイヤー移動機能を実装"

Step 3: /simplify を実行  ← ここがポイント
├── 重複コードの検出・統合
├── 可読性の改善
└── 不要な処理の削除

Step 4: 変更を確認
├── git diff で修正内容をレビュー
└── 意図しない変更がないか確認（必須）

Step 5: 改善コミット & PR作成
├── git commit -m "refactor: /simplify で品質改善"
└── gh pr create
```

### Unity開発での具体例

以下のようなコードがセッション中に書かれたとします。

```csharp
// /simplify 実行前
public class EnemySpawner : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var prefab = Resources.Load<GameObject>("Enemy");
            var instance = Instantiate(prefab);
            instance.GetComponent<Rigidbody>().mass = 2f;
            instance.GetComponent<Rigidbody>().drag = 0.5f;
            instance.GetComponent<Rigidbody>().useGravity = true;
            instance.GetComponent<MeshRenderer>().material.color = Color.red;
            instance.GetComponent<MeshRenderer>().shadowCastingMode
                = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
```

`/simplify` を実行すると、以下のように改善されます。

```csharp
// /simplify 実行後 — 機能は完全に同一
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var instance = Instantiate(enemyPrefab);

            var rb = instance.GetComponent<Rigidbody>();
            rb.mass = 2f;
            rb.drag = 0.5f;
            rb.useGravity = true;

            var renderer = instance.GetComponent<MeshRenderer>();
            renderer.material.color = Color.red;
            renderer.shadowCastingMode
                = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
```

**改善点：**
- `Resources.Load` → `SerializeField` に変更（Unityのベストプラクティス）
- `GetComponent` の重複呼び出し → ローカル変数にキャッシュ
- 関連する設定をグループ化して可読性向上

いずれも動作は完全に同一です。

---

## /batch — 大規模マイグレーションを並列で一気に実行

### 何をするスキルか

`/batch` は、コードベース全体に影響する変更を**自動分解・並列実行・PR自動作成**するスキルです。

従来、数十ファイルにまたがるマイグレーション作業は、1ファイルずつ修正 → テスト → コミットの繰り返しでした。`/batch` はこの作業を並列化し、各ユニットを独立した git worktree で同時に処理します。

### 実行フロー

```
Phase 1: 調査
├── Exploreエージェントがコードベースを分析
├── 影響ファイルを特定
└── 依存関係を解析

Phase 2: 計画
├── 5〜30の独立ユニットに分解
├── ユニット間のファイル衝突を回避
└── ユーザーに計画を提示 → 承認待ち

Phase 3: 並列実行（承認後）
├── 各ユニット = 1ワーカー × 1 git worktree
├── 実装 → テスト → /simplify → コミット
└── 全ワーカーが同時並行

Phase 4: PR作成
├── 各ユニットが自動でPRをオープン
└── オーケストレーターが結果を集約
```

### 使い方

自然言語で変更内容を指示するだけです。

```
> /batch replace all deprecated OnGUI methods with UI Toolkit equivalents in Assets/Scripts/
```

するとClaude Codeが自動で：

1. 対象ファイルを全探索
2. 独立したユニットに分解して計画を提示
3. ユーザーの承認後、並列実行を開始
4. 完了後、ユニットごとのPR URLを一覧表示

### /batch の内部アーキテクチャ

各ワーカーは独立した git worktree で作業するため、ファイルシステムレベルで完全に分離されています。

```
Repository/
├── .git/                         （共有リポジトリ）
├── Assets/                       （メインワーキングディレクトリ）
└── .claude/worktrees/
    ├── worker-unit-1/Assets/     ← Unit 1: Scripts/Player/
    ├── worker-unit-2/Assets/     ← Unit 2: Scripts/Enemy/
    ├── worker-unit-3/Assets/     ← Unit 3: Scripts/UI/
    └── worker-unit-4/Assets/     ← Unit 4: Editor/
```

さらに重要なのは、**各ワーカーが完了時に自動で `/simplify` を実行する**点です。つまり、生成されるPRはすでに品質チェック済みの状態です。

---

## Unity開発での活用シナリオ

### シナリオ1: Unity 6へのAPI更新（/batch）

Unity 6で非推奨になったAPIを一括更新する。

```
> /batch update all deprecated Unity 2022 APIs to Unity 6 equivalents in Assets/
```

| 非推奨API | Unity 6での代替 |
|:---|:---|
| `Application.isPlaying` チェックなしの `OnDestroy` | null チェック追加 |
| `UnityWebRequest` の旧コールバック | `await` パターンに変更 |
| `PlayerPrefs` の直接参照 | `Settings` クラス経由に変更 |

50ファイルの更新が、手作業なら**丸1日**。`/batch` なら**並列実行で数十分**。

### シナリオ2: テストフレームワーク移行（/batch）

NUnit の `[Test]` から `[UnityTest]` + `IEnumerator` への移行。

```
> /batch convert [Test] async methods to [UnityTest] IEnumerator pattern in Assets/Tests/
```

### シナリオ3: 名前空間の統一（/batch）

```
> /batch add namespace DSGarage.UnityMCP to all C# files in Assets/Scripts/ that lack a namespace
```

フォルダごとに独立ユニットとして処理。asmdef 境界に沿った分解が自動で行われます。

### シナリオ4: PR前の最終仕上げ（/simplify）

Editor拡張やテストコードなど、長時間セッションで書いた冗長なコードを一括改善。

```
# 2時間のセッション後…
> /simplify
```

---

## 従来ワークフローとの比較

### PRを出すまで（/simplify の効果）

| 工程 | 従来 | /simplify 導入後 |
|:---|:---|:---|
| 実装 | 60分 | 60分（変わらない） |
| 手動レビュー・整理 | 30分 | **0分**（自動化） |
| /simplify 実行 | — | 2〜3分 |
| 差分確認 | — | 5分 |
| **合計** | **90分** | **67〜68分** |

### 大規模マイグレーション（/batch の効果）

| 規模 | 従来（手作業） | /batch |
|:---|:---|:---|
| 10ファイル | 2時間 | 15分 |
| 50ファイル | 1日 | 30分 |
| 100ファイル | 2〜3日 | 1時間 |

並列実行のため、ファイル数が増えても処理時間は線形には増えません。

---

## 2つのスキルの関係性

`/simplify` と `/batch` は独立して使えますが、組み合わせることで最大の効果を発揮します。

| | /simplify | /batch |
|:---|:---|:---|
| **用途** | セッション内の品質改善 | 大規模な並列マイグレーション |
| **対象範囲** | 最近変更したファイル | コードベース全体の該当箇所 |
| **出力** | コードの自動修正 | 複数PRを自動作成 |
| **実行単位** | 1セッション内 | 複数 git worktree で並列 |
| **使用頻度** | 毎日（PR前に毎回） | 必要時（マイグレーション等） |

**`/batch` は内部で `/simplify` を自動実行する**ため、生成されるPRは最初から品質チェック済みです。

```
日常の開発:
  実装 → commit → /simplify → 確認 → commit → PR

大規模マイグレーション:
  /batch（内部で /simplify 含む） → N個のPRが自動生成
```

---

## Unity開発での注意点

### /simplify を使う際の注意

| 注意点 | 対応 |
|:---|:---|
| `SerializeField` の変更 | Inspector上の参照が切れる可能性。Prefab確認が必要 |
| Editor スクリプトの変更 | `CustomEditor` のレイアウトが変わることがある |
| 自動修正の確認 | 必ず `git diff` で変更内容を目視確認してからコミット |

### /batch を使う際の注意

| 注意点 | 対応 |
|:---|:---|
| `.meta` ファイル | ファイルリネームを伴う変更はmetaの整合性に注意 |
| asmdef 境界 | 計画フェーズでユニット分割がasmdef境界と合っているか確認 |
| Prefab参照 | クラス名変更はシリアライズ参照を壊す可能性。計画段階で除外を指示 |
| Editor / Runtime分離 | `#if UNITY_EDITOR` 内のコードは別ユニットにすべき |

---

## まとめ

| 項目 | /simplify | /batch |
|:---|:---|:---|
| 何をするか | 変更コードの品質を自動改善 | 大規模変更を並列実行・PR自動作成 |
| いつ使うか | PR作成前（毎回） | マイグレーション時 |
| 実行時間 | 2〜3分 | 規模による（15分〜1時間） |
| Unity での主な用途 | MonoBehaviourの整理、テストコード改善 | API更新、名前空間統一、テスト移行 |

`/simplify` は「PRに出す前の30分の手作業」を自動化し、`/batch` は「丸1日かかるマイグレーション」を並列で数十分に短縮します。

どちらも「コードの機能は変えずに、品質と開発速度を上げる」という共通の設計思想を持っています。Unity開発のように大量のスクリプトファイルを扱うプロジェクトでは、この2つのスキルが日常のワークフローを大きく変えるはずです。

---

## 参考リンク

- [Claude Code 公式ドキュメント](https://docs.anthropic.com/en/docs/claude-code)
- [Boris Cherny（/simplify & /batch 開発者）による紹介](https://www.threads.com/@boris_cherny/post/DVR-HzBkqRd/)
- [UniMCP4CC — Unity MCP Server for Claude Code](https://github.com/dsgarage/UniMCP4CC)
