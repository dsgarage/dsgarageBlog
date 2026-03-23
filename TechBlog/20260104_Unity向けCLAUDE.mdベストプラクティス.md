---
title: "Unity向けCLAUDE.mdベストプラクティス"
emoji: "📋"
type: "tech"
topics: ["Unity", "ClaudeCode", "CLAUDE.md", "AI", "ゲーム開発"]
published: false
---

# Unity向けCLAUDE.mdベストプラクティス

Claude Codeを使う上で最も重要なファイルが**CLAUDE.md**です。

このファイルにプロジェクトのルールを書いておくと、AIが自動的にそのルールに従ってくれます。

本記事では、Unity開発に特化したCLAUDE.mdの書き方を解説します。

---

## CLAUDE.mdとは

### 概要

**CLAUDE.md**は、プロジェクトルートに配置する**Claude Code向けの設定ファイル**です。

Claude Codeはこのファイルを読み込み、プロジェクト固有のルールを理解します。

### なぜ必要か

```
CLAUDE.mdがない場合:
├── 毎回同じ説明を繰り返す
├── AIが勝手な命名規則を使う
├── プロジェクトのルールを無視される
└── 生成コードの品質がバラバラ

CLAUDE.mdがある場合:
├── ルールが自動的に適用される
├── 一貫したコードが生成される
├── プロジェクトの文脈を理解
└── レビュー負担が減る
```

---

## Unity向けテンプレート（完全版）

以下は、Unityプロジェクト向けの実用的なテンプレートです。

```markdown
# MyGame

2Dアクションゲームプロジェクト。プレイヤーがステージをクリアしていく形式。

## 技術スタック

- Unity 2022.3.20f1 LTS
- URP (Universal Render Pipeline)
- Input System (新Input System)
- DOTween
- UniTask
- ターゲット: iOS / Android

## フォルダ構成

Assets/
├── _Project/           # プロジェクト固有のアセット
│   ├── Scripts/        # C#スクリプト
│   │   ├── Core/       # コアシステム
│   │   ├── Player/     # プレイヤー関連
│   │   ├── Enemy/      # 敵関連
│   │   ├── UI/         # UI関連
│   │   └── Utils/      # ユーティリティ
│   ├── Prefabs/        # Prefab
│   ├── Scenes/         # シーン
│   ├── Materials/      # マテリアル
│   ├── Sprites/        # 2Dスプライト
│   └── Audio/          # サウンド
├── Plugins/            # サードパーティ
└── Resources/          # 動的ロード用（最小限に）

## コードスタイル

### 基本ルール

- インデント: スペース4つ
- 波括弧: 同じ行（K&Rスタイル）
- 1ファイル1クラス
- ファイル名はクラス名と一致

### 命名規則

| 種類 | 規則 | 例 |
|:---|:---|:---|
| クラス | PascalCase | PlayerController |
| public変数 | camelCase | moveSpeed |
| private変数 | _camelCase | _currentHealth |
| 定数 | UPPER_SNAKE | MAX_HEALTH |
| メソッド | PascalCase | TakeDamage() |
| イベント | On + 動詞 | OnPlayerDeath |

### SerializeField

// 推奨
[SerializeField] private float _moveSpeed = 5f;

// 非推奨（publicフィールド）
public float moveSpeed = 5f;

## アーキテクチャ

### シングルトン

- GameManagerのみシングルトン許可
- その他は依存注入を使用

### イベント駆動

// イベント発行
public static event Action<int> OnScoreChanged;

// イベント購読
OnScoreChanged += HandleScoreChanged;

### コンポーネント設計

- 1コンポーネント1責任
- 200行を超えるコンポーネントは分割を検討

## 禁止事項

### コード

- FindObjectOfTypeの多用禁止（起動時1回のみ許可）
- UpdateでのGetComponent禁止（キャッシュすること）
- publicフィールドの使用禁止（SerializeFieldを使用）
- magic number禁止（定数またはSerializeFieldで定義）

### 構造

- Resourcesフォルダの肥大化禁止
- Hierarchyの深いネスト禁止（5階層まで）
- 1シーンあたり2000オブジェクト超禁止

## MCP使用時の注意

### 操作の優先順位

1. MCPのAPIを使用
2. やむを得ない場合のみエディタスクリプト

### 禁止操作

- AssetDatabaseの直接操作（MCPを使うこと）
- EditorWindowの新規作成
- バッチモードスクリプトの生成

### 確認が必要な操作

- Prefabの上書き
- シーンの保存
- ビルド設定の変更
```

---

## 書くべき内容

### 1. 技術スタック

どのバージョン、どのパイプライン、どのパッケージを使っているか。

```markdown
## 技術スタック

- Unity 2022.3 LTS
- URP (Universal Render Pipeline)
- 新Input System
- Addressables
```

**なぜ重要か**: AIが適切なAPIを選択できる。古い方法を提案しなくなる。

### 2. フォルダ構成

どこに何を置くか。

```markdown
## フォルダ構成

Scripts/
├── Core/       # ゲームの基盤システム
├── Gameplay/   # ゲームプレイロジック
├── UI/         # UIコントローラー
└── Utils/      # ユーティリティ
```

**なぜ重要か**: 新規ファイルを適切な場所に作成できる。

### 3. コードスタイル

インデント、波括弧、命名規則。

```markdown
## コードスタイル

- インデント: スペース4つ
- 波括弧: 同じ行に開始
- private変数: アンダースコアプレフィックス
```

**なぜ重要か**: 生成されるコードがプロジェクトと一貫する。

### 4. 禁止事項

やってはいけないこと。

```markdown
## 禁止事項

- Updateでの文字列比較禁止
- FindObjectOfTypeの多用禁止
- publicフィールド禁止
```

**なぜ重要か**: AIが悪いパターンを提案しなくなる。

### 5. MCP固有のルール

MCPを使う上での注意点。

```markdown
## MCP使用時の注意

- Unity操作はMCP経由で行うこと
- AssetDatabaseを直接使わないこと
- Prefab変更前は確認を取ること
```

**なぜ重要か**: AIがMCPを迂回せずに正しく操作する。

---

## 命名規則の伝え方

### 明確なルール表

```markdown
## 命名規則

| 対象 | 形式 | プレフィックス | 例 |
|:---|:---|:---|:---|
| GameObject | PascalCase | なし | PlayerCharacter |
| Prefab | PascalCase | P_ | P_Enemy_Slime |
| Material | PascalCase | M_ | M_Player_Body |
| Texture | PascalCase | T_ | T_Ground_Diffuse |
| Animation Clip | PascalCase | A_ | A_Player_Run |
| Script | PascalCase | なし | PlayerController.cs |
| Scene | PascalCase | なし | MainMenu.unity |
```

### 例を添える

ルールだけでなく、具体例を示すと精度が上がります。

```markdown
### 良い例

P_Enemy_Goblin
M_Environment_Grass
PlayerController.cs

### 悪い例

enemy_goblin (アンダースコア小文字)
mat_grass (省略形)
playerController.cs (camelCase)
```

---

## フォルダ構成の説明

### 階層構造で示す

```markdown
## フォルダ構成

Assets/
├── _Project/              # プロジェクト固有（アンダースコア始まりで最上部に）
│   ├── Art/
│   │   ├── Models/        # 3Dモデル
│   │   ├── Textures/      # テクスチャ
│   │   └── Animations/    # アニメーション
│   ├── Audio/
│   │   ├── BGM/
│   │   └── SFX/
│   ├── Prefabs/
│   │   ├── Characters/
│   │   ├── Environment/
│   │   └── UI/
│   ├── Scenes/
│   │   ├── Main/          # メインシーン
│   │   └── Test/          # テスト用シーン
│   └── Scripts/
│       └── (上記参照)
├── Plugins/               # サードパーティプラグイン
├── StreamingAssets/       # ビルドに含める生データ
└── Resources/             # 動的ロード（使用は最小限に）
```

### 配置ルールを明示

```markdown
### 配置ルール

- 新規スクリプト → _Project/Scripts/ の適切なサブフォルダ
- 新規Prefab → _Project/Prefabs/ のカテゴリフォルダ
- テスト用 → 名前に _Test サフィックス、または Test/ フォルダ
- サードパーティ → Plugins/ 直下（変更しない）
```

---

## よくある間違い

### 1. 情報が多すぎる

```markdown
# 悪い例（情報過多）
すべてのクラスの詳細、変更履歴、メンバー全員の名前...
```

**Claude Codeは必要な情報を取捨選択できます。**
重要なルールを簡潔に書くのがベスト。

### 2. 曖昧な表現

```markdown
# 悪い例
コードは綺麗に書いてください
適切な命名をしてください
```

**具体的なルールを書く:**

```markdown
# 良い例
- private変数は _ プレフィックス
- 1メソッド30行以内
- クラスは200行以内
```

### 3. 更新されていない

プロジェクトの進行とともに、CLAUDE.mdも更新が必要です。

```markdown
## 更新履歴

- 2025-01-01: 初版作成
- 2025-01-15: 命名規則にMaterialを追加
- 2025-02-01: MCP使用ルールを追加
```

---

## まとめ

### CLAUDE.mdの役割

- **プロジェクトルールをAIに共有**
- 一貫したコード生成
- レビュー負担の軽減

### 書くべき内容

| セクション | 内容 |
|:---|:---|
| 技術スタック | Unity版、パイプライン、パッケージ |
| フォルダ構成 | どこに何を置くか |
| コードスタイル | インデント、命名規則 |
| アーキテクチャ | 設計方針 |
| 禁止事項 | やってはいけないこと |
| MCP注意 | MCP固有のルール |

### ポイント

- 簡潔に書く
- 具体的に書く
- 例を添える
- 定期的に更新

---

## 参考リンク

- [CC2UniMCP GitHub](https://github.com/dsgarage/CC2UniMCP)
- [Claude Code 公式](https://claude.com/claude-code)
- [Unity C# スタイルガイド](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity)

---
