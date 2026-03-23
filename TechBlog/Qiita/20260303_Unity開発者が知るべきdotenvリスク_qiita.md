# Unity開発者が知るべき.envリスク — AIエージェント時代のAPIキー管理

## はじめに

「APIキーを `.env` に書いておけばOK」——Web開発の世界では常識的な運用です。しかし、Unity開発者にとって `.env` は馴染みのないファイルです。

Claude CodeやMCPを導入すると、Anthropic APIキー、X APIキー、各種MCPサーバーの認証情報など、**複数のAPIキーをローカル環境に置く必要**が出てきます。Webの慣習に従って `.env` に平文で書き始める。これが、AIエージェント時代の新しいセキュリティリスクです。

本記事では、Unity開発者が `.env` 平文保管のリスクを理解し、**macOS Keychain を使った安全なAPIキー管理**（LLM Key Ring）に移行するまでの具体的な手順を解説します。

## 対象読者

- Unity開発者で、Claude Code / MCP を使い始めた方
- `.env` ファイルにAPIキーを保管している方
- AIエージェント時代のセキュリティリスクに関心がある方

## .envファイルとは何か

`.env` は、環境変数をキー=値の形式で記述するテキストファイルです。

```bash
# .env の例
ANTHROPIC_API_KEY=sk-ant-xxxxxxxxxxxxx
X_API_KEY=xxxxxxxxxxxxxxxx
X_API_SECRET=xxxxxxxxxxxxxxxx
MCP_SERVER_TOKEN=xxxxxxxxxxxxxxxx
```

Node.js の `dotenv` パッケージや、シェルの `source .env` で読み込んで使います。Webのバックエンド開発では極めて一般的な仕組みです。

### Unity開発者にとっての対比

| 用途 | Unity の仕組み | Web の仕組み |
|:---|:---|:---|
| ゲーム設定値 | `PlayerPrefs`（レジストリ/plist） | `.env` / 環境変数 |
| 開発用パラメータ | `ScriptableObject`（アセット） | `.env` / 設定ファイル |
| 秘密情報 | そもそも扱わない | `.env`（平文） |

**ポイントは3行目です**。Unity開発では、APIキーのような秘密情報をローカルに保管する場面がほぼありませんでした。

## なぜ今Unity開発者に関係するのか

Claude Code + MCP の導入により、Unity開発者のローカル環境にAPIキーが出現するようになりました。

```bash
# Claude Code 本体
ANTHROPIC_API_KEY=sk-ant-xxxxxxxxxxxxx

# X API（ブログ投稿の自動化等）
X_API_KEY=xxxxxxxxxxxxxxxx
X_API_SECRET=xxxxxxxxxxxxxxxx
X_ACCESS_TOKEN=xxxxxxxxxxxxxxxx
X_ACCESS_TOKEN_SECRET=xxxxxxxxxxxxxxxx
```

これらを「とりあえず `.env` に書いておこう」と始める。Web開発者なら `.gitignore` に追加するのが習慣ですが、Unity開発者にはその習慣がありません。

### 脅威モデル

![.env 平文保管の脅威モデル](images/env_threat_model.png)

## .env平文保管の3つの脅威

### 脅威1: うっかり git commit

**最も多い事故**です。`.gitignore` に `.env` を追加していても、以下のケースで漏洩します。

```bash
# ケース1: .gitignore の追加を忘れた
git add -A && git commit -m "初期コミット"
# → .env がそのままプッシュされる

# ケース2: AIエージェントが追加した
# Claude Code が git add -A を実行
```

GitHubには過去のコミットも残ります。一度プッシュしたAPIキーは、コミットを削除しても復元可能です。

**実例**: GitGuardianの報告によると、2024年にGitHub上で**1,280万件**の秘密情報が新たに検出されています。

### 脅威2: シェル履歴への混入

```bash
# 履歴に残る
export ANTHROPIC_API_KEY=sk-ant-xxxxxxxxxxxxx

# ~/.zsh_history に永続保存される
```

### 脅威3: AIエージェントによる抽出

これが**AIエージェント時代の新しいリスク**です。

悪意あるプロンプトインジェクションにより、エージェントが `cat .env` や `env | grep API` を実行する可能性があります。

2026年2月には、**hackerbot-claw** と呼ばれるAI自動攻撃ボットが、Microsoft、DataDog等のOSSリポジトリのGitHub Actions設定ミスを自動スキャンし、CI/CDトークンを窃取する事件が発生しました。

## 解決策 — LLM Key Ring（lkr）

[LLM Key Ring](https://github.com/yottayoshida/llm-key-ring)（`lkr`）は、yotta氏が開発したRust製CLIツールです。APIキーを**macOS Keychainに暗号化保存**し、`.env` に平文で置く必要をゼロにします。

### インストールと初期設定

```bash
# Homebrew でインストール
brew install yottayoshida/tap/lkr

# APIキーを登録（対話的にプロンプトで入力）
lkr set ANTHROPIC_API_KEY --kind runtime
# → macOS Keychain に暗号化保存される
```

### lkr exec ワークフロー

`lkr exec` は、Keychainからキーを復号し、**子プロセスの環境変数としてのみ**注入します。

![lkr exec ワークフロー](images/lkr_exec_flow.png)

```bash
# Claude Code を安全に起動
lkr exec -- claude

# 任意のコマンドを安全に実行
lkr exec -- python my_script.py
```

### 3層TTYガード

![TTYガード 3層防御](images/tty_guard_layers.png)

| レイヤー | 防御内容 | 攻撃例 |
|:---|:---|:---|
| Layer 1: stdout ブロック | 非対話環境で `--plain`/`--show` を拒否 | `lkr get KEY --plain` |
| Layer 2: clipboard ブロック | 非TTYでコピーをスキップ | `pbpaste` での取得 |
| Layer 3: exec デフォルト | 「出力しない」が最強の防御 | そもそも値が表示されない |

## Unity開発での実践

```bash
# ① APIキーを登録（初回のみ）
lkr set ANTHROPIC_API_KEY --kind runtime

# ② Claude Code を安全に起動
lkr exec -- claude

# これだけ。.env は不要。
```

### MCP設定での活用

```bash
# X API キーも登録
lkr set X_API_KEY --kind runtime
lkr set X_API_SECRET --kind runtime
lkr set X_ACCESS_TOKEN --kind runtime
lkr set X_ACCESS_TOKEN_SECRET --kind runtime

# まとめて起動
lkr exec -- claude
# → 全APIキーが環境変数として注入
```

## まとめ

| 項目 | 従来（.env） | lkr（Keychain） |
|:---|:---|:---|
| 保管場所 | プレーンテキストファイル | macOS Keychain（暗号化） |
| git 漏洩リスク | `.gitignore` 依存（人的ミス） | ファイルが存在しない |
| シェル履歴 | `export` で残る | `lkr exec` で残らない |
| AIエージェント耐性 | `cat .env` で読める | TTYガードで拒否 |
| セットアップ | 簡単 | `brew install` + `lkr set` |

### 最低限やるべき3つのこと

1. **`.env` をやめる** — `lkr set` でKeychainに移行し、`.env` ファイルを削除する
2. **`lkr exec -- claude` で起動する** — これだけで全APIキーが安全に注入される
3. **`.gitignore` を確認する** — 過去に `.env` をコミットしていないか確認する

## 参考リンク

- [もう.envにAPIキーを平文で置くのはやめた — LLM Key Ring（yotta氏）](https://zenn.dev/yottayoshida/articles/llm-key-ring-secure-api-key-management)
- [LLM Key Ring GitHub](https://github.com/yottayoshida/llm-key-ring)
- [hackerbot-claw: GitHub Actions Exploitation（StepSecurity）](https://www.stepsecurity.io/blog/hackerbot-claw-github-actions-exploitation)
- [Claude Code ステータスページ](https://status.anthropic.com)
