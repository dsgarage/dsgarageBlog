# GDC 2026: State of Level Design 2026 — レベルデザインの現在地2026

> **日本の読者へ**: 欧米のゲーム開発では「レベルデザイン」（ゲームのステージ・マップ・空間を設計する専門職およびその作業）が独立した職種として確立しています。日本のスタジオでは「プランナー」や「ゲームデザイナー」が空間設計を兼務することが多いですが、欧米の AAA スタジオではレベルデザイナーが専門チームとして組織され、空間のレイアウト・動線・難易度曲線・ナラティブとの統合までを一貫して担当します。本記事の議論はその文脈で読み進めてください。

GDC 2026 の月曜日、Level Design Focus トラックのオープニングを飾ったのが「State of Level Design 2026」パネルでした。Witcher 4 を開発中の CD Projekt Red、The Outer Worlds 2 をリリースしたばかりの Obsidian Entertainment、Psychonauts 2 の Double Fine Productions、GTA シリーズや Call of Duty を手がけてきたデザイナー、そしてモバイルシューターの最前線――異なるジャンル・規模・文化を持つ 5 名が一堂に会し、「レベルデザイナーとは何者か」を約 50 分にわたって議論しました。

本記事では、会場で収録したトランスクリプトをもとに、パネリストの発言を可能な限り忠実に再構成しています。誰が何を語ったかを明確にしながら、**役割の変化**、**オープンワールドの空間設計**、**プロトタイプと検証手法**、**デザイン原則**の 4 軸で整理します。

---

## セッション概要

| 項目 | 内容 |
|:---|:---|
| **セッション名** | State of Level Design 2026 |
| **形式** | パネルディスカッション（Panel Discussion — 複数の登壇者がモデレーターの進行のもとテーマについて議論する形式） |
| **開催日** | 2026年3月10日（月） |
| **トラック** | Level Design Focus（GDC Festival of Gaming） |
| **モデレーター** | Seth Marinello（Principal Designer, Double Fine Productions） |

### パネリスト

| 名前 | 役職 | 所属 | 代表作 |
|:---|:---|:---|:---|
| Miles Tost | Level Design Lead（レベルデザインリード） | CD Projekt Red | The Witcher 3, Cyberpunk 2077, Witcher 4（開発中） |
| Cameron | Mission/Level Designer（ミッション・レベルデザイナー） | Sturd Ventures（元 Rockstar Games, Infinity Ward） | GTA シリーズ, Call of Duty シリーズ |
| Scott | Design Director（デザインディレクター） | Mass Promotion | モバイルシューター |
| Dan Chao | Area Designer（エリアデザイナー） | Obsidian Entertainment | Call of Duty: Modern Warfare 2, XDefiant, The Outer Worlds 2 |
| Seth Marinello | Principal Designer（プリンシパルデザイナー） | Double Fine Productions | Psychonauts 2 |

> **「State of ～」パネルとは**: GDC では毎年、特定の専門分野について業界の第一線で活躍する複数の開発者がステージ上で議論する「State of ～（～の現在地）」と題したパネルディスカッションが開催されます。レベルデザイン、ゲームデザイン、アニメーション、ゲーム AI など分野ごとに行われ、その年のトレンドと課題を俯瞰できる恒例セッションです。

---

### この記事で読めること

- **「レベルデザイナー」という職種は存在するか？**
- **オープンワールドの空間設計**
- **プロトタイプと検証**
- **デザイン原則**
- **ミッションデザインとレベルデザインの関係**
- **パネルから見えるレベルデザインの未来**
- **日本のプランナーへの示唆**
- ...ほか全9セクション

> **本記事のボリューム**: 約30,759文字 / スライド画像0枚
> スピーカーのトランスクリプト（発言の文字起こし）を原文・日本語訳つきで完全収録しています。

---

<!-- ===== ここから有料エリア（Note エディタで有料ラインを設定） ===== -->

## 1. 「レベルデザイナー」という職種は存在するか？

パネル冒頭、モデレーターの Seth が投げかけた質問はシンプルでした。

> "At your studio, do you have a separate level design department? And do you have a role of level designer on your team?"
>
> （あなたのスタジオにはレベルデザイン部門がありますか？ レベルデザイナーという役職は存在しますか？）

この一問への回答がスタジオごとにまったく異なり、業界全体で「レベルデザイナー」の定義が流動的であることを象徴していました。

> **日本との対比**: 日本の大手スタジオでは「プランナー」が企画・仕様・レベル設計・スクリプト実装を幅広くカバーするのが一般的です。欧米ではこの範囲を「レベルデザイナー」「システムデザイナー」「ナラティブデザイナー」「コンバットデザイナー」など細分化された専門職に分割する傾向がありますが、以下の議論はその職種の境界線すら揺らいでいる現状を示しています。

### Obsidian Entertainment: 「エリアデザイナー」という統合職

Dan Chao はまず、Obsidian には従来の「レベルデザイナー」という肩書きが存在しないことを明かしました。

> "For Obsidian Entertainment, we actually do not have a level design department. We have area designers, and we have world builders."
>
> （Obsidian Entertainment には実はレベルデザイン部門がありません。私たちにはエリアデザイナーとワールドビルダーがいます。）

エリアデザイナー（Area Designer）とは、ゲーム内の特定エリアについて空間レイアウト・敵配置・クエスト実装・環境ストーリーテリングまでを一貫して担当する職種です。一方のワールドビルダー（World Builder）は 3D 空間の構築やビジュアル面の実装を主に担います。Dan は両者の違いをこう説明しました。

> "World builders are a little bit more art driven. While level designers tend to do a little bit more gameplay and scripting."
>
> （ワールドビルダーはよりアート寄りで、レベルデザイナー的な役割のほうはゲームプレイやスクリプティングをより多く担当します。）

さらに注目すべきは、エリアデザイナーの守備範囲の広さです。

> "We do typical level design, layout, and blockout. We do combat staging. We do narrative implementation, like quests, dialogues. We set up NPCs that you can talk to. We set up the objects and props that you can interact with. And sometimes we do puzzles and very one-off kind of setups."
>
> （典型的なレベルデザインやレイアウト、ブロックアウト（仮配置）をやります。コンバットステージング（戦闘の演出配置）もやります。ナラティブ実装――クエストや会話もやります。話しかけられる NPC の設置、インタラクト可能なオブジェクトやプロップの設定、時にはパズルや一回限りの特殊セットアップも。）

日本のプランナー職に近い「何でもやる」スタイルですが、Obsidian の場合はこれが意図的な設計思想に基づいています。The Outer Worlds 2 では、複数の惑星にまたがるストーリーを先に設計し、そのストーリーに合わせてワールドを構築するというアプローチを取りました。

> "We have a lot of content and story we have to go through, and we just kind of have all the missions and all the quests kind of figured out before the planets or the actual open world. So then we are trying to design the world around the story. At the end of the day, Obsidian is a narrative-driven studio. So everything starts with story."
>
> （膨大なコンテンツとストーリーがあり、惑星やオープンワールドの前にすべてのミッションとクエストをだいたい固めます。だからワールドをストーリーに合わせて設計するんです。結局のところ、Obsidian はナラティブドリブンなスタジオですから。すべてはストーリーから始まります。）

### CD Projekt Red: アーティストが作っていた時代から 15 人チームへ

Miles Tost は CD Projekt Red のレベルデザイン部門の歴史を語りました。その変遷は、東欧の RPG スタジオが世界的メガスタジオへと成長する過程そのものです。

> "We have a bit of an interesting history with level designers. It's almost like the reverse where we started out with zero level designers. When I joined, there was a second one. The first one is now a tech artist, so it doesn't count."
>
> （レベルデザイナーに関して、ちょっと変わった歴史があるんです。逆パターンというか、レベルデザイナーがゼロの状態から始まりました。僕が入ったときは 2 人目で、最初の 1 人は今テックアーティストになったから、カウントしません（笑）。）

> "So back then the artists would basically do what was level design. You can kind of tell."
>
> （当時はアーティストがレベルデザインに相当する作業をやっていました。まあ、見ればわかりますよね。）

会場からは笑いが起きました。Witcher 初期作品のレベルデザインがアート主導だったことへの自虐的なジョークです。

その後、Cyberpunk 2077 の開発中にチームは約 8 人に成長し、当時はまだ「エンカウンターデザイナー」（Encounter Designer — 戦闘遭遇の設計を専門とする職種）と「レベルデザイナー」が分かれていました。転機は Cyberpunk の DLC「Phantom Liberty」の開発移行期でした。

> "At the end of Cyberpunk's development going into Phantom Liberty, that's the moment when we basically merged this. We went for a transition period where we basically tried to level up the encounter designers to also be able to build their own blockouts. And vice versa."
>
> （Cyberpunk の開発終盤から Phantom Liberty に移行するとき、この 2 つを統合しました。移行期間を設けて、エンカウンターデザイナーがブロックアウトも組めるように、逆もまた然りでスキルアップさせたんです。）

そして現在の Witcher 4 チームでは、15〜16 人のレベルデザイナーが「ワールドデザイン」から「エンカウンターロジック実装」までを一貫して担当しています。

> "Nowadays what we do is we own the world design process — that is basically planning the game world. What content do we work on? Where is the forest, where is the river, why is the forest there? Where do people live in relation to that? So we plan the entire internal logic of that. Then we also do the classic level design of building locations in blockouts — the dungeon, the cave, the warehouse in Cyberpunk. And nowadays we also implement the encounter logic — NPC logic, in Cyberpunk's case, device logic. Where the cameras are, things that can happen, all that."
>
> （今は僕たちがワールドデザインのプロセスを所有しています。つまりゲームワールドの計画ですね。どんなコンテンツを作るか？ 森はどこにあるか、川はどこにあるか、なぜそこに森があるのか？ 人々はそれに対してどこに住んでいるのか？ こうした内部ロジック全体を計画します。さらにクラシックなレベルデザイン――ダンジョン、洞窟、Cyberpunk なら倉庫のブロックアウトも行います。加えて今はエンカウンターロジックも実装します。NPC のロジック、Cyberpunk の場合はデバイスロジック――カメラの位置や、起こり得るイベントすべて。）

> "This gives us a lot of stuff to work with, a really high level of ownership. It's also very demanding."
>
> （やれることがすごく多い。オーナーシップのレベルがとても高いです。同時にとても要求水準も高い。）

Miles の言葉からは、レベルデザイナーの役割が単なる「空間を作る人」から「ワールドの論理を設計し、実装まで責任を持つ人」へと進化した軌跡が読み取れます。

### Double Fine Productions: ジェネラリストへのシフト

モデレーターの Seth は自社 Double Fine の状況をこう語りました。

> "At Double Fine, we have kind of moved towards having general game designers. We definitely have specialties in level design, but we make so many different kinds of games that it's very hard to have that be a separate department."
>
> （Double Fine では、ジェネラルなゲームデザイナーへと移行しつつあります。レベルデザインの専門性はもちろんありますが、あまりにも多様な種類のゲームを作るので、それを独立した部門にするのは難しいんです。）

> "Which is very different from when I first started in the industry, where there would be one or two systems designers on a team and the majority of the designers were actually fully dedicated to level design."
>
> （業界に入ったばかりの頃とは全然違います。当時はシステムデザイナーが 1〜2 人いて、デザイナーの大半はレベルデザインに完全に専念していましたから。）

Double Fine は『Psychonauts 2』のようにレベルごとにゲームメカニクスが大きく異なるゲームを作るスタジオです。ステージごとに世界観もルールも変わるため、「レベルデザイン専門」よりも「何でも設計できるゲームデザイナー」のほうがフィットするという判断です。

### Cameron（Sturd Ventures / 元 Rockstar・Infinity Ward）: ジェネラリスト × コ・デベロップメント

Cameron は、業界で広がるコ・デベロップメント（Co-development — 複数のスタジオが共同で一つのタイトルを開発する体制）の影響に言及しました。

> "Especially with the growth of co-development throughout the industry, I think when you're having an internal team that's maybe on the smaller side, having that broader generalist designer role is really something we've been trying to aim for."
>
> （業界全体でコ・デベロップメントが広がっている中、内部チームがやや小規模な場合、より広いジェネラリスト的なデザイナー職を目指すのは本当に重要だと思います。）

> "Even myself, mission designer by title, but I'm definitely laying out the geo and setting up all the scripting — doing the whole nine yards."
>
> （僕自身、肩書きはミッションデザイナーですが、ジオメトリのレイアウトもスクリプト設定も全部やっています。いわゆる「全部入り」です。）

Cameron はジェネラリスト化のメリットを「1 人に 1 チャンクを任せられること」と表現しました。

> "We can just assign a designer to a whole chunk of a thing and they run everything about it. They're going to be the most familiar with it. And if someone from a co-development team needs to jump in, there's one source of truth, one contact for that."
>
> （1 人のデザイナーにまるごと一つの塊を任せて、その全部を担当してもらえます。その人が一番よく知っている。コ・デベロップメントチームの誰かが入ってくるときも、情報源は 1 つ、窓口は 1 人です。）

### Scott（Mass Promotion）: T 字型デザイナー

Scott はモバイルシューターの開発を率いる立場から、Valve が提唱した「T 字型開発者」（T-shaped developer — 幅広い分野の基礎知識を持ちつつ、特定領域に深い専門性を持つ人材モデル）という概念を引き合いに出しました。

> "There's this analogy that Valve used, I don't know if they invented this, but I like it — which is the idea of a T-shaped developer. You have a horizontal understanding of a lot of things, and then you have an extreme depth in one area."
>
> （Valve が使っていたアナロジーがあります。発明したのかは知りませんが、好きな概念です。T 字型デベロッパーという考え方。多くのことについて水平方向の理解があり、一つの領域に極めて深い専門性がある。）

> "Level designers have their particular expertise, that's the depth. But the way we approach design is that everybody is contributing to this whole."
>
> （レベルデザイナーには特定の専門性がある。それが T の縦棒です。でも僕らのデザインアプローチは、全員が全体に貢献するというものです。）

---

## 2. オープンワールドの空間設計 — 加算のレベルデザイン

議論が最も白熱したのは、オープンワールドにおけるレベルデザインの特殊性についてでした。ここでは「ジム」（Gym）という業界用語が頻出します。

### 「ジム」とは何か

Cameron が聴衆に向けて用語を解説しました。

> "For those who haven't heard this term — maybe gym, or zoo, some people just call them sandboxes. It's really just a separate level. When you're working on an open world game, you open up the main level, it takes you like... something like that just to watch the loading bar go up, and then if you're in Unreal, you're watching shaders compile and all these fun things."
>
> （この用語を聞いたことがない方のために。ジム、ズー、サンドボックスとも呼ばれます。要するに独立したテスト用レベルです。オープンワールドのゲームを作っていると、メインレベルを開くだけで延々とロードバーを眺めることになる。Unreal ならシェーダのコンパイルを見守る楽しい時間もあります（笑）。）

> "So you might make a gym or some kind of test space that allows you to hit the ground running, boot up the editor way faster, and work with your direct tools. And the hope, the dream is that your gym work just magically goes and works in the main level. That's simple... but it's not always that simple."
>
> （だからジムやテストスペースを作って、エディタを高速起動して直接ツールで作業できるようにするわけです。夢は、ジムでの作業がメインレベルにそのまま魔法のように移行すること。シンプルな話ですが……いつもそうシンプルにはいきません。）

### Obsidian のアプローチ: ジムを使わない理由

Dan Chao は Obsidian がジムをほぼ使わないことを明かし、その理由を技術面と設計面の両方から説明しました。

> "For us, we don't actually use gyms because our open world map is a little bit smaller. Every content that we're trying to implement is directly implemented to the open world so that we can start optimizing for performance very early on, because a lot of stuff worked really well for a hard-loaded map, but it won't work for the open world. The open world has a lot more resources and a lot of memory use."
>
> （私たちはジムを実質使いません。オープンワールドのマップが比較的小さいので。実装しようとするコンテンツはすべて直接オープンワールドに入れます。パフォーマンス最適化を早い段階から始められるからです。ハードロードマップでうまく動くものがオープンワールドでは動かないことが多い。リソースもメモリも桁違いに使いますから。）

ただし、ロード時間は依然として課題です。

> "We do use the world partition by Unreal, so we can just load a small chunk of the map. This way it is a little bit faster, but it still takes about five to ten minutes for me to load into the game."
>
> （Unreal の World Partition（ワールドパーティション — マップを小さなチャンクに分割して必要な部分だけロードする仕組み）は使っています。これで少し速くなりますが、それでもゲームにロードするのに 5〜10 分かかります。）

Dan はさらに、ジムを使わない設計上の本質的な理由を語りました。

> "Sometimes our open world POI is not in a vacuum — there are stuff around it. I worked on a POI that is super close to a couple of open world encounters. And whenever you start any combat within the POI, guys come from the surrounding area trying to attack you. So you'll never be able to find out if your level actually works in a gym."
>
> （オープンワールドの POI（Point of Interest — プレイヤーが訪れる注目地点）は真空中に存在するわけではなく、周囲に色々なものがあります。私が担当した POI は複数のオープンワールドエンカウンターのすぐ近くにあり、POI 内で戦闘を始めると周辺エリアから敵が攻めてくる。だからジムではレベルが本当に機能するかわからないんです。）

### CD Projekt Red: シームレス世界の罪は消えない

Miles Tost は、CD Projekt Red がオープンワールドで直面する根本的な課題をこう表現しました。

> "We don't have the thing where you go to a new level and now you've loaded a completely different world space and now it's like the next level and all of your sins are gone. You have this giant interconnected world."
>
> （僕たちには「新しいレベルに行ったら完全に別のワールドスペースがロードされて、はい次のレベル、前の罪は全部消えました」ということがないんです。巨大な相互接続されたワールドがある。）

Cyberpunk 2077 の Night City（ナイトシティ）での具体的な苦労を語りました。

> "We really were struggling with trying to figure out, 'Oh, we need 15 blocks wide of game space inside of one building.' How do we make that look like a believable city when now this mall, if you put it into a city block, is 15 blocks wide? It was a long process."
>
> （1 つのビルの中に 15 ブロック分のゲームスペースが必要、とか本当に苦労しました。そのモールを都市のブロックに配置すると 15 ブロック分の幅になるわけです。どうやって信じられる都市に見せるのか？ 長いプロセスでした。）

Miles はワールドで直接作業することの重要性を強く訴えました。

> "I really had to fight my designers and go like, 'No, I know it's more convenient to work in the map instance or whatever, but go and build it in the world because you need all of the context.' One of the principles we're trying to always follow when we create open worlds is continuity — when I finish a piece of content, what can I see next on the horizon that might attract me? You can't really do that when you're not working in the world."
>
> （デザイナーたちに本気で戦いを挑む必要がありました。「いや、マップインスタンスで作業するほうが便利なのはわかる。でもワールドで作れ。コンテキストが全部必要なんだ」と。オープンワールドを作るとき常に守ろうとしている原則は continuity（連続性）です。コンテンツを 1 つ終えたとき、地平線上に次に惹きつけられるものが見えるか？ ワールドの中で作業しないとそれはできません。）

さらに Miles は、ワールドの「信じられる存在感」（believability）への強いこだわりを示しました。

> "If I place something in the world, I want to have a feeling that it was actually — not just some random dot that exists because the game needs it to exist, but it's actually been part of the world and there's like history to it. And I, as a player, can go to this place and understand how it came to be, how it connects to other places."
>
> （ワールドに何かを配置するとき、それが単にゲームが必要だから存在するランダムな点ではなく、実際にワールドの一部であり、歴史があるという感覚が欲しい。プレイヤーとしてその場所に行って、どうやってこれが生まれたのか、他の場所とどう繋がっているのかを理解できるようにしたい。）

### Cameron: 加算のレベルデザインという本質

Cameron は、オープンワールド設計の本質を「加算のレベルデザイン」（additive level design）と表現し、自身の経験をもとに「減算」との対比を語りました。

> "I remember really freaking out first working on Battlefield, because a lot of the maps you would start, you just drop a terrain down and you're adding things into that. I was so used to coming from Dead Space, being like, 'I have a corridor and I'm just building the game off of this corridor.' And even doing outdoor spaces, I'm gonna start building a canyon out of BSP geometry."
>
> （Battlefield で初めて作業したとき、本当にパニックになったのを覚えています。マップの多くは地形をドロップして、そこにものを追加していく。Dead Space の「廊下があって、その廊下からゲームを構築する」に慣れていたので。屋外でも BSP ジオメトリで峡谷を作る感覚でした。）

> "And then when you're just given a two-kilometer by two-kilometer map to start off with, you're like, 'What do I do with this?' I muted this building because it wasn't working and now it's just empty. I can't subtract that space. It screws up my sightlines."
>
> （2km 四方のマップをいきなり渡されて「これどうすれば？」と。うまくいかない建物を消したら、ただの空き地になった。空間を減算できない。サイトライン（視線の通り道）が全部崩れるんです。）

> "Open world design is kind of almost by philosophy like an additive level design. I've had this level design whiplash where I'm working on GTA — which I won't talk about, but you can probably imagine — or Call of Duty, where it's just whatever we need it to be. And then now trying to find a middle ground."
>
> （オープンワールドのデザインは哲学的にほぼ「加算のレベルデザイン」です。GTA で作業して――詳しくは言えませんが想像はつくでしょう――あるいは Call of Duty で「必要なものは何でも作る」をやって、今はその中間を探している。レベルデザインの鞭打ち症みたいなものですね。）

### コ・デベロップメントとテストスペースの価値

Cameron はコ・デベロップメント体制でのジムの実用的な価値にも触れました。

> "I'm sure everyone here has experience working with co-developers. Being able to have a test space that you can point co-developers to is really important because depending on how your communication works across your different teams and studios, they might not have the full context for your level."
>
> （ここにいる皆さんもコ・デベロップメントの経験があるでしょう。コ・デベのチームに指し示せるテストスペースがあるのは本当に重要です。スタジオ間のコミュニケーションの仕方次第で、彼らはレベルの全体像を把握していないかもしれない。）

> "If you have to say, 'Oh, you open up the main world, then the debug menu, then launch to this part, then do this' — that's all these steps. Versus if you just say, 'Go to this gym, it'll give you this exact stuff.' You're saving everyone a ton of time."
>
> （「メインワールドを開いて、デバッグメニューを開いて、ここに飛んで、これをやって」なんて言わなきゃいけないのと、「このジムを開けばそのまま全部入ってるよ」と言えるのとでは。みんなの時間を大幅に節約できます。）

### モバイルシューター: 思ったほど違わない

Scott は、モバイルゲーム開発のレベルデザインが PC/コンソールと本質的に変わらないことを強調しました。

> "It's not really that different, to be honest. I think this is an impression that lots of players have — that mobile is somehow fundamentally different. But the experience of building it, trying it, deploying it, and then having people play it on the device... it's pretty much the same."
>
> （正直、それほど変わりません。モバイルは根本的に違うという印象を多くのプレイヤーが持っていると思いますが、作って、試して、デプロイして、デバイスでプレイしてもらう……体験はほぼ同じです。）

> "It's worth mentioning that half of this world — the eastern half of this world — mobile shooters are massive. So they are loving these games. It's here where people have the impression that there's something fundamentally different about it. It's really just people getting used to the thing."
>
> （言及すべきは、世界の半分――東半球ではモバイルシューターが巨大だということです。みんなこのゲームを愛している。「根本的に違う」という印象を持つのは欧米側の話で、単に慣れの問題です。）

ただし、プラットフォーム固有の配慮はあります。

> "Obviously you have a smaller screen space. You probably have to think about the environment a little bit differently — maybe it shouldn't get noisy, maybe you can get a little bit cleaner about things that you're looking at."
>
> （画面が小さいのは明らかです。環境の見せ方は少し変える必要があるでしょう。ノイズを減らして、見るものをもう少しクリーンにする、とか。）

---

## 3. プロトタイプと検証 — 「早く失敗できる環境」を作る

パネルの中盤は、デザインをどう検証するかという実践的な議題に移りました。

### Obsidian: ドキュメント → プロトタイプ → イテレーション

Dan Chao は Obsidian の体系的なプロセスを説明しました。

> "Before we do any actual production — blockout or just building spaces — we always dedicate a lot of time to work out a couple of documents: area design documents, narrative design documents, and art design documents. We are really good at keeping everything documented."
>
> （実際のプロダクション、ブロックアウトやスペース構築の前に、必ず多くの時間をドキュメント作りに使います。エリアデザインドキュメント、ナラティブデザインドキュメント、アートデザインドキュメント。ドキュメントを整備するのは得意です。）

> "Then we will move on to a prototype. At this stage, not everything needs to be ready. Sometimes some features are still under development. But we try to do the best we could so that our playtesters can play from start to finish, so that we can evaluate the experience a little bit more effectively."
>
> （その後プロトタイプに移行します。この段階で全部が揃っている必要はありません。まだ開発中の機能もある。でもプレイテスターがスタートからフィニッシュまで遊べるよう最善を尽くし、体験をより効果的に評価できるようにします。）

> "Sometimes we do need to do some hacky design or hard coding to make something work. We are working on something that I can't really disclose, but we use a lot of hacky ways to prototype the design before we actually get the chance to get actual support on that."
>
> （ハック的なデザインやハードコーディングで無理やり動かすこともあります。詳しくは言えませんが、正式なサポートを得る前にハック的な方法を多用してデザインをプロトタイプします。）

Dan は自身の哲学として、プロトタイプ段階では他のチームに依頼せず自分でやりきることを好むと語りました。

> "For me, specifically, I like to do everything on my end. I don't like to commission combat design or system design. If we're just going to prototype, I'd rather do it myself. But later on, they should be stepping in and help with stuff."
>
> （個人的には、プロトタイプ段階では全部自分でやりたい。コンバットデザインやシステムデザインに依頼はしたくない。ただのプロトタイプなら自分でやる。本格的になったら彼らが入ってきて手伝うべきですが。）

### Scott: テスト、テスト、テスト

Scott は検証の核心をシンプルに言い切りました。

> "Test the hell out of it."
>
> （とにかく徹底的にテストしろ。）

その上で、より構造的なアプローチを展開しました。

> "We have to be able to build spaces that are informed by the design of the game. Do we have things that are set in stone that we know are pillars of the experience? That's something where we don't have to have all the confusion about what we're trying to build."
>
> （ゲームデザインに基づいたスペースを構築できなければなりません。体験の柱として確定しているものはあるか？ それがあれば、何を作ろうとしているかについて混乱する必要がない。）

> "Once you have a thing that you can prototype and get out in front of people as soon as you can, you need to test that. And try to see — are there ways for us to validate and confirm through telemetry? What can we see the players actually doing? What are we trying to measure in terms of success?"
>
> （プロトタイプができたら、可能な限り早く人前に出してテストする。テレメトリ（ゲーム内のプレイヤー行動データ）で検証・確認する方法はあるか？ プレイヤーが実際に何をしているか見えるか？ 成功をどういう指標で測ろうとしているか？）

### Cameron: 3 つの P（Pitch, Profile, Playtest）

Cameron は独自のフレームワーク「3 つの P」を紹介しました。

> "I've got this silly list that I've put together to quantify how you validate things. I call it the three P's. I know, I taught a class at one point and my students were like, 'Oh my gosh.' Sorry."
>
> （バリデーション方法を定量化するための、ちょっとバカバカしいリストを作りました。3 つの P と呼んでいます。はい、以前授業で教えたとき学生に「マジかよ」って言われました。すみません。）

> "It's Playtest, Profile, and Pitch. Obviously you have that sort of initial pitch to align on what it is that you're actually trying to accomplish. Then you actually execute on that vision, whether that's a prototype or otherwise."
>
> （Playtest（プレイテスト）、Profile（プロファイル）、Pitch（ピッチ）です。まずピッチで達成しようとしていることを擦り合わせる。次にそのビジョンを実行する。プロトタイプでもそれ以外でも。）

> "Then you do what's called a profile — you kind of take a step back and it's literally just goal comparison. It's not 'Is this thing subjectively fun?' It's 'Does it meet the goal?'"
>
> （次にプロファイルを行います。一歩引いて、文字通りゴールとの比較をする。「これ主観的に面白い？」ではなく「ゴールを達成しているか？」です。）

> "And then if it meets the goal, well, we are biased to this piece of content because we're the ones who created it. So now we playtest. Someone less familiar with it can then give you the subjective take. Because your subjective take is biased."
>
> （ゴールを達成していたら、次はプレイテスト。自分たちは作った本人だからバイアスがある。だから馴染みの薄い人に主観的な評価をもらう。あなたの主観はバイアスがかかっているから。）

### CD Projekt Red: 「早く失敗できる環境」

Miles Tost は CD Projekt Red のバリデーションプロセスにおける課題と哲学を率直に語りました。

> "I think that's one of the major areas that we in our team can probably improve — the process of validating and deciding it's good enough to move on. We have historically had a bit of a... we don't really have an economic approach to that."
>
> （チームとして改善の余地が大きい分野の一つだと思います。検証して「これで先に進んでいい」と判断するプロセス。歴史的に、エコノミカルな（経済的な・効率的な）アプローチを取ってこなかった。）

ブランチングナラティブ（分岐するストーリー）とオープンワールドの組み合わせが、検証を特に困難にしていると説明しました。

> "The nature of the types of games we're building with this heavy focus on branching narratives in open worlds makes it really hard to understand how things are coming together until they kind of do."
>
> （ブランチングナラティブを重視したオープンワールドゲームという性質上、全体がどう組み上がるかは、実際に組み上がるまで本当にわからないんです。）

> "We have a tendency to invest a lot into the pre-visualization of this process — blockouts, pre-vis, little videos and all that to help us understand as much as we can before. Recently the tendency for us has been to try to go much cheaper to get even faster to playable."
>
> （このプロセスのプレビジュアライゼーションに多くの投資をする傾向があります。ブロックアウト、プレビズ、ちょっとしたビデオなど。最近の傾向は、もっと安く、もっと早くプレイアブルに到達しようとすることです。）

そして Miles はキーワードとなるフレーズを使いました。

> "We're trying to build an environment in which we can fail faster. That's like having tools that allow us to prototype quickly, having multiple testing rounds and internal feedback. But it's basically building an environment where the team has a high level of resilience to get back up and go, 'Okay, let's try again because this sucked.'"
>
> （**早く失敗できる環境**を作ろうとしています。素早いプロトタイプを可能にするツール、複数のテストラウンドと内部フィードバック。要するに、チームが高い回復力（レジリエンス）を持って「OK、やり直そう。これはダメだったから」と立ち上がれる環境を構築するということです。）

> "We regularly took out large chunks of the game. We haven't made one game where I truly thought, 'Oh yeah, this will be simple.' We're constantly doing a thing that is very new to us — be it new IP, then first person, now we have a new main character that we need to establish."
>
> （ゲームの大きな塊を定期的に切り捨てています。「これはシンプルにいくぞ」と本気で思えたゲームは一本もない。常に自分たちにとって非常に新しいことに挑んでいる。新 IP、それからファーストパーソン、今度は新しい主人公を確立しなければならない。）

### Seth: イテレーションは魔法の杖ではないが、最善のアプローチ

モデレーターの Seth はパネル全体の議論を俯瞰してこうまとめました。

> "It is an iterative process. Your studio or your individual designers can get better and better at making really good guesses, but we're still not at the point where we really have well-defined formal structures that will give you all the answers ahead of time. It is almost always an iterative process."
>
> （これはイテレーティブなプロセスです。スタジオや個々のデザイナーは、良い推測を立てることがどんどん上手くなれる。でも、事前にすべての答えを出してくれるような明確に定義された形式構造を持つところまでは、まだ到達していません。ほぼ常にイテレーティブなプロセスなのです。）

> "Being able to start from a solid pitch but then evolve that in an iterative manner on how what's coming in — that is still, I think, the best approach that we have."
>
> （しっかりしたピッチからスタートして、入ってくるフィードバックに応じてイテレーティブに進化させる。これがまだ、私たちが持つ最善のアプローチだと思います。）

---

## 4. デザイン原則 — 何が普遍で、何がスタジオ固有か

パネル終盤の質問で、各パネリストが自身のデザイン原則を語りました。

### Scott: プレイスタイルを保証する空間

Scott はマルチプレイヤーマップの設計原則を語りました。

> "In the specific sort of multiplayer tighter map design, so much of it is about making sure that we enable that players and their playstyles are able to have a good time."
>
> （タイトなマルチプレイヤーマップデザインでは、プレイヤーとそのプレイスタイルが楽しめることを保証するのが本当に大事です。）

> "If you're thinking about a normal shooter map and someone prefers a long-range playstyle versus someone who likes to run and gun — we have to think about how we enable that at all times."
>
> （普通のシューターマップで、遠距離プレイスタイルを好む人と走り撃ちが好きな人がいる。常にその両方をどう成立させるかを考えなければなりません。）

具体的な原則としては以下を挙げました。

> "Not making the map too symmetrical in terms of environment design — that's confusing. You want to get people oriented. Affordances — if we've built grappling hooks, double jumps, it's important that a player can look and learn the space and then be able to say, 'I know I can climb on this thing' or 'This is a space where I know I have a clean shot on a player in this area.'"
>
> （環境デザインでマップを対称にしすぎない。紛らわしくなります。プレイヤーがオリエンテーション（方向感覚）を得られるようにしたい。アフォーダンス（行動を誘発する見た目のヒント）も重要。グラップリングフックやダブルジャンプを実装したなら、プレイヤーが空間を見て学び、「ここは登れる」「ここからならあのエリアの敵を撃てる」と判断できることが大切です。）

### Dan Chao: 現実世界が最高のリファレンス

Dan は独自の哲学を端的に述べました。

> "I do not like referring to other people's design. The design always starts with either real life... I do not like to go to other people's game and try to refer to that because then it kind of limits your imagination. A real-life world is your best reference."
>
> （他人のデザインを参照するのは好きではありません。デザインは常に現実世界から始めます。他のゲームを見て参考にしようとすると、想像力が制限されてしまう。現実世界が最高のリファレンスです。）

### Cameron: ピッチングは最重要スキル

Cameron は「ピッチング」（自分のデザインを他者に売り込み、理解を得る行為）の重要性を強調しました。

> "I can't impart on everybody enough that pitching the thing and getting buy-in and persuading people is one of your number one responsibilities in level design or design in general. And if people are telling you 'I don't get it,' you need to figure out how they can get it, or even cut it."
>
> （何度でも言いたいのですが、自分のデザインをピッチして、バイインを得て、人を説得すること。これはレベルデザイン、いやデザイン全般において最重要の責務の一つです。「わからない」と言われたら、わかるようにするか、切るかを判断しなければならない。）

### Miles: 切ることへの勇気

Miles と Cameron の間で「カッティング」（大胆にコンテンツを削除すること）の文化について議論が交わされました。

> Cameron: "They [CD Projekt Red] have a very positive culture around cutting. I think as designers it's really important for us to be willing to lose entire chunks of missions or large chunks of geo. We need to be comfortable with cutting stuff and not being married to everything that we do."
>
> （CD Projekt Red はカッティングに対して非常にポジティブな文化を持っています。デザイナーとして、ミッションの大きな塊やジオメトリの大きな部分を失う覚悟は本当に大切です。自分の作ったものすべてに固執せず、切ることに慣れる必要がある。）

---

## 5. ミッションデザインとレベルデザインの関係

Cameron は、ミッションデザイナーとレベルデザイナーの関係性について掘り下げました。この議論は、大規模チームと小規模チームでの役割分担の違いを浮き彫りにします。

> "Ideally, a mission designer is really more of a high-level person that, even before any blockout is placed, is able to help mold the possibility of a mission emerging out of that space. And then, in a place like Call of Duty where you have a dedicated level designer, they help execute on that vision."
>
> （理想を言えば、ミッションデザイナーはより上位レベルの存在で、ブロックアウトが配置される前から、その空間からミッションが生まれる可能性を形作る手助けをします。Call of Duty のように専任のレベルデザイナーがいる場合は、そのビジョンの実行を担当する。）

> "The mission designer sets the goal, the level designer executes on that goal, and then the scripter fills in all the bits between. But when you're on a smaller team, the mission designer is probably going to set the goal, execute on the goal, and fill in the encounters."
>
> （ミッションデザイナーがゴールを設定し、レベルデザイナーがゴールを実行し、スクリプターがその間を埋める。でも小さなチームでは、ミッションデザイナーがゴール設定・実行・エンカウンターの充填まで全部やるでしょう。）

Seth はここで業界全体の変遷を指摘しました。

> "The chunk of work that we used to think of as the true meat and potatoes of level design — building blockouts, placing encounters — that is kind of becoming just one part of it. There's spatial design, area design, world design, mission design."
>
> （かつてレベルデザインの「本筋」だと思われていた作業――ブロックアウト構築、エンカウンター配置――は、今や全体の一部に過ぎなくなりつつある。空間デザイン、エリアデザイン、ワールドデザイン、ミッションデザインと分化している。）

---

## 6. パネルから見えるレベルデザインの未来

パネル全体を通して浮かび上がったいくつかの方向性を整理します。

### 6-1. 「レベルデザイナー」は消えないが、意味が変わる

5 つのスタジオすべてが「レベルデザイン」の仕事をやっていますが、その呼び方と範囲はバラバラです。Obsidian の「エリアデザイナー」、CD Projekt Red の「全権委任型レベルデザイナー」、Double Fine の「ジェネラリスト・ゲームデザイナー」、Cameron の「ミッションデザイナー兼何でも屋」、Scott の「T 字型デベロッパー」。名前は違えど、共通するのは**オーナーシップの拡大**です。空間を作るだけでなく、その空間の論理・ストーリー・遊びのすべてに責任を持つ方向へ進んでいます。

### 6-2. オープンワールドは「ワールドの中で作る」しかない

CD Projekt Red と Obsidian という、現在最も注目されるオープンワールド RPG の制作者 2 人が口を揃えて語ったのは、「ジムでは本当のことがわからない」という事実でした。パフォーマンス最適化、周辺コンテンツとの干渉、視覚的連続性――これらはすべてワールドの中で直接作業しなければ検証できません。

### 6-3. 「早く失敗する」文化がクオリティを支える

CD Projekt Red の Miles が語った「fail faster」の哲学は、カジュアルなスローガンではなく、ツール投資・チームの心理的安全性・大胆なカッティング文化によって支えられた組織的な取り組みです。Cameron が証言した「CD Projekt Red はカッティングにポジティブ」という外部からの評価が、これを裏付けています。

### 6-4. ドキュメントとプロトタイプの二段構え

Obsidian の「まずドキュメント、次にプロトタイプ、そしてイテレーション」というプロセスは、体系的でありながら柔軟です。「ハック的でもいいからまずプレイアブルにする」という Dan の姿勢は、完璧主義に陥りがちなドキュメント文化のバランサーとして機能しています。

### 6-5. モバイルとコンソールの壁は思ったより薄い

Scott の「ほとんど同じ」という証言は、モバイルゲーム開発を特殊なものと見なしがちな業界の認識に挑戦するものでした。東半球のモバイルシューター市場の巨大さへの言及は、北米・欧州中心の GDC 聴衆にとって良いリマインダーだったでしょう。

---

## 日本のプランナーへの示唆

パネルの議論を、日本のゲーム開発者の視点で咀嚼してみます。

**「プランナー」は実は先進的だった可能性がある。** 欧米が長年の専門分化を経て「ジェネラリスト回帰」へ向かっている一方で、日本のプランナー職は最初から広い守備範囲を持っていました。Obsidian のエリアデザイナーや Cameron の「全部入りミッションデザイナー」は、ある意味で日本のプランナーに近い役割です。

**ただし「名前がないこと」は課題になりうる。** 欧米では「エリアデザイナー」「ワールドデザイナー」「ミッションデザイナー」と名付けることで、期待される責任範囲が明確になります。日本の「プランナー」は範囲が広すぎて、採用時にも評価時にも曖昧さが残りがちです。

**「ピッチング」は日本の現場でも必須スキル。** Cameron が「最重要の責務」と呼んだピッチングは、日本では「企画書を書く」に近い行為ですが、ドキュメントだけでなく口頭での説得力まで含めた概念です。デザインの良し悪しだけでなく、それをチームに「買わせる」能力が問われています。

---

## まとめ

GDC 2026「State of Level Design」パネルが示したのは、「レベルデザイナー」という職種の輪郭が溶け始めているということでした。しかしそれは消滅ではなく、進化です。空間を作る技術は変わらず中核にありながら、ワールドの論理設計、ナラティブ実装、チームへのピッチング、大胆なカッティング、そして「早く失敗できる環境」の構築という、より広い責任を引き受ける方向へ進んでいます。

Miles Tost が誇らしげに語った「毎プロジェクト、もう少し責任を広げようとしている」という言葉は、レベルデザイナーという職種の未来を端的に表していました。

> "We try to kind of claim a bit more responsibility and input every project, because we feel we can bring a lot of value to the games that we create."
>
> （毎プロジェクト、もう少し多くの責任と発言権を獲得しようとしています。自分たちが作るゲームに大きな価値をもたらせると感じているから。）

---

## 参考リンク

- [GDC 2026 Schedule — State of Level Design 2026](https://schedule.gdconf.com/)
- [CD Projekt Red — The Witcher 4](https://www.thewitcher.com/)
- [Obsidian Entertainment — The Outer Worlds 2](https://outerworlds2.obsidian.net/)
- [Double Fine Productions](https://www.doublefine.com/)

---

## おわりに

最後までお読みいただきありがとうございます。GDC 2026 の他のセッションレポートも順次公開していますので、ぜひフォローしてお待ちください。

**dsgarage Games** | GDC 2026 現地レポート
