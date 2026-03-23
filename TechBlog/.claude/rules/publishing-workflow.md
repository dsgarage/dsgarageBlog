# 出力・公開ワークフロールール

## End-to-End 公開フロー

```
Phase 1: 執筆         /blog-write <テーマ>
    → 原本 YYYYMMDD_タイトル.md を作成
    → 挿絵・ダイアグラムを draw.io MCP / Figma MCP で作成
    → X 用の告知動画を生成（./build-video.sh）

Phase 2: ビルド       /blog-publish <記事.md>
    → Qiita/Zenn/note/PDF を一括生成

Phase 3: デプロイ     （/blog-publish の続き）
    → qiita_deploy → git push（Actions で自動公開）
    → zenn_deploy → git push（連携で自動公開）

Phase 4: X 投稿       （/blog-publish の続き）
    → 公開 URL 確定後、PR 動画付きで X に投稿
    → リプライで Qiita/Zenn の記事 URL を添付
```

---

## Phase 1: 執筆

### 図解ツール選択の指針

| 用途 | ツール | MCP ツール名 |
|:---|:---|:---|
| フロー図・構成図・状態遷移図 | **draw.io**（第一選択） | `mcp__drawio__open_drawio_xml` / `_mermaid` |
| FigJam ダイアグラム | **Figma MCP** | `mcp__figma-remote-mcp__generate_diagram` |
| デザインモック・UI 図 | **Figma MCP** | `mcp__figma-remote-mcp__generate_figma_design` |
| シンプルなノード接続図 | **Graphviz** | `./build-diagram.sh images/図名.dot` |

- 記事には**最低1つの図**を含めること
- draw.io は XML（複雑なレイアウト）と Mermaid（素早い作成）の2モード
- Figma MCP で `generate_diagram` を使った場合、返された URL を必ずユーザーに提示する

### PR動画の生成

```bash
./build-video.sh YYYYMMDD_タイトル.md
```
- H2 見出しから要点を自動抽出（最大3つ）
- 除外見出し: 概要, まとめ, 参考リンク, 次のステップ, おわりに, はじめに
- 出力: `out/YYYYMMDD_タイトル.mp4` + `out/YYYYMMDD_タイトル_props.json`

---

## Phase 2: プラットフォーム別変換ルール

### Qiita
- 吹き出し記法: `:::note info` / `:::note warn` / `:::note alert`
- 画像: GitHub の raw URL または imgur にアップロードして参照
- 中間ファイル: `Qiita/YYYYMMDD_タイトル_qiita.md`

### Zenn
- 吹き出し記法: `:::message` / `:::details タイトル`
- 中間ファイル: `Zenn/YYYYMMDD_タイトル_zenn.md`

### note
- `./convert-to-mt.sh 記事.md > note_import/YYYYMMDD_タイトル.txt`
- Movable Type 形式で出力（手動インポート）

### PDF
- `./build-pdf.sh YYYYMMDD_タイトル.md`
- 出力先: `PDF/YYYYMMDD_タイトル.pdf`

---

## Phase 3: GitHub 経由デプロイ

### デプロイ先リポジトリ

| プラットフォーム | リポジトリ | デプロイ方式 | ファイル配置先 |
|:---|:---|:---|:---|
| Qiita | `dsgarage/Qiita` | GitHub Actions（qiita-cli） | `public/<slug>.md` |
| Zenn | `dsgarage/Zenn` | GitHub 連携（自動） | `articles/<slug>.md` |

### スラッグ命名規則

デプロイ用のファイル名は **kebab-case のスラッグ**を使用する（日付を含めない）。

```
原本:  20260306_Anthropic公式skill-creatorでスキルを自作する.md
slug:  anthropic-skill-creator
```

### Qiita デプロイ手順

1. **frontmatter を Qiita 用に書き換え**:
   ```yaml
   ---
   title: 記事タイトル
   tags:
     - ClaudeCode
     - AI
   private: false
   updated_at: ''
   id: null
   organization_url_name: null
   slide: false
   ignorePublish: false
   ---
   ```
2. **ファイル配置**: `qiita_deploy/public/<slug>.md`
3. **コミット & プッシュ**:
   ```bash
   cd qiita_deploy
   git add public/<slug>.md
   git commit -m "記事タイトル を追加"
   git push origin main
   ```
4. GitHub Actions が自動で Qiita に公開

### Zenn デプロイ手順

1. **frontmatter を Zenn 用に設定**（`published: true`）:
   ```yaml
   ---
   title: "記事タイトル"
   emoji: "絵文字"
   type: "tech"
   topics: ["ClaudeCode", "AI", "Unity"]
   published: true
   ---
   ```
2. **ファイル配置**: `zenn_deploy/articles/<slug>.md`
3. **コミット & プッシュ**:
   ```bash
   cd zenn_deploy
   git add articles/<slug>.md
   git commit -m "記事タイトル を追加"
   git push origin main
   ```
4. GitHub 連携で自動的に Zenn に公開
5. **公開 URL**: `https://zenn.dev/dsgarage/articles/<slug>`

---

## Phase 4: X（Twitter）投稿

Qiita/Zenn の公開 URL が確定した後に実行する。

### ツイート構成

```
メインツイート（動画付き）:
  📝 新着ブログ: {タイトル}

  1. {要点1}
  2. {要点2}
  3. {要点3}

  #ClaudeCode #AI #Unity #GameDev

リプライツイート:
  📖 記事はこちら 👇
  {Qiita の記事URL}
```

### ハッシュタグ

記事の内容に応じて適切なハッシュタグを選択する（例: `#ClaudeCode #AI #CLI #自動化`）。
Unity 無関係なら `#Unity` `#GameDev` は付けない。

### 文字数制限
- X の上限: 280文字（日本語は1文字=2カウント → 実質約140文字）
- 上限を超える場合は要点を短縮・ハッシュタグを削減

### 実行手順

```bash
# 1. ドライラン（必須）
./post-video.sh YYYYMMDD_タイトル.md <記事URL> --dry-run

# 2. ユーザーに内容を提示し承認を得る

# 3. 本投稿
./post-video.sh YYYYMMDD_タイトル.md <記事URL>
```

**ユーザーの承認なしに本投稿してはならない。**

---

## Graphviz 図の生成（補助）

```bash
./build-diagram.sh images/図名.dot       # .dot → .png
./build-diagram.sh images/図名.dot svg   # .dot → .svg
```

- カラーパレット: `images/_style.dot` を参照
- DPI: 150（PNG）
