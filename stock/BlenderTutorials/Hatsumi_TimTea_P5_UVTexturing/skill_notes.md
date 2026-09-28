# Hatsumi「TimTea Part 5 UV Mapping/Texturing」Skill 化用ノート

出典: YouTube「Making A 3D Vtuber Avatar for TimTea! Part 5 UV Mapping/Texturing『Blender』」（Hatsumi、2024-05-14 公開、37:34）。
凡例: 【話者】= 作者の意見・判断（本動画はナレーションがないため、テロップと画面上の行動から読み取れる判断のみ）、【事実】= Blender やアドオンの一般的な仕様、【画面】= 動画の画面で確認した値。不確かなものは「(要確認)」。時刻は動画内の時刻（フレーム抽出時刻に基づく目安）。

## 前提・環境

- 【画面】ナレーションなし。冒頭テロップは「hii im back / this video covers uv mapping and texturing」（00:08）
- 【画面】Blender のバージョン表示は確認できず。アドオンのサイドバータブに UV Squares、Ucupaint（1.0.12）、Outline Helper、VRM、MMD、CATS、iFacialMocap、BlenderKit が見える
- 【画面】ワークスペースは UV Editing と Texture Paint を往復。左に UV エディター、右に 3D ビューポート
- 【画面】体・髪・衣装はコレクション（Body / hair / Clothing）で分けている。衣装は belt jacket、hoodie、jacket inner、pants、shoes など多数のオブジェクト
- 【画面】体の素体（base re.001）には Mirror（Axis X、Clipping オン、Merge 0.001 m）と Subdivision Surface（Levels Viewport 1、Render 2）が付いたまま展開している

## 1. シーム（00:08〜）

- 編集モードでシーム（Mark Seam）を入れる。【画面】肩まわり（腕の付け根の輪）、手首、脚の側面などに赤いシーム線（00:08〜01:34）
- 【事実】シームは UV 展開時の切れ目。目立たない側面・内側・服の縫い目に置くのが一般的
- 【画面】Mirror モディファイアー適用前に展開しており、左右で同じ UV を共有していると思われる（要確認。左右で塗り分けたい部分があるなら適用後に展開する必要がある）

## 2. 展開（Unwrap）

- 【画面】Unwrap の操作パネル: Method Angle Based、Fill Holes オン、Correct Aspect オン、Use Subdivision Surface オフ、Margin 0.001（靴 27:45、髪 32:18）
- 【事実】Angle Based は Blender の既定の展開方法。Conformal より歪みが少ない傾向
- 【画面】体は胴と脚がひと続きの大きなアイランド、指を広げた手、足、小さな部品に分かれる（07:16 の UV 表示）

## 3. 画像（テクスチャ）の作成と保存（02:37 頃）

- UV エディター > Image > New で画像を作成。【画面】名前「TimTea Body Base」、作成直後は黒
- Image > Save As で PNG 保存。【画面】File Format PNG、Color RGBA、Color Depth 8、Compression 15%、保存先はプロジェクトフォルダ
- 【画面】衣装用「Tim Tea Clothing」、髪用「Tim Tea Hair Base」も同様に作成。体・衣装・髪で画像を分ける
- 【事実】Blender 内で作った画像は保存しないとファイルに残らない。塗る前に一度保存し、Texture Slots の Save All Images でまとめて保存できる

## 4. アイランドの整理（03:35〜）

- 衣装オブジェクトを複数選んで同時に編集モード（マルチオブジェクト編集）に入り、1 枚の画像に向けてまとめて展開・配置する
- UV Squares（UV エディターのサイドバー）でアイランドを格子化する。【画面】パネルの項目: Snap to Axis (X or Y)、Snap with Equal Distance、To Grid By Shape、To Square Grid、Rip Vertex、Rip Faces、Snap to Closest Unselected、「V - Join (Stitch), I - Toggle Islands」
- 【画面】ベルト・ひもなど帯状のパーツは長方形に直してまとめて並べる（05:41）
- 【画面】髪の房も格子化し、並べたアイランドを Scale 0.214 で縮めて空きに詰める（33:43 頃）
- 【画面】靴は衣装の展開後に追加で展開し、衣装画像の空いている場所に置く（27:45）
- 【事実】UV を直線にそろえると、線や模様がゆがみにくく、画像の面積も無駄になりにくい

## 5. テクスチャペイント: 体（07:16〜）

- Texture Paint ワークスペース。【画面】Texture Slots の Mode は Material、スロットは TimTea Body Base / soft shade.png / Light.png / Shadow.png の 4 枚
- 【画面】TexDraw、Blend Multiply、Radius 91 px、Strength 1.000（07:17）
- 【画面】Standard Brush（外部のブラシ素材から読み込んだと思われるアイコン）、Blend Mix、Radius 28 px、Strength 1.000 で鎖骨・腹筋・ひざの線を描く（08:07）
- 【画面】Smear、Strength 0.274〜0.296、Radius 112〜145 px で線と血色をぼかす（09:50〜12:13）
- 【話者】（画面上の行動から）体は線を描いてから大きめの Smear でなじませる順番

## 6. テクスチャペイント: 衣装・髪を Ucupaint のレイヤーで（15:10〜）

- 【事実】Ucupaint は画像をレイヤーとして重ね、合成方法を選んで塗れるアドオン。サイドバーの Ucupaint タブで操作する
- 【画面】衣装のレイヤー（上から）: Tim Tea Clothing Shadow、Soft Shade、Light、Clothing（土台）。影レイヤーの合成は「Mult…」（Multiply と思われる）、値 1.00。土台は Mix 1.00
- 【画面】Soft Shade レイヤーの合成は「So…」表示（Soft Light か、要確認）
- 【画面】影色は明るい紫。乗算で下の色を暗くする
- 【画面】描く: TexDraw、Blend Mix、Radius 7〜10 px、Strength 1.000、Stroke Spacing 10%、Anti-Aliasing オン
- 【画面】消す: 同じ TexDraw で Blend を Erase Alpha（Radius 46〜73 px）
- 【画面】ぼかす: Soften、Radius 8 px、Strength 0.496、Blur Mode Gaussian、Kernel Radius 2
- 【画面】塗りつぶし: Fill ツールも使用（16:42）
- 【画面】髪のレイヤー: Tim Tea Hair Light、Soft shade、Shadow、Hair Base の 4 枚（36:19）
- 【話者】（画面上の行動から）影の縁は一部だけぼかし、くっきりした境目を残す

## 7. マテリアル（髪）

- 【画面】髪（ponytail など）のマテリアル: Surface が Emission、Color に ColorRamp、Strength 1.000
- 【画面】髪には OH_Outline_Material も付く（Outline Helper アドオンのアウトライン用、と思われる。要確認）
- 【事実】Emission で出すと、ライトの当たり方に左右されにくいセルルック寄りの見た目になる

## 落とし穴・チェック項目

- 新規画像は塗る前に保存する。Save All Images でまとめて保存する習慣をつける
- ミラー適用前に展開すると左右の UV が重なる。左右非対称の模様（例: ズボンの片脚だけの X 模様）がある部分は、そのパーツを別オブジェクトにするか適用後に展開する（本動画の X 模様は別オブジェクト「x pants」、要確認）
- 帯状パーツは格子化してから配置すると、塗りのゆがみと画像の無駄が減る
- 陰影をレイヤーで分けておくと、後から影だけ・光だけを直せる
- 後から追加するパーツ（靴）のために、画像に空きを残しておく

## Blender Fes Extra「3D衣装の作り方」との違い

- **ベイクの有無。** Extra の講座は、ハイモデルから Substance 3D Painter でノーマルと AO を焼き、テクスチャも Substance で作る。本動画はベイクをせず、Blender の Texture Paint と Ucupaint で色と陰影を手で描く
- **陰影の持たせ方。** Extra は AO やライティングの焼き込みで立体感を出し、lilToon の影設定と組み合わせる。本動画は Shadow / Light / Soft Shade のレイヤーに手描きの陰影を持たせ、髪は Emission でライトの影響を抑える
- **UV の整え方。** Extra は Mio3 UV でグリッド化・直線化し、6 枚のタイルに部位ごとに詰める。本動画は UV Squares で格子化し、体・衣装・髪の 3 枚の画像に分ける
- **向いている絵柄。** Extra は少しリアル寄りの衣装、本動画はアニメ調の VTuber アバター。完全なイラスト調ならベイクを省いて描く、という Extra の講師の補足とも整合する
