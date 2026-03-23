---
title: "ClipSaveで大量ログをAIに渡す技術"
subtitle: "Claude Code・Codexのコンテキストを守るクリップボード保存ツール"
author: "dsgarage"
date: "2026-03-02"
---

# ClipSaveで大量ログをAIに渡す技術

## はじめに

「このUnityのコンソールログ、全部見てほしい」

Claude CodeやCodexを使って開発していると、1万行を超えるログデータをAIに渡したくなる場面があります。しかし、そのままプロンプトに貼り付けると何が起こるでしょうか。

コンテキストウィンドウの大半がログで埋まり、AIの応答品質が落ちる。最悪の場合、コンテキストが圧縮されて過去のやり取りが失われます。

本記事では、この問題を解決するために作った **ClipSave** というツールと、その設計思想を紹介します。

---

## 問題：プロンプトに貼り付けるログが重すぎる

AIコーディングツールのコンテキストウィンドウには上限があります。Claude Codeの場合、コンテキストが上限に近づくと古いメッセージが自動圧縮されます。

ここに1万行のUnityログを貼り付けたらどうなるか。

```
ユーザーのコンテキスト使用量:

  通常のプロンプト:    約100トークン
  1万行のログを貼付:  約30,000トークン ← コンテキストの大部分を占有
```

さらに深刻なのは、AIの auto memory（`.claude/` 配下の記憶ファイル）にまでログの断片が残ってしまうケースです。セッションをまたいでゴミデータが参照され続ける可能性があります。

### 具体的な弊害

| 問題 | 影響 |
|:---|:---|
| コンテキスト圧迫 | 過去の指示や会話が圧縮・消失 |
| 応答品質の低下 | ログに引きずられて的外れな回答 |
| メモリ汚染 | auto memory にログ断片が残留 |
| コスト増大 | トークン消費が不必要に増加 |

---

## 解決策：ファイルに保存してパスだけ渡す

Claude Codeは `Read` ツールでローカルファイルを直接読めます。Codexも同様です。つまり、ログの実体はファイルに置いて、プロンプトにはパスだけ渡せばいい。

```
# Before: ログを直接貼り付け（30,000トークン消費）
このエラーを分析してください
[ここに1万行のログ]

# After: パスを渡すだけ（30トークン程度）
このファイルを読んでエラーの原因を分析してください
/Users/you/Downloads/ClipBoard/20260302143052_ClipBoard.text
```

AIツール側がファイルを読む際には、必要な部分だけを効率的に処理できるため、コンテキストへの負荷は最小限です。

---

## ClipSave：ワンキーでクリップボードをファイル化

この「コピー → ファイル保存 → パスをクリップボードに」という手順を自動化するのが **ClipSave** です。

### 動作フロー

```
1. ログをコピー         (Cmd+C)
2. ClipSave を実行      (Cmd+Shift+C)
3. ファイルに保存        ~/Downloads/ClipBoard/20260302143052_ClipBoard.text
4. パスがクリップボードに 自動コピー
5. AIツールにパスを貼付   (Cmd+V)
```

タイムスタンプ付きのファイル名（`YYYYMMDDHHmmss_ClipBoard.text`）で保存されるため、複数のログを時系列で管理できます。

### 技術的な仕組み

ClipSaveの中身は30行程度のシェルスクリプトです。

```bash
# AppleScript経由でクリップボード内容を取得（サンドボックス対応）
CLIP_CONTENT=$(osascript -e 'the clipboard as text')

# タイムスタンプ付きファイルに保存
TIMESTAMP=$(date +%Y%m%d%H%M%S)
FILEPATH="$SAVE_DIR/${TIMESTAMP}_ClipBoard.text"
printf '%s' "$CLIP_CONTENT" > "$FILEPATH"

# ファイルパスをクリップボードにコピー
osascript -e "set the clipboard to \"${FILEPATH}\""
```

ポイントは `pbpaste` / `pbcopy` ではなく `osascript`（AppleScript）を使っている点です。macOS のショートカット.app から呼び出す場合、サンドボックス環境では `pbpaste` がクリップボードにアクセスできません。AppleScript経由であればこの制約を回避できます。

---

## インストール方法

### 1. ダウンロード & インストール

```bash
curl -L https://github.com/dsgarage/ClipSave/releases/download/v1.0.0/ClipSave-v1.0.0-macos.tar.gz | tar xz
cd ClipSave
./install.sh
```

### 2. キーボードショートカットの設定

1. macOS の **ショートカット.app** を開く
2. 新規ショートカットを作成、名前を「ClipSave」に
3. 「シェルスクリプトを実行」アクションを追加
4. 内容を `~/Scripts/save_clipboard.sh` に設定
5. シェルを `/bin/zsh`、入力を「入力なし」に
6. 詳細 (i) から **キーボードショートカット** > `Cmd+Shift+C` を設定

### 3. テスト

テキストをコピーして `Cmd+Shift+C` を押し、`Cmd+V` で貼り付けてファイルパスが出れば成功です。

---

## 実践：Claude Codeでの活用パターン

### パターン1: エラーログの分析

```
# Unityのコンソールログ（5,000行）をコピー → Cmd+Shift+C

# Claude Codeに：
以下のUnityログからNullReferenceExceptionの原因を特定してください
/Users/me/Downloads/ClipBoard/20260302150000_ClipBoard.text
```

### パターン2: ビルドログの比較

```
# 成功時のビルドログを保存 → 20260302100000_ClipBoard.text
# 失敗時のビルドログを保存 → 20260302110000_ClipBoard.text

# Claude Codeに：
この2つのビルドログを比較して、失敗の原因を特定してください
/Users/me/Downloads/ClipBoard/20260302100000_ClipBoard.text
/Users/me/Downloads/ClipBoard/20260302110000_ClipBoard.text
```

### パターン3: API レスポンスの解析

```
# 巨大なJSONレスポンスをコピー → Cmd+Shift+C

# Claude Codeに：
このAPIレスポンスのスキーマを分析して型定義を生成してください
/Users/me/Downloads/ClipBoard/20260302120000_ClipBoard.text
```

---

## 設計判断：なぜこの形になったか

### なぜ専用アプリではなくシェルスクリプトなのか

シェルスクリプトにした理由は3つあります。

1. **依存ゼロ**: macOS標準のzshとAppleScriptだけで動く
2. **透明性**: 30行のスクリプトなので中身がすぐ確認できる
3. **カスタマイズ性**: 保存先やファイル名形式を簡単に変更できる

### なぜクリップボード経由なのか

「ファイルに直接リダイレクトすればいいのでは？」という疑問があるかもしれません。しかし、ログの発生元は様々です。Unityコンソール、ブラウザのDevTools、ターミナル出力——これらに共通するのは「コピーできる」ことです。クリップボード経由にすることで、あらゆるソースに対応できます。

---

## まとめ

| 項目 | 詳細 |
|:---|:---|
| 何をするツールか | クリップボードのテキストをタイムスタンプ付きファイルに保存 |
| 何が解決されるか | AIコーディングツールのコンテキスト消費を削減 |
| 対応環境 | macOS 12 以降 |
| ライセンス | MIT |

大量のログをAIに渡したいとき、プロンプトに直接貼り付けるのはコンテキストの無駄遣いです。ファイルに保存してパスを渡す——このシンプルなワークフローが、AIコーディングの効率を大きく改善します。

---

## リンク

- GitHub: [dsgarage/ClipSave](https://github.com/dsgarage/ClipSave)
- リリース: [v1.0.0](https://github.com/dsgarage/ClipSave/releases/tag/v1.0.0)
