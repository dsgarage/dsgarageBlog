---
name: blog-write
description: 新規テックブログ記事を執筆する。テーマを指定すると、テンプレートに沿った原本Markdownを生成し、draw.ioやFigma MCPで図解・挿絵を作成し、PR動画を生成する。記事の執筆や新規作成について言及された場合に使用する。
allowed-tools:
  - Read
  - Write
  - Edit
  - Bash
  - Glob
  - Grep
  - WebSearch
  - WebFetch
  - mcp__drawio__open_drawio_xml
  - mcp__drawio__open_drawio_mermaid
  - mcp__drawio__open_drawio_csv
  - mcp__figma-remote-mcp__generate_figma_design
  - mcp__figma-remote-mcp__generate_diagram
  - mcp__figma-remote-mcp__get_screenshot
  - mcp__figma-remote-mcp__get_design_context
argument-hint: "<テーマ or タイトル>"
---

# blog-write スキル

テーマ「$ARGUMENTS」のテックブログ記事を執筆する。

## Step 1: 記事本文の作成

1. **既存記事の確認**: 重複テーマがないかルートの `.md` ファイルを確認
2. **テンプレート読み込み**: `_template.md` を読む
3. **ガイドライン確認**: `BLOG_WRITING_GUIDELINE.md` の品質基準に従う
4. **原本作成**: ルートに `YYYYMMDD_タイトル.md` を作成
   - 今日の日付を使用
   - frontmatter（title, subtitle, author: "dsgarage", date）を記入
   - リード文に3要素（問題提起・具体例・価値）を含める
   - 構成順序: 問題→背景→影響→解決策→まとめ
   - 専門用語の初出に説明を付ける
   - 記事末尾に UniMCP4CC 誘導を含める（該当テーマの場合）

## Step 2: 挿絵・ダイアグラムの作成

記事の内容に応じて、以下のツールで図解を作成する。**図がない記事は読みにくい**ため、最低1つは図を含めること。

### ツール選択の指針

| 用途 | ツール | 備考 |
|:---|:---|:---|
| **フロー図・構成図・状態遷移図** | draw.io（XML or Mermaid） | 技術的なダイアグラムの第一選択 |
| **FigJam ダイアグラム** | Figma MCP `generate_diagram` | フローチャート・シーケンス図・ガント |
| **デザインモック・UI図** | Figma MCP `generate_figma_design` | Webページのキャプチャ→Figmaデザイン化 |
| **シンプルなノード接続図** | Graphviz（.dot） | `./build-diagram.sh` で PNG 変換 |

### draw.io での図解作成

技術的なダイアグラムは draw.io を優先する。

```
1. 記事の構造・フローを分析して図の種類を決める
2. draw.io XML or Mermaid 記法で図を作成
   - mcp__drawio__open_drawio_xml: 複雑なレイアウト、カスタムスタイル
   - mcp__drawio__open_drawio_mermaid: フロー・シーケンス図を素早く作成
3. draw.io エディタが開くので、ユーザーが確認・編集できる
4. エクスポートした画像を images/ に保存
```

### Figma MCP での図解作成

FigJam ダイアグラムやデザイン系の図は Figma MCP を使用する。

```
1. mcp__figma-remote-mcp__generate_diagram で Mermaid 記法からダイアグラム生成
   - 返された URL をユーザーに提示する（必須）
2. Web ページのデザインキャプチャが必要な場合は generate_figma_design
3. 既存の Figma デザインを参照する場合は get_design_context / get_screenshot
```

### Graphviz での図解作成（シンプルな図向け）

```bash
# .dot ファイルを images/ に作成
# カラーパレット: images/_style.dot を参照
./build-diagram.sh images/図名.dot
```

### 記事への埋め込み

```markdown
![図の説明](images/図名.png)
```

## Step 3: X 用告知動画の作成

記事が完成したら PR 動画を生成する。

```bash
./build-video.sh YYYYMMDD_タイトル.md
```

- H2 見出しから要点を自動抽出（最大3つ）
- 出力: `out/YYYYMMDD_タイトル.mp4` + `out/YYYYMMDD_タイトル_props.json`
- 除外見出し: 概要, まとめ, 参考リンク, 次のステップ, おわりに, はじめに

## 出力

- `YYYYMMDD_タイトル.md`（ルートに作成）
- `images/` に図解（draw.io / Figma / Graphviz）
- `out/YYYYMMDD_タイトル.mp4`（PR動画）
