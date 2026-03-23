# GDC 2026: UX Mobile Porting Challenges in Prince of Persia: The Lost Crown — AAAタイトルをモバイルで妥協なく遊ばせるためのUX戦略

コンソール向けに開発されたアクションゲームをモバイルに移植する。15個のボタンと4〜6本の指で操作するゲームを、平らなガラス面と2本の親指だけで再現しなければならない。しかも「劣化版」ではなく、コンソール版と同等の深い体験を提供する必要がある。

GDC 2026 の UX Summit で Ubisoft Da Nang の Alexis Denance 氏が語ったのは、まさにその挑戦の記録だった。チーム規模わずか10名、開発期間約1年。450件以上のカメラ問題への対処、パリィ（Parry＝敵の攻撃タイミングに合わせて弾き返す防御アクション）操作の根本的な再設計、そして「20%のプレイヤーがゲームプレイに到達しなかった」というオンボーディングの致命的問題の発見と解決。

本記事では、現地で録音・撮影した素材をもとに、スピーカーの発言を引用しながらセッションの全容を再構成する。モバイル移植に関わるすべての開発者にとって、実践的な知見の宝庫となるはずだ。

> **Prince of Persia シリーズについて**: 初代『Prince of Persia』は1989年に Jordan Mechner が開発した2Dアクションゲームで、滑らかなロトスコープアニメーションで当時大きな話題を呼んだ。その後『The Sands of Time（時間の砂）』（2003年）で3Dアクションアドベンチャーとして復活し、時間操作メカニクスがシリーズの代名詞となった。2024年にリリースされた『The Lost Crown』は、2Dメトロイドヴァニア（探索型2Dアクション。広大なマップを探索し、新しい能力を獲得して進行する）として原点回帰を図り、メディアから高い評価を得た作品である。

---

## セッション概要

| 項目 | 内容 |
|:---|:---|
| **セッション名** | UX Mobile Porting Challenges in 'Prince of Persia: The Lost Crown' |
| **スピーカー** | Alexis Denance（Ubisoft Da Nang / Game Content Director） |
| **日時** | 2026年3月9日（月）10:30 - 11:30 |
| **会場** | Moscone Center West Hall, Room 3002 |
| **トラック** | Design / UX Summit |
| **チーム規模** | 約10名 |
| **開発期間** | 約1年 |

![セッション会場と登壇者 Alexis Denance 氏。ベトナム・ダナンの Ubisoft スタジオから GDC に登壇した](source/images/IMG_4450.jpg)

---

### この記事で読めること

- **スピーカーとスタジオの背景**
- **移植の出発点**
- **チームのミッションステートメント**
- **カメラ**
- **コントロール**
- **HUDのイテレーション**
- **代替操作**
- ...ほか全20セクション

> **本記事のボリューム**: 約28,490文字 / スライド画像13枚
> スピーカーのトランスクリプト（発言の文字起こし）を原文・日本語訳つきで完全収録しています。

---

<!-- ===== ここから有料エリア（Note エディタで有料ラインを設定） ===== -->

## 1. スピーカーとスタジオの背景

Alexis Denance 氏は、ゲーム業界で約15年のキャリアを持つ。プロダクションとデザインの両方の経験を経て、現在は Ubisoft Da Nang の Game Content Director を務める。

> "I've been working in the industry for almost 15 years, both in production and now as game content director. I joined Ubisoft Da Nang in central Vietnam, in the studio, run six years ago."
>
> （業界で約15年、プロダクションとゲームコンテンツディレクターの両方を経験してきました。6年前にベトナム中部のダナンにある Ubisoft スタジオに参加しました）

Denance 氏は自身を「GDCでスピーカーとして登壇する最初のベトナムスタジオの代表」と紹介し、ベトナムのゲーム開発シーンが成長していることにも触れた。

![スピーカー紹介スライド。15年のキャリア、モバイルとPC/コンソール両方の経験、自身も Prince of Persia ファンであることが記載されている](source/images/IMG_4451.jpg)

Da Nang スタジオの過去プロジェクトには、Ubisoft Nano、Valiant Hearts: Coming Home（PC/コンソール移植）、Rabbids: Legends of the Multiverse、Skull and Bones（Ubisoft Singapore との共同開発）などがある。モバイルとPC/コンソールの両方での開発経験が、今回の移植における大きなアドバンテージとなった。

---

## 2. 移植の出発点 — 「素晴らしいゲーム」がもたらすプレッシャー

### HD版の完成度

![HD版の出発点。The Game Awards の Innovation in Accessibility 受賞、Metacritic 86点、Nintendo Switch で60FPS動作](source/images/IMG_4453.jpg)

『The Lost Crown』のHD版は、2024年1月にリリースされ、アクセシビリティ（障がいの有無や環境を問わず誰もがゲームを楽しめるようにする取り組み）へのイノベーション賞を受賞し、Metacritic 86点を獲得していた。

> "We have to consider the finished product immediately first. And here we are very lucky with huge critiques and player appreciation, winning awards like innovation in accessibility. And for us it was exciting to see them win, but also with a lot of pressure on our adaptation."
>
> （移植スタジオとしては、まず完成品の状態を見なければなりません。ここでは批評家とプレイヤーの両方から高い評価を受け、アクセシビリティのイノベーション賞も受賞していました。彼らの受賞は嬉しかったのですが、同時に私たちの移植に対するプレッシャーも大きかった）

技術面では、Nintendo Switch（RAM わずか4GB）上で完全3Dのゲームが60FPSで動作するほど最適化されていた。

> "The game was also super optimized since it's fully 3D and runs at 60 FPS on Switch One which has only 4 GB RAM. So it's honestly very impressive what the ESG team accomplished on this."
>
> （完全3Dでありながら、4GBしかRAMがないSwitchで60FPSで動作するほど最適化されていました。HD開発チームの成果には正直感嘆しました）

### HD版のアクセシビリティ機能が移植を助けた

![HD版のアクセシビリティ機能。難易度パラメータの細かなカスタマイズ、大きなフォントサイズ、Memory Shard機能](source/images/IMG_4452.jpg)

HD版は、難易度パラメータの詳細なカスタマイズ、視覚障がい者向けのハイコントラストモード、メトロイドヴァニアの認知負荷（Cognitive Load＝プレイヤーの脳が同時に処理しなければならない情報量）を軽減する Memory Shard 機能などを備えていた。Denance 氏は Memory Shard 機能について個人的な思いを語った。

> "Personally, I love this feature, but without this feature I don't think I would have played the game. I'm generally too lost on metroidvanias because I don't have the time to do it in a straight run and forget where I am."
>
> （個人的にこの機能が大好きです。これがなかったらこのゲームをプレイしなかったと思います。メトロイドヴァニアではいつも迷子になるんです。一気にプレイする時間がなくて、前回どこにいたか忘れてしまうので）

これらのアクセシビリティ機能への取り組みは、データが整理されパラメータが外部公開されていたことを意味し、モバイル版での調整が格段にやりやすかった。

---

## 3. チームのミッションステートメント

HD開発チーム（Ubisoft Montpellier）は、当初モバイル移植に懐疑的だった。Da Nang チームは説得のためにミッションステートメントを掲げた。

![ミッションステートメント: "Bring Prince of Persia: The Lost Crown to Mobile with no Quality downgrade on 3Cs, Visuals, and Performances, Best in Class Native Mobile Controls, Controller Support"](source/images/IMG_4455.jpg)

> "When we proposed to do the mobile porting, our team defined its own mission statement to convince the ESG team, and it was really needed because initially they were not really convinced that their game would be ported on mobile."
>
> （モバイル移植を提案したとき、私たちは独自のミッションステートメントを定義してHD開発チームを説得する必要がありました。彼らは最初、自分たちのゲームがモバイルに移植されることに確信を持っていなかったので）

| 目標 | 詳細 |
|:---|:---|
| **品質の維持** | 3Cs（Camera, Controls, Character）、ビジュアル、パフォーマンスで品質低下なし。60FPS維持 |
| **タッチ操作** | ベストインクラスのネイティブモバイルコントロール |
| **コントローラーサポート** | PC/コンソールと完全に同一の操作体験 |
| **モバイル専用機能** | プラットフォームのニーズに合わせた専用の適応 |

Switch の画面サイズを基準にUIが設計されていたため、要素のサイズはモバイルでもほぼそのまま使えた。**UXの適応が最大の課題**だった。

---

## 4. カメラ — 450件以上の問題との戦い

### 画面比率の壁

![カメラの出発点。HD版は16:9のみサポート、モバイルは4:3〜22:9。カメラはロケーション単位の手動設定で、巨大マップ＝大量の問題](source/images/IMG_4456.jpg)

HD版は16:9のみサポートしていたが、モバイルデバイスは4:3（タブレット）から22:9（折りたたみスマホ）まで幅広い。さらにメトロイドヴァニアのカメラは「ロケーション固有」で設定されており、システマティック（全マップに一律適用できる仕組み）ではなかった。

> "The way the camera was built on HD used that screen description, so it was really location specific as opposed to systemic. This meant that whatever we changed on mobile we had to change the full map manually."
>
> （HD版のカメラは画面サイズに依存する形で構築されており、システマティックではなくロケーション固有でした。つまり、モバイルで何かを変えるたびにマップ全体を手動で変更しなければならなかった）

加えて、モバイルでは画面上にHUD（Head-Up Display＝画面上に常時表示される情報UI）のボタンが多数表示され、その上にプレイヤーの指が乗る。

> "Let's not forget your mobile, which means two things. You have a lot of buttons on screen and usually on those buttons you have fingers. So it can really become really tricky to see an enemy prepare an attack by hiding half of the screen with your fingers."
>
> （モバイルだということを忘れないでください。つまり2つのことがあります。画面上にたくさんのボタンがあり、そのボタンの上には指がある。指で画面の半分を隠してしまうと、敵が攻撃を準備しているのが本当に見えなくなります）

### 450件の問題の分類

数日間のQCテストで **450件以上の問題** が発見された。20:9以上の極端な画面比率は市場シェアが3〜4%しかなかったが、それ以下の比率は確実にカバーする必要があった。

> "The result in a few days of testing was over 450 issues referenced splitting four main categories requiring different solutions."
>
> （数日間のテストの結果、異なるソリューションを必要とする4つの主要カテゴリに分類される450件以上の問題が見つかりました）

| カテゴリ | 症状 | 対応 |
|:---|:---|:---|
| **画面端の表示崩れ** | ブロッカー（進行を制御する見えない壁）がはみ出す | ブロッカー位置を画面端に合わせて移動 |
| **キャラクターのフレームアウト** | 画角変更でキャラが画面外に | カメラ位置の微調整 |
| **HUDとの干渉** | レベル端でキャラがHUDに隠れる | 透過処理。ただし遷移地点が多く、戦闘は少ないため影響は限定的 |
| **視野の問題（タブレット）** | 上下に見えてはいけないエリアが見える。FOV依存で修正が連鎖する | レターボックス（黒帯）で対応 |

### 市場データに基づく意思決定

折りたたみスマホが市場の **11%** を占めていることが判明。最終的に20:9までのフルスクリーン対応（約300件の修正）を実施し、タブレットは4:3でレターボックスとした。

> "If you have a game that necessitates those screen extensions and you're not doing that effort immediately, as I launch the game as a player it feels very cheap. So it doesn't push me to convert."
>
> （もしあなたのゲームが画面拡張を必要とするのに、その対応をしていなかったら、プレイヤーとして起動した瞬間に安っぽく感じます。それでは購入への動機づけにならない）

画面拡張はメニューの対応も必要だったが、HD版のアクセシビリティへの注力のおかげでUIが柔軟に作られており、ボタンサイズも大きかったため、メニュー側の修正は比較的軽微だった。

---

## 5. コントロール — 15ボタンを2本の指で操作する

### 課題の本質

![コントロールの課題。15ボタン＋コンボ、4〜6本の指 → 平面ガラス＋2本の指](source/images/IMG_4458.jpg)

HD版は15個のボタン（コンボを含めるとさらに多い）を4〜6本の指で操作する設計だった。これを物理的フィードバックのない平面で2本の指に落とし込む必要がある。

> "So now imagine you have to do a jump, edge attack, to melee while being in mid-air. That's a relatively basic combo in the Lost Crown but it's already involving considerable different inputs. So it's a tough fit for sure, but there are plenty of tricks that we use to make it more instinctive for players."
>
> （ジャンプ、エッジアタック、空中近接攻撃。これは The Lost Crown では比較的基本的なコンボですが、すでにかなり異なる入力が必要です。間違いなく難しいフィットですが、プレイヤーにとってより直感的にするためのトリックはたくさんあります）

### コントローラーサポート — 5%が5倍のコンバージョン

コントローラーサポートの実装はほぼコスト不要だった。HD版のコントロール設計がそのまま使えるからだ。しかしそのデータは驚くべきものだった。

> "Although we have under 5% of our player base using a controller, we saw a massive difference on the conversion from the free trial to players using a controller."
>
> （コントローラーを使うプレイヤーは全体の5%未満ですが、フリートライアルからの購入コンバージョン率に劇的な差がありました）

5%未満のプレイヤーが使うだけだが、コンバージョン率は **5倍** 。Denance 氏はマーケティングやゲームページでコントローラー対応をアピールすることの重要性を強調した。

### プラットフォーミングと戦闘の異なる課題

ゲームには2つの主要なゲームプレイがある。プラットフォーミング（足場から足場へ正確にジャンプしていくアクション）と戦闘だ。

> "Platforming has a music to it, generally speaking. A weak input that you need to perform, which is harder with no physical button, a high base and a huge development for precision."
>
> （プラットフォーミングにはリズムがあります。物理ボタンなしでは困難な弱い入力が必要で、基本速度が高く、精度への要求が非常に大きい）

> "It's generally more forgiving than platforming because you will not necessarily die as you fail a combo, but still you want the players to be able to pull off those impressive combos even with touch control."
>
> （戦闘はプラットフォーミングよりも寛容です。コンボを失敗しても必ずしも死ぬわけではありませんが、タッチ操作でもあの印象的なコンボを決められるようにしたかった）

---

## 6. HUDのイテレーション — ピッチから最終形まで

### 終わりなき調整

> "It was a constant iteration and adjustment for both tests within the team and observation made during the test. Some major changes came from different types and more automatic controls, but a lot of the more invisible changes came from size and position tweaks as well as adding clear feedback on buttons."
>
> （チーム内のテストとテスト中の観察の両方による、絶え間ないイテレーションと調整でした。大きな変更は別の操作方式やオートメーション化から生まれましたが、目に見えにくい変更の多くはサイズと位置の微調整、そしてボタンへの明確なフィードバックの追加から来ました）

### 操作ロジックの変更は最小限に

> "As much as we can, first we wanted to avoid modifying the logic of controls themselves. So the question is, is there any action we cannot perform on mobile? And lucky for us there was only one of these cases."
>
> （できる限り、まず操作ロジック自体の変更を避けたかった。問題は「モバイルで実行できないアクションがあるか」です。幸運なことに、該当するケースは1つだけでした）

![操作ロジックの唯一の変更：スプリント。HD版はSlide(R2)+Movement+Hold Slideだが、モバイルではHold Slideで右指がブロックされスライド攻撃が不可能に。解決策はMovementの長押しでスプリント発動に変更](source/images/IMG_4462.jpg)

その1つは「スプリント（ダッシュ走り）」だった。HD版ではトリガーを引きながら移動し、トリガーを押し続けて走り続ける。モバイルではこの操作で右指が完全にブロックされ、スライド中のジャンプキック攻撃ができなくなる。

> "On mobile though, if you do this you are blocking the right finger from any other input. This means you cannot perform this jump kick attack, which is not really an option for us because we don't want to make any compromises on gameplay."
>
> （モバイルでこれをやると、右指が他の入力から完全にブロックされます。つまりジャンプキック攻撃ができなくなる。ゲームプレイで妥協したくないので、これは選択肢にありません）

解決策は、左スティックの長押しでスプリントを発動する方式への変更だった。

### ボタンの触覚フィードバック

> "For us it was really about giving the player a visual feedback. So at any button press, we light up and give a size bump. One big mistake here that we initially made is that we made the bump to go smaller. When you have your finger on top of the button, you actually can't see anything."
>
> （プレイヤーに視覚的なフィードバックを提供することが重要でした。ボタンを押すたびにライトアップしてサイズを膨らませます。最初にやってしまった大きなミスは、ボタンを小さくする方向にバンプさせたことです。指がボタンの上にあると、実際には何も見えません）

ボタンは押したとき「大きく」なる必要がある。小さくなると指の下に隠れて何も見えなくなるからだ。

バイブレーション設計も重要だが、HD版のバイブレーション設計がそのままモバイルに適していた。

> "Vibration design is also key. For us the HD version was already very suited for the feedback, so we did not have to make a lot of changes. Still it's very useful for players, probably more so than the visual feedback."
>
> （バイブレーション設計もカギです。HD版がすでにフィードバックに適していたので、多くの変更は必要ありませんでした。それでもプレイヤーにとって非常に有用で、おそらく視覚的フィードバック以上に重要です）

さらに、ボタン内にスキルのクールダウン状態や矢の残数を表示するなど、情報をコントロールUI自体に組み込む工夫もなされた。

---

## 7. 代替操作 — モバイルネイティブな方法

コンソールのボタンをそのまま並べるのではなく、モバイルならではの操作方法も提供した。画面右側に最大8個以上のボタンが並ぶ終盤を見据え、画面の整理（declutter）が重要だった。

> "We also wanted to offer more mobile native ways and help to declutter the screen overall, as we had over 8 buttons under the right side in the end game."
>
> （よりモバイルネイティブな方法を提供し、画面全体を整理したかった。終盤では画面右側に8個以上のボタンがありましたから）

| アクション | ボタン操作 | モバイル代替 | 備考 |
|:---|:---|:---|:---|
| **ダッシュ** | 専用ボタン | スワイプ | ボタンを1つ削減 |
| **クレアボヤンス**（透視能力） | ボタンのトグル（ON/OFF） | 画面上どこでもダブルタップ | 画面全体にオーバーレイが出るためボタン不要 |
| **特殊攻撃（アミュレット）** | 2ボタン同時押し | スワイプまたは専用ボタン | HD版の2ボタン同時押しを1入力に簡略化 |

> "Ultimately for default setup, we kept the buttons for the main action, simply because there are more usual controls for players. We didn't want to take much changes there."
>
> （最終的にデフォルト設定では、メインアクションにはボタンを残しました。プレイヤーにとってより一般的な操作方法だからです。そこであまり冒険はしたくなかった）

> "Bottom line, with all of these options combined, you can remove up to 5 buttons from the screen without compromising much on gameplay."
>
> （結論として、これらのオプションをすべて組み合わせると、ゲームプレイをほとんど犠牲にせずに画面からボタンを最大5個削除できます）

---

## 8. パリィ — 最も困難だった操作設計

パリィ（敵の攻撃を弾き返す防御操作）は、移植全体で最も困難な課題だった。戦闘中はプレイヤーの両手が移動と攻撃で忙しく、パリィのために指を切り替える余裕がない。

> "Parry was the toughest control we needed to adapt. So typically in combat, you'll be moving and attacking at the same time. So taking the time to parry an attack requires switching one of your two busy fingers, and quite fast because you are reacting to a layout attack."
>
> （パリィは適応が最も困難なコントロールでした。戦闘中は移動と攻撃を同時に行っています。パリィのためには忙しい2本の指のどちらかを切り替える必要があり、しかも敵の攻撃に反応しているのでかなり速くなければならない）

### 3つの試行錯誤

**試行1: ゾーンタップ** — 画面左下のエリアをタップするとパリィ発動。左右どちらの指でも使えるが...

> "So my first instinct was to try to make it a zone, specifically at the bottom or at the left edge of the screen... But both in the teams and in playtest, everybody hated that solution. So I had to admit defeat here."
>
> （最初の直感はゾーン方式を試すことでした。画面下部や左端を...しかしチーム内でもプレイテストでも、全員がこのソリューションを嫌いました。ここでは敗北を認めざるを得ませんでした）

**試行2: クラシックなボタン** — 独立したパリィボタンを配置。精度は良いが、ボタンが増える問題と配置の悩みが生じる。

> "So then we tried the classic button too. So it works, but it requires fast reaction and precision. That's also one more button, and that's also the question of where do you place it? You place it on the left, you force the interruption of the movement. You place it on the right, force the interruption of the player action."
>
> （次にクラシックなボタンも試しました。動作はしますが、速い反応と精度が必要です。さらにボタンが1つ増え、どこに置くかという問題もある。左に置けば移動を中断させ、右に置けば攻撃アクションを中断させる）

**試行3: 「指を離す」方式（Let Go）** — 画面から全ての指を離すと、そのフレームでパリィ可能な攻撃が来ていればパリィが発動する。

> "Just let go of the screen. Meaning that on the frame of impact, if there is no input on the screen, the main character will parry automatically if that attack could be parried in the first place. It requires less precision on where to tap. And it also helps with timing. It's much more intuitive than the zone. It's faster to execute because you just have to remove fingers instead of remove it and place it somewhere else."
>
> （画面から指を離すだけです。インパクトのフレームで画面に入力がなければ、その攻撃がパリィ可能なものであればキャラクターが自動的にパリィします。どこをタップするかの精度が少なくて済み、タイミングも助けてくれます。ゾーン方式よりはるかに直感的で、指を離すだけなので実行も速い）

### 最終決定

> "Ultimately that was our best option, but since it's also not the most common type of control, we still went with a button by default as a safety. But personally whenever I play the game, I play with this on because it's a game changer for combat."
>
> （最終的にはこれが最良の選択肢でしたが、最も一般的な操作方式ではないため、安全策としてデフォルトはボタンにしました。でも個人的にプレイするときはいつもこれをONにしています。戦闘のゲームチェンジャーですから）

---

## 9. オートメーション化 — ボタンを減らしても操作権は奪わない

一部の操作は自動化してボタンそのものを削除した。

| 自動化した操作 | 仕組み | プレイヤーの操作権 |
|:---|:---|:---|
| **オートポーション** | 体力低下時に自動でポーション使用 | ポーションボタンを完全に削除 |
| **オートヒット** | 敵に近づくと自動で近接攻撃を発動 | 方向入力や他のスキルで割り込み可能 |

> "So while the game is pushing for the melee attack, you can still use direction and other skills to interrupt those melee attacks and perform more complex combos. So again here the goal was to not compromise on the game even when we tried to bring simplifications to players."
>
> （ゲームが近接攻撃を推進している間も、方向入力や他のスキルで近接攻撃を中断して、より複雑なコンボを実行できます。ここでもゲームを妥協しないことが目標でした。プレイヤーに簡略化を提供しようとしているときでさえ）

---

## 10. 操作カスタマイズ — プレイヤーに権限を渡す

### カスタマイズ機能の設計

> "So we built a control customization feature accessible in time from the pause menu. We have 3 profiles to try out different setups. Include the training room to test your controls with platforms and one enemy. This way the player can test immediately their changes."
>
> （ポーズメニューからいつでもアクセスできるコントロールカスタマイズ機能を構築しました。3つのプロファイルで異なるセットアップを試せます。プラットフォームと1体の敵がいるトレーニングルームも含まれており、プレイヤーは変更をすぐにテストできます）

カスタマイズ可能な項目:
- 各ボタンの **サイズ・位置・透明度**
- 入力方式の切り替え（ボタン/スワイプ/ゾーンタップ）
- **3つのプロファイル** で異なるセットアップを保存
- **トレーニングルーム** で即座にテスト

### HD開発チームによる「卒業試験」

カスタマイズ機能が完成した段階で、HD開発チームにビルドを送った。これがチームにとっての「クラッシュテスト」だった。

> "So once we had our full set of controls and controls customization, we sent a build to the HD team, which was a real crash test for us. So they replied to this video which was the best confirmation we could ask for. Here you have a dev from the HD team performing on touch control his favorite combo."
>
> （コントロールとカスタマイズの全セットが揃った段階で、HD開発チームにビルドを送りました。これは私たちにとって本当のクラッシュテストでした。彼らはこの動画で応えてくれました。これが得られる最高の確認でした。HD開発チームの開発者がタッチ操作でお気に入りのコンボを決めている映像です）

同時に、ゲーム中の最難関プラットフォーミングレベルもタッチ操作でクリアできることを確認した。

> "As in parallel we were looking into the most complex platforming level of the games and we were also able to pass them, confirming that the game can be finished on touch controls. For us it was a huge moment... and lucky for us because we only had a few months left in production."
>
> （並行して、ゲーム中の最も複雑なプラットフォーミングレベルに取り組み、クリアできることを確認しました。タッチ操作でゲームを完走できることの確認です。私たちにとって大きな瞬間でした。残り数か月しかなかったので、本当に幸運でした）

---

## 11. キャラクター挙動の調整 — パンドラの箱

### 変更のリスク

> "The question for us was more, should we make any changes to the character, knowing that it's a real Pandora's box. Because the problem is that when you start to change matrix you will impact your level, and that puts you in an endless loop closer to a creation project than a porting."
>
> （私たちにとっての問題は「キャラクターに変更を加えるべきか」でした。これは本当のパンドラの箱です。キャラクターのパラメータを変え始めるとレベルに影響が出て、移植というよりも新規制作に近い終わりのないループに入ってしまう）

メトロイドヴァニアでは、キャラクターの能力がエリアの進行制限として機能するため、能力を変更するとゲーム進行自体が壊れる可能性がある。

### 最小限の調整のみ実施

![キャラクター調整。Wall Grab Hold（壁掴み保持）とDouble Air Dash（空中二段ダッシュ）。どちらもデフォルトOFF](source/images/IMG_4472.jpg)

**壁掴み保持（Wall Grab Hold）**: 壁につかまった際に自動でずり落ちるのを止め、プレイヤーがボタンを押して手動で降りる方式に。プラットフォーミングの長いシーケンスを分割し、考える時間を確保する。

**空中二段ダッシュ（Double Air Dash）**: 空中ダッシュを2回実行可能に。空中制御が向上するが、レベルデザインを壊す可能性があった。

> "This had the potential to break the level design though. So that's a good example of where we had to call the HD team because they knew exactly the 3 to 5 potential places in the game where this could break the level design."
>
> （これはレベルデザインを壊す可能性がありました。HD開発チームに確認が必要だった良い例です。彼らはゲーム中のレベルデザインが壊れうる3〜5か所を正確に知っていました）

### 難易度支援機能

**5回死亡時のシールド提示**: 同じ場所で5回以上死んだプレイヤーに、1回だけダメージを吸収するシールドをオプション提示。10回で停止し、アクセシビリティと難易度オプションの探索を提案する。

> "If a player dies more than five times on the same checkpoint, we offer an optional shield... We did not stop there, because after 10 days we gave the message to the player suggesting to explore more accessibility and difficulty options."
>
> （同じチェックポイントで5回以上死んだプレイヤーにはオプションのシールドを提供します...10回を超えるとメッセージを表示し、アクセシビリティと難易度オプションの探索を提案します）

**ゲーム速度75%スローダウン**: 全体の速度を75%に落として反応時間を増やす機能。HD開発チーム側でも検討されていた機能を、モバイル版で実現した。

> "It allows the player to slow down time to 75% of the original speed. So what it does is it gives the player a larger reaction time between inputs which lowers the challenge both platforming and combat."
>
> （プレイヤーが時間をオリジナル速度の75%に減速できます。入力間のリアクション時間が長くなり、プラットフォーミングと戦闘の両方の難易度が下がります）

**プラットフォーミングアシスト**: HD版に存在した機能を全プレイヤーにオプション提供。ポータルを見つけてスキップを選択すると、レベルを飛ばせる。

---

## 12. モバイル専用の追加機能

### ビジュアルキューの強化

![ビジュアルキュー。方向アシスト（キャラクター周囲に入力方向の矢印を表示）と音声依存アイテムの視覚化](source/images/IMG_4495.jpg)

物理スティックがないモバイルでは、今どの方向に入力しているかが分かりにくい。

> "We added a direction assist, so that's just an arrow indicating your current direction input. Specially here because you don't have a physical limitation of a joystick. This really helps to confirm visually the direction that your current input is."
>
> （方向アシストを追加しました。現在の入力方向を示す矢印です。物理的なジョイスティックの制限がないモバイルでは特に重要で、現在の入力方向を視覚的に確認するのに本当に役立ちます）

### 音声依存機能の視覚化

宝探しアミュレット「Prospective Eye」は音声キューに大きく依存していた。モバイルプレイヤーの多くはサウンドOFFでプレイする。

> "It's an amulet that helps you to locate treasures. It is quite small and we relied a lot on audio on HD. Knowing that a lot of mobile players play with sound off, we added a pulse setting to make sure that it was visible."
>
> （宝の位置を特定するアミュレットです。小さくて、HD版では音声に大きく頼っていました。多くのモバイルプレイヤーがサウンドOFFでプレイすることを知っていたので、視覚的に見えるようパルス表示を追加しました）

### 入力精度の補正

> "Generally speaking, we know that mobile inputs are slightly less accurate. So we wanted to account for this by being more generous on the slowdown time window."
>
> （一般的に、モバイル入力は精度がやや低いことはわかっています。そこでスローダウン時間のウィンドウをより寛容にすることで補正しました）

パリィの反応時間を **25%延長** し、斜め入力の判定角度を拡大して方向コンボが発動しやすくなるよう調整した。

---

## 13. プレイテスト戦略 — 3回の異なるフォーマット

> "It was always part of our strategy to have playtests. The game was challenging, and player observation was really important for us. We did three playtests."
>
> （プレイテストは常に戦略の一部でした。ゲームが難しいため、プレイヤーの観察が本当に重要でした。3回のプレイテストを実施しました）

### 第1回: リモート・16人・基本操作の検証

| 項目 | 内容 |
|:---|:---|
| **形式** | リモート（オンライン） |
| **参加者** | 16人。モバイルとPC/コンソールの両方のプレイヤー |
| **焦点** | 3Cs（Camera, Controls, Character）とフリートライアルの長さ |
| **手法** | プレイヤーの画面録画＋入力表示＋ビデオでの反応収集 |

> "We collected it as early as we were, just going for basic controls and layout."
>
> （できるだけ早い段階で、基本的なコントロールとレイアウトについて収集しました）

**主な発見:**

1. **パリィのゾーンタップが全員に嫌われた** → この時点で廃止
2. **方向コンボが発動しない問題** を発見。プレイヤー自身は気づいていなかったが、映像からの分析で判明

> "Not something that was explicit from players, but we noticed on player footage that direction combos were not triggered remotely. So we enlarged the definition of what was up and diagonals to facilitate the trigger on those controls."
>
> （プレイヤーからの明示的なフィードバックではありませんでしたが、プレイヤーの映像から方向コンボがまったく発動していないことに気づきました。そこで「上」と「斜め」の判定範囲を拡大して、発動しやすくしました）

3. **コントローラーへの要望** が多数。ある参加者は自前のコントローラーを接続してプレイし始めた

> "We even had one player trying to manage to bring his controller as the feature was already available. We learned nothing from this player because we wanted to go about touch with the controller."
>
> （コントローラー対応がすでに実装されていたので、コントローラーを持ち込んでプレイし始めたプレイヤーもいました。このプレイヤーからはタッチ操作について何も学べませんでした）

4. **フリートライアルが短すぎる** → プレイヤーに「もっと遊びたい」という気持ちは生まれたが、HD版を知るプレイヤーからは「この完全なゲームがモバイルに収まるのか」という驚きの声も

### 第2回: 内部テスト・ゲーム完走の検証

| 項目 | 内容 |
|:---|:---|
| **形式** | 社内プレイセッション |
| **参加者** | Ubisoft 社内のモバイルゲーム経験者 |
| **焦点** | 「タッチ操作でゲームをクリアできるか」の二択的回答 |

最難関のイベントがエンドゲームに集中していたため、フルプレイスルーは不可能だった。そこで、すでにゲームをクリアした社員を集め、プレイヤー統計に基づいて選定したレベルをプレイさせた。

> "So the decision was to focus on players who beat the game already... We set it a bunch of levels based on the actual player stats on where they are. And we made sure to include platforming, combat, and boss fights."
>
> （ゲームをすでにクリアした人に絞る決断をしました...実際のプレイヤー統計に基づいてレベルを選定し、プラットフォーミング、戦闘、ボス戦をすべて含めました）

結果: **すべてのレベルがクリアされた**。ただしボス戦でのコントロール問題が特定された。

### 第3回: 対面テスト（Ubisoftシンガポール）・プリセットの検証

![プレイテスト3回目の結果。対面テスト、外部プレイヤー参加、コントロールリマッピングの検証、デフォルトプロファイルの決定](source/images/IMG_4493.jpg)

| 項目 | 内容 |
|:---|:---|
| **形式** | 対面（Ubisoft シンガポール） |
| **参加者** | 外部プレイヤー |
| **焦点** | プリセット・オプションの使用観察、デフォルト設定の決定 |

> "This time, we worked with Ubisoft user research in Singapore, used to organize the test with external testers, and we sent the team member on site to see it live, which was stability skiing to get first-hand learnings."
>
> （今回はシンガポールの Ubisoft ユーザーリサーチと協力し、外部テスターでテストを実施しました。チームメンバーを現地に送って直接観察させました。ファーストハンドの学びを得るためです）

**最大の発見**: プレイヤーが**独自の創造的な操作設定**を編み出してコンボを決めていた。

> "I'm saying that some players play with unexpected ways, playing like a spider like this. It's definitely not something we expect."
>
> （予想外の方法でプレイするプレイヤーがいました。蜘蛛のようにこうやって。まったく予想していませんでした）

---

## 14. 3つのプリセットプロファイル

プレイテストの結果を踏まえ、3つのプロファイルを定義した。

| プロファイル | 対象 | 特徴 |
|:---|:---|:---|
| **PC/コンソール体験** | コントローラー使用者 | HD版と完全に同じ操作 |
| **モバイル体験** | タッチ操作（**90%以上のプレイヤーが使用**） | タッチ最適化されたデフォルト設定 |
| **カジュアル体験** | ライト層 | オートパリィ、オートポーション、90%スピード |

> "Or default mobile experience was used by over 90% of our players once live."
>
> （デフォルトのモバイル体験は、ローンチ後90%以上のプレイヤーに使用されました）

さらに、戦闘難易度の専用プロファイル「Fighter」も設定した。タッチ操作に合わせて敵ダメージ0.7倍、敵体力0.7倍、ボス体力0.5倍、環境ダメージ0.5倍、パリィ難易度「Very Easy」、回避ウィンドウ「Long」に調整されている。

---

## 15. オンボーディングとフリートライアル — 最も戦略的な画面

### フリートライアル vs プレミアム

![フリートライアル vs プレミアム。"Let the Player Judge"（プレイヤーに判断させる）、"Keep it Short"（短くする）、"Finish on a High Note"（最高の瞬間で終わる）](source/images/IMG_4495.jpg)

> "I do not think there is a definitive proof that there is a better format between straight premium being purchased directly from the store and free trial. In fact, I don't even think you can prove it for a single product and it depends on game to game."
>
> （ストアから直接購入するプレミアム版とフリートライアルのどちらが良いかの決定的な証拠はないと思います。実際、単一の製品でも証明できないと思いますし、ゲームによって異なります）

フリートライアルを選択した理由は、操作の複雑なゲームでは**プレイヤーに自分の端末で動くか、操作が合うか判断させる**のが最善だと判断したからだ。

### 致命的な発見: 20%がゲームプレイに到達しなかった

> "Take your players directly to gameplay. Twenty percent of our players did not reach gameplay. We had too much friction from the extra steps going into gameplay."
>
> （プレイヤーを直接ゲームプレイに導いてください。20%のプレイヤーがゲームプレイに到達しませんでした。ゲームプレイに入るまでの余計なステップが多すぎたのです）

操作方式の選択画面がフリクション（摩擦＝ユーザーの行動を阻害する障壁）になっていた。90%がデフォルト（タッチ操作）を選ぶのに、わざわざ選択画面を挟む意味がなかった。

> "We were seeing 90 percent use the default option. So we needed to remove it from the first time user experience."
>
> （90%がデフォルトオプションを使っていました。だから初回ユーザー体験からこの選択画面を削除する必要がありました）

### フリートライアルの終了タイミング

> "If we think that there is a right moment to end your free trial, and that's on the double-end push. So it would be after the peak fines, between a clear break on the story."
>
> （フリートライアルを終了する正しい瞬間があるとすれば、それはダブルプッシュのタイミングです。盛り上がりのピーク後、ストーリーの明確な区切りの間で）

ボス戦直後の盛り上がった瞬間にフリートライアルを終了させた。さらにトレーラーを挟んで購入への動機づけを行った。

### 購入の最大タイミングは「プレイ前」

> "It was really interesting to see that the biggest purchase moment was even before playing the game."
>
> （最大の購入タイミングがゲームをプレイする前であったことは本当に興味深かった）

そこで購入のコールトゥアクション（CTA＝ユーザーに行動を促すUI要素）を、メインメニュー、ポーズメニュー、フリートライアル終了画面の3か所に配置した。

> "This is quite critical, if you work on a free trial, when mobile players need to be won on the first session, because they may not launch the game again."
>
> （フリートライアルで重要なのは、モバイルプレイヤーは最初のセッションで獲得しなければならないということです。二度とゲームを起動しないかもしれないからです）

---

## 16. プリプロダクションの重要性

![プリプロダクションファネル。完成品の分析 → ジャンル固有の課題の調査 → 競合ゲームの分析 → スコープ設定 → プレイテスト。80%以上のスコープが製造開始前に定義済み](source/images/IMG_4499.jpg)

セッションの締めくくりとして、Denance 氏はモバイル移植のプリプロダクションフレームワークを提示した。

> "Following this, we had 80% of our scope already defined before starting production. So more than a new creation, still with 20% uncertainty. So make sure to keep some buffer for the sensitive part, so that you can really push the polish forward."
>
> （このアプローチに従った結果、制作開始前にスコープの80%以上が定義済みでした。新規制作よりは予測可能ですが、20%の不確定要素はあります。デリケートな部分にバッファを確保して、品質の磨き込みを推進できるようにしてください）

| ステップ | 内容 |
|:---|:---|
| 1. 完成品の分析 | 強みと弱みを徹底分析 |
| 2. ジャンル固有の課題 | モバイルにおけるジャンル特有の問題を調査 |
| 3. 競合分析 | 同ジャンルの成功事例をインスピレーションまたはベンチマークに |
| 4. スコープ設定 | ソリューションを早期にスコープし統合 |
| 5. プレイテスト | 移植でもプレイテストは有効 |

---

## 17. Q&A セッションからの補足情報

### チーム規模と開発期間

> "So at the peak we are about 10, and I am working on a new game, and it was trusted for about a year."
>
> （ピーク時で約10名、約1年のプロジェクトでした）

質問者が「もっと大きなチームだと思った」と驚く場面があった。

### バイブレーション設計の実装コスト

バイブレーション設計は大きな作業になると予想していたが、HD版の設計がそのまま使えた。

> "Actually, all of the design on the HD software were really well suited for us to work with. So we didn't have to change much. But if you have to start from scratch, you do want a lot of confirmation on your action."
>
> （実際には、HD版のバイブレーション設計がそのまま適していました。大きな変更は不要でした。ただし、ゼロから始めるなら、アクションの確認としてのバイブレーションに力を入れるべきです）

モバイルではまず「操作の確認」としてのバイブレーションを優先すべきで、ゲーム体験の強化（敵を倒したときの爽快感など）は二の次だとアドバイスした。

### コントローラー接続時のUI切り替え

コントローラーを接続すると、PlayStation/Xbox/PC それぞれに対応したボタンマッピングとビジュアルが自動で表示され、画面上のタッチ操作UIは全て非表示になる。

### リモートプレイテストの運営

専門のプレイテストサービスを利用。ビルドを提供し、対象プレイヤーの募集・選定・テスト実施・画面録画・AI支援によるタイムスタンプ付きレポート・アンケートまでを一括で委託した。

---

## まとめ — 5つの教訓

| # | 教訓 | 具体例 |
|:---|:---|:---|
| 1 | **妥協しない** | コンソール版と同等のコンボ・プラットフォーミングをタッチで実現 |
| 2 | **選択肢を与える** | 3つのプロファイル、ボタン/スワイプ/ゾーンの切り替え、フルカスタマイズ |
| 3 | **データで判断する** | プレイヤー映像から方向コンボ未発動を発見、市場シェアで画面比率対応を決定 |
| 4 | **オンボーディングの摩擦は致命的** | 20%の離脱をもたらした操作選択画面の削除 |
| 5 | **キャラ挙動の変更はパンドラの箱** | HD開発チームと連携し、壊れうる3〜5か所を事前特定してから変更 |

### 数字で見るプロジェクト

| 指標 | 数値 |
|:---|:---|
| チーム規模 | 約10名 |
| 開発期間 | 約1年 |
| カメラ問題 | 450件以上 |
| 画面比率対応 | 4:3〜20:9 |
| コントローラー使用率 | 5%未満 |
| コントローラー使用者のコンバージョン率 | タッチの5倍 |
| デフォルト（モバイル体験）使用率 | 90%以上 |
| ゲームプレイ未到達率（改善前） | 20% |
| プリプロダクションでのスコープ定義率 | 80%以上 |
| App Store 評価 | 4.5以上 |
| ダウンロード数 | 300万以上 |

---

## 写真タイムスタンプ対応表

| 時刻 | ファイル | 対応セクション |
|:---|:---|:---|
| 10:31 | IMG_4450〜4451 | 会場・登壇者 |
| 10:33〜10:36 | IMG_4452〜4458 | カメラ・ミッション・出発点 |
| 10:41〜10:43 | IMG_4459〜4461 | コントロールの課題 |
| 10:47 | IMG_4462 | 操作ロジック変更（スプリント） |
| 10:50〜10:52 | IMG_4463〜4464 | ボタンフィードバック設計 |
| 10:54 | IMG_4465 | 移動操作の選択肢 |
| 10:56〜10:57 | IMG_4466〜4471 | 代替操作 |
| 10:58〜10:59 | IMG_4472〜4474 | パリィ操作の試行錯誤 |
| 11:00〜11:01 | IMG_4475〜4477 | オートメーション化 |
| 11:02〜11:04 | IMG_4478〜4481 | 操作カスタマイズ |
| 11:05〜11:06 | IMG_4482〜4487 | モバイル専用機能 |
| 11:07〜11:08 | IMG_4488〜4492 | キャラクター挙動調整 |
| 11:09〜11:10 | IMG_4493〜4494 | プレイテスト |
| 11:11〜11:14 | IMG_4495〜4498 | プリセットプロファイル |
| 11:16 | IMG_4499 | オンボーディング・まとめ |

---

## 記事化メモ

- 3本のトランスクリプト（計96KB）と50枚のスライド写真に基づいて再構成
- 音声認識ベースのため、一部はスライド画像と照合して補完
- データ（5%、20%、90%、25%、450件、10名、1年等）はセッション内で明言された数値
- 写真は相対パス（同フォルダ内）で参照。記事化時にアップロード先URLに置換すること

---

## おわりに

最後までお読みいただきありがとうございます。GDC 2026 の他のセッションレポートも順次公開していますので、ぜひフォローしてお待ちください。

**dsgarage Games** | GDC 2026 現地レポート
