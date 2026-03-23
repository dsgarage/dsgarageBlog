# ストック: .envを軸にしたセキュリティ記事

## ステータス: 構想中

## きっかけ

- 元記事: [もう.envにAPIキーを平文で置くのはやめた — macOS Keychain管理CLI「LLM Key Ring」](https://zenn.dev/yottayoshida/articles/llm-key-ring-secure-api-key-management)
  - 著者: yotta (@yottayoshida)
  - 公開日: 2026-03-01
- 関連: [hackerbot-claw: GitHub Actions Exploitation](https://www.stepsecurity.io/blog/hackerbot-claw-github-actions-exploitation)

## 元記事の要点

### LLM Key Ring (`lkr`) とは
- macOS Keychain にAPIキーを暗号化保存するRust製CLIツール
- `.env` に平文でAPIキーを置くリスクをゼロにする
- `lkr exec -- python script.py` で環境変数として注入（stdout/ファイル/クリップボードに出さない）

### なぜ今これが重要か
- AIエージェントがローカルでコマンドを自動実行する時代
- `.env` 平文保管の3大リスク:
  1. **うっかりコミット**: `.gitignore` は人間の運用ミスに弱い
  2. **シェル履歴への混入**: コマンドライン引数やログに残る
  3. **プロンプトインジェクション**: AIエージェントが秘密を自動抽出する可能性

### 3層のTTYガード（AIエージェント対策）
1. **Layer 1**: stdout ブロック — 非対話環境で `--plain`/`--show` を拒否
2. **Layer 2**: クリップボード ブロック — 非TTYではコピーをスキップ
3. **Layer 3**: `exec` をデフォルトルートに — 「出力しない」が最強の防御

### 種別管理
- `runtime`: 推論API呼び出し用（普段使う）
- `admin`: 強権限（使用量確認等、普段触れない）
- `exec`/`gen` は runtime のみ注入 → 権限混在を防止

### 脅威モデル（守れる/守れない）
| 守れる | 守れない |
|:---|:---|
| .env平文常駐 → Keychain保管 | root権限での侵害 |
| CLI引数/履歴への露出 → プロンプト入力 | genで生成したファイルの同一権限読み取り |
| クリップボード残留 → 30秒自動クリア | IDE統合ターミナル(pty)すり抜け |
| 非対話環境からの吸い出し → TTYガード | 子プロセスのログ出力 |
| メモリ残留 → Zeroizing<String> | |

### 技術スタック
- Rust workspace: `lkr-core`(ロジック) + `lkr-cli`(CLI)
- `KeyStore` trait でストア抽象化（将来 Linux libsecret / Windows Credential Manager 対応）
- `Zeroizing<String>` でDrop時メモリゼロクリア

## 記事の方向性（案）

### 切り口: Unity/ゲーム開発者向け `.env` セキュリティ
- Claude Code + MCP 環境での `.env` リスク（CLAUDE.md に書いたAPIキーが漏洩するシナリオ）
- hackerbot-claw の GitHub Actions 攻撃と `.env` の関係
- LLM Key Ring の紹介と Unity 開発での実践的な導入方法
- `lkr exec` で Claude Code を起動するワークフロー

### 関連トピック
- hackerbot-claw（2026年2月、AI自動攻撃ボット）
  - Microsoft, DataDog, CNCF 等のOSSを攻撃
  - GitHub Actions の CI/CD 設定ミスを自動スキャン
  - 5つの攻撃手法: Go init()注入、スクリプト注入、ブランチ名インジェクション、ファイル名インジェクション、AIプロンプト注入
  - Trivy リポジトリの完全乗っ取り
- OpenClaw セキュリティ問題
  - ClawHub に1,184以上の悪意あるスキル
  - 13.5万インスタンスが安全でないデフォルト設定で公開
  - 9件のCVE（うち3件はRCE）

## 参考リンク

- [LLM Key Ring GitHub](https://github.com/yottayoshida/llm-key-ring)
- [LLM Key Ring crates.io](https://crates.io/crates/lkr-cli)
- [hackerbot-claw (StepSecurity)](https://www.stepsecurity.io/blog/hackerbot-claw-github-actions-exploitation)
- [From Clawdbot to OpenClaw (Vectra AI)](https://www.vectra.ai/blog/clawdbot-to-moltbot-to-openclaw-when-automation-becomes-a-digital-backdoor)
- [OpenClaw Security Risks](https://blog.cyberdesserts.com/openclaw-malicious-skills-security/)
