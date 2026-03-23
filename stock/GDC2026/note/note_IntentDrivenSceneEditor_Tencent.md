# GDC 2026: Intent-Driven Game Scene Editor — 意図駆動型ゲームシーンエディタとAIゲームエンジンの未来

「AIにゲームシーンを作らせたい」――そう考えたとき、多くの開発者が最初に試すのは生成AIへのプロンプト入力だろう。しかし現実には、「城を置いて」と指示しても、物理法則を無視したオブジェクトが空中に浮かび、ライティングは破綻し、ゲームとして成立しない出力が返ってくる。

GDC 2026 の AI Summit で、Tencent Games（テンセント・ゲームズ――世界最大のゲーム企業テンセントのゲーム開発部門。『王者栄耀』『PUBG Mobile』等を擁し、Riot Games や Supercell への出資でも知られる）が発表した「Intent-Driven Game Scene Editor（意図駆動型ゲームシーンエディタ）」は、この根本問題に正面から切り込むセッションだった。スピーカーは序盤から大胆な問いを投げかけた――**従来のゲームエンジンはAI時代に居場所があるのか？** そしてその回答として、シンボリズム（Symbolism＝記号主義）とコネクショニズム（Connectionism＝結合主義）という AI の二大潮流を対比し、両者の融合によって「開発者の意図をゲームシーンに変換する」新アーキテクチャを提示した。

本記事では、会場で録音したスピーチの内容を中心に、撮影したスライド写真9枚を補助として、セッションの全体像を再構成する。トランスクリプトは DJI Mic 2 で収録し Whisper で文字起こしした素材であり、会場のノイズや英語音声認識の限界から一部不明瞭な箇所があるが、スライドの記載内容と突き合わせることで発言の意図を正確に復元している。AI 時代のゲーム開発パイプラインを考える全ての開発者にとって、設計判断の指針となる内容だ。

---

## セッション概要

| 項目 | 内容 |
|:---|:---|
| **セッション名** | Intent-Driven Game Scene Editor |
| **サブタイトル** | Voice Driven Procedural Game Scene Generation（音声駆動によるプロシージャルゲームシーン生成） |
| **発表者** | Tencent Games |
| **トラック** | AI Summit（GDCの中でもAI技術に特化した専門トラック。ゲームAIの研究者・実務者が集まり、最先端の事例を共有する場として業界内で高い評価を受けている） |
| **カテゴリ** | AI / Machine Learning / Game Engine |
| **開催** | GDC 2026（2026年3月10日、サンフランシスコ Moscone Center） |
| **キーワード** | Symbolic AI（記号主義AI）, Connectionist AI（結合主義AI）, PCG（Procedural Content Generation＝手続き型コンテンツ生成）, AI Agent（AIエージェント＝自律的にタスクを実行するAIシステム）, Game Engine |

---

### この記事で読めること

- **問題提起: ゲームエンジンはAI時代に居場所があるのか**
- **Symbolism vs Connectionism: AIの二大潮流**
- **ブラックボックス問題: なぜAIの出力は信頼できないのか**
- **エネルギー効率: 10倍から100倍の差**
- **継続的学習と破壊的忘却**
- **進化の弧: コネクショニズムからシンボリズムへ**
- **知能の指数関数的爆発: カーボンからシリコンへ**
- ...ほか全14セクション

> **本記事のボリューム**: 約22,328文字 / スライド画像9枚
> スピーカーのトランスクリプト（発言の文字起こし）を原文・日本語訳つきで完全収録しています。

---

<!-- ===== ここから有料エリア（Note エディタで有料ラインを設定） ===== -->

## 1. 問題提起: ゲームエンジンはAI時代に居場所があるのか

セッションは、AI が急速にゲーム開発に浸透している現状の整理から始まった。スピーカーは会場にこう問いかけた。

> "Did you ever stop and ask ourselves... [whether] traditional game engines still have a place in this new [AI] game?"
>
> （立ち止まって自問したことはありますか――従来のゲームエンジンは、このAIの新しいゲームにおいて居場所があるのか、と）

この問いは修辞的なものではなく、セッション全体を貫く主題だった。現在、画像生成・テキスト生成・動画生成と、AIは次々とクリエイティブ領域に参入している。ゲーム開発もその例外ではなく、プロンプト一つでゲームシーンを生成しようとする試みが学術・産業の両面で進んでいる。しかしスピーカーは、その「純粋なAIアプローチ」には根本的な限界があると主張した。

スピーカーの発言を要約すると、現状のAIベースのシーン生成には以下の問題がある:

1. **プロンプト情報の不足** -- テキストだけでは、ゲームシーンに必要な物理法則・ライティング・コリジョン等の情報を十分に伝えられない
2. **ロバスト性の欠如** -- 同じプロンプトでも出力が安定せず、制御が困難
3. **ブラックボックス性** -- 出力の根拠が不透明で、問題発生時に介入できない
4. **計算リソースの巨大さ** -- 大規模モデルの推論には膨大なGPUリソースが必要

これらの問題を理解するために、スピーカーはAIの歴史における二つの大きな潮流――シンボリズムとコネクショニズム――の比較に踏み込んだ。

---

## 2. Symbolism vs Connectionism: AIの二大潮流

### 2.1 二つのパラダイムの定義

スピーカーは、ゲーム開発における AI のアプローチを二つのパラダイムに整理した。

> "On one side, you have a traditional game engine which is about symbolism. It's like... a program... [with] hard-coded rules and [a] fresh module. You are basically teaching the computer [a] recipe [of] what to do."
>
> （一方には、シンボリズムに基づく従来のゲームエンジンがある。ハードコードされたルールとモジュールを持ち、基本的にはコンピュータに「何をすべきか」のレシピを教えるものだ）

> "On the other side, you [have] AI... which is all about [connectionism]. Instead, [it finds] patterns from [massive] data... generating patterns and rules [that] can be used [on its] own."
>
> （他方には、コネクショニズムに基づくAIがある。大量のデータからパターンを見出し、自らルールを生成して使う）

つまり:

- **シンボリズム（Symbolic AI / 記号主義AI）**: 人間がルールを明示的にプログラムする。ゲームエンジンの物理演算、シーングラフ、コリジョン検出がこれに該当する。入力は「命令（Instructions）」であり、出力は決定論的。
- **コネクショニズム（Connectionist AI / 結合主義AI）**: データからパターンを学習する。LLM（大規模言語モデル）、画像生成AI、ニューラルネットワーク全般がこれに該当する。入力は「データ（Patterns / Data）」であり、出力は確率的。

スピーカーはこの対比を「ゲーム開発のルールにおけるブレイクアウト（breaking out the game in the rules of game development）」と表現し、両者の長所・短所を4つの軸で比較した。

### 2.2 四つの比較軸（Four Key Advantages）

会場に表示されたスライドは、Symbolic AI と Connectionist AI を4つの軸で対比するものだった。

![Four Key Advantages: Symbolic AI vs Connectionist AI](source/images/IMG_4774.jpg)

*スライド写真: 「FOUR KEY ADVANTAGES」。左側が Symbolic AI（Traditional Game Engine）、右側が Connectionist AI（AI Game Engine）。4つの比較軸が視覚的に対比されている。*

スピーカーはまず Symbolic AI の利点を説明した。

> "Symbolism... it's [explainable]. The rules are transparent. So we know... [exactly] how it works."
>
> （シンボリズムは説明可能だ。ルールは透明で、どう動くか正確にわかる）

以下がスライドに記載された4つの軸の要約である:

| 比較軸 | Symbolic AI（従来のゲームエンジン） | Connectionist AI（AIゲームエンジン） |
|:---|:---|:---|
| **説明可能性** | Explainable（透明なロジックとルール） | Black Box（不透明で解釈不能） |
| **データとロジックの関係** | Data & Logic Decoupled（分離） | Weight Coupled（データとロジックが絡み合う） |
| **エネルギー効率** | High Energy Efficiency（高効率） | Energy Intensive（高い計算・電力消費） |
| **継続的学習** | Continual Learning（インクリメンタルな知識蓄積） | Catastrophic Forgetting（新しい学習が古い知識を上書き） |

この4軸は、セッション後半の「なぜゲームエンジンをAIエージェントツールとして使うべきか」という結論に直結する重要な論点である。以下、各軸について詳しく見ていく。

---

## 3. ブラックボックス問題: なぜAIの出力は信頼できないのか

### 3.1 ニューラルネットワークの不透明性

4つの比較軸の中で、スピーカーが最も時間を割いて説明したのが「ブラックボックス問題」だった。

![Black Box: Trained Neural Network](source/images/IMG_4775.jpg)

*スライド写真: 「TRAINED NEURAL NETWORK (BLACK BOX)」。Input Data（Images, Text）がニューラルネットワークに入り、Output Predictions（Labels, Text）が出力される。右側に大きく「? NO EXPLICIT LOGIC / UNINTERPRETABLE」と表示。下部には Transformer アーキテクチャの図。*

スピーカーの説明を補足すると、このスライドはコネクショニストAIの本質的な構造を示している:

1. **入力**: 画像やテキストなどの生データ
2. **処理**: 学習済みニューラルネットワーク（Trained Neural Network）がデータを処理
3. **出力**: ラベルやテキストなどの予測結果

問題は、この「処理」の部分が完全なブラックボックスであることだ。

> "There's no [explicit] logic inside the [model]. We don't know how [it] provide[s]... this decision."
>
> （モデルの内部には明示的なロジックがない。どうやってその判断を導いたのか、我々にはわからない）

ゲーム開発において、これは致命的な問題となる。例えば、AIがシーンに配置したオブジェクトが物理的に不自然な位置にある場合、なぜそうなったのかを特定する手段がない。デバッグが不可能なシステムは、プロダクション環境では使えない。

### 3.2 データとロジックの分離（Decoupling of Data and Logic）

続くスライドでは、ニューラルネットワークとシンボリックAIにおける「データとロジックの関係」がさらに詳しく比較された。

![Decoupling of Data and Logic](source/images/IMG_4776.jpg)

*スライド写真: 「Decoupling of Data and Logic」。左側が「Neural Networks: Integrated Data & Logic」で、Weight Parameters にロジックとデータが統合された密結合構造を示す。右側が「Symbolism (Symbolic AI): Decoupled Data & Logic」で、Logical Rules（IF A THEN B 形式）と Data Features（構造化データ）が分離されたモジュラー構造を示す。*

スライド下部には以下の要約テキストが記載されていた:

> **Separation of data and logic**: The logical rules and data features of neural networks are both integrated into weight parameters, which are difficult to separate effectively. In contrast, symbolism can achieve better decoupling of logic and data. It allows more adjustable parameters to be exposed in the later stage, or local logic [to] be modified without affecting the overall stability of the system at all.
>
> （データとロジックの分離: ニューラルネットワークでは、論理規則とデータ特徴量がともに重みパラメータに統合されており、効果的に分離することが困難である。対照的に、シンボリズムはロジックとデータのより良い分離を実現できる。後段でより多くの調整可能パラメータを公開でき、システム全体の安定性に影響を与えることなくローカルなロジックを修正できる）

左側のニューラルネットワーク図では、Weight Parameters（WG）が網の目のように密に接続され、「Holistic Optimization（全体最適化）」「Difficult to Separate Effectively（効果的な分離が困難）」「Tightly coupled, opaque structures（密結合で不透明な構造）」と注記されている。

右側のシンボリックAI図では、「IF A THEN B → RULE X」のような明示的ルール、Knowledge Graph、構造化された Data Features が「Explicit Rules（明示的ルール）」「Better Decoupling & Modularity（優れた分離性とモジュール性）」「Independent Inputs（独立した入力）」「Adjustable Parameters & Local Modification（調整可能なパラメータとローカル修正）」「Modular, transparent structures（モジュラーで透明な構造）」として整理されている。

これはゲーム開発の実務において極めて重要なポイントだ。ゲームエンジンでは、物理演算のパラメータ（重力、摩擦係数など）を個別に調整できる。一方、ニューラルネットワークでは「城の重力だけを変更する」といったピンポイントの調整が原理的に困難である。

---

## 4. エネルギー効率: 10倍から100倍の差

### 4.1 論理ゲート vs ニューラルネットワーク

エネルギー効率の比較は、セッションの中でも特にインパクトのあるスライドで示された。

![High Energy Efficiency](source/images/IMG_4777.jpg)

*スライド写真: 「High Energy Efficiency」。左側が SYMBOLISM（Energy Efficient）で、XNOR 論理ゲート1-2個による最小限の計算を示す。右側が CONNECTIONISM（Energy Intensive）で、4つの隠れユニットを持つ2層パーセプトロンによる大量の乗算・加算を示す。下部に「ENERGY GAP: 10x - 100x」のバーグラフ。*

スピーカーはこの差を具体的に説明した。

> "We use a task [like] simple rules of games, [like an] XNOR game. [With symbolism,] we use very small energy."
>
> （シンプルなゲームルール、例えばXNORゲームのようなタスクを使う。シンボリズムでは非常に少ないエネルギーで済む）

> "On the left, we have [connectionism]. The AI [must process] even [a] simple [task]... [through] dozens of multiplications and additions in [an iterative] process. This requires massive computation. And as a result, [it uses] 10 to 100 [times more] energy."
>
> （コネクショニズムでは、AIは単純なタスクでさえも数十回の乗算と加算を繰り返すプロセスで処理しなければならない。これには大量の計算が必要で、結果として10倍から100倍のエネルギーを消費する）

スライドの具体的な内容:

- **シンボリズム側**: 入力 A, B に対して XNOR 論理ゲート 1-2 個で Output（0 or 1）を算出。「Minimal computation, direct solution.（最小限の計算、直接的な解法）」
- **コネクショニズム側**: 同じタスクに対して「Dozens of Multiplications & Additions. Two-layer perceptron with 4 hidden units.（数十回の乗算と加算。4つの隠れユニットを持つ2層パーセプトロン）」が必要。「Massive computation, iterative process. ~10-100x Energy Consumption.（大量の計算、反復的プロセス。約10-100倍のエネルギー消費）」

下部のバーグラフでは、Symbolism のバーが極めて小さく、Connectionism のバーが 0 から 200x まで大きく伸びている。「ENERGY GAP: 10x - 100x (Approximately 1-2 orders of magnitude difference)」と明記されている。

### 4.2 ゲーム開発への含意

この10-100倍のエネルギー差は、単なるコスト問題ではない。リアルタイム処理が求められるゲームにおいて、フレームごとの計算量が10-100倍になることは、実用的には「使えない」ことを意味する。

スピーカーは、スケーラブルなアプリケーションにおいてこの差が決定的になると強調した。ゲームエンジンが物理演算を数個の論理ゲートで処理できる問題に対して、ニューラルネットワークは数百のノードと重み計算を経由しなければならない。これはゲームのフレームレートとリソース消費に直接影響する。

ただし後述するように、全てのケースでシンボリズムが優位というわけではなく、PBR（物理ベースレンダリング）のような複雑な方程式では、ニューラルネットワークによる近似の方が計算コストが低くなる場合もある。この点はセッション終盤のスライドで明確に示された。

---

## 5. 継続的学習と破壊的忘却

### 5.1 知識の蓄積 vs 知識の上書き

4つの比較軸の最後は、「継続的学習（Continual Learning）」と「破壊的忘却（Catastrophic Forgetting）」の対比だった。

スピーカーの発言から:

> "For example, [one of] the most important difference[s]... is sustainable, continuous learning. [In symbolism,] when [you] add a new [rule], [you can add it] from [outside]. It's [a] real [incremental] and more practical [approach]."
>
> （例えば、最も重要な違いの一つが持続可能な継続的学習だ。シンボリズムでは、新しいルールを追加する際、外部から追加できる。これは真にインクリメンタルで、より実用的なアプローチだ）

ゲームエンジンの物理シミュレーションを例にとると:

- **Symbolic AI（ゲームエンジン）**: 新しい物理マテリアル（例: 水、布、氷）を追加しても、既存のルールには影響しない。ナレッジベースはインクリメンタルに成長する。
- **Connectionist AI（ニューラルネットワーク）**: 新しいデータで再学習すると、以前学習した知識が上書きされる可能性がある（Catastrophic Forgetting＝破壊的忘却）。

スライド（IMG_4774）では、Symbolic AI 側に「Continual Learning (Incremental Knowledge Base)」、Connectionist AI 側に「Catastrophic Forgetting (New Overwrites Old)」と明記されていた。

これはゲーム開発のパイプラインにおいて、長期的なメンテナンス性と拡張性に直結する。プロジェクトが進むにつれてルールが増えていくゲーム開発では、新しい要素を追加するたびに既存の挙動が壊れるリスクは許容できない。

---

## 6. 進化の弧: コネクショニズムからシンボリズムへ

### 6.1 生物進化とのアナロジー

セッション中盤で最も哲学的な議論が展開された。スピーカーは、生物の知能の進化とAIの発展を重ね合わせる壮大な図を示した。

![The Evolutionary Arc: From Connectionism to Symbolism](source/images/IMG_4778.jpg)

*スライド写真: 「THE EVOLUTIONARY ARC: FROM CONNECTIONISM TO SYMBOLISM」。左側に生物進化（Biological Evolution - Life's Path）、右側にモダンAI開発（Modern AI Development - The Analogy）。中央に「THE TRANSITION: From Implicit Pattern Matching to Explicit Rule Generation（暗黙的パターンマッチングから明示的ルール生成への移行）」。下部に「COEXISTENCE, NOT REPLACEMENT: Neither is "better". The symbolic structures rely on the connectionist substrate for grounding and adaptability.（置き換えではなく共存: どちらが「より良い」わけでもない。記号的構造は、接地と適応のためにコネクショニストの基盤に依存する）」*

> "From the history of [intelligence's] evolution, we can see... [a] transition from [connectionism] to symbolism."
>
> （知能の進化の歴史から、コネクショニズムからシンボリズムへの移行が見える）

スピーカーが提示した進化の流れ:

**生物の進化（左側）**:
1. コネクショニスト基盤 -- 生物のニューロンネットワーク（直感、適応）
2. 暗黙的パターンマッチング -- 環境からのパターン認識
3. 内的ロジックの誘導 -- 抽象的な原理の発見
4. シンボリック表現 -- 言語、文化、数学的原理

**モダンAI開発（右側）**:
1. LLM（大規模言語モデル） -- コネクショニスト学習（大規模データフィッティング）
2. 暗黙的パターンマッチング -- テキストからのパターン認識
3. 内的ロジックの誘導 -- コード生成、推論
4. 生成されたシンボリック出力 -- コネクショニスト基盤の上に構築された言語とコード

> "The large language models are [connectionist] and they are [trained on massive data]. But they are now generating symbolism."
>
> （大規模言語モデルはコネクショニストであり、大量のデータで学習されている。しかし今、それらはシンボリズムを生成し始めている）

この洞察は重要だ。LLM はコネクショニストな手法（ニューラルネットワーク）で学習されているが、その出力はコードやルールといった「シンボリック」なものである。つまり、コネクショニズムとシンボリズムは対立するものではなく、知能の異なるレイヤーを構成しているということだ。

### 6.2 共存であって置き換えではない

スライド下部の注記は、セッション全体の核心メッセージだった:

> **COEXISTENCE, NOT REPLACEMENT**: Neither is "better". The symbolic structures rely on the connectionist substrate for grounding and adaptability.
>
> （**共存であって置き換えではない**: どちらが「より良い」わけでもない。記号的構造は、接地と適応のためにコネクショニストの基盤に依存する）

スピーカーはこう述べた:

> "[The key is that]... the symbolic structures [rely] on the connectionist substrate for grounding and adaptability. You don't have to [choose] one [or] another. You can [have both]."
>
> （鍵は、記号的構造がコネクショニストの基盤に依存していることだ。一方を選ぶ必要はない。両方を持つことができる）

この「共存」の考え方が、セッション後半で提示される「Engine as Agent Tools」アーキテクチャの思想的基盤となる。

---

## 7. 知能の指数関数的爆発: カーボンからシリコンへ

### 7.1 8段階の進化カーブ

次にスピーカーが示したのは、知能の進化を時間軸で俯瞰する壮大な曲線だった。

![Exponential Explosion of Intelligence](source/images/IMG_4779.jpg)

*スライド写真: 「EXPONENTIAL EVOLUTION INDEX CURVE OF INTELLIGENCE -- From Carbon-Based Biological Evolution to Silicon-Based AI - A Narrative of Recapitulation」。縦軸が Intelligence Level / Task Processing Capability（対数スケール）、横軸が Time（数十億年前から未来へ）。8つのマイルストーンが指数関数的曲線上に配置されている。*

スライドに記載された8段階（+ 将来展望）:

| # | マイルストーン | 説明 |
|:---|:---|:---|
| 1 | Multicellular Life | 複雑な知覚と協調（生物学的起源） |
| 2 | Human Brain Formation | 抽象的思考、自己認識、高度な認知 |
| 3 | Writing & Language | シンボリックな知識の継承、世代を超えた伝達、文明の時代 |
| 3b | Emergent Collective Biological Intelligence | 複雑な人間社会、共有される文化的知識、グローバルな知識処理ネットワーク |
| 4 | First Industrial Revolution | 機械が物理労働を代替、人的生産性の大幅な向上 |
| 5 | Information Technology Revolution | コンピュータとインターネット、デジタルおよびグローバルな情報処理 |
| 6 | AI Large Models Era | 機械が汎用的な理解・生成・推論を獲得 |
| 7 | AI Coding | AI プログラミング、継続的学習、経験の蓄積と交換 |
| 8b | Emergent Collective Silicon Intelligence | AIエージェント群が相互運用、新形態の協調と分散知能 |

スピーカーはこの図に対して:

> "This [curve] charts a profound recursion. Intelligence progressed from slow biological evolution (Carbon-Based Era) to exponential technological acceleration."
>
> （この曲線は深遠な再帰を描いている。知能は、ゆっくりとした生物学的進化（カーボンベース時代）から指数関数的な技術的加速へと進んだ）

特に注目すべきは、ステップ 3（Writing & Language）と ステップ 7（AI Coding）の対応関係だ。人類が文字を発明して「知識の継承」を可能にしたのと同様に、AIがコードを書くようになることで「知識の継承」の新しいラウンドが始まる――これがスピーカーの主張だった。

> "[AI Coding acts] as their symbolic language, unlocking a new round of 'knowledge inheritance', much like early writing."
>
> （AIコーディングは彼らのシンボリック言語として機能し、初期の文字がそうであったように、「知識の継承」の新しいラウンドを解き放つ）

スライド下部の要約:

> The imminent emergence of 'Collective Silicon Intelligence' through agent swarms will parallel human collective intelligence, triggering the final, explosive leap towards true Global Collective Intelligence.
>
> （エージェント群によって間もなく出現する「集団的シリコン知能」は、人間の集団知能と並行し、真のグローバル集団知能への最終的で爆発的な跳躍を引き起こすだろう）

### 7.2 ゲーム開発への含意

この壮大な進化論的視点がゲーム開発と何の関係があるのか。スピーカーの意図は、次のセクションで明確になった。ゲームエンジンは、人間が何十年もかけて蓄積してきた「知識の結晶」であり、それ自体がシンボリックAIの巨大な実装例なのだ。

> "[The game engine is] the stack use of human knowledge, [built] over decades... [It represents] the [distribution] of human [intelligence]."
>
> （ゲームエンジンとは、何十年にもわたって蓄積された人間の知識の積層利用であり、人間の知能の結晶を表している）

---

## 8. Engine as Agent Tools: ゲームエンジンをAIエージェントのツールとして

### 8.1 学術的アプローチの限界

セッションのクライマックスは、「Engine as Agent Tools」のスライドだった。

![Engine as Agent Tools](source/images/IMG_4781.jpg)

*スライド写真: 「Engine as Agent Tools」。左側にピラミッド型の「Purely AI (Black Box)」アプローチ。右側に「PCG / Runtime PCG - Game Engine as an AI Agent (A prior distribution built by humans over decades)」。*

スライドの左側は「Academic Approaches（学術的アプローチ）」として、純粋なAIによるシーン生成の問題を列挙していた:

1. Insufficient information in the prompt（プロンプトの情報不足）
2. Poor robustness, poor controllability（ロバスト性と制御性の欠如）
3. A black box -- hard to intervene（ブラックボックス -- 介入困難）
4. Huge hardware resources（巨大なハードウェアリソース）

この左側のピラミッドは「Narrow Prompt（狭いプロンプト）」を頂点とし、ブラックボックスなAIを通して「Result（結果）」を出力する。結果のクオリティバーは低い位置にとどまっている。

### 8.2 ゲームエンジンをエージェントツールにする

右側のアプローチが、本セッションの核心的な提案だ。

> "Instead of asking [AI to do everything], we use [the game engine] as an AI agent [tool]."
>
> （AIに全てをやらせるのではなく、ゲームエンジンをAIエージェントツールとして使う）

右側の図では:
- **Broad Prompt（広いプロンプト）**: High-level intent, selection, editing（ハイレベルな意図、選択、編集）
- **AI → Meta Data**: AIはメタデータ（指示情報）の生成に専念
- **PCG / Runtime PCG**: ゲームエンジンが実際のシーン生成を担当
- **Game Engine as an AI Agent**: 「A prior distribution built by humans over decades（人間が数十年かけて構築した事前分布）」

右下のメモには、このアプローチの利点が4つ記載されていた:

1. **More prompt information** like selection, etc.（選択等のより多くのプロンプト情報）
2. **Minimal AI involvement, less uncertainty**（AIの関与を最小限に、不確実性を減らす）
3. **Game engine is probably the most powerful AI agent tool**（ゲームエンジンはおそらく最も強力なAIエージェントツール）
4. **Great performance and efficiency**（優れたパフォーマンスと効率）

### 8.3 次元の劇的な削減

スライドの右端には「Dimension reduced drastically（次元が劇的に削減される）」と記載されていた。これは、AIが処理すべき問題空間が大幅に縮小されることを意味する。

従来のアプローチ（左側）:
- AIが「3Dシーン全体」を生成しなければならない -- 膨大な次元数

提案アプローチ（右側）:
- AIは「メタデータ」（何をどこに置くか、どんなスタイルにするか）だけを生成
- 実際の3D配置、物理演算、ライティングはゲームエンジンが処理

> "Your [intent], your higher-level ideas. The [engine] handles [the] future. You make those ideas and [translate] them into real. The [engine] generates the [scenes]."
>
> （あなたの意図、高レベルのアイデア。エンジンがその先を処理する。あなたがアイデアを作り、エンジンがそれを現実のシーンに変換する）

この「次元削減」の考え方は、問題を扱いやすいサイズに分解するというソフトウェアエンジニアリングの基本原則に通じるものであり、AIの能力が限られている現段階で特に有効なアプローチだ。

### 8.4 「ゲームエンジンこそ最も強力なAIエージェントツール」

セッション前半の理論的議論は、全てこの一文に収束した。

> "The game engine is probably the most powerful AI agent tool [that has ever been built]."
>
> （ゲームエンジンは、おそらくこれまでに作られた中で最も強力なAIエージェントツールだ）

この主張の根拠を整理すると:

1. **事前分布としての価値**: ゲームエンジンには、物理法則・マテリアルシステム・ライティングモデル・コリジョン検出など、人間が何十年もかけて蓄積した知識が凝縮されている。これは「a prior distribution built by humans over decades（人間が数十年かけて構築した事前分布）」として機能する
2. **説明可能性**: ルールベースであるため、出力の根拠を追跡でき、デバッグが可能
3. **エネルギー効率**: 論理演算ベースのため、ニューラルネットワークに比べて10-100倍効率的
4. **制御性**: パラメータを個別に調整でき、システム全体を壊さずに修正可能
5. **確実性**: AIの関与を最小限に抑えることで、出力の不確実性を大幅に低減

---

## 9. シンボリズムとコネクショニズムの融合: 未来のランドスケープ

### 9.1 相互変換と融合

ここまでの議論は「シンボリズム優位」に聞こえるかもしれないが、スピーカーは明確にバランスを取った。

![The Future of AI: Symbolism & Connectionism Convergence](source/images/IMG_4780.jpg)

*スライド写真: 「THE FUTURE OF AI: SYMBOLISM & CONNECTIONISM CONVERGENCE」。左側が Symbolism（Traditional Computing）、右側が Connectionism（Neural Networks）。中央に「MUTUAL CONVERSION & FUSION（相互変換と融合）」の渦巻き。*

スライドの内容:

**Symbolism 側（Traditional Computing）**:
- CPU ベース
- Exact, Discrete Logic（厳密な離散論理）
- Complex functions (e.g., full PBR light equation) require explicit, high-cost computation.（PBR ライティング方程式のような複雑な関数は、明示的で高コストな計算を必要とする）
- Complex Equation → **High Computational Cost** (Slow, Precise)

**Connectionism 側（Neural Networks）**:
- GPU ベース
- Non-linear Superposition（非線形重ね合わせ）
- Approximates complex functions in finite range with significantly **LOWER** computation.（有限範囲で複雑な関数を大幅に低い計算量で近似する）
- Neural Net Approx. → **Lower Computational Cost** (Fast, Approximate) -- e.g., 1-2 orders magnitude lower for PBR

つまり、前述のエネルギー効率の議論はシンプルなタスク（XNORゲームなど）に関するものであり、PBR のような複雑な方程式では**逆にニューラルネットワーク近似の方が計算コストが低い**場合がある。

### 9.2 Future Landscape

スライド下部の「FUTURE LANDSCAPE」ボックスには:

> Hybrid systems leverage *both* approaches for **OPTIMIZED EFFICIENCY**.
> Examples: Super-resolution, Light Distribution Fitting.
> Energy efficiency is relative to task and method.
>
> （ハイブリッドシステムは、最適化された効率のために両方のアプローチを活用する。例: 超解像、光分布フィッティング。エネルギー効率はタスクと手法に相対的である）

> "[Connectionism and symbolism will] keep working together."
>
> （コネクショニズムとシンボリズムはともに協力し続ける）

スピーカーはこのスライドで、「Non-linear Superposition（非線形重ね合わせ）」と「Approximation（近似）」という二つの矢印が渦巻きの中心で交わる図を指して説明した。シンボリズムの厳密さとコネクショニズムの柔軟性が「相互浸透し融合する（Interpenetration & Merging）」未来像が描かれていた。

ゲーム開発における具体例:
- **超解像（Super-resolution）**: レンダリング結果をニューラルネットワークでアップスケール（DLSS, FSR など）
- **光分布フィッティング（Light Distribution Fitting）**: 物理ベースの照明計算をニューラルネットワークで近似
- **PCG（Procedural Content Generation）**: ルールベースの地形生成にAIによるバリエーション追加

これらは既に商用ゲームエンジンで実装されている技術であり、シンボリズムとコネクショニズムの融合は理論上の話ではなく、すでに始まっている現実だ。

---

## 10. World Generation Tools: 音声駆動プロシージャルゲームシーン生成

### 10.1 セッション第3部の導入

セッション前半の理論的議論を経て、後半は具体的なツールのデモンストレーションに移った。

![World Generation Tools](source/images/IMG_4782.jpg)

*スライド写真: 「03 World Generation Tools -- Voice driven procedural game scene generation」。Tencent Games x GDC のロゴ。*

> "Now, let's [move to the] demonstration."
>
> （では、デモンストレーションに移ろう）

スピーカーが交代し、音声でゲームシーンを生成するデモが行われた。これが「Intent-Driven Game Scene Editor」の実装部分であり、前半で論じた「Engine as Agent Tools」アーキテクチャの具体的な適用例となる。

### 10.2 アーキテクチャの推定

デモの詳細はトランスクリプトからは十分に復元できないが、スライドの内容と前半の議論から、以下のアーキテクチャが推定される:

```
ユーザーの音声入力
    ↓
音声認識（STT: Speech-to-Text）
    ↓
LLM による意図解析（Intent Parsing）
    ↓
メタデータ生成（AI → Meta Data）
    ↓ ← この時点でAIの役割は終了
ゲームエンジン（PCG / Runtime PCG）
    ↓
ゲームシーン出力
```

この流れでは:
- **AIが担当する部分**: 音声認識、意図の解析、メタデータの生成（何を、どこに、どのようなスタイルで配置するか）
- **ゲームエンジンが担当する部分**: 実際の3Dオブジェクトの配置、物理法則の適用、ライティング、コリジョン設定、地形生成

AIの役割を「メタデータ生成」に限定し、実際のシーン構築をゲームエンジンのPCGシステムに委ねることで、前半で議論した「ブラックボックス問題」「エネルギー効率」「制御性」の全てが改善される設計となっている。

### 10.3 「意図駆動」の意味

セッションタイトルの「Intent-Driven（意図駆動）」は、この設計思想を端的に表現している。

ユーザーが指定するのは「何を作りたいか」という**意図**だけだ:
- 「中世の城を高台に置いて」
- 「周囲に松の木を配置して」
- 「夕暮れのライティングにして」

この意図をLLMが構造化されたメタデータに変換し、ゲームエンジンのPCGシステムが物理法則に従ったシーンとして実現する。AIは「何を」を決め、エンジンは「どうやって」を知っている。この役割分担が、純粋なAIアプローチよりも遥かに安定した出力を可能にする。

---

## 11. 考察: 日本のゲーム開発への示唆

### 11.1 自社パイプラインへの適用

本セッションの知見を自社開発パイプラインに適用する際のポイント:

**AIの投入ポイントを見極める**:
- メタデータ生成（レベルデザインのパラメータ決定）: AIが得意
- 実際のシーン構築（オブジェクト配置、物理設定）: エンジンが得意
- テクスチャ・マテリアル生成: AIが得意
- ライティング計算の近似: ハイブリッド（Neural Net Approximation）
- QAテスト（バグ検出、バランス調整）: AIが得意

**既存のPCGパイプラインを活かす**:
- Houdini や Unreal Engine の Procedural Generation は、既にシンボリックAIの一形態
- ここにLLMベースの意図解析を追加するだけで、本セッションのアーキテクチャに近づく
- 全てを刷新する必要はなく、インクリメンタルに導入できる

### 11.2 「Engine as Agent Tools」パターンの汎用性

本セッションで提示された「AIの役割をメタデータ生成に限定し、ドメイン固有のルールベースシステムに実行を委ねる」というパターンは、ゲーム開発に限らない:

- **建築設計**: AIが意図を解析 → BIM（Building Information Modeling）が物理的・法規的制約を処理
- **音楽制作**: AIがメロディのアイデアを生成 → DAW が音響的・楽理的ルールを適用
- **製造業**: AIが設計パラメータを提案 → CAD/CAM が物性・加工制約を処理

「ドメイン知識の結晶であるルールベースシステムを、AIエージェントのツールとして活用する」という発想は、あらゆる産業のAI活用に応用可能な設計パターンだ。

---

## まとめ

Tencent Games の GDC 2026 セッション「Intent-Driven Game Scene Editor」は、AIとゲームエンジンの関係について根本的な再考を促すものだった。

**セッションの核心メッセージ**:

| 主張 | 内容 |
|:---|:---|
| **ゲームエンジンはAI時代に不要にならない** | シンボリックAIとしての4つの優位性（説明可能性・データロジック分離・エネルギー効率・継続的学習）を持つ |
| **AIの役割を限定すべき** | AIはメタデータ生成に専念し、実際のシーン構築はゲームエンジンに委ねる |
| **共存が正解** | シンボリズムとコネクショニズムはどちらが優れているわけでもなく、タスクに応じて最適な組み合わせが異なる |
| **ゲームエンジンは最強のAIエージェントツール** | 人間が数十年かけて蓄積した知識の結晶であり、AIの事前分布として最も価値が高い |

スピーカーの最後の言葉を引用して締めくくりたい:

> "The game engine is probably the most powerful AI agent tool [ever built]."
>
> （ゲームエンジンは、おそらくこれまでに作られた中で最も強力なAIエージェントツールだ）

この一文は、AI万能論が広がる中で、「AIをどう使うか」という問いに対する明快な指針を与えてくれる。全てをAIに任せるのではなく、人間が長年かけて蓄積した知識体系をAIのツールとして活用する――そのアプローチこそが、ゲーム開発のみならず、あらゆるドメインにおけるAI活用の最適解なのかもしれない。

---

## セッション中のスライド一覧

本記事で参照したスライド写真（会場で筆者撮影）:

| # | ファイル | 内容 |
|:---|:---|:---|
| 1 | IMG_4774.jpg | Four Key Advantages: Symbolic AI vs Connectionist AI |
| 2 | IMG_4775.jpg | Black Box: Trained Neural Network |
| 3 | IMG_4776.jpg | Decoupling of Data and Logic |
| 4 | IMG_4777.jpg | High Energy Efficiency: Symbolism vs Connectionism |
| 5 | IMG_4778.jpg | The Evolutionary Arc: From Connectionism to Symbolism |
| 6 | IMG_4779.jpg | Exponential Explosion of Intelligence |
| 7 | IMG_4780.jpg | The Future of AI: Symbolism & Connectionism Convergence |
| 8 | IMG_4781.jpg | Engine as Agent Tools |
| 9 | IMG_4782.jpg | World Generation Tools: Voice driven procedural game scene generation |

---

## 参考リンク

- [GDC 2026 公式サイト](https://gdconf.com/)
- [GDC 2026 AI Summit セッション一覧](https://schedule.gdconf.com/)
- [Tencent Games 公式サイト](https://game.qq.com/)

---

## おわりに

最後までお読みいただきありがとうございます。GDC 2026 の他のセッションレポートも順次公開していますので、ぜひフォローしてお待ちください。

**dsgarage Games** | GDC 2026 現地レポート
