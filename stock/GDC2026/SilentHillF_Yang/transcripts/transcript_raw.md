# 'Silent Hill F' — 生トランスクリプト

> ※ Talksribe自動文字起こし。音声認識ノイズあり。随時修正・追記する。

---

## セッション開始・自己紹介（15:11〜15:14）

```
[15:11:30] It's in the sound engineer and then decided that there was a really formula. So I started over in QA, back in the States,
[15:11:36] working on on my properties over there. They came back to the US and it worked on the most famous by chronic.
[15:11:48] arena brother gave it all time. I'm going to write a little place. They're all stars. I'm going to write a little place. So people will just pick that up. Thank you.
[15:11:58] for sure. And then after the end of the chapter, go to Germany to work in the online kind of free to play a piece of space out there. Spend a few years out there before getting opportunity
[15:12:08] you go to Taiwan to work on 5.50-5 edition. I was a designer around that and then the game director
[15:12:12] Sight no death, which is what we're all here for today's very long story in.
[15:12:24] So if you have heard about this, so on at viewbars, we are a codev and external developer based up on Type-A, the Taiwan, and the remote branch
[15:12:34] and as soon as I've added some of the stuff we worked out previously, I'm always recently being Dead Rising Deluxe Me Master and it's on Hill Death.
[15:12:44] say that I'm very honored to be here today to talk to all of you guys, but it takes a team to build a game. And so some of the guys over at Neobarots over here and I think
[15:12:54] for all our things is for every game that we build. We actually put a website so everybody can kind of be moving message for the team everybody.
[15:12:55] website and all shows again at the end of the talk.
[15:13:08] But it's not just us because it takes a lot of people to build a game these days
[15:13:10] or code developer. We have an own external part of this too, everything.
[15:13:22] from MoCAP and sound design to localization tons of stuff. So if interested, that is the 4 credit list for sound hello app and we'll show that again at the end of the talk.
[15:13:35] So quick overview, what we're going to talk about today is kind of how we decide on the key vision for sound.
[15:13:45] Core as it supports games is tension and how we can use melee combat and everything that out
[15:13:55] that the first kind of mainline entry for over a decade in the Sunville franchise. And hope it's very different
[15:14:05] are not based in the town of San Hill or the neighborhoods, but we're based in a completely different areas, so in the 1960s,
[15:14:15] side. Another big difference is we are up here, the mainly focused game, no guns, head all. So I think not gonna be wondering what the G-pointless top is, as part of a
[15:14:21] towards my little combat. And the answer is, it was awesome. That's what we did in the death of my top. Thank you very much.
[15:14:37] Thank you. Anyway, it's okay, look work good.
[15:14:43] So, we're going to talk about how we set our key visions or our pillars really. So in the very beginning, when the one that's not our new sort of autonomousite came
```

### 解読メモ（自己紹介〜概要）

**Al Yang の経歴:**
- QAからキャリアスタート（アメリカ）
- ドイツでオンライン/F2Pゲーム開発を数年
- 台湾に移りNeobards Entertainmentへ
- **Dead Rising Deluxe Remaster**（"Dead Rising Deluxe Me Master"）を担当
- その後 **Silent Hill F**（"Silent Hill Death" / "Sight no death"）のゲームディレクター

**Neobards Entertainment について:**
- 台湾拠点のコデベロッパー（Co-developer / 外部開発スタジオ）
- モーションキャプチャ・サウンドデザイン・ローカライズなど多数の外部パートナーと協業
- クレジットリストをセッション末尾で紹介予定

**トークの概要:**
- Silent Hill Fのキービジョン（ピラー）の決め方
- ホラーゲームの「緊張感（テンション）」をコアとして支えるメレー戦闘の設計
- Silent Hill F の特徴:
  - **10年以上ぶりのメインラインナンバード作品**
  - 舞台はサイレントヒルの町ではなく**全く別の場所（1960年代の日本）**
  - **銃なし・完全近接戦闘（メレーオンリー）**
  - 聴衆から「なぜ銃がないのか？」という突っ込みが飛ぶ → Al Yangが笑いで返す

**ピラー設計の話が始まる（15:14:43〜）**

---

## ピラー設計とメレー採用の背景（15:14〜15:17）

```
[15:14:53] just have a very rough script and a few pieces of concept art. I mean, like five pieces of concept art.
[15:15:03] I know even the talk to you about my little combat, I think it's important to go backwards and see how we even got this decision in the first place.
[15:15:13] is when we had a few weeks, but I had to feel like a sound pill. But because it's been a long time since the franchise has been around, we had to take creative risks.
[15:15:23] The big thing to be kind of looked at is not just a South Hill, but four games in general, out there, use a lot of range to combat to create tension.
[15:15:33] And not just us, but a lot of other franchises out there. So this is something we really get into to quite early. We knew what the past was like.
[15:15:43] So we're going to talk about what we fit in, and then kind of the sound of franchises, kind of loosely defined kind of three errors,
[15:15:53] And we're doing these titles that come to post teams on error, we're kind of about the west. And then kind of where we are now, where Sun Hill is trying to kind of make a resurgence.
[15:15:55] and it's been set in places not just around the town.
[15:16:07] So all of these titles appear as kind of the sign hill titles coming out recently and we knew all about the at the very beginning when we knew we could do this game
[15:16:17] discussion with the economy kind of looking ahead how could we stand out. We had a few likes on Hill, but how do we stand up in the rest of these games? And one is like a
[15:16:27] or classic or a Sun Hill gameplay, it's more kind of interactive experience. And something very dangerous for us here
[15:16:37] is the point where it's kind of a cold status game. We're already there's a lot of things that people are like, okay, this feels like a side. This feels like a side-held.
[15:16:47] And the way I think like the thing about this is that one more thing about creativity is let's say you go to McDonald's and you get orders on it and you get this, you get sushi.
```

### 解読メモ（ピラー設計の背景）

**開発初期の状態:**
- 最初はラフなスクリプトとコンセプトアート**5枚程度**しかなかった
- まずメレー戦闘の話をする前に、そこに至った経緯を説明したい、と前置き

**なぜクリエイティブリスクが必要だったか:**
- フランチャイズが長期間休眠していたため、復活にはリスクを取る必要があった
- サバイバルホラーゲームは一般的に「遠距離武器（銃）で緊張感を作る」設計が主流
- Silent Hillだけでなく他の多くのフランチャイズもこのアプローチを採用していた

**Silent Hill フランチャイズの3つのエラ（時代）:**
1. **クラシック期**（Team Silent制作、初期作品群）
2. **西洋開発期**（西洋スタジオへのアウトソース時代）
3. **現在**（Silent Hill が復活しようとしている時代）→ 複数のSilent Hillタイトルが同時進行

**差別化の問題:**
- 同時期に複数のSilent Hillタイトルが出る中でどう目立つか？
- Silent Hillゲームには「クラシックゲームプレイ系」と「インタラクティブ体験系」がある
- 危険なのは**「コールドステータスゲーム（cold status game）」**になること
  = 「これがSilent Hillらしい」という要素を並べるだけで、自分たちの個性がない状態

**マクドナルドのアナロジー（続く）**

---

## フランチャイズDNAと差別化の哲学（15:16〜15:19）

```
[15:16:47] And the way I think like the thing about this is that one more thing about creativity is let's say you go to McDonald's and you get orders on it and you get this, you get sushi.
[15:16:57] Now, it's not worse. It's not a worse food. I think some people would say, like a higher quality food. But if you go to McDonald's,
[15:17:00] regular reason if you're there because you're following you want a recent burger. All right, that's what you need.
[15:17:12] expect even if I give you something better it's too different from what you're coming to expect if you're not gonna have a good time so that's a little
[15:17:22] you wouldn't work with maybe something like a fish burger chicken burger, something that can be creative but still fit with the kind of the cobweights of what players would be expecting.
[15:17:32] It is so different that it's completely recognizable to the franchise.
[15:17:42] practical and how to apply our creativity shoe. For us, common look in all of these titles is XQ.
[15:17:47] But what I don't know if you're looking at is cap is really unique, you need to be like askers. Things are always like this.
[15:17:58] seem a little bit of activated information side. So that's a big thing. But not only that,
[15:18:08] you want to kind of do a lot of internal modges that, you know, for fans who'd be very noticeable,
[15:18:18] for people in her yard that it was still be fun, but it wouldn't be like, oh, like a aha moment. So for instance, we have a level in the,
[15:18:28] It's how F where 90% of the doors are locked. People would be like, like, that is terrible game design. Why would you make a level where every inventory
[15:18:38] and to a minute, like 90% of the doors are locked. But we did that, the kind of replicate, but we didn't do it every year, for instance.
[15:18:48] and try to replicate the feeling of older games. So in the very beginning of the game, we have this law, lowly walk, kind of downhill into town, very rarely single, side by two,
[15:18:58] And kind of kind of like we had a lot of discussion about you some lower looks again nothing like right in your face
[15:19:08] you know, there's like a bugging aha there. On my personal favorites, this location go over our Seattle House, again, I can direct a lot to
[15:19:18] So, kind of aside, because we're talking about kind of being unique. For me, one thing we'll talk about kind of being completely fresh and unique.
```

### 解読メモ（フランチャイズDNAと差別化）

**マクドナルドのアナロジー（完全版）:**
> 「マクドナルドに行って寿司を出される。寿司は悪い食べ物じゃない、むしろ上質かもしれない。でもマクドナルドに来た理由はハンバーガーを食べたいからだ。より良いものでも、期待から大きく外れていたら良い体験にならない。フィッシュバーガー・チキンバーガーなら創造的でもフレームの中に収まる。」

→ **ポイント**: プレイヤーのフランチャイズへの期待（DNA）を守りながら、その中で創造的に差別化する。「全く別物」にならないこと。

**実践的なアプローチ:**
- Silent Hillタイトルすべてに共通する要素 = **霧（fog）**（"XQ"と誤認識）
- フランチャイズの「らしさ」を維持しながらも独自性を持たせる

**ファン向けのホマージュ（内部的なオマージュ）:**
- ファンには「ああ、これだ！」となる仕掛け
- 新規プレイヤーには普通に楽しめるが気づかない
- 例：**Silent Hill Fのあるレベルでドアの90%が鍵がかかっている**
  - 表面上は「ひどいゲームデザイン」に見える
  - でも旧作ファンには懐かしい感覚を再現するための設計
  - **すべてのレベルでやるわけではない**（やりすぎない）
- **ゲーム冒頭**：町に向かって緩やかに下っていく一本道の歩き（旧作へのオマージュ）
- 低キーな仕掛けで「露骨に顔面に押しつけない」方針
- Al Yang個人の好きな場所として特定のロケーション（"Seattle House"→おそらく別の名称）を挙げる

**次のテーマへ:**
- 「完全に新しく・ユニークであること」についての話に移行

---

## ジャンルサイクル論と脚本・世界観の整合性（15:19〜15:22）

```
[15:19:28] work we're building on the work on others, there's only so much to go around. And things that are usually popular usually you do around every few generations or so,
[15:19:34] years, 15 years or so. So example in your is just a beat-em-em-em, right here. I'm like, we're kind of at, I would say, even the middle of the beat-em-em-em-em.
[15:19:46] for a month or so. At the moment, however, do I like a big drought? 10 years is so ready to thousands to have a lot of people before they have like old nags for that.
[15:19:56] 10 years you get this research stuff the same type of game as that just for games It's just an idiot you know flex your movies. I think we are a detail in a pound of superhero
[15:20:06] But again, you look at as far back. That was what that there was a big group of spiritual movies. Then, and the whole thing is, it's not that these kind of mechanics
[15:20:16] play with these type of things or bad. This is that they get popular. We're going to park where after a while at then, you see like a little rabbit, a month of the initial amount of time,
[15:20:26] I'm tired of the, because there's too many that have died without it comes back. So again, it's not that it's not popular right now. I just remind myself that people keep talking about it
[15:20:36] don't play it for now but it's time to bring this kind of stuff back again kind of the past forms the future as well as do our research and that's why you don't
[15:20:40] Yeah, so people got it. Thank you. All right, I was listening to it. But that was a nice one.
[15:20:52] So the second thing we really work there is kind of our script and world integrity.
[15:21:02] I'll hear quite a bit of the open act of great. So, well, those of you who don't know this guy, he's been a very political author, a very strong body horde,
[15:21:12] the price would take a 1.5 and 2.5 is one. I really recommend to check it out. You'll find
[15:21:22] as we just were actually, Sont Hill hip, is very, as you can see, zero, seven like. Because the thing is, because we haven't known all or we really haven't even did this thought,
[15:21:32] still have to feel like solid feel. All right, so that was a big thing. We have there. But as you guys know, we really have to maintain. You can't just write a script if they get a game.
[15:21:42] You have to have a bright display play from the original story there. So it's been all of time kind of putting back together and usually stories are kind of like one major beat to another major beat.
[15:21:52] versus the stuff that's kind of a visual level structure. And with games, again, it's interactive. The pacing is dictated all the large part
[15:22:02] kind of take care of this and then just a lot of things we have to think about at this point in time. So I'm going to take you on for essentially two characters for this guy's double.
```

### 解読メモ（ジャンルサイクル＋第2ピラー）

**ジャンルサイクル論（メレーの根拠）:**
- ゲームのジャンルは**約15年サイクル**で波が来ては去る
- Beat-em-up（ベルトスクロールアクション）は今まさにそのサイクルの真ん中あたり
- 10年間の「枯渇期」の後、同種のゲームが大量に出てまた飽和する
- **スーパーヒーロー映画**と同じ現象（霊的映画ブーム → スーパーヒーロー映画ブーム）
- メカニクスやジャンル自体が悪いわけではない。ただ飽和する、そしてまた戻ってくる
- 「**過去が未来を形成する（The past forms the future）**」
- → メレー戦闘が今ちょうど戻ってくるタイミング、という自信の根拠

**第2ピラー：脚本と世界観の整合性（Script and World Integrity）**
- 著名な作家（政治的な作風・強烈なホラー）が脚本に参加
  - "1.5 and 2.5" → おそらく著作の参照（作家名はノイズで不明確）
- Silent Hill Fの脚本は「非常に実験的（very zero, seven like）」なアプローチ
- 課題：脚本を書くだけでは不十分 → ゲームとして「プレイアブルな物語」にする必要がある
- 通常の物語 = 主要ビートからビートへの線形構造
- ゲームの物語 = プレイヤーがペーシングを決める → 構造が根本的に異なる
- 次：**2人の主人公（2 characters）**についての話へ

---

## 2主人公の設計課題・世界観・外部開発の制約（15:22〜15:24）

```
[15:22:12] So just follow these things you see over here, but he's basically your double in the work on everything at that point in time. So that's actually our key mark
[15:22:22] I can't even start from the, or the piece of work from ours. The, here's how it says it. And you look at this, you're like, oh gosh, this is so this is supposed to be,
[15:22:25] these are having to also have came clear to both of these characters in there. How are you gonna do this? And wrenched!
[15:22:37] and copy one of the things that came up because if you put a bullet for one character, you have to do for the other character. I think this count the world's spoiler alert, kind of melt together at the end.
[15:22:47] have maybe characters that use Grinch that don't interact with the range. So this is something we talked about really really early on. I'll get to that a little bit later.
[15:22:57] real quick is just kind of flushing out the world. And so what's talking about the world integrity is that they have to feel willing to move to people of the culture.
[15:23:07] a concert or Japan, but it also has a feel of interesting people having been there, kind of with a bit of tourism if you will looking at the world. So it really has been a lot of time,
[15:23:10] make sure things will kind of tie to your electric, kind of the make sure of that things.
[15:23:23] you were below the culture of time period would feel great to kind of get that kind of sense of
[15:23:33] And if you know about, so the last thing, and this is kind of the side, I think is very important.
[15:23:43] All right, so as external or code developers, we have a very strong rule which if you do
[15:23:53] You already have a scale point. You already have a budget. And these are usually hard set. If you go on release, then there's no like, well, let's have a talk and maybe we'll extend your, you're like, you know, your mouth's home.
[15:24:03] like, no, this game is supposed to ship here and you know this year's advance. So for us, we've worked with backwards. And it's about not doing the coolest stuff possible,
[15:24:13] the stuff in the time and resources you have possible. And everybody wants to do the
[15:24:23] I feel better at the never shipping end of me in a photo said it's like play games play, but games always good, but it's a leg game enough.
[15:24:33] and you're out of the streets. So again, it's just been cracker, sometimes. And everybody wants to be the coolest thing.
```

### 解読メモ（2主人公の課題・世界観・外部開発の制約）

**2人の主人公（スポイラー注意）:**
- Silent Hill Fは**2人の主人公**が存在する（ダブル構造）
- 設計上の課題：**片方のキャラクターに何かを実装したら、もう一方にも同じものが必要になる**
  - 例：一方にある武器・アクション → もう一方にも用意しなければ整合性が取れない
- **スポイラー**：2人の物語は最終的に「溶け合って（melt together）」収束する
- 近接戦闘キャラクターであり、遠距離武器と絡まない設計（銃なしの補強）
- → この制約がメレーオンリーのシステム設計に早期から影響を与えた

**世界観の整合性（World Integrity）:**
- 舞台：**1960年代の日本**
- 日本人・日本文化に対して**リアルに感じられること**が大前提
- 同時に世界中のプレイヤーが「観光客の目線」で楽しめる作り
  → ローカルに深く根ざしつつ、グローバルにアクセスしやすい
- 文化・時代考証に多くの時間を投入

**外部開発の鉄則：スコープとリリース死守:**
- コデベロッパーとして、スコープ・予算・リリース日はすべて**ハードセット（変更不可）**
- 「リリース日延長をお願いできますか？」という選択肢は存在しない
- アプローチ：**リリース日から逆算して設計**
- 目標：「最もかっこいいものを作る」ではなく**「与えられた時間とリソースで最善のものを作る」**
- 「完璧だが未発売」より「十分に良く・発売済み」の方が価値がある
  → **"A good game shipped is better than a perfect game never shipped"** 的な哲学

---

## リスク管理とメレーオンリー決定の本当の理由（15:24〜15:26）

```
[15:24:42] with those. We really copied these risks. We thank for these danger points. So I don't know if you
[15:24:53] a story never been here for a year first time ever done this before I first never worked on this type of game
[15:25:04] point right there as part of the example. And we really want to kind of take the time and
[15:25:13] So we built characters before they were there before, you know, so like okay, we're not to start about this, what the heck, getting the pacing wreck into story pits,
[15:25:23] points. So when we apply for these things, we always budget like true through times not of time, get the stuff done. And if we go faster than we expect that's great,
[15:25:33] back but will never go over scheduled it could be planned for these danger points here. And that was one of the big things for Hienapo is going to look at range.
[15:25:43] and kind of spiral out of control very fast. So it's just a some phrase like, okay, let's have her use, you know, raise weapons, you're like, okay, now we have to build, okay,
[15:25:53] is it's a no targeting, does that mean she could be a Mexican body part so we have any animations. So like kind of, what she does with body parts. So there's three points now.
[15:26:03] extra resource in there is just one type of gun that can't abore and we have expert type of range. Let's do a boat list of that works well, okay, but both you have to
```

### 解読メモ（リスク管理＋メレーオンリーの真因）

**リスクを直視する姿勢:**
- 「危険ポイント（danger points）」を認識・記録し、計画に織り込む
- Al Yang にとってこのタイプのゲームは**初めての経験**（ホラーゲーム未経験）
- キャラクターを先に構築してからストーリーのペースを整合させる手法
- タイムバジェットの鉄則：**必要な時間の3倍を見積もる**
  - 早く終わればラッキー、絶対にスケジュールをオーバーしない

**メレーオンリー決定の本当の理由（スコープ管理）:**

> 「遠距離武器（銃）を持たせるだけで、スコープは一瞬で制御不能になる」

- 銃1本追加 → ターゲティングシステムが必要
- ターゲティング → 当たる身体部位ごとのアニメーションが必要
- 身体部位 → ヒットリアクション・VFX・ダメージ計算…と芋づる式に膨れ上がる
- **「遠距離武器を1種追加するだけで3つの新しい課題が生まれる」**

→ **メレーオンリーはクリエイティブな判断と同時に、スコープ管理の現実的な判断でもあった**

---

## 遠距離武器カットの決定・制作コスト対効果・チームの強みを活かす（15:26〜15:28）

```
[15:26:13] physics on the arrow, just to start one word is like, so we have range of spirals of control really really fast. And because again, this is a little like a danger point for us
[15:26:23] something. Good to get interested in. We decided to cut it quite early and also the manicure we had some class of part with her whether we're using like a dozen and just
[15:26:33] with the story we were trying to tell. The next thing we look at is the Prof. Cost of the Pigeon Scene. As I think this is pretty easy to digest
[15:26:43] and it adds an add like five points to the game. In terms of fun on the spending hundred hours, on this thing is an add like two points for most part, or for whatever gives more
[15:26:48] cost efficiency here again if you don't want to overreach on the stuff you try to build your strength and then if there's an example of this
[15:27:00] is for reasonable resistance if you guys have a scene as game before. It's a four versus one asymmetric PVP game. It's kind of like a haunted house.
[15:27:10] place the mesh right in the case as dead of crooks that is randomized that you kind of build and use this to kind of try to feed these forepaders, kind of try to escape from their
[15:27:20] And the turnaround for that project in terms of the pitch was a week for the pitch and a month for the prototype. So it's super fast.
[15:27:30] And what the reasons we decided to go this direction is actually we had a lot of people in the company, public really big, match it together and put them on fans.
[15:27:40] So of course, we're like, okay, you're going to stretch with this stuff. You have a lot of building knowledge. So go in this direction. We had, we could go much, much faster and kind of build it.
[15:27:50] their strengths because we didn't have to start over and kind of research how these things have to work. And again, it doesn't work for everybody and this is something that happens
[15:28:00] you try to build the strengths of your team and again, it's been a lot of time. This is actually the first I want to say,
```

### 解読メモ

**遠距離武器カットの最終判断（15:26:13〜）:**
- 矢（弓）の物理演算だけでもスコープが爆発する = 「危険ポイント」として認識
- **早い段階でカットを決定**
- ストーリーとの整合性からも遠距離武器は不要という結論に

**制作コスト対効果の分析フレームワーク（"Production Cost Analysis"）:**
- 「100時間かけてファン要素を追加しても、ゲームの楽しさを+2ポイントしか上げない機能がある」
- 一方で「5時間で+5ポイント」の機能もある
- **過剰なリーチをせず、チームの強みを活かすものを選ぶ**

**「Reasonable Resistance」の事例（4v1 非対称PVP）:**
- 4v1の非対称PVPゲーム（ホーンテッドハウス形式、Dead by Daylight的）
- ランダム生成マップでモンスター役がサバイバーを狩る
- ピッチ1週間・プロトタイプ1ヶ月という超高速開発
- 理由：**会社内に既にそのジャンルの知識を持つ人材が多かった**
  → 一からリサーチ不要で、チームの既存スキルを最大活用
  → 開発速度が格段に上がった
- → **チームの強みに沿って方向性を決める**ことの実践例

---

## テンションマップ・デザイナーの「フレーバー」管理（15:28〜15:30）

```
[15:28:10] I hope everything's planned out from here. We do this on paper, not because you should be paper-protective. Everything, but it's like, okay, when you plan for these things in advance,
[15:28:20] We know where we're supposed to have players feel tense. We know about how long the one players have spent in the year. What's going to happen?
[15:28:30] I want to go back and look at that at it for ares that, you know, like the tens you see on there, like, okay, this is really want this to kind of keep a chance of tension.
[15:28:40] and it's scheduled for this thing and it's pretty critical for us because we don't
[15:28:50] It's kind of a side and I know it's kind of a kind of a illusion thing to think here, but talk about the internal blocks here
[15:28:52] games is that all you really like is trying to do is to play games.
[15:29:04] team members, me and the strengths. So what this kind of means is let's say we build
[15:29:14] level designer on more than about hour and a half to hours of the game in a row because everyone has the only unique flavor, only taste, the way they think about things.
[15:29:24] If you have, if you split up your designers, split your team so it's kind of like, if people's thought process, if people's kind of flavors, constantly changing, you get also feds that pacing in the game.
[15:29:34] We have like a section that's like five hours of the same person designing. It's going to be like that for five hours. But you have different people going there, basically different on the same goal, with different people approaching it.
[15:29:44] have a very, very different. And something I like to talk about is how it flows the type of errors here, which is at the end of the day,
[15:29:54] signable, let's say the goal is, you know, they give me a classic ice cream, like giving the most classic ice cream flip. And my mind I'm thinking chocolate, but then our peanut
[15:30:04] Okay, I propose the know-load. At this point, am I wrong? Are they wrong? No, not at this at all,
[15:30:14] personal preference. Okay, so in this case what we like to do with our these is always that the team members pose this thing kind of use their ideal.
```

### 解読メモ（テンションマップ・デザイナーフレーバー管理）

**テンションを紙でマッピングする（Tension Map）:**
- すべてのテンション設計を**事前に紙で計画**する
- プレイヤーがどこで緊張すべきかを事前に把握
- プレイヤーが各エリアでどのくらい時間を過ごすかを把握
- テンションの山・谷をスケジュールとして管理 = ホラーゲームとして必須

**デザイナーの「フレーバー（個性）」問題:**
- 各デザイナーには独自の「フレーバー（思考の個性）」がある
- 1人のレベルデザイナーが**連続して1.5〜2時間分のゲーム**を設計するとその人のフレーバーが支配的になる
- 5時間分を1人が担当 → 単調になる
- **解決策**：デザイナーをローテーション → 異なる個性が同じゴールに向かって多様性をもたらす

**「フレーバーエラー」とアイスクリームのアナロジー:**
- 「クラシックなアイスクリームを」→ 自分はチョコ、相手はピーナッツバターを想像
- どちらも間違いではない、個人の好みの問題
- 解決策：各チームメンバーが自分の「理想」を提案してすり合わせる（続く）

---

## テンションの本質と現代ホラー戦闘の課題（15:30〜15:32）

```
[15:30:24] It's always your ideal, it's going to feel just like one person's game. Have a lot of ideas in there as long as they fit the goal.
[15:30:34] experience there. Again, don't want to take a step back and do what's best for the game
[15:30:41] Let's talk about tension. We just have a key challenge. We had and running into all this stuff for a side help.
[15:30:56] So scary, which is why I'm feeling right now standing here. And a lot of people play horror games, they're like, I want to feel scary.
[15:31:06] Now I'm playing a working pretty scary, but I want to see this. What we really think they're saying is you want to feel tense.
[15:31:16] being in tents for us. Hortings are about the buildup and the release of tension. The buildup is just important as a release. Flabbed you guys were kind of like horror gloves,
[15:31:26] for the killer actually does the, that is the most tense part. It's like, once you get stabbed, you're like, okay, then the gore kind of happens,
[15:31:27] at the point in time. So that buildup is the most important.
[15:31:40] to us. So for my opinion, I think modern working in combat is actually really hard to do because you have to be
[15:31:50] of wheelie and cheat areas. And like all the kind of games you had like fixed camera angles, you had like 10 controls you had.
[15:32:00] kind of, but now we kind of modern games, people are expecting kind of more smooth gameplay control. There's quality of life that has become kind of
```

### 解読メモ（テンションの本質・現代ホラー戦闘の課題）

**フレーバーエラーの解決策（続き）:**
- 自分の理想だけを押し通すと「1人のゲーム」になる
- **ゴールに沿う限り多くのアイデアを盛り込む**
- 個人の好みより「ゲームにとって何が最善か」を優先する

**ホラーゲームの本質 = テンション:**
> 「プレイヤーは"怖い"と言うが、本当に求めているのは"緊張感"だ」

- ホラーゲームの核心 = **緊張のビルドアップと解放（Buildup & Release）**
- **ビルドアップはリリースと同じくらい重要**
- 例：ホラー映画でキラーが行動する「直前」が最もテンションが高い
  → 実際に刺されたらもうテンションは解放されている
- → **Silent Hill Fにとって"恐怖の前の緊張感"をどう維持するかが設計の核心**

**現代のホラー戦闘が難しい理由:**
- 狭い空間・閉所での戦闘設計が難しい
- 旧来のホラーは固定カメラ・タンクコントロールという"不便さ"がテンションを生んでいた
- 現代プレイヤーはスムーズな操作性・QOLを当然として期待する
- → **快適な操作性とホラーの緊張感を両立させる**のが現代ホラー戦闘の本質的課題

---

## メレー戦闘で緊張感を生む設計：テンションの反転（15:32〜15:34）

```
[15:32:10] careful about how you make Janky and how you make it Janky. And for us it was even tougher
[15:32:20] ranged, but with the melee secondary. And for us, we split their arm, which is melee, as
[15:32:30] So we had to do is we had to take a lot of these kind of range systems and kind of open to a melee equivalent.
[15:32:40] So what you do fix, what isn't broken really, because four games have been usually these type of systems for a very, very little time.
[15:32:50] So something that's really kind of easy to understand is with a very story. The closer the enemy is to you, the more tense it's going to be because
[15:33:00] or when they're further away they can't hit you, but you can get them. All right, so we'll flip it. We have resource of scarcity, so folks, for instance, reload.
[15:33:05] And then tabriculating features like a crosshair, so in a horror game, it's not getting rid of your gun and you just start shooting.
[15:33:17] the crosshair, kind of go down to one angle at the correct place. It takes time to do these things and it slows down the pacing of the game quite a bit.
[15:33:27] we move to melee. All right. So, and we'll talk about this in detail, we'll let you have durability on the weapon, and have focus, which is kind of like an analog
[15:33:37] and then kind of flipped the script, which is open, you can have range, you don't have a range of advantage. So we had the same place as the enemy to kind of
[15:33:47] attack, but what if the enemy had more range of advantage on you, so it's more tense when you're further away from them, it's kind of flipping that thing. So we spend a lot of
[15:33:57] time with their animations and the text for our monsters were there much more agile
```

### 解読メモ（テンションの反転：メレー設計の核心）

**開発経緯の補足:**
- 当初は遠距離メイン・近接サブの設計 → 完全メレーに切り替え
- 遠距離システムをメレー相当に変換していく作業が必要だった

**遠距離ホラーの既存システムを参照（壊れていないから直さない）:**
- 弾の希少性（リロード）
- クロスヘアを合わせる時間 → ゲームペースを意図的に遅らせる = テンション生成

**【最重要】テンションの反転（Flip the Script）:**

従来の遠距離ホラー：
```
敵が近い → 危険・テンション高
敵が遠い → 安全（射撃で倒せる）
```

Silent Hill F のメレー設計：
```
敵が近い → むしろ安全（攻撃できる）
敵が遠い → テンション高（敵が突進してくる、対処困難）
```

> 「敵から**離れているほどテンションが高い**。距離を詰めて戦うことが正解」

- 敵に近距離レンジアドバンテージを与える設計
- 敵のアニメーションを**俊敏**にして遠くからでも脅威に見せる
- 武器耐久度 + フォーカスメーター（スタミナ的なアナログ）で緊張感を制御

---

## テンションとフラストレーションの境界線（15:34〜15:36）

```
[15:34:07] our spaces, much quicker. All right, let's blow up time on the animation of why that tells Big Elves to have really an attention to the player in these situations.
[15:34:09] But...
[15:34:21] actually frustrates a lot of people and that's kind of coming next thing we want to talk about, which is frustration, which is really hard to quantify in terms of what is something that's
[15:34:31] and then once something is frustrating, and it's different for everybody, actually. Or what's challenging for someone, might be good to easy for someone else.
[15:34:41] to build around that. But at the same time, you can have a little tension completely because again, that's not as the
[15:34:51] you have to feel accomplished when you get past this challenge. And the thing here is there's a difference between what the character on the screen is
[15:35:01] And what you guys have to play here is if you're not the character mid-scared, but you like, it was not scary at all.
[15:35:11] six to years, not dating myself. But again, sometimes more experienced
[15:35:21] There you are. It's kind of like when you've never seen this, you walk into an area to video games. It's like, it's strangely empty here and there's all these health and you know like,
[15:35:23] It's like, I wonder what's going on.
[15:35:35] And I was watching my nephew play, it's game game game. It's like, so oh my god, there's a little boss who's here, I'm like, oh, oh, oh, oh my goodness.
[15:35:45] the piece I'm like, I'm sure what's surprised, but after like three or four more times of
[15:35:48] I'm going to do it.
[15:35:59] We don't want tension, we want players to feel tense, but we're very tough to kind of press the line. Frustration is something we struggle with constantly, we've not.
```

### 解読メモ（テンション vs フラストレーション）

**敵のアニメーションと空間の俊敏性:**
- 狭い空間でも敵が素早く動く → プレイヤーの注意を引き続ける
- アニメーションの質がテンション維持に直結

**フラストレーションとテンションの違い（定量化が難しい問題）:**
- テンションが高すぎると**フラストレーション**になる
- フラストレーションの閾値は**人によって異なる**
  - ある人には適度な挑戦 = 別の人には簡単すぎる
- テンションを完全に排除することもできない → それはホラーではない
- **チャレンジを乗り越えたときの達成感**がホラーの快感

**キャラクターの恐怖 ≠ プレイヤーの恐怖:**
- 画面のキャラクターが怖がっていても、プレイヤーが怖いとは限らない
- 経験豊富なプレイヤーは怖さを感じない場合も
- 典型例：「広い部屋 + ランダムな回復アイテム配置」→ 熟練者は即座に「ボス部屋だ」と察知
- **甥っ子の事例**：初見のボス戦 → 甥っ子は大パニック → しかし3〜4回失敗して慣れると全く怖くなくなる
- → **慣れがテンションを殺す**（familiarity kills tension）

**核心的な課題:**
> 「プレイヤーに緊張感を感じてほしい。でもテンションとフラストレーションの境界線を踏み外さないのは非常に難しい。これは我々が常に格闘し続けている問題だ。」

---

## 慣れによるテンション劣化・ラーニングカーブの定量化問題（15:36〜15:38）

```
[15:36:09] on that besides a lot of tests. All right, and that's mainly because, again, there's this mechanical perception
[15:36:19] attention the more your more is something less tense becomes I'm this is a tension there but it's much easier for you to go around and deal with it
[15:36:27] So again, for more games, it's very interesting because the first time you play a horror, it's like just very dramatic kind of tense experience and then the second time you play it, you're like, oh
[15:36:39] Again, my, like, you know, don't get hit, it's be grung. And then, well, my game's been half. It's been so tropeous for that, it was like, we've been tired game without healing. You're like, you're like, you're like, you're like,
[15:36:49] game under two hours, you're like, you can target him, you use it like, like, less than three
[15:36:59] and then it becomes more of action game where you are trying to master these unagulated controls. That's where the accomplishment comes in
[15:37:01] of it, right?
[15:37:12] So this is the very, something very interesting for us because taking this account, because we did want the first round to be super frustrating
[15:37:22] when we had a lot of players coming in my happy feeling with these type of games and then
[15:37:32] So I'll talk about learning race here and how to quantify it as tough.
[15:37:42] You're like, oh that's easy. I have 20 level stronger, and fire magic sucks. You'll lose a lot of faster. It's hard to get over here.
[15:37:52] And how do you play fighting game for 10 hours? Like how much better are you? I don't have a play ranked, even if you go for a leg. I'm just stronger in my health.
[15:38:02] I knew it was better and it was better. That's my zone, but I don't know. It's very tough to quantify the other zone. Very, very, very expected.
```

### 解読メモ（慣れによるテンション劣化・ラーニングカーブ）

**機械的習熟によるテンション劣化:**
- 操作に習熟するほど、そのシステムが**テンションを生まなくなる**
- 対処法を知っていると同じ脅威が怖くなくなる

**ホラーゲームの初回 vs 2回目問題:**
- 初プレイ：非常にドラマチックで緊張感のある体験
- 2回目：「ここでダメージを受けなければいい」という攻略目線になる
- 旧作ホラーゲームのスピードラン例：
  - ヒーリングなしでクリア
  - 2時間以内にクリア
  - タンクコントロールを完全に習熟してアクションゲーム化
  - **達成感はここから来る**（難しいシステムを極めた感覚）
- → **ホラー → アクションゲームへの変容**が起きる

**初回プレイ体験の設計:**
- フラストレーションになりすぎないよう設計したい
- 同時に、このタイプのゲームに慣れた経験者プレイヤーも多く来ることを考慮

**ラーニングカーブの定量化問題:**
- RPG的な成長（Lv+20、炎魔法が弱い等）= **測定可能・わかりやすい**
- 格闘ゲームを10時間プレイした後にどれだけ上達したか = **測定困難**
  - 「自分が上手くなったのは分かるが、どれだけ上手くなったかは分からない」
- メレー戦闘のスキル成長も同様に**定量化が非常に難しい**
- → 設計者がプレイヤーの成長を客観的に評価するには多くのテストプレイが必要

---

## サポートシステム・プレイヤーの選択と難易度設計（15:38〜15:40）

```
[15:38:12] of perceived the game in terms of tension. It's constantly changed. Like we said, the first run is like this and the second run is I will now assist the action in play in the
[15:38:22] Because of what we have, a lot of support systems can say easy to understand progression
[15:38:32] So even though I've been playing this game for this long, and I'm not sure how much we gotta have mechanical, like the merriety, I'm like this much better, sure. I think this important,
[15:38:42] You can kind of sport-skinned career modes right now. They'll bring much RPGs at this point in time. I think that's, again, a sport system really helps.
[15:38:52] can't afford to gain like that. Again, it's about a player getting better versus the
[15:39:02] those kind of like-like with players decide whether they want to take advantage of these systems. So we love this, you can't get everything at once, you kind of get some stuff and we kind of
[15:39:12] that, so we don't get the framework of the flick. It's at once, but if you guys have been reading forums playing the game, you know that there's some really
[15:39:21] at the first round, again, that is on purpose because we want the players to take advantage of the systems and change the way the game is completed.
[15:39:32] all this tension is really reduced with player choice. But honestly, the more choices it takes more time, you don't always have this.
[15:39:42] first came out. It was a lot harder for a lot of audience. It's especially ones that have been from the kind of visual novel session. So it's been a
[15:39:53] that you want to enjoy as a part of the maybe expand we need it. And it's very tough because you usually build your audience, but you're not quite sure how important you're going to pull in.
[15:39:58] As for this but when you build it stuff it's more just choice. Have many choices you can. When you play it to reduce that frustration.
```

### 解読メモ（サポートシステム・プレイヤー選択・難易度）

**サポートシステムで成長を可視化:**
- メカニカルなスキル上達は数値化しにくい → **RPGライクなサポートシステム**で補完
- プレイヤーが「自分はこれだけ強くなった」と感じられるシステム
- キャリアモード・アンロック要素 = 成長の可視化装置
- スキル成長が見えにくい分、サポートシステムが特に重要

**プレイヤーの選択によるテンション軽減:**
- すべてのシステムを最初から開放しない = **段階的解放**
- プレイヤーが自分でシステムを活用するかどうかを選べる
- **初回プレイが意図的に難しい** = サポートシステムの活用を促す設計
- → テンションはプレイヤーの選択によって軽減される

> 「テンションはプレイヤーの選択肢の多さによって軽減される。選択肢が多いほどフラストレーションは下がる」

---

## 情報の隠蔽がテンションを生む（15:40〜15:42）

```
[15:40:10] All right, next thing I want to talk about is information. So this is, I think, the most critical things here,
[15:40:20] or draw attention to specific information. I think that is very too full of it. So I'm going to be then of side hill one
[15:40:24] and something out of that, and I don't know if I forget, because I'm a playboy this game, and you're having an enemy. Don't know, but you don't know, or you can hit the one you do.
[15:40:36] You don't know if you hit the more that. And these types of no processor is kind of a main generally in the direction of master,
[15:40:46] And you might hit it if it just plays the good. You don't have to bullets you have. You have to open the menu to see how you bullets you have left. No.
[15:40:56] tension at this point. But we felt like that feeling was really strong for
[15:41:06] tension there. Now it's time the major thing we focused on. So the thing with 3D is that
[15:41:12] crosshair, click on enemy, pull the trigger, you're like, okay, I'm gonna hit whatever in the crosshair. Once you're swinging, I can't make it any way.
[15:41:24] and they were just attacking them in general. A lot of people have worse at dealing with kind of 3D space
[15:41:34] much, much harder to kind of tell. If things are hit or not, and then get with the focus on this area, like we can't buy a game setup. You're very good at moving
[15:41:44] in the only enemies, so that's not what you have to do to kind of like make sure you this whatnot.
[15:41:54] and I focused on how we obscure information in this area to make it more tense, based on is it gonna hit or not.
[15:42:01] This is our heavy attack on our title weapon. From the screenshot, you can kind of hard to tell if you're gonna hit her.
```

### 解読メモ（情報の隠蔽によるテンション生成）

**【重要】情報の隠蔽（Information Obscuring）がテンションの核心:**
> 「これが最も重要なことだと思う。特定の情報にプレイヤーの注意を引きすぎると、テンションが失われる」

**Silent Hill 1 の古典的事例:**
- 敵に当たっているかどうか分からない
- 弾丸が何発残っているかHUDに表示されない → **メニューを開かないと確認できない**
- この「不確実性」が強烈なテンションを生んでいた

**銃（遠距離）のクリアな情報 vs メレーの曖昧な情報:**

| | 遠距離（銃） | メレー |
|:---|:---|:---|
| 命中確認 | クロスヘアで明確（「当たる」が分かる） | **当たるか分からない**（3D空間での判断困難） |
| テンション | クロスヘアが答えを出してしまう | 当たるか分からない不安が続く |

- 3D空間でのメレー攻撃は**当たるかどうかが本質的に曖昧**
- 人間は3D空間の当たり判定を直感的に把握しにくい
- → **この曖昧さをテンション生成に積極的に活用**

**ヘビーアタックのスクリーンショット例（15:42:01）:**
- タイトル武器のヘビーアタック画面
- スクリーンショットを見ても「当たるかどうかが分かりにくい」 → 意図的な設計

**多様な視聴者への対応:**
- リリース当初はビジュアルノベル系ファン（SHのストーリー目当て層）には難しすぎた
- ホラーアクション経験者とビジュアルノベルファンでは求めるものが違う
- **「作れるなら選択肢を多く」**がフラストレーション対策の基本哲学

---

## 攻撃の同時性・AIのクロスドロー・アニメーション研究（15:42〜15:44）

```
[15:42:13] Now let's put enough we wonder like when you we're there the speed of the attack as speed of what it comes out of the enemy you do it one through the very tense
[15:42:23] So this is kind of a video of this happening. As you see, when you're raising that pipe,
[15:42:33] when it's breathing, it's really tough to tell if your action is going to connect or not just this life. And this is something we spend a lot of time, actually.
[15:42:43] what kind of animation for the actions of an enemy, you can see that it's going towards enemy and you prepare to raise the pipe. AI is like a lot of getting to mount the attack pose
[15:42:50] And the whole purpose of that is like, am I gonna hit or am I going to get hit? There's that tension in that moment right then.
[15:43:01] happens. The business will allow AI to do this cross-drawn to make it very tense when you're attacking, especially for high-risk, high-roar moves like your heavy attack.
[15:43:03] Yeah.
[15:43:15] So, going back to kind of like the kind of like a world issue, we have to make things very realistic from sort of a far-locked animation, we spend a lot of time to research,
[15:43:25] like the actual visual. So this is the preference for black and black which is our first posture.
[15:43:35] So a lot of the motions basically went ahead and we wanted to make sure if the light was the actual thing it was to shoot life all these things
[15:43:38] go from that. Very kind of smooth. The watch is kind of performance and it's done.
[15:43:50] in the tech there's you're always kind of watching it's kind of the jumpscare at that point time
[15:44:00] want to make sure that the enemy had the range and range at all times to the player group. Again, we're flipping the script on his head.
```

### 解読メモ（攻撃の同時性・AIクロスドロー・アニメーション）

**「当たるか、当たられるか」の同時性テンション:**
- パイプを振り上げている間、敵も攻撃モーションに入る
- プレイヤーの問い：**「自分の攻撃が当たるのか、それとも自分が当たられるのか？」**
- この「どちらになるか分からない瞬間」がメレー戦闘の緊張感の本質

**AIのクロスドロー（cross-draw）設計:**
- AIが意図的に**プレイヤーの攻撃と同時に攻撃モーションに入る**仕様
- ヘビーアタックなどハイリスク・ハイリワードの行動で特に顕著
- → リスクを取るほどテンションが上がる設計

**世界観とアニメーション研究:**
- 1960年代日本の動作をリアルに再現するため大量のリサーチ
- 実際の所作（和服の動き・伝統的な姿勢など）を参考に
- モーション全体がスムーズで自然に見えるよう設計
- 監視する動作 = ジャンプスケアのような緊張感を生む

**「フリップ・ザ・スクリプト」再確認:**
- 敵は常にプレイヤーに対してレンジアドバンテージを持つ設計
- = 遠ければ遠いほど危険、近づくほど安全という反転構造の徹底

---

## ショットガンFPS比喩・ボス変身ステージ・複数脅威によるテンション（15:44〜15:45）

```
[15:44:10] the safety and more dangerous. It's the further way you are, the more dangerous is. You can kind of think about this, is it's like a FPS, and it's just have a shotgun,
[15:44:20] that's a bad time. If you have to go and find them and get close to them. It's a very tense. If it was an hyper-epal, that was never rifle, then most people would be like kind of a white hole.
[15:44:30] kind of deal. Again, this is kind of a big thing we thought about. And not only on the end of these themselves, but kind of on those stages, we want to have a lot of attention there, so this is
[15:44:40] in the game's boilers. And then the purple wheels pop jealous of this kind of thing, this character. But as she did his game,
[15:44:47] kind of like have a stage of transformed, like there's lava going everywhere. And it's really hard to see what's going on. Actually, she's doing lava, she's just lava going up everywhere.
[15:44:59] is enemies everywhere and again, the purpose of that is to create more attention because we knew that it's hard to focus on more than one thing in this type of kind of
[15:45:05] So every extra thing you had on their iPad, apps and stuff, and that's something we're very, very cognizant of.
```

### 解読メモ（ショットガン比喩・ボス変身ステージ）

**「ショットガンFPS」比喩でフリップ・ザ・スクリプトを説明:**
- ショットガンしか持っていないFPS → 敵に近づかないといけない = 非常に緊張
- スナイパーライフルしか持っていないFPS → 遠距離から狙い撃ち = 緊張感が低い
- SHFでも同様の構造：**遠いほど危険（敵のリーチ）、近づくほど安全（自分のメレー）**
- → 「遠距離 = 安全」という一般的なゲームデザインを意図的に逆転

**ステージ設計でも同じテンション哲学を適用:**
- 敵個体だけでなく、**ステージ自体**でもテンションを生み出す
- あるボスキャラクター（「she」）の変身演出例：
  - ステージが溶岩で覆われる（lava going everywhere）
  - 視覚的に状況が把握しにくい → **「何が起きているか分からない」不安**
  - 敵が複数出現する
- → **複数の脅威に同時に対処しなければならない状況** = テンションの急上昇
- 「1つのことに集中するのが難しい状況を意図的に作る」
- ゲーム内に追加される全ての要素がテンションに貢献するよう意識的に設計

---

## 弱さの感覚（Feeling of Weakness）とリアクション設計（15:45〜15:47）

```
[15:45:16] It's a very Christmas problem.
[15:45:27] So one of the problems around it is this kind of feeling of weakness. We're very common hippy, which you couldn't feel like a powerhouse at his in the first place.
[15:45:39] very tough because the carpet heavy game. And this is something we've read into that was
[15:45:48] with zombies and like, really like hop-off dudes.
[15:45:59] those type of games like an issue like a zombie, the most that when you hit the zombies
[15:46:09] It keeps blocking for you. It's kind of like a wall. You feel very weak even though you're supposed to be using a very strong weapon. So it's about to get about the reaction.
[15:46:19] attack a week. All right. So, again, it's for us the reaction is more important than the actual attack.
[15:46:29] It's like, it's like, not very much movement on the kind of movement, but the reaction
[15:46:38] a tech. You know, one bunch of man-off videos watch that, but it's the given given
[15:46:49] and SoundCloud F even though we don't now in the reactions when you hit him them.
[15:46:56] In video when you're playing the game it's not too bad. But like watching a video is like there's no strike on the highest. It doesn't feel good. Then you'll like it impact. And that was a one.
```

### 解読メモ（弱さの感覚・リアクション設計）

**「弱さの感覚」= メレーゲームのクラシックな問題:**
- "Christmas problem" → "classic problem"（音声誤認識）
- **パワーハウスのように感じられない問題** — メレー重視ゲームの典型課題
- ゾンビゲームの典型例：
  - ゾンビを攻撃してもブロックされ続ける = **壁を殴っている感覚**
  - 強力な武器を使っているはずなのに「弱い」と感じる
  - → テンションよりもフラストレーションに転化する

**核心的な結論: リアクションが攻撃より重要**
> 「我々にとって、エネミーのリアクション（ヒット反応）は実際の攻撃そのものより重要だ」

- 攻撃モーション自体の動きは大きくなくても良い
- **ヒット時に敵がどう反応するか** = 打撃感・爽快感・恐怖感の源泉
- 格闘映像（man-on-man fighting videos）を大量に研究してリアルなリアクションを参考に

**Silent Hill F でのリアクション問題（"SoundCloud F" → "Silent Hill F"）:**
- SHFのヒットリアクションを重視して設計したが課題が発生：
  - **ゲームプレイ中** → 問題なく、ちゃんと手応えを感じる
  - **録画映像（動画）で見ると** → 打撃感が薄く見える
  - 「動画で見ると一番強い打撃がヒットしているように見えない。良く感じない」
  - → プレイヤー体験と観客体験でのギャップが課題


---

## 「見ること」と「感じること」の違い・アニメーション決断・リソース設計（15:47〜15:49）

```
[15:47:08] problem even though that was the goal. And they're like, oh, you just just no power, I have to text, but we're like, well, is that the point? Kind of do.
[15:47:18] but again, which is seeing in which fielding are two different things. So it's a big lesson you learn there. We, but for our kind of heavy attachment,
[15:47:28] to go overboard on that with things like it's up, or we really, really want to feel very strong on this kind of stuff, but again,
[15:47:38] And going back to kind of more talk about planning is early on in the morning, kind of habits,
[15:47:47] realistic attack animations. That probably would have made things feel better, but again, based on time, new exactly how much time we had, we had a lot of different
[15:47:58] really different shapes and sizes. We have a lot of different weapons. Both are on, fun, time here, I'm going to sing here and then darken apples.
[15:47:59] Okay, that just basically
[15:48:10] and we have to do different demonstrations so that's one of the things we made a higher decision on really on to kind of save time.
[15:48:21] Next thing to talk about resource. I think this is very obvious for a lot of people here which is resource scarcity in hard days.
[15:48:31] know what a relationship between full empty. Like to say, you don't know how scared
[15:48:41] You don't have to cut that resource exchange rate, but you need to have a preference point for all of these.
[15:48:51] and I have one bullet that I see enemy. Am I going to attack this enemy? Well, that's not how much stuff happened there. That's a big thing. If I kill this enemy, it's going to take one hit,
[15:49:01] and of course, you can take 10 hits that's an influencer decision too. And the big thing that you also do is no resource drops, then it's gonna be a kill that's it.
```

### 解読メモ（見ることと感じること・アニメーション・リソース設計）

**「見ること」と「感じること」は別物 — 大きな教訓:**
- フィードバックがあった：「パワーがない感じがする（no power）」
- 開発チームの反応：「でもそれが意図だったのでは？（is that the point?）」
- → 客観的に見て「迫力がない」と感じられることと、プレイヤーが操作して「強さを感じる」ことは**全く別の体験**
- > **"seeing and fielding（feeling）are two different things"** — これは大きな気づき
- ヘビー攻撃については特にこの傾向が顕著で、実際はかなり強くなるよう設計している

**アニメーション制作の計画的な妥協（早期決断）:**
- より**リアルな攻撃アニメーション**を作れば体感は向上したかもしれない
- しかしスケジュール上の制約（制作時間）から早期に「どこまで作るか」を決断
- 多様な武器・多様な形状とサイズ = アニメーションの組み合わせが膨大
- → **時間節約のためにアニメーション数を制限するという上位決断を早期に行う**
- 妥協の範囲を事前に計画しておくことが重要（スコープクリープ防止）

**リソース不足（Resource Scarcity）= ホラーゲームの古典的手法:**
- 「これは多くの人が分かると思う」 = 業界では周知の手法
- リソースが**満タンか空か**の関係性を理解させること
- 「弾が1発しかない状態で敵に遭遇する」
  - 攻撃すべきか？ → そのリソース計算がプレイヤーの脳を動かす
  - 敵を倒せるか？ / 倒すのに何発かかるか？ / リソースドロップはあるか？
  - → **この意思決定プロセス自体がテンションを生む**
- リソース交換レートを細かく設定する必要はないが、**基準点（preference point）は必ず持つ**
- リソースドロップがない敵 = 倒すメリットが「排除」だけ → それ自体が戦略的判断を迫る


---

## リソース設計の続き・リソース交換レート（15:49〜15:50）

```
[15:49:11] negative gain on resources not positive. So we're going to encourage people to hit enemies.
[15:49:21] and from the, that's the way it is a story back to everything we're resistance. When we start that game or you know it was meant to be very extra kind of like
[15:49:31] and I'll just come back to your account, a Skate Room type of game. However, as soon as you get something done,
[15:49:41] And it's just a system in nature. But so that's the one that resource driver was informed. Some people realized that something was needed,
[15:49:51] but something you want to push, which don't want to text up, it don't need to. Another thing here is kind of resource exchange rate on other side,
[15:50:01] I was going to need in order to reduce that damage. But she'll let damage back. And you see, there's a lot of just like the horror in the general, it's like, we're just going to, you know,
```

### 解読メモ（リソース設計の続き・交換レート）

**「ネガティブゲイン」でも戦闘を促す設計:**
- 敵を倒してもリソースドロップがない（ネガティブゲイン）場合でも、**戦闘を選ぶ理由を作る**
- 「敵を倒すことを促す = 安全な通路の確保」という正当なメリット
- ピュアなリソース計算だけでなく、「通れるようになる」「スペースが確保される」という文脈的な価値

**ゲーム開始時の方向性の変遷:**
- 当初は「非常にExtraな（余裕のある/探索的な）」ゲーム設計を意図していた
- スケートルーム（Skate Room）型ゲーム（=自由探索・ゆったりした体験）を目指していた節がある
- しかし実装が進むにつれて自然とシステムが収束 → リソース設計が必要と判明
- 「誰かが何かが必要だと気づいた」= 必要性から逆算してリソース設計を導入

**リソース交換レートのもう一つの側面（ダメージ交換）:**
- ヒールするためにリソースが必要
- → **「ダメージを受けてもリソースを使って回復する」という交換**がプレイヤーの意思決定に入ってくる
- ホラーゲームの一般的傾向：「とにかくプレイヤーにリソースを使わせる」方向に行きがち
- → SHFはこのバランスを慎重に設計している


---

## 意図的な薬過剰配置・初回プレイ体験設計・弱点システム（15:50〜15:52）

```
[15:50:07] difficulty or damage you take because of this kind of thing. But we did this on purpose, which is if you guys will want to gain the...
[15:50:19] The first ending is kind of can't go half as overdose moment where she's like tells every worship and I would do some different roles in the story.
[15:50:29] everywhere in the game there's so many of them. And because we knew the first time we go through the game that it would be less than what it was going on,
[15:50:39] have that crunch that kind of extra resources. So you're like, okay, just keep popping pills.
[15:50:49] and you'd be like, that's not what I did, that's not what happened. But the game is, I kind of forced that narrative leap into the gameplay.
[15:50:59] You know, you have that feeling. I remember eating love forms in the beginning. First of all, people like you guys suck.
[15:51:09] Check what we did down like, yes, yes. Yes. Yes. I think again, it's just a black line.
[15:51:19] side of story, kind of narrative aside, the automates sense for your first play to be the help you go through.
[15:51:29] because the energy went to the second third time as you're for more promoted to game where an A's are fully mechanics, you rely less and less on these things.
[15:51:39] So because of resources, Oregon's RLL resource conservation is a correct way to detect enemies so far
[15:51:49] type of, you know, or fractions, or like, on a base thing. It's a tension, it just a big thing. It's a big thing. It's a big push damage. We don't have aiming, so how do we do that?
[15:51:59] So we have two types of black contact and type of your text. So this is a fire-clashing option because they're basically, you can sit like, based on monster. Just like text.
```

### 解読メモ（薬過剰配置・初回プレイ設計・弱点システム）

**意図的なリソース（薬）の過剰配置 — 初回プレイへの配慮:**
- ゲーム全体に回復アイテム（薬）を**大量に配置**した
- 理由：初回プレイでは何が起きるか分からない → 「とにかく薬を飲め」という状況を作る
- 「ひたすら薬を飲み続ける体験」 → これは**意図的**な設計
- ストーリー上の意味：主人公（Hinako）がパニックになって行動する文脈と**ゲームプレイが一致**
  - = 「ゲームプレイがナラティブに強制的に引き込まれる」体験
- 「薬のオーバードース（過剰摂取）のような瞬間」がエンディングのひとつに絡む

**初回 vs 2回目以降のプレイデザイン:**
- 初回プレイ：リソースを惜しみなく使う（薬を飲む・補助システムに頼る）ことを前提に設計
- 2回目・3回目：メカニクスが身につく → リソースへの依存が自然と減る
- → **プレイを重ねるほどゲームの姿が変わる**（初回ホラー → 熟練アクション）という意図的な設計思想

**「君たちはひどい（you guys suck）」発言の文脈:**
- 初見プレイヤーがゲームを「難しすぎる」「理不尽」と感じた際の反応
- 開発チームはそれを**意図的に作り込んだ**と認識 → 方向性として正しいと判断

**弱点システム（Weakness System）— メレーでの"エイム"代替:**
- 銃がないのに「弱点を狙う」という概念をどう実装するか？
- 2種類の「ブラックコンタクト」（接触タイプ）= 実質的な**弱点テキスト（属性テキスト）**
- 「敵のモンスター種別に応じて選択できる」 = Fire Clash（炎系）などの属性攻撃オプション
- → 銃のエイム的な「ターゲット選択・判断」をメレーシステムで代替するアプローチ


---

## ヘビーアタックのリスク・リワード / アクティブ vs パッシブ・リソース管理（15:52〜15:55）

```
[15:52:09] I'll over here. Not really having a good time because you can't can't stagger them the most part of you need to hit back.
[15:52:19] and a lot of these encounters take the more dangerous way to play or the more than one enemy,
[15:52:29] best way to do these. So that's where kind of our heavy attacks come in. So this is the same monster
[15:52:38] right away. And that's again more strategic stuff. You have choice and again,
[15:52:49] do this, but there's a lot of resource kinds of patients here. We need to build that. Because again, this equivalent here is like taking down the zombie with all the body shots
[15:52:59] and lying on taking your time and hitting your headshots. And that's why again, for having text, there's a lot of risk reward that you can have to cut.
[15:53:09] this is right there's a tension and I can hit on my goal and hit it and if you kind of take look at the little person move on the bottom of corner there you can see when you hit the kind of
[15:53:17] but that's pretty much our animals do really under wet. So for the same counter, just went down by just a little bit. If you look at the previous one, I'm starting to finish the same monster.
[15:53:29] and quite a bit more resources here. And I'm talking with that. You're using some health itself. Extra resources are talking with that.
[15:53:39] in this area. And again, we had to have that kind of strategy built in the energy
[15:53:42] And this is kind of a lesson we learned here. This is what we call an active versus passive.
[15:53:54] resource management. And we are really, really happy to be able to make this the way that this pushes. The type of things you have to worry about at different times of the game.
[15:54:04] So passive resource management for us is kind of like what's in my backpack. How do you add it? So I have, you know, what's my goal of, you know, just kind of like a rational level stuff
[15:54:14] at the resource management is like how much health but have it in the moment. More importantly for us as a stamina you have a lot of that to it
[15:54:24] the moment in the comment. Now we have our opinion to the enemy at the pay attention to. All these other things, you might position the concerns, etc. in a kind of like a range
[15:54:34] how much help they have, how much ammo they have, you're like, that's really all you have for your vote at that point of time. All's positioned for us and like, two extra things on top of it
[15:54:44] tough for a lot of people with process. So that's something that we'll talk about a little bit, but we definitely have been estimated for four game players that can manage so many different types.
```

### 解読メモ（ヘビーアタックのリスク・リワード）

**ライトアタック vs ヘビーアタックの比較デモ（15:52〜15:53）:**
- ライトアタックで同じ敵を倒す場合：スタッガーさせにくい → 反撃を食らいながら戦う → **多くのリソースを消費**
- ヘビーアタックを使う場合：戦略的に使えば**より少ないリソース**で同じ敵を倒せる
- 「ゾンビをボディショットで仕留めようとする vs ヘッドショットを狙って確実に倒す」のアナロジー
  - 体に当て続けるのは安全だが遅くリソースを浪費する
  - ヘッドショット（= ヘビーアタックの弱点狙い）はリスクがあるが効率的
- **ヘビーアタックには大きなリスク・リワードが存在する** = テンションの源泉

**画面左下のゲージで確認できる戦果比較（15:53）:**
- 同じ敵を倒すのに、パッシブな戦い方では体力・リソースをより多く消耗
- ヘビーアタック活用では消耗が大幅に少ない → 画面に示して説明

**アクティブ vs パッシブ・リソース管理（15:53〜15:54）:**

> 「これは我々が学んだ大きな教訓。アクティブとパッシブのリソース管理と呼んでいる」

| | パッシブ・リソース管理 | アクティブ・リソース管理 |
|:---|:---|:---|
| タイミング | 戦闘前・戦闘外 | 戦闘中の瞬間瞬間 |
| 内容 | バックパックに何を持つか / アイテム構成 | 今この瞬間の体力・スタミナ残量 |
| 思考レベル | 事前計画・合理的判断 | リアルタイム判断 |

- **スタミナ管理がアクティブ側の核心**：今この瞬間にスタミナがどれだけあるか
- 戦闘中は：敵の位置・自分の位置・敵の体力残量・スタミナ…全てを同時に処理しなければならない
- FPS等では「弾薬残量・ポジション」だけで済むが、SHFはさらに2項目多い
- → **多くのプレイヤーには処理が難しい**と認識している課題

---

## テンポ設計・異なるリズムの混在（15:55〜15:56）

```
[15:55:02] Thanks. Oh, we're going to go to the app. We're going to go to the app. We're going to go to the app.
[15:55:14] And last big thing, you want to talk about your type of type. I'll talk a time, it's about two specific things.
[15:55:24] So I think this is greatest thing for us, or something from across which is for plus 4,
[15:55:34] action unit or games actually can have an exact save part of the particular canics is not there can't have a whole dog
[15:55:44] because that action is a great effort to produce. It's that constantly happening. This is a complicated problem.
[15:55:52] very important is not just having a lower stage in the case of the stage, but having two things fresh differently is happening here, which is going to have massive comment and lots of content.
[15:56:06] and I have the rivers on this kind of thing. So the example I would like to use is, how the water is and how the water is.
[15:56:09] which can be the most effective. All right, they have different rhythms to go. So let's say, R.
[15:56:25] So our weapons and weapons and weapons and weapons are like ours. Hi, I'm going to like this one.
```

### 解読メモ（テンポ設計・異なるリズム）

**「最後の大きなテーマ」= テンポ / リズム（15:55〜）:**
- "last big thing" = テンポ（Tempo / Pacing）についての2点
- "for plus 4" → "Formula（公式）" の誤認識か、またはスライドタイトル

**アクションゲームの根本的な問題:**
- 純粋なアクションゲームでは「同じメカニクスが常に繰り返される」
- = 「ずっと同じことをやり続ける（constantly happening）」という複雑な問題
- ただステージを並べるだけでは解決しない

**解決策 = 「異なるリズムを混在させる」:**
- 重要なのは「ステージを低くする（低難度ステージを置く）」だけではない
- **2つの異なるリズムが同時に存在すること**が重要
- 比喩：川の流れ = 速い流れと遅い流れが混在している
- 武器のリズム：各武器には**それぞれ異なるリズム（テンポ）**がある


---

## 武器ごとの異なるリズム・レベルデザインへのテンポ適用（15:56〜15:58）

```
[15:56:37] and analogy there, but then for our actual, our switch hammer, our shotgun. This is more likely to look at. Okay.
[15:56:45] It goes very different there. And then four are kind of a light. It goes well as a G. And then you're kind of fast to count on.
[15:57:02] Yes, so a lot of comments. So again, we're using rhythm to kind of control the comment and the whole thing about this is that everybody kind of yields attached to different type of rhythm different type of music.
[15:57:11] attached to different type of music. If you play a final game, some people just start to generate some play for African characters. Only they like to win the most type of characters. You know.
[15:57:16] So, but again for weapons it's not that you have a correct weapon.
[15:57:28] time of different people like different rhythms you have to give them that choice but it also has to feel different because if every weapon kind of has the same rhythm you don't get you don't
[15:57:29] and facing the difference in variety.
[15:57:36] Now again, goes towards our levels too. So more designer levels, pace, we have to have ridden for the fuck-tap.
[15:57:48] I'm gonna go through it and watch the dark shrine, you can have a go through it. And you'll play the game in those days. The dark shrine with a much more common heavy. All the weapons there have no durability
[15:57:58] a resource that you have to worry about at the point times that we can focus on something else. And that's kind of like, again, switching up the rhythm and switching up the tempo,
```

### 解読メモ（武器ごとのリズム・レベルへのテンポ適用）

**武器ごとに異なるリズム（テンポ）:**
- スイッチハンマー（Switch Hammer）= 重く遅いリズム（「ショットガン」と同列で説明）
- ライト系武器 = 軽く速いリズム
- 各武器がそれぞれ**明確に異なるリズム感**を持つよう設計

**「正しい武器」は存在しない:**
> 「武器に正解はない。人それぞれ好きなリズム・好きな音楽がある」
- 格闘ゲームでの例：スピードキャラ派 vs パワーキャラ派
- → **どのリズムも正解**、プレイヤーに選択肢を与えることが重要
- ただし全武器が同じリズムになると**多様性・バリエーションが失われる** → 意図的に差別化

**リズムをレベルデザインにも適用（15:57〜15:58）:**
- レベルデザイナーもリズム＝テンポを意識してレベルを設計する
- **「ダークシュライン（Dark Shrine）」エリアの例:**
  - このエリアの武器は**全て耐久値なし（no durability）**
  - = 武器破損を気にしなくて良い → **リソース管理の心配事をひとつ取り除く**
  - → プレイヤーは別のこと（戦略・探索・緊張感）に集中できる
  - = **リズムとテンポを切り替えるエリア設計**の具体例
- 「武器耐久値という心配事をこのエリアでは除外することで、別の種類の緊張感に集中させる」


---

## スタミナがテンポ制御の核心・攻守共有リソース設計（15:58〜16:00）

```
[15:58:08] later in the game, kind of runes together, the kind of create a new experience here. Spoilers.
[15:58:18] and we use tempo as a kind of a key pacing vehicle to be there and that's stamina. So what's stamina is this little bar down here,
[15:58:22] this we regulate all actions. What we clear. Let me say that again, the difference in action games.
[15:58:34] and hard days for us is the amount of actions you can do with the rate average you can do things. This is what we use to control the day. All right, so both offensive and defensive
[15:58:44] resource that kind of regenerates over time and again for us it's gated by how much you have
[15:58:48] example of if it's at the bar down there as you can not go to the tagging, it's resources going down.
[15:59:01] But it's being over aggressive so when this monster attacks, she cannot dodge, it's hit quite a lot of damage at this point.
[15:59:11] It's actually very difficult for us. This is a bit learning. Again, we talked about how too much active resource management is all employers connect process that all
[15:59:21] or share the same resource. So you can think about using bullets to attack and defend in that case, kind of like a analogy.
[15:59:31] but something will definitely kind of take a look at it in the future too. But even though it seems like a very clean mechanic, just because it's clean just because it feels like it works.
[15:59:40] for your particular audience as if it's something that you have to keep in mind.
[15:59:51] shooting start to aiming in a in-horse, as that little bar, as you can see, it's kind of going up.
[16:00:01] just like cross hairs the point of this is to slow down pacing in the game. So I mean that just kind of like it's hitting the bottom of the tank, not stop.
```

### 解読メモ（スタミナがテンポ制御の核心）

**スタミナ = テンポ制御の主要手段（"key pacing vehicle"）:**
- スタミナは画面下部の小さなバーで表示
- **全ての行動（攻撃・回避）をスタミナで統制** = ゲームのテンポそのものを調整する仕組み
- > 「アクションゲームとホラーゲームの違いは、行動の量と行動できるレート（速度）だ」
- スタミナが空になると攻撃も回避もできなくなる

**スタミナ切れによるピンチ映像デモ（15:59:01）:**
- 過度に攻撃的に行動 → スタミナ切れ
- 敵が攻撃してきても**回避ができない** → 大ダメージ
- = 「スタミナを使いすぎるとリスクが跳ね上がる」体験を実演

**攻撃と回避が同じリソース（スタミナ）を共有する設計:**
- 攻撃に使うと回避の余力がなくなる（弾薬を攻守に使うアナロジー）
- = **行動の選択にトレードオフが生まれる** → テンションの源泉
- 「非常にクリーンなメカニクスに見えるが、自分たちのオーディエンスに合うかは常に考える必要がある」
- アクティブ・リソース管理の多さがプレイヤーには難しいという反省もあり、今後も見直す

**スタミナ回復のアニメーション（エイム中）:**
- 武器をエイム（構え）状態にすると、スタミナバーが回復していく
- = 「クロスヘア（照準）を持つことと同じ機能」 → 一時停止・ペースを落とすための行動
- ゲームのペースを**意図的に落とす**メカニクスとして機能
- 「タンクの底を叩くようなもの（ガス欠になる前に補給する感覚）」


---

## フォーカステック・カウンター攻撃・テンションの本質（16:00〜16:02）

```
[16:00:11] in a point-time. And this drives two main features here, which is a character and a focus
[16:00:21] and possibly the master, you will suspect the tag, and you'll see this hash of all this, having hit. The riskiest button, which is heavy-attack button,
[16:00:24] you can do counter that point and hit the due to reduce the amount of resources in heaven.
[16:00:36] the same thing which the top bar of them and also does take a durability to use was kind of like a free hit at this point in time.
[16:00:46] without again, paced in the game to make players really watching enemy at this point time. And in a sense, because not every single attack can be
[16:00:56] You have to, it's not just like, oh, I see the enemy move and I hit the button, or you're going to get hit back, you know, and you take the first damage, but that actually shows its still wearing tents.
[16:01:06] I don't know if this is kind of like an in-game jump, like a car back jumpscare, we have to have to be careful, but it's not just like, okay, we'll many goals hit this button,
[16:01:15] watch do I dodge? Do I actually get the attack button? You have to be very careful about this.
[16:01:26] So we have folks tech, which is a, okay, I don't want to think about those anymore.
[16:01:27] the more. So.
[16:01:38] One important thing for us is, we talk about tension all this time. But at least intention is just support. Again, the weight is greater than the moment,
[16:01:49] or just for a general, you want, once the moment happens, the tension's not there anymore. So we're going back to this slide, for instance,
[16:01:59] It's tense when you're kind of waiting, but as you attack or the monster attacks, it's like
```

### 解読メモ（フォーカステック・カウンター・テンションの本質）

**スタミナがもたらす2つの主要機能（16:00:11）:**
- キャラクター（Character） = 操作感・個性
- フォーカス（Focus） = 集中・注意力の管理

**カウンター攻撃（Counter）= 高リスク・高リワードの極致（16:00:21〜）:**
- 敵の攻撃モーションを読んでヘビーアタックボタンを押す = カウンター
- 「最もリスクの高いボタン（riskiest button）= ヘビーアタック」を敵の攻撃に合わせて使う
- 成功するとリソースを大幅に節約できる「フリーヒット」に近い効果
- スタミナバーの上部（耐久値）も消費するが、**見返りが非常に大きい**

**「見ていなければならない」設計:**
- 全ての攻撃がカウンター可能ではない → **プレイヤーは常に敵を注視しなければならない**
- 「敵が動いたからボタンを押す」という単純反応では返り討ちにあう
- 「回避するか？攻撃ボタンを押すか？」の判断が必要 = 高い注意力を要求
- これ自体がテンションを生む = 「インゲームのジャンプスケア的な要素」

**「フォーカステック（Focus Tech）」= 思考を手放せる瞬間（16:01:26）:**
- "folks tech" → "Focus Tech" の誤認識
- 「もうこれ以上考えたくない」状態をあえて作るシステム
- = 高度な集中状態が続いた後に「判断を委ねられる瞬間」を設ける

**【核心的結論】テンションの本質 = 待機の中にある（16:01:38〜）:**
> 「テンションについてずっと話してきたが、テンションはあくまでサポートに過ぎない。**待つこと（weight/wait）が瞬間よりも重要**だ」
- テンションは**出来事が起きる前の待機状態**に存在する
- 攻撃が実際に発生した瞬間 → テンションは消える（解放される）
- 「待っているときが緊張感のピーク。攻撃するか、モンスターが攻撃してくると、それが解放される」
- = **テンションとは「これから何かが起きる」という予期の感覚**


---

## マジックキー（システムブレーカー）・戦闘はパズル・ビーストアーム（16:02〜16:04）

```
[16:02:09] So how do you kind of really attention? So pacing, pacing the game is a big thing. If I'm not really sources, if you can't get against more tense,
[16:02:19] And we have something called on a master key, which is kind of a system breaker for us. So what does this, so in like a...
[16:02:29] or it just kind of games this could be like a grenade or a rocket launch. It's basically something where you're like, I don't want to think anymore.
[16:02:39] for us combat is like a type of puzzle. It's the same energy every cop ever you encounter is a puzzle
[16:02:49] instead of kind of life trying to figure out what kind of obtuse clues it's more of the dance
[16:02:59] Because there's no way players want to have a hard time dealing with, so it's key for us to give them this kind of system break, it's magic key.
[16:03:09] this magic key, to let them have decide when I want to start using. So again, in these other games, if we're nays, you're rocking on a channel, you're like, okay, you all want to say,
[16:03:19] of the free source. We have that tension there. But you know when you get to point where the tension starts turning the frustration you like screwed up
[16:03:29] you pass there. And that's a big thing for it. And in recent days, the stuff that came
[16:03:34] you know it's not just kind of slow in image because it's not bad.
[16:03:45] all its forms in the very kind of river, very very very military for everything. If basically just rock paper scissors, you can have better health, or worse health, or kind of equal health from this part.
[16:03:55] You need to get your knee this rocker, which is good. I don't think I can ever go this thing. But you have to change what happens. That's key to releasing this tension.
[16:04:05] you don't know what's tense and what's frustrating for particular types of players. So for that frame, you know what we have as large type of beast arm, as beast arm here,
```

### 解読メモ（マジックキー・戦闘はパズル・ビーストアーム）

**テンションの維持方法 = ペーシング（16:02:09）:**
- リソース不足だけではテンションを上げ続けることはできない
- → **ペーシング（緩急）**がテンションを持続させる鍵

**「マジックキー（Magic Key）」= システムブレーカー（16:02:19〜）:**
- "master key" → "Magic Key"（SHFのゲーム内アイテム名）の誤認識
- 他のゲームでの「グレネード」や「ロケットランチャー」に相当するもの
- = 「もう考えたくない！」というときに使える**システムを破壊する切り札**
- プレイヤーが**自分で使うタイミングを決められる**ことが重要
- テンションがフラストレーションに転化する直前に使える逃げ道を提供

**「戦闘 = パズル」という哲学（16:02:39〜）:**
> 「戦闘はパズルの一種。遭遇する敵との戦いはひとつひとつがパズルだ。難解な謎解きではなく、**ダンス（dance）に近い感覚**」
- 「どんな手がかりがあるか」という従来のパズルではなく
- 「敵の動きに合わせて自分の行動を選ぶ」というダンスのような相互作用
- → メレー戦闘の本質を「ダンス」と定義

**テンション → フラストレーション の転化ポイント（16:03:19）:**
- テンションが蓄積し続けると必ずフラストレーションに変わる瞬間が来る
- そのポイントを超えてしまったら「システムブレーカー（マジックキー）」で解放する

**ロック・ペーパー・シザーズ的な状況変化（16:03:45〜）:**
- 戦闘はじゃんけん的に「有利・不利・互角」の状況が生まれる
- 「現状を変えること（change what happens）」がテンションを解放する鍵
- = 同じ状況に固定されないこと

**ビーストアーム（Beast Arm）= 大型システムブレーカー（16:04:05）:**
- プレイヤータイプごとに何がテンションで何がフラストレーションかは異なる
- その解決策として「ビーストアーム（Beast Arm）」という大型の特殊アームシステムを導入
- = ゲームの通常ルールを逸脱した強力な切り札システム


---

## ビーストアームの補足・フォーカスアタック・まとめ・Q&A（16:04〜16:05）

```
[16:04:10] make sense with the story. If you have a thing to gain, you can be going like what the hell another thing isn't this important in.
[16:04:22] But it makes sense. It makes sense with the game trust me. All right. This is, it says there's a lot of way in this small and
[16:04:32] reasons, just be honest, because of us. So, because our time in a book is kind of less aggressive, I want this gameplay.
[16:04:42] They speak. Tongue down. Use the focus attack for that. So it's a much weaker version of this kind of match key. It still does the job of match key for that
[16:04:52] But again, it's important that you give these things kind of resources. It's not like every single thing I think of is a grenade or rocket-automatic,
[16:05:02] is sparingly kind of doing. All right. So kind of wrapping up. Again, if you think you talked about for us,
[16:05:12] game, especially in combat. You have to watch it for first-fishing. Line in there.
[16:05:22] how you like the pacing of the comic under your game itself. Again, thank you for the team over here at New Wars and climbing,
[16:05:32] want to take this time, but it's trying to go over to Q&A. If you guys are interested, these are kind of our team, kind of like a approach you can kind of check out all the teams here.
[16:05:42] by and you see please fill out your speaker and graduation for help. This speaker is all that helps Katie see you in that one lot and it's determined
[16:05:52] Thank you.
[16:05:55] Thank you.
[16:10:21] Thank you.
```

### 解読メモ（ビーストアーム補足・フォーカスアタック・まとめ）

**ビーストアームのストーリー上の整合性（16:04:10〜）:**
- 「なぜこんな腕が突然使えるの？」という疑問に対して
- > 「ストーリーと整合している。ゲームを信頼してほしい（trust me）」
- = ナラティブとゲームプレイが一致するよう設計されている（ゲームプレイのナラティブ強制）

**フォーカスアタック = マジックキーの軽量版（16:04:32〜）:**
- "Tongue down（タングダウン）" → フォーカス攻撃の入力コマンド説明か
- フォーカスアタック = マジックキーよりずっと弱いが**同じ役割（システムブレーカー）**を果たす
- より侵略性の低いプレイスタイルにも対応

**システムブレーカーの節約原則（16:04:52）:**
> 「全てをグレネードやロケット砲にしてはいけない。節約して使うことが大切だ」
- 強力なシステムブレーカーが頻繁に使えると逆にテンションが生まれなくなる
- 希少性を保つことで**切り札としての価値を維持**

**セッションまとめ（16:05:02〜）:**
- 「我々が話したことを振り返ると…」
- ゲーム全体、特に**戦闘においてペーシング（緩急）を監視し続けることが重要**
- "first-fishing" → **ファーストプレイ（初回体験）** = 初見プレイヤーの体験を最優先に設計すること
- 「自分のゲームのコミック（戦闘）のペーシングがどうあるべきかを考え抜くこと」

**謝辞・チーム紹介（16:05:22）:**
- "New Wars" → **Neobards Entertainment**（台湾スタジオ）
- Q&Aへ移行
- スピーカー評価フォームの記入を依頼

**セッション終了: [16:05:52〜16:05:55] "Thank you."**

---

## セッション全体まとめ

Silent Hill F のメレーオンリーホラーゲームというコンセプトの設計哲学を5つの柱で解説：

1. **ピラー設計** — フランチャイズDNA（サバイバルホラー）を守りながら、Neobards の強みを活かしたメレー特化
2. **テンション設計** — "Scary or Tense?" → ホラーゲーム = テンションのビルドアップと解放
   - 遠距離武器カット → 距離の逆転（遠いほど危険）
   - 情報の隠蔽（3D空間の曖昧さ）
   - AIクロスドロー（当たるか当たられるか）
3. **フラストレーション vs テンション** — キャラクターの恐怖 ≠ プレイヤーの恐怖 / 慣れがテンションを殺す / プレイヤー選択（難易度・アシスト）で対処
4. **リソース設計** — FULL と EMPTY の関係 / アクティブ vs パッシブ管理 / スタミナが攻守を共有
5. **テンポ設計** — 武器ごとの異なるリズム / スタミナがペーシングの主要手段 / マジックキー（システムブレーカー）で解放

> **核心的結論**: 「テンションは待機の中にある。出来事が起きた瞬間、テンションは消える。戦闘はダンスだ。」


---

## スライド38〜47 確認済み内容

### スライド38（Tension | Resource）
- **Active vs Passive Resource Management**
- アイテム画面：Red Capsules / Bandage / Ramen / Chocolate / Higashi
- Red Capsules: "Painkillers in capsules. Relieves pain and raises spirits. Slightly restores Health and current Max Sanity"

### スライド39（Tension | Time）
- **Key Components**:
  - Tempo (Pacing)
  - Rhythm
- Hinakoが教室で楽譜を眺めているシーン

### スライド40（Tension | Rhythm）
- 2枚のゲームプレイ比較（青い霧の街 vs 赤い神社内部）
- リズムの異なる2つのシーンを並置

### スライド41（Tension | Time）
- **Tempo = Key pacing regulator**
- **Stamina**（スタミナバーUI）

### スライド42（Tension | Time）
- **Tempo = Key pacing regulator**
- **Stamina:**
  - Regulates pacing through action rate
  - Drives all combat actions (active)
  - Gated by time and amount

### スライド43（Tension | Time）
- **Focus**
- **Analogue to aiming in horror shooters**
  - Slows down pacing
  - Player can also attack at will
- フォーカス時のUI（月型ゲージ + スタミナバー回復中）

### スライド44（Tension | Releasing Tension）
- **Wait > Moment**（テキストのみ）

### スライド45（Tension | Releasing Tension）
- **Wait > Moment**
  - Pacing
  - Resource Amount
  - **Master Key** (System Breaker) ←赤字強調

### スライド46（Wrap Up）
- **Key Drivers of Tension:**
  - Frustration
  - Information / Resources
  - Time / Rhythm
- 菊頭モンスターとHinakoの接近戦スクリーンショット

### スライド47（Q&A）
- QRコード2枚：NeoBards / Full Team

