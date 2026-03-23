# TechBlog 執筆ガイドライン

このドキュメントは、CC2UniMCP関連のブログ記事を執筆する際のワークフローとガイドラインを定義します。

## 概要

本ブログシリーズの目的：
1. Claude Code単体でのUnity開発の限界を明確にする
2. MCPがその限界をどう解決するかを示す
3. CC2UniMCPの導入と活用方法を解説する

---

## Phase 1: 記事構成の設計

### 1.1 リード文の3要素

すべての記事は以下の3要素でリード文を構成する：

```markdown
[問題提起]
読者が直面している/直面するであろう問題を提示。
共感を得られる具体的な状況を描写する。

[具体例]
実際のプロジェクトや有名ゲームでの例。
数値を含めると説得力が増す。

[この記事の価値]
この記事を読むことで何が得られるか。
読者のメリットを明確にする。
```

#### 良いリード文の例

```markdown
「AIでゲーム開発が自動化できる」——そう期待してClaude Codeを使い始めました。
しかし、Unity開発の現実は甘くありませんでした。

スクリプトは30秒で生成されるのに、Sceneへの配置で10分かかる。
この非対称性こそが、AI時代のUnity開発の本質的な課題です。

本記事では、Claude Code単体でUnity開発をした場合の限界を明確にします。
「どこまでできるか」を知ることで、適切なツール選択ができるようになります。
```

#### 悪いリード文の例

```markdown
Claude Codeは非常に優秀なAIエージェントです。
コードを書く、ファイルを編集する、gitを操作する——多くのことができます。
```

→ 問題提起がなく、読者の関心を引けません

### 1.2 本文の構成順序

以下の順序を守ることで、文脈の混在を防ぐ：

```
1. 問題の提示（できること vs できないこと）
2. なぜそうなるのか（技術的背景）
3. 具体的な影響（作業フロー比較）
4. 解決策の提示（または次回への導線）
5. まとめ
```

---

## Phase 2: コンテンツの品質基準

### 2.1 良いコンテンツの条件

| 条件 | 説明 | 確認方法 |
|:---|:---|:---|
| **視覚的** | 図やコードで結果が見える | ASCII図、コードブロックがあるか |
| **教育的** | 思考プロセスが分かる | 「なぜ」が説明されているか |
| **実用的** | 実際の開発で使える | 具体的な手順があるか |
| **最小限** | 冗長な説明がない | 各段落が必要不可欠か |
| **段階的** | 基礎から応用へ | 読者を置いていっていないか |

### 2.2 専門用語の取り扱い

初出の専門用語は必ず説明を付ける：

```markdown
### 良い例
MCP（Model Context Protocol）は、AIとツールを接続するためのプロトコルです。

### 悪い例
MCPを使えばUnity Editorを操作できます。
```

### 2.3 コードブロックの書き方

```csharp
// 良い例：何をするコードか明記
// Claude Codeが生成したプレイヤー移動スクリプト
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    // ...
}
```

```csharp
// 悪い例：説明なしのコード
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
}
```

---

## Phase 3: 図解のガイドライン

### 3.1 Graphvizで図を作成（第一優先）

**図式はまずGraphvizで作成する**。ASCII図よりも視認性が高く、PDF出力でも崩れない。

#### ワークフロー

```bash
# 1. .dotファイルを作成
vim images/diagram_name.dot

# 2. PNGに変換
./build-diagram.sh images/diagram_name.dot

# 3. 記事に埋め込む
![説明](images/diagram_name.png)
```

#### スタイルガイド

- `images/_style.dot` にカラーパレットとテンプレートを定義済み
- 新規作成時はこのスタイルに準拠すること
- カラー: Primary（青系）、Neutral（グレー系）、Accent（アンバー系）

#### 例：スペクトラム図

```dot
digraph example {
    rankdir=LR
    node [style="filled,rounded" shape=box]

    a [label="項目A" fillcolor="#dbeafe" fontcolor="#1e40af"]
    b [label="項目B" fillcolor="#93c5fd" fontcolor="#1e3a8a"]

    a -> b
}
```

### 3.2 外部画像の保存

画像パスや画像URLが渡された場合は、原則として記事本文用の `images/` フォルダにコピーして保存する。

ただし、イベント現地で撮影したスライド、録音、配布 PDF、Talksribe の生文字起こしのような元資料は `stock/<topic>/<session>/source/` 側で管理する。記事本文で使うために清書・切り出し・再編集したものだけを `images/` に置く。

#### ファイルパスが渡された場合

```bash
cp /path/to/image.png images/descriptive_name.png
```

#### URLが渡された場合

```bash
curl -o images/descriptive_name.png "https://example.com/image.png"
```

#### 注意：クリップボード画像について

会話に直接貼り付けられた画像（クリップボードから）は、ファイルシステム上に存在しないため直接コピーできない。この場合はユーザーに以下を依頼：

1. スクリーンショットをファイルとして保存してもらう
2. 保存先パスを教えてもらう
3. `images/` フォルダにコピーする

### 3.3 ASCII図（Graphvizが不適切な場合のみ）

簡易的なフロー図や、コードブロック内での説明にはASCII図を使用：

```
Step 1: スクリプト生成
├── PlayerController.cs を生成
└── 所要時間: 30秒
```

### 3.3 比較表の活用

対比を示す際は必ず表を使用する：

```markdown
| できること | できないこと |
|:---|:---|
| C#スクリプト生成 | Sceneへのオブジェクト配置 |
| エディタスクリプト生成 | コンポーネントの追加 |
```

### 3.3 フロー図の活用

手順や比較を示す際はインデント付きリストを活用：

```markdown
Step 1: スクリプト生成（AIができる）
├── PlayerController.cs を生成
└── 所要時間: 30秒

Step 2: Unity Editorでの設定（人間がやる）
├── Hierarchyで Create Empty
├── 名前を "Player" に変更
├── PlayerController.cs をアタッチ
└── 所要時間: 3-5分
```

---

## Phase 4: 記事間の導線

### 4.1 シリーズの流れ

各記事は独立して読めるが、シリーズとしての流れも意識する：

```
1. Claude Codeとは何か → 基礎知識
2. AIとUnityだけでどこまでできるか → 問題提起
3. UnityでAIを使う時の壁 → 問題の深堀り
4. MCPがUnityにもたらすもの → 解決策の提示
5. CC2UniMCP導入ガイド → 実践
```

### 4.2 次回への導線

記事末尾に次のステップを示す：

```markdown
---

## 次のステップ

この問題を解決するのが**MCP（Model Context Protocol）**です。
MCPを使えば、AIがUnity Editorを直接操作できるようになります。

→ 次回：「MCPがUnityにもたらすもの」
```

### 4.3 過度なシリーズ化の回避

- 各記事は単独で完結させる
- 「前回の記事を読んでください」は禁止
- 必要な前提知識は記事内で簡潔に説明

---

## Phase 5: 執筆チェックリスト

### 執筆前

- [ ] 記事のゴール（読者が得る価値）を明確化
- [ ] 対象読者の前提知識を確認
- [ ] 記事の構成（見出し構成）を設計

### 執筆中

- [ ] リード文に3要素（問題提起・具体例・価値）を含む
- [ ] 専門用語の初出に説明を付ける
- [ ] 図やコードで視覚化
- [ ] 問題→背景→影響→解決策の順序を維持
- [ ] 各見出しが論理的につながっている

### 公開前

- [ ] リード文が読者の関心を引くか
- [ ] 結論が明確か
- [ ] 次のステップへの導線があるか
- [ ] PDFビルドでレイアウト崩れがないか

---

## よくある問題と対処法

### 問題1: 文脈の混在

**症状**: 「できること」と「できないこと」が交互に出てくる
**対処法**: セクションを明確に分離する

```markdown
## Claude Codeができること
（ここに全て記載）

## Claude Codeができないこと
（ここに全て記載）
```

### 問題2: 抽象的すぎる説明

**症状**: 「便利です」「効率的です」で終わる
**対処法**: 具体的な数値や例を追加

```markdown
❌ 「作業が効率化されます」
✅ 「手作業13分 → MCP経由30秒に短縮」
```

### 問題3: 専門用語の羅列

**症状**: 用語だけが並んでいて理解できない
**対処法**: 各用語に「つまり〜」を追加

```markdown
❌ 「MCPでAPIを叩いてJSONを返す」
✅ 「MCP（AIとツールをつなぐ仕組み）でUnityに命令を送り、
    結果をJSON（データ形式）で受け取る」
```

### 問題4: 結論が弱い

**症状**: 「以上です」で終わる
**対処法**: 読者のアクションを促す

```markdown
❌ 「Claude Codeには限界があります。」
✅ 「Claude Code単体では10%しかカバーできない。
    この90%のギャップを埋めるのがMCPである。」
```

---

## テンプレート

### 技術解説記事テンプレート

```markdown
---
title: "タイトル"
emoji: "絵文字"
type: "tech"
topics: ["Unity", "AI", "ClaudeCode"]
published: false
---

# タイトル

[問題提起]
〜という期待があった。しかし現実は〜。

[具体例]
実際に〜をやってみると、〜という結果になる。

[この記事の価値]
本記事では〜を明確にする。これを理解することで〜できるようになる。

---

## セクション1: 現状の説明

### できること
（箇条書き + コード例）

### できないこと
（箇条書き + 具体例）

---

## セクション2: なぜそうなるのか

（技術的背景をASCII図で説明）

---

## セクション3: 具体的な影響

（作業フロー比較、時間の比較）

---

## まとめ

| できること | できないこと |
|:---|:---|
| 〜 | 〜 |

### 結論

（明確なメッセージ）

---

## 次のステップ

（次回記事への導線、または読者のアクション）

---

## 参考リンク

- [リンク1](URL)
- [リンク2](URL)
```

---

## UniMCP4CCへの誘導

すべての記事で、UniMCP4CCへの誘導を含める：

- **リポジトリ名**: UniMCP4CC
- **GitHub URL**: https://github.com/dsgarage/UniMCP4CC
- **正式名称**: Unity MCP Server for Claude Code

### 記載例（:::messageボックス）

```markdown
:::message
**Unity開発者の方へ**

Claude Codeは強力なツールですが、Unity Editorを直接操作することはできません。
この問題を解決するのが**UniMCP4CC**（Unity MCP Server for Claude Code）です。

- GitHub: [dsgarage/UniMCP4CC](https://github.com/dsgarage/UniMCP4CC)
- 対応Unity: 2021.3 LTS以降
- ライセンス: MIT
:::
```

### 注意

- `CC2UniMCP`、`UniMCP2CC` などの類似名は使用しない
- 必ず `UniMCP4CC` を使用すること

---

## 関連ファイル

- 記事ソース: `/Users/daisuketsukada/Documents/dsgarageBlog/TechBlog/`
- 生成PDF: `/Users/daisuketsukada/Documents/dsgarageBlog/TechBlog/PDF/`
- ビルドスクリプト: `./build-pdf.sh`
- **UniMCP4CC**: https://github.com/dsgarage/UniMCP4CC

---

このガイドラインに従って執筆することで、読者にとって価値のある、一貫性のある記事シリーズを作成できます。
