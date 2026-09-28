# Blenderから広がる、体験型リアルタイムコンテンツのつくり方

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | Blenderから広がる、体験型リアルタイムコンテンツのつくり方 |
| イベント | Blender Fes 2026 AW Day2 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月27日（日）12:30–13:30 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-951 |
| アーカイブ | Vimeo event 6213809（配信内 9599〜13799 秒 = 壁時計 12:25:00–13:35:00） |

## 概要（チャンネルページより）

Blenderを起点に、Geometry Nodesを活用したアセット制作や、ゲームエンジンをはじめとするリアルタイムツールへのデータ連携、リアルタイム描画を前提としたアニメーションの設計など、体験型リアルタイムコンテンツ制作の具体的なワークフローを紹介します。実際の制作事例をもとに、3DCG制作からリアルタイム環境への実装、ディスプレイや空間条件に合わせた演出設計まで、各工程での考え方や実践的なポイントを解説します。 また、複数のツールを横断するコンテンツ制作において、なぜBlenderを制作のベースとして採用しているのか。制作効率やツール間のデータ連携、試行錯誤のしやすさ、表現の自由度といった観点から、リアルタイムコンテンツ制作にBlenderを活用するメリットや考え方を紹介します。

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | 中野雄太 | raw inc. | Co-Founder \| Creative Director |
| 2 | 寺澤佑希斗 | raw inc. | CG Designer |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/` に残置）
  - `bf2026_d2s03_newoman_works_overview.jpg` ← frame_0016（12:36:31）
  - `bf2026_d2s03_imagination_ocean_led.jpg` ← images_t015/frame_0021（12:37:21）
  - `bf2026_d2s03_garden_horizontal_led.jpg` ← frame_0024（12:42:03）
  - `bf2026_d2s03_interactive_definition.jpg` ← frame_0027（12:46:37）
  - `bf2026_d2s03_three_render_methods.jpg` ← frame_0028（12:47:19）
  - `bf2026_d2s03_flower_alpha_assets.jpg` ← frame_0030（12:57:35）
  - `bf2026_d2s03_why_video_assets.jpg` ← frame_0031（12:57:56）
  - `bf2026_d2s03_state_transition_slide.jpg` ← frame_0032（13:01:24）
  - `bf2026_d2s03_gn_plant_parameters.jpg` ← frame_0035（13:15:39）
  - `bf2026_d2s03_three_takeaways.jpg` ← frame_0038（13:20:15）

## 成果物

- 記事: `20260927_BlenderFes2026AW_S11_体験型リアルタイムコンテンツ.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260927_S3_RealtimeContent_raw.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- `source/transcripts/S3.srt` / `S3.vtt` / `S3.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成。記事は `source/transcripts/20260927_S3_RealtimeContent_raw.txt` の本編 12:30:08〜13:21:15 を要約して執筆。12:36:53〜12:39:53 は記録映像でナレーションなし）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
