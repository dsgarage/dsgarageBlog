# Blenderで作る『超かぐや姫！』のCG背景

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | Blenderで作る『超かぐや姫！』のCG背景 |
| イベント | Blender Fes 2026 AW Day1 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月26日（土）10:00–11:00 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-952 |
| アーカイブ | Vimeo event 6144090（配信内 599〜4799 秒 = 壁時計 09:55:00–11:05:00） |

## 概要（チャンネルページより）

「塗る」「描く」だけが背景じゃない！ アニメ背景屋ならではの3DCGワークフローを、『超かぐや姫！』で実際に使用したモデルを参考に説明します。 ・CG背景の目的は3D背動！ペイントよりもプロシージャルに拘った理由。 ・Blenderは魔法のキャンパス！シェーディングとライティングで表現する色の世界。 ・アニメ背景に流儀無し！ルック実現のために使えるアセットやアドオンは躊躇なく使うべし。 これからアニメ背景を目指す方も、CGを取り入れたいと考えている同業の方も、一度筆を置いて「こういった手法や考え方もあるんだ」と感じていただければ幸いです。

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | 中尾陽子 | 株式会社キューン・プラント | 背景美術デザイナー |
| 2 | 草間徹也 | 株式会社キューン・プラント | 3DCGアーティスト |
| 3 | 高橋舞 | 株式会社キューン・プラント | 3DCGテクニカルアーティスト |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は `images/` に選別コピー済み（10 枚、元の壁時計。最後の 2 枚は `source/images_t015/` から）
  - `bf2026_s01_haido_raw.jpg`（frame_0023, 10:05:58）背動カットの RAW 映像
  - `bf2026_s01_shader_edge_highlight.jpg`（frame_0040, 10:14:54）ツクヨミ建物のシェーダー作業画面
  - `bf2026_s01_gn_meeting_space.jpg`（frame_0042, 10:16:18）ミーティングスペースの超高層建築群
  - `bf2026_s01_kassen_animation.jpg`（frame_0045, 10:17:15）合戦の吹き流し・垂れ幕アニメーション
  - `bf2026_s01_render_passes.jpg`（frame_0051, 10:22:09）レンダリング要素の個別出力
  - `bf2026_s01_addon_physical_starlight.jpg`（frame_0065, 10:28:09）アドオン紹介（Physical Starlight and Atmosphere）
  - `bf2026_s01_spiral_particles.jpg`（frame_0083, 10:40:59）スパイラル柱の粒子エフェクト
  - `bf2026_s01_graduation_stage_lanterns.jpg`（frame_0089, 10:47:21）卒業ライブのステージと灯籠
  - `bf2026_s01_spiral_gn_nodes.jpg`（images_t015/frame_0116, 10:45:12）スパイラル柱のモディファイアスタックとパーティクルのノード
  - `bf2026_s01_lantern_gn_overview.jpg`（images_t015/frame_0129, 10:49:00）灯籠のノード全体像と配置点

## 成果物

- 記事: `20260926_BlenderFes2026AW_S01_超かぐや姫のCG背景.md`（ドラフト）
- 要点: `summary.md`
- Skill 化メモ: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260926_S1_KaguyahimeCGBackground_QoonPlant.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- `source/transcripts/S1.srt` / `S1.vtt` / `S1.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成。本編は 10:01〜10:54、以降は CM・次枠待ち）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
