---
title: "GDC 2026: Secrets of the Tabletop Games Industry — ボードゲーム業界25年のベテランが語る「売れるゲーム」の作り方"
subtitle: "How to Design, Playtest, and Pitch Your Tabletop Game — Lessons from Asmodee"
author: "dsgarage"
date: "2026-03-11"
---

# GDC 2026: Secrets of the Tabletop Games Industry — ボードゲーム業界25年のベテランが語る「売れるゲーム」の作り方

「自分のボードゲームを作りたい」——そう思ったとき、最初に何をすべきだろうか。美しいアートワークを発注する？ Kickstarter（クリエイターが製品の企画段階で支援者から資金を募るクラウドファンディングサービス。欧米のボードゲーム業界では事実上の標準的な資金調達・販売チャネルとなっている）のページを作る？ 実はどちらも間違いだ。世界最大級のテーブルトップゲームパブリッシャー Asmodee で25年のキャリアを持つ Bryan Bornmueller は、「プロトタイプは速く作れ、美しくするな」と断言する。

GDC 2026 の Tabletop Summit で行われた本セッションでは、Catan（カタン）や Ticket to Ride（チケット・トゥ・ライド）を擁する Asmodee のデザインディレクターが、業界の構造からゲームデザインのプロセス、パブリッシャーへのピッチ（作品を売り込むための短いプレゼンテーション）まで を網羅的に解説した。デジタルゲーム開発者にとっても、「プロトタイプ → テスト → 削る → 人とつながる」というサイクルは、そのまま自分の開発プロセスに適用できる普遍的な知見だ。

> **Tabletop Summit とは**: GDC（Game Developers Conference）は毎年サンフランシスコで開催されるゲーム業界最大の開発者会議だ。その中の「Tabletop Summit」は、ボードゲーム・カードゲーム・TRPG などアナログゲームに特化したトラックで、2020年代に入って急速に存在感を増している。デジタルゲーム開発者が大半を占める GDC の中で、アナログゲームの知見を共有する貴重な場として機能している。

本記事では、セッション全体の内容を整理し、テーブルトップゲーム業界への参入を考える人、あるいはゲームデザインの本質を学びたい全ての開発者に向けて、実践的なノウハウをまとめる。

> **日本のボードゲーム文化との違い**: 日本では同人イベント（ゲームマーケット等）で個人が直接販売する「同人→商業」のルートが確立しているが、欧米では「デザイナーがパブリッシャーにピッチして契約を取る」という分業モデルが主流だ。本セッションは後者の欧米モデルを前提として語られている。また、欧米ではゲームの評価・情報交換の中心に BGG（BoardGameGeek——世界最大のボードゲームデータベース兼コミュニティサイト。ユーザーレビュー・ランキング・フォーラムが集約されており、欧米のボードゲーマーにとって事実上の標準プラットフォーム）が存在し、ここでの評価がゲームの商業的成功に大きく影響する。

---

## セッション概要

| 項目 | 内容 |
|:---|:---|
| **イベント** | GDC Festival of Gaming 2026 |
| **日時** | Tuesday, March 10, 2026 / 10:30am - 11:30am |
| **タイトル** | Secrets of the Tabletop Games Industry |
| **スピーカー** | Bryan Bornmueller（Asmodee / Office Dog Studio） |
| **会場** | Room 3018, West Hall |
| **パス** | Festival Pass, Game Changer Pass |
| **レベル** | All |
| **トラック** | Design（Tabletop Summit——ボードゲーム・TRPG等のアナログゲーム専門トラック） |
| **形式** | Lecture |

---

## スピーカー紹介: Bryan Bornmueller

Bryan Bornmueller はテーブルトップゲーム業界で25年以上の経験を持つベテランだ。セッション冒頭、彼は自身のキャリアを軽快に振り返った。

> "I've been in the game industry for 25 years. I started out working at a game store, which I recommend to anyone who's getting started in the game industry."
>（ゲーム業界に25年います。最初はゲームストアで働いていました。ゲーム業界に入りたい人には誰にでも勧めます）

ゲームストアの店員からキャリアをスタートし、その後ミネソタ州に移って Fantasy Flight Games（『アーカムホラー』『キーフォージ』等で知られる米国の大手パブリッシャー）に入社した。Bryan はこの時期を特に思い入れ深く語った。

> "When I started there, it went from like 20 people to like 300 people."
>（入社したとき20人くらいだった会社が、300人くらいまで成長した）

その後 Fantasy Flight Games が Asmodee に買収され、一時別の流通業者 PSI に移籍した後に Asmodee に復帰。現在は Asmodee 傘下のスタジオ **Office Dog** を設立し、オリジナルゲームの開発を手がけている。

> **Asmodee とは**: フランスに本社を置く世界最大級のボードゲームグループ。Fantasy Flight Games、Days of Wonder、Z-Man Games など多数のパブリッシャーを傘下に持ち、『カタン』『チケット・トゥ・ライド』『パンデミック』『ドブル（Spot It!）』など世界的ヒット作を多数擁する。日本でもホビージャパン等を通じて多くのタイトルが流通している。

現在の Bryan の肩書は「ゲームデザイナー兼デザインディレクター」だが、もう一つ重要な役割がある。外部デザイナーからのゲームのスカウティング（有望な作品を発掘・評価すること）だ。

> "I also have a very long title that basically does scout for games for some of us. When we first find some games, you need to come look out."
>（それと、長い肩書がもう一つあって、基本的にはいくつかのブランドのためにゲームをスカウトしています。有望なゲームを見つけたら、チームに「これを見てくれ」と持っていく役割です）

代表作には Golden Geek Award（BGG が主催するボードゲームの年間賞）ベスト協力ゲーム部門受賞作品や、「The Fellowship of the Ring: Trick-Taking Game」（2025年）がある。セッション中、自らこのトリックテイキングゲーム（マストフォローやトランプの切り札のように、場に出されたカードに対して手札から1枚ずつ出し、最も強いカードを出した人が「トリック」を獲得するジャンル）のプロトタイプ制作過程を具体例として紹介していたのが印象的だった。

---

## 1. テーブルトップゲーム業界の4層構造

### ホビーゲーム市場とは

Bryan はまず、自分が扱う領域を「ホビーゲーム」と定義した。約50年の歴史を持つこの市場は、いわゆる大衆向けゲーム（マスマーケット。日本でいえばトイザらスや家電量販店で売られる『人生ゲーム』『モノポリー』のような層）とは異なる独自のエコシステムを形成している。

> "Hobby games has about a 50 year history... several kind of big innovations in all of those."
>（ホビーゲームには約50年の歴史があり、その中でいくつかの大きなイノベーションがありました）

ホビーゲーマーはどういう人たちか。Bryan は彼らを「カジュアルなゲーマーとは違う」と説明し、その特徴を強調した。

> "It's one of the things that's really wonderful about tabletop games, is that if you're into it, you have to introduce other people to the thing you like."
>（テーブルトップゲームの素晴らしいところは、自分がハマったら、好きなものを他の人に紹介しなければならないこと）

デジタルゲームなら一人で買って一人で遊べる。しかしボードゲームは違う。遊ぶためには誰かにルールを教え、テーブルに座ってもらう必要がある。この「布教」の構造が、ホビーゲーム市場の独特なダイナミクスを生んでいる。ホビーゲーマーは新しいゲームを積極的に探し、友人にルールを教え、地元のゲームストア（FLGS: Friendly Local Game Store と呼ばれる、ボードゲーム・TCG を専門に扱う小売店。日本の「ボードゲームカフェ」や「イエローサブマリン」「すごろくや」のような専門店に近い存在）で定期的に遊ぶ層だ。

### 4つの階層

Bryan は「テーブルトップ業界の構造は非常に有機的で奇妙な動物だ（a very organic, working animal）」と前置きした上で、業界を4つの階層（Tier）に整理した。

| 階層 | 役割 | マージン | リスクの性質 |
|:---|:---|:---|:---|
| **Designer（デザイナー）** | ゲームのアイデアを生み出し、プロトタイプを作る | --- | 時間と労力の投資。金銭的投資は比較的小さい |
| **Publisher（パブリッシャー）** | ゲームを製品化し、製造・マーケティングを担う | 中 | 開発費・印刷費・マーケティング費を先行投資 |
| **Distributor（卸売業者）** | パブリッシャーと小売店の間を取り持つ | 極小 | 膨大な在庫を抱える必要がある |
| **Retailer（小売店）** | 消費者にゲームを直接販売する | 大 | 店舗・スタッフの維持コスト |

**デザイナー**について、Bryan はこう補足した。

> "There are people that do this as a full-time job, like they just make game ideas."
>（フルタイムの仕事としてやっている人たちもいます。ゲームのアイデアを作ることだけを仕事にしている人たちが）

ただし、多くのデザイナーは独立した個人事業主だ。パブリッシャーと契約してロイヤリティ（売上の3〜8%程度が相場）を受け取るモデルが一般的である。

**パブリッシャー**は、デザイナーのアイデアやプロトタイプを受け取り、それを物理的な製品にする存在だ。

> "Find someone to make a factory to make and print the game, and also art design, illustrations, plastic pieces... whatever the game needs. The publishers ultimately decide what's going to go in a box."
>（工場を見つけてゲームを印刷・製造させ、アートデザイン、イラスト、プラスチック駒……ゲームに必要なものすべて。最終的に箱に何を入れるかを決めるのがパブリッシャーです）

**流通業者**は「最も馴染みのない（the most foreign of all the companies）」存在だが、「最も重要な存在の一つ」でもあると Bryan は強調した。

> "Just in the U.S., it's a big country, and it takes a lot of effort to get something where it's going to be."
>（アメリカは大きな国で、製品を届けるべき場所に届けるには膨大な労力がかかる）

**小売店**はオンライン販売（Amazon 等）も含まれるが、Bryan が特に意識しているのは独立系のホビーショップだ。

### 仮想100ドルゲームの利益配分

Bryan はスライドで仮想的な100ドルのゲームを例に、お金の流れを示した。「あくまで仮説的な丸い数字だが、現実に基づいている（hypothetical round numbers that are based on reality）」と前置きした上で——

```
消費者が $100 で購入
  └→ Retailer（小売）: $100 で販売、$50 で仕入れ → 粗利 $50
      └→ Distributor（卸売）: $50 で販売、$40 で仕入れ → 粗利 $10
          └→ Publisher（出版）: $40 で販売、製造・デザイナー報酬を支払い
```

各階層の利益配分の根拠は「リスク」にある。小売店は物理的な店舗を構え、スタッフを雇い、多品種を少量ずつ在庫する。だから利益率が高い。一方、流通業者は大量に仕入れて大量に捌く薄利多売モデルだ。Bryan の流通業者の友人が語った言葉が、この構造を鮮やかに描写していた。

> "If we buy 10 games, when we sell nine, you're breaking even. Sell the 10th, you make a little bit of money. But hopefully they're doing that for a lot of games, and without a lot of effort."
>（10個仕入れて9個売って、やっとトントン。10個目が売れてようやく少し利益が出る。でもうまくいけば、それを大量のゲームで、あまり労力をかけずにやっている）

パブリッシャーは最も複雑なリスクを背負う。開発費、マーケティング費、製造費をすべて先行投資し、製品が売れるという「前提（assumption）」のもとに動く。Bryan はこの点について、小売店や流通業者との決定的な違いを指摘した。

> "The retailers and the distributors get to sort of evaluate... And the designers have a huge investment of their own time and effort, but usually relatively little hard financial."
>（小売店と流通業者は（売れるかどうかを）評価してから仕入れることができる。デザイナーは自分の時間と労力を大量に投資するが、通常は金銭的な投資は比較的少ない）

### 階層の重複と業界の小ささ

現実にはこれらの階層は明確に分かれていない。Bryan は苦笑しながら——

> "There are, of course, distributors who publish games... There are designers who own stores and retailers who own publishers. It goes on and on and on."
>（もちろん、パブリッシングもする卸売業者もいますし、店を持つデザイナーもいるし、パブリッシングに乗り出す小売店もある。きりがない）

そして業界で有名なジョークを紹介した。

> "People in one tier are always envious of another tier."
>（ある階層の人は、いつも別の階層を羨ましがる）

これは日本のゲーム業界でも馴染み深い話だ。同人作家が商業出版に憧れ、出版社が同人の自由さに憧れる——構造は洋の東西を問わない。

### 各階層でのキャリアパス

Bryan は各階層へのキャリアパスを具体的に示した。

- **小売店**: 既存のゲームストアで働く。Bryan 自身がこのルートからスタートしたことを思い出してほしい。彼はこの経験を強く推奨している。「他人のゲームを新しいプレイヤーに薦める経験は、自身のデザインスキルを磨くのに最高だった（pitching other people's games to new people who play games. It's a skill. And the more you know, the better you get）」。あるいは自分で店を開くこともできるが——「ゲームストアは大変です。ピンク色の壁と広い床が必要で、モップがけが好きじゃないとやってられない」と笑いを取っていた。

- **卸売業者**: 数は少ないが求人はある。ロジスティクスが得意なら可能性があるが、「非常に高額で、非常に競争的（very, very expensive, very competitive）」だと釘を刺した。

- **パブリッシャー**: コンベンション（Gen Con、Essen Spiel、Origins など大規模なボードゲーム見本市・即売会。日本のゲームマーケットに相当するが、規模は数万人〜数十万人と桁違いに大きい）でのパートタイムヘルプが入口になる。「コンベンションやイベントでのパートタイムのヘルプをいつも探しています。ドアに足を挟むいい方法です（They're always looking for part time help at conventions and events. Such a good foot in the door）」。プレイテスター（発売前のゲームをテストプレイして改善点をフィードバックする役割）として関わるのも有効だ。「彼らはゲームを遊んでくれる人を求めています。そうやって中の人たちと知り合いになれる」とも付け加えた。インターンシップの制度も多くのパブリッシャーにあるという。

- **デザイナー**: スタッフデザイナーの正社員職もあるが、多くは独立デザイナー（フリーランス）として活動。Bryan はこの点について、ある業界内のリソースにも言及した。「業界内にはフォーカスグループのようなものがあり、ウェブサイトやFacebookで求人情報——テーブルトップ開発やパブリッシングの仕事情報——が流れています。よく見かけます」。

---

## 2. ゲームデザインのプロセス

Bryan が示したデザインプロセスは極めてシンプルだった。スライドに映し出されたフローをなぞりながら、彼はこう語った。

> "And then you make the prototype, and then you try it out. And then you change everything, and then you try it out. And then you change everything. And then you try it out. And you change it a little less. And then when you're happy with it, you can pitch it. And if you're fortunate, you will find someone to sign, and then they'll put it out."
>（プロトタイプを作る。それを試す。全部変える。また試す。全部変える。また試す。今度は少し変更が減る。満足したらピッチする。運が良ければ契約してくれる人が見つかって、出版してもらえる）

```
アイデア → プロトタイプ → プレイテスト → 変更 → プレイテスト → 変更
→ ... → 変更が少なくなってきたら → ピッチ（売り込み） → (運が良ければ) 出版
```

この「変更 → テスト」のサイクルは、ソフトウェア開発のアジャイル手法を彷彿とさせる。デジタルゲーム開発者にとっても直感的に理解できるプロセスだろう。

### Step 1: アイデアからプロトタイプへ

Bryan はアイデアの段階について、率直にこう切り出した。

> "The step one is having idea. I can't help you with that. Those are your ideas."
>（ステップ1はアイデアを持つこと。これは私には教えられない。それはあなた自身のアイデアだ）

しかしステップ1.5——プロトタイプの制作——については豊富なアドバイスがあった。最も重要なのは**速度**だ。

> "You want to do it as quick as you can. I personally always like a lot of pleasure. When you have an idea, you're sort of moving around. And you think about how you need to scribble and add on some papers, quick as you can, to find things that your imagination could not see."
>（できるだけ速くやれ。個人的に、アイデアがあるときはワクワクしている。あれこれ考えて動き回っている。できるだけ速く紙に書き出す。自分の想像力だけでは見えなかったことを発見するために）

ここでの Bryan のポイントは明確だ。頭の中で考えているだけでは見つけられない問題が、物理的なプロトタイプを作った瞬間に見えてくる。だからこそ、速く作ることが重要なのだ。

使うツールについては、**何でもいい**と断言した。

> "Just use tools you already know. Don't feel intimidated that you need a software tool or have great craft skills or anything."
>（すでに知っているツールを使え。ソフトウェアツールが必要だとか、優れた工作スキルが必要だとか、そういうことに怯えるな）

Bryan 自身は Apple Pages でカードを作ったという。

> "I made an Apple Pages template for a very long portion of their development process, which I don't recommend. I'm not advocating for that, but it was fast. And I could do it. It was mostly just cards. And it didn't get in my way to just printing out the sorts of paper and putting them in card sleeves."
>（開発期間のかなり長い間、Apple Pages でテンプレートを使っていた。これを推奨しているわけではない。でも速かった。できたから。ほとんどカードだけだったので、紙に印刷してカードスリーブに入れるだけで十分だった）

ここで会場から笑いが起きた。Bryan のクリエイティブ・ディレクターは、このPagesで作ったプロトタイプに最初は眉をひそめたという。しかし最終的にはちゃんとしたテンプレートを作ってもらえた——「準備が整ったらね（once it was ready for that）」。

#### プロトタイプの鉄則: 美しくするな

![プロトタイプ vs 完成品の比較スライド](source/images/IMG_4708.jpg)
*Bryan が示した「ITERATE」スライド。プロトタイプに直接書き込み、変更を素早く反映することの重要性を説いた*

Bryan はプロトタイプの美的完成度について、極めて強い言葉で警告した。

> "Don't get too hung up on the aesthetics of your prototype, especially early on. Make it clear, make it understandable. But unless the aesthetics are the point, it's just going to slow you down."
>（プロトタイプの見た目にこだわりすぎるな。特に初期段階では。明瞭に、わかりやすくしろ。でも見た目がゲームの本質でない限り、見た目にこだわるのはスピードを落とすだけだ）

さらに、美しいプロトタイプがイテレーション（反復改善）を妨げる理由も説明した。

> "If it's really beautifully typeset, that's going to slow down your iteration."
>（本当に美しく組版してしまうと、イテレーションが遅くなる）

美しく作り込んだカードを修正するのは心理的に抵抗がある。しかし手書きのメモなら、プレイテストの最中にさっと書き換えられる。この「変更のしやすさ」がプロトタイプの本質だ。

#### Reiner Knizia のプロトタイプ——500作のベテランでもこのレベル

Bryan は自身のトリックテイキングゲーム「The Fellowship of the Ring」だけでなく、同僚のゲームデザイナーの事例も紹介した。

> "On the right is the prototype that Reiner Knizia sent us on his Hobbit dice game. He's made 500 games. One of the most professional in the game industry."
>（右側は、ライナー・クニツィアがホビットのダイスゲームについて送ってきたプロトタイプです。彼は500以上のゲームを作ってきた。業界で最もプロフェッショナルな人物の一人です）

Reiner Knizia（ライナー・クニツィア——『モダンアート』『ケルト』『ロスト・シティ』など数々の名作で知られるドイツ出身の巨匠。ドイツ年間ゲーム大賞を複数回受賞している）ほどのベテランでも、プロトタイプは極めてシンプルだった。Bryan はスライドの比較画像を指しながら言った。

> "The most important part is gameplay-wise, it's identical. But obviously, a lot of effort went into making it look a little nicer. But it totally worked. That's hard to see. It's like the worst work of art you could probably find."
>（最も重要なのは、ゲームプレイの観点では完成品と同一だということ。明らかに、見た目を良くするために多くの労力がかけられた（完成品には）。でもプロトタイプは完全に機能した。見た目は……おそらく今まで見た中で最もひどい芸術作品だ）

会場からは大きな笑いが起きた。500以上のゲームをデザインしてきた世界的巨匠でも、プロトタイプは「おそらく過去のゲームの使い回しの印刷物（probably used in a bunch of low-grade games over the years）」を使っていた。しかし——「完全に明瞭で、完全に使える（totally clear, totally usable）」のだ。

Bryan はここで、プロトタイプに過度な品質を求める必要がないことを念押しした。

> "I'm not saying we need to have community games branded box that this prototype came in."
>（このプロトタイプが入っていたブランド入りの箱が必要だと言っているわけではない）

---

## 3. プレイテスト——6つの段階と観察の技法

### プレイテストのタイムライン

Bryan はプレイテストを明確な段階に分けて説明した。

> "I think about it in kind of a work-for-stage-of-use."
>（段階ごとに使い分けるものだと考えています）

#### 第1段階: セルフテスト（Early Testing）

> "Probably early on, you just want to make a prototype. Have your idea and play it with some people you trust. Just to get your own eyes on whether your idea has any merit or not. It doesn't make sense, has obvious problems, it isn't usable."
>（最初の段階では、プロトタイプを作って信頼できる人と遊ぶだけでいい。自分のアイデアに価値があるかどうか、自分の目で確認するだけだ。意味が通るか、明らかな問題がないか、使えるかどうか）

Bryan は「信頼できる人」について補足した。「自分自身」でも構わない。一人でセットアップして遊んでみる。戦略やバランスのことは後回しでいい。まず「生き残れるか（survive）」が大事だ。

そして重要な注意を加えた。

> "And don't spend a long time in this state, right? You're sort of validating things. And this is also a time where you can just put it away. It's fine. You'll have another idea."
>（この段階に長くいるな。検証しているだけだ。ここで棚に仕舞っても構わない。大丈夫。次のアイデアが来る）

ダメだと思ったら見切りをつける。これはソフトウェア開発の「fail fast」の精神と同じだ。

#### 第2段階: 身近な人とのテスト（Friends & Family）

> "I hope you have a supporting partner or spouse or someone in your life. You know, don't make them play your game every night."
>（パートナーや配偶者など、支えてくれる人が生活の中にいることを願います。でも、毎晩ゲームを遊ばせるのはやめてあげて）

ここで会場から共感の笑いが漏れた。多くのゲームデザイナーにとって、家族は最初の——そして最も忍耐強い——プレイテスターだ。

#### 第3段階: ゲームストアでのテスト

> "Finding a local game store is great. Talk to the manager. If they sell food and drinks there, buy some food and drinks from the store and give you play testers."
>（地元のゲームストアを見つけるのがいい。マネージャーに話をして。もし飲食を出している店なら、フードやドリンクを買って、プレイテスターを確保しよう）

ゲームストアでフードを買うのは、場所を使わせてもらうことへの礼儀であり、実利的にもプレイテスターを惹きつける手段だ。日本のボードゲームカフェでも同じことが言えるだろう。

#### 第4段階: イベントでのテスト

Bryan は、予想以上に多くのプレイテストイベントが存在することを強調した。

> "There are protospiel events and incubators. In a lot of cities like this, it's kind of staggering how many play testing events are out there."
>（プロトシュピール（Protospiel——ボードゲームの試作品を互いに遊んでフィードバックし合う、デザイナー同士の交流イベント。ドイツ語で「プロトタイプの遊び」の意味）のようなイベントやインキュベーターがある。こういう都市では、驚くほど多くのプレイテストイベントが開催されている）

GDC の会場内にもプロトスペース（エスカレーター付近）があり、Bryan はそこでのテストも推奨していた。ただし、こうしたイベントのフィードバックには注意も必要だ。

> "Of course, you always want to put a grain of salt on your feedback from those sorts of events, because people who go and play test games are maybe not the average board game mind."
>（もちろん、こうしたイベントのフィードバックは話半分に聞く必要がある。なぜならプレイテストイベントに来る人たちは、平均的なボードゲーマーとは違うかもしれないから）

これは重要な洞察だ。プレイテストイベントの常連は「ゲームを評価する」ことに慣れた層であり、一般消費者とは感覚が異なる。

#### 第5段階: オンラインテスト

> "If your game is suitable to go online... Tabletop Simulator and stuff, there's a lot of platforms that are out there."
>（もしゲームがオンラインに向いているなら……Tabletop Simulator とかいろいろなプラットフォームがある）

しかしここで Bryan は、自身の苦い経験を共有した。

> "I certainly was developing a game that was really fun in person, but took 10 times as long online."
>（対面では本当に面白いゲームを開発していたのに、オンラインだと10倍時間がかかった）

物理的なゲームを作っているなら、オンラインでのフィードバックはプラットフォームの制約によるものであって、ゲームそのものの問題ではない可能性がある。「個人的にそれを受け取らないで（don't take that personally）」とも助言した。

#### 第6段階: ブラインドプレイテスト（最終段階）

Bryan が最も熱を込めて語ったのがこの段階だ。

> "Blind play testing. If you're publishing yourself, you definitely want to do this. You're working with a publisher, maybe they'll handle this for you, but you're very, very lucky. Because it's the worst. It's super important, but it's the worst."
>（ブラインドプレイテスト。自分でパブリッシングするなら絶対にやるべき。パブリッシャーと組んでいるなら彼らがやってくれるかもしれないが、それは非常にラッキーだ。なぜなら最悪だから。超重要だけど、最悪なんだ）

ブラインドプレイテストとは、デザイナーが一切介入せずに、初見のプレイヤーにプロトタイプを渡して遊んでもらうテストだ。

> "Handing your prototype to a group of people, and then opening it up and playing it like they just bought it. Without you in the room or helping, and just watching them struggle through your rules."
>（プロトタイプをグループに渡して、彼らがお店で買ったばかりのように開封して遊んでもらう。あなたは部屋にいない、あるいは手助けしない。ただ彼らがルールブックに苦戦するのを眺めるだけ）

Bryan は声のトーンを落として言った。

> "And this is where you're going to find every mistake you've ever made in your process."
>（ここで、あなたがプロセスの中で犯したすべてのミスが発見される）

多くのデザイナーが犯す過ちは、ブラインドプレイテストから始めようとすることだ。

> "I definitely have met a lot of game designers that have an idea, and they're like, great, I'll start blind play testing my game. This is definitely the last phase."
>（アイデアを持って「よし、ブラインドプレイテストから始めよう」というデザイナーに何度も会った。これは間違いなく最後のフェーズだ）

ブラインドプレイテストは「ゲームをチューニングする場」ではない。カードの文言を変えたり、新しい特殊能力を試したりする段階は、もう過ぎていなければならない。

> "This is not the phase to be tuning the game, changing the words on cards, trying out new special abilities. This is all about having theory behind and understanding the game as well."
>（これはゲームをチューニングする段階ではない。カードの文言を変えたり、新しい特殊能力を試したりする段階ではない。ゲームの理論的背景を理解しているかどうかの確認だ）

そしてブラインドプレイテストには「未プレイの人」が必要だという物理的制約もある。

> "You have to find people that have never played your game before to do a blind play test."
>（ブラインドプレイテストをやるには、あなたのゲームを一度も遊んだことがない人を見つけなければならない）

だから、プロセスの早い段階でブラインドプレイテストをやってしまうと、貴重な「未プレイの人」リソースを無駄遣いすることになるのだ。

### プレイテスト観察のコツ——言葉より行動を見ろ

Bryan はプレイテストで何を観察すべきかについて、非常に明確な指針を示した。

> "The most important thing is emotions. Watch people play and watch how they react. They're going to tell you words. They're going to say things about your game. That's nice. But it doesn't really matter. What really matters is watching the play, watching how they're doing."
>（最も重要なのは感情だ。人々がプレイするのを観察し、彼らがどう反応するかを見る。彼らは言葉で何かを伝えてくるだろう。ゲームについて何か言うだろう。それはありがたい。でもそれはあまり重要ではない。本当に重要なのは、プレイを見ること、彼らがどうしているかを見ることだ）

テスターが具体的な改善提案をしてくることもある。「この能力は嘘っぽい」「ここを変えたら」——しかしBryan はこう切り返す。

> "When they make suggestions... you can write it down, it's okay. But don't take that seriously. But if someone's smiling, take that seriously. If someone's frowning or looking at their phone, that's important."
>（提案があったら書き留めればいい。でもそれを真に受けるな。でも誰かが笑顔なら、それを真剣に受け止めろ。誰かがしかめ面をしていたり、スマホを見ていたら——それが重要なシグナルだ）

さらに、テスターからのフィードバックの受け取り方についても鋭い区別を示した。

> "If people tell you something feels wrong, I find that to be really, really good feedback. If they tell you how to fix it, I don't think... hey, you should figure out how to fix that."
>（「何かが間違っている気がする」と言われたら、それは本当に良いフィードバックだ。でも「こう直せば」と言われたら——いや、直し方を考えるのはあなたの仕事だ）

「問題の指摘」は信頼できるが、「解決策の提案」は鵜呑みにするな。これはソフトウェア開発のユーザーフィードバックの鉄則とも完全に一致する。

### ゲームの良し悪しを判断する「3ステップ」

Bryan は「ゲームが良いかどうかを判断する簡単な3ステップ」を披露した。

> "Any time I play a game with people... if they tell you 'It's nice, it's pretty good,' my game is bad, or at least not very exciting."
>（人とゲームを遊んで「いいね、まあまあだね」と言われたら——そのゲームはダメだ。少なくとも、あまりエキサイティングではない）

「いいね」は社交辞令だ。では、良いサインとは？

> "If they ask to play again at the end, you're on the right track. That's a very good sign."
>（終わった後に「もう1回やろう」と言われたら、正しい方向に進んでいる。とても良いサインだ）

しかし Bryan が本当に求める反応はさらにその先にある。

> "But really, the reaction I want is people pretending to take the game at the end of the play test. Like, that's really the sign. Or maybe the more polite version of that is, they ask you for a copy of the prototype."
>（でも本当に欲しい反応は、プレイテストの後にゲームを持って帰ろうとする人だ。それこそが本当のサインだ。あるいはもう少し礼儀正しいバージョンだと、プロトタイプのコピーを求められる）

プレイテスターがプロトタイプを「盗もう」とする——これがゲームの品質を証明する究極の指標だ。

### デザイン哲学: 足すより削れ

Bryan はゲームデザインの本質について、印象的な一言を残した。

> "I actually feel like a lot of the development process of games is getting rid of things."
>（ゲーム開発プロセスの多くは、実はものを削る作業だと感じている）

そして「プロセスの終わり」を判断する基準も示した。

> "Once you start making changes that are consistently making it worse, you're getting to the end of the process."
>（変更を加えても一貫してゲームが悪くなるようになったら——プロセスの終わりに近づいている）

これは収束のサインだ。改善の余地が減ってきたということは、ゲームが完成に近づいていることを意味する。

### イテレーションの速度——プレイテスト中に直せ

Bryan はイテレーションの速度について、直感に反するアドバイスを送った。

> "Take the feedback and iterate. As quick as you can. Don't get hung up on having to go through and make everything beautiful. If you're in the middle of the game and something is wrong, snatch the card out of the player's hands, write on it with a Sharpie."
>（フィードバックを受けたらすぐにイテレートしろ。できるだけ速く。すべてを美しくしなければならないという考えに囚われるな。ゲームの最中に何かが間違っていたら、プレイヤーの手からカードをひったくって、シャーピー（太字マーカー）で書き直せ）

これが、プロトタイプを美しく作らない理由だ。プレイテストの最中にマーカーで書き直せるようなプロトタイプこそが、最も効率的なイテレーションを可能にする。

---

## 4. パブリッシャーへのピッチ

### いつピッチすべきか

Bryan は「早すぎるピッチ」を強く戒めた。

> "You don't want to pitch me as soon as you have an idea. Most publishers who want games want them through some test stages, a little more robust."
>（アイデアを思いついた瞬間にピッチするな。ゲームを求めているパブリッシャーは、ある程度のテスト段階を経た、もう少しロバストなものを求めている）

では、いつが適切か。

> "Once your iteration has slowed down, then it's safe to start pitching. It doesn't need to be done. You don't need to be like 'it's final' or 'it's ready for the printer.' That's not what I'm saying."
>（イテレーションの速度が落ちてきたら、ピッチを始めて安全だ。完成している必要はない。「最終版だ」とか「印刷所に出せる状態だ」とは言っていない）

そして、プレイテストでのある体験がピッチの準備が整ったサインになるという。

> "If people ask to buy your prototype, that's a good sign that you're ready to pitch."
>（プレイテスターに「このプロトタイプを買いたい」と言われたら、ピッチの準備ができた良いサインだ）

さらに、プレイテストがピッチの練習にもなると指摘した。

> "This is another great thing about play testing, especially with strangers. You're going to have to pitch the game to balance and just sit down and get them to stay sitting down."
>（見知らぬ人とのプレイテストの素晴らしいところは、ゲームをピッチしなければならないことだ。座ってもらって、座り続けてもらう。それ自体がピッチの練習になる）

### セルシート（Sell Sheet）の構成

Bryan はセルシート（1〜2ページのゲーム紹介資料。パブリッシャーへの最初の接点となる）の具体例として、「River of Gold」のセルシートをスライドで紹介した。

> "So over on the right is the example of the sell sheet by... ultimately published as River of Gold."
>（右側は、最終的に River of Gold として出版されたゲームのセルシートの例です）

Bryan が推奨する構成：

| 要素 | 説明 |
|:---|:---|
| **ショートピッチ** | 冒頭に1-2文でゲームを説明。「最初に何を読むか」 |
| **箱裏スタッツ** | プレイ人数・時間・対象年齢。業界の全員がこの形式で書く（"Everyone in the tabletop industry writes games in this format"） |
| **基本プレイ概要** | どう遊ぶかの簡潔な説明。ルールブックではない |
| **プレイ中の写真** | プロトタイプの見た目が良くなくても、プレイの雰囲気が伝わる画像が重要（"Pictures plus what kind of play gives me that kind of feeling of the game very quickly"） |
| **コンポーネント一覧** | 内容物リスト。パブリッシャーが製造コストを推算するために必要 |
| **連絡先** | 必須。セルシートがゲームから離れて一人歩きすることがある |

> "Sell sheets are very important. It's lots of people ask, or just as the way in, if you don't already know, have a relationship with a publisher, this is the way to sort of make that contact."
>（セルシートは非常に重要だ。多くの人が求めるものであり、パブリッシャーとまだ関係がないなら、コンタクトを取る方法だ）

### エレベーターピッチ——30秒の勝負

Bryan はピッチ動画の重要性も強調した。

> "This needs to be a super short pitch."
>（超短いピッチでなければならない）

そして、多くのピッチ動画で見る過ちを指摘した。

> "I can't tell you how many times my attention has wavered in a pitch where it's like 30 seconds in, and they're still thanking me for their time and explaining... No, I clicked on your YouTube video because I was interested already. Just dive in."
>（何度注意力が途切れたか数え切れない。30秒経ってもまだ「お時間いただきありがとうございます」と言っている。いや、興味があるからYouTubeの動画をクリックしたんだ。さっさと本題に入れ）

パブリッシャーへの投稿プロセスも説明した。

> "If I get a blind submission, very much by process... the company email, what's in it? I think the email probably doesn't say much, and I open the sell sheet. And if the sell sheet grabs me, then I'll watch the pitch video. And I like both of those. My next step is to set up a time to play the game."
>（ブラインド投稿を受け取ると——会社のメールを開く。メール本文はたぶんあまり重要じゃない。セルシートを開く。セルシートが気になったらピッチ動画を見る。両方良ければ、次のステップはゲームを遊ぶ時間を設定すること）

つまり、パブリッシャーの関心は「階段状（stair step）」に上がっていく。メール → セルシート → ピッチ動画 → 実際のプレイ。各段階で脱落する可能性がある。

### レッドフラグ——パブリッシャーが警戒するピッチ

Bryan はパブリッシャーの視点から、ピッチで見る「レッドフラグ（警告信号）」を率直に列挙した。

#### 1. 完成度が高すぎる

> "If it's very polished, if it looks like they're about ready to launch a Kickstarter... I don't know if they want to work with a publisher or not."
>（非常に完成度が高い場合——Kickstarterを始める直前のように見える場合——パブリッシャーと組みたいのかどうかわからない）

パブリッシャーは「一緒に作る」ことを前提としている。すでに完成した製品を渡されても、変更や調整の余地がない。

> "Some publishers just want to get your print files... but from the publisher side, we're all very opinionated, we're going to want to tinker around with stuff, change stuff up, maybe re-theme the game."
>（印刷ファイルだけ欲しいパブリッシャーもいるが……パブリッシャー側からすると、我々はみんな意見を持っている。あちこちいじりたいし、変更したいし、もしかしたらゲームのテーマを変えたいかもしれない）

Bryan は具体的なアドバイスも添えた。「ゲームの見た目がデザイナーの頭の中で固まっているなら、自分で出版するか Asmodee のディストリビューター経由で流通させることを検討した方がいい」。

#### 2. メカニクスの詰め込みすぎ

> "You can have like every mechanical genre, certain word, sort of popular right now on your sell sheet. That's, I don't know, it just means you like this stuff. It's like, 'oh, it's an area control, social deduction, resource management, time-travel game.' That's not a compelling game, those are just words."
>（セルシートに今流行りのメカニクスの用語を全部盛り込むことはできる。でもそれは……ただそういうものが好きだということを意味するだけだ。「エリアコントロール、ソーシャル・デダクション、リソースマネジメント、タイムトラベルゲーム」——それは魅力的なゲームではない。ただの言葉の羅列だ）

焦点の定まらないゲームは、何が面白いのかが伝わらない。

#### 3. AI生成に見える

> "If the game looks like it was mostly made by generative AI, I'm not very interested. I'm looking to work with designers with original ideas. I have power for art. I don't really want to publish systems. I want to publish one really great game."
>（ゲームが主に生成AIで作られたように見えたら、あまり興味がない。オリジナルのアイデアを持つデザイナーと一緒に仕事したい。アートのリソースは持っている。システムを出版したいわけじゃない。本当に素晴らしいゲームを1つ出版したいんだ）

これは2026年のGDCならではの発言だ。AI生成ツールが普及した現在、「人間のデザイナーの創造性」がより重視されるようになっている。

#### 4. 最初からシステム展開を前提にしている

Bryan は Asmodee の自社タイトルを例に、興味深い洞察を示した。

> "How many Ticket to Rides are there? And Pandemic is... our systems are like... none of those was pitched as a system. Like surely you go around and you tie us from all the great civilizations. No, it was just a game that was successful, and publishers are creative, you can build out from a really great game."
>（チケット・トゥ・ライドは何種類あるか？ パンデミックは……我々のシリーズはすべて——どれもシリーズとしてピッチされたわけではない。「世界中の偉大な文明を巡るシリーズです」みたいなピッチではなかった。ただ1つの成功したゲームがあって、パブリッシャーがクリエイティブに展開した結果だ）

> "If you tell me it's like, 'oh, we can put 30 different licenses on this idea,' I don't know, you're more on the product... I'm on the games."
>（「このアイデアに30の異なるライセンスを載せられます」と言われたら——あなたはプロダクトの話をしている。私はゲームの話をしたい）

### パブリッシャーとの協業——デザイナーの役割

Bryan はパブリッシャーとの協業について、デザイナーの立場からの心構えを語った。

> "Your job as a designer is to keep them true to whatever your vision for the game was, and don't let it be something that it's not."
>（デザイナーとしてのあなたの仕事は、パブリッシャーをあなたのゲームのビジョンに忠実であり続けさせることだ。ゲームを本来のものとは違うものにさせないこと）

しかし同時に、パブリッシャーの提案に対するオープンさも重要だという。

> "You're going to have the most experience with the game for a long time in the process, so hopefully they'll go back to you. Your gut reaction as they start showing you things is super, super valuable."
>（プロセスの中で長い間、そのゲームについて最も経験がある人はあなただ。だからパブリッシャーはあなたに相談してくるはずだ。彼らが何かを見せてきたときの、あなたの直感的な反応は、ものすごく価値がある）

パブリッシャーがアートワークやコンポーネントのデザインを見せてきたとき、「何か違う」という直感は大事にすべきだ。デザイナーが100回プレイテストした経験から来る直感は、パブリッシャーにとっても貴重な指針になる。

一方で、パブリッシャー側の責任も明確にした。

> "It's the publishers' job ultimately to make a product. They're going to take the financial risk and build the logistics."
>（最終的に製品を作るのはパブリッシャーの仕事だ。財務リスクを取り、ロジスティクスを構築するのは彼らだ）

> "If you want to take the financial risk and build the logistics, then maybe you want to be a publisher."
>（もし自分で財務リスクを取りたい、ロジスティクスを構築したいなら——あなたはパブリッシャーになりたいのかもしれない）

---

## 5. 業界の心得——上級者向けアドバイス

セッションの終盤、Bryan は「上級者向けの考え（advanced thoughts）」として、業界で生き残るための心得を語った。

### 橋を燃やすな

> "I've been in the industry for a couple decades, and it's much smaller than you think. So burning a bridge in the tabletop industry really spreads fast."
>（この業界に20年以上いるが、あなたが思っているよりずっと小さい。テーブルトップ業界で橋を燃やすと、本当に速く広がる）

そして、業界に集まる人の動機について、美しい一言を残した。

> "Most of us are not here to buy yachts. We really love it."
>（我々のほとんどはヨットを買うためにここにいるんじゃない。本当にこれが好きなんだ）

だからこそ——

> "Treat people well, and that will get around."
>（人を大切に扱え。それは広まる）

### 小さく始める——ゲームマーケットモデル

Bryan は、最初の一歩として興味深いルートを提案した。

> "A nice model is these game markets, where a designer prints 20 or 50 copies of their game and sells them for about $10. That's a nice way to get your feet wet, and get your game out there, and get responses."
>（ゲームマーケットのようなモデルが良い。デザイナーが20〜50部を印刷して、10ドルくらいで売る。足を濡らすのにいい方法だし、ゲームを世に出してフィードバックを得られる）

これはまさに日本のゲームマーケットで毎年行われていることだ。欧米にも同様の文化があり、Bryan はそのルートからパブリッシャーの目に留まるゲームもあると付け加えた。「ゲームマーケットを経由してパブリッシャーに注目されるゲームもある（certainly some games that you think about publishers that way too）」。

### ホームストアを持て

> "I very much recommend having a retailer, that's your home retailer. You don't have to buy a ton of games from them, but just go there, know the person who runs it, ask for permission to play test your games there."
>（「ホームリテイラー」——行きつけの店を持つことを強く勧める。大量に買う必要はない。ただ通って、店主と顔見知りになって、プレイテストの許可をもらう）

そしてその関係は、ゲームデザインのキャリアが進むにつれて大きな支えになるという。

> "As you advance through your game design, they'll support you."
>（ゲームデザインのキャリアが進むにつれて、彼らはあなたを支えてくれる）

### リソースの活用

Bryan はセッションの最後に、活用すべきリソースを紹介した。

> "Some good resources out there for games. I have a chart for reading, for listening. There are sessions and tools and all of these sorts of things. Take advantage of what's out there."
>（ゲームのための良いリソースがある。読むもの、聴くもののチャートがある。セッションやツール、そういうものすべて。世の中にあるものを活用しろ）

そして、テーブルトップゲーム業界の文化そのものが「共有と教育」の精神に根ざしていることを改めて強調した。

> "I think there's something about the tabletop industry, you know, you have to teach people games to play games, and it just sticks with us as we go through the industry and we want to keep sharing and helping."
>（テーブルトップ業界には何か特別なものがある。人にゲームを教えなければゲームが遊べない。その精神が業界を通じて我々に染み付いていて、共有し続けたい、助け続けたいと思うのだ）

---

## まとめ

| テーマ | キーメッセージ |
|:---|:---|
| **業界構造** | 4層（小売・卸売・出版・デザイナー）は重複し、驚くほど小さなコミュニティ |
| **プロトタイプ** | 速く作れ、美しくするな。500作のクニツィアでもプロトタイプは「最悪の芸術作品」 |
| **プレイテスト** | 6段階で広げる。言葉より行動・感情を観察。問題の指摘は信頼し、解決策の提案は疑え |
| **デザイン哲学** | ゲーム開発の多くは「削る作業」。変更が悪化を招くようになったら終わりが近い |
| **ピッチ** | 30秒で本題に入れ。セルシート → ピッチ動画 → 実プレイと段階的に関心を引く |
| **レッドフラグ** | 完成度が高すぎる、メカニクス詰め込み、AI生成、システム前提のピッチは警戒される |
| **業界の心得** | 橋を燃やすな。みんなヨットを買いたいわけじゃない。本当に好きだからやっている |
| **最初の一歩** | 20-50部を印刷して売る「ゲームマーケット」モデルと、行きつけの店（ホームストア）を持つこと |

デジタルゲーム開発者にとっても、ゲームデザインの本質——**プロトタイプを速く作り、テストし、削り、人とつながる**——は変わらない普遍的な教訓だった。Bryan のセッションは、テーブルトップとデジタルの垣根を超えた「ものづくりの原則」を思い出させてくれるものだった。

> "Most of us are not here to buy yachts. We really love it."

---

## 参考リンク

- [GDC 2026 Tabletop Summit](https://gdconf.com/)
- [Asmodee 公式サイト](https://www.asmodee.com/)
- [BoardGameGeek（BGG）](https://boardgamegeek.com/)
