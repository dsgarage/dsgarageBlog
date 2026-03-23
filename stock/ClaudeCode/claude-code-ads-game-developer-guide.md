# Claude Code × 広告運用 — ゲームデベロッパーのための完全ガイド

> **公開日**: 2026-03-03
> **著者**: dsgarage
> **カテゴリ**: AdTech / GameDev / AI自動化

---

## はじめに

2026年、AI によるコード生成だけでなく、**広告運用の自動化**が現実のものとなりました。本記事では、Anthropic の CLI ツール「Claude Code」を活用して、ゲームデベロッパーが AdMob・Firebase・Google Ads の費用対効果を劇的に改善する方法を解説します。

きっかけとなったのは、以下の2つのリソースです：

- **[claude-ads](https://github.com/AgriciDaniel/claude-ads)** — Claude Code 向け広告監査スキル（186チェック項目）
- **[Stormy.ai ガイド](https://stormy.ai/blog/claude-code-google-ads-audit-guide)** — Google Ads 監査の実践ガイド

---

## 目次

1. [claude-ads とは何か](#1-claude-ads-とは何か)
2. [ゲームデベロッパーへの2つの価値](#2-ゲームデベロッパーへの2つの価値)
3. [マネタイズ最適化：AdMob + Firebase](#3-マネタイズ最適化admob--firebase)
4. [UA最適化：Google Ads + Claude Code](#4-ua最適化google-ads--claude-code)
5. [Claude Code との具体的な連携方法](#5-claude-code-との具体的な連携方法)
6. [フルファネル統合：Firebase で収益を可視化](#6-フルファネル統合firebase-で収益を可視化)
7. [導入ロードマップ](#7-導入ロードマップ)
8. [現時点の制限と今後の展望](#8-現時点の制限と今後の展望)

---

## 1. claude-ads とは何か

**claude-ads** は、Claude Code 上で動作する包括的な広告監査・最適化スキルパッケージです。

### 対応プラットフォーム

| プラットフォーム | チェック数 |
|---|---|
| Google Ads | 主要 |
| Meta Ads | 主要 |
| YouTube | 対応 |
| LinkedIn | 対応 |
| TikTok | 対応 |
| Microsoft Ads | 対応 |
| **合計** | **186項目** |

### 5層アーキテクチャ

```
Layer 5: テンプレート層 ─ 11業界別の戦略フレームワーク
Layer 4: 参照層       ─ RAGパターンによるオンデマンド知識読込
Layer 3: エージェント層 ─ 6つの並行サブエージェントで高速分析
Layer 2: サブスキル層  ─ プラットフォーム別の深層分析
Layer 1: オーケストレーター層 ─ コマンドルーティング
```

### 主要コマンド

```bash
/ads audit              # 全プラットフォーム一括監査（Health Score 0-100）
/ads google             # Google Ads 深層分析
/ads meta               # Meta Ads 深層分析
/ads budget             # 予算配分の最適化提案
/ads creative           # クリエイティブ監査
/ads plan gaming        # ゲーム業界向け戦略テンプレート
```

### 手動監査 vs Claude Code 監査

| 項目 | 手動監査 | Claude Code 監査 |
|---|---|---|
| 速度 | 4〜8時間 | **5分未満** |
| 深さ | スプレッドシート程度 | API層の詳細分析 |
| チェック数 | 人的制限あり | **186項目の自動チェック** |
| 一貫性 | 人的ミスあり | 再現性のある結果 |

> **図1**: claude-ads アーキテクチャ図（→ `diagrams/01_claude-ads-architecture.drawio`）

---

## 2. ゲームデベロッパーへの2つの価値

ゲーム開発者にとっての広告活用は**2つの軸**で考える必要があります。

### A. マネタイズ（収益化）

ゲーム内に広告を表示して収益を得る側面。AdMob、メディエーション、Firebase A/B Testing が主役。

### B. UA（ユーザー獲得）

広告を出稿してインストール数を伸ばす側面。Google Ads、Meta Ads + Claude Code 監査で費用対効果を改善。

### アーキテクチャパターンとしての応用価値

claude-ads の設計パターンは、広告以外にも応用できます：

| claude-ads の機能 | ゲーム開発への応用 |
|---|---|
| 186個の広告チェック | Unity プロジェクトの品質チェック（未使用アセット、missing reference 等） |
| Health Score 0-100 | ビルドヘルススコア（パフォーマンス・メモリ・描画コールの総合評価） |
| 6並行サブエージェント | 並行QA（レンダリング/物理/UI/音声/ネットワーク/メモリを同時検査） |
| 業界テンプレート | ジャンル別テンプレート（FPS/RPG/パズル等のベストプラクティス） |
| MCP でAPI接続 | Unity MCP でエディタと直結 |

> **図2**: ゲームデベロッパーの広告2軸図（→ Figma ダイアグラム）

---

## 3. マネタイズ最適化：AdMob + Firebase

### 広告フォーマットの最適な使い分け

| フォーマット | eCPM | 推奨配置 | ゲームでの例 |
|---|---|---|---|
| **リワード動画** | 最高（収益の40〜50%） | ユーザー選択型 | ライフ回復、ガチャ追加回数 |
| **リワードインタースティシャル** | 高い | 自然な区切り | ステージ間、リザルト画面 |
| **インタースティシャル** | 中〜高 | 画面遷移時 | レベルクリア後 |
| **アプリオープン広告** | 中 | 起動時 | アプリ復帰時 |
| **バナー** | 低い | 常時表示 | ゲーム画面下部（補助的） |

**実績データ**:
- Dreamotion 社: リワード広告導入で **eCPM 70%向上、広告収益 2.5倍**
- Cross Field 社: アプリオープン広告で **ARPU 30%向上**

### メディエーション設定（2026年最新）

> **重要**: 2026年1月31日で Unity Ads のウォーターフォールが廃止されました。**ビッディング（リアルタイム入札）への移行が必須**です。

```
推奨メディエーション構成:

AdMob メディエーション
├── ビッディング（リアルタイム入札）
│   ├── Google AdMob（必須）
│   ├── Unity Ads（ビッディングのみ対応）
│   ├── Meta Audience Network
│   └── AppLovin / ironSource
└── リージョン別メディエーショングループ
    ├── Tier1（US/JP/UK） → 高 eCPM Floor
    ├── Tier2（EU/KR/TW） → 中 eCPM Floor
    └── Tier3（その他）   → 低 eCPM Floor
```

### Firebase A/B Testing で広告配置を検証

Firebase を使えば、新しい広告配置をリテンションに悪影響がないか検証してからロールアウトできます。

**手順**:

1. Firebase Analytics と AdMob をリンク → ARPU/LTV が自動計算される
2. Remote Config でフラグ管理（例: `show_rewarded_interstitial = true/false`）
3. A/B テストで少数ユーザーに新広告を配信
4. リテンション・課金収益への影響を同時測定
5. 悪影響がなければ全ユーザーに展開

> **図3**: メディエーション構成図（→ `diagrams/03_mediation-architecture.drawio`）

---

## 4. UA最適化：Google Ads + Claude Code

ゲームのインストール広告を出稿している場合、Claude Code で費用対効果を自動改善できます。

### よく見つかる無駄

| 問題 | 影響 | Claude Code での検出方法 |
|---|---|---|
| ブランドキーワードへの無競争入札 | 予算の20〜30%が無駄 | 検索語レポート自動分析 |
| コンバージョン設定漏れ | 最適化が効かない | タグ整合性チェック |
| 低品質プレースメント | CPI が異常に高い | プレースメント別パフォーマンス分析 |
| 地域ターゲティングの不備 | 非対象国への配信 | 地域別コスト分析 |

### 監査の実行フロー

```
/ads audit 実行
    │
    ├→ エージェント1: Google Ads 分析
    ├→ エージェント2: Meta Ads 分析      ← 6エージェント並行実行
    ├→ エージェント3: YouTube 分析
    ├→ エージェント4: 予算分析
    ├→ エージェント5: クリエイティブ分析
    └→ エージェント6: コンバージョン分析
    │
    ▼
  統合レポート出力
  ├ Health Score: 72/100
  ├ Critical: コンバージョン設定漏れ 2件
  ├ Warning: CPI高騰キーワード 5件
  └ 優先アクションプラン（優先度順）
```

> **図4**: 監査フロー図（→ Figma ダイアグラム）

---

## 5. Claude Code との具体的な連携方法

Claude Code との連携は **3つのレイヤー** で構成されます。それぞれ独立して導入可能です。

### レイヤー 1: Google Ads MCP（UA広告データの直結）

Google 公式の MCP サーバーでキャンペーンデータを Claude Code から直接取得します。

**セットアップ**:

```bash
# Step 1: Google Ads API の認証情報を取得
# Google Cloud Console → プロジェクト作成
# Google Ads API を有効化
# OAuth 2.0 クライアント ID を作成 → credentials.json をダウンロード
# Google Ads Manager → ツール → API センター → Developer Token を申請

# Step 2: Claude Code に MCP サーバーを登録
claude mcp add google-ads-mcp \
  -e GOOGLE_APPLICATION_CREDENTIALS=/path/to/credentials.json \
  -e GOOGLE_PROJECT_ID=your-project-id \
  -e GOOGLE_ADS_DEVELOPER_TOKEN=your-token \
  -- pipx run --spec git+https://github.com/googleads/google-ads-mcp.git google-ads-mcp
```

**プロンプト例**:

```
「アプリインストールキャンペーンの過去30日間のCPIを表示して」
「CTRが1%以下のアプリ広告グループを一覧にして」
「地域別のインストール数とコストを比較して」
```

> **制限**: 現在は読み取り専用（データ取得・分析のみ、入札変更等は不可）

### レイヤー 2: Firebase MCP（A/Bテスト・Remote Config 操作）

Firebase 公式の MCP サーバーで、広告配置の A/B テストを Claude Code から制御します。

**セットアップ**:

```bash
# プラグインインストール（最も簡単）
claude plugin install firebase@firebase

# または手動追加
claude mcp add firebase -- npx -y firebase-tools@latest mcp
```

**操作可能な Firebase 機能**:

| Firebase 機能 | Claude Code でできること |
|---|---|
| Remote Config | 広告フラグの取得・更新（`show_rewarded_ad`, `ad_frequency` 等） |
| Crashlytics | 広告SDK起因のクラッシュ調査 |
| Cloud Messaging | プッシュ通知の送信 |
| Firestore | カスタム分析データの読み書き |

**プロンプト例**:

```
「Remote Config の 'rewarded_ad_placement' パラメータを取得して」
「Crashlytics で AdMob 関連のクラッシュを直近7日分調べて」
```

> **注意**: Firebase MCP は AdMob のレポートデータ（eCPM等）には直接アクセスできません。

### レイヤー 3: claude-ads スキル（包括的な監査・最適化）

MCP で取得したデータを 186項目のチェックリストで自動監査するスキルです。

**インストール**:

```bash
curl -fsSL https://raw.githubusercontent.com/AgriciDaniel/claude-ads/main/install.sh | bash
```

> **図5**: 連携アーキテクチャ全体図（→ `diagrams/05_integration-architecture.drawio`）

---

## 6. フルファネル統合：Firebase で収益を可視化

3つのレイヤーを組み合わせることで、**UA広告出稿からゲーム内広告収益まで**を一気通貫で管理できます。

```
UA広告出稿（Google Ads / Meta Ads）
    │
    │  インストール
    ▼
Firebase Analytics（ユーザー行動追跡）
    │
    │  セッション・リテンション・課金
    ▼
AdMob 収益（広告マネタイズ）
    │
    ▼
LTV（生涯価値）= 課金収益 + 広告収益
    │
    ▼
UA の ROAS（広告費用対効果）を正確に計算
```

Firebase で AdMob をリンクすると、**広告収益も含めた正確な LTV** が算出されます。これにより、UA 広告の入札を「広告収益込みの ROAS」で最適化でき、ゲーム開発者にとって最も大きなメリットとなります。

> **図6**: フルファネルフロー図（→ Figma ダイアグラム）

---

## 7. 導入ロードマップ

| 優先度 | アクション | 所要時間 | 期待効果 |
|---|---|---|---|
| **高** | Firebase MCP を導入 | 5分 | Remote Config / Crashlytics 操作 |
| **高** | claude-ads スキルをインストール | 2分 | 186項目の自動監査 |
| **高** | AdMob メディエーションをビッディングに移行 | 1〜2日 | eCPM 向上 |
| **高** | リワード広告の配置最適化（Firebase A/B Test） | 1週間 | 収益 +30〜50% |
| **中** | Google Ads MCP を導入 | 30分〜 | UA広告の無駄削減 20〜35% |
| **中** | Firebase ↔ AdMob リンクで LTV 可視化 | 1日 | ROAS 改善 |
| **低** | 地域別メディエーショングループ最適化 | 継続的 | eCPM の底上げ |

---

## 8. 現時点の制限と今後の展望

| 項目 | 現状 | 今後の見通し |
|---|---|---|
| AdMob 収益データ | MCP 直結なし | Google が AdMob MCP を出す可能性あり |
| Google Ads 操作 | 読み取り専用 | 書き込み対応のロードマップあり |
| Firebase Analytics | MCP 非対応 | BigQuery エクスポート経由なら取得可能 |
| AI メディエーション | 未実装 | オンデバイス ML で広告表示予測が進行中 |

---

## 参考リンク

- [claude-ads (GitHub)](https://github.com/AgriciDaniel/claude-ads)
- [Stormy.ai - Google Ads 監査ガイド](https://stormy.ai/blog/claude-code-google-ads-audit-guide)
- [Google Ads MCP サーバー（Google公式）](https://developers.google.com/google-ads/api/docs/developer-toolkit/mcp-server)
- [Firebase MCP サーバー](https://firebase.google.com/docs/ai-assistance/mcp-server)
- [AdMob メディエーションガイド](https://support.google.com/admob/answer/13420272?hl=en)
- [Firebase A/B Testing](https://firebase.google.com/docs/ab-testing)
- [Firebase × AdMob 連携](https://firebase.google.com/docs/admob/analytics-and-firebase)
- [広告頻度最適化チュートリアル](https://firebase.google.com/docs/tutorials/optimize-ad-frequency/solution-overview)
- [Unity Ads ビッディング設定](https://docs.unity.com/monetization-dashboard/en-us/manual/bidding-in-google)

---

*本記事は Claude Code (Opus 4.6) の支援を受けて作成されました。*

---

## 付録: ダイアグラム一覧

### draw.io（ブラウザで編集可能）

| 図 | 内容 | リンク |
|---|---|---|
| 図1 | claude-ads 5層アーキテクチャ | [draw.io で開く](https://app.diagrams.net/?grid=0&pv=0&border=10&edit=_blank#create=%7B%22type%22%3A%22mermaid%22%2C%22compressed%22%3Atrue%2C%22data%22%3A%22rVVNj6owFP01XZrQlkK7bBGcSWaSF2XjklH0GYm8oC7m37%2FbQoGCI0ZnQsbLbXvuOfej7Kvs319EvFQhT8Jv%2B5yvX%2FtmDRGyKbLrNp9l2zO8MxQzpDCSPoop4gRJYgyKVGQ988bDA%2BtJrAc3hqQAPIjafz4YYgq2fGTfeWXCUrPXAYaYtDFEaIEjlw48vKPMohMGCgGSzNAMkWCIR3oDbAMnLGkjaJa0p8ZnP%2BJLy6pV2WUi0SrZ%2FJ5Q3xXqW6HAKNEYQIQDkdARsZQLSww3sVQyJAbJaTIGTOQwY9xmTMSjJWHkJ0Y%2B14kS3BjSJI0jFesok9KoK032agiM%2BDBbitulcMTIrSGUx9fbdZ3gPzYlrDHaggVPxplURVxVxFE1jq%2FEcCykcsSMOlhYMn31%2FS4k3R6nC3tNrEIdoBtVs6THJtD1Ft60TOzKxG7x5Cip2NXbYz0cmUGvDlNBelN9szXbJA7mvK04dU8B4G29%2BWmrPffuPk0PphgaXvNMkIh%2BrVx3779Fnf1FWe6LHN6lvnwnSvZZn%2FnML9mDJ9b1iXV5Ta9f%2BXRPvDc9cTgd8%2B37afJAWu9PD8e0PE7zXzUCDpuqPJe7yx0VD5YOLjkljSGRUPdT%2FtaEf8uz4qIxVpuyynVbejPsTU%2BM%2FFOfN%2BF8MyJMX928vW6I%2BzkkekqbdhjdROP%2BUnSSwrKlwPU5DQYJ8HX%2FD0ZRzEfjc%2Fvqu5lo%2BDoTbzZDNEbmC9Z7of0X0n%2FBQxDcW10YX2C62Frr1oLGs2babVwNAfuxoZp2IxTGmpCgcddcvs2IGVG7Q1GYm476UnhzgUh0vlTlMa%2Bd8AeeTVmUVe3Y7XY%2FwfkOHFPSi1%2BBow5cECkveQWOOHDhPPKSZArOgyn4AQ47cDyevwRnitfBkTiKQvy8WNMAHVwchDG0%2BtNwSxdOKCZU8ADcfw%3D%3D%22%7D) |
| 図3 | AdMob メディエーション構成 2026 | [draw.io で開く](https://app.diagrams.net/?grid=0&pv=0&border=10&edit=_blank#create=%7B%22type%22%3A%22mermaid%22%2C%22compressed%22%3Atrue%2C%22data%22%3A%22vVZtb5swEP41%2FlgJMAb7o03wpHWZqpVon%2FtCsqhRiEi6qf9%2BdzYGTJo2gLYWJVfbd89zz92ZbuqHwy8SBYUigYTv9jm%2BPm6aPRJF8nlZPaKZU8IpkWFj8NQYEZHUGbzZUlmzolLn5QwFhxP8kys0OCcCIeE3AawBkf4jl4Spf0EHUNkCgS9j%2B4I0roI6ZHqBgs3WIgNfjUS4IyWlO%2BwCSuUOa7cVuy1wZ4QzIhmqJjIiFy6g%2BFi3%2FqNCq%2BGXqtrsShTViMmyfY8dQ3yAygXC4koPxyp1DVRkoVb77enNIB0HONNEBCMkMm8MVGoSO2rZLcvTA5J7fd6W%2B6cSGX4vT3%2Bq%2BmVcuLjpzcPhW%2FV7uzerGqNt62p%2FX73WEPuzgOX%2BeUIXNm3Ub3L%2BTpNj93DTPX0l255r3aEi6fX9VDT9VGzLOiQUfVb3kPnXO%2FhY3dqCCwQRHDPM7pbwpXdVVY%2FSt4g6nMji5CuAuP0BH8VPixNj5jAWc3Boh0MtTtNoYtFrPUCCjJIWdkF4PhZ2dLVhLKWZEkZESLgru3AlleKskvnZbPHra6ub2g77TOohDswetpcyXFIijDygjdGjEdHcWTzodcOIsuhoBBVTFP%2F27C4QfTYsTjWeDF8f3cuCdsMySGtSNrSXzX9h6SYD9wMsxDTesc%2B7fQ%2Bmfl2iszupd8ZeRYNOvkB3FDnmtYhw9xvvUTmHsYN73aR%2BNrsSZ%2F%2FmhlC8CuBFi4sJMS%2FC1qSdGX%2FkXnTuRecOt5Pvc%2FFYF0l3R3R3RHdEoKqtyc6zOp7e7D8KSG%2B93e2MejSOOEPP7Hiqq5fSLsIPrDxVO7j%2BzMJ6vb4QzujThaOx5IzOCBd54bRSWfApuyAILoWjXriQpymKN5ld7LPTiUrU9HCFr12UZ1kazgg30I6KLJyRbOFrJ5hkMpkeTvvJ5mmc0WxGOD%2FZPEnzaEay2k9Wh1kc6Ol9p%2F1GobHgixmNAvPcD6cWIF16Bbu%2F%22%7D) |
| 図5 | Claude Code 連携アーキテクチャ全体図 | [draw.io で開く](https://app.diagrams.net/?grid=0&pv=0&border=10&edit=_blank#create=%7B%22type%22%3A%22mermaid%22%2C%22compressed%22%3Atrue%2C%22data%22%3A%22tVbbjtowEP2aeVzJceLgPNomppW26pbdVupjYAOLmpIqCZX27zu%2B5QLshUVFFhts58zxmYvZNsWfJ6DkdglE4LMf7WG19WtAqaqKw2OJtqrtA%2FIMOAERQ55CRkAKyGPgFAS1RgxShZm5n%2BFpmNFhJvIG4lB6RGA8lAImT2lQYHPz1ssvTk9xWzyXDZoRxOaNRV1vK4MmHlv8%2FqLuXmcxHguz23KaoDC1tzDkvmz%2BGl%2BB4nsgxd3nM5D4bebV%2FlhPI76TOhiZmIrvlhhwbsXHSOUmWIi1EN9ubRjHAUGDB2zxJvVy%2F%2FgR7anTXu%2BaclW05aW66173HuEazZc%2BsZbl77pzibXf7LZObmbSmlsFswh4EDdjQaTMyyZRyBmIHERkDJQY5b6Ehlr6%2FG6K9ql67nbrdhxxPXVoh%2Fc8C0tIipuwSW0DrUHKiyjo%2B0HWtqubsieAY3Zc1MaLdeezMQp%2BJUjuZ%2Fj8f%2BVQ7HJobbvBTWGLJBDLTspEvj%2B7xNyrAFQ7WPSw64wUkamgDATGltkgSxPwSR9Dp7PjAGHsLomCWESOgNiW%2Bw5MqzLV6vrBaSVfBk2n0NRWTtkVVwPHU%2BDYAP%2BsDw%2BH1fWkkyl24tIysaXJfdlls6vdsKkbdrb8xLRNopGc3G19L76sA4hFOmWQGgbqxwcO9mZRff3%2B4LMc5ZPY5SLb4ogV1J003B%2FZfFr5sdmDxD6VRdWZqrxfj1tFf%2F%2BP7pShRfVdpNeoX8qGpXPHO3sk%2FFNAyc0NxDn4%2B3j009yZr2zX0%2B1LZfekYFtxMLElvgJhesXoFxZueM9U2mDHIzsZ2Wxkp0eOrkUbeJlYnyjXds%2F274U9z2ZXVbafxqliCjOCqrZr6l%2Blm8QPzqzrqm7cxGazeQHOx2AATChnOvk4oD4G1FoJyt8CJIS8AOhiNuARInl2BUGj7hgvj2aEsXfg%2FQM%3D%22%7D) |

### Figma / FigJam（ブラウザで編集可能）

| 図 | 内容 | リンク |
|---|---|---|
| 図2 | ゲームデベロッパー広告2軸フロー | [FigJam で開く](https://www.figma.com/online-whiteboard/create-diagram/9227c178-f2d4-4a00-83b0-4b254fc6c6f8?utm_source=other&utm_content=edit_in_figjam&oai_id=&request_id=07499c13-783b-454a-a8db-cce555b3a83f) |
| 図4 | Claude Code 広告監査シーケンス | [FigJam で開く](https://www.figma.com/online-whiteboard/create-diagram/961b8ff3-e1b6-4bee-8bd5-f3cf6112a990?utm_source=other&utm_content=edit_in_figjam&oai_id=&request_id=5cb0e229-7d34-44d7-93b1-48a0ee91292c) |
| 図6 | フルファネル統合フロー | [FigJam で開く](https://www.figma.com/online-whiteboard/create-diagram/e378f242-f3e7-4fdc-8f80-dbcd82fa3419?utm_source=other&utm_content=edit_in_figjam&oai_id=&request_id=8dbd1b39-784b-404b-b6f0-97bd0c83916e) |
