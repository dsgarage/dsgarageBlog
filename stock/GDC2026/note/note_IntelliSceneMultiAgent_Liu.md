# GDC 2026: IntelliScene — マルチエージェントAIがゲームシーン配置を自動化する

オープンワールドゲームの開発で、最も時間を食う工程は何か。プログラミングでもモデリングでもない。**シーン装飾（Scene Decoration）**――3D 空間内にオブジェクトを配置して「生活感」や「世界観」を演出する作業だ。部屋に家具を配置し、棚に本を並べ、テーブルの上にコーヒーカップを置く。プレイヤーが数秒で通り過ぎる場所にも、「そこに人が暮らしていた痕跡」を演出しなければならない。あるオープンワールドタイトルでは、メインストーリーに関係しない「非コアエリア」がマップの7割以上を占める。この膨大な空間を、人手で埋め続けるのは限界がある。

Tencent Games（テンセント・ゲームズ。『Honor of Kings』『PUBG Mobile』等を擁する世界最大級のゲームパブリッシャー）の AI 研究者 Lockliu（Zhongyuan Liu）が GDC 2026 で発表した **IntelliScene** は、この問題に正面から挑む。複数の AI エージェント（自律的に判断・行動する AI プログラム）が「考えて、計画して、配置する」というプロのデザイナーの思考プロセスを再現し、ゲームシーンを自動構築するシステムだ。バージョン 1.0 から 3.0 への進化を追った本セッションは、**LLM（Large Language Model＝大規模言語モデル。ChatGPT 等の基盤技術）単体の限界**と、それを**Vision（画像認識）+ Language（言語理解）+ Geometry（幾何学的空間認識）の融合**で突破する方法論を明快に示した。

本記事では、現地で聴講した内容をもとに、IntelliScene の技術的アプローチ、実験結果、そして MCP（Model Context Protocol＝AI モデルが外部ツールと連携するための標準プロトコル）を活用する 3.0 の構想までを解説する。ゲーム開発における AI 活用の最前線を知ることで、自プロジェクトへの応用可能性が見えてくるはずだ。


---

## セッション概要

GDC（Game Developers Conference）は毎年3月に米国サンフランシスコで開催される世界最大のゲーム開発者カンファレンスだ。技術講演、ビジネスセッション、展示が1週間にわたって行われ、世界中から数万人の開発者が集まる。

| 項目 | 内容 |
|:---|:---|
| **イベント** | GDC Festival of Gaming 2026 |
| **日時** | Tuesday, March 10, 2026 / 11:30am - 12:30pm（現地時間 PST） |
| **タイトル** | IntelliScene: Multi-Agent for Reasoning-Driven Game Scene Layout（推論駆動のゲームシーンレイアウトのためのマルチエージェント） |
| **スピーカー** | Lockliu / Zhongyuan Liu（Tencent Games）Senior AI Researcher and Game Developer |
| **会場** | Room 3014, West Hall（Moscone Center） |
| **パス** | Festival Pass, Game Changer Pass |
| **レベル** | Advanced（上級者向け） |
| **トラック** | Game & Production Technology, Design |
| **形式** | Sponsor Session（スポンサー企業による技術講演。Tencent Games AI 提供） |

Moscone Center の West Hall は GDC において技術系セッションが集中するエリアで、Room 3014 は比較的こぢんまりとした部屋だ。11:30am という午前中のスロットにもかかわらず、席はほぼ埋まっていた。Tencent Games が GDC でスポンサーセッションを持つことの珍しさ――中国のゲーム大手が自社の AI 研究をここまでオープンに発表するケースはまだ少ない――もあってか、聴講者の注目度は高い。

セッション全体は4パートで構成される。

| セクション | 内容 |
|:---|:---|
| **01** | The Essence of Scene Construction — シーンデザインの本質と課題 |
| **02** | IntelliScene 1.0 — テキストAIだけで挑んだ最初のバージョン |
| **03** | IntelliScene 2.0 — 画像生成AIの力を借りた進化版 |
| **04** | Future Work / Possibilities — 3.0の予告とゲームエンジンAIの未来 |

![セッション構成スライド](source/images/IMG_4717.jpg)

---

### この記事で読めること

- **シーン構築の本質**
- **IntelliScene 1.0**
- **IntelliScene 2.0**
- **Future Work / Possibilities**
- **Tencent AI Booth とデモ体験**
- **まとめ**
- **スピーカー情報**
- ...ほか全8セクション

> **本記事のボリューム**: 約26,489文字 / スライド画像32枚
> スピーカーのトランスクリプト（発言の文字起こし）を原文・日本語訳つきで完全収録しています。

---

<!-- ===== ここから有料エリア（Note エディタで有料ラインを設定） ===== -->

## スピーカー情報

- **Lockliu / Zhongyuan Liu** -- Tencent Games, Senior AI Researcher and Game Developer
- Google Scholar: 被引用数256、AIGC（AI Generated Content）Games分野の研究者
- SIGGRAPH 2026 関連セッションにも登壇予定

### 関連論文

- **Imaginarium: Vision-guided High-Quality 3D Scene Layout Generation** -- SIGGRAPH Asia 2025 (ACM Transactions on Graphics)

---

## 1. シーン構築の本質 -- なぜこの問題は難しいのか

### ゲームシーン構築（Scene Layout）とは

Lockliu はまず、スライドにオープンワールドゲームの美麗なスクリーンショットを並べ、こう切り出した。

> "Game scene construction is to build an immersive virtual world, within the constraints of game engine and hardware, based on the game's story, planning intent, and design aesthetics, by combining pre-made 3D assets."
>（ゲームシーン構築とは、エンジンとハードウェアの制約の中で、ゲームのストーリー・企画意図・デザイン美学に基づき、あらかじめ制作された3Dアセットを組み合わせて没入的な仮想世界を構築することだ）

プレイヤーはコンテンツを速く消費する。デザイナーは「同じパターンの繰り返し」ではなく、テーマ性があり、ストーリーに合致し、美的にも心地よいシーンを大量に作らなければならない。だがこの作業は極めて時間がかかる。

![Game Scene Construction](source/images/IMG_4719.jpg)

### 伝統的ワークフローの「氷山理論」

シーンデザインのパイプラインは6段階で構成される。プレイヤーが目にするのは氷山の一角（完成されたシーン）だが、水面下には膨大な工程が隠れている。Lockliu が提示した図はこの「氷山理論」を可視化したものだ。

```
Design Planning → Visual Preview → Asset Preparation
    → Scene Assembly → Narrative Details → Optimization & Testing
```

| 段階 | 担当 | 内容 |
|:---|:---|:---|
| Planning | Designer/Level | レイアウト設計、ゲームプレイ検証（Greybox＝仮の箱で構成を確認） |
| Design | Art | ビジュアルスタイルの決定、コンセプトアート作成 |
| Production | Art/Tech | 3Dモデル（アセット）の制作・選定・最適化 |
| Assembly | Art | シーン組立、ライティング、雰囲気づくり、ディテールの充填 |
| Narrative | Art/Designer | 環境に物語を語らせる演出（例: テーブルの上に飲みかけのコーヒー → 住人が急いで出た痕跡） |
| Optimization & Test | Tech/QA | 描画パフォーマンス、衝突判定、バグ修正 |

![Iceberg Theory](source/images/IMG_4720.jpg)

### シーン装飾の4つの課題

Lockliu はシーン装飾が抱える根本的な課題を4つ挙げた。

- **高反復性**: 本棚に本を並べる、食器棚に皿を入れるといった大量の小物配置。一つひとつは単純だが、量が膨大
- **時間消費**: 特にオープンワールドの非コアエリア（プレイヤーが素通りするかもしれない場所）の充填。マップの大部分はこの「素通りされるかもしれない場所」だ
- **一貫性維持の難しさ**: 「中世ファンタジー」の世界に現代の椅子が混入するようなミスを防ぐには経験が必要。スタイルの一貫性（style consistency）は、大規模チームでは特に崩れやすい
- **クリエイティブボトルネック**: 「自然に」かつ「ストーリーテリングのセンスを持って」配置するには熟練が要る。ただ物を置くだけではなく、「なぜそこにあるのか」という理由が必要だ

> **"Our proposal: use AI to support the non-core area, so that human designers can focus on the core creative work."**
>（提案：非コアエリアにAI支援を導入し、人間のデザイナーはコアエリアの創造的作業に集中する）

日本のゲーム開発に置き換えて考えると、例えば『ゼルダの伝説 ティアーズ オブ ザ キングダム』のようなオープンワールドタイトルで、プレイヤーが訪れるかどうかわからない無数の洞窟や村落の内装をすべて手作業で埋める負担を想像すると、この問題の深刻さがわかる。

---

## 2. IntelliScene 1.0 -- LLM Agent Swarm（テキストAIだけで挑む）

### コンセプト: "Slow Thinking"（遅い思考）

Lockliu が 1.0 の解説で繰り返し強調したのが **"slow thinking"** という言葉だった。

> "Humans in design, you care for what you do sometimes. You pause and wait. You need slow thinking."
>（人間のデザイナーは、時に立ち止まって考える。急がず、待つ。「遅い思考」が必要なのだ）

人間のシーンデザイナーは、いきなり家具を配置するのではなく、まず「この部屋はどんな人が住んでいるか」「生活動線はどうか」を**推論・計画**してから手を動かす。この「遅い思考」――Daniel Kahneman の二重過程理論（System 1 が直感的で速い思考、System 2 が分析的で遅い思考）でいう "System 2" ――を、**複数のAIエージェントが役割分担して再現**するのが IntelliScene 1.0 のコンセプトだ。

従来の data-driven（データ駆動型）のシーン生成モデルとの違いについて、Lockliu はこう述べた。

> "Compared to pure data-driven models, the use of high-quality reasoning data... the model can now understand why there should be a certain placement. Our approach has a reason to report. So I can justify decisions and use tools, more like human thinking."
>（純粋なデータ駆動モデルと比較して、高品質な推論データを使うことで、モデルは「なぜそこに置くべきか」を理解できるようになる。我々のアプローチには理由がある。人間の思考のように、判断を正当化し、ツールを使える）

一つのAIにすべてをやらせるのではなく、「要件を分析するエージェント」「アセットを選ぶエージェント」「座標を計算するエージェント」など、専門家チームのように分業させる。これが **Agent Swarm（エージェント群）** の考え方だ。

> "Not just an empty table. We need to find a flow, target, and impressive model."
>（ただの空テーブルではない。動線、目的、印象的なモデルを見つける必要がある）

![Why Multi-Agent](source/images/IMG_4723.jpg)

### アーキテクチャ: 3層構造

LLMエージェントが人間のように、要件に基づき、空間条件を考慮して、空間デザインタスクを完了する。Lockliu が示したアーキテクチャ図は4つのレイヤーで構成されていた。

| レイヤー | 役割 | 何をするか |
|:---|:---|:---|
| **Layer 1: Requirement** | ユーザー入力 | 「モダンなリビングを作って」等のテキスト要件 + 部屋の3Dモデル |
| **Layer 2: Analysis Agent** | 要件の分析・拡張 | 空間の寸法を把握し、アセットリストを推論し、各家具のサイズを推定 |
| **Layer 3: Generation Agent** | 配置案の生成 | 3D座標（JSON形式）でオブジェクトの位置・向きを出力 |
| **Layer 4: Optimization Agent** | 品質の検証・補正 | 物理的に破綻していないか、美的にバランスが取れているかを計測・修正 |

![IntelliScene 1.0 Architecture](source/images/IMG_4724.jpg)

### 10ステップ・パイプライン

ユーザーの一文の要件から最終的な3Dシーンまで、10段階に分解して処理する。いきなり座標を出すのではなく、段階的に情報を積み上げていくのがポイントだ。Lockliu はこれを "step by step"（一歩ずつ）と表現した。

> "First, we understand the request. Then brainstorm, what objects are needed. Find their edge details like the side panel here. And then decide why each object is there, and the whole layout will be planned."
>（まず要件を理解する。次にブレインストーミング――何のオブジェクトが必要か。サイドパネルのようなディテールまで把握する。そして各オブジェクトがなぜそこにあるべきかを決め、全体のレイアウトを計画する）

```
User Requirement（「和風の書斎を作って」）
  → Requirement Understanding（書斎に必要な要素を理解）
    → Asset List Reasoning（机、本棚、障子、座布団…を推論）
      → Asset Design Reasoning（各アセットのデザイン方針を決定）
        → Asset Size Reasoning（各アセットの現実的なサイズを推定）
          → Usage Reason Reasoning（各アセットの用途と配置理由を推論）
            → Layout Relation Reasoning（「机は窓際」「本棚は壁沿い」等の関係を推論）
              → Select Layout Constraints（制約条件を設定）
                → Initial Coordinate Prediction（初期座標を予測）
                  → Constraint Optimization Coordinates（制約に基づき座標を最適化）
                    → Scene Whitebox（最終的な3D配置の完成）
```

### 課題と解決策

#### Problem 1: LLMは精密な座標計算が苦手

ChatGPT等のLLMは言語が得意だが、「この棚をX=2.3m, Y=0, Z=1.5mに置いて、45度回転」といった**精密な数値計算は本質的に苦手**だ。直接座標を生成させると、家具が壁にめり込んだり、空中に浮いたりする。

> "We should adapt. Let the reasoning do what it can do best: understanding meaning and logic, like 'this should keep some distance' or 'place it against the wall'. Then we let the work of actual numbers to optimization."
>（適応すべきだ。推論には推論が得意なことをさせる：意味と論理の理解だ。「ある程度の距離を保つべき」「壁に沿って配置すべき」という判断。そして実際の数値計算は最適化に任せる）

**解決策: 線形計画法（Linear Programming）の導入**。LLMには「この椅子は机の前に置くべき」「壁から30cm離すべき」といった**関係性の記述だけを担当させ、精密な座標計算は数学ソルバーに任せる**という役割分担だ。

さらに、全オブジェクトを一度に配置（one shot）するのではなく、**1つずつ順番に配置**していく。

> "We don't just place everything in one shot. So we split it into a recursive process. First, place one, then check the error, then place the next. One shot and repeat, step by step. Reducing errors."
>（一度にすべてを配置するのではない。再帰的なプロセスに分割する。まず1つ配置し、エラーをチェックし、次を配置する。一つずつ繰り返す。エラーを削減しながら）

![Problem 1](source/images/IMG_4727.jpg)

#### Problem 2: 入れ子構造のレイアウト（フラクタル問題）

現実のシーンでは「棚の中に本を並べる」「壁に沿って家具を配置する」など、**レイアウトの中にさらにレイアウトがある**入れ子構造が頻出する。

> "We don't just place the furniture. We also need to place books on this shelf."
>（家具を配置するだけではない。その棚の上に本も配置しなければならない）

**解決策: Recursive Hierarchical Layout（再帰的階層レイアウト）**。「このオブジェクトの上/中に、さらに子オブジェクトを配置できるか？」を判定し、YESなら同じレイアウト処理を再帰的に繰り返す仕組みだ。

![Problem 2](source/images/IMG_4728.jpg)

### 省力化ツール3つ

IntelliScene 1.0 には、手作業を削減するための3つの補助ツールが組み込まれている。

| ツール | 機能 | 解説 |
|:---|:---|:---|
| **Environmental Narrative Generation** | シーンにストーリーを付与 | 既存のアセット構成から「ここは誰がどう使っている場所か」を自動生成 |
| **Asset Auto-Labeling** | アセットの自動分類 | 数千ものアセットに対し、カテゴリ・サイズ・用途を自動でタグ付け |
| **Position Calculation** | 配置座標の計算 | 美的バランスと物理法則の両方を考慮した座標算出 |

![1.0 Demo](source/images/IMG_4729.jpg)

### 1.0 の成果と限界

Lockliu はデモ映像を流した。テキスト入力から生成された部屋のシーンが次々と映し出される。

> "We prepared 110 tests in a real game map. The scenes already show basics, story-telling, and aesthetics."
>（実際のゲームマップで110のテストを準備した。シーンは基本、ストーリーテリング、美学をすでに示している）

**成果**:
- 実際のゲームマップで**約110シーン**を自動生成。テキストだけのAIでここまでできることを実証
- 後続バージョンのための**高品質な訓練データ（推論過程付き）を蓄積**。これが2.0以降の基盤になる

しかし、1.0には明確な限界があった。

**限界**:
1. **LLM Hallucination（幻覚）** -- LLMが「もっともらしいが事実と異なる」出力をする現象。空間や物理を直接知覚できないため、物理的に不合理な配置を生成する場合がある
2. **幾何精度の限界** -- 座標・回転角・スケールの精密制御はLLMの苦手領域。線形計画法で補っても限界がある
3. **視覚情報の欠如** -- テキストだけのやりとりでは「見た目の雰囲気」を伝えきれない。「モダンなリビング」と言っても、人によってイメージは千差万別

> "The reasoning-based approach in version 1.0 showed results, but understanding of space and physics can create more reasonable placement. Hard to precisely control position, rotation, and scale — only in direction."
>（1.0の推論ベースアプローチは成果を示したが、空間と物理への理解があればより合理的な配置が可能だ。位置・回転・スケールの精密制御は難しく、方向性だけに留まっていた）

![1.0 Results and Limitations](source/images/IMG_4734.jpg)

---

## 3. IntelliScene 2.0 -- Visual Knowledge VLM Agent（画像の力を借りる）

### 1.0の限界を超える: テキストからビジョンへ

1.0の限界を踏まえ、Lockliu は 2.0 のコンセプトをこう説明した。

> "The goal of 2.0 is to use the VLM agent to send their best path on a virtual project. Many models are now present in human nature — understanding quality, and generating scenes. The goal is to achieve more precise control and repair."
>（2.0の目標は、VLMエージェントを使って仮想プロジェクトに最適なパスを送ること。多くのモデルが人間のような能力を持ちつつある。品質の理解とシーン生成。目標はより精密な制御と修正を実現することだ）

2023-2025年にかけて**画像生成AI**（FLUX, Stable Diffusion等）と**画像認識AI**（Vision Transformer等）が急速に進歩した。その進化を取り込んだのが 2.0 だ。

核心アイデアはシンプルだ。「モダンなリビング」というテキストからまず**参考画像（guidance image）を生成**し、その画像を「お手本」として3Dシーンを組み立てる。画像にはテキストでは表現しきれない**配置の空間関係、スタイルの雰囲気、色彩の調和**が含まれている。

### パイプライン（処理の流れ）

```
User Input: "Modern living room with vintage furniture"（ヴィンテージ家具のモダンなリビング）
  → Step 1: ガイダンス画像を自動生成（AI画像生成）
    → Step 2-5: 画像を分析して3D情報に変換（AI画像認識）
      → Step 6: 最終的な3Dレイアウトを出力（数学的最適化）
```

設計思想は「**プロのアーティストのようにデザイン＆構築する**」こと。かつ、全工程が透明（ホワイトボックス＝中で何が起きているかわかる）で、任意のステップでユーザーが介入・修正できる。ブラックボックスのAIに丸投げするのではなく、**人間とAIの協調**を重視している。

![Visual Guidance Pipeline](source/images/IMG_4736.jpg)

![Architecture Diagram](source/images/IMG_4737.jpg)

### Step 1: 高品質シーンデータセットの構築

IntelliScene 2.0 の土台となるのが、チームが独自に構築したデータセット。AIの性能は訓練データの質に直結するため、ここに大きな投資がされている。Lockliu はデータセットの構築について熱を込めて語った。

#### 3Dモデルライブラリ

> "We built an asset library of about 500 categories. Each model includes detail annotation — English name, style, and a short description. How likely to appear in a scene? Requiring a surrounding space. Whether it's stackable and whether objects can be placed on or inside it."
>（約500カテゴリのアセットライブラリを構築した。各モデルにはアノテーション（注釈データ）が含まれる。英語名、スタイル、短い説明。シーンでの出現確率、必要な周囲スペース、積み重ね可能かどうか、上に物を置けるか中に入れられるか）

- **500カテゴリ、計2,042の高品質リアリスティック3Dモデル**
- ソース: オープンソース + 自社制作 + Unreal Engine Marketplace
- 各モデルに**20項目のアノテーション**を自動付与

> "This annotation generated automatically by our agents using Blender tool capabilities."
>（このアノテーションは、Blenderツールの機能を使って我々のエージェントが自動生成した）

つまりアノテーション作業自体もAIエージェントが担当している。Blender（オープンソースの3DCGソフト）のAPIを叩いて、モデルの形状分析からカテゴリ推定、サイズ計測、スタッキング可能性の判定までを自動化しているのだ。

![3D Model Library](source/images/IMG_4740.jpg)

#### 3Dシーンデータセット

> "We built a dataset with 20 scene categories, 147 scenes. We used criteria by our artists and our design experts. Each scene equals about 27 models on average."
>（20のシーンカテゴリ、147のシーンでデータセットを構築した。アーティストとデザイン専門家の基準を用いた。各シーンは平均約27モデルを含む）

ただし、参考資料では1シーンあたり平均**43.62個**と記載されている。いずれにせよ、既存の代表的データセット **3D-Future** の平均 5.09個と比較すると**約9倍の密度**だ。5個のオブジェクトが置かれた部屋と、44個のオブジェクトが置かれた部屋では、レイアウトの複雑さが桁違いに異なる。

アセットクラス数（オブジェクトの種類数）も、3D-Future の 34種に対し IntelliScene は **500種** -- 約15倍の多様性がある。

![3D Scene Dataset](source/images/IMG_4743.jpg)

#### データ品質基準（人間のアーティストが設定）

Lockliu がデータ品質への拘りを語った場面は印象的だった。

> "Everything must not only look good but also have clear story. You can see the style, storytelling."
>（すべてが見た目に良いだけでなく、明確なストーリーを持たなければならない。スタイルとストーリーテリングが見えなければならない）

品質基準は以下の4つだ。

- **Has Story（物語がある）**: オブジェクト間に機能的な関係がある。例: 茶テーブルの上にティーカップ → 「誰かがお茶を飲んでいた」
- **Has Aesthetic Value（美的価値がある）**: 構図・スケール・相対位置が合理的
- **Avoid Over-occlusion（過度な遮蔽を回避）**: 体積の1/2以上が他のオブジェクトに隠れるような配置を避ける
- **Clear Description（明確な記述）**: 各シーンに背景設定・機能・具体的なオブジェクト情報をテキストで付与

![Dataset Standard](source/images/IMG_4742.jpg)

### Step 2: ガイダンス画像の生成（FLUX + DreamBooth ファインチューニング）

テキストから生成する「お手本画像」の中に登場するオブジェクトを、3Dモデルライブラリの実際のアセットに近づけること。これが Step 2 の目標だ。

> "We use fine-tuning the image generation model like FLUX to turn the guidance image to match our asset library."
>（FLUXのような画像生成モデルをファインチューニングして、ガイダンス画像が我々のアセットライブラリと一致するようにする）

手法は **DreamBooth** の応用だ。DreamBooth は本来「特定の犬の写真を数枚学習させると、その犬を様々なポーズで生成できる」ような個別学習技術。これを拡張し、ライブラリ内の3Dモデル群をまとめて学習させた。

技術的ポイント:
1. **Special Token [V]**: ライブラリ内の各モデルカテゴリに固有の識別子を割り当て
2. **Layered Optimization Strategy**: オーバーフィッティング防止のため、まず低解像度で軽量に学習（LoRA＝少数パラメータのみ更新する効率的手法）
3. **Combined Loss Function**: 生成画像がお手本に近いか（L2ノルム）と、元モデルの多様性を維持しているか（Prior preservation loss）の両方を評価

> "This fine-tuning can learn from many asset types. It doesn't overfit the training set and still generates diverse scenes."
>（このファインチューニングは多様なアセットタイプから学習できる。訓練セットにオーバーフィットせず、なおかつ多様なシーンを生成できる）

**結果**: 生成画像に含まれるメインオブジェクトが、ライブラリ内のモデルTop5候補に含まれる確率 **88.55%** を達成。かつ、多様なシーン生成能力も維持。

> "The chart on the right shows a clear improvement."
>（右のチャートは明確な改善を示している）

![DreamBooth Fine-tuning](source/images/IMG_4744.jpg)

### Step 3: Scene Parsing（お手本画像の解析）

生成されたガイダンス画像から「何がどこにあるか」を読み取る工程。3段階で処理する。

> "Starting with detecting and sensing the main objects, we combine several foundation models — detection and segmentation. So we add depth in the beginning step."
>（主要オブジェクトの検出・認識から始め、複数の基盤モデルを組み合わせる。検出とセグメンテーション。さらに最初のステップで奥行き情報も追加する）

1. **VLM（Vision Language Model＝画像理解AI）** で画像全体を分析し、オブジェクトリストを生成
2. **Grounding DINO v2（物体検出モデル）** で各オブジェクトの位置（バウンディングボックス）を特定
3. **SAM（Segment Anything Model＝Meta社が開発した万能セグメンテーションモデル）** で各オブジェクトの正確な輪郭を切り出す

![Scene Parsing](source/images/IMG_4749.jpg)

#### Visual Scene Graph（視覚的シーングラフ）の構築

個々のオブジェクトを認識した後、オブジェクト間の**空間的な関係**を構造化する。

> "After understanding individual objects, we also need to understand how they relate to each other. We do this by building a scene graph. This is building on our 1.0 text-based scene graph work, but now it's driven by the image."
>（個々のオブジェクトを理解した後、それらがどう関係しているかも理解する必要がある。それをシーングラフの構築で行う。これは1.0のテキストベースのシーングラフの延長だが、今回は画像によって駆動される）

- **Support（支持関係）**: 「テーブルがカップを支えている」
- **Against Wall（壁接触関係）**: 「タンスが壁に接している」

![Scene Graph](source/images/IMG_4750.jpg)

### Step 4: 3Dモデル検索（お手本に最も似たアセットを探す）

ガイダンス画像の各オブジェクトに対して、3Dモデルライブラリから**見た目とサイズが最も近い**アセットを検索する。

> "The key goal is high similarity between the objects in the image and our library assets. Even if the objects in the image are not an exact match, as long as the meaning and the overall shape are similar, we can find really nice matches."
>（キーゴールは、画像内のオブジェクトとライブラリアセットの高い類似性だ。画像のオブジェクトが完全に一致しなくても、意味と全体的な形状が似ていれば、本当に良いマッチを見つけられる）

> "If the library doesn't contain the full match, the result can still be interesting."
>（ライブラリに完全な一致がなくても、結果は面白いものになりうる）

- **3つの情報を統合**: カテゴリ + 外観（CLIP特徴量による類似度計算）+ サイズ
- セグメンテーションで切り出したオブジェクト画像と、ライブラリのモデルレンダリング画像を比較

![3D Model Retrieval](source/images/IMG_4751.jpg)

### Step 5: Category-Level Object Pose Estimation（2D画像から3Dの姿勢を推定）

ここがIntelliScene 2.0の技術的に最も難しい部分だ。2Dの画像に写っているオブジェクトが、3D空間でどの位置・どの角度にあるかを推定する。

> "The hardest part is measuring the 3D pose from a 2D image."
>（最も難しい部分は、2D画像から3Dの姿勢を計測することだ）

#### Stage 1: Coarse Estimation（粗い推定）

3Dモデルを**162の異なる視点**からレンダリングし、DINOv2（Meta社の自己教師あり学習の視覚AI）で特徴を抽出。ガイダンス画像のオブジェクトと162視点それぞれの特徴を比較し、候補を絞り込む。

> "We run their many standard models of our 3D model from different views. Use cross-feature matching to compare the real object in the image with the candidate views."
>（3Dモデルを異なる視点からレンダリング。クロス特徴マッチングを使って、画像内の実オブジェクトと候補視点を比較する）

![Pose Estimation - Coarse](source/images/IMG_4753.jpg)

#### Stage 2: Fine Selection（精密な選択）

候補視点の中から最適な1つを選ぶために、**RANSAC + ホモグラフィ行列**を使用する。

- **RANSAC**: ノイズを含むデータから外れ値を除外しつつ正しいモデルを推定する手法
- **ホモグラフィ行列**: 2つの画像間の「平面の変換関係」を表す数学的な行列

> "We use the objects' orientation — the main face, the front direction. We combine visual features and geometric features."
>（オブジェクトの向き――メインの面、正面の方向を使う。視覚特徴と幾何特徴を組み合わせる）

![Pose Estimation - Fine](source/images/IMG_4754.jpg)

#### Stage 3: Geometric Information Augmentation（幾何情報による補強）

> "Visual features are robust but coarse. Geometric features are accurate but fragile. We combine both."
>（視覚特徴は頑健だが粗い。幾何特徴は正確だが脆い。両方を組み合わせる）

3Dモデルの点群データから生成したOBB（回転付き直方体）の4方向の垂直面を幾何学的な参照として使用し、視覚ベースと幾何ベースの推定を**適応的に切り替え**る。

![Pose Estimation - Geometric](source/images/IMG_4755.jpg)

### Step 6: グローバル最適化（全オブジェクトを矛盾なく配置する）

ここまでのステップで各オブジェクトの位置・角度を個別に推定したが、個別の推定結果を単純に合成すると、オブジェクト同士が重なったり浮いたりする。

> "In the final step of global optimization, we use constraints from earlier — traditionally like 'no overlap', 'must be supported', 'must touch the wall'. Under those constraints, we define how much to move each object. The final thing is to match the guidance image. We use a global optimization method like simulated annealing to search for the best overall solution."
>（グローバル最適化の最終ステップでは、先に定義した制約を使う。「重ならない」「支持される」「壁に接する」。これらの制約のもとで、各オブジェクトの移動量を定義する。最終目標はガイダンス画像との一致だ。焼きなまし法のようなグローバル最適化手法で、最適な全体解を探索する）

- **焼きなまし法（Simulated Annealing）**: 金属加工の焼きなましから着想を得た最適化アルゴリズム。最初は大胆に配置を動かし、徐々に微調整に切り替える
- 最後に**Physics Simulation（物理シミュレーション）**を実行し、物理法則に反する配置を解消

> "Besides constraints in optimization, we also add physics like gravity. Objects will naturally stay on support or settle in a stable way. Which people relate to as physically reasonable in the final scene."
>（最適化の制約に加えて、重力のような物理法則も追加する。オブジェクトは自然に支持面に留まるか安定した状態に落ち着く。人々が最終シーンで「物理的に合理的だ」と感じるように）

![Layout Optimization](source/images/IMG_4756.jpg)

![Optimization Results](source/images/IMG_4757.jpg)

### 実験結果: 本当に使えるのか？

#### 人間評価（100名の美術専攻学生による評価）

> "We ran 100 of the art students to evaluate. We also added a particular game art engineer outside the company to review."
>（100名の美術学生に評価してもらった。さらに社外のゲームアートエンジニアにもレビューを依頼した）

既存の最先端手法（SOTA＝State Of The Art）3つとの比較:

| 比較対象 | 合理性＆現実性（IntelliScene選好率） | 美的品質（IntelliScene選好率） |
|:---|:---|:---|
| vs **DiffuScene** | **75-83%** | **75-86%** |
| vs **Holodeck** | **74-80%** | **68-83%** |
| vs **LayoutGPT** | **79-86%** | **84%** |

全比較対象・全指標で過半数を大幅に超える圧勝だ。

![Human Evaluation](source/images/IMG_4758.jpg)

#### 忠実度評価

| 指標 | スコア | 意味 |
|:---|:---|:---|
| **Main Object Recovery Rate** | **90.62%** | お手本画像のメインオブジェクトの約91%を3Dシーンで再現 |
| **Category Retention Rate** | **96.07%** | オブジェクトのカテゴリを約96%正しく維持 |
| **Scene Graph Relation Identification** | **91.95%** | 「AがBの上に載っている」等の関係を約92%正しく認識 |
| **GPT-4o Layout Similarity Score** | **7.75/10** | GPT-4oにお手本と出力を比較させた類似度 |

![Fidelity Evaluation](source/images/IMG_4759.jpg)

### Agent Demo: Imaginarium

IntelliScene 2.0 の技術は、**SIGGRAPH Asia 2025**（コンピュータグラフィックス分野の最高峰学会の一つ）に論文「Imaginarium: Vision-guided High-Quality 3D Scene Layout Generation」として**ACM Transactions on Graphics**（CG分野の最権威ジャーナル）に採択されている。

> "The paper was also published at SIGGRAPH Asia."
>（論文はSIGGRAPH Asiaでも発表された）

> "We also tested the system in real-time game projects. Here's an efficiency test in our game engine."
>（実際のゲームプロジェクトでもシステムをテストした。これは我々のゲームエンジンでの効率テストだ）

デモとして、Gradio（機械学習モデルのWebデモを簡単に作れるツール）で構築されたインターフェースが紹介された。9ステップの処理が可視化される:

1. Text Input（テキスト入力）
2. Image Generation（ガイダンス画像生成）
3. Detection & Segmentation（物体検出＆輪郭抽出）
4. Object Depth（奥行き推定）
5. Wall and Floor Detection（壁・床の検出）
6. Scene Graph Generate（関係グラフ構築）
7. 3D Retrieval（3Dモデル検索）
8. Pose Estimation（姿勢推定）
9. Blender Layout（3Dシーン出力）

![Agent Demo](source/images/IMG_4760.jpg)

---

## 4. Future Work / Possibilities

### Summary: 1.0から2.0への進化

Lockliu はまとめのスライドで、2バージョンの進化を簡潔に振り返った。

| | Why（なぜ必要か） | How（どう解決したか） | What（何が残ったか） |
|:---|:---|:---|:---|
| **1.0** | シーン構築は時間・労力・人材に依存 | テキストAIの「遅い思考」で問題を分解 | 空間/幾何の精度に限界 |
| **2.0** | テキストだけでは限界 | 画像生成＋画像認識＋数学的最適化の融合 | 高品質・高精度を実現、SOTA超え |

> "The target: speed up things in traditional scene building and use AI to boost the efficiency and the creativity. Use AI and agents and the slow thinking."
>（目標は、伝統的なシーン構築を加速し、AIで効率と創造性を向上させること。AIとエージェントと遅い思考を使って）

> "Continue collecting high-quality reasoning data is the key for success."
>（高品質な推論データの継続的収集が成功の鍵だ）

**Core Value（核心的価値）**: Vision + Language の融合が有効であることを実証した。そして**高品質データの継続的蓄積が、他社が簡単に真似できない産業参入障壁**を構築する。

![Summary](source/images/IMG_4761.jpg)

### IntelliScene 3.0（予告）

セッション終盤、Lockliu は聴衆の期待を超える内容を披露した。IntelliScene 3.0 の構想だ。

> "We propose a hierarchical placement system. The top layer is the virtual agent, the design brain. The bottom layer is the placement domain-specific model."
>（階層的配置システムを提案する。トップレイヤーは仮想エージェント、デザインの「脳」だ。ボトムレイヤーは配置に特化したドメインモデルだ）

#### 2つの柱: 「脳」と「小脳」

人間の脳に例えた2つのシステムを持つ:

| | Art Design Brain（芸術的デザインの「脳」） | Positional Intuition Cerebellum（配置直感の「小脳」） |
|:---|:---|:---|
| **役割** | VLM Agent Swarm（画像理解AIの群れ） | 配置特化型の大規模モデル |
| **担当** | シーン全体の観察、ストーリーの理解、タスクの分解、配置エリアの選択 | 6自由度の姿勢生成（X,Y,Z位置 + 3軸回転）、衝突回避、物理法則の理解 |
| **知識基盤** | シーンデザイナーの熟練知識をSOP化したナレッジベース | 3Dゲームエンジン向けのエンドツーエンド基盤モデル |
| **インタラクション** | -- | **MCP interactions -- Claude Code Style** |

> "The top layer analyses the scene, gathers the story goal, brings the job into steps, chooses area to work on. The bottom layer — placement domain-specific model — focuses on physics and spatial execution."
>（トップレイヤーがシーンを分析し、ストーリー目標を把握し、ジョブをステップに分解し、作業エリアを選択する。ボトムレイヤー――配置特化ドメインモデル――が物理と空間実行に専念する）

3.0 では、3Dゲームエンジン内でのアセット配置を **MCP（Model Context Protocol）インタラクション**――Claude Code が IDE やターミナルとやりとりするのと同じプロトコル――で行うエンドツーエンド基盤モデルが構想されている。AIがゲームエンジンのAPIを直接呼び出してアセットを配置する、という世界観だ。

![IntelliScene 3.0](source/images/IMG_4764.jpg)

#### 既存手法の課題と3.0の技術的貢献

Lockliu は既存手法（LayoutGPT、Holodeck、LayoutVLM等）の3つの課題を指摘した。

> "First, most methods can only take text or image as input. They cannot see the 3D scene directly. Second, the output is referral only — position, orientation, or semantic level. It does not include full geometric information. Third, the model does not truly understand spatial relationships and the physical interactions between objects."
>（第一に、ほとんどの手法はテキストか画像しか入力として受け取れない。3Dシーンを直接見ることができない。第二に、出力は参照レベル――位置、向き、意味レベルのみ。完全な幾何情報を含まない。第三に、モデルはオブジェクト間の空間的関係と物理的相互作用を真に理解していない）

**3.0の3つの技術的貢献**:
1. **3D-aware input encoding**: シーンとアセットの**点群データ（Point Cloud）**を直接モデルに入力。2D画像ではなく3Dデータを直接扱う
2. **Coarse-to-fine pose prediction（粗→精の段階的予測）**: まず大まかな位置を決め、次に精密に調整する2段階方式
3. **Spatial reasoning and planning（空間推論＆計画）**: 高品質データで訓練することで空間的な推論能力を獲得

> "So the model can see the details of each asset in the room. It speaks the language of geometry and space. The prediction greatly improves final placement accuracy."
>（モデルは部屋の中の各アセットの詳細を見ることができる。幾何と空間の言語を話す。予測は最終配置の精度を大幅に向上させる）

![3.0 Contributions](source/images/IMG_4765.jpg)

#### 3.0 の比較結果

> "First, our placement model produces more precise and realistic results. Second, our method is more stable in complex rooms — irregular shapes, obstacles. Third, our method is much faster. When the number of objects grows from five to twenty, our time stays around 250 seconds, and the other methods become much slower. In comparison, 35 objects take 825 seconds for competitors."
>（第一に、我々の配置モデルはより精密でリアリスティックな結果を生成する。第二に、複雑な部屋――不規則な形状、障害物がある空間――でもより安定している。第三に、はるかに高速だ。オブジェクト数が5から20に増えても、我々の処理時間は約250秒で推移するが、他の手法ははるかに遅くなる。35オブジェクトでは競合手法は825秒かかる）

![3.0 Comparison](source/images/IMG_4766.jpg)

![3.0 Examples](source/images/IMG_4767.jpg)

---

## Tencent AI Booth とデモ体験

セッションの締めくくりで、Lockliu は GDC 会場での体験型デモに言及した。

> "Our booth is called AISHOP — Artificial Intelligence Testing Workshop. You are welcome to move forward to our booth, just in the course, and try our demo. We stress the demonstrations that are available."
>（我々のブースは AISHOP と呼ばれる。AIテスティングワークショップだ。ぜひブースに来てデモを体験してほしい。利用可能なデモンストレーションを重視している）

Tencent Games AI は GDC 2026 の展示フロアに AISHOP と名付けたブースを設置し、IntelliScene の実機デモを公開していた。来場者がテキストを入力すると、リアルタイムでシーンが構築されていく様子を体験できる。

---

## まとめ

| バージョン | アプローチ | 入力 | 強み | 弱み |
|:---|:---|:---|:---|:---|
| **1.0** | テキストAIチーム + 数学的最適化 | テキストのみ | 推論ベースの配置、訓練データの蓄積 | 幾何精度が低い、AIの幻覚 |
| **2.0** | 画像生成AI + 画像認識AI + 3D数学 | テキスト＋画像 | 高忠実度、美的品質でSOTA超え | 処理が多段で時間がかかる |
| **3.0** | 一気通貫の配置特化型大規模モデル + MCP | 3D点群＋テキスト | 高速・高精度・頑健 | 開発中 |

### ゲーム開発者へのインパクト

1. **大規模マップの非コアエリア**のシーン装飾を大幅に自動化できる可能性。人間のデザイナーはコアエリアの創造的作業に集中できる

2. **Vision + Language + Geometry の融合**――テキストAI単体では不可能だったシーン構築が、画像と3D幾何学の力で実用レベルに到達しつつある

3. **高品質データの蓄積**が競争優位を生む。アルゴリズムは論文で公開されても、500カテゴリ・2,042モデル・147シーンのデータと、それに付随する詳細なアノテーションは簡単に真似できない。Lockliu が "We are still spending" と言ったように、データセット構築は今も進行中だ

4. **MCP（Model Context Protocol）スタイルのインタラクション**――AIがゲームエンジンのAPIを直接呼び出す方式――がゲーム開発AIの標準的なインターフェースになる可能性。Claude Code がターミナルを操作するように、IntelliScene 3.0 が Unreal Engine や Unity のAPIを操作する未来が示された

5. 1.0→2.0→3.0の進化は**約2年のスパン**。この分野の進歩速度は極めて速い

### 日本のゲーム開発における示唆

同じ GDC 2026 で聴講した Kate Edwards のカルチャライゼーションセッションでは、「インディ・ジョーンズ」のシーンに配置されたアーティファクト一つひとつが地質学的・考古学的に考証された話が語られた。あのレベルの「意図的な配置」はAIには代替し難い。一方で、IntelliScene が狙う「非コアエリアの大量配置」は人間がやるべき仕事ではない。

つまり、**「何を置くか」は人間が決め、「どう並べるか」はAIが実行する**。この分業が、次世代のゲーム開発の姿だろう。日本のスタジオは欧米に比べてチーム規模が小さいことが多い。だからこそ、IntelliScene のような技術で「人手のかかる非コア作業」を自動化する恩恵は大きい。

---

## 参考リンク

- [GDC 2026 Session Page](https://schedule.gdconf.com/session/intelliscene-multi-agent-for-reasoning-driven-game-scene-layout-presented-by-tencent-games/917891)
- [Tencent Games GDC 2026 Press Release](https://www.prnewswire.com/news-releases/tencent-games-showcases-tech-advancements-shaping-future-player-experience-at-gdc-2026-302707052.html)

---

## おわりに

最後までお読みいただきありがとうございます。GDC 2026 の他のセッションレポートも順次公開していますので、ぜひフォローしてお待ちください。

**dsgarage Games** | GDC 2026 現地レポート
