# Figma MCPでデザインからUnity UIを生成する — /simplify・/batchとの連携ワークフロー

## はじめに

「Figmaにデザインがあるのに、Unity側のUIは手書き」——この非対称性に、毎回時間を取られていませんか。

デザイナーが Figma で作った画面を見ながら、RectTransform の数値を手入力し、カラーコードをコピーし、フォントサイズを合わせる。1画面あたり30分〜1時間。10画面あれば丸1日が消えます。

本記事では、**Figma MCP**（Model Context Protocol）を使って、AIがFigmaデザインを直接読み取り、Unity向けのUIコードに変換するワークフローを解説します。さらに、`/simplify` と `/batch` を組み合わせることで、複数画面への横展開まで自動化する方法を紹介します。

## 対象読者

- Unity開発者（UI Toolkit / uGUI を使う方）
- Figmaでデザインを受け取ってUnityに落とし込む工程がある方
- Claude Codeを使っている、または導入を検討している方

## Figma MCPとは何か

MCP（Model Context Protocol）は、AIと外部ツールを接続するためのプロトコルです。Figma MCPは、このプロトコルを通じてClaude CodeがFigmaのデザインデータに直接アクセスできるようにする仕組みです。

従来のワークフローでは、人間がFigmaとUnity Editorの間を行き来してデザインを転写していました。Figma MCPを使うと、**AIがデザインを読み取る**工程が自動化されます。

![従来 vs Figma MCP ワークフロー比較](images/figma_mcp_comparison.png)

### 主要ツール

Figma MCPには複数のツールがありますが、Unity開発で使うのは主に以下の3つです。

| ツール | 用途 | 戻り値 |
|:---|:---|:---|
| `get_design_context` | デザインの構造・スタイル・コードヒントを取得 | コード + スクリーンショット + ヒント |
| `get_screenshot` | 特定ノードのスクリーンショットを取得 | 画像データ |
| `get_variable_defs` | デザイントークン（色・余白・フォント等の変数）を取得 | 変数定義一覧 |

## セットアップ

### 前提条件

- Claude Code がインストール済み
- Figma アカウントを持っている

### Claude Codeへの登録確認

Figma MCPが正しく登録されているかを確認するには、Claude Code上で以下のように呼び出します。

```
> Figma MCPのwhoamiを実行して
```

認証済みであれば、メールアドレスとプラン情報が返ってきます。

```json
{
  "email": "your@email.com",
  "handle": "yourname",
  "plans": [{ "name": "Your Team", "seat": "Full", "tier": "starter" }]
}
```

このレスポンスが返れば、Figma MCPは使用可能な状態です。

## 問題: MCPは登録されていても使われない

ここで重要な注意点があります。

**MCPツールは登録されていても、AIが自発的に使わないケースが多い**。これはFigma MCPに限らず、MCP全般に共通する課題です。

たとえば「このFigmaのデザインを実装して」と指示しても、AIがFigma MCPのツールを呼び出さず、URLを見てなんとなくの推測で実装してしまうことがあります。

### 原因

AIにとってMCPツールは「使える手段の1つ」に過ぎません。明示的に指示しなければ、既存の知識だけで回答しようとする傾向があります。

### 解決策: CLAUDE.mdにルールを書く

プロジェクトの `CLAUDE.md` に以下のルールを追加することで、**AIがFigma MCPを確実に使うように制御できます**。

```markdown
## Figma MCP 利用ルール

### 必須手順
- Figma URL が提示されたら、必ず `get_design_context` でデザインデータを取得すること
- URL を見て推測で実装することは禁止。必ずMCPツールでデータを取得してから実装する
- デザイントークン（色・余白・フォントサイズ）は `get_variable_defs` で取得し、ハードコードしない
- 実装後は `/simplify` を実行すること

### URL の読み方
- `figma.com/design/:fileKey/:fileName?node-id=:nodeId`
  - nodeId の "-" は ":" に変換して使う
- `figma.com/design/:fileKey/branch/:branchKey/:fileName`
  - branchKey を fileKey として使う

### 出力形式
- UI Toolkit（UXML + USS）を優先する
- uGUI が必要な場合は明示的に指示する
```

この設定により、Figma URLが会話に登場した時点で、AIが自動的にMCPツールを呼び出すようになります。

## 実践ワークフロー: 1画面のUI実装

![1画面のUI実装フロー](images/figma_mcp_single_flow.png)

### Step 1: Figmaデザインの取得

Claude Codeに Figma の URL を貼り、以下のように指示します。

```
このFigmaのデザインを get_design_context で取得して、
Unity UI Toolkit の UXML + USS として実装して。

https://www.figma.com/design/XXXXXXX/MyGame?node-id=123-456
```

`get_design_context` は以下の情報を返します。

- **コード**: React + Tailwind ベースの参考実装
- **スクリーンショット**: デザインの視覚的な画像
- **ヒント**: Code Connectマッピング、デザイントークン、アノテーション

### Step 2: Unity向けに変換

Figma MCPが返すコードはReact + Tailwind形式です。**これはそのまま使うものではなく、参考実装です**。Claude Codeがこの情報をもとに、Unity UI Toolkit（UXML + USS）のコードを生成します。

```xml
<!-- 生成されるUXML例 -->
<ui:UXML xmlns:ui="UnityEngine.UIElements">
  <ui:VisualElement class="header-container">
    <ui:Label text="ステータス" class="header-title" />
    <ui:VisualElement class="status-bar">
      <ui:VisualElement class="hp-bar" />
      <ui:Label text="100/100" class="hp-text" />
    </ui:VisualElement>
  </ui:VisualElement>
</ui:UXML>
```

```css
/* 生成されるUSS例 */
.header-container {
    flex-direction: row;
    justify-content: space-between;
    padding: 16px;
    background-color: #1a1a2e;
}

.header-title {
    font-size: 24px;
    color: #e0e0e0;
    -unity-font-style: bold;
}

.hp-bar {
    width: 200px;
    height: 20px;
    background-color: #e74c3c;
    border-radius: 4px;
}
```

### Step 3: /simplify で品質チェック

生成されたコードに対して `/simplify` を実行します。

```
> /simplify
```

`/simplify` が検出・修正する典型的な問題：

| 問題 | 修正内容 |
|:---|:---|
| ハードコードされたカラー値 | USS変数（カスタムプロパティ）に抽出 |
| 冗長なクラス名 | 命名規則の統一 |
| 重複するスタイル定義 | 共通スタイルシートへの統合 |

### Step 4: Unityで配置・確認

生成されたUXMLをUnityシーンに配置し、Figmaのデザインと見比べます。

1. シーンにUIDocumentコンポーネントを持つGameObjectを作成
2. 生成された `.uxml` をソースアセットに設定
3. Game View でレイアウトを確認
4. ズレがあればClaude Codeに修正を指示

`get_screenshot` でFigma側のスクリーンショットを取得しておけば、Unity側と並べて目視比較できます。

## 実践ワークフロー: 複数画面の横展開（/batch）

![/batch 並列実行アーキテクチャ](images/figma_mcp_batch.png)

1画面のUIが完成したら、残りの画面を `/batch` で一括実装します。

### Step 1: 全画面のデザイン情報を取得

```
このFigmaファイルの get_metadata で全ページ・フレーム一覧を取得して。
https://www.figma.com/design/XXXXXXX/MyGame
```

### Step 2: デザイントークンの取得

```
get_variable_defs でこのFigmaのデザイントークンを取得して、
Assets/UI/Styles/tokens.uss として保存して。
```

### Step 3: /batch で全画面を並列実装

```
> /batch Assets/UI/ 以下に、Figmaの各画面フレームをUI Toolkit（UXML + USS）として
  実装して。共通トークンは tokens.uss を参照すること。
```

各ワーカーは独立した git worktree で動作するため、ファイル衝突は発生しません。

## よくある問題と対処法

| 問題 | 症状 | 対処法 |
|:---|:---|:---|
| Figma MCPが呼ばれない | URLを貼っても推測で実装される | `CLAUDE.md` にルールを追加 |
| 色やサイズがずれる | USSの値がFigmaと微妙に異なる | `get_variable_defs` でトークン取得 |
| React形式で出力される | UXML/USSではなくReact+Tailwindが返る | プロンプトで出力形式を明示 |
| アニメーションが再現できない | プロトタイプのトランジションが反映されない | Animator/DOTweenで別途実装 |

## まとめ

| 工程 | 従来 | Figma MCP + /simplify + /batch |
|:---|:---|:---|
| デザイン情報の転写 | 手動（30分/画面） | MCP自動取得（数秒） |
| UIコード作成 | 手書き（30分/画面） | AI自動生成（2〜3分） |
| 品質チェック | 手動レビュー（15分） | /simplify（2〜3分） |
| 10画面への横展開 | 10時間 | /batch で並列（30分） |

Figma MCPの本質は「デザイナーの成果物をAIが直接読める」ことにあります。

これまでは、FigmaとUnityの間に「人間の目と手」というボトルネックがありました。Figma MCPはこのボトルネックを取り除き、`/simplify` が品質を担保し、`/batch` がスケールさせる。

**デザインからUIコード生成までをAIが一気通貫で処理する**——これが、Figma MCP を中心としたUnity UI開発の新しいワークフローです。

## 参考リンク

- [Figma MCP Server ガイド（Figma公式）](https://help.figma.com/hc/en-us/articles/32132100833559-Guide-to-the-Figma-MCP-server)
- [Claude Code /simplify と /batch 解説](https://qiita.com/dsgarage/items/claude-code-simplify-batch)
- [Figma to UI Toolkit Converter（Unity Asset Store）](https://assetstore.unity.com/packages/tools/utilities/figma-to-ui-toolkit-converter-272042)
