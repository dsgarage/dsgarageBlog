# VRChatワールドができるまで

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | VRChatワールドができるまで |
| イベント | Blender Fes 2026 AW Day2 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月27日（日）16:15–17:15 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-991 |
| アーカイブ | Vimeo event 6213809（配信内 23099〜27299 秒 = 壁時計 16:10:00–17:20:00） |

## 概要（チャンネルページより）

VRChatワールド制作を題材に、Blenderで空間をつくり、Unityへ持ち込み、VRChat上で確認・調整するまでの基本的なワークフローを、自身の制作例を交えて紹介します。 Blenderでは、リファレンスの集め方、人体や実寸を基準にしたラフモデル、VRでのスケール確認、詳細モデリング、UV・テクスチャ制作などを扱います。Unityでは、Blenderからのデータ移行、Material・Texture・Shaderの設定、Lighting、Light Probe・Reflection Probe、Post Processing、VRChatでの動作確認までを取り上げます。 Blenderでモデリングはできるものの、その前後に何をすればVRChatワールドとして完成するのか、その制作工程全体を把握できる内容です。

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | Fujito | — | 空間デザイナー / XRクリエイター |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/images_t015/` に残置）
  - `bf2026_d2s06_world_pool_terrace.jpg` ← images_t015/frame_0020（16:18:07）
  - `bf2026_d2s06_bubble_diagram_plan_sketch.jpg` ← images_t015/frame_0023（16:20:31）
  - `bf2026_d2s06_reference_structuring_section.jpg` ← images_t015/frame_0025（16:22:57）
  - `bf2026_d2s06_workflow_two_phases.jpg` ← images_t015/frame_0026（16:23:57）
  - `bf2026_d2s06_rough_model_human_scale.jpg` ← images_t015/frame_0030（16:25:27）
  - `bf2026_d2s06_unity_vcc_setup.jpg` ← images_t015/frame_0034（16:27:24）
  - `bf2026_d2s06_unity_material_shader_texture.jpg` ← images_t015/frame_0059（16:48:49）
  - `bf2026_d2s06_lightmapping_settings.jpg` ← images_t015/frame_0062（16:53:11）
  - `bf2026_d2s06_post_processing_compare.jpg` ← images_t015/frame_0063（16:56:08）
  - `bf2026_d2s06_in_world_third_person.jpg` ← images_t015/frame_0074（17:08:18）

## 成果物

- 記事: `20260927_BlenderFes2026AW_S14_VRChatワールドができるまで.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260927_S6_VRChatWorld_Fujito.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- `source/transcripts/S6.srt` / `S6.vtt` / `S6.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成。記事は `source/transcripts/20260927_S6_VRChatWorld_Fujito.txt` の本編 16:14:59〜17:09:45 を要約して執筆）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
