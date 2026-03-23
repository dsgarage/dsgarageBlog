---
title: "AIの暴走をcommands/で解決する"
emoji: "🛡️"
type: "tech"
topics: ["AI", "ClaudeCode", "Unity", "MCP", "プロンプトエンジニアリング"]
published: false
---

# AIの暴走をcommands/で解決する

「MCPでは難しいので、EditorWindowを作成しましょう」

AIがこう提案してきたとき、あなたはどうしますか？

一見、親切で実用的な提案に見えます。しかし、これは**AIの暴走**の始まりかもしれません。MCPで実現可能な機能を、AIが勝手にEditor拡張で代替しようとする——この「回避パターン」を放置すると、MCP-Firstアーキテクチャが崩壊します。

本記事では、Claude Codeの `commands/` を使って、AIの思考パターンを制御し、暴走を防ぐ方法を解説します。

:::message
**Unity開発者の方へ**

本シリーズで紹介するAI×Unity開発を実践するためのツール**UniMCP4CC**を公開しています。
Claude CodeからUnity Editorを直接操作できるようになります。

- GitHub: [dsgarage/UniMCP4CC](https://github.com/dsgarage/UniMCP4CC)
- 対応Unity: 2021.3 LTS以降
- ライセンス: MIT
:::

---

## 前提：MCP-First開発とは

### UniMCP4CCとは

**UniMCP4CC**（Unity MCP for Claude Code）は、Claude CodeからUnity Editorを直接操作するためのMCPサーバーです。

```
┌─────────────┐      MCP      ┌─────────────────┐
│ Claude Code │ ◄──────────► │ Unity Editor    │
│  (AI)       │   Protocol   │ (UniMCP4CC)     │
└─────────────┘              └─────────────────┘
```

従来のUnity開発では、開発者がUnity Editorを手動で操作していました。UniMCP4CCを使うと、AIがMCPプロトコル経由でUnityを直接操作できます。

### MCP-Firstアーキテクチャ

UniMCP4CCでは、**MCP-First**というアーキテクチャを採用しています：

```
MCP-Firstの原則:
├── すべてのUnity操作はMCP API経由で行う
├── 操作の記録・再生が可能
├── 外部ツール連携が容易
└── テスト可能性の向上
```

このアーキテクチャを維持するためには、AIが「MCPで実現できないからEditor拡張で解決しよう」と**勝手に回避策を取らないようにする**必要があります。

### Claude Codeの「思考骨格」

Claude Codeでは、プロジェクトごとに `.claude/` ディレクトリを配置し、AIの振る舞いを制御できます：

```
.claude/
├── rules/      # 制約（〜するな）
├── skills/     # 知識（〜のやり方）
├── commands/   # 行動指示（〜せよ）
├── agents/     # 専門エージェント
└── CLAUDE.md   # プロジェクト概要
```

本記事では、この中の `commands/` を活用してAIの暴走を防ぎます。

---

## AIが「暴走」するとはどういうことか

### 典型的な暴走パターン

AIと開発を進めていると、こんな提案を受けることがあります：

```
❌ AIの暴走パターン:

ユーザー: 「シーン内のオブジェクトを一覧表示したい」

AI: 「EditorWindowを作成して、一覧表示するUIを実装しましょう」

↓ これが暴走
```

なぜこれが問題か？

**MCPには `unity.inspect.scene.list` というAPIが存在します。**

AIは、既存のMCP APIを使えば解決できる問題を、「より便利そう」「より直感的そう」という理由で、独自のEditor拡張で解決しようとしたのです。

### 暴走の結果

```
暴走を許した結果:
├── EditorWindow.cs が生成される
├── MCPを経由しない独自実装が増える
├── 同じ機能が複数箇所に実装される
├── メンテナンス対象が増える
└── MCP-Firstアーキテクチャが崩壊する
```

---

## なぜAIは暴走するのか

### AIの思考パターン

AIは基本的に「ユーザーの要求を最も効率的に解決しよう」とします。

```
AIの思考:
├── 「一覧表示が必要」
├── 「EditorWindowなら視覚的に見やすい」
├── 「すぐに実装できる」
└── 「これが最善だ！」→ 暴走
```

問題は、AIが**アーキテクチャの制約を考慮していない**ことです。

「MCP経由で操作すべき」というプロジェクトのルールを、AIは知りません。いや、CLAUDE.mdに書いてあっても、**具体的な行動指針がなければ無視されがち**です。

### ルールだけでは不十分

多くの開発者は、`.claude/rules/` にルールを書きます：

```markdown
# rules/safety.md
Editor拡張の作成は原則禁止。MCP APIを使用すること。
```

しかし、これだけでは不十分です。なぜなら：

- ルールは「何をすべきでないか」しか伝えない
- 「代わりに何をすべきか」が明確でない
- 具体的なワークフローが示されていない

---

## commands/による解決

### commands/とは（公式ドキュメントより）

Claude Codeの `commands/` は、**カスタムスラッシュコマンド**を定義する機能です。

公式ドキュメント（[Claude Code - Custom Slash Commands](https://docs.anthropic.com/en/docs/claude-code/slash-commands)）から引用：

> **Custom slash commands allow you to define reusable prompts.**
>
> Create Markdown files in `.claude/commands/` folder to add project-specific commands. For example, `.claude/commands/my-command.md` becomes available as `/project:my-command`.

つまり、`.claude/commands/` にMarkdownファイルを配置すると、`/project:コマンド名` でいつでも呼び出せるようになります。

### commands/の位置づけ

```
.claude/
├── rules/      # 制約（〜するな）- 常に自動参照
├── skills/     # 知識（〜のやり方）- 必要時に参照
└── commands/   # 行動指示（〜せよ）- /コマンドで明示呼び出し
```

| | rules/ | skills/ | commands/ |
|:---|:---|:---|:---|
| **性質** | 制約 | 知識 | 行動指示 |
| **内容** | やってはいけないこと | ドメイン知識 | 具体的な手順 |
| **強制力** | 中 | 低 | **高** |
| **呼び出し** | 自動参照 | 必要時参照 | `/command` で明示呼び出し |

### 公式ドキュメントのコマンド例

公式ドキュメントでは、以下のようなフォーマットが紹介されています：

```markdown
---
description: Ask questions about the codebase
allowed-tools: Read, Grep, Glob, Task
---

Answer questions about this codebase.
Use the allowed tools to find relevant code and information.
```

`---` で囲まれた**フロントマター**でメタデータを定義し、その下に具体的な指示を記述します。

### 実装例：/new-editor コマンド

Editor拡張作成時の暴走を防ぐため、`/new-editor` コマンドを実装しました。

```markdown
# .claude/commands/new-editor.md

---
name: new-editor
description: Editor拡張作成時の制限ルール。MCPで実現可能な機能をEditor拡張で代替することを禁止。
---

# /new-editor コマンド

**重要**: MCPで実現可能な機能を、Editor拡張で代替・回避することは **禁止** です。

❌ 禁止: 「MCPでは難しいのでEditorWindowを作りましょう」
❌ 禁止: 「MCP APIの制限を回避するためにEditor拡張で...」
✅ 正解: 「MCPのAPIを使って実現します」
✅ 正解: 「必要なAPIがなければ、MCP APIとして新規実装します」
```

### 禁止される思考パターンを明示

コマンドの中で、**AIの思考パターンそのものを禁止**しています：

```markdown
## 禁止される思考パターン

AIが以下のような提案をすることを禁止します：

❌ 「この機能はMCPでは実現が難しいので、EditorWindowを作成しましょう」
❌ 「MCP APIの制限があるため、PropertyDrawerで対応します」
❌ 「より効率的にするため、BuildProcessorを実装しましょう」
❌ 「MCP経由だと複雑なので、直接Editor拡張で...」
```

これにより、AIは「こう考えてはいけない」と理解します。

### 正しい思考パターンも明示

禁止だけでなく、**正しい行動も明示**します：

```markdown
## 正しい対応

✅ 「既存のMCP APIを組み合わせて実現します」
✅ 「この機能を実現するMCP APIが不足しているため、新規APIを実装します」
✅ 「unity.xxx.* APIを使用して対応します」
```

---

## 効果的なcommands/の書き方

### 1. 思考パターンを明示する

ルールだけでなく、**AIの思考そのものを制御**します：

```markdown
## 禁止される思考パターン

❌ 「〜では難しいので、代わりに〜」
❌ 「〜の制限を回避するため〜」
```

### 2. 具体例を多く示す

抽象的なルールより、**具体的な禁止例**が効果的です：

```markdown
## 禁止される回避パターン

| パターン | 禁止理由 | 正しい対応 |
|:---|:---|:---|
| EditorWindow作成 | MCP APIで代替可能 | MCP APIを使用 |
| PropertyDrawer作成 | inspect/apply APIで対応可能 | MCP APIを使用 |
| BuildProcessor作成 | build APIで対応可能 | MCP APIを使用 |
```

### 3. 正しい行動も示す

「やるな」だけでなく「代わりにこうしろ」を明示します：

```markdown
## 正しいワークフロー

1. まず既存のMCP APIを確認する
2. 組み合わせで実現できないか検討する
3. 不足している場合はMCP APIとして新規実装
4. ユーザーの明示的な許可なしにEditor拡張は作成しない
```

---

## 実際の適用例

### Before: 暴走パターン

```
ユーザー: 「複数オブジェクトの位置を一括変更したい」

AI（暴走）: 「EditorWindowを作成して、複数選択と一括変更のUIを
            実装しましょう。以下のコードを生成します...」
```

### After: 制御されたパターン

```
ユーザー: 「複数オブジェクトの位置を一括変更したい」

AI（制御済）: 「unity.transform.setBatch APIを使用します。
              以下のパラメータで一括変更できます：

              {
                "method": "unity.transform.setBatch",
                "params": {
                  "items": [
                    {"path": "Cube_1", "position": {"x":0,"y":1,"z":0}},
                    {"path": "Cube_2", "position": {"x":2,"y":1,"z":0}}
                  ]
                }
              }」
```

---

## commands/の構造

今回、**UniMCP4CC**リポジトリの `.claude/commands/` ディレクトリに、以下の3つのコマンドを実装しました：

```
# UniMCP4CCリポジトリ構造
dsgarage/UniMCP4CC/
├── Editor/              # MCP API実装
├── Samples~/            # サンプル・テスト
├── CLAUDE.md            # プロジェクト概要
└── .claude/
    ├── rules/           # 制約ルール
    ├── skills/          # スキル定義
    ├── agents/          # エージェント定義
    └── commands/        # ← ここに実装
        ├── test-runner.md   # TestRunner APIの使用手順
        ├── build.md         # Build Pipeline APIの使用手順
        └── new-editor.md    # Editor拡張作成の制限
```

これらのコマンドは、UniMCP4CCを使ってUnity開発を行う際に、Claude Codeから `/project:test-runner`, `/project:build`, `/project:new-editor` として呼び出せます。

### /test-runner

```markdown
TestRunner APIを使用したテスト実行コマンド。

利用可能なAPI:
- unity.test.runAll
- unity.test.runEditMode
- unity.test.getResults
...
```

### /build

```markdown
Build Pipeline APIを使用したビルドコマンド。

利用可能なAPI:
- unity.build.player
- unity.build.getSettings
- unity.build.setSettings
```

### /new-editor

```markdown
Editor拡張作成時の制限ルール。

禁止: MCPで実現可能な機能をEditor拡張で代替すること
正解: MCP APIを使用する、または新規APIを実装する
```

---

## まとめ

### AIの暴走とは

| 暴走パターン | 問題 |
|:---|:---|
| 「MCPでは難しいからEditor拡張で」 | MCP-Firstアーキテクチャの崩壊 |
| 「効率的だからEditorWindowで」 | 独自実装の増加 |
| 「すぐにできるからPropertyDrawerで」 | メンテナンス対象の増加 |

### commands/による解決

| 要素 | 効果 |
|:---|:---|
| 禁止される思考パターンの明示 | AIの思考そのものを制御 |
| 具体的な禁止例 | 曖昧さの排除 |
| 正しい行動の明示 | 代替手段の提示 |
| 例外条件の明示 | 柔軟性の確保 |

### 結論

```
rules/  = 制約を伝える（〜するな）
skills/ = 知識を伝える（〜のやり方）
commands/ = 行動を指示する（〜せよ）

AIの暴走を防ぐには、commands/ で具体的な行動指示を与えることが効果的。
```

---

## 次のステップ

今回実装した `commands/` は、[UniMCP4CC](https://github.com/dsgarage/UniMCP4CC)リポジトリの `.claude/commands/` で公開しています。

### UniMCP4CCを使う場合

1. UniMCP4CCをUnityプロジェクトにインストール
2. Claude Codeで `/project:new-editor` 等を呼び出す
3. AIがMCP-Firstアーキテクチャに従って動作する

### 自分のプロジェクトに適用する場合

1. プロジェクトルートに `.claude/commands/` ディレクトリを作成
2. タスク別のコマンドファイル（`.md`）を追加
3. AIの思考パターンを制御する記述を追加
4. `/project:コマンド名` で呼び出し

あなたのプロジェクトでも、AIの暴走を防ぐためのcommands/を実装してみてください。

---

## commands/ファイルの書き方

### ファイル構造

```
.claude/
└── commands/
    ├── test-runner.md
    ├── build.md
    └── new-editor.md
```

### 公式ドキュメントによるフォーマット

公式ドキュメントでは、以下のフロントマターオプションがサポートされています：

```yaml
# フロントマター（ファイル先頭に記述）
description: コマンドの説明（/help で表示される）
allowed-tools: Read, Grep, Glob, Task, Bash, Write, Edit
```

| オプション | 説明 |
|:---|:---|
| `description` | コマンドの説明文。`/help` で表示される |
| `allowed-tools` | 使用を許可するツールを制限（オプション） |

また、コマンド内で `$ARGUMENTS` を使うと、呼び出し時の引数を展開できます：

```markdown
Fix issue #$ARGUMENTS
```

これを `/project:fix-issue 123` と呼び出すと、`Fix issue #123` に展開されます。

### 本記事で使用したフォーマット

UniMCP4CCでは、より詳細なメタデータを使用しています。ファイル構成は以下の通りです：

**1. フロントマター（ファイル先頭）**

```yaml
name: コマンド名
description: 簡潔な説明（1行）
```

**2. 本文構成**

```text
# /コマンド名 コマンド

## 概要
このコマンドが何をするかの説明。

## 利用可能なAPI
| API | 説明 |
|-----|------|
| unity.xxx.yyy | 説明 |

## 標準ワークフロー
1. ステップ1
2. ステップ2
3. ステップ3

## 禁止事項
（禁止）やってはいけないこと

## 正しい対応
（正解）やるべきこと
```

### フロントマターの詳細

```yaml
name: test-runner          # コマンド名（ファイル名と合わせる）
description: 説明文        # 1行で簡潔に
```

### 禁止パターンの書き方

AIの思考パターンそのものを禁止する場合、**会話形式**で書くと効果的です：

```markdown
## 禁止される思考パターン

AIが以下のような提案をすることを禁止します：

❌ 「この機能はMCPでは実現が難しいので、EditorWindowを作成しましょう」
❌ 「MCP APIの制限があるため、PropertyDrawerで対応します」
```

### 正しいパターンの書き方

禁止と対になる形で、**正しい行動も明示**します：

```markdown
## 正しい対応

✅ 「既存のMCP APIを組み合わせて実現します」
✅ 「この機能を実現するMCP APIが不足しているため、新規APIを実装します」
```

### 表による対比

禁止と正解を対比させると、AIが理解しやすくなります：

```markdown
| パターン | 禁止理由 | 正しい対応 |
|---------|---------|-----------|
| EditorWindow作成 | MCP APIで代替可能 | MCP APIを使用 |
| PropertyDrawer作成 | inspect/apply APIで対応 | MCP APIを使用 |
```

### ワークフローの書き方

番号付きリストで具体的な手順を示します：

```markdown
## 標準ワークフロー

1. `unity.inspect.xxx` で現状を確認
2. `unity.apply.xxx` で変更を適用
3. `unity.inspect.xxx` で結果を確認
```

### JSON例の書き方

API呼び出しの具体例を示す場合、JSONコードブロックを使用します：

```json
{
  "method": "unity.transform.setBatch",
  "params": {
    "items": [
      {"path": "Cube_1", "position": {"x":0,"y":1,"z":0}},
      {"path": "Cube_2", "position": {"x":2,"y":1,"z":0}}
    ]
  }
}
```

### 例外の書き方

完全禁止ではなく例外がある場合、明示的に記載します：

```markdown
## 例外（ユーザーの明示的許可が必要）

以下の場合のみ、ユーザーに確認の上で実装可能：

1. **Unity MCP Server のコアインフラ改善**
2. **MCP APIとして新規実装する場合**
3. **ユーザーが明示的にEditor拡張を要求した場合**

**例外を適用する場合は、必ずユーザーに確認すること。**
```

---

## commands/ vs rules/ vs skills/

| | commands/ | rules/ | skills/ |
|:---|:---|:---|:---|
| **目的** | 行動を指示する | 制約を伝える | 知識を伝える |
| **内容** | 「〜せよ」 | 「〜するな」 | 「〜のやり方」 |
| **強制力** | 高 | 中 | 低 |
| **呼び出し** | `/command` で明示 | 自動参照 | 必要時参照 |
| **適用場面** | 特定タスク実行時 | 常時 | 学習・参考 |

### 使い分けの例

```
「Editor拡張を作るな」→ rules/
「Editor拡張の知識」→ skills/
「Editor拡張作成時の具体的手順と禁止パターン」→ commands/
```

---

## 参考リンク

- [UniMCP4CC GitHub](https://github.com/dsgarage/UniMCP4CC) - 本記事で紹介したcommands/が実装されています
- [Claude Code Documentation](https://docs.anthropic.com/en/docs/claude-code) - Claude Code公式ドキュメント
- [Claude Code - Custom Slash Commands](https://docs.anthropic.com/en/docs/claude-code/slash-commands) - commands/の公式リファレンス
- [Model Context Protocol](https://modelcontextprotocol.io/) - MCPの公式仕様

---
