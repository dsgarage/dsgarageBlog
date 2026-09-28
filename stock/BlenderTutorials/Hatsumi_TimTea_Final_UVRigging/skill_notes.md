# Hatsumi「TimTea Final Part Rigging/Blendshapes」Skill 化用ノート

出典: YouTube「Making A 3D Vtuber Avatar for TimTea! Final Part UV Rigging/Blendshapes『Blender』」（Hatsumi、2024-09-19 公開、24:44）。
凡例: 【話者】= 作者の意見・判断（本動画はナレーションがないため、テロップと画面上の行動から読み取れる判断のみ）、【事実】= Blender や関連ツールの一般的な仕様、【画面】= 動画の画面で確認した値。不確かなものは「(要確認)」。時刻は動画内の時刻（フレーム抽出時刻に基づく目安）。

## 前提・環境

- 【画面】ナレーションなし。冒頭テロップは「This is the final video in this series, this covers rigging and blendshapes」（00:01）
- 【画面】アーマチュアのプロパティに Layers / Protected Layers（ボーンレイヤー）が表示される。Blender 4.0 でボーンコレクションに置き換わった UI なので、3.x 系と推定（要確認）
- 【画面】サイドバーのタブ: Outline Helper、BlenderKit、iFacialMocap、VRM、MMD、CATS、Animation、PDT、Rigify、Ucupaint など
- 【画面】アーマチュアのプロパティに Wiggle Armature パネルがある（揺れ物を Blender 内で試すアドオンと思われる、名称・用途は要確認）
- 【画面】アーマチュアのオブジェクトの Scale が 15.404（00:01）。読み込み元の単位の都合かは不明（要確認）
- 【画面】タイトルの「UV」に当たる作業は確認できず

## 1. アーマチュア: 揺れ物ボーンの追加（00:01〜02:02）

- 体の骨格はすでにある状態から開始。編集モードで衣装・髪のボーンを足す
- 【画面】確認できたボーン名: string.L_01（パーカーのひも）、belt arm L_01.003（腕のベルト）、back belt L_01 / L_02 / L_01.R（背中のベルト）、cowlick 1（アホ毛）、hair top L 01.005 / 01.015（髪の房）
- 【画面】back belt L_02 の Parent は back belt L_01。Deform オン（00:45）
- 【画面】髪は房ごとに数本ずつ、前髪・横髪・後ろ髪・ポニーテールに配置
- 【画面】02:02 時点のアーマチュア統計: Bones 339、Joints 420（編集モード）
- 命名: 「部位名 + 左右 + 段の番号」。左右は .L / .R または _L / L_01.R などが混在（要確認）
- 【事実】.L / .R を末尾に付けると、Blender の Symmetrize や X 軸ミラーでの対になるボーンの自動判定が効く
- 【事実】揺れ物は根元から先へ親子でつなぐ。VRM の揺れ物設定（Spring Bone）や VRChat の PhysBone も、この親子の鎖を単位に設定する

## 2. 親子付けと自動ウェイト（要確認）

- 【画面】メッシュとアーマチュアをどう親子付けしたか、自動ウェイト（With Automatic Weights）を使ったかは映っていない（要確認）
- 【画面】髪・衣装の各オブジェクトに Armature モディファイアー: Object Armature、Bind To の Vertex Groups オン、Bone Envelopes オフ、Preserve Volume オフ、Multi Modifier オフ（08:07、14:03）
- 【事実】Ctrl+P > With Automatic Weights はボーンからの距離でウェイトのたたき台を作る。With Empty Groups は名前だけの空グループを作る
- 【画面】体の頂点グループ名はボーン名と一致: hips、chest、shoulder.L / .R、upper_arm.L、hand.L / .R、thumb.proximal.L / .R、thumb.intermediate.L、middle.intermediate.L / .R、middle.distal.L / .R、ring.proximal.L、little.distal.L / .R、foot.L / .R、toes.L / .R、neck、head、eye.L
- 【事実】これらの名前は VRM Add-on for Blender が作るヒューマノイドのボーン名と同系統（要確認）

## 3. ウェイト調整: 体（02:22〜04:09）

- ウェイトペイントでボーン 1 本ずつ頂点グループを選んで塗る
- 【画面】ブラシ: Weight 1.000、Strength 1.000、Radius 52〜62 px
- 【画面】ツールバー: Draw、Blur、Average、Smear、Gradient、Sample Weight
- 【画面】体のメッシュ: Vertices 18,558、Faces 18,420
- 【画面】上腕 → 手のひら → 指の各関節 → 足・つま先の順に確認
- 【画面】04:09 頃にポーズモードへ切り替え、Pose Position を確認
- 【話者】（画面上の行動から）指は関節 1 つずつ、境目にグラデーションを残して塗る

## 4. ウェイト調整: 髪・衣装（07:20〜17:46）

- 【画面】前髪横の頂点グループ: Side face L 1.L / 1.R、Side face L 2.L / 2.R、Side face L 3.L …（07:20）
- 【画面】前髪: head、front bang L 1.L / 1.R、front bang L 2.L / 2.R …（17:46）
- 【画面】ポニーテール: hair bottom L 1.005.R〜1.009.L など（16:47）
- 【画面】房のウェイトはボーン 1 本分ずつの帯状。隣の帯と少し重ねる
- 【画面】ブラシ Radius 20〜39 px、Weight 1.000、Strength 1.000
- 【画面】髪全体: Vertices 79,368（前髪横の編集時）
- 注意: この区間はフレームが 7 枚のみ。衣装のウェイトの詳細は未確認

## 5. シェイプキー: 目（18:22〜20:10）

- 【画面】体（body.001）のシェイプキー: Basis、eyeBlinkLeft、eyeBlinkRight、eyeWideLeft、eyeWideRight
- 作り方: キーを選び、値を 1 付近に上げた状態でスカルプトする
  - 【画面】eyeBlinkLeft 0.904 で Smooth（Strength 0.493、Radius 53 px）、0.429 で Grab（Strength 0.400、Radius 53 px）
- 【事実】スカルプトや編集モードの変形は、アクティブなシェイプキーにだけ記録される
- 片側だけのキーの作り方（20:10）:
  - 【画面】新規キー Key 56 を作り、編集モードで片側の頂点を選んで Vertex > Blend from Shape、Shape: Basis、Blend 1.000
  - 【事実】Blend from Shape は選んだ頂点だけを指定キーの形に寄せる。Basis に 1.000 で寄せると、選んだ側だけ元に戻る
  - 両側を動かした形から左右別のキーを作る用途と思われる（要確認）

## 6. シェイプキー: 口・ほお・鼻（20:45〜24:19）

- 編集モードでプロポーショナル編集を使って動かす
  - 【画面】jawOpen: Proportional Falloff Smooth、Proportional Size 0.218、Connected オン（21:25）
  - 【画面】cheekSquintLeft: Proportional Falloff Smooth、Size 0.135、Connected オン（23:37）
- 【画面】確認できたキー名（ARKit の 52 種と同名）: eyeBlinkLeft / Right、eyeWideLeft / Right、eyeSquintLeft / Right、eyeLookDownRight、eyeLookOutLeft / Right、eyeLookInLeft / Right、browDownLeft / Right、browInnerUp、browOuterUpLeft / Right、jawOpen、jawLeft、jawRight、jawForward、mouthClose、mouthFunnel、mouthPucker、mouthLeft、mouthRight、mouthShrugLower / Upper、mouthRollLower / Upper、mouthSmileLeft / Right、mouthFrownLeft / Right、mouthStretchLeft / Right、mouthLowerDownLeft / Right、mouthUpperUpLeft / Right、cheekSquintLeft / Right、cheekPuff、tongueOut、noseSneerLeft / Right
- 【画面】ARKit 系の後ろに Key、happy、sad、shock を追加（24:19）
- 【事実】ARKit の 52 ブレンドシェイプと同じ名前にしておくと、iPhone の顔トラッキング（iFacialMocap や VSeeFace の連携など）で値をそのまま当てられる。VTuber の界隈では「パーフェクトシンク」と呼ばれる
- 命名の規則: 大文字小文字は ARKit の表記どおり（小文字始まりのキャメルケース、左右は末尾の Left / Right）。1 文字違うとトラッキング側で拾われない

## 7. VRM / VRChat 向けの前提

- 【画面】タグに vrchat、vseeface。サイドバーに VRM タブがあるが、VRM の書き出しや Spring Bone の設定場面は映っていない（要確認）
- 【事実】VRM では、ボーンを Humanoid の各部位に割り当て、揺れ物は Spring Bone で、表情は Expression（VRM 0.x では BlendShape）でシェイプキーに対応付ける
- 【事実】VRChat 向けは Unity で PhysBone と表情の設定を行う。ボーン名やシェイプキー名をそろえておくと、どちらの設定でも探しやすい
- 【話者】（画面上の行動から）Blender 側では「ボーン・頂点グループ・シェイプキーの名前をそろえる」までを行い、設定はツール側に任せる流れ

## 落とし穴・チェック項目

- 揺れ物の鎖は親子を確認する（Relations の Parent）。途中が切れていると揺れ物設定で 1 本として扱えない
- 頂点グループ名はボーン名と完全に一致させる。一致しないグループは変形に使われない
- 房や布のウェイトは帯を少し重ね、0 と 1 の急な切り替わりを作らない
- シェイプキーを編集するときは、アクティブなキーが正しいかを必ず確認する（Basis を編集すると全キーに影響する）
- 片側キーは Blend from Shape で作ると左右の形がずれない
- ARKit 名は綴りと大文字小文字を一覧と照合する

## Blender Fes Extra「3D衣装の作り方」との違い

- **ウェイトの付け方。** Extra の講座は、衣装とアーマチュアを「空のグループで」親子付けし、自動ウェイトは使わない。素体のウェイトをデータ転送モディファイアーで衣装へ移し、yuzuWeightEditor の表で数値を入れて調整する。本動画はウェイトペイントのブラシで、ボーン 1 本ずつ直接塗っている（親子付けの方法は映っていない）
- **仕上げの処理。** Extra は「クリーン」「合計を制限（4）」「すべてを正規化」で Unity 向けに整える。本動画ではこれらの操作は確認できなかった
- **揺れ物ボーン。** Extra は親ボーンから押し出し、ウェイトのないエンドボーンで締める。本動画も親子の鎖で作るが、エンドボーンの有無は確認できなかった（要確認）
- **扱う範囲。** Extra は衣装 1 着のウェイトまでで、シェイプキーは扱わない。本動画はアバター本体の表情として、ARKit 系のシェイプキーまで作る
- **最終的な確認場所。** Extra は Unity と lilToon で見た目と動きを確かめる。本動画は Blender の中で終わっており、書き出し先での確認は映っていない
