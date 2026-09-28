# Hatsumi「TimTea Part 4 Shoes」Skill 化用ノート

出典: YouTube「Making A 3D Vtuber Avatar for TimTea! Part 4 Shoes『Blender』」（Hatsumi、2023-09-01 公開、23:24、ナレーションなし）。
凡例: 【話者】= 作者の意見・運用上の判断、【事実】= Blender の一般的な仕様、【画面】= 動画の画面で確認した値・操作。画面から推測したものや判別できないものは「(要確認)」。時刻は動画内の時刻。
注意: この動画は BGM のみでナレーションがない。【話者】の項目はテロップと説明文由来のみ。手順はすべて【画面】からの読み取り。確認したフレームは 30 枚で、フレーム間の操作は推測を含む。

## 前提・環境

- 【画面】Blender のバージョンは読み取れない（要確認）
- 【画面】N パネルのタブ: Item / Tool / View / Edit / Outline Helper / BlenderKit / iFacialMocap / VRM / MMD / Misc / CATS / Animation / PDT / Rigify / Ucupaint
- 【画面】Outline Helper パネルに Add/Set Outline・Adjust Outline・Remove Outline のボタン。トゥーン用の輪郭線を付けるアドオンと推測（名称・入手元は要確認）。靴の造形には使っていない
- 【画面】背景と横に設定画（靴のアップ、全身）を常時表示。設定画の靴: 白いつま先とソール、青いアッパー、赤いひものハイカットスニーカー
- 【話者】14:34 のテロップ「Week 2 Day 6- Jacket Details」。制作日ごとに区切って記録している

## 1. 靴の土台: 平面からケージを作る（00:15〜00:35）

- 【画面】オブジェクト名は Plane.001。足（素体 base re）を複製せず、平面から作る
- 【画面】00:15: Front Orthographic で、設定画の靴の輪郭に重ねて頂点を移動（Dx 1.941 m などのヘッダー表示あり）
- 【画面】00:25〜00:35: 足をすっぽり囲む低ポリの箱状ケージ。ワイヤーフレーム越しに中の足が見える
- 【画面】モディファイアー: Subdivision Surface（Catmull-Clark、Levels Viewport 1、Render 2、Optimal Display）→ Mirror（Axis X）
- 【画面】Mirror Object は 00:15 時点で空欄、01:39 以降は base re（素体）
- 【事実】Mirror Object を指定すると、そのオブジェクトの原点を中心に反転する。靴の原点が体の中心にない場合でも左右の靴を正しく映せる
- 【事実】Subdivision が Mirror より上だと、中央の継ぎ目がなめらかに処理されないことがある。靴のように左右が離れた部品では問題にならない
- Skill 化の手順案: Plane を追加 → 正面図で足首〜つま先の輪郭に頂点を置く → 押し出しで足を囲む箱にする → Mirror（Mirror Object = 素体）と Subdivision を追加

## 2. スカルプトで形を出す（01:01〜03:15）

- 【画面】Grab ブラシ Strength 0.400。Radius は 155 px（01:01〜01:39）→ 130 px（02:35）→ 89 px（03:15）→ 188 px（04:36）→ 138 px（05:23）
- 【画面】01:01: X 線表示に近い半透明の表示で、靴の中の足指が透けて見える。足を見ながら靴の外形をつま先側へ引き出す
- 【画面】01:39: Front Orthographic で設定画と並べ、足首まわりの高さを合わせる
- 【画面】02:35: 甲から履き口の面が紫に色分けされる（マテリアルか Face Sets か要確認）。ベロと履き口の範囲の確認用と推測
- 【画面】03:15: 甲のつなぎ目付近を小さい Radius で整える
- 【事実】スカルプトの Grab は頂点数を変えないので、ケージの低ポリ構成を保ったまま形を詰められる
- 貫通対策: 足（素体）を表示したまま半透明で作業し、足が靴から突き出していないか常に見える状態にする

## 3. ソール（04:02〜06:44）

- 【画面】04:02: Right Orthographic、編集モード。靴底の辺（マゼンタで強調）を選んで高さをそろえる
- 【画面】05:23: 側面から見ると、底の近くに複数の辺ループが接近し、ソールの段差が見える
- 【画面】06:09: 底の近くに接近した 2 本の辺ループを選択（Right Orthographic）
- 【画面】06:44: 側面の完成形。つま先は低く丸く、甲から足首まで立ち上がるハイカット。底はフラット
- 【事実】Subdivision Surface では、辺ループを近づけるほどその位置の曲面が締まり、溝や角として見える
- 【画面】ソールは別オブジェクトになっていない（Plane.001 の一部）。完成時は shoes という 1 オブジェクト
- Skill 化の指針: ソールを別パーツにする方式（厚み付きの底板を別に作る）と、同一メッシュで辺ループを寄せて溝を作る方式を並べる。この動画は後者
- Skill 化の手順案: 底面の外周ループを選ぶ → ループカットを 2 本追加して底の近くへ寄せる → 2 本の間をわずかに内側へ縮めて溝にする（縮める操作は画面で未確認、要確認）

## 4. ハトメ（アイレット）（07:25〜08:37）

- 【画面】オブジェクト名 Circle.002。完成時は shoe holes
- 【画面】モディファイアー: Mirror（Axis X、Mirror Object base re、Clipping オフ、Merge オン 0.001 m）→ Subdivision Surface
- 【画面】リングを複製して履き口の縁に沿って並べる。Move X 0.0676 m（07:25）
- 【画面】07:59: Select Linked（Delimit の選択肢 Normal / Material / Seam / Sharp / UVs が表示）で 1 個ずつ選んで調整
- 【画面】08:37: 履き口の左右に小さなハトメが縦に並ぶ（片側 5〜6 個程度、要確認）
- 【事実】Select Linked（L または Ctrl+L）で、同じオブジェクト内のつながった部分だけを選べる。複製したリングを 1 オブジェクトにまとめても個別に動かせる
- Skill 化の手順案: Circle を追加 → 押し出しと拡大縮小で平たいリングにする → 複製して履き口の縁に並べる → 1 オブジェクトにまとめる → Mirror で反対の靴へ

## 5. 靴ひも（09:45〜10:48）

- 【画面】オブジェクトは NurbsPath（カーブ）。完成時は laces（laces things も存在、用途は要確認）
- 【画面】カーブの編集モードのツールバー: Select Box / Cursor / Move / Rotate / Scale / Transform / Annotate / Measure / Draw / Extrude / Radius / Tilt / Shear / Randomize
- 【画面】モディファイアー: Subdivision Surface → Mirror（Mirror Object base re）
- 【画面】制御点をハトメの位置に置き、甲の上で X 字に交差させる。Move Z -0.0529 m（09:45）
- 【画面】10:48: 交差したひもが 5 段程度、最上部から横に結び目の輪と垂れた端がある（要確認）
- 【画面】ひもの太さの付け方（Geometry の Bevel Depth か、別の方法か）は画面で確認できない（要確認）
- 【事実】カーブの太さは Object Data プロパティの Geometry → Bevel → Depth で付け、Radius ツールで制御点ごとに太さを変えられる
- Skill 化の手順案: NurbsPath を追加 → 制御点をハトメ位置へ順にスナップ → 交差の上下でひもが重ならないよう Z を調整 → Bevel Depth で太さ → 結び目は別カーブで輪を作る

## 6. パンツの装飾（11:43〜13:29）

- 【画面】11:43: pants の編集。Move Z 0.1365 m、Proportional Editing オン、Falloff Smooth、Proportional Size 0.180
- 【画面】12:50: X 字の Plane（Subdivision のみ）を太ももに重ね、その下に編み上げ状の帯。完成時は x pants
- 【画面】13:29: Subdivision 付きの Cube で小さな箱形の部品（用途は要確認）
- Skill 化の指針: 服の表面に乗る装飾は別オブジェクトの平面で作り、服から少し浮かせて貫通を避ける

## 7. ジャケットの細部（14:34〜22:40）

- 【画面】14:34: base re.004（手袋。手の甲と手首を含む、要確認）の手の甲に紫、手首に緑の色分け。Material.011 / Material.013
- 【画面】15:39: shirt.003 の辺に Edge Crease Factor -0.256
- 【画面】16:59: jacket outer の前端を Move X 0.3789 m、Proportional Editing（Smooth、Size 0.198、Connected）で調整
- 【画面】20:34: jacket outer のすその辺に Edge Crease Factor 0.643
- 【画面】22:40: Cube.001 を Rotate 5.61°（Z 軸）。胸元の小さな飾り部品と推測（要確認）
- 【事実】Edge Crease は Subdivision Surface がかかったときの辺の締まり具合を 0〜1 で指定する。Shift+E で設定し、負の値を入れると既存の値から減らす
- 【画面】完成時の Clothing コレクション: belt jacket / belt sleeve / belts / choker / gloves blue / gloves outer / hoodie / jacket inner / jacket outer / jacket outer.001 / jacket pockets / pants / shirt。コレクション外: BezierCircle / Cube / Cube.001 / jacket buttons / laces / laces jacket / laces things / shoe holes / shoes / x pants / zipper

## 落とし穴・チェック項目

- 靴の Mirror には Mirror Object（素体）を入れる。空欄のままだと靴自身の原点で反転する
- 足（素体）を表示したまま半透明で造形し、つま先・かかとの貫通を目で確認する
- ソールの溝は辺ループ 2 本の間隔で締まり具合が変わる。寄せすぎると角が立ちすぎる
- ハトメとひもは靴本体と別オブジェクトにする。数や位置をあとから変えやすい
- ひもの制御点はハトメの中心に合わせる。ずれるとハトメを貫通して見える
- 角の締まりは辺ループの追加ではなく Edge Crease でも調整できる（頂点数を増やさない）
