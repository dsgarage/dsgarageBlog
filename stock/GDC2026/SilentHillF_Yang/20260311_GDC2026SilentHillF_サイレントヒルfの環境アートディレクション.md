---
title: "GDC 2026: Silent Hill f — 銃を捨てたホラーゲームが辿り着いた「緊張感」の設計哲学"
subtitle: "The Challenges of Creating a Melee-Only Horror Game"
author: "dsgarage"
date: "2026-03-11"
---

# GDC 2026: Silent Hill f — 銃を捨てたホラーゲームが辿り着いた「緊張感」の設計哲学

ホラーゲームから銃を取り上げたら、何が残るのか。Silent Hill シリーズ10年以上ぶりのメインライン新作「Silent Hill f」は、シリーズの伝統だった射撃武器を完全に排除し、メレー（Melee：近接戦闘）のみで恐怖体験を構築するという大胆な決断を下した。GDC 2026 で登壇したゲームディレクター Al Yang 氏（NeoBards Entertainment）は、その決断がクリエイティブな野心だけでなく、スコープ管理（Scope Management：開発する機能の範囲を現実的なリソースに収める管理手法）というプロダクションの現実から生まれたことを率直に語った。

約1時間のセッションで明かされたのは、「テンション（緊張感）」を軸にゲーム全体を設計するフレームワークだった。ピラー設計、フランチャイズDNAの継承、テンションの反転、リソース設計、テンポ設計 -- ホラーゲームに限らず、あらゆるジャンルのゲームデザイナーにとって示唆に富む内容を、47枚のスライドとトランスクリプトをもとに詳細にレポートする。

> **GDC（Game Developers Conference）について**: 毎年3月にサンフランシスコで開催される世界最大のゲーム開発者向けカンファレンス。技術・デザイン・ビジネスなど複数のトラック（分野別セッション群）に分かれ、現役の開発者が自身のプロジェクトの知見を共有する。講演者は自社タイトルの開発プロセスを「ポストモーテム（開発の振り返り）」として率直に語る文化があり、失敗談も含めた実践的な知見が得られる点が特徴だ。

---

## セッション概要

| 項目 | 内容 |
|:---|:---|
| タイトル | 'Silent Hill f': The Challenges of Creating a Melee-Only Horror Game |
| スピーカー | Al Yang（Game Director / Studio Creative Director） |
| 所属 | NeoBards Entertainment（台湾・台北） |
| トラック | Design |
| 形式 | Lecture（60分） |
| 日時 | 2026年3月11日（水）15:10 - 16:10 |
| 場所 | Room 2005, West Hall |

![タイトルスライド](slides/gdc2026_silenthill_f_01.jpg)
*セッションのタイトルスライド。主人公 Hinako のコンセプトアートが印象的だ*

---

## 1. スピーカーの経歴と NeoBards Entertainment

### Al Yang 氏 -- サウンドエンジニアからゲームディレクターへ

Al Yang 氏は自己紹介を軽いユーモアで始めた。

> "My name's Al. I am the game director for Silent Hill f, and the studio creative director at Neobards. A little bit about myself. I start off in audio, actually, as a sound engineer, and then decided that I wasn't really for it, so I started over in QA, back in the States."
>
> "Went to China with THQ for a few years, working on online properties over there. Then came back to the US..."
>
> （私の名前は Al。Silent Hill f のゲームディレクターであり、Neobards のスタジオ・クリエイティブディレクターです。もともとサウンドエンジニアとしてキャリアを始めたのですが、向いていないと気づいてアメリカで QA からやり直しました。中国で THQ のオンラインタイトルを数年担当した後、アメリカに戻り...）

その後のキャリアは多岐にわたる。中国で THQ のオンラインタイトルを数年開発、アメリカに戻って PlayStation All-Stars Battle Royale に参加（本人は "probably the most famous, iconic arena brawler game of all time" と冗談めかして紹介し、会場から拍手と笑いが起きた。"It's definitely the top one out there, for sure" と畳みかけている）、ドイツでの F2P（Free-to-Play：基本無料）PvP 開発を経て、台湾で Final Fantasy XV Pocket Edition のデザインディレクター、続いて Resident Evil Resistance のゲームディレクター、そして Silent Hill f のゲームディレクターに就任した。

![スピーカー紹介](slides/gdc2026_silenthill_f_02.jpg)
*Al Yang 氏の経歴スライド*

### NeoBards Entertainment -- 台湾から世界へ

> "We are a codev and external developer based out in Taipei, Taiwan, one of the other branch studio in Suzhou."
>
> （我々は台北に拠点を置くコデベロッパー（共同開発スタジオ）で、蘇州にもブランチスタジオがあります。）

NeoBards Entertainment は台湾・台北に本拠を置くコデベロッパー（Co-developer：外部の開発パートナースタジオ）だ。Dead Rising Deluxe Remaster などの実績があり、KONAMI をはじめとする多数のパートナーと協業している。日本の大手パブリッシャーが持つIPの開発を台湾や韓国のスタジオが担うケースは近年増加しており、NeoBards はその中でも CAPCOM・KONAMI といった大手との協業実績を持つ有力スタジオである。

Yang 氏は個人の功績ではなくチームへの敬意を繰り返し強調した。

> "I think I'll be remiss to say that I'm very honored to be here today to talk to all of you guys, but it takes a team to build a game."
>
> （ここで言わなければ失礼になりますが、今日皆さんにお話しできることを大変光栄に思います。でもゲームを作るにはチームが必要です。）

NeoBards では全タイトルについてチームメンバーがメッセージを残せるウェブサイトを用意しているという。"For every game that we build, we actually put up a website so everybody can kind of leave a little bit of a message from the team to everybody"（全てのゲームについてウェブサイトを立ち上げ、チームから皆さんへのメッセージを残せるようにしている）。また、KONAMI やモーションキャプチャ、サウンドデザイン、ローカライズなど多数の外部パートナーとの協業体制も紹介され、外部開発スタジオとしてのプロフェッショナリズムが窺えた。

![NeoBards チーム](slides/gdc2026_silenthill_f_03.jpg)
*NeoBards のチームメンバー紹介スライド*

---

## 2. セッションの全体像 -- 3つの柱

Yang 氏はセッションの全体構成を冒頭で提示した。

> "Quick overview of what we're gonna talk about today is kind of how we decide on the key vision for Silent Hill f, kind of from zero. And then for us, kind of what the core essence of horror games is, tension, and then how we can use melee combat to kind of bring that out."
>
> （今日お話しする内容の概要です。まず Silent Hill f のキービジョンをゼロからどう決めたか。次にホラーゲームの核心 -- テンション（緊張感）。そしてメレー戦闘でそれをどう引き出すか。）

続けてゲームの特徴を簡潔に紹介した。

> "We are the first kind of mainline entry for over a decade in the Silent Hill franchise. And what makes us very different is that we are not based in kind of the town of Silent Hill or kind of its surroundings, but we're based in completely different areas. So 1960s, Showa era, Japan, the countryside. And the other big difference is we are a purely melee focused game, no guns at all."
>
> （我々は Silent Hill フランチャイズにおいて10年以上ぶりのメインラインタイトルです。大きな違いは、舞台がサイレントヒルの町やその周辺ではなく、全く別の場所 -- 1960年代、昭和時代の日本の田舎であること。もうひとつの大きな違いは、完全にメレー特化のゲームで、銃が一切ないことです。）

「なぜ銃をなくしたのか？」という最大の疑問に対して、Yang 氏はまず冗談で返した。

> "So I think a lot of you will be wondering what the key point of this talk is, is why we went towards melee combat. And the answer is, it was awesome. That's what we did. That's my talk. Thank you very much."
>
> （この講演のキーポイントが何か、なぜメレーにしたのかと皆さん気になっていると思います。答えは、最高だったから。以上、ありがとうございました。）

会場が笑いに包まれた後、「Q&A はありますか？」と畳みかけてから、本題に入っていった。

---

## 3. ピラー設計 -- 「マクドナルドで寿司を出すな」

### 開発初期の状態 -- コンセプトアート5枚からのスタート

> "In the very, very beginning, when Okamoto-san, our producer from the Konami side, came to us, we basically just had a very rough script and a few pieces of concept art. I mean like five pieces of concept art."
>
> （開発の本当に最初、KONAMI 側のプロデューサーである岡本さんが我々のところに来たとき、持っていたのはラフなスクリプトとコンセプトアート数枚 -- 5枚くらいのコンセプトアートだけでした。）

Yang 氏はメレー戦闘の話をする前に、まず意思決定プロセスを遡ることの重要性を述べた。"I know even though the talk today is about melee combat, I think it's important to kind of go backwards and see how we even got to this decision in the first place."（今日の講演はメレー戦闘がテーマですが、まず遡ってこの決断に至った経緯を見ることが重要だと思います。）

ここからゲームの方向性を「ピラー」（Pillar：設計の柱。開発チーム全員が判断基準にする、プロジェクトの根幹となる方針）として定める必要があった。Yang 氏は早い段階で、ホラーゲーム全般が遠距離戦闘でテンションを生み出している伝統に注目していたことにも触れた。"Not just us, but a lot of other franchises out there"（我々だけでなく、他の多くのフランチャイズもそうだ）と指摘し、そこに差別化の余地を見出していた。

### Silent Hill フランチャイズの3つの時代

> "The Silent Hill franchise has kind of been loosely defined with kind of three eras for me. Team Silent Hill was kind of the OG team working on these titles. Then after post-Team Silent Hill, we're kind of went out to the West, and then kind of where we are now, where Silent Hill is trying to make a resurgence."
>
> （Silent Hill フランチャイズは私にとって大きく3つの時代に分けられます。Team Silent がオリジナルチームとして制作していた時代。Team Silent 以降、開発が西洋に移った時代。そして今、Silent Hill が復活しようとしている現在。）

1. **クラシック期** -- KONAMI 社内の Team Silent（初代〜4作目を手がけた伝説的開発チーム。山岡晃氏の音楽、伊藤暢達氏のクリーチャーデザインなど、日本人クリエイターが生んだ独自の恐怖表現で世界的評価を得た）による初期作品群
2. **西洋開発期** -- Team Silent 解散後、Climax Studios や Vatra Games など西洋スタジオへの委託時代
3. **復活期** -- 2022年の KONAMI Transmission で一挙発表された、複数タイトルが同時進行する現在

Silent Hill f が開発を開始した時点で、Silent Hill: The Short Message、Ascension、2 Remake、Townfall など複数のタイトルが進行中だった。

### 差別化の課題 -- カルトステータスのジレンマ

> "We had to feel like Silent Hill, but how do we stand out from the rest of these games? ... Something very dangerous for us here is that Silent Hill is to the point where it's kind of a cult status game, where there's a lot of things that people are like, okay, this feels like Silent Hill, this doesn't feel like Silent Hill."
>
> （Silent Hill らしさは守らなければならない。でも他のタイトルとどう差別化するか？... 危険なのは、Silent Hill が一種のカルト的地位にあるゲームだということです。「これは Silent Hill っぽい」「これは Silent Hill っぽくない」という期待がすでに固まっている。）

ここで Yang 氏は、記憶に残るアナロジーを持ち出した。

> "Let's say you go to McDonald's and you order something, you get this, give you sushi. Now, it's not worse, it's not the worst food. I think some people might say it might be a higher quality food, but if you go to McDonald's, you're there for a particular reason, you're there because you want a greasy burger. That's what you expect. Even if I give you something better, it's too different from what you're coming to expect, you're not gonna have a good time."
>
> （マクドナルドに行って注文したら寿司が出てきた。寿司は悪い食べ物じゃない、むしろ上質かもしれない。でもマクドナルドに来た理由は脂っこいハンバーガーが食べたいからだ。たとえより良いものでも、期待からかけ離れていたら楽しめない。）

> "So maybe sushi wouldn't work, but maybe something like a fish burger, chicken burger, something that would be creative, but still fit within kind of the confines of what players would be expecting. Again, you can't be creative if it's so different that it's completely unrecognizable to the franchise."
>
> （寿司は無理でも、フィッシュバーガーやチキンバーガーなら。プレイヤーの期待の枠組みの中で創造的なもの。フランチャイズとして認識できないほど違うものでは、創造的とは言えません。）

**ポイント**: プレイヤーのフランチャイズへの期待（DNA）を守りながら、その中で創造的に差別化する。「全く別物」にならないこと。Yang 氏はこれを "maybe we have to be brave, but very practical in how we apply our creativity here"（創造性の適用において、大胆でありつつも非常に実践的でなければならない）と表現した。

![Vision Driver 1](slides/gdc2026_silenthill_f_10.jpg)
*Vision Driver 1: "Unique but SILENT HILL" -- 共通の軸はアトモスフィア（雰囲気・空気感）*

### アトモスフィアとインダイレクト・ホマージュ

> "The common link in all of these titles is atmosphere. So for Silent Hill, doesn't matter what title you're looking at, it's kept as really unique, you really dream like atmosphere. Things aren't always what they seem, but it's kind of a little bit obfuscated information side."
>
> （全タイトルに共通するのはアトモスフィア（雰囲気）です。どのタイトルを見ても、夢のような独特の雰囲気がある。物事は見かけ通りではない。情報が少し曖昧にされている面がある。）

長期間の休眠から復活するにあたり、チームはファン向けの「インダイレクト・ホマージュ（間接的なオマージュ）」を随所に仕込んだ。

> "We have a level in Silent Hill f where 90% of the doors are locked. People would be like, that is terrible game design. Why would you make a level where every single door you interact with, it's locked, it's locked. But we did that to kind of replicate that old feel, but we didn't do it everywhere."
>
> （Silent Hill f にはドアの90%が施錠されたレベルがあります。「ひどいゲームデザインだ。なぜ全部のドアが鍵がかかってるんだ」と言われるかもしれない。でもこれは旧作の感覚を再現するためにやったことで、全レベルでやっているわけではありません。）

> "In the very beginning of the game, we have this long lonely walk down a hill into town, very reminiscent of Silent Hill 2."
>
> （ゲームの冒頭には、丘を下って町に向かう長い孤独な道のりがあります。Silent Hill 2 を彷彿とさせるものです。）

Yang 氏のお気に入りのオマージュは、ピアノの家にあるこけし人形だという。"My personal favorite is this kokeshi doll from our piano's house. Again, like a direct homage to previous games."（個人的なお気に入りはピアノの家のこけし人形。旧作への直接的なオマージュです。）

ファンには「ああ、これだ！」と分かる仕掛け。新規プレイヤーには普通に楽しめるが気づかない。"Nothing like right in your face"（顔面に押しつけるようなものではない）というオマージュの哲学が、フランチャイズDNAの継承と差別化を両立させている。

---

## 4. ジャンルサイクル論 -- 「なぜ今メレーなのか」

Yang 氏はメレー採用の背景として、ゲームジャンルの周期的なブームについて興味深い持論を展開した。

> "Things that are popular usually loop around every few generations or so, or like every 10 years, 15 years or so. So an example here is just the beat-em-up... we're kind of at, I would say, in the middle of the beat-em-up kind of renaissance at the moment. However, there was like a big drought for like 10 years or so."
>
> （人気のあるものは数世代ごと、10年から15年くらいの周期でループします。例えばベルトスクロールアクション。今まさにルネサンスの真っ只中ですが、その前に10年くらいの枯渇期がありました。）

> "It's not that these kind of mechanics or this kind of gameplay or these type of things aren't bad. It's just that they get popular. They're really popular after a while, then you see like a rapid amount of them in a short amount of time. Maybe people get kind of tired of them because there's too many and it kind of dies down and comes back."
>
> （メカニクスやゲームプレイ自体が悪いわけではない。ただ人気が出て、短期間に大量に出て、多すぎて疲れて、落ち着いて、また戻ってくる。）

スーパーヒーロー映画のブームと衰退を引き合いに出し（"I think we are at the tail end of kind of superhero movie boom at the moment"（スーパーヒーロー映画ブームの末端にいると思う））、**「過去が未来を形成する（the past forms the future）」** という原則を示した。リサーチの重要性を強調した上で、歴史の教訓として冗談を添えた。

> "That's why you don't attack Russia in winter."
>
> （だから冬にロシアを攻めてはいけないのです。）

会場はこのジョークにも反応し、Yang 氏は笑いを確認してから次のセクションへ進んだ。メレー戦闘は「過去の遺物」ではなく、まさにサイクルの中で復活するタイミングにあるという確信が、チームの方向性を後押しした。

---

## 5. 第2ピラー -- 脚本と世界観の整合性

### 竜騎士07と物語の構造

> "How many people know about Ryukishi07, like the author? ... He's a very prolific author, very kind of strong body of work. I really recommend you check it out. You'll find that his work is very Silent Hill-like, or as this word actually, Silent Hill f is very Ryukishi07-like, because the thing is, because we had a known author, we really had to imitate his style, but it still had to feel like Silent Hill."
>
> （竜騎士07という作家を知っている人はどのくらいいますか？... 非常に多作で強力な作品群を持つ作家です。ぜひチェックしてみてください。彼の作品は非常に Silent Hill 的、というか正確には Silent Hill f が非常に竜騎士07的なのです。著名な作家がいる以上、彼のスタイルを再現しなければならない。でも同時に Silent Hill らしさも保たなければならない。）

脚本をゲームに落とし込む際の課題にも言及した。

> "You can't just write a script and make it a game. Even for a movie, you have to have a brighter screenplay from the original story. Usually stories are kind of like one major beat to another major beat, especially since this stuff is kind of visual novel structured, and with games, again, it's interactive. The pacing is dictated by a large part by the player."
>
> （脚本を書いてそのままゲームにはできない。映画でさえ原作から脚色が必要です。物語は通常、主要なビートからビートへの線形構造で、特にビジュアルノベル的な構造の場合はそうです。でもゲームはインタラクティブで、ペーシングの大部分はプレイヤーが決める。）

### 2人の主人公 -- ダブル構造の設計課題

> "Our main character Hinako was essentially two characters for those of you guys who don't know. So just all of these things you see over here, is basically you're doubling the work on everything."
>
> （主人公のヒナコは実質2人のキャラクターです。ここに見えているもの全てが、作業量として倍になるということです。）

この2人構造は銃の排除にも直結した。

> "And ranged combat, one of the things that came up, because if you put it through for one character, do you have to do for the other character? And because the worlds, spoiler alert, kind of meld together at the end, you have to have maybe characters that use range that don't interact with range. So this is something we thought about really, really early on."
>
> （遠距離戦闘の問題も早い段階で浮上しました。片方のキャラクターに実装したら、もう片方にも必要になるのか？ネタバレですが、2つの世界は最終的に融合するので、遠距離武器を使うキャラクターと使わないキャラクターが交差する問題が出てくる。これは本当に早い段階で考えたことです。）

### 1960年代日本の再現 -- 世界観の整合性

> "It has to feel believable to people of the culture. So people that lived through 1960s, kind of Showa era Japan, but it also has to feel interesting to people who haven't been there, kind of a bit of tourism, if you will."
>
> （その文化の人々にとって信じられるものでなければなりません。1960年代の昭和の日本を経験した人にとって正しく感じられ、同時にそこに行ったことのない人にとっても興味深いものでなければならない。一種のツーリズムのようなものです。）

台湾のスタジオが1960年代の日本の田舎を舞台にしたゲームを作る。この文化的な挑戦に対して、チームは時代考証に多くの時間を投入した。ローカルに深く根ざしつつ、グローバルにアクセスしやすい世界を構築するというバランスは、日本IPの海外開発において常に課題となるテーマだ。

---

## 6. 外部開発の鉄則 -- 「出荷しないゲームより出荷するゲーム」

### スケジュールは変更不可

> "As external or co-developers, we have a very strong rule, which is you do as much as you can with what you have, because at the very beginning, we already have a schedule, we already have a budget, and these are usually hard set."
>
> （外部開発者・コデベロッパーとして、我々には強い原則があります。持っているもので最大限のことをする。最初からスケジュールと予算があり、これらは通常ハードセット（変更不可）です。）

> "It's about not doing the coolest stuff possible, it's about doing the coolest stuff in the time and resources you have possible."
>
> （最もクールなものを作ることではなく、与えられた時間とリソースで最もクールなものを作ることが目標です。）

Yang 氏は宮本茂の名言を引き合いに出しつつ、現実的な反論を加えた。

> "For me, a shipping game is infinitely better than never shipping. Miyamoto says like, you know, delayed game is delayed, but a good game is always good, but you delay a game enough, your studio's gonna go out of business, and now you have nothing, and you're out on the streets."
>
> （私にとって、出荷するゲームは出荷しないゲームより無限に良い。宮本さんは「遅れたゲームはいずれ良くなるが、悪いゲームはずっと悪い」と言いましたが、延期しすぎたらスタジオが潰れて何もなくなり、路頭に迷います。）

### 「デンジャーポイント」の計画

Silent Hill f は NeoBards にとって初めてのストーリー重視リニアホラーゲームだった。Yang 氏自身にとってもこのジャンルは初めての経験だった。しかし全てがゼロからではないことも強調した。

> "This is the first time Neobards has built a story heavy linear horror game. First time we've ever done this before. It's the first time I've ever worked on this type of game. That's a danger point right there, that's probably the biggest danger point."
>
> （NeoBards がストーリー重視のリニアホラーゲームを作るのは初めてです。こんなタイプのゲームに取り組むのは私自身も初めて。これはデンジャーポイントです。おそらく最大のデンジャーポイントです。）

"But we built characters before, we built AI before, so it's like, okay, we're not too concerned about this, but getting the pacing right, getting the story beats, doing all these cutscenes, these are danger points."（でもキャラクター構築やAI開発の経験はある。そこは心配していない。でもペーシングの調整、ストーリービートの構築、カットシーン制作 -- これらがデンジャーポイントです。）

デンジャーポイント（危険ポイント）に対する対策は明快だった。

> "When we plan for these things, we always budget like two, three times a month of time to get this stuff done. And if we go faster than we expect, that's great, we've earned that time back, but we'll never go over schedule because we plan for these danger points."
>
> （こうしたリスクに対して、常に2〜3倍の時間を見積もります。予想より早く終わればラッキーで、時間を取り戻せる。でもスケジュールを超過することは絶対にない。デンジャーポイントを計画に織り込んでいるからです。）

---

## 7. 銃を捨てた本当の理由 -- スコープ管理の連鎖反応

ピラー設計と外部開発の制約を踏まえた上で、Yang 氏はいよいよメレーオンリーの決断に至った核心的な理由を語った。それは「かっこいいから」でも「差別化のため」でもなく、スコープ管理の現実だった。ヒナコ（Hinako）に遠距離武器を持たせるかどうかの検討が、まさにそのデンジャーポイントだった。

> "When we look at ranged, a lot of things kind of spiral out of control very fast. So it's just a simple phrase like, okay, hey, let's have her use ranged weapons, you're like, okay, now we have to build an aiming system, now we have targeting, does that mean she can aim at specific body parts? So we have to do animations... So there's weak points now, so now we need bullets, so there's extra resources in there. It's just one type of gun, is that kind of boring? Do we have extra type of range? Well, let's do a bow instead, that works. Oh, hey, but bow, you have to draw bows for this extra animation there... we don't even have physics on the arrow."
>
> （遠距離武器を検討すると、あっという間にスコープが制御不能になるんです。「彼女に遠距離武器を持たせよう」と言うだけで、エイムシステムが必要になり、ターゲティングが必要になり、特定の身体部位を狙えるのかとなり、専用アニメーションが必要になり、弱点があるなら弾も必要で、銃1種類だとつまらないから弓にするかとなり、弓なら引くアニメーションが必要で、矢の物理演算もないし...）

> "Just that one word, it's like, should we have range? It spirals out of control really, really fast."
>
> （「遠距離武器を入れるべきか？」というたった一言が、本当にあっという間にスコープを爆発させます。）

さらにテーマとの整合性の問題もあった。

> "Also thematically, we had some clashes with her whether with using like guns, and it just didn't fit with the story we're trying to tell here."
>
> （テーマの面でも、彼女に銃を持たせることにはストーリーとの齟齬がありました。我々が語ろうとしている物語に単純に合わなかった。）

**メレーオンリーはクリエイティブな判断と同時に、スコープ管理の現実的な判断でもあった。**

### コスト効率の分析フレームワーク

> "If I'm gonna spend 100 man hours building this thing, and it's gonna add like five points to the game in terms of fun, or I'm gonna spend 100 hours on this thing, it's gonna add like two points, for the most part, or whatever, give us more cost efficiency here."
>
> （100人時を投じて楽しさを5ポイント上げる機能と、同じ100人時で2ポイントしか上がらない機能があるなら、コスト効率の良い方を選ぶ。）

この原則を Resident Evil Resistance の事例で補足した。Resistance は4対1の非対称 PvP ゲームで、1人がマスターマインドとしてカードデッキを使い、4人のプレイヤーはホーンテッドハウスからの脱出を目指す構造だった。

> "The turnaround for that project in terms of the pitch was a week for the pitch and a month for the prototype. So it was super fast. And one of the reasons we decided to go this direction is actually, we had a lot of people in the company that were really big Magic the Gathering, Pokemon fans. So of course, we're like, okay, you are doing the strategy for this stuff, you have a lot of building knowledge, so going in this direction, we could go much, much faster and kind of build to their strengths, because we didn't have to start over and kind of research how these things had to work."
>
> （Resistance はピッチ1週間、プロトタイプ1ヶ月でした。非常に速かった。この方向に進んだ理由のひとつは、社内に Magic the Gathering や Pokemon のファンが多くいたからです。彼らは戦略系の知識が豊富で、この方向なら遥かに速く進められる。チームの強みに沿って進められるので、ゼロからリサーチする必要がなかった。）

チームの既存スキルセットを活かせる方向を選ぶことで、リサーチから始める必要がなくなり、開発速度が格段に上がる。**「チームの強みに沿ってデザインの方向性を決める」** -- これは NeoBards の外部開発哲学の核心だ。"You should try to build the strengths of your team"（チームの強みを活かすべき）と繰り返し強調した。

---

## 8. テンションマップ -- 紙の上でホラーを設計する

### ゲーム全体の緊張感を事前に計画する

> "We spend a lot of time on this, this is actually the first, I want to say, three hours of Silent Hill f, everything's planned out from here. We do this on paper... when you plan for these things in advance, you can catch things, we know where we're supposed to have players build tension, we know roughly how long we want players to spend in each area."
>
> （これには多くの時間を費やしました。Silent Hill f の最初の3時間分は全てここから計画されています。紙の上でやる。事前に計画しておけば問題を早期に発見できる。プレイヤーにテンションを感じてほしい場所、各エリアでの滞在時間を事前に把握しています。）

テンションマップは、ゲーム全体の緊張感の波を可視化する設計ドキュメントだ。テンションが高まる区間と、プレイヤーが息をつける区間を明示的にスケジュール化し、開発チーム全員で共有する。

### デザイナーの「フレーバー」をペーシングに活かす

テンションマップの運用に関連して、Yang 氏は独自のチーム管理手法を紹介した。

> "I never put the same level designer on more than about hour and a half, two hours of the game in a row, because everyone has their own unique flavor, own kind of taste, way to think about things. If you have a section that's like five hours of the same person designing, it's gonna feel like that for five hours, but you have different people going there on basically different, on the same goal, but different people approaching it, you're gonna have it feel very, very different."
>
> （1人のレベルデザイナーにゲームの1時間半〜2時間以上を連続して担当させません。全員に固有のフレーバー、味、思考のスタイルがある。5時間を同じ人がデザインしたら、5時間ずっとその人の味になる。同じゴールに向かって異なる人がアプローチすれば、全く違う感触になります。）

### アイスクリームのタイブレーカー

チーム内の意見対立を解決する方法についても語った。

> "Let's say the goal is give me the most classic ice cream flavor, and in my mind, I'm thinking chocolate, but then our team member comes up and it's like, okay, I propose vanilla. Well at this point, am I wrong? Are they wrong? No, not all of us are wrong, but it's down to a matter of personal preference."
>
> （ゴールが「最もクラシックなアイスクリームの味」だとして、私はチョコレートだと思っているが、チームメンバーがバニラを提案する。この時点で、私が間違っている？彼らが間違っている？いいえ、誰も間違っていない。個人の好みの問題です。）

> "We always let a team member propose as they kind of use their idea. Again, because it's always your idea is gonna feel just like one person's game. You have a lot of ideas in there as long as they fit the goal, you're gonna get that kind of much more varied, more layered experience."
>
> （常にチームメンバーに自分のアイデアを提案させます。なぜなら自分のアイデアだけを通すと1人のゲームになってしまう。ゴールに合致する限り、多くのアイデアを盛り込むことで、より多様で重層的な体験が生まれます。）

---

## 9. テンション設計 -- ホラーの核心は「怖さ」ではなく「緊張感」

### Scary vs Tense

セッションの中核であるテンション設計は、Yang 氏の明快な定義から始まった。

> "When a lot of people play horror games, they're like, I want to feel scared. Now I'm playing a horror game, pretty scared, but for us when we see this, what we really think they're saying is you want to feel tense. There's a very big difference between feeling scary and feeling tense."
>
> （ホラーゲームをプレイする人は「怖くなりたい」と言います。でも我々がそう聞いたとき、本当に言いたいのは「緊張感を感じたい」だと思っています。怖いと感じることと緊張を感じることは全く違います。）

> "For us, horror games are about the buildup and the release of tension. That buildup is just as important as the release. For a lot of you guys who are kind of like horror buffs, you know that before the killer actually does the deed, that is the most tense part. Once you get stabbed, you're like, okay, then the gore kind of happens, but the tension is gone."
>
> （我々にとってホラーゲームとは、テンションのビルドアップ（蓄積）とリリース（解放）です。ビルドアップはリリースと同じくらい重要。ホラー好きの方なら分かると思いますが、キラーが実際に手を下す直前が最もテンションが高い。刺された瞬間にテンションは消えるんです。）

### 現代ホラー戦闘の構造的困難

> "Modern horror game combat is actually really hard to do because you have the awkward kind of unwieldiness in key areas. Older kind of games, you had like fixed camera angles, you had like tank controls, the unwieldiness was there already kind of, but now with kind of modern games, people are expecting kind of more smooth gameplay controls. There is quality of life that has become kind of standard. So you have to be very careful about how you make janky and how you make it janky."
>
> （現代のホラーゲーム戦闘は本当に難しい。旧作には固定カメラ、タンクコントロールという不便さがあり、それ自体がテンションを生んでいました。でも現代のゲームではスムーズな操作性が当然として期待される。QOL（クオリティ・オブ・ライフ）が標準になった。だから「どう不便にするか」「どこを不便にするか」に非常に注意を払わなければならない。）

旧来のホラーゲームでは操作の不便さがテンションを生む装置として機能していた。しかし現代のプレイヤーは操作の快適さを当然のものとして期待する。"Janky" をどこに、どう配置するか -- この「快適さとホラーの緊張感の両立」が、現代ホラー戦闘の本質的課題だとYang氏は指摘した。

さらに従来のホラーゲームは遠距離が主、メレーが副だったのに対し、Silent Hill f はそれを完全に逆転させたことの困難さにも触れた。"Traditional kind of horror games focused on ranged or melee as secondary. And for us, we split it around, which is melee as primary and actually no secondary."（伝統的なホラーゲームは遠距離が主でメレーが副。我々はそれをひっくり返し、メレーが主で、副は存在しない。）

---

## 10. テンションの反転 -- 「Flip the Script」

### 距離の意味を逆転させる

従来のホラーゲームでは、敵との距離が近いほど危険で、離れるほど安全だった（銃で撃てるから）。Silent Hill f はこれを完全に逆転させた。

> "With a ranged horror game, the closer the enemy is to you, the more tense it's gonna be because you're safer when they're further away, they can't hit you, but you can hit them. So we had to flip that."
>
> （遠距離武器のあるホラーでは、敵が近いほど緊張する。遠ければ安全で、自分は当てられるが敵は当てられない。これを反転させなければなりませんでした。）

> "What if the enemy had more range advantage on you? So it's more tense when you're further away from them, it's kind of flipping that thing."
>
> （もし敵の方がレンジアドバンテージを持っていたら？敵から離れているほど緊張する。それが反転です。）

この「フリップ・ザ・スクリプト（台本をひっくり返す）」を理解しやすくするため、FPS の比喩が使われた。

> "You can kind of think about this is if you're playing an FPS and you just have a shotgun and there's snipers in the map, that's a bad time. If you have to go and find them and you have to get close to them, it's very tense. If you have a sniper rifle, then it's mostly just like kind of a whack-a-mole kind of thing."
>
> （FPS でショットガンしか持っていないのに、マップにスナイパーがいる状況を想像してください。最悪です。見つけて近づかなければならない。それは非常に緊張する。スナイパーライフルを持っていれば、もぐらたたきのようなものですが。）

| 距離 | 従来の遠距離ホラー | Silent Hill f のメレー設計 |
|:---|:---|:---|
| 敵が遠い | 安全（射撃で倒せる） | **テンション高**（敵が突進してくる、対処困難） |
| 敵が近い | 危険（テンション高） | **安全**（自分の攻撃が届く） |

### 敵のアニメーションとAIクロスドロー

この反転を成立させるために、敵の動きに多大な投資が行われた。

> "We spent a lot of time with our animations and the type of attacks for our monsters where they're much more agile than the player and also we're able to kind of cross much larger spaces much quicker. A lot of time on animations, wind-ups, tells, fake-outs, to really add tension to the player in these situations."
>
> （モンスターのアニメーションと攻撃パターンに多くの時間を費やしました。モンスターはプレイヤーよりはるかに俊敏で、広い空間をずっと速く横切れる。ワインドアップ（攻撃の予備動作）、テル（攻撃の予兆）、フェイクアウト（フェイント）にも時間を費やし、これらの状況でプレイヤーにテンションを加えています。）

特に印象的だったのが「AIクロスドロー」の設計だ。

> "When you're raising that pipe, when the opponent's raising his brother's head, it's really tough to tell if you're actually gonna connect or not, distance-wise... The AI is like, oh, I'm gonna get hit. It's kind of an attack pose at that point in time. And the whole purpose of that is like, am I gonna hit, or am I going to get hit? There's that tension in that moment."
>
> （パイプを振り上げているとき、敵も攻撃モーションに入る。距離的に当たるかどうか本当に分からない。AIが「やられる！」と反応して攻撃姿勢を取る。その瞬間の「当たるのか、当たられるのか？」というテンション、そこが全てです。）

プレイヤーが攻撃モーションに入ると、AIが同時に攻撃モーションに移行する。「自分の攻撃が先に当たるか、それとも敵に先を取られるか」 -- このどちらになるか分からない一瞬が、メレー戦闘のテンションの核心だ。

### ボスデザインの文化的リサーチ

第1ボスの「フルッゴ（Furggo）」については、巫女の所作を忠実に再現するための文化的リサーチに言及した。"This is the reference for Furggo, who's our first boss. She's a shrine maiden, kind of very elegant motions here."（フルッゴは最初のボスで、巫女です。非常に優雅な動き。） 実際の祭祀の動きを参考にし、優雅な動きから突然攻撃に転じる緩急が、一種のジャンプスケアとして機能するよう設計した。

### ボスステージでの環境テンション

敵個体だけでなく、ステージ自体でもテンションを生み出す設計が紹介された。

> "This is Rinko, this is our second boss in the game... as she gets angrier and angrier, we purposely kind of like have the stage kind of transform. Like there's lava going everywhere and it's really hard to see what's going on. Actually, she's throwing lava too... There's enemies everywhere. And again, our purpose of that is to add and create more tension because we knew that it's hard to focus on more than really one thing in this type of kind of game. So for every extra thing you add on there, it kind of adds up."
>
> （リンコはゲームの第2ボスです。彼女が怒りを増すにつれて、意図的にステージを変容させます。溶岩が至るところに広がり、何が起きているか把握しにくくなる。実際に溶岩を投げてきます。敵も至るところにいる。こうしたすべてがテンションを追加する。このタイプのゲームでは1つ以上のことに集中するのは難しい。追加される全ての要素がテンションを積み上げます。）

![ボス戦](slides/gdc2026_silenthill_f_30.jpg)
*ボス戦のスライド。ステージの変容がテンションを増幅する*

---

## 11. テンションとフラストレーションの境界線

### 区別できない問題

テンションを追求すれば必然的にフラストレーション（苛立ち）との境界線の問題にぶつかる。Yang 氏はこの問題に正面から向き合った。

> "Frustration is really hard to quantify in terms of when it's something that's tense and when it's something that's frustrating. And it's different for everybody, actually. What's challenging for someone might be really easy for someone else."
>
> （いつテンションでいつフラストレーションなのか、定量化するのは本当に難しい。実際、人によって違います。誰かにとっての挑戦は、別の人には簡単すぎるかもしれない。）

> "You cannot remove tension completely because again, that's not just a horror game, but you need tension for accomplishment. You have to feel accomplished when you get past this challenge."
>
> （テンションを完全に排除することはできません。これはホラーゲームに限らず、達成感にはテンションが必要です。この挑戦を乗り越えたときに達成感を感じなければならない。）

### キャラクターの恐怖とプレイヤーの恐怖のギャップ

> "There's a difference between what the character on the screen is feeling and what you guys, the player, is feeling. The character may be scared, but you're like, it's not scary at all."
>
> （画面上のキャラクターが感じていることと、プレイヤーが感じていることには違いがあります。キャラクターは怖がっているかもしれないけど、あなたは「全然怖くない」と思っているかもしれない。）

Yang 氏は甥の体験を例に挙げた。

> "I was watching my nephew play this game. He's like, oh my God, a boss was here. I'm like, uh-huh, yeah, there was a boss there. Oh my goodness, weren't you surprised? Yeah, I didn't expect him to be there. I'm like, mm-hmm. But you know what, after like three or four more times of that, or even the next time, he's gonna see that and be like, uh-huh, oh, I know what's coming."
>
> （甥がゲームをプレイしているのを見ていました。「うわ、ボスだ！」と驚いている。私は「うん、そうだね、ボスだね」と。でも3〜4回もやれば、次は「ああ、来るの分かってた」になる。）

**「慣れはテンションを減らす」（"Familiarity reduces tension. The more you're familiar with something, the less tense it becomes."）** -- これはホラーゲームの根本的なジレンマだ。Yang 氏はその解決策について "I don't have an easy answer for you on that, besides a lot of testing"（たくさんテストする以外に簡単な答えはない）と率直に認めた。

### 初回 vs 2回目以降のプレイ体験

> "For horror games, it's very interesting because the first time you play a horror game, it's like this very thematic kind of tense experience. And then the second time you play it, you're like, oh, look at mine. Like, zero death, don't get hit, speed run. And then a lot of games even have specific trophies for that, which is like, beat the entire game without healing, beat the entire game under two hours."
>
> （ホラーゲームは面白いことに、初回はテーマ性のある緊張体験。2回目は「ノーデス、ノーダメ、スピードラン」になる。多くのゲームにはそのための専用トロフィーまである。「回復なしでクリア」「2時間以内にクリア」とか。）

> "First time, a horror game is very interesting. The first time it's a thematic experience and everything else becomes more of an action game where you are trying to master these vulnerability controls. That's where the accomplishment comes in on the subsequent runs."
>
> （初回はテーマ体験。それ以降はアクションゲームになり、操作の習熟を目指す。達成感は2周目以降から生まれるんです。）

---

## 12. ラーニングカーブの定量化問題 -- RPG vs メレー

メレー戦闘における成長の可視化は、RPGとは根本的に異なる課題を抱えている。

> "Let's say I play an RPG for 10 hours. You're like, how much better are you? You're like, well, that's easy. I'm 20 levels stronger and I have fire magic so I can deal with these water bastards."
>
> （RPG を10時間プレイしたら、どれだけ上手くなった？簡単に答えられる。20レベル上がって炎の魔法を覚えたから水の敵に対処できる。）

> "How do you play a fighting game for 10 hours? Like, how much better are you? I don't know if you play ranked, even if you come to like, how much stronger am I? How is my neutral better? My anti-air is better? That's my zoning, but I don't know. It's very tough to quantify that unless you're very, very introspective."
>
> （格闘ゲームを10時間プレイしたら？どれだけ上手くなった？ランクマッチをやっても、自分のニュートラル（中間距離の立ち回り）はどう良くなった？対空は？ゾーニングは？分からない。非常に内省的でない限り定量化は難しい。）

そしてこの定量化の困難さは、テンションの感じ方が常に変化することに直結する。"The first run is like this and the second run is like, well, now it's just an action game. It's not even a horror game anymore."（初回はこうだが2回目は「もうアクションゲームだ。ホラーゲームですらない」になる。）

この問題への対策として、チームは**RPG ライクなサポートシステム**を導入した。プレイヤーの機械的なスキル上達は数値化しにくいが、キャラクターの成長をシステムとして可視化することで「自分は強くなっている」という感覚を提供する。Yang 氏はスポーツゲームのキャリアモードが今やほぼ RPG になっていることを例に挙げ、サポートシステムの有効性を補強した。

> "We have a lot of support systems that say easy to understand progression at this point to kind of soften the blow."
>
> （分かりやすい成長のサポートシステムを用意して、衝撃を和らげています。）

> "It's about the player getting better versus the character getting better. I think you need both of these for games like this."
>
> （プレイヤー自身の上達と、キャラクターの成長。このタイプのゲームには両方が必要だと思います。）

段階的解放の仕組みも導入された。

> "You can't get everything at once. You kind of get some stuff and we kind of slowly open it up."
>
> （全てを一度には手に入らない。少しずつ開放していきます。）

竜騎士07のファン層への配慮も語られた。

> "When the game first came out, we found it was a lot harder for a lot of our audience, especially the ones that have been from the kind of the visual novel section."
>
> （ゲームが発売されたとき、特にビジュアルノベル方面から来たオーディエンスにとっては、かなり難しいことが分かりました。）

---

## 13. 情報の隠蔽 -- テンション生成の核心

### Silent Hill 1 の教訓

> "Lack of information or drawn attention to specific information. I think that is very key for horror games."
>
> （情報の欠如、あるいは特定の情報への注意の誘導。これがホラーゲームにとって非常に重要だと思います。）

> "I'm a big fan of Silent Hill 1. And something I won't ever forget is that when you're playing this game and you're aiming at an enemy, you don't know if you're gonna hit them or not. There's no crosshair, you're just kind of aiming generally in the direction of the monster... You don't know how many bullets you have. You have to open the menu to see how many bullets you have left."
>
> （私は Silent Hill 1 の大ファンです。忘れられないのは、敵に照準を合わせているのに当たるかどうか分からないこと。クロスヘアがなくて、モンスターの方向にだいたい向けているだけ。弾が何発残っているかも分からない。メニューを開かないと確認できない。）

この「不確実性」が生む緊張感を、メレー戦闘でどう再現するか。

### 3D空間の曖昧さを活用する

> "The thing with 3D is that attack distance is actually much harder to grasp. When you have a crosshair, you put it on the enemy, you pull the trigger, you're like, okay, I'm gonna hit whatever's in the crosshair. But if you're swinging like a melee weapon in a horror game, we're just, I think humans in general, a lot of people have, we're just worse at dealing with kind of 3D space, kind of gauging distance and it makes it much, much harder to kind of tell if things are gonna hit or not."
>
> （3D空間では攻撃距離を把握するのが本当に難しい。クロスヘアがあれば敵に合わせてトリガーを引くだけ。でもメレー武器を振るとなると、人間は一般的に3D空間での距離感が得意ではない。当たるかどうか判断するのがずっと難しくなります。）

> "From the screenshot, it's kind of hard to tell if you're gonna hit or not. That's the point, and that's what we want."
>
> （スクリーンショットを見ても当たるかどうか分かりにくい。それが狙いで、それが我々の望んでいることです。）

| 項目 | 遠距離（銃） | メレー |
|:---|:---|:---|
| 命中確認 | クロスヘアで明確 | **当たるか分からない**（3D空間での判断困難） |
| テンション | クロスヘアが答えを出してしまう | 当たるか分からない不安が持続 |

**メレー戦闘が本質的に持つ3D空間の曖昧さを、バグではなくテンション生成の資産として活用する** -- これが Silent Hill f のメレー設計における最も重要なインサイトだ。

---

## 14. 弱さの感覚（Feeling of Weakness）とリアクション設計

### リアクションは攻撃より重要

メレーゲームの古典的な問題として、「壁を殴っている感覚」がある。

> "In those types of games, when you're shooting like a zombie, you'll notice that when you hit the zombies, it's kind of like it just kind of flinches. It keeps walking towards you. It's kind of like a wall. You feel very weak, even though you're supposed to be using a very strong weapon. So it's about the reaction. The reaction is telling you that you're attacking weak."
>
> （ゾンビシューターで敵を撃つと、ちょっとひるむだけでまた歩いてくる。壁みたいだ。強力な武器を使っているはずなのに弱く感じる。リアクション次第なんです。リアクションが「あなたの攻撃は弱い」と伝えている。）

> "For us, the reaction is more important than the actual attack."
>
> （我々にとって、リアクションは実際の攻撃そのものより重要です。）

ワンインチパンチのアナロジーが引用された。モーションは小さくても、受け手のリアクションが大きければ、見る者は「強力な攻撃だ」と感じる。

### 「見ること」と「感じること」のギャップ

Silent Hill f では意図的にヒットリアクションを抑制する設計を採用したが、予想外の問題が発生した。

> "Even though we've really dumbed down the reactions when we're hitting them, because you're swinging around this kind of heavy metal pipe, it didn't feel as good as a lot of players in video. When you're playing the game, it's not too bad, but when you're watching video, like there's no strength in there. It doesn't feel good. It didn't feel like an impact."
>
> （ヒットリアクションを意図的に抑えたのですが、重い金属パイプを振っているにも関わらず、動画で見ると迫力がない。プレイ中はまだいいのですが、動画で見ると打撃感がない。インパクトを感じない。）

> "They're like, well, there's just no power behind these attacks. But we're like, well, isn't that the point? ... But again, what you're seeing and what you're feeling are two different things. So it's a big lesson we learned there."
>
> （「攻撃にパワーがない」と言われた。「でもそれが意図では？」と思ったけど...見ていることと感じていることは別物だ。これは大きな教訓でした。）

ヘビーアタックについてはヒットストップ（攻撃がヒットした瞬間にフレームを一時停止する演出）を大幅に強化して、この問題に対処した。

アニメーションの計画的な妥協についても率直に語った。

> "Early on we wanted to kind of have the weapon kind of bounce off the character. So we'd have more realistic attack animations. That probably would have made things feel a bit better. But again, based on time, we knew exactly how much time we had."
>
> （当初は武器がキャラクターに当たって跳ね返るような、よりリアルな攻撃アニメーションを作りたかった。それならもう少し手応えが良くなったかもしれない。でも時間の制約があり、持ち時間は分かっていました。）

**「見て良いもの」ではなく「遊んで良いもの」を優先する。ただしその判断が視聴者体験とプレイヤー体験のギャップを生むことがある** -- 正直な振り返りだった。

---

## 15. リソース設計 -- 「FULL と EMPTY の関係」

### リソーススカーシティの基本

> "Resource scarcity in horror games. You need to know what kind of relationship between full and empty. You don't know how much you have, you don't know how scary things are, you don't know when it's gone."
>
> （ホラーゲームにおけるリソーススカーシティ（資源の希少性）。FULL と EMPTY の関係性を理解させることが必要です。自分が何をどれだけ持っているか分からない、どれだけ怖いことが起きるか分からない、いつなくなるか分からない。）

> "I have a hundred bullets in my backpack, and I have one bullet, and I see an enemy. Am I gonna attack this enemy? Well, it depends on how much stuff I have in there."
>
> （バックパックに100発あるのか1発しかないのか。敵を見たとき攻撃するか？それは持っているリソース次第です。）

### 敵を倒してもリソースは増えない

Silent Hill f の大胆な設計として、**敵のリソースドロップを完全に排除した**ことが紹介された。

> "The big thing we did also was no resource drops, and you'll find that every enemy you kill, that's it. It's only a negative gain on your resource, it's not positive. So we're not encouraging people to hit enemies."
>
> （大きな決断として、リソースドロップをなくしました。敵を倒しても得られるものは何もない。リソースはマイナスにしかならない。つまり敵を攻撃することを推奨していないんです。）

> "However, give someone a weapon, and they're gonna attack every single thing in front of them. That's just the way it is."
>
> （ただし、武器を持たせれば目の前にあるもの全てを攻撃する。それが人間の性です。）

Resident Evil Resistance での経験がここでも引用された。

> "When we started that game early on, it was meant to be very kind of less combat-driven, kind of escape room type of game. However, as soon as you give someone a gun, they will shoot every single thing that moves, every single test. And it's just human nature, at this point."
>
> （Resident Evil Resistance を開発し始めたとき、戦闘は控えめで脱出ゲームのようなものを意図していました。でも銃を持たせた瞬間、動くもの全てを撃ちました。全テストで。もうこれは人間の本性です。）

### 意図的な薬の過剰配置 -- ゲームプレイとナラティブの融合

最も巧みな設計のひとつが、回復アイテム（Red Capsules）の配置だった。

> "The first ending is kind of this overdose moment where she just pills everywhere, you just kind of overdose on these red pills in the story. But in the game, we have these things everywhere. They're everywhere in the game, there's so many of them."
>
> （最初のエンディングは一種のオーバードース（過剰摂取）の瞬間で、ストーリー上で赤い薬を大量に摂取する。ゲーム内にもこれらは至るところにあります。本当にたくさんある。）

> "Because we knew the first time you go through the game that you're gonna be less familiar with what's going on, you wanna give players kind of that crutch, that kind of extra resources. So you're like, okay, just keep popping pills."
>
> （初回プレイでは何が起きるか分からないので、プレイヤーに松葉杖（クラッチ）、余分なリソースを与えたかった。「とにかく薬を飲み続けろ」という感覚です。）

そしてエンディングに到達したとき、プレイヤーは初めて気づく。ゲーム中ずっと薬を飲み続けていたこと自体が、ストーリー上のオーバードースと呼応していたことに。さらに Yang 氏は、この設計がゲームプレイ上の実益もあったと補足した。"It also made sense for the first playthrough because it helped you go through, it reduced a lot of the frustration for kind of first-time players."（初回プレイにおいてもフラストレーションを減らす実用的な効果があった。）2周目以降、プレイヤーがメカニクスに習熟すると、回復アイテムへの依存度は自然に下がっていく。

> "We didn't want players to get to the ending and be like, that's not what I did, that's not what happened. But that's the game we did by kind of forcing that narratively into the gameplay."
>
> （エンディングで「自分はそんなことしてない」とは思ってほしくなかった。ゲームプレイにナラティブを強制的に織り込むことで、物語とプレイ体験を一致させたんです。）

フォーラムでの反応を見たときの喜びも語られた。

> "I remember reading a lot of forums in the beginning, first came out, people were like, these guys suck. I can't believe they did that. I was like, yes, yes, yes."
>
> （発売直後のフォーラムを読んでいたら「こいつらは最悪だ、信じられない」と書かれていた。私は「よしよしよし」と思いました。）

![リソース設計](slides/gdc2026_silenthill_f_38.jpg)
*Active vs Passive Resource Management のスライド。Red Capsules、Bandage、Ramen などのアイテムが表示されている*

### ライトアタック vs ヘビーアタック -- リソース交換レートの可視化

銃のないゲームで「弱点を狙う」概念をどう実装するか。Silent Hill f は2種類の攻撃で解決した。

> "We have two types of attacks, light attack and heavy attacks."
>
> （2種類の攻撃があります。ライトアタックとヘビーアタック。）

ライトアタックで同じ敵を倒す場合、スタッガー（怯み）させにくいため反撃を食らいながら戦うことになり、多くのリソースを消費する。ヘビーアタックは高リスクだが、少ないリソースで同じ敵を倒せる。

> "The equivalent here is like taking down the zombie with only body shots, versus really lining it up, taking your time, and hitting them with headshots."
>
> （ゾンビをボディショットだけで仕留めるか、時間をかけてヘッドショットを狙うか。それと同じです。）

画面左下の月型ゲージが武器の耐久値を示しており、ライトアタックの消費とヘビーアタックの消費の差が視覚的に示された。

---

## 16. アクティブ vs パッシブ・リソース管理

Yang 氏はリソース管理を2つのカテゴリに分類する独自のフレームワークを紹介した。

> "This is what we call active versus passive resource management."
>
> （これは我々がアクティブ vs パッシブ・リソース管理と呼んでいるものです。）

| 区分 | パッシブ・リソース管理 | アクティブ・リソース管理 |
|:---|:---|:---|
| タイミング | 戦闘前・戦闘外 | 戦闘中のリアルタイム |
| 内容 | バックパックの中身、アイテム構成 | 今この瞬間の体力、スタミナ残量 |
| 思考レベル | 事前計画、合理的判断 | 瞬間的判断 |

> "Passive resource management for us is kind of like what's in my backpack, how many items do I have, what's my total health, just kind of progression level stuff... Active resource management is like how much health do I have in the moment. More importantly for us, how much stamina you have, how much sanity you have."
>
> （パッシブ・リソース管理はバックパックの中身、アイテム数、総体力、進行レベルなど。アクティブ・リソース管理は今この瞬間の体力、特にスタミナとサニティ（正気度）。）

銃を使うホラーゲームでは「弾薬残量とポジション」を管理すればよいが、Silent Hill f のメレー設計ではスタミナ、サニティ、武器耐久値が加わる。

> "It was very tough for a lot of people to process. So that's something we definitely underestimated. For a lot of game players, managing so many different types of resources simultaneously is hard."
>
> （多くのプレイヤーにとって処理が難しかった。これは確実に過小評価していた点です。多くのゲームプレイヤーにとって、同時にこれだけ多くの種類のリソースを管理するのは大変なことです。）

---

## 17. テンポ設計 -- 武器のリズム、レベルのリズム

### 武器ごとの異なるリズム

> "Everybody kind of gravitates towards a different type of rhythm, different type of music. If you play a fighting game, some people just start to gravitate towards the fastest characters... So for weapons it's not that you have a correct weapon. Different people like different rhythms, you have to give them that choice."
>
> （人はそれぞれ異なるリズム、異なる音楽に惹かれる。格闘ゲームでもスピードキャラが好きな人もいれば...武器に正解はありません。異なるリズムを好む人がいて、その選択肢を与えなければならない。）

> "It also has to feel different because if every weapon kind of has the same rhythm, you don't get that pacing difference and variety."
>
> （全ての武器が同じリズムだと、ペーシングの違いとバリエーションが生まれない。それでは駄目です。）

### レベルデザインへのテンポ適用 -- ダークシュラインの例

テンポの切り替えはレベルデザインにも適用された。

> "The Dark Shrine, it's much more combat heavy. All the weapons there have no durability, a resource that you have to worry about at that point in time so that we can focus on something else. And that's kind of like switching up the rhythm and switching up the tempo."
>
> （ダークシュラインは戦闘が多いエリアです。そこでは全ての武器に耐久値がない。それまで心配しなければならなかったリソースをひとつ取り除くことで、別のことに集中できる。リズムとテンポを切り替えるということです。）

ある心配事を意図的に取り除くことで、プレイヤーは別の種類の緊張感に集中できる。これがエリア単位でのテンポ切り替えの具体的な手法だ。

![テンポ設計](slides/gdc2026_silenthill_f_39.jpg)
*Tension | Time -- Key Components: Tempo (Pacing) と Rhythm*

---

## 18. スタミナ -- テンポ制御の核心

### 攻防共有リソースとしてのスタミナ

> "Stamina. What stamina is, this little bar down here, it basically regulates all actions for the player. The difference between action games and horror games for us is the amount of actions you can do, the rate at which you can do things. This is what we use to control the game."
>
> （スタミナ。画面下部のこのバーで全プレイヤーアクションを制御します。アクションゲームとホラーゲームの違いは、実行できるアクションの量と速度。これがゲームを制御する手段です。）

> "Both offensive and defensive actions use this resource that kind of regenerates over time. And it's gated by how much you have, what you can increase throughout the game, and then the time it takes to regenerate."
>
> （攻撃行動と防御行動がこの時間経過で再生するリソースを共有します。保有量、ゲーム内での増加量、再生にかかる時間でゲートされています。）

スタミナ切れの実演映像が流れた。

> "She's being over aggressive, so when this monster attacks, she cannot dodge. It's hit quite a lot of damage at this point."
>
> （攻撃的すぎたため、モンスターが攻撃してきたとき回避できない。この時点でかなりのダメージを受けています。）

> "You can think about using bullets to attack and defend in that case, kind of like an analogy."
>
> （弾薬を攻撃にも防御にも使うアナロジーを想像してください。）

Yang 氏はこのメカニクスの反省点にも触れた。"Even though it seems like a very clean mechanic, just because it's clean, just because it feels like it works, does it work for your particular audience?"（クリーンなメカニクスに見えても、実際にあなたのオーディエンスにとって機能するか？それは別問題だ。）

### フォーカス -- メレー版のクロスヘア

スタミナ回復の仕組みとして「フォーカス」が紹介された。

> "Focus. This is basically our analogy to aiming in horror shooters. The point of this is to slow down pacing in the game. So you're not just hitting the button, you're gonna attack nonstop, but you can break out of it at any point in time."
>
> （フォーカス。ホラーシューターにおけるエイム（照準）に相当する仕組みです。目的はゲームのペーシングを落とすこと。ただボタンを連打して攻撃し続けるのではなく、いつでも中断できる。）

武器を構える動作によってスタミナが回復し、次のアクションに備える時間が生まれる。銃のホラーゲームで「照準を合わせる時間」がペースを落としていたのと同じ機能を、メレー戦闘で再現している。

フォーカスから派生する2つの主要機能として、**カウンター攻撃**と**フォーカスアタック**が紹介された。カウンターは、フォーカス中に敵の特定の攻撃タイミングでヘビーアタックボタンを押すことで発動する。成功すると耐久値を消費せず、サニティ回復も得られる「フリーヒット」となる。ただし全ての攻撃がカウンター可能ではないため、「回避すべきか、カウンターすべきか」の判断にテンションが生まれる。Yang 氏はこれを "a combat jump scare"（戦闘版ジャンプスケア）と形容した。

![スタミナ設計](slides/gdc2026_silenthill_f_42.jpg)
*Stamina: Regulates pacing through action rate / Drives all combat actions / Gated by time and amount*

---

## 19. テンションの解放 -- 「待つこと > 瞬間」

### テンションの本質

セッション全体を通底する最も重要なテーゼが、終盤で提示された。

> "We talk about tension all this time. But releasing tension is just a support. The wait is greater than the moment, especially for horror games, or just horror in general. Once the moment happens, the tension's not there anymore."
>
> （テンションについてずっと話してきましたが、テンションの解放はあくまでサポートです。**待つこと（wait）は瞬間（moment）より大きい** -- 特にホラーゲーム、あるいはホラー全般において。瞬間が訪れた途端、テンションはもうそこにはない。）

> "It's tense when you're kind of waiting, but as soon as you attack or the monster attacks, it's like, oh shit, the tension's gone and replaced by something else, be it good or bad."
>
> （待っているときは緊張する。でも攻撃するか、モンスターが攻撃してきた瞬間に、テンションは消えて何か別のものに置き換わる -- 良いものであれ悪いものであれ。）

**テンションとは「これから何かが起きる」という予期の感覚である。出来事そのものではない。**

![Wait > Moment](slides/gdc2026_silenthill_f_44.jpg)
*"Wait > Moment" -- セッション全体のテーゼを要約するスライド*

### マスターキー -- システムブレーカー

テンションの蓄積がフラストレーションに転化する直前に使える「逃げ道」として、「マスターキー（Master Key）」というシステムブレーカーが設計された。

> "We have something called a Master Key, which is kind of a system breaker for us. In like ranged kind of games, this could be like a grenade or a rocket launcher. It's basically something where you're like, I don't want to think anymore. I'm just going to use this and bypass whatever challenge is in front of me."
>
> （マスターキーと呼んでいるものがあります。システムブレーカーです。遠距離戦闘のゲームではグレネードやロケットランチャーに相当するもの。「もう考えたくない。これを使って目の前の課題を突破する」というときの手段です。）

### 戦闘はダンスである

> "For us, combat is like a type of puzzle. It's the same energy. Every encounter is a puzzle that the player has to kind of get past. It's just, instead of kind of trying to figure out obtuse clues, it's more of a dance with the controls."
>
> （我々にとって戦闘はパズルの一種です。同じエネルギー。全てのエンカウンターはプレイヤーが乗り越えるべきパズルです。難解な手がかりを探すのではなく、コントローラーとのダンスに近い。）

Yang 氏は戦闘の本質を「じゃんけん」に例えた。"Combat, in all its forms, is basically just rock, paper, scissors. You're going to have better outcome or worse outcome or kind of equal outcome."（戦闘はどんな形式でも基本的にじゃんけんだ。結果は良いか悪いか引き分けかだ。）マスターキーは、そのじゃんけんに「銃」を持ち込むような存在だとも表現した。

マスターキーの使い道はプレイヤー自身が決める。

> "It's a master key to let them decide when I want to start using it. So in the other games, you have grenades, you have a rocket launcher. You won't want to save these because you know it's a limited resource. You have that tension there, but you get to the point where the tension starts turning to frustration. You're like, screw it, I don't want to deal with this, I don't think about it. I'm just going to use this, I'm going to get past there."
>
> （マスターキーは、プレイヤーにいつ使うか決めさせるものです。他のゲームではグレネードやロケランがある。限られたリソースだから温存したい。そこにテンションがある。でもテンションがフラストレーションに変わり始めたら「もう嫌だ、考えたくない、使って突破する」となる。）

### ビーストアーム -- 物語と一体化したシステムブレーカー

Dark Shrine のシステムブレーカーとして「ビーストアーム（Beast Arm）」が紹介された。見た目のインパクトに会場から反応が出たが、Yang 氏は笑いながら釈明した。

> "If you haven't played the game, you're going to be going like, what the hell am I looking at? Isn't this a horror game? But it makes sense. It makes sense when you play the game. Trust me. There's a lot of layers of symbology and reasons."
>
> （ゲームをプレイしていない人は「何を見ているんだ？ホラーゲームじゃないのか？」と思うでしょう。でも意味があるんです。プレイすれば分かる。信じてください。象徴と理由の層がたくさんある。）

Dark Shrine のもう1人の主人公「ヴァルキリー（Valkyrie）」は、ヒナコよりも攻撃的でないプレイスタイルが想定されている。そのため、フォーカスアタックがマスターキーの軽量版として機能する。

> "It's a much weaker version of this kind of Master Key. It still does the job."
>
> （マスターキーのずっと弱いバージョンですが、役目は果たします。）

ただし、これらのシステムブレーカーを安易に配置しすぎないことも強調された。

> "It's not like every single thing I pick up is a grenade or a rocket launcher. You can use these things sparingly."
>
> （拾うもの全てがグレネードやロケランではない。節約して使うものです。）

![Releasing Tension](slides/gdc2026_silenthill_f_45.jpg)
*Wait > Moment / Pacing / Resource Amount / Master Key (System Breaker)*

---

## 20. まとめ -- テンションの5つのドライバー

Yang 氏は最後に、セッション全体を簡潔にまとめた。

> "Key drivers of tension in horror games, especially in combat. You have to watch for frustration. Resource scarcity. Timing, how you regulate the pacing of the combat and your game itself."
>
> （ホラーゲーム、特に戦闘におけるテンションのキードライバー。フラストレーションに注意すること。リソースの希少性。タイミング、戦闘とゲーム全体のペーシングをどう制御するか。）

![Wrap Up](slides/gdc2026_silenthill_f_46.jpg)
*Key Drivers of Tension: Frustration / Information & Resources / Time & Rhythm*

Silent Hill f のメレーオンリーホラーの設計哲学を、5つの柱で整理する。

### 1. ピラー設計 -- フランチャイズDNAの継承と差別化

- 「マクドナルドで寿司を出すな」 -- プレイヤーの期待の枠内で創造的であること
- アトモスフィア（雰囲気）が全タイトルの共通DNA
- インダイレクト・ホマージュで旧作ファンにも新規プレイヤーにも対応

### 2. テンション設計 -- 恐怖の核心は「緊張感のビルドアップと解放」

- "Wait > Moment" -- テンションは出来事の前の待機状態に存在する
- 距離の反転（Flip the Script）-- 遠いほど危険、近いほど安全
- 情報の隠蔽 -- 3D空間の曖昧さをテンション生成の資産として活用
- AIクロスドロー -- 「当たるか、当たられるか」の同時性

### 3. フラストレーション管理 -- テンションとの境界線を見守る

- キャラクターの恐怖とプレイヤーの恐怖は別物
- 慣れがテンションを殺す -- 初回と2回目以降で異なる体験設計
- RPG ライクなサポートシステムで成長を可視化
- プレイヤー選択（難易度・アシスト）でフラストレーションを軽減

### 4. リソース設計 -- FULL と EMPTY の関係性

- 敵を倒してもリソースは増えない（ネガティブゲイン設計）
- アクティブ vs パッシブ・リソース管理の2層構造
- 回復アイテムの過剰配置 = ナラティブとゲームプレイの融合（オーバードース演出）
- ライトアタック vs ヘビーアタック = リソース交換レートの戦略性

### 5. テンポ設計 -- リズムでペーシングを制御する

- 武器ごとに異なるリズム（正解はない、プレイヤーの好みに委ねる）
- エリアごとのテンポ切り替え（ダークシュラインの耐久値なし設計）
- スタミナが攻防共有リソースとしてテンポを制御
- マスターキー（システムブレーカー）でテンション → フラストレーション転化を防止

---

## Silent Hill フランチャイズと日本IPの海外開発

本セッションは、ゲームデザインの知見にとどまらず、日本のIP を海外スタジオが開発するトレンドの一断面でもある。

Silent Hill は1999年に KONAMI の社内チーム Team Silent が生み出した、日本発のサバイバルホラーIPだ。Team Silent 解散後、開発は海外スタジオに移り、評価は不安定な時期が続いた。2022年の KONAMI Transmission で複数タイトルの同時進行が発表され、復活期に入った。

その中で Silent Hill f は、台湾・台北の NeoBards Entertainment が開発を担当する。シリーズ初の「日本を舞台にした作品」を台湾スタジオが手がけるという、文化的に興味深い座組だ。Yang 氏が繰り返し強調した「世界観の整合性（World Integrity）」-- 1960年代の昭和の日本を、その文化を経験した人にも、そうでない人にも説得力のある形で描くという課題は、まさにこの文化的な座組から生まれている。

NeoBards はコデベロッパーとして「スケジュールと予算はハードセット」「リリース日から逆算して設計する」「チームの強みに沿って方向性を決める」という徹底したプロダクション規律を持っている。これは外部開発スタジオの制約であると同時に、明確な強みでもある。銃を捨てる決断も、この規律の中から生まれた。

---

## 筆者の所感

セッション終了時、Yang 氏はスピーカー評価フォームの記入を促しつつ、最後まで笑いを忘れなかった。"Please fill out your speaker evaluation form... it determines if I have to work in the office or in the mines next week."（スピーカー評価フォームに記入してください。それで来週オフィスで働くか鉱山で働くかが決まります。）

60分のセッションの中で Yang 氏が一貫して伝えていたのは、「完璧な理想を追うな、手持ちのカードで最善のハンドを作れ」という実践的な哲学だった。

「メレーオンリーにした理由」の答えが、「かっこいいから」でも「差別化のため」でもなく、**「銃を1丁追加するだけでスコープが制御不能になるから」** という生々しい開発現場の判断だったことが印象的だ。クリエイティブな判断とプロダクションの現実は対立するものではなく、制約が創造性を生むのだという好例を見せてもらった。

また、「テンションはサポートに過ぎない。待つことが瞬間より大きい」 という定義は、ホラーゲームに限らずあらゆるインタラクティブ体験に適用できる原則だ。映画、音楽、料理 -- 期待感（アンティシペーション）は体験そのものより大きな感情を生むことがある。

台湾のスタジオが日本を舞台にしたホラーゲームを開発し、GDC で英語で語る。そのセッションを日本人の筆者がサンフランシスコで聴いている。ゲーム産業のグローバルな循環を実感する1時間だった。

---

## 参考リンク

- [GDC 2026 Session: 'Silent Hill f': The Challenges of Creating a Melee-Only Horror Game](https://schedule.gdconf.com/)
- [NeoBards Entertainment](https://www.neobards.com/)
- [Silent Hill f 公式サイト](https://www.konami.com/games/silenthill/)
