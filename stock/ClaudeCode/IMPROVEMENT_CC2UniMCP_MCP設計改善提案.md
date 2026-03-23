---
title: "CC2UniMCP MCP設計改善提案"
emoji: "🔧"
type: "tech"
topics: ["Unity", "MCP", "CC2UniMCP", "設計", "最適化"]
published: false
---

# CC2UniMCP MCP設計改善提案

## 背景

Zennの記事「[MCPサーバー設計のベストプラクティス](https://zenn.dev/ncdc/articles/c1d65b6e939185)」で指摘されている問題点を、CC2UniMCPの現状と照らし合わせて改善提案をまとめる。

---

## 記事の要点

### 3つの主要な問題

| 問題 | 内容 | 影響 |
|:---|:---|:---|
| **①ツール数が多すぎる** | APIをそのまま転記すると10個以上に | AIの判断が混乱 |
| **②説明文が冗長** | API仕様書の詳細を転記 | トークン消費増大 |
| **③レスポンスが大きすぎる** | 汎用APIは数十フィールド返す | 履歴蓄積でコスト爆発 |

### 推奨される対策

- ツールは**統合**し、操作種別をパラメータ化
- 説明は**200文字以下**に絞る
- レスポンスは**必要最小限**に加工
- **Prompt Caching**でツール定義をキャッシュ
- **ページング**で大量データを分割

---

## CC2UniMCP現状分析

### 1. ツール数

```
現在の総API数: 777個

内訳:
├── Public Edition:   116個
├── Standard Edition: 449個
└── Pro Edition:      215個
```

**カテゴリ別Top5:**

| カテゴリ | API数 | 割合 |
|:---|:---|:---|
| uitoolkit | 187 | 24% |
| probuilder | 93 | 12% |
| cinemachine | 47 | 6% |
| lighting | 45 | 6% |
| timeline | 42 | 5% |

**問題点:**
- 777個は記事の「10個以上で判断が混乱」の**77倍**
- UIToolkitだけで187個 → AIが選択に迷う
- 毎回全ツール定義を読み込むとトークン消費が膨大

### 2. 説明文の長さ

```
現状統計:
├── 最短:  6文字
├── 最長: 149文字
└── 平均: 110-130文字
```

**良い例（簡潔）:**
```
"1フレーム進める"
"シーンを開く"
"現在のシーンを保存"
```

**改善が必要な例:**
```
"プロパティを削除"  → 何のプロパティ？
"要素を作成"        → 何の要素？
```

**問題点:**
- 一部の説明が短すぎて意図が不明確
- パラメータ説明が混在している場合がある

### 3. レスポンスサイズ

**tools/list のレスポンス構造:**

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "result": {
    "tools": [
      // 777個のツール定義がここに入る
      {
        "name": "unity.editor.play",
        "description": "Playモードを開始",
        "category": "editor",
        "inputSchema": { ... },
        "useCases": [...],
        "relatedApis": [...]
      },
      // ... 残り776個
    ],
    "totalCount": 777,
    "categories": [...],
    "edition": { ... },
    "guidelines": { ... }
  }
}
```

**問題点:**
- 777個を一括送信 → 推定50KB〜100KB
- 毎回の会話で履歴に残る → コスト爆発

---

## 改善提案

### 提案1: ツール統合（粒度の最適化）

**現状: 個別API方式**
```
unity.uitoolkit.button.create
unity.uitoolkit.button.delete
unity.uitoolkit.button.setProperty
unity.uitoolkit.label.create
unity.uitoolkit.label.delete
unity.uitoolkit.label.setProperty
...（187個）
```

**改善案A: 汎用API方式**
```
unity.uitoolkit.element.create    # 全要素共通
unity.uitoolkit.element.delete    # 全要素共通
unity.uitoolkit.element.setProperty  # 全要素共通

# 要素タイプをパラメータで指定
{
  "type": "button",  // button, label, textfield, etc.
  "name": "StartButton",
  "properties": { ... }
}
```

**改善案B: バッチAPI方式**
```
unity.uitoolkit.batch.execute
{
  "operations": [
    { "action": "create", "type": "button", "name": "Start" },
    { "action": "create", "type": "label", "name": "Title" },
    { "action": "setProperty", "target": "Start", "property": "text", "value": "開始" }
  ]
}
```

**効果:**
- 187個 → 3〜5個に削減（95%以上削減）
- AIの選択負荷が大幅減少

### 提案2: カテゴリ別ツールリスト

**現状:**
```
tools/list → 777個一括返却
```

**改善案:**
```
tools/list                    → カテゴリ一覧のみ返却
tools/list?category=uitoolkit → UIToolkit関連のみ返却
tools/list?category=timeline  → Timeline関連のみ返却
```

**レスポンス例:**
```json
// tools/list （カテゴリ一覧モード）
{
  "categories": [
    { "name": "editor", "toolCount": 15, "description": "エディタ制御" },
    { "name": "scene", "toolCount": 12, "description": "シーン操作" },
    { "name": "uitoolkit", "toolCount": 5, "description": "UI構築" },
    ...
  ],
  "totalTools": 50,  // 統合後の数
  "message": "カテゴリを指定して詳細を取得: tools/list?category=xxx"
}
```

**効果:**
- 初回ロードが軽量化
- AIが必要なカテゴリのみ取得可能

### 提案3: 説明文の標準化

**フォーマット統一:**
```
[動詞] + [対象] + [補足（オプション）]
```

**改善例:**

| 現状 | 改善後 |
|:---|:---|
| "プロパティを削除" | "指定オブジェクトのプロパティを削除する" |
| "要素を作成" | "UI要素を指定コンテナ内に作成する" |
| "1フレーム進める" | "エディタを1フレーム進める（デバッグ用）" |

**文字数ガイドライン:**
- 最小: 20文字（意図が伝わる最小限）
- 推奨: 50〜80文字
- 最大: 150文字（これ以上は分割）

### 提案4: レスポンスの軽量化

**現状のレスポンス:**
```json
{
  "result": {
    "gameObjects": [
      {
        "name": "Player",
        "instanceId": 12345,
        "path": "/Player",
        "layer": 0,
        "tag": "Player",
        "isActive": true,
        "isStatic": false,
        "transform": {
          "position": {"x": 0, "y": 1, "z": 0},
          "rotation": {"x": 0, "y": 0, "z": 0, "w": 1},
          "scale": {"x": 1, "y": 1, "z": 1}
        },
        "components": [
          {"type": "Transform"},
          {"type": "Rigidbody"},
          {"type": "CapsuleCollider"}
        ],
        "children": [...]
      },
      // ... 大量のオブジェクト
    ]
  }
}
```

**改善案A: サマリーモード**
```json
{
  "result": {
    "summary": {
      "totalObjects": 150,
      "topLevelObjects": 12,
      "activeObjects": 145
    },
    "topLevel": [
      {"name": "Player", "childCount": 3},
      {"name": "Environment", "childCount": 50},
      {"name": "UI", "childCount": 25}
    ],
    "message": "詳細は unity.scene.getObject で個別取得"
  }
}
```

**改善案B: フィールド選択**
```json
// リクエスト
{
  "method": "unity.scene.getObjects",
  "params": {
    "fields": ["name", "path", "isActive"],  // 必要なフィールドのみ
    "depth": 1  // 子オブジェクトは1階層まで
  }
}

// レスポンス（軽量）
{
  "result": [
    {"name": "Player", "path": "/Player", "isActive": true},
    {"name": "Enemy", "path": "/Enemy", "isActive": true}
  ]
}
```

### 提案5: Prompt Caching対応

**Claude API の Prompt Caching:**
- ツール定義をキャッシュ可能
- 2回目以降のコストを**1/10に削減**

**実装案:**

```javascript
// mcp-bridge/index.js
const TOOL_DEFINITIONS_CACHE_KEY = "unity-mcp-tools-v1";

async function getToolDefinitions() {
  // Claude APIのPrompt Caching用ヘッダー
  return {
    tools: cachedToolDefinitions,
    cache_control: {
      type: "ephemeral",
      ttl: 3600  // 1時間キャッシュ
    }
  };
}
```

### 提案6: エディション別の最適化

**現状:**
- Public/Standard/Proの3エディション
- 全エディションのツールが一括で読み込まれる

**改善案:**
```
# 設定ファイルで利用エディションを明示
.unity-mcp-config.json
{
  "edition": "standard",
  "enabledCategories": ["editor", "scene", "gameObject", "prefab"],
  "disabledCategories": ["probuilder", "shader"]
}

# 結果: 必要なツールのみ返却
tools/list → Standard + 有効カテゴリのみ（例: 200個）
```

---

## Skills連携による最適化

MCP APIの最適化に加え、**Skills（ワークフロー定義）との連携**により、さらなる効率化が可能。

### Skillsとは

Claude Codeの機能で、**よく使うワークフローをスラッシュコマンドで呼び出せる**仕組み。

```
ユーザー: /setup-player

→ Skillに定義された手順をAIが実行
  1. unity.gameobject.create("Player")
  2. unity.component.add("CharacterController", {...})
  3. unity.component.add("Rigidbody", {...})
  4. ...
```

### MCP最適化 × Skills の関係

```
┌─────────────────────────────────────────────────────────┐
│                    最適化レイヤー                        │
│                                                         │
│  ┌─────────────────────────────────────────────────┐   │
│  │  Layer 3: Skills（ワークフロー抽象化）          │   │
│  │  └── 複数操作を1つのコマンドに束ねる            │   │
│  └─────────────────────────────────────────────────┘   │
│                         ↓                               │
│  ┌─────────────────────────────────────────────────┐   │
│  │  Layer 2: MCP最適化（提案1-6）                  │   │
│  │  └── ツール数削減、レスポンス軽量化             │   │
│  └─────────────────────────────────────────────────┘   │
│                         ↓                               │
│  ┌─────────────────────────────────────────────────┐   │
│  │  Layer 1: MCP API（777個）                      │   │
│  │  └── 個別のUnity操作                            │   │
│  └─────────────────────────────────────────────────┘   │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

### 提案7: Skills連携API最適化

インストールされたSkillsに基づいて、必要なAPIカテゴリのみを自動で有効化する。

**Skill → APIカテゴリ マッピング:**

```json
// skills-api-mapping.json
{
  "setup-player-2d": {
    "requiredCategories": ["gameObject", "component", "sprite", "physics2d", "animator"]
  },
  "setup-camera": {
    "requiredCategories": ["gameObject", "component", "camera", "cinemachine"]
  },
  "create-ui": {
    "requiredCategories": ["gameObject", "ugui", "uitoolkit"]
  }
}
```

**動作フロー:**

```
1. インストール済みSkillsを取得
2. 各Skillの requiredCategories を集約
3. 有効カテゴリリストを生成
4. tools/list で有効カテゴリのみ返却
```

**効果:**

| 状況 | API数 |
|:---|:---|
| Skills なし（全API） | 777個 |
| 2D系Skills のみ | ~150個 |
| UI系Skills のみ | ~120個 |

### 提案8: プロファイルシステム

プロジェクトタイプ別の推奨設定をプロファイルとして提供する。

**プロファイル例:**

| ID | 名前 | Skills | API数目安 |
|:---|:---|:---|:---|
| `2d-action` | 2Dアクション | player-2d, enemy-2d, ui | ~150 |
| `3d-fps` | 3D FPS | player-fps, weapon, enemy-3d | ~200 |
| `ui-app` | UIアプリ | create-ui, setup-canvas | ~120 |
| `cinematic` | シネマティック | setup-camera, timeline | ~180 |
| `full` | フル機能 | 全Skills | 777 |

**プロファイル定義:**

```json
// profiles/2d-action.json
{
  "id": "2d-action",
  "name": "2Dアクションゲーム",
  "skills": ["setup-player-2d", "create-enemy-2d", "create-ui"],
  "categories": {
    "required": ["editor", "scene", "gameObject", "component"],
    "enabled": ["sprite", "physics2d", "animator", "ugui"],
    "optional": ["timeline", "cinemachine"]
  },
  "estimatedApiCount": 150
}
```

### 提案9: セットアップUI改善

初回セットアップをウィザード形式に改善し、プロファイル選択を直感的にする。

**ウィザードフロー:**

```
Step 1: プロジェクトタイプ選択
┌─────────────────────────────────────────────────────────┐
│  どのようなプロジェクトを作成しますか？                 │
│                                                         │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐    │
│  │  🎮 2Dゲーム │  │  🎮 3Dゲーム │  │  🖥️ UIアプリ │    │
│  │  API: ~150  │  │  API: ~200  │  │  API: ~120  │    │
│  └─────────────┘  └─────────────┘  └─────────────┘    │
└─────────────────────────────────────────────────────────┘

Step 2: オプション機能選択
┌─────────────────────────────────────────────────────────┐
│  追加機能を選択してください:                            │
│                                                         │
│  ☐ Timeline（カットシーン用）                          │
│  ☐ Cinemachine（カメラ演出用）                         │
│  ☑ Prompt Caching（推奨）                              │
└─────────────────────────────────────────────────────────┘

Step 3: 確認・実行
┌─────────────────────────────────────────────────────────┐
│  プロファイル: 2Dアクション                            │
│  有効API数: 150個（777個中）                           │
│  インストールSkills: 8個                               │
│                                                         │
│  [セットアップ実行]                                     │
└─────────────────────────────────────────────────────────┘
```

### Skills連携の効果まとめ

```
Skillsで解決できる:
├── 定型作業の自動化
├── ツール選択の認知負荷（Skillが手順を指定）
├── 一貫性の確保
└── ユーザーの学習コスト削減

MCP最適化で解決できる:
├── 初回のツール定義ロードコスト
├── 非定型作業でのツール選択
├── 大きなレスポンスの蓄積
└── APIそのものの複雑さ

両方を組み合わせることで:
├── 定型作業 → Skillsで即実行
├── 非定型作業 → 最適化されたMCPで効率的に実行
└── トータルでコスト・精度が向上
```

---

## 優先度マトリクス

### MCP API最適化（v0.9.0）

| 提案 | 効果 | 実装難易度 | 優先度 |
|:---|:---|:---|:---|
| 提案1: ツール統合 | 高 | 高 | ★★★ |
| 提案2: カテゴリ別リスト | 高 | 中 | ★★★★★ |
| 提案3: 説明文標準化 | 中 | 低 | ★★★★ |
| 提案4: レスポンス軽量化 | 高 | 中 | ★★★★ |
| 提案5: Prompt Caching | 高 | 低 | ★★★★★ |
| 提案6: エディション最適化 | 中 | 低 | ★★★ |

### Skills連携（v0.9.0）

| 提案 | 効果 | 実装難易度 | 優先度 | 依存 |
|:---|:---|:---|:---|:---|
| 提案7: Skills連携API | 高 | 中 | ★★★★ | 提案2 |
| 提案8: プロファイル | 高 | 中 | ★★★★ | 提案7 |
| 提案9: セットアップUI | 中 | 中 | ★★★ | 提案7,8 |

---

## 実装ロードマップ

### 依存関係図

```
v0.9.0 実装順序

Phase 1（基盤・並行可能）
┌─────────────────────────────────────────────────────┐
│  #92 Prompt Caching    #93 カテゴリ別リスト        │
│  #94 レスポンス軽量化  #95 ツール統合（長期）      │
└─────────────────────────────────────────────────────┘
                      ↓
Phase 2（Skills連携）
┌─────────────────────────────────────────────────────┐
│  #96 Skills連携API最適化                            │
│      └── #93に依存                                  │
└─────────────────────────────────────────────────────┘
                      ↓
Phase 3（プロファイル）
┌─────────────────────────────────────────────────────┐
│  #97 プロファイルシステム                           │
│      └── #96に依存                                  │
└─────────────────────────────────────────────────────┘
                      ↓
Phase 4（UI）
┌─────────────────────────────────────────────────────┐
│  #98 セットアップUI改善                             │
│      └── #96, #97に依存                             │
└─────────────────────────────────────────────────────┘
```

### 推奨実装順

| 順番 | Issue | 提案 | 理由 |
|:---|:---|:---|:---|
| 1 | #93 | 提案2: カテゴリ別リスト | 他の最適化の基盤 |
| 2 | #92 | 提案5: Prompt Caching | 即効性が高い |
| 3 | #94 | 提案4: レスポンス軽量化 | #93と並行可能 |
| 4 | #96 | 提案7: Skills連携 | #93完了後 |
| 5 | #97 | 提案8: プロファイル | #96完了後 |
| 6 | #98 | 提案9: セットアップUI | #97完了後 |
| 7 | #95 | 提案1: ツール統合 | 長期（他と並行可） |

---

## 期待される効果

### コスト削減

```
現状（777ツール一括）:
├── tools/list: 約100KB
├── 毎会話でのトークン: 約30,000トークン
└── 月間コスト: $XXX

改善後:
├── tools/list: 約10KB（カテゴリ別）
├── 毎会話でのトークン: 約3,000トークン（Caching込み）
└── 月間コスト: $XXX × 0.1〜0.3
```

### 精度向上

```
現状:
├── AIがツール選択に迷う確率: 高
├── 誤ったAPIを呼ぶケース: 時々発生
└── リトライ回数: 平均1.5回

改善後:
├── AIがツール選択に迷う確率: 低
├── 誤ったAPIを呼ぶケース: 稀
└── リトライ回数: 平均1.0回
```

---

## 参考資料

- [MCPサーバー設計のベストプラクティス](https://zenn.dev/ncdc/articles/c1d65b6e939185)
- [Anthropic Prompt Caching](https://docs.anthropic.com/claude/docs/prompt-caching)
- [Model Context Protocol仕様](https://modelcontextprotocol.io/)

---

## GitHub Issues

### マイルストーン: v0.9.0 - API最適化

#### MCP API最適化

| Issue | タイトル | 提案 | 状態 |
|:---|:---|:---|:---|
| [#92](https://github.com/dsgarage/CC2UniMCP/issues/92) | Prompt Caching対応 | 提案5 | Open |
| [#93](https://github.com/dsgarage/CC2UniMCP/issues/93) | カテゴリ別ツールリスト | 提案2 | Open |
| [#94](https://github.com/dsgarage/CC2UniMCP/issues/94) | レスポンス軽量化 | 提案4 | Open |
| [#95](https://github.com/dsgarage/CC2UniMCP/issues/95) | ツール統合（長期） | 提案1 | Open |

#### Skills連携

| Issue | タイトル | 提案 | 依存 | 状態 |
|:---|:---|:---|:---|:---|
| [#96](https://github.com/dsgarage/CC2UniMCP/issues/96) | Skills連携API最適化 | 提案7 | #93 | Open |
| [#97](https://github.com/dsgarage/CC2UniMCP/issues/97) | プロファイルシステム | 提案8 | #96 | Open |
| [#98](https://github.com/dsgarage/CC2UniMCP/issues/98) | セットアップUI改善 | 提案9 | #96,#97 | Open |

### 関連マイルストーン: v0.8.0 - MCPサーバーコア

| Issue | タイトル |
|:---|:---|
| [#77](https://github.com/dsgarage/CC2UniMCP/issues/77) | API数の爆発問題 - tools/listの最適化 |
| [#78](https://github.com/dsgarage/CC2UniMCP/issues/78) | Node.jsブリッジのオーバーヘッド削減 |
| [#79](https://github.com/dsgarage/CC2UniMCP/issues/79) | API選択・発見の効率性向上 |
| [#80](https://github.com/dsgarage/CC2UniMCP/issues/80) | 接続・セットアップの簡素化 |
| [#81](https://github.com/dsgarage/CC2UniMCP/issues/81) | リアルタイム性・状態管理の改善 |
| [#82](https://github.com/dsgarage/CC2UniMCP/issues/82) | エラーハンドリング・デバッグ機能の強化 |
| [#83](https://github.com/dsgarage/CC2UniMCP/issues/83) | C#スクリプト生成フォールバック問題 |

---

## 更新履歴

| 日付 | 内容 |
|:---|:---|
| 2025-12-29 | Skills連携提案追加（提案7-9）、マイルストーン設定（v0.8.0/v0.9.0） |
| 2025-12-29 | GitHub Issues作成（#92-#98） |
| 2025-12-28 | 初版作成 |

---
