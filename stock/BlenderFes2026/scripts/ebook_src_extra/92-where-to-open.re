= 「開く場所」総索引

各章の終わりに添えた「開く場所」の表と、本文で触れた操作の場所を、アプリ別・工程順にまとめ直した索引です。手を動かしながら「あの操作はどこだったか」を探すときに使っていただけると便利です。

==[notoc] 索引の読み方

 * Blender は 5.x の日本語 UI の表記を基本にし、英語 UI の名前を括弧で添えています。講座の解説は Blender 5.1 で行われています
 * 「>」はメニューやパネルをたどる順番、「→」はその後に続ける操作を表します
 * 講座画面や公式マニュアルで表記を確かめきれなかった項目には「(要確認)」を付けています。ご自身の環境の表記を優先していただくと確実です
 * 「章」の列は、その操作を主に扱っている本書の章です

== Blender

=== モデリング(第 3 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-blender-1]{
章	目的	開く場所
----
3	素体を読み込む	ファイル > インポート > FBX (.fbx)
3	下絵(デザイン画)を置く	-Y 視点で Shift+A > 画像 > 参照(Reference)
3	下絵を正面だけに表示する	プロパティエディター > オブジェクトデータタブ > エンプティ > 表示先(Show In)の「透視投影」をオフ、「不透明度」(Opacity)を下げる
3	下絵を選択できないようにする	アウトライナー > フィルター > 制限の切り替え > 選択可能(Selectable)を表示 → 下絵の矢印をオフ
3	テクスチャの色あせを直す	プロパティエディター > レンダータブ > カラーマネージメント > ビュー変換(View Transform)を「標準」に
3	素体の色を表示する	ビューポートシェーディング > マテリアルプレビュー → マテリアルタブ > ベースカラー(Base Color)に画像テクスチャ
3	左右対称に作る	3D ビューポート > サイドバー(N) > 編集(Edit)タブ > AutoMirror
3	辺の流れを整える	編集モード > 右クリック > Set Flow(EdgeFlow)/ LoopTools > Relax
3	辺の向きを変える	編集モード > 右クリック > 辺を時計回りに回転(Rotate Edge CW)
3	辺や面をまとめる	編集モード > 削除 > 辺や面を統合(Collapse Edges & Faces)
3	繰り返しパーツを並べる	プロパティエディター > モディファイアータブ > 配列(Array)+ カーブ(Curve)
3	五角形以上の面を探す	編集モード > 選択 > 特徴で全選択(Select All by Trait) > 面の辺数(頂点数 4、タイプ「大きい」)
3	非多様体を探す	編集モード > 選択 > 特徴で全選択 > 非多様体(Non Manifold)
3	面の向きを確かめる	ビューポートオーバーレイ > 面の向き(Face Orientation)→ Alt+N > 反転 / 面の向きを外側に揃える
3	法線をリセットする	Alt+N > ベクトルをリセット / オブジェクトデータタブ > 形状データ > カスタム法線データをクリア
//}

=== UV 展開(第 4 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-blender-2]{
章	目的	開く場所
----
4	シャープを一括でシームにする	編集モード > 選択 > 類似選択(Select Similar) > シャープ → 右クリック > シームをマーク(Mark Seam)
4	UV を展開する	UV Editing ワークスペース > A で全選択 > UV > 展開 > アングルベース(Angle Based)
4	UV の歪みを見る	UV エディター右上 > オーバーレイ > UV ストレッチ(UV Stretch)
4	UV を直線にする	UV エディター > サイドバー(N) > Mio3 UV > グリッド化 / 矩形 / ストレート(日本語表記は要確認)
4	Mio3 UV を入れる	編集 > プリファレンス > エクステンションを入手(Get Extensions) > Mio3 UV
4	UV タイルを増やす	UV エディター右上 > オーバーレイ > タイル数(Tiles)
4	左右の UV を重ねる	ミラーモディファイアー > データ > ミラー U(Mirror U)
//}

=== ハイモデル(第 5 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-blender-3]{
章	目的	開く場所
----
5	ポリゴンを細かくする	プロパティエディター > モディファイアータブ > モディファイアーを追加 > 生成 > マルチレゾリューション(Multiresolution) > 細分化 / リニア
5	シワを見やすくする	3D ビューポート右上 > ビューポートシェーディングのポップオーバー > 照明 > MatCap
5	左右対称に描く	スカルプトモードのヘッダー右 > 対称(Symmetry)の X
5	割りを作り直す	スカルプトモードのヘッダー > Dyntopo(ディテールタイプ「相対ディテール」)→ 密度(Density)ブラシ
5	ZBrush へ送る	GoB のボタン(GoB アドオン)
//}

=== ベイクのためのパーツ分け(第 6 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-blender-4]{
章	目的	開く場所
----
6	交差するパーツを分ける	編集モード > L でリンク選択 > メッシュ > 分離 > 選択(Separate > Selection、P)
6	ベイク用のパーツを整理する	アウトライナー > 右クリック > 新規コレクション(New Collection)
6	ベイク用の FBX を書き出す	ファイル > エクスポート > FBX > 選択したオブジェクト / スケールを適用「すべてFBX」/ 三角面化
//}

=== ボーンとウェイト(第 8 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-blender-5]{
章	目的	開く場所
----
8	根元を自由に動かす	プロパティエディター > ボーンタブ > 関係(Relations) > 接続(Connected)をオフ
8	ボーンを見やすくする	オブジェクトデータタブ(アーマチュア) > ビューポート表示 > 表示方法(スティック)/ 軸 / 最前面
8	ボーンロールをそろえる	編集モード > アーマチュア > ボーンロール > ロールを再計算(Recalculate Roll) > グローバル +Z 軸
8	衣装とアーマチュアを親子付けする	オブジェクトモード > Ctrl+P > 空のグループで(With Empty Groups)
8	素体のウェイトを移す	モディファイアーを追加 > 編集 > データ転送(Data Transfer) > 頂点データ > 頂点グループ > 最近接面の補間
8	揺れ物のウェイトを付ける	ウェイトペイントモード > グラデーションツール(自動正規化をオン)→ ウェイト > スムーズ
8	数値でウェイトを調整する	yuzuWeightEditor のエディターウィンドウ(頂点を選ぶと表に値が出る)
8	変えたくないグループを守る	プロパティエディター > オブジェクトデータタブ > 頂点グループの鍵アイコン
8	Unity 向けに整える	ウェイトペイント > ウェイト > クリーン / 合計を制限(Limit Total、制限 4)/ すべてを正規化(Normalize All)
//}

=== Unity への書き出しと質疑応答(第 9・10 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-blender-6]{
章	目的	開く場所
----
9	Unity 向けの FBX にする	ファイル > エクスポート > FBX > アーマチュア > リーフボーン追加(Add Leaf Bones)をオフ
10	ひねりを確認する	ポーズモード > ヘッダーの座標系「ローカル」、ピボットポイント「個々の原点」 > R → Z
10	角の丸まりを防ぐ	編集モード > 辺を選択 > Shift+E(辺のクリース)→ サブディビジョンサーフェスを適用
10	揺れ物のウェイトを残して転送する	オブジェクトデータタブ > 頂点グループの鍵アイコン → データ転送
//}

== Substance 3D Painter

=== ベイク(第 6 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-sp-1]{
章	目的	開く場所
----
6	プロジェクトを作る	ファイル > 新規(Ctrl+N) > メッシュの「選択」
6	ベイク画面を開く	テクスチャセットの設定(Texture Set Settings) > メッシュマップをベイク(Bake Mesh Maps)
6	ハイモデルと読み取り範囲を指定する	ベイク画面 > 共通設定 > ハイポリメッシュ / 前面・背面の最大距離 / アンチエイリアス
6	ローとハイを名前で対応付ける	ベイク画面 > 共通設定 > マッチ > メッシュ名別(By Mesh Name)
6	他パーツの影を入れない	ベイク画面 > Ambient occlusion > セルフオクルージョン > 同じメッシュ名のみ
6	焼いた画像を書き出す	ファイル > テクスチャを書き出し(Ctrl+Shift+E) > 出力テンプレート(RGB / グレー)
//}

=== テクスチャ(第 7 章)

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-sp-2]{
章	目的	開く場所
----
7	ノーマルと AO を編集できるようにする	テクスチャセットの設定 > チャンネルの + / 標準ミキシングとアンビエントオクルージョンのミキシングを「置き換え」(Replace)
7	UV 境界の描画の問題を避ける	テクスチャセットの設定 > UV パディング > 3D スペースネイバー
7	見え方を整える	表示設定(Display Settings) > 環境マップ / シェーダー設定(Shader Settings)
7	形と色を分けて塗る	レイヤーパネル > 塗りつぶしレイヤーを追加 > 右クリック > 黒のマスクを追加 > マスクを右クリック > ペイントを追加
7	UV 単位で塗る	ツールバー > ポリゴン塗りつぶし(Polygon Fill、4 キー) > UV 単位のモード
7	布端を膨らませる	マスクを右クリック > ジェネレーターを追加 > UV Border 系(要確認)→ 塗りつぶしレイヤーの Height
7	縫い目を引く	ツールバー > パスツール(Path)+ ステッチ系のブラシ
7	手で直す	ツールバー > 指先ツール(Smudge)
7	環境光を焼き込む	レイヤーにフィルターを追加 > Baked Lighting Environment / Baked Lighting Stylized
7	ツヤを調整する	塗りつぶしレイヤーのプロパティ > Roughness
//}

== ZBrush

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-zbrush]{
章	目的	開く場所
----
5	作業用のコピーを作る	サブツール(SubTool) > 複製(Duplicate)
5	左右対称に描く	シンメトリ有効(X)
5	ポリゴンの流れを整える	ジオメトリ > ZRemesher → ディバイド(Divide)
//}

ZBrush の日本語 UI の表記は、講座画面で確かめきれていません(要確認)。

== Unity と lilToon

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-unity]{
章	目的	開く場所
----
9	フォルダーを分ける	Project > Create > Folder(Material / Texture / FBX)
9	FBX の読み込み設定をする	FBX を選択 > Inspector > Model タブ > Read/Write をオン、Blend Shape Normals を None
9	マテリアルを取り出す	FBX の Inspector > Materials タブ > Extract Materials
9	シェーダーを変える	マテリアルの Inspector > Shader > lilToon
9	AO で影を制御する	マテリアルの Inspector > 影設定 > AO マップ
9	ノーマルマップを効かせる	マテリアルの Inspector > ノーマルマップ設定 → Fix Now
9	縁を明るくする	マテリアルの Inspector > リムライト設定(マスクに AO マップ)
9	反射を付ける	マテリアルの Inspector > 光沢設定 > 反射 / 環境光の反射
9	金属の質感を出す	マテリアルの Inspector > マットキャップ設定
9	HDRI をキューブマップにする	テクスチャの Inspector > Texture Shape を Cube に
9	表示を固定する	Inspector 右上の鍵アイコン
//}

lilToon の各設定は、見出しのチェックをオンにしないと効かない点に注意が要ります。

== そのほかのアプリ

//latextsize[|p{0.06\linewidth}|p{0.24\linewidth}|p{0.55\linewidth}|]
//table[where-other]{
章	目的	開く場所
----
2	もう一方のポーズで確かめる	ペイントソフト > 変形ツール(自由変形)で腕を傾ける
2	資料を並べて見比べる	PureRef に画像と三面図を並べて貼る
//}
