# 絵作りと効率を両立させる、キャラクターシェーディング技法 自主制作アニメ映画『砂塵ノ中デ』メイキング

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | 絵作りと効率を両立させる、キャラクターシェーディング技法 自主制作アニメ映画『砂塵ノ中デ』メイキング |
| イベント | Blender Fes 2026 AW Day1 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月26日（土）11:15–12:15 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-946 |
| アーカイブ | Vimeo event 6144090（配信内 5099〜9899 秒 = 壁時計 11:10:00–12:30:00（延長版）） |

## 概要（チャンネルページより）

・クオリティと作業量のバランスを取りながら、数百カットのビジュアルをコントロールする考え方 ・よりイラスト的で魅力的なビジュアルを作るための、キャラシェーディング技法 ・GeometoryNpdesとShaderNodesを活用した、効率とビジュアルを両立させるキャラシェーディング技法 ・レタッチに頼らずに、仕組みで絵作りをする為のシェーダーワークフロー ・リムライトやSDFテクスチャを活用したフェイスシェーディングなど、より絵的なキャラクターライティングのセットアップ技法

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | 三縁エイト | — | 映像監督、Blenderアーティスト |
| MC | 藤田将 | — | 司会 |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は選別して `images/` にコピー済み（元フレームは `source/images/` に残す）
  - `bf2026_s02_workload_200cuts.jpg` — frame_0022（11:21:25）作業量の掛け算スライド
  - `bf2026_s02_system_reproduction.jpg` — frame_0024（11:24:00）感覚をシステムで再現
  - `bf2026_s02_game_shader_x_illust_look.jpg` — frame_0027（11:28:35）ゲームシェーダー × イラスト調ルック
  - `bf2026_s02_goo_engine.jpg` — frame_0031（11:33:23）Goo Engine 紹介
  - `bf2026_s02_fresnel_vs_scene_rim.jpg` — frame_0032（11:34:30）Fresnel と Scene Rim の比較
  - `bf2026_s02_shader_info.jpg` — frame_0033（11:36:45）Shader Info の比較
  - `bf2026_s02_face_sdf_comparison.jpg` — frame_0039（12:06:58）SDF 4方式の比較
  - `bf2026_s02_hair_normal_editing.jpg` — frame_0043（12:13:30）髪の法線編集 4方式
  - `bf2026_s02_paint_over_line.jpg` — images_t015/frame_0033（11:30:36）3DCG特性の再検討
  - `bf2026_s02_face_modifier_stack.jpg` — images_t015/frame_0050（12:19:30）顔オブジェクトのモディファイアスタック

## 成果物

- 記事: `20260926_BlenderFes2026AW_S02_キャラクターシェーディング技法.md`（ドラフト）
- 要点: `summary.md`
- スキル化メモ: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260926_S2_CharacterShading_Eight.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）。本編は 11:15:00〜12:24:31（締めの挨拶まで）。12:24:34 以降は告知・CM。記事はこれを要約して作成
- `source/transcripts/S2.srt` / `S2.vtt` / `S2.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
