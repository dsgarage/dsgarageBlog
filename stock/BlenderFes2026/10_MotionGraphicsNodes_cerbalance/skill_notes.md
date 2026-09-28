# D2 S2 ノードで楽しむモーショングラフィックス表現 — Skill 化用ノート

出典: Blender Fes 2026 AW Day2 S2（cerbalance、韓国語講演、11:15–12:15、本編は 12:08 頃まで）。使用バージョンは画面から特定できず、講演では Blender 5.0〜5.2 の機能に言及。
凡例: 【話者】講演で語られた内容 / 【事実】Blender の仕様として確認できる内容 / 【画面】配信フレームで確認した内容。聞き取りが不明確な箇所は「(要確認)」。ノード名は英語 UI 名。`cs_` で始まる名前は登壇者の自作ノードグループで、Blender 標準ノードではない。

## 0. 前提・方針

- 【話者】非破壊で修正に強い構造を作る。素材が変わっても、事前に決めたルールが新しい結果に自動で対応する形にする
- 【話者】ノードをつなぐ前に「画面をどんな構造として見るか」を決める
- 【話者】表現を点・線・面（と、それらが組み合わさった物体）に分解し、各要素がどんなルールで配置・変化しているかを見る
- 【話者】同じ表現にも複数の出発点がある（例: 割れたガラス = 1 枚の面を分割する / 粒をまいてガラス片に育てる）。最初に正解を決めず、合わない方法を除外し、作って検証して選ぶ
- 【話者】作る前に簡単な「制作シナリオ」（必要な機能と、最初の形をどの順に発展させるか）を書き、予想が合っているか作りながら確かめる
- 【話者】学習は完成品の手順をなぞるより、「今ある点をどう動かす・増やす・散らす・別の形にするか」を自問して機能を 1 つずつ覚える
- 【話者】自分の仮説で作る → 違えば YouTube や Google で「もっと良い方法」を探す、の繰り返しで学んだ
- 【話者】できることとできないことを意識的に分け、できない部分は表現の方針で割り切る（例: 拾ったオブジェクトのテクスチャは使わず、単色に近い非現実的なシェーダーに置き換える）

## 1. 初期の習作で得た置き換え（Geometry Nodes）

- 【話者】らせん階段: キューブを一定間隔で上げて回転させる方法 → らせん状のカーブをポイントに変換し Instance on Points で並べる方法のほうがよい
- 【事実】カーブからポイントへの変換は Curve to Points、ポイントへの配置は Instance on Points
- 【話者】切り欠きドーナツ: Mesh Boolean を何度も使う → Extrude → Merge by Distance は形が不自然で最適化にも問題があった
- 【話者】ここで For Each を覚えた（For Each Element ゾーン、要確認）
- 【話者】一度作ったノードは再利用して、同じオブジェクトを繰り返し作る流れになる

## 2. World シェーダーで空と星を作る（Shader Nodes / World）

- 【話者】World で Texture Coordinate を使うと、大きな球に描くようにテクスチャが貼られる。これを利用して Voronoi Texture で星を作る
- 【画面】「star」フレーム: Texture Coordinate（Generated、Object 欄に Camera）→ Vector Rotate（X Axis）→ Voronoi Texture（3D、F1）の Distance → Map Range ×3 → Multiply → Mix
- 【画面】「sky」フレーム: Texture Coordinate → Mapping → Gradient Texture（Linear）→ Color Ramp → Hue/Saturation/Value → Gamma
- 【画面】「Satellite」フレームに RaySatellite という自作グループが複数並び、Maximum で合成
- 【話者】自作グループ RaySphere: カメラから特定方向へ光線を飛ばし、仮想の球に投影する関数。Radius を下げると構造が分かりやすい
- 【画面】RaySphere の接続: Texture Coordinate（Generated、Camera）→ Vector Rotate（X Axis）→ RaySphere（入力 Vector / Pos / Radius、出力 Vector / Mask）→ Vector Rotate（Z Axis）
- 【画面】山のシルエット: RaySphere Mount（出力 Mask / Noise）→ Color Ramp → Mix
- 【話者】落とし穴兼発見: カメラが球の範囲を外れると別の色が出る。問題だが、それを利用すると面白い画面になった

## 3. Dot Product で溝を色分けする柱シェーダー

- 【話者】内積（Dot Product）で画面中心からの距離を求め、溝のある柱を影ではなく色の分離で見せる
- 【画面】Pillar Shader グループ: Mapping → Separate XYZ → Combine XYZ → Dot Product → Greater Than / Multiply → Map Range → Color Ramp
- 【画面】グループ入力: Shadow Key、Distance、Bright、Dark、Threshold、From Min、From Max、Rust Color
- 【話者】テクスチャを外して自作シェーダーに置き換える作業が多いので、グループ化して多くの場所に使い回した
- 【話者】Dot Product 以外は基礎的な概念だけでできている

## 4. モデリングせずに建物を組む（Geometry Nodes）

- 【話者】建物は「線を描く → 面にする → 立体にする」構造と捉え、線から面を作ってから Extrude する
- 【話者】アーチを両側に使うと不自然なので、片側をブリッジ、反対側をアーチにする。Offset で自由に調整できるようにした
- 【話者】アーチ形を繰り返してキューブと合わせたパーツを 4 種類作り、コレクションで切り替えられるようにした
- 【画面】配置の後段: Length → Greater Than → Delete Geometry → Transform Geometry → Set Material

## 5. Shader の Raycast で霧・ハーフトーンを作る

- 【話者】霧: カメラの前に Raycast 用の平面を置き、ピンクがかった霧を重ねる
- 【画面】霧の接続: Geometry の Incoming → Vector Math（Scale、-1.000）→ Raycast の Direction。Geometry の Position → Raycast の Position。Raycast の Hit Distance → Math（Divide）→ Map Range → Color Ramp → Multiply
- 【話者】ハーフトーン: 「Incoming で方向ベクトルを作り、Scale で -1 倍して Raycast に入れる」のが一番大事
- 【画面】ハーフトーンの接続: Geometry の Incoming → Vector Math（Scale、-1.000）→ Raycast の Direction（Length 26 前後、要確認）→ Hit Distance → Map Range（Float、Linear、Clamp）→ Mix Shader の Factor。Mix Shader の 2 入力は Transparent BSDF と Emission → Material Output の Surface
- 【話者】Voronoi Texture でハーフトーンの模様が作れる。Map Range や Mix で値を変えるとさらに多様な表現になる
- 【画面】別案: Texture Coordinate → Separate XYZ → Math（Add）→ Math（Less Than）→ Mix Shader（Transparent BSDF と混合）
- 【話者】落とし穴: Raycast を使うマテリアルは EEVEE で Render Method を必ず Blended にする。しないと表示に問題が出る
- 【画面】マテリアル設定の Render Method が Blended
- 【話者】効果を強めたいときは色の明度を変える。色は何色でもよい
- 【話者】Raycast ノードに気づいたのは Blender 5.0 か 5.1 のころ（要確認）

## 6. Geometry Nodes で破片を作る（Cell Fracture）

- 【話者】破片用アドオンは使わず、ノードで分割した
- 【画面】「Cell Fracture」フレーム: Grid → Join Geometry / Extrude Mesh / Set Position → 面を用意。Distribute Points on Faces（Random、Density、Seed）→ Instance on Points → Mesh Boolean（Difference）→ Scale Elements（Edge、Scale 1.000）→ Delete Geometry（Edge、All）＋ Compare（Integer、Equal）→ Bake（Animation）
- 【画面】「Instantiation & Set Transform」フレーム: Split to Instances（Point、Group ID に Mesh Island）→ Translate Instances（Local Space）→ Rotate Instances（Local Space）
- 【話者】大きいまま割ると最適化の問題が出るので、小さく作ってから拡大する
- 【話者】一部を削って不規則に見せる。破片は似た形の使い回しで、色とスケールで違いを出す
- 【話者】主役でない装飾なら、多少不自然でも目立たないので割り切ってよい
- 【話者】画面全体に使うには重い

## 7. Blob Tracking の 2D Convert（Geometry Nodes）

- 【話者】自作グループ Trail Capture: オブジェクトのポイントを拾って枠と線を描く。一番重要なのは内部の「2D Convert」
- 【話者】2D Convert は、カメラ方向へ光線を飛ばし、3D の点をカメラに見えるとおりの位置で平面に押し付ける処理
- 【話者】Raycast は「ある方向に仮想の光線を飛ばし、どこで何に当たったかを取得する」ノード
- 【画面】2D Convert の接続: Active Camera → Object Info（Location / Rotation / Scale）。Grid（Vertices X 2、Y 2、Size X / Size Y はグループ入力）→ Transform Geometry（Translation = カメラ Location、Rotation = カメラ Rotation）→ Transform Geometry（Scale、Absolute 経由）→ Raycast の Target Geometry
- 【画面】Subtract（Vector）でカメラ Location と Position の差を取り Ray Direction へ（差を取る向きは要確認）。Raycast の Is Hit → Set Position の Selection、Hit Position → Set Position の Position
- 【画面】後段: Mesh to Curve → Set Position → Curve to Mesh
- 【画面】Trail Capture の入力: Trail Points、Trail Selection、NDC（Boolean）、NDC Scale（0.050）、Trail / Object、Camera。手前に Random Value（Boolean、Seed 75）
- 【話者】NDC オフ: 線は 3D 空間の点をそのまま追う。NDC オン: 線がカメラの前に張り付き、遠近に関係なく一定の太さで見える
- 【話者】NDC という名前は厳密な用語の使い方ではない
- 【話者】前段階の例: チューブ状オブジェクトからシミュレーションで点を打ち続け、その点からカーブを作る。2D Convert なしだとチューブの点に沿って伸びるだけ

## 8. Compositor Nodes のトラッキング UI

- 【話者】レンダー表示とコンポジターを同時に有効にして使う。最適化していないので環境によっては落ちる可能性がある
- 【話者】Voronoi Texture とモンキー（Suzanne）を使い、AOV も利用している（詳細は要確認）
- 【話者】3D 位置を画面座標に変えるノード（「3D to Screen」相当、名称は要確認）は検索すれば既存のものが見つかり、中身も同じ仕組みなので説明を省略
- 【話者】Map UV: UV があれば画像をその UV 上に配置できる。画面に文字を並べるために使った
- 【話者】cs_Pixelize UV: Image Coordinates の Pixel 出力（左下から右上へ増えるピクセル値）を一定値で割り、Fraction で端数を取って U を作る。下段で V を作り、合成して UV マップにする
- 【画面】Pixelize UV の内部: Multiply → Floor → Divide、Divide → Fraction → Divide → Add → Combine XYZ（Z 1.000）。Float Curve も接続
- 【画面】cs_Pixelize UV の Pixel Size は 50 px（字幕では「Pixel Grid」と表示、要確認）
- 【話者】cs_String Sprite: 外部スプライトを使わず、800×80 px のキャンバスに数字を描く。Number / ABC を切り替え可能
- 【画面】String Sprite の設定: Canvas Size X 800 px、Y 80 px、Scale 1.000。Text は MonoSans、Size 26.9 px、Center / Middle。String は Length 12、Offset -2、Spacing 4、Delete に文字を指定
- 【話者】判断基準: 数字の「1」は Map UV で並べたときに目立って不自然なので削除している
- 【話者】Map UV の後、Maximum で cs_Track Object と合成する
- 【話者】cs_Track Object: blob として 100×50 px の四角形を作る。値を 200 や 100 に変えると形が変わる。ノイズで時間ごとの変化も見られる
- 【画面】Track Object の内部: Noise Texture（4D、fBM、Normalize、Scale 5.000、Detail 2.000、Roughness 0.500）→ Math（Multiply、2.000）→ Math（Power）→ Math（Multiply）
- 【話者】要所は Subtract、Multiply、Power（Exponent）。これらはグループの外から制御できる
- 【話者】落とし穴: コンポジターは解像度が変わるとずれるので合わせる
- 【話者】線は Cryptomatte（要確認）で取り出したマスクに Glare の Fog Glow（要確認）をかけて作った
- 【話者】このファイルは後日配布予定

## 9. Math ノードでアニメーションを自動化する

- 【話者】モーショングラフィックスで使うグラフの形は限られているので、あらかじめカーブとして用意し、それで動きを作ると速く効率的
- 【話者】cs_Motion Fraction に Float Curve がつながっており、一定間隔の動きがそのカーブどおりになる
- 【話者】Idle Factor でリニアな動きとの混ぜ具合を決める。実際のモーショングラフィックスはグラフをそのまま使わず、ある程度リニアと混ぜる（値を上げ下げしたときの向きは要確認）
- 【画面】cs_Motion Fraction の入力: W、Centering、Idle Factor（0.833）、Fraction（1.000）。出力: Value、WFloor
- 【話者】音楽と切り離せないので BPM を扱うノードと組み合わせる
- 【画面】cs_BPM Time: BPM 142.000、Cycle 2。出力 Value / Floor / Modulo
- 【話者】cs_Vector Mapping: Motion Fraction で作った動きを、位置・回転・スケールにどう割り当てるかを設定するノード
- 【画面】接続: cs_BPM Time → cs_Motion Fraction ×2 → Random Value ×3 → cs_Vector Mapping（Position / Rotation / Scale）→ Instance on Points → Transform Geometry
- 【話者】中央の星形は World に置いたもので、画面中央に固定される。ほかはカメラに追従して生成される

## 10. クロスシミュレーションで文字を揺らす（Geometry Nodes）

- 【話者】Blender 5.2 で Geometry Nodes からクロスシミュレーションが使えるようになった（ノードに「Experimental」と表示、正式名は要確認）
- 【話者】一番大事なのは、最初から文字をシミュレーションに入れないこと。まず Mesh Line で始める
- 【話者】ポイント表示（Mesh to Points、要確認）で確認しながら、クロスシミュレーションのノードに入れる
- 【話者】揺れはノイズや Voronoi で Factor にパルスを入れたもの
- 【話者】上端の点をピンで固定: Capture Attribute で位置を取り → Separate XYZ で Z だけ → Greater Than で上端だけを選択
- 【話者】Greater Than の値をアニメーションさせると、ある瞬間に一斉に落ちる動きが作れる
- 【話者】自作の Count / Repeat グループ: Instance on Points で Mesh Line を複数複製して Realize する。複製数や Y・Z 方向の間隔を変えるとアニメーションが変わる
- 【話者】文字との合成: String to Curves → 回転 → Fill Curve → Transform で縦向きにし、カーブに沿ってインスタンス化する（ノード名は要確認）
- 【話者】String to Curves の Curve Index / Line Index（要確認）は今回の方法には不要。文字の繰り返し方を変えたいときに使う
- 【話者】Custom Force（要確認）で重力のような力を加えられる。登壇者はノイズと Multiply / Scale で風の強さを変えた。衝突も表現できる

## 11. 聞き取りで不確かな語

- 「납시 분리」: 画面では Mesh Boolean（Difference）で分割していた（要確認）
- 「키토매트」: Cryptomatte と推定（要確認）
- 「포 글로」: Glare の Fog Glow と推定（要確認）
- 「슬기 투 스크린」: 3D to Screen 相当のノードと推定（要確認）
- 「TrackNCC」: Track Object のオプションを指す語と推定（要確認）
- 「Cloud Simulation Experimental」: クロスシミュレーションのノード（要確認）
