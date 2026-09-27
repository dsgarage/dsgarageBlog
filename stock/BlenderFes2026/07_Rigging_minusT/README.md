# ボーン1本から始めるキャラクターリギング

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | ボーン1本から始めるキャラクターリギング |
| イベント | Blender Fes 2026 AW Day1 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月26日（土）17:30–18:30 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-943 |
| アーカイブ | Vimeo event 6144090（配信内 27599〜31799 秒 = 壁時計 17:25:00–18:35:00） |

## 概要（チャンネルページより）

本セッションでは、リギングの基礎からキャラクターリギングまで紹介します。 リギングの基礎では、ボーンの操作と作成方法からウェイト設定まで、リギング作業の流れを解説します。 効率的な頂点とボーンの位置設定、Boneの回転(Euler、Quaternion)の違いなど、数学的原理を簡単に解説します。 キャラクターリギングではadd-onを使わずに一つのボーンから直接設定する工程の流れを解説します。 ・人体のボーンの作成 ・Weight Painting ・腕と脚のIK設定 ・Shape KeyとDriverを使った表情のリギング また、blenderの機能を応用した効率的なリギングを紹介します。 ・ 色々なConstraintsを活用した指と足のコントローラー ・ Deform Modifierを使った効率的な衣装リギング ・ 衣装のオブジェクトをvertex parentingで簡単にリギング などのポイントを紹介する予定です。

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | minusT | 記載なし（公式タイムテーブル） | 3DCGアーティスト |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は `source/images_t015/` から選別コピーして `images/` に置いた（10 枚）
  - `bf2026_s07_rig_overview.jpg`（17:30:32）/ `bf2026_s07_basics_skinning.jpg`（17:36:36）/ `bf2026_s07_spine_torso_bones.jpg`（17:40:18）/ `bf2026_s07_hand_finger_bones.jpg`（17:43:15）/ `bf2026_s07_weight_paint_hips.jpg`（17:50:15）
  - `bf2026_s07_finger_bend_euler.jpg`（17:52:45）/ `bf2026_s07_finger_copy_rotation.jpg`（18:03:12）/ `bf2026_s07_driver_generator.jpg`（18:15:09）/ `bf2026_s07_ik_controls_collections.jpg`（18:21:00）/ `bf2026_s07_skirt_bones.jpg`（18:27:12）

## 成果物

- 記事: `20260926_BlenderFes2026AW_S07_ボーン1本から始めるリギング.md`（講演は韓国語、記事は日本語で要約）
- 要点: `summary.md` / Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260926_S7_Rigging_minusT.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- 文字起こしは韓国語(`-l ko`)。日本語強制版は S7.origja.* として ~/Downloads 側に保存
- `source/transcripts/S7.srt` / `S7.vtt` / `S7.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
