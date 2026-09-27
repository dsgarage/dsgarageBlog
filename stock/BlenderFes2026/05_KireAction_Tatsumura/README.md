# 「キレ」を意識した2Dアニメ的アクション演出

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | 「キレ」を意識した2Dアニメ的アクション演出 |
| イベント | Blender Fes 2026 AW Day1 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月26日（土）15:00–16:00 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=16 |
| アーカイブ | Vimeo event 6144090（配信内 18599〜22799 秒 = 壁時計 14:55:00–16:05:00） |

## 概要（チャンネルページより）

3DCGでキャラクターを動かすと、普通は「滑らかで立体的な動き」になりやすいです。 しかし、2Dアニメらしい動きは必ずしも滑らかではありません。 あえて動きを飛ばす、形を崩す、タイミングを極端にする、カメラから見たシルエットを優先する。 こうした「正確な3D」から意図的に外れる操作によって、2Dアニメ特有の気持ちよさを3DCGでも再現できます。 ここではBlenderを使って、3Dキャラクターを2Dアニメのように動かすための考え方を解説します。

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | 龍村某 | — | CGアーティスト |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は選別・加工して `images/` に置く（作成済み。当初の frame_0013〜0015 から 5 枚、再抽出の `source/images_t015/` から 5 枚。15:15〜15:36 は再抽出でもフレームなし）
  - `bf2026_s05_limited_demo_character.jpg` — frame_0013 のコピー（15:01:17、デモのキャラクターと縦にそろったキー）
  - `bf2026_s05_dopesheet_aligned_keys.jpg` — frame_0013 のドープシート部分を切り出し（15:01:17）
  - `bf2026_s05_camera_view_before_effect.jpg` — frame_0014 のコピー（15:37:33、エフェクト前のカメラビュー）
  - `bf2026_s05_slash_effect_camera_empty.jpg` — frame_0015 のコピー（15:38:33、斬撃エフェクトとカメラ用エンプティのキー）
  - `bf2026_s05_slash_effect_closeup.jpg` — frame_0015 のカメラ枠を切り出し 3 倍に拡大（15:38:33）
  - `bf2026_s05_ball_shapekey_keys.jpg` — images_t015/frame_0017 のコピー（15:07:09、ボールのシェイプキーとキー間隔）
  - `bf2026_s05_sword_plane_uneven_keys.jpg` — images_t015/frame_0019 のコピー（15:12:48、剣に見立てた板と緩急のあるキー）
  - `bf2026_s05_camera_shake_empty.jpg` — images_t015/frame_0025 のコピー（15:41:00、カメラ制御用エンプティ）
  - `bf2026_s05_skirt_bones_offset_keys.jpg` — images_t015/frame_0026 のコピー（15:43:36、揺れ物ボーンのずらしたキー）
  - `bf2026_s05_demo_broken_pose.jpg` — images_t015/frame_0030 のコピー（15:46:03、締めのデモ）

## 成果物

- 記事: `20260926_BlenderFes2026AW_S05_キレのアクション演出.md`
- 要約: `summary.md`
- Skill 化メモ: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260926_S5_KireAction_Tatsumura.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- `source/transcripts/S5.srt` / `S5.vtt` / `S5.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成。記事は `source/transcripts/20260926_S5_KireAction_Tatsumura.txt` の 15:00〜15:47 を参照して執筆）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
