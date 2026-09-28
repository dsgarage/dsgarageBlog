# ノードで楽しむモーショングラフィックス表現

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | ノードで楽しむモーショングラフィックス表現 |
| イベント | Blender Fes 2026 AW Day2 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月27日（日）11:15–12:15 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-947 |
| アーカイブ | Vimeo event 6213809（配信内 5099〜9299 秒 = 壁時計 11:10:00–12:20:00） |

## 概要（チャンネルページより）

Blender1年目、モーショングラフィックデザイナーのNodes活用方法と制作アプローチをご紹介いたします。 このセッションでは、私がBlenderを学び、作業に活用しながら身につけた方法を基に、Geometry Nodesをより簡単に理解し、望む表現を実現するためにどのようにアプローチするかをお話しします。 代表作やさまざまな習作の実際のノード構成を見ながら、Geometry Nodesだけでなく、Compositor Nodes、Shader Nodesなど、Blenderの多様なNode Systemをモーショングラフィック制作にどのように活用できるかをご紹介いたします。 【セッションに含まれる内容】 １.モーショングラフィックデザイナーがBlenderのNodesを見る方法 これまで行ってきたモーショングラフィックの作業や制作方法が、Blenderを学ぶ過程にどのような影響を与えたのか、そして望む結果を作るために必要な機能を見つけて組み合わせる、私独自の制作アプローチをご紹介いたします。 2.Geometry Nodes制作アプローチ 複雑に見えるGeometry Nodesを理解し活用するために、どのように問題を分割し、必要なノードを見つけ、さまざまなNode Systemへと発展させていったか、その方法を説明いたします。 3.代表作および習作を通じたNodes活用事例 3-1. [Geometry Nodes] 最初の習作のアプローチ方法 3-2. DJMAX「Dreamscape by GhostFinal」MVのノード使用解説 3-3. [Geometry Nodes] Raycastを活用した2D Tracking Interface制作解説 3-4. [Compositor Nodes] Object Tracking Interface制作解説 4.モーショングラフィック表現に活用しやすいNodesの用例紹介 最近実験している作業をもとに、セッションを視聴される皆様にぜひ使っていただきたいNodesの活用法をまとめました。 [Geometry Nodes] Math Nodesを活用したアニメーション自動化システム [Geometry Nodes] Cloth Simulation Nodeを活用したテキストアニメーション表現 [Shader Nodes] Raycastを活用したグラフィックデザイン表現

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | cerbalance | — | Motion Designer / Supervisor |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は `source/images_t015/` から選別コピーして `images/` に置いた（10 枚）
  - `bf2026_d2s02_approach_decompose.jpg`（11:18:36）/ `bf2026_d2s02_world_star_voronoi.jpg`（11:33:48）/ `bf2026_d2s02_pillar_dot_product.jpg`（11:35:45）/ `bf2026_d2s02_shader_raycast_fog.jpg`（11:39:48）/ `bf2026_d2s02_cell_fracture.jpg`（11:42:30）
  - `bf2026_d2s02_trail_2d_convert.jpg`（11:45:45）/ `bf2026_d2s02_trail_ndc_tracking.jpg`（11:47:54）/ `bf2026_d2s02_compositor_pixelize_uv.jpg`（11:52:42）/ `bf2026_d2s02_motion_fraction_bpm.jpg`（12:00:36）/ `bf2026_d2s02_shader_raycast_halftone.jpg`（12:08:03）

## 成果物

- 記事: `20260927_BlenderFes2026AW_S10_ノードで楽しむモーショングラフィックス.md`（講演は韓国語、記事は日本語で要約）
- 要点: `summary.md` / Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260927_S2_MotionGraphicsNodes_cerbalance.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- 文字起こしは韓国語(`-l ko`)。本編は 11:15:10〜12:08:46 で切り出し範囲内に完結
- `source/transcripts/S2.srt` / `S2.vtt` / `S2.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
