# S7 ボーン1本から始めるキャラクターリギング — Skill 化用ノート

出典: Blender Fes 2026 AW Day1 S7（minusT、韓国語講演、17:30–18:30）。使用バージョンは画面表示で Blender 5.2.0 LTS。
凡例: 【話者】講演で語られた内容 / 【事実】Blender の仕様として確認できる内容 / 【画面】配信フレームで確認した内容。聞き取りが不明確な箇所は「(要確認)」。

## 0. 前提・方針

- 【話者】アドオンを使わず、Armature に 1 本目のボーンを置くところから手で組む。状況に応じてボーンを自分で構成する力をつけるのが目的
- 【話者】リグの良し悪しでアニメーション作業の効率が変わる。ボーンごとに「どう回るか・誰が触るか」を決めてから設定する

## 1. アーマチュアの基本操作

- 【話者】Shift+A → Armature で追加。Edit Mode でボーンの基本配置（レスト）を作り、Pose Mode で動かす
- 【話者】ボーンは Head / Tail の 2 点を持ち、Pose Mode では Head を中心に回転する
- 【話者】重なったボーンは複数回クリックまたは Alt+クリックで選び分ける
- 【話者】Armature プロパティ > Viewport Display で Names（名前表示）、In Front（メッシュ越しに表示）、Axes（ローカル軸表示）をオンにすると作業しやすい。Display As を Stick にするとウェイト塗り中にボーンが邪魔になりにくい
- 【話者】Bone プロパティは Edit Mode と Pose Mode で表示項目が異なるので注意
- 【話者】Extrude（E）で作ったボーンは元ボーンが Parent になる。Shift+A で追加したボーンは Parent なし
- 【話者】Relations の Connected を外すと親子関係を保ったまま離せる（ビューポートでは点線で表示）
- 【話者】Edit Mode ではピボットポイントを Individual Origins にしておくと、スケールや回転が各ボーンの Head 基準になる

## 2. 回転モードの選び方（Euler / Quaternion）

- 【事実】ボーンの Pose Mode での回転モードは既定で Quaternion (WXYZ)。オブジェクトは既定で XYZ Euler
- 【話者】2 つの向きの補間: Quaternion は最短経路でなめらか。Euler は横にぶれる回転になることがあり、ジンバルロックも起きる
- 【話者】自由な方向に回るボーンは Quaternion にするとアニメーションを付けやすい
- 【話者】Quaternion は 4 つの数値から向きを直感的に読めない。180 度を超えて何回転もさせる場合は最短経路になるため、キーフレームを細かく打つ必要がある
- 【話者】Euler は角度をそのまま読め（例: X = 373°）、Graph Editor で回転の加減速を作りやすい。回転軸が決まったボーン、一方向に何回転もするボーンは Euler にする
- 【話者】指のボーンは XYZ Euler にする。複数ボーンは Rotation Mode を変えたあと右クリック > Copy to Selected で一括反映

## 3. 人体ボーンの作成順と命名

- 【話者】3D Cursor をワールド原点に戻して（右クリック > Snap > Cursor to World Origin）アーマチュアを追加
- 【話者】1 本目は `Origin`（キャラクター全体の位置用）。F2 で名前を付ける
- 【話者】体の中心から Tail を Extrude して胸 → 首 → 頭。spine の Head から下へ Extrude して骨盤（hips）
- 【画面】ボーン名は `Origin` / `torso` / `hips` / `spine` / `ribs` / `neck` / `head`
- 【話者】spine と hips の間に `torso` を追加し、spine と hips の Parent を torso（Connected オフ）、torso の Parent を Origin にする → Origin で全体、torso で胴体をまとめて動かせる
- 【話者】脚: X-Axis Mirror をオフにして hips を Shift+D で複製 → Extrude で太もも・すね・足・つま先。thigh の Parent を hips にする
- 【画面】脚のボーン名は `thigh` / `shin` / `foot` / `toe`
- 【話者】腕: 肩ボーンを作り Parent を ribs（Connected オフ）。上腕 → 前腕 → 手を Extrude。上から見て腕の中心を通るよう調整し、upper_arm の Parent を shoulder にする
- 【画面】腕のボーン名は `shoulder` / `upper_arm` / `lower_arm` / `hand`
- 【話者】手: palm を作り、そこから指を 3 本 Extrude。palm の Parent を hand。複製して他の指と親指を作る
- 【画面】手のボーン名は `palm`〜`palm.003`、`finger_index.001`〜`.003`、`finger_middle`、`finger_ring`、`finger_pinky`、`thumb.00x`
- 【話者】複数ボーンの改名は Ctrl+F2（Batch Rename）で、対象を Object ではなく Bone にする
- 【話者】指ボーンの高さは、正面から見て指の中心よりやや上（甲側）に置くと曲げたときに自然になる
- 【話者】左半分を作ったら全選択 > Armature > Names > Auto-Name Left/Right で `.L` を付与 → Armature > Symmetrize で右側を生成（名前は `.R` に置換される）。以後 X-Axis Mirror オンで対称編集できる
- 【話者】Axes 表示でローカル軸を確認。全選択して Alt+R でロールをリセットしてから、指はローカル X 軸が曲げ軸になるよう Roll を調整する（親指は特にずれやすい）
- 【話者】目: `eye.L` を追加して Parent を head。デフォルメキャラは目が大きいので、大きな球の中心あたりに Head を置く。Symmetrize で `eye.R`
- 【画面】目のボーン名は `eye.L` / `eye.R`（大文字小文字は要確認）

## 4. スキニングとウェイト

- 【話者】変形に関わらないボーン（Origin、torso、各種コントロールボーン）は Bone プロパティの Deform をオフにする → 頂点グループが作られない
- 【話者】メッシュ → アーマチュアの順に選び Ctrl+P > Armature Deform > With Automatic Weights。子になり、Armature モディファイアーと頂点グループが追加される
- 【話者】Ctrl+P の 3 種の違い: With Automatic Weights（自動ウェイト）/ With Empty Groups（空の頂点グループだけ作成）/ Armature Deform（頂点グループなし）。Object でペアレントした場合は Armature モディファイアーを手動で追加する
- 【話者】Armature モディファイアーは Subdivision Surface より上に置く。頂点が少ない状態で変形させてから細分化する
- 【話者】Mirror モディファイアーが未適用のオブジェクトは片側の頂点しかないので、`.L` のウェイトだけ設定すれば `.R` 側はモディファイアーが補う
- 【話者】Weight Paint はアーマチュア → メッシュの順に選んで入る。Bone Selection が使え、Alt+クリックでボーンを選ぶと対応する頂点グループが選ばれる
- 【話者】Options の Auto Normalize はオンで作業する（ウェイト合計 1 を保つ）
- 【話者】Gradient ツール: Weight 1 で上から下へドラッグすると 1→0 のグラデーション。消したい側は Weight 0 でグラデーション。塗りやすい角度に回してから引く
- 【話者】Blur でウェイトをなめらかにする。自動ウェイトはボーンが密集する場所でにじむ。脚のボーンは腰まで影響が広がりやすいので上側を 0 にする
- 【話者】塗りたくない部分に塗れてしまうときは、Edit Mode で面を選んでから Weight Paint の Face Selection Masking で塗る
- 【話者】指のウェイトは数値で割り当てる: 頂点を選択 > 頂点グループの ▼ > Remove from All Groups → 指先ボーンに Weight 1.0 で Assign。関節のループは隣接 2 本に 0.5 ずつ Assign
- 【話者】済んだ部分は H で隠して次へ進み、最後に Alt+H で戻す → 二重作業を防ぐ
- 【話者】N サイドバーの Vertex Weights で値を変えてもアクティブ頂点しか変わらない。選択頂点すべてに揃えるには Copy ボタンを押す
- 【話者】別オブジェクトの頭は With Empty Groups → head グループに全頂点 1.0 → head をロック → Delete All Unlocked Groups で不要グループを一括削除
- 【話者】Mirror モディファイアーがないオブジェクト（目など）は L と R を両方割り当てる
- 【話者】首と頭の境目が不自然なら、体の首の最上部で head グループのウェイトを少し足してなじませる

## 5. トポロジーとウェイトの関係（落とし穴）

- 【話者】ウェイトをいくら直しても改善しないときは、頂点の配置が原因のことがある
- 【話者】関節付近にループ 3 本 → 曲げると内側に食い込む。中央のループを抜く → 内側は解消するが外側の形が保てない
- 【話者】外側を狭く・内側を広くループを置く → 形を保つ関節になる。内外でループ数を変えると両方の性質を持たせられる
- 【話者】肘は形を保つため頂点を密に。指は曲げたときに中央が少しへこむほうが自然なので 3 本ループのままにする
- 【話者】ボタンのような付属物は、土台（リボン）の頂点がある位置に配置する。土台に頂点がない位置だと変形時に離れる → モデリング段階で頂点位置を合わせておく

## 6. ボーンコレクションの整理

- 【話者】コントロールボーンを作る前にコレクションで分ける。M キーで移動し、表示のオン/オフで必要なボーンだけ見る
- 【画面】コレクション名は `CONTROL` / `CONTROL-IK` / `Facial` / `DEFORM` / `DEFORM-hand` / `DEFORM-Cloth` / `DEFORM-Skirt` / `Mechanism`
- 【話者】Origin と torso は CONTROL、変形する人体ボーンは DEFORM、手は DEFORM-hand、直接触らない補助ボーンは Mechanism に入れて隠す
- 【話者】コントロールボーンは Bone プロパティ > Viewport Display の Bone Color で色分けし、Copy to Selected で一括適用

## 7. 腕の IK

- 【話者】Edit Mode（X-Axis Mirror オン）で hand を Shift+D 複製 → G Z 0.1 でずらして改名 `hand.IK.L`（右は `.R`）→ Deform オフ → 少し大きくして G Z -0.1 で元の位置に戻す（重なっても見分けがつく）
- 【話者】upper_arm と lower_arm の間から Extrude → Connected を外して後方へ移動 → `elbow.IK.L` / `.R`
- 【画面】IK ボーン名は `hand.IK.L` / `elbow.IK.L` / `knee.IK.L` / `Leg.IK.L` / `head.IK`
- 【話者】IK ボーンの Parent を Origin にする（腕から独立して自由に動かすため）。複数は Copy to Selected で一括設定
- 【話者】Pose Mode で lower_arm に Inverse Kinematics → Target: Armature / `hand.IK.L`。Chain Length 1 だと前腕だけ、2 で上腕まで追従
- 【話者】Pole Target: `elbow.IK.L`。Pole Angle を調整して肘が elbow.IK の方向に曲がるようにする（右側は角度が異なる場合あり）
- 【話者】落とし穴: Edit Mode で上腕と前腕が完全に一直線だと、IK が曲げる方向を決められない → 肘関節を少し後方にずらしておく
- 【話者】右側: lower_arm.R → Shift+クリックで lower_arm.L をアクティブにし Copy Constraints to Selected Bones → Target と Pole Target を `.R` に差し替え、Pole Angle を再調整
- 【話者】hand に Copy Rotation（Target: `hand.IK.L`）を追加 → IK ボーンだけで手の向きも制御できる。右側はコピーしてターゲットを差し替え

## 8. 脚と足の IK（かかと上げ対応）

- 【話者】foot.L を複製して X に 0.1 m ずらし `foot.IK.L` / `.R` → スケールを変えて元位置へ
- 【話者】膝から Extrude → Connected オフ → 前方へ移動 → `knee.IK.L` / `.R`
- 【話者】IK ボーンを CONTROL-IK へ移動、Deform オフ、Parent を Origin
- 【話者】shin.L に腕と同じ要領で IK（Target: foot.IK、Pole: knee.IK、Chain Length 2 と推定 (要確認)）。これで hips を動かすと脚が自動で追従する
- 【話者】問題: このままだと足裏が床に接地せず回転してポーズが付けにくい → 足の階層を組み直す
- 【話者】foot.L の Tail から Extrude → `Leg.IK.L` → Deform オフ・色設定・Parent を Origin
- 【話者】foot.L に Copy Rotation（Target: foot.IK）
- 【話者】foot.L の Tail から Extrude → `foot.IK.001.L` → Parent を Leg.IK。スナップを Vertex にしてボーンの Head/Tail に吸着させ、foot と逆向きで Head が foot の先端にあるボーンにする
- 【話者】foot.IK.L の Parent を foot.IK.001.L に変更 → foot.IK.001 を回すと子の foot.IK が動き、かかとが上がる
- 【話者】toe を複製して `toe.IK` → Parent を Leg.IK → toe.L に Copy Rotation（Target: toe.IK）→ つま先を foot.IK.001 と独立して動かせる
- 【話者】foot.IK は直接触らなくなるので Mechanism コレクションへ移して非表示
- 【話者】結果: Leg.IK（足の位置）、foot.IK.001（かかと上げ）、toe.IK（つま先）の 3 本で、足裏を床に付けたままポーズが取れる
- 【話者】作ったコントロールボーンはすべて Deform をオフにする

## 9. 頭と目のトラッキング

- 【話者】head を Shift+D で複製して前方へ移動 → `head.IK` → 前を向くよう回転 → Parent を Origin → Deform オフ
- 【話者】head に Track To（Target: head.IK）→ Track Axis を調整して正しい方向を向かせる
- 【話者】Track To の Target Z をオンにすると、head.IK を回したときに頭の傾き（ロール）も追従する
- 【話者】目も同じ方法で視線コントロールを作れる（講演では省略）

## 10. 指と手のひらのコントローラー

- 【話者】finger_*.002 に Copy Rotation（Target: finger_*.001）
- 【話者】Target と Owner の Space を Local Space にする → ローカル回転値をコピーする
- 【話者】Axis は X のみ残す → 曲げ方向（ローカル X）だけをコピーし、他の軸はコピーしない
- 【話者】Mix を Add にする → コピーした角度に加えて、そのボーンを手で追加回転できる
- 【話者】finger_*.003 にも同じ Copy Rotation を貼る → 1 本目を回すだけで指全体が曲がる
- 【話者】他の指へは Copy to Selected で貼り、各 Target を差し替える
- 【話者】palm.002 に Copy Rotation（Target: palm.003、Local Space、Influence 0.5）→ palm.003 の 50% だけ追従
- 【話者】palm.001 に同じ Copy Rotation を Influence をさらに下げて追加。加えて palm.001 に Target: palm.L の Copy Rotation を弱い Influence で追加 → 両端の palm 2 本で手の丸みを作れる（具体値は要確認）
- 【話者】検証: 左手のポーズを Ctrl+C → Ctrl+Shift+V（反転ペースト）で右手に貼り、右側の設定が正しいか確認する

## 11. 表情（Shape Key + Driver）

- 【話者】デフォルメキャラの表情は、ボーン＋ウェイトより Shape Key で直接形を作るほうが意図どおりにしやすい
- 【事実】Shape Key があるメッシュではモディファイアーを適用できない
- 【話者】左右で異なる表情を作るには Mirror の適用が必要 → Shape Key をいったん全削除 → Mirror を適用 → Basis と新規 Shape Key を追加 → Edit Mode で目を閉じた形を作る
- 【話者】眉（EyeBrow）と顔（Face）の両オブジェクトに同じ表情の Shape Key を作る
- 【話者】反対側: Shape Key の値を 1 にした状態で ▼ > New Shape from Mix → 新しい Shape Key を Flip（Mirror Shape Key）で左右反転 (メニュー名は要確認)
- 【画面】Shape Key 名は `Eye.Close.L` / `Eye.Close.R`、制御ボーン名は `Facial.Eye.Close.L` / `.R`
- 【話者】Facial コレクションを作り、目の近くにボーンを追加 → Deform オフ → Parent を head（頭の回転に追従）→ Symmetrize で R 側
- 【話者】Shape Key の Value を右クリック > Add Driver → Type を Averaged Value → 変数の Object にアーマチュア、Bone に制御ボーン、Type を Y Location、Space を Local Space
- 【話者】そのままでは大きく動かさないと効かない → Drivers エディターの Modifiers で Generator を追加し、x の 1 次係数を 50 にする（移動量 × 50）
- 【画面】Generator は Expanded Polynomial、Order 1、Constant 0.000、x^1 50.000
- 【話者】右クリック > Copy Driver / Paste Driver で顔オブジェクトの Shape Key にも貼る → ボーン 1 本で 2 オブジェクトを同時に制御。R 側は貼ってから Bone を R 側に差し替え
- 【話者】制御ボーンに Limit Location（Local Space、Y の Min/Max）→ Affect Transform をオンにしないと表示だけが制限され値は範囲外に出る → オンにして実値も制限

## 12. 衣装: 単純な付属物

- 【話者】帽子の頭部分: Armature Deform でペアレントし、頂点グループを手動追加して head に 1.0。Armature モディファイアーを Subdivision より前に移動
- 【話者】靴: toe だけに割り当てる → かかとを上げても靴は動かず、つま先を動かしたときだけ曲がる
- 【話者】リボンの装飾ボーン: 新しいボーンコレクションに追加し、Parent を ribs。Extrude で制御ボーンを作り、`.L` が末尾に来るよう命名 → Alt+R でロールをリセット
- 【画面】リボンのボーン名は `Ribon.L` / `Ribon.001.L` / `Ribon.002.L`（綴りは画面どおり）
- 【話者】ボーンが多い場合、With Empty Groups は大量の頂点グループを作ってしまう → Armature Deform（グループなし）でペアレント → Weight Paint で必要なボーンだけ選択 → Weights > Assign Automatic from Bones → 選んだボーンの頂点グループだけ自動ウェイトで作られる
- 【話者】リボンの布部分は ribs に 1.0、頭側は head に 1.0（H で隠しながら順に割り当て）
- 【話者】L 側が R のボーンに引っぱられるなど自動ウェイトの誤りは Weight Paint で 0 を塗って直す
- 【話者】体を動かしてついてこない頂点がある → ウェイト未設定の頂点がある → 追加で割り当てる
- 【話者】ボタン: 変形させないため全頂点に同じウェイトを割り当てる（例: Ribon.002 に追従）

## 13. 衣装: 体に密着したドレス（Data Transfer）

- 【話者】ドレスを With Empty Groups でペアレント（空の頂点グループを全ボーン分作成）
- 【話者】Data Transfer モディファイアーを追加し、Armature モディファイアーより上に置く
- 【話者】Source に体オブジェクト、Vertex Data をオン、Vertex Groups を Nearest Face Interpolated → 最も近い面からウェイトを補間して取得
- 【話者】落とし穴: Source の体がアーマチュアで変形していると、大きなポーズでウェイトがおかしくなる → 体を Shift+D で複製し、Armature Deform が効かない状態にしたものを Source にする
- 【話者】ドレスに必要な spine、ribs、hips などの頂点グループだけを残し、他は削除
- 【話者】Blender 外（ゲームエンジン等）で使う場合は、Deform 系モディファイアーの代わりに Data Transfer でウェイトを転送して使う

## 14. 衣装: スカート（Surface Deform ケージ）

- 【話者】フリルなど複雑なスカートは Surface Deform で単純なケージに変形を任せる
- 【話者】8 頂点の Circle を追加 → Edit Mode で Extrude・編集してスカートの外側を覆うケージにする（衣装と少し重なる程度、上下にも少し延長）
- 【話者】ケージは Object プロパティ > Visibility > Ray Visibility の Camera と Shadow をオフ、Outliner でもレンダー無効にする
- 【話者】ドレスに Surface Deform（Target: ケージ）を追加し、Armature の後に置く → Bind。ケージを変形するとドレスが追従する
- 【話者】ケージの形を編集したら Unbind → Bind し直す
- 【話者】フリルのオブジェクトにも同じケージで Surface Deform → 2 オブジェクトを 1 つのケージで変形
- 【話者】アーマチュアとケージだけを選び、テンキー / でローカルビューにして作業
- 【話者】ボーンコレクションに Skirt を追加し、スナップを Vertex にしてケージの頂点に吸着させながらスカートのボーンを配置。Parent を hips
- 【画面】スカートのボーン名は `Skirt.b` / `Skirt.b.001`〜`Skirt.b.004`（.L / .R 付き）
- 【話者】左側を作ったら Auto-Name → Symmetrize で右側
- 【話者】Edit Mode で Shift+N > Global +Z Axis でロールを一括で揃える → Pose Mode でローカル X / Z 回転だけでスカートのポーズが取れる
- 【話者】ケージを Armature Deform でペアレント → Weight Paint でスカートのボーンを選び Assign Automatic from Bones → スカートのボーンだけの頂点グループを作る → Weight Paint で調整
- 【話者】ケージの上部は hips、さらに spine の頂点グループも追加してウエストを体に追従させる
- 【話者】ドレスの上半分は体と同じ Armature 変形、下半分は Surface Deform にしたい → 頂点グループ `SurfaceDeform` を作り、下半分の頂点に 1.0 を Assign
- 【話者】Surface Deform の Vertex Group に `SurfaceDeform` を指定、Armature の Vertex Group にも同じグループを指定して Invert → 上半分は Armature、下半分はケージで変形

## 15. 頂点ペアレント

- 【話者】ボタン → Shift+クリックでスカートを選択 → Tab で Edit Mode → ボタン付近の頂点を 3 つ選んで Ctrl+P（Make Vertex Parent）
- 【話者】ボタンにウェイトを付けなくても、3 頂点の中心に貼りついたまま動く。Blender 内で完結するモデルなら最も手軽

## 16. 手首のねじれ（Bendy Bones）

- 【話者】手を回すと前腕が不自然にねじれる。通常は前腕にツイストボーンを追加してウェイトを設定して解決する
- 【話者】代わりに lower_arm の Bone プロパティ > Bendy Bones で Segments を増やす（講演では 4）
- 【話者】Armature の Display As を B-Bone にすると分割が見える。太すぎる表示は Pose Mode の Transform > Scale B-Bone（Ctrl+Alt+S）で細くする
- 【話者】Edit Mode で Bendy Bones の Ease In / Ease Out をどちらも 0 → 前腕が曲線状に曲がらず、手の回転に合わせてねじれだけが分散される（ツイストボーン相当）

## 17. 検証手順

- 【話者】ウェイト調整は、Pose Mode で R キーでボーンを回しながら確認し、不要な影響を消す
- 【話者】最後に腕と脚の IK、指、表情のコントローラーをすべて使ってポーズを付け、破綻がないか確かめる
