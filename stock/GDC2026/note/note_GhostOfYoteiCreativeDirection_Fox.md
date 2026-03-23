# GDC 2026: Does It Make You Feel Like a Wandering Ronin? — Ghost of Yotei のクリエイティブディレクション全記録

「このフィーチャーは本当にゲームに必要なのか？」――ゲーム開発の現場では、魅力的なアイデアが次々と生まれる一方で、何を残し何を捨てるかの判断が日々求められる。Ghost of Tsushima の続編として開発された Ghost of Yotei では、Sucker Punch Productions の共同クリエイティブディレクター Nate Fox と Jason Connell がたった一つの問い――**"Does it make you feel like a wandering ronin?"（さすらいの浪人の気分にさせるか？）**――をすべての意思決定の基準に据えた。

季節変化システムの全面カット、Breath of the Wild に触発されたロッククライミングの撤回、ダイエジェティックマップ（ゲーム世界内に実在する物として描くUI手法）の断念。大胆なプロトタイプを作っては壊し、失敗からエッセンスだけを救い出す。GDC 2026 の現地で聴講した約60分のセッションを、トランスクリプトに基づいて詳細にレポートする。

> **日本の読者へ**: Ghost of Tsushima / Yotei は、アメリカ・ワシントン州のスタジオが日本の時代劇を題材にオープンワールドゲームを制作するという、いわば「逆輸入」的なプロジェクトだ。元寇の対馬を舞台にした前作は、日本のプレイヤーからも高い評価を受け、対馬市から感謝状が贈られるほどの文化的インパクトを生んだ。続編の舞台は北海道・羊蹄山（蝦夷富士）周辺。欧米の開発者が日本の歴史と美意識をどう解釈し、クリエイティブの判断基準へどう落とし込んでいるのか――本セッションはその舞台裏を覗ける貴重な機会だった。

---

## セッション概要

| 項目 | 内容 |
|:---|:---|
| セッション名 | Does It Make You Feel Like a Wandering Ronin? Creative Direction for Ghost of Yotei |
| スピーカー | Nate Fox / Jason Connell（Sucker Punch Productions, Co-Creative Directors） |
| 日時 | 2026年3月9日（月）15:10 - 16:10 |
| 会場 | Moscone Center West Hall, Room 2005 |
| トラック | Design / Team Leadership |
| レベル | Entry-Level |

セッションは6部構成で、スタジオ文化の紹介から始まり、ビジョンの策定、プロトタイピング、チームコミュニケーション、テスト、そしてトレーラー制作まで、Ghost of Yotei の開発プロセス全体を時系列で追う内容だった。

![セッション開始前の会場。スクリーンには「Does it make you feel like a wandering Ronin?」のタイトルスライドが映し出されている](source/images/IMG_4545.jpg)

---

### この記事で読めること

- **Part I: Sucker Punch のスタジオカルチャー**
- **Part II: ビジョンの策定**
- **Part III: プロトタイピング**
- **Part IV: チームとの対話**
- **Part V: カットの文化**
- **Part VI: テストとトレーラー**
- **まとめ**
- ...ほか全9セクション

> **本記事のボリューム**: 約27,803文字 / スライド画像2枚
> スピーカーのトランスクリプト（発言の文字起こし）を原文・日本語訳つきで完全収録しています。

---

<!-- ===== ここから有料エリア（Note エディタで有料ラインを設定） ===== -->

## Part I: Sucker Punch のスタジオカルチャー — 「小さなチームで大きなゲームを」

### 二人のクリエイティブディレクター

![登壇する Nate Fox（左）と Jason Connell（右）。GDC Festival of Gaming のステージにて](source/images/IMG_4546.jpg)

Sucker Punch Productions は創業28年、ワシントン州ベルビューに本社を置く Sony Interactive Entertainment（SIE）傘下のファーストパーティスタジオだ。Rocket Robot on Wheels（N64）から Sly Cooper シリーズ（PS2/PS3）、inFAMOUS シリーズ（PS3/PS4）と、PlayStation プラットフォームを中心にアクションゲームを手がけてきた。2020年発売の Ghost of Tsushima は全世界1,200万本以上を売り上げ、日本国内でも「日本人が作ったと思った」という声が上がるほどの文化的リサーチの深さで知られる。

Nate Fox はスタジオ歴27年、ゲームプレイとナラティブ（物語体験の設計）を担当する。自己紹介で印象的だったのは、ゲームデザイナーを目指す若者への助言だ。

> *"I believe the best experience young people can have, the comparison to becoming a game designer, is a background in theater where as a young person, you can really screw the pooch by not thinking about what the audience cares about most. Making games, we've got to always think about how people will see what you're doing."*
>（ゲームデザイナーを目指すなら、演劇のバックグラウンドが最良の経験だと信じている。演劇では「観客が何を一番気にしているか」を考えずにいると、本当にしくじる。ゲームを作るときも常に、プレイヤーが自分のやっていることをどう見るかを考えなければならない）

Jason Connell はスタジオ歴17年。コンピュータサイエンスの学位を持ち、Ghost of Tsushima ではアートディレクター（ビジュアル面の方向性決定）として参加した。二人は12〜13年にわたって共同クリエイティブディレクターとして、inFAMOUS Second Son / First Light から Ghost シリーズまでを手がけてきた。

### 意図的に「小さい」を選ぶ

Connell はスタジオの規模について率直に語った。

> *"We are very intentionally slow growing. We are not trying to be super big. I think we're like 150 people or something like that with our full-time staff."*
>（私たちは意図的にゆっくり成長している。巨大になろうとはしていない。フルタイムスタッフは150人くらいだ）

> *"I understand that we say that we're making big games with a small team, asterisk — there's people here making games by themselves. But for us, in terms of our peers in the industry and other studios, we like to be on the smaller side. It really forces us and pressures us to make really good decisions."*
>（「小さいチームで大きなゲームを」と言うのは、一人でゲームを作っている人がいる中で申し訳ないが、業界の他のAAAスタジオと比較すれば小さい側にいたい。小さいからこそ、良い判断をすることを強いられる）

### クリエイティブの指針 — 三つの原則

Connell はスタジオのクリエイティブ哲学として三つの原則を紹介した。

**1.「What's best for the game?（ゲームにとって何が最善か？）」**

> *"If there are two people that want to run features, what's best for the game is sort of an easy way to put everything else aside."*
>（二人がそれぞれ別のフィーチャーをやりたいとき、「ゲームにとって何が最善か」と問えば、他のすべてを脇に置ける）

**2.「クリエイティブにはフラット」**

Sucker Punch は組織としてはフラットではないが、クリエイティブにおいてはフラットであろうとしている。

> *"Anyone in the studio can go to anyone about any feature. There's people on the QA team who talk about combat, or somebody on the art team wants to say they're having an idea about the story."*
>（QAチームのメンバーがコンバットについて意見を言い、アートチームの誰かがストーリーのアイデアを持ち込む。これがうちの文化だ）

ただし全員参加の投票で意思決定することは避けている。小規模チームだからこそ「アクション志向」でなければ開発が止まる。

> *"You can talk all day about whether you should go to the beach or the mountains, but you can just also pick one and decide to suck it up. Making a decision is so much better — getting it in the game is what matters most."*
>（ビーチか山か一日中議論できるが、どちらかを選んで腹をくくることもできる。決断する方がずっといい。ゲームに入れることが最も重要だ）

**3. ファンタジーステートメント（ビジョン宣言）**

そしてセッション全体を貫く核心が、ゲームの本質を一文で表現する**ファンタジーステートメント**の文化だ。これは Sucker Punch の DNA に組み込まれたものであり、Ghost のために新たに発明したものではない。

Connell は過去タイトルのステートメントを振り返った。

| タイトル | ステートメント |
|:---|:---|
| Sly Cooper | *"That which is most devious wins"*（最も狡猾なものが勝つ） |
| Ghost of Tsushima | *"Transport people to feudal Japan"*（封建時代の日本にタイムスリップさせる） |
| **Ghost of Yotei** | **"Does it make you feel like a wandering ronin?"**（さすらいの浪人の気分にさせるか？） |

> *"We really want to make big games, but we don't want to grow our studio exponentially. We need unified, narrow vision statements."*
>（大きなゲームを作りたいが、スタジオを指数関数的に拡大はしたくない。だからこそ統一された、狭いビジョンステートメントが必要だ）

---

## Part II: ビジョンの策定 — 「さすらいの浪人」の発見

### Ghost of Tsushima の DNA を解剖する

Ghost of Tsushima を出荷した後、チームはまず「あのゲームの本質は何だったのか」を自問した。

> *"What exactly made Ghost of Tsushima, Ghost of Tsushima? What was the center mass DNA of that game, especially when we're looking at making a sequel?"*
>（Ghost of Tsushima を Ghost of Tsushima たらしめていたものは何か？　続編を作るならなおさら、その中心的 DNA を理解しなければならない）

答えは「さすらいの侍」だった。オープンワールドを自由に駆け回り、遠くに見える寺院に向かって冒険し、問題を刀の腕で解決する。Fox はこう言った。

> *"You are out there moving around. Crucially, you have that weapon on your hip, and you solve problems with expert swordsmanship. But it's not just that — it's also the freedom to explore. We want to empower your curiosity so that you can see something like that temple in the distance, know that there's something there to discover."*
>（あなたはそこにいて、動き回っている。重要なのは腰に刀を帯びていること。卓越した剣術で問題を解決する。しかしそれだけじゃない。探索の自由もある。遠くに見える寺に何かがあると感じ、好奇心に従って発見する）

映画的なインスピレーション、「文化のタイムマシン」としての真正性。これらは前作で培われた「茶色い部分（継承するもの）」だ。しかしセッションの主題は**赤い部分（新しく加えるもの）**にある。

### 蝦夷（Ezo）を舞台に選んだ理由

Fox は続編の舞台に北海道（当時の蝦夷地）を選んだ理由を三つ挙げた。

> *"It is unbelievably beautiful. It's vast and it's incredibly diverse."*
>（信じられないほど美しい。広大で、信じられないほど多様だ）

> *"Back in the day, it was very far north past the rule of law of mainland Japan."*
>（当時は本土日本の法の支配のはるか北にあった）

> *"You have no master protecting you. You have to take care of yourself. And if you're in a landscape that's beyond the rule of law, and crucially, there is a price on your head — suddenly, the world is full of danger. People are out to get you."*
>（主人を持たない浪人として、自分の身は自分で守らなければならない。法の及ばない土地で、しかも自分の首に賞金がかかっている。突如として世界は危険に満ちる。人々があなたを狙っている）

法の支配が及ばない蝦夷地で、賞金首の浪人として一人旅をする。前作の仁（Jin）は武士の掟を破ることに苦悩したが、GoY の浪人アツ（Atsu）は自由と危険の中に生きる。この対比が、続編としての明確なアイデンティティを作り出した。

主人公についても Fox は語った。

> *"We wanted to tell a new origin story, a new hero, a new legend. It's exciting for us as game makers — we think it's exciting for players because they get to be there at the ground level for a new journey to become a legendary ghost."*
>（新しいオリジンストーリー、新しいヒーロー、新しい伝説を語りたかった。プレイヤーにとっても、伝説の「ゴースト」になる旅の出発点に立てるのはエキサイティングだ）

> *"What's new is a character you're going to meet as a youngster and understand her motivations for what befell her as an adult on her quest for revenge."*
>（新しいのは、幼い頃に出会い、大人になった彼女が復讐に駆り立てられる動機を理解するという構造だ）

### 「ポップクイズ」 — ビジョンステートメントの使い方

Fox はセッション中、聴衆にビジョンステートメントの使い方を体験させた。

> *"Going to a production of Noh theater, or a smelly gambling den — which makes you feel more like a ronin? You're right, it's the gambling den."*
>（能の公演を観に行くのと、薄汚い賭場と。どちらがより浪人の気分にさせるか？　正解、賭場だ）

> *"Leading a cavalry charge, or sneaking around and killing thieves who are ripping off somebody else to make a buck — which makes you feel more like a wandering ronin? The second one."*
>（騎兵隊の突撃を指揮するのと、他人から金を巻き上げている盗賊を忍んで仕留めるのと。浪人の気分にさせるのは後者だ）

このシンプルな二択の問いが、チーム内のあらゆるフィーチャー議論で繰り返し使われた。

### ピラーシート — 「ホットなスプレッドシート」

ビジョンが固まると、Fox と Connell はピラーシート（pillar sheet）と呼ぶスプレッドシートを作成した。

> *"Put that at the top of this — that's right friends, a spreadsheet. Oh, I know. It's so hot."*
>（ファンタジーステートメントをこの一番上に置く。そう、友よ、スプレッドシートだ。ホットだろ？）

会場は笑いに包まれた。しかしこのスプレッドシートこそが、チーム全体の意思決定エンジンとなる。左列にはゲームを構成する要素が優先順位順に並ぶ。Fox は聴衆に「さすらいの浪人のファンタジーで最も重要なものは？」と問いかけた。

> *"Yes, it's combat. That's what you want to do — you want to get in fights."*
>（そう、コンバットだ。戦いたいだろう？）

ピラーシートの構成は以下の通りだ。

| 優先度 | ピラー | 説明 |
|:---|:---|:---|
| 最高 | **コンバット** | 新しい近接武器・投擲武器を追加 |
| 高 | **探索** | 好奇心を報いる。見つけた先に必ず何かがある |
| 高 | **真正性** | 時代劇としてのタイムマシン体験 |
| 高 | **Co-op（協力プレイ）** | 七人の侍的な「仲間と共に戦う」感覚 |
| 中 | **アートのトーン** | ビジュアルでゲームの雰囲気を語る |
| 中 | **サイドコンテンツの厚み** | ゴールデンパスより高い優先度 |
| 中 | **バレーの多様性** | 各谷が異なるフレーバーとボスを持つ |
| 中低 | **ゴールデンパス** | メインストーリー。サイドコンテンツの下 |
| 低 | **キャラクターカスタマイズ** | 自分だけの浪人を作る |
| 低 | **浪人ミニゲーム** | 賭場など世界観に溶け込む遊び |
| 低 | **アクセシビリティ** | 幅広いプレイヤーに対応 |
| 最低 | **ストリームライン** | カット対象。浪人体験に貢献しないもの |

注目すべきは、**ゴールデンパス（メインストーリー）がサイドコンテンツより低い優先度**に置かれていることだ。

> *"We put more energy as a team in having things for you to potentially ride right by and not find. But if you do find them under your own curiosity, it's more of your own experience."*
>（プレイヤーが馬で素通りして見つけないかもしれないものに、チームとしてより多くのエネルギーを注いだ。しかし自分の好奇心で見つけたなら、それはより「自分だけの体験」になる）

探索で見つけた報酬は、ゲームプレイを変える実質的なものだ。例えば槍の達人に出会えば、新しい戦闘スタイルを習得できる。これはゴールデンパスにはなく、完全にオプショナルだ。

> *"This dude with the spear in our game — you can learn how to fight with the spear and it changes the way you attack enemies. And it's not on the Golden Path, it's completely optional. By exploring, you get paid for it."*
>（槍を持ったこの男。槍の戦い方を学ぶと攻撃スタイルが変わる。ゴールデンパスにはない、完全にオプショナルだ。探索への報酬だ）

ピラーシートは**リスク評価**にも使われた。上位の項目は前作で実績があり低リスク。中位の「コンバット拡張」は新武器の追加でリスク中程度。下位の新規フィーチャーは未経験領域で高リスク。

> *"By having a mix of things that we think we've got a good handle on all the way to things that we're going to work a lot harder to figure out, we can understand where we're taking our risk."*
>（熟知しているものから、もっと苦労して解決しなければならないものまで混在させることで、どこにリスクを取っているかが見える）

そして最も重要なルール。

> *"If it's not on this list, we're not going to fund it in the game."*
>（このリストに載っていないものは、ゲームに投資しない）

Co-op（協力プレイ）について Fox はこう補足した。

> *"I know this is not necessarily lone wanderer territory. But we think that feeling of fighting alongside your friends is frankly classic to the genre. If you've ever watched Seven Samurai, you'll know what I'm talking about — the feeling of brotherhood."*
>（孤独な放浪者の領域ではないかもしれない。しかし仲間と共に戦う感覚は、率直に言ってこのジャンルの古典だ。七人の侍を見たことがあれば、わかるだろう。兄弟の絆だ）

そしてさりげなく告知が挟まれた。

> *"Today is March 10th — tomorrow, Ghost of Yotei Legends comes out free."*
>（今日は3月10日。明日、Ghost of Yotei Legends が無料で出る）

---

## Part III: プロトタイピング — 大胆に作り、潔く壊す

ピラーシートが固まると、8〜15人の小規模チームがプロトタイピングに入った。Connell はこのフェーズを率直に振り返った。

> *"We were a bit underfunded. We should have done a better job there."*
>（少し投資不足だった。もう少しうまくやるべきだった）

しかし得られた学びは大きかった。以下、4つのケーススタディを詳述する。

### ケーススタディ1: メモリーフリップ（季節変化）— ピッチの花形がカットされるまで

GoY の初期ピッチで最大の目玉だったのが、**ゲーム全体にまたがる季節変化メカニクス**だった。Connell がその構想を語った。

> *"Anywhere in the game world, when you're playing as Atsu — this lone, revengeful-seeking ronin traveling the dark cold winter of Hokkaido — and you can press this button and it can flip to a moment and be back in the spring of her youth with her brother nearby. And we aimed to have this everywhere."*
>（ゲームワールドのどこにいても、アツとしてプレイ中に――復讐を求めてさまよう孤独な浪人が北海道の暗く寒い冬を旅しているときに――ボタン一つで春に切り替わり、幼い頃の記憶の中、兄弟がそばにいる世界に戻れる。これをゲーム全域で実現するつもりだった）

プロトタイプは非常に早い段階で制作された。まだアツのモデルが完成しておらず、前作の仁（Jin）のアセットを流用していた。Fox のナレーション付きの映像がチーム内で共有された。これはビジョンを伝えるコミュニケーションツールとしても機能した。

映像で Fox が語ったナラティブのコンセプト。

> *"In her memories, she's always there with her brother. The two traveled together. But as an adult, there is no brother. She's alone and broken."*
>（記憶の中では、いつも兄弟と一緒だ。二人は共に旅をした。しかし大人になった今、兄弟はいない。彼女は一人で、壊れている）

子供時代の記憶には壮大な杉の木が立ち、寺院の参道は活気に満ち、五重塔には人々の暮らしがあった。大人として戻ると、杉は切り倒され、参道は荒れ果て、五重塔は空っぽだ。

> *"By going back and forth into her memories, the player creates questions in their mind. And it's as simple as that — it's instant with the button press."*
>（記憶を行き来することで、プレイヤーの心に問いが生まれる。ボタンを押すだけ。それだけのことだ）

技術的には、同じフィデリティ（忠実度）のアセットを二世界分作る必要があった。チームの反応は率直だった。

> *"Our team heard about it and was like, wow — we're going to have to create half as big of a game."*
>（チームの反応は「うわ、ゲームの規模を半分にしないといけない」だった）

そして核心の問い。

> *"It doesn't make you feel like a wandering ronin. Not really. It's a very cool, potentially very flashy narrative tool."*
>（浪人の気分にさせない。正直なところ。クールで派手になりうるナラティブツールではあるが）

判断は明確だった。ゲーム全域への展開を断念し、**主人公の故郷（homestead）に限定したナラティブツール**に転用した。トラウマが起きた場所で、ボタンを押せばいつでも母・弟・父との思い出に戻れる。かつて美しかった木は今や焼け焦げ、誰かに傷つけられている。

> *"We focus on bringing this feature straight to her home, which is where all the trauma happens. You can press the button at any time — you can see your mom, see your little brother, see your dad, see how the tree used to be beautiful and now it's all scarred."*
>（このフィーチャーを彼女の故郷に集中させた。すべてのトラウマが起きた場所だ。いつでもボタンを押せる。母が見える。弟が見える。父が見える。かつて美しかった木が、今や傷だらけになっているのが見える）

最終的には、出荷直前でこの機能自体がカットされた。ゲーム全体にまたがるフィーチャーではなくなった以上、投資に見合わないと判断されたのだ。

### ケーススタディ2: ロッククライミング — BotW への憧れと「偽陽性」の罠

Fox は会場にこう問いかけた。

> *"Who here played Breath of the Wild and thought every game should feature climb-on-anything, stamina runs down and you fall to your death? I know I did."*
>（ブレス オブ ザ ワイルドをプレイして「すべてのゲームにどこでも登れてスタミナが減って落下死する仕組みを入れるべきだ」と思った人は？　私はそう思った）

チームはすぐにプロトタイプに着手した。スタミナバーの設計、登る方向の選択、壮大な仕組みになるはずだった。

> *"We got right in there and started prototyping with a stamina bar and how to choose where she was going to go. It was going to be magnificent. Unfortunately, it wasn't."*
>（すぐにプロトタイプ作りを始めた。スタミナバー、行き先の選択。壮大なものになるはずだった。残念ながら、そうはならなかった）

まず Ghost of Tsushima のマップ上でテストし、次に蝦夷地（Ezo）のマップに持ち込んだ。しかし蝦夷地のマップは多様な谷地形を意図して設計されており、チョークポイント（隘路）や「登るべきでない壁」が大量に存在していた。

> *"Because of us trying to make a variety of different valleys, there were a lot of choke points and frankly walls that you weren't supposed to climb. And if we gave you a tool that said you can climb anywhere, and you started using it, you would find dead ends. It pretty much trained you to stop climbing because exploration wasn't worth it."*
>（多様な谷を作ろうとしたために、チョークポイントや登るべきでない壁がたくさんあった。「どこでも登れる」ツールを使い始めると、行き止まりだらけだ。探索する価値がないから、プレイヤーは登ることをやめるように訓練されてしまう）

Fox はこれを**「偽陽性（false positives）」**と呼んだ。登れるという期待を与えておきながら報酬がない。

チームは前作の方式に戻ることを決断した。

> *"If you see that beautiful white bird move, it means that you can climb, and there's always guaranteed something worth climbing to find. This really helps people feel like, oh, it's going to be worth my time to explore."*
>（あの美しい白い鳥のマークが見えたら、登れる場所で、必ず登った先に何か価値あるものがある。これがあると「探索する価値がある」と感じてもらえる）

そして Fox は自戒を込めて締めくくった。

> *"Frankly, we should have known ahead of time because guess what's not on the pillar sheet? Rock climbing is not a core aspect of being a wandering ronin."*
>（正直、事前にわかっていたはずだ。ピラーシートに載っていないもの、それがロッククライミングだ。浪人の核心的要素ではない）

### ケーススタディ3: ワールドスケール — 「自由」をどう感じさせるか

ピラーシートの「自由（freedom）」と「放浪（wandering）」をどう融合させるか。非常に早い段階で、大胆な実験が行われた。

> *"Very early on, we're like, okay, how free can we be? We just skip story stuff, drop you straight into the game and say, go to this shrine. See if you can pick your enemy based off of these cards and then the wind will guide you to whatever region."*
>（ストーリーをスキップして、いきなりゲームに放り込む。「この祠に行け」とだけ伝え、カードで敵を選ぶと風が導いてくれる）

> *"It was a pretty bold idea for us at the time. We found that it didn't really give us all the context we needed, but there was a lot of really great learnings — mostly in that we liked how big the world felt."*
>（当時としてはかなり大胆なアイデアだった。必要な文脈は足りなかったが、「世界がどれだけ大きく感じられるか」という大きな学びがあった）

この学びは北海道への取材旅行の印象とも一致した。北海道は広大で、常にどこかに壮大な山が見える。ここから方針が転換される。チームの強みであるビジュアルとアトモスフェアで「放浪感」を表現するアプローチだ。

> *"We started right away just putting out terrain and windy cool grass and atmospheres and pull back the camera a little bit just to try to see what it would feel like to traverse this big space."*
>（テレインを配置し、風に揺れる草とアトモスフェアを加え、カメラを少し引いて、この広大な空間を横断するとどう感じるかを試した）

> *"Getting it on sticks, seeing what it feels like, putting some light music to it, maybe taking some of our other atmospherics that we have built for Ghost of Tsushima — rain and things like that. What does it feel like to traverse this?"*
>（コントローラーで操作し、感覚を確かめ、軽い音楽を載せ、前作で作った雨などのアトモスフェリクスを持ってきて重ねる。この空間を横断するとどう感じるか？）

ある段階で「広すぎてコンテンツを埋められない」と気づく。最終的には前作よりはるかに広大だが、密度と美しさを維持するスイートスポットを見つけた。

広い平原にはゲームメカニクスが埋め込まれた。

> *"If you stay in the rivers of flowers, you get a speed boost because she likes the flowers. It's a really beautiful experience — the white petals going up."*
>（花の川沿いを走るとスピードブーストがかかる。彼女が花を好きだから。白い花びらが舞い上がる、本当に美しい体験だ）

また、広大なサイトライン（見通し）を活かすため、前作にはなかった**スパイグラス（望遠鏡）**が追加された。ダイエジェティック（ゲーム世界内に実在するもの）なツールとして、遠くの目標を発見する探索メカニクスだ。

### ケーススタディ4: ダイエジェティックマップとジャーナル — 「うまくいかなかったもの」

Connell はトーンを変えた。

> *"Let me show you about some stuff that sucked."*
>（ダメだったものを見せよう）

Ghost of Tsushima で風がプレイヤーをガイドする仕組みが高く評価された。GoY ではさらに踏み込み、「通常のビデオゲームのマップに頼らない」方向を模索した。

**試み1: ダイエジェティックマップ**

プレイヤーの向きに合わせてマップが回転する、ゲーム世界内で手に持って見る地図を制作した。

> *"It was a really cool prototype, but it didn't make you feel like a wandering ronin. I think it probably made you feel like you need to throw up."*
>（本当にクールなプロトタイプだったが、浪人の気分にはさせなかった。おそらく吐き気を催す気分にさせた）

しかしポーズ画面に遷移しなくていい没入感には価値があったため、すぐには諦めず次のイテレーションへ。

**試み2: ジャーナル（日記帳）**

主人公が常に書き込んでいるジャーナルを UI の代替として使うアイデア。しかし致命的な問題があった。

> *"We have a game rule: if you're in the game world looking at any kind of writing, it's always in the time period appropriate way it should be written."*
>（ゲーム内のルールがある。ゲーム世界で文字を見るときは、常にその時代にふさわしい表記でなければならない）

Ghost シリーズでは、ゲーム世界内のすべてのテキストが漢字や当時の書体で統一されている。ジャーナルにローカライズされたテキストを表示すれば、この世界観ルールが崩壊する。日本文化の真正性をゲームの核心に据えるタイトルとして、これは受け入れられない妥協だった。

**最終判断: ポーズ画面マップに回帰、ただし実験のエッセンスを反映**

チームはポーズ画面でのマップ表示に戻した。しかし単なる撤退ではなく、失敗した実験の学びを活かしてアップグレードした。

> *"All of the decisions we made to make it feel like it was hand drawn. It's less sophisticated, a little bit more hand drawn. Or if somebody gives you a map in the world, you can actually place it — it's not just automatically populated. If it's raining outside, the little rain splashes on the map. If it turns dark outside, it turns dark. Just to try to make it feel like it's not just a user interface."*
>（手描き風に感じさせるあらゆる工夫を入れた。洗練されすぎず、もう少し手描きっぽく。ゲーム世界で誰かから地図を貰ったら、自動でマップに表示されるのではなく自分で配置する。外が雨なら雨の染みがつき、暗くなればマップも暗くなる。単なるユーザーインターフェースではないと感じさせるために）

ジャーナルのアイデアも形を変えて生き残った。**トピックカード（Topic Cards）**として、主人公がミッションの話を聞くたびに小さなカードに書き留める形式が実装された。

> *"We actually came up with these things called Topic Cards, which kept a list of all these — anytime she'd hear about a mission, she'd write a little card. You can flip through them all."*
>（トピックカードというものを考案した。ミッションの話を聞くたびに小さなカードに書き留める。全部めくって見られる）

カードはカテゴリ分けされ、それぞれに美しいアートワークが描かれた。漢字表記問題を回避しつつ、世界観に溶け込む UI を実現した。

---

## Part IV: チームとの対話 — ビジョンを150人に届ける方法

### 6週間マイルストーン構造

Sucker Punch の開発は6週間ごとのマイルストーン構造で管理されている。

> *"Our team is built on a milestone structure where we have six weeks per milestone. They're made up of weekly town halls, and then on that sixth week, it's the big show."*
>（6週間ごとのマイルストーン構造だ。毎週のタウンホールがあり、6週目がビッグショーだ）

週次のタウンホールは軽量なアップデートの場。新メンバー紹介やポリシー変更、さらにはキッチンの新しいスナック情報まで共有される。一方、6週間マイルストーンミーティングでは、チームリードが目標の達成・未達を報告し、アニメーションの進捗映像や新レンダリング機能のデモが披露される。

具体例として Ninja Valley セクションの開発計画が示された。4つのマイルストーン（各6週間、合計24週間）でコンテンツ完成を目指す。Pre-Alpha 終了時点で「すべてのバレーのフレーバーが揃っていること」がチェックポイントだ。

### 野心的な年間ゴール — 達成できなくても価値がある

2023年1月、チームは年間ゴールを設定した。

> *"Through the end of this year, we want to have the entire Golden Path playable from beginning to end."*
>（年末までにゴールデンパスを最初から最後までプレイアブルにする）

> *"Some people batted their eyes at this and said, no, I don't believe this is a doable thing. But some people said, yeah, I could see it."*
>（目を丸くして「無理だ」と言う者もいた。「やれる」と言う者もいた）

> *"You can make goals like this. You just have to decide what you're not going to do in order to make that goal happen. Cut some weak things to fund the strong bits to make it come alive."*
>（こういうゴールは設定できる。ただし「やらないこと」を決めなければならない。弱い部分を切って、強い部分に投資する）

結果は――達成できなかった。最後のバレーは不安定で、最後の2ミッションは存在すらしなかった。しかし Fox は誇らしげだった。

> *"I am happy to say we did not succeed at this goal. But imagine if we hadn't had that goal — we would have had a much bigger game that we'd have to support. These kind of aspirational goals keep the momentum going and force us to make crisp decisions."*
>（この目標を達成できなかったことを喜んで報告する。しかし目標がなかったら、もっと大きく散漫なゲームを抱え込んでいただろう。野心的なゴールがモメンタムを維持し、キレのある判断を強制する）

### AMA（Ask Me Anything）セッション

Insomniac Games（Spider-Man シリーズの開発元）から着想を得た施策として、毎週水曜に30分間のオープン Q&A を実施した。

> *"Anybody can write in questions about anything they want. This is just open season — ask anything you want. And we're going to be totally honest. We don't know the answer, we say we don't know."*
>（何でも聞いてくれ。オープンシーズンだ。完全に正直に答える。答えがわからなければ「わからない」と言う）

タウンホール（火曜）の翌日に設定することで、前日理解できなかったことを翌日すぐに質問できる。特にリモートワーク環境で効果を発揮した。

> *"We often learn a lot from what people ask about. If they want more clarification on the intro of the game, it means we're doing a bad job providing details on that."*
>（チームが何を質問するかから、私たちも多くを学ぶ。ゲームのイントロについて質問が集中するなら、その説明が不十分だということだ）

### ミッションレビュー — 色分けアウトラインで物語と遊びを統合する

> *"We at Sucker Punch have issues in trying to wrangle narrative over the top of gameplay. This is tricky business because of course you want both to be as great as possible."*
>（ナラティブをゲームプレイの上に重ねようとするのは難しい。両方を最高にしたいのだから、厄介なビジネスだ）

解決策は**色分けされたトップレベルアウトライン**だ。「High New Shaman」ミッションを例に説明された。

> *"If you see a blue bullet point, it's called out as gameplay. These are mission verbs — finding, fighting, sneaking, riding, climbing, puzzle solving. This is the stuff of the game."*
>（青い箇条書きはゲームプレイ。これらをミッション動詞と呼ぶ。発見、戦闘、潜入、騎乗、クライミング、パズル。これがゲームの実質だ）

ミッション動詞の例として「finding（発見）」が挙げられた。プレイヤーがアイヌの村を見つけ、口論が聞こえ、近づいて関わりを持つ。これはカットシーンではなく、コントローラーを握ったまま（on-the-sticks）体験するゲームプレイだ。

カットシーンには**クラス分け（A/B/C）**がある。

> *"A Class C — the camera's going to be pretty far away, the blocking is going to be very simple, the lighting will be very simple. They're pretty cheap to produce. Whereas a Class A — bespoke lighting, custom cameras, white blood on cloth. It is a bigger moment, harder to pull off."*
>（Class C はカメラが遠く、ブロッキングもライティングもシンプル。制作コストが低い。一方 Class A は専用ライティング、カスタムカメラ、布のシミュレーションまで。大きなモーメントで、作り込みが重い）

二つのミッションのアウトラインを横に並べることで、ゲームプレイ動詞のバランスが一目でわかる。

> *"If there's a lot of blue, it's going to be fun. If it's a lot of red, it's going to be very slow."*
>（青が多ければ楽しい。赤が多ければテンポが遅い）

---

## Part V: カットの文化 — 削ぎ落としを祝う

> *"We try really hard to make cutting things from the game an incredibly positive thing."*
>（ゲームから何かを切ることを、信じられないくらいポジティブなものにしようと努力している）

開発後半、チームは**毎週のカットセレモニー**を始めた。

> *"We just started to make weekly cuts and celebrate them, and people would get up in front of all the team and just say what they cut today. People clap. Turns out, it actually works really well. People like to be clapped."*
>（毎週カットを行い、それを祝う。チームの前に立って「今日これをカットした」と宣言する。拍手が起きる。驚くほどうまくいった。人は拍手されるのが好きだ）

> *"Cutting the game is sharpening. If you have four things you can work on and you've got rid of one of them, then suddenly your game is a lot better."*
>（ゲームをカットすることは研ぐことだ。4つの作業があって1つ削れば、突然ゲームがぐっと良くなる）

具体例として、ゲーム後半の湖畔エリアが紹介された。美しい風景だがコンテンツ、コレクティブル、ミッション、戦闘が一切ない状態だった。一人のチームメンバーが「このスペース、そもそも必要か？」と問い、30分で山の稜線をブロックアウトしてスペースを埋めた。

> *"One person on our team says, hey, do we really even need that space? And like half an hour, kind of blocked in a new mountain ridge and just filled it in."*
>（チームの一人が「このスペース、本当に必要か？」と言い、30分で新しい山の稜線をブロックインして埋めた）

結果、風景はむしろ美しくなり、ミッションチームが数週間分のコンテンツを作る負担が消えた。

> *"There's too many ideas. There's all kinds of cool crap that we want to do in every single game. And there's not enough time. And because we're not just growing our team beyond where we are — there's not a lot of people either. So there's no access other than trimming."*
>（アイデアが多すぎる。どのゲームでもやりたいクールなものが山ほどある。でも時間が足りない。チームも増やさない。だから削ること以外に道がない）

---

## Part VI: テストとトレーラー — ビジョンの検証と結晶化

### 内部フォーカステスト（IFT）

> *"Somebody had the idea — wouldn't it be cool if ninjas were just lying in wait and jump down from the snow and attacked you? And we said, yeah, that is cool. Let's make a valley around that."*
>（「忍者が雪の中に潜んでいて飛び降りて襲ってきたらクールじゃないか？」。それでバレー全体がそのコンセプトで作られた）

これが Ninja Valley の起源だ。しかし Fox が語りたかったのは外部テストではなく、**内部フォーカステスト（IFT）**の効用だった。プロダクション開始6か月目から、チーム全員がデモをプレイし Google シートでフィードバックを記入する。

> *"The sucker puncher in yellow is freaked out because they've been working on a thrown weapon feature. They don't feel like it's ready for prime time. But guess what — you've had enough time. You have to put up or shut up. Get it out there, man."*
>（黄色のスタッフは投擲武器フィーチャーに取り組んでいてまだ見せたくないとビビっている。でも十分な時間はあった。見せるか黙るかだ）

Connell はスタジオの姿勢をこう表現した。

> *"We are a vision-led, data-informed studio."*
>（ビジョン主導で、データに情報を得るスタジオだ）

IFT のデータが、先に紹介したプロトタイプのうちどれが「浪人の気分にさせるか」を実証した。もう一つの効果は、リモートワーク環境でのコミュニケーション改善だ。

> *"These three artists in the front row can suddenly figure out — oh gosh, there are a lot of flowers we're going to have to support going forward."*
>（前列の3人のアーティストが「花がこんなにたくさんある。これを今後サポートしなきゃいけないのか」と気づく。コミュニケーションが増える）

### トレーラー制作 — なぜ自分たちで作るのか

セッション最終パートで Connell は、トレーラーが単なるマーケティングツールではなく**クリエイティブディレクションの仕上げ**だと語った。

> *"That first trailer when everybody knows about your game, you get to announce it — why have somebody else make it? You should make it yourself."*
>（ゲームを世界に発表するあの最初のトレーラー。なぜ他の誰かに作らせるのか？ 自分たちで作るべきだ）

トレーラーがスタジオにもたらす効果は三つある。

**1. ゲームのアイデンティティを研ぎ澄ます**

> *"It forces you to go through all these pieces that people have been working on — content people, mission people, systems people, environment artists and combat."*
>（コンテンツ、ミッション、システム、環境アート、コンバット。スタジオの隅々から集めて一つにまとめることを強制される）

**2. 最終デザインの決定を促す**

トレーラー制作には約1年かかる。その過程でキャラクターデザインの最終決定が迫られた。前作の仁は「ザ・侍」の鎧兜だったが、アツは用心棒（Jimbo=「用心棒」）にインスパイアされた、よりアジャイルな浪人スタイル。2年半温めてきたアイデアをトレーラーのために結晶化させた。

> *"The trailer was the forcing function for us."*
>（トレーラーが私たちにとっての強制機能だった）

蝦夷オオカミ（Ezo Wolf）は絶滅した北海道のオオカミで、アツの物語のパラレルとして使われた。双剣（twin blades）は彼女と兄弟の絆を象徴する。マスク、リベンジベルト。トレーラーの制作過程でこれらのアイコニックなイメージが確定していった。

**3. コミュニティの反応から学ぶ**

トレーラー公開後、コミュニティが蝦夷オオカミに強く反応した。

> *"People really liked it and had some really cool ideas. So we started investing in it more — we made it more about a combat buddy, you can hang out with it. We built it an entire progression tree. This was not the plan."*
>（プレイヤーがとても気に入り、クールなアイデアもあった。だからもっと投資した。コンバットバディにして一緒に過ごせるようにし、プログレッションツリーを丸ごと作った。これは当初の計画にはなかった）

音楽もトレーラーの重要な要素だった。Connell は問いかけた。

> *"How can music help make you feel like a wandering ronin?"*
>（音楽はどうすれば浪人の気分にさせられるか？）

コンポーザーとの協業で、ゲームの感情的コアを音楽で表現する作業がトレーラー制作と並行して進んだ。

---

## まとめ — たった一つの問いで全てを判定する

セッションの最後に Connell は「写真を撮ってください」と促しながら、TL;DR（要約）を提示した。

| テーマ | アプローチ |
|:---|:---|
| **チーム規模** | 約150名。意図的に小規模を維持し、判断の質で勝負する |
| **ファンタジーステートメント** | 一つの問いですべてのフィーチャーを判定する |
| **ピラーシート** | 退屈なスプレッドシートだが、議論と優先順位の基盤 |
| **野心的ゴール** | 達成できなくてもいい。70-75%で十分な推進力 |
| **カットの文化** | 削ることはシャープにすること。毎週祝う |
| **内部フォーカステスト** | ビジョン主導、データで情報を得る |
| **トレーラー** | 外注しない。アイデンティティを研ぎ澄ます最良の手段 |

> *"We love making big games at the team size that we are. This is a huge part of our culture and we don't anticipate a change."*
>（今のチームサイズで大きなゲームを作ることが好きだ。これはスタジオ文化の根幹であり、変えるつもりはない）

セッション全体を通じて一貫していたサイクルを図式化すると、こうなる。

```
大胆にプロトタイプ → ピラーで判定 → 失敗からエッセンスを救出 → 形を変えて活かす
```

季節変化はナラティブツールに。ダイエジェティックマップは手描き風マップに。ジャーナルはトピックカードに。ロッククライミングのように完全にカットされたものもあるが、それは「ピラーにないものには投資しない」という原則の健全な適用だった。

---

## 日本の開発者への示唆

本セッションは Entry-Level（初級者向け）にカテゴライズされていたが、経験を問わず示唆に富む。特に日本の開発者にとって興味深い点を三つ挙げたい。

**文化的真正性へのこだわり**: ゲーム内のすべてのテキストを当時の日本語表記で統一するというルールが、ジャーナル機能のカットに直結した。欧米スタジオが日本文化の表現にここまで厳格なルールを課していること自体が、リサーチと敬意の深さを物語る。

**「北海道」の解釈**: 蝦夷地を「法の支配が及ばない危険な辺境」として描くことは、現代の日本人には馴染みのない解釈だろう。しかし時代設定を考えれば、中央政権の支配外にあった蝦夷地は浪人が放浪するには格好の舞台であり、「主人のいない剣士が賞金首として危険な北の果てを旅する」という構図は、日本の時代劇の伝統とも共鳴する。

**「決断」の文化**: 「ビーチか山か一日中議論できるが、選んで腹をくくれ」という Fox の言葉は、日本の開発現場でしばしば見られる「全員合意型」の意思決定とは対照的だ。約150人で AAA タイトルを作るには、このスピード感が不可欠なのだろう。そしてそのスピードを支えるのが、「Does it make you feel like a wandering ronin?」というたった一つの問いなのだ。

---

## 参考リンク

- [GDC 2026 公式スケジュール](https://schedule.gdconf.com/)
- [Sucker Punch Productions 公式サイト](https://www.suckerpunch.com/)
- [Ghost of Yotei 公式サイト](https://www.playstation.com/games/ghost-of-yotei/)

---

## おわりに

最後までお読みいただきありがとうございます。GDC 2026 の他のセッションレポートも順次公開していますので、ぜひフォローしてお待ちください。

**dsgarage Games** | GDC 2026 現地レポート
