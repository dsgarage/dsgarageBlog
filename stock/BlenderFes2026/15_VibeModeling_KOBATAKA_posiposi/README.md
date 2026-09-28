# AIエージェント × Blenderで挑む Vibe Modeling

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | AIエージェント × Blenderで挑む Vibe Modeling |
| イベント | Blender Fes 2026 AW Day2 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月27日（日）17:30–18:30 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-942 |
| アーカイブ | Vimeo event 6213809（配信内 27599〜32399 秒 = 壁時計 17:25:00–18:45:00。予定を超過したため 4800 秒に延長） |

## 概要（チャンネルページより）

AIエージェントと会話しながら、コードでBlenderを動かしてモデリングを進めていく”Vibe Modeling”。自分でポリゴンを触らないのに、本当に作品は作れるのか？ そして、それはどこまで通用するのか？ 本セッションでは、Blender初心者の状態からこの手法で作品づくりに挑んできたKOBATAKA氏とposi_posi氏が、実際に作り上げた作例を交えながらその実態を語ります。 「AIが得意な形状と、意外なほど苦手な形状」の境界線。その線引きがわかると、他の3D生成AIとの使い分けや、Vibe Modelingを活かすべき場面がはっきり見えてきます。さらに、AIに「ポン出し」させるだけでは絶対にたどり着けないクオリティに到達するための、お二人ならではのイテレーションの回し方や、手作業では面倒な工程をコードならではの発想で一気に片づけるテクニックなど、実践者だからこそ語れる知見が次々と登場します。 そして「人間が握るべき工程」と「AIに任せていい工程」は結局どこで分かれるのか。 AI×Blenderに興味はあるけれど何から始めればいいかわからない方、すでに試してみたものの伸び悩んでいる方、どちらにも「次の一歩」が見つかるはずです。

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | KOBATAKA | — | — |
| 2 | posi_posi | — | — |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- `source/images_t015/` に閾値 0.15 の抽出フレーム（延長分まで含む 91 枚）を置く
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/images_t015/` に残置）
  - `bf2026_d2s07_works_without_blender.jpg` ← images_t015/frame_0019（17:34:22）
  - `bf2026_d2s07_strong_weak_12shapes.jpg` ← images_t015/frame_0028（17:38:55）
  - `bf2026_d2s07_tree_11rounds.jpg` ← images_t015/frame_0032（17:43:55）
  - `bf2026_d2s07_hand_and_eye_overview.jpg` ← images_t015/frame_0041（17:58:08）
  - `bf2026_d2s07_llm_wiki_library.jpg` ← images_t015/frame_0042（18:00:39）
  - `bf2026_d2s07_mug_decision.jpg` ← images_t015/frame_0048（18:10:33）
  - `bf2026_d2s07_human_ai_process_map.jpg` ← images_t015/frame_0053（18:14:24）
  - `bf2026_d2s07_fox_blocking.jpg` ← images_t015/frame_0059（18:18:14）
  - `bf2026_d2s07_fixed_camera_time_study.jpg` ← images_t015/frame_0061（18:22:33）
  - `bf2026_d2s07_gn_api_pitfall.jpg` ← images_t015/frame_0065（18:26:14）

## 成果物

- 記事: `20260927_BlenderFes2026AW_S15_AIエージェントとVibeModeling.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260927_S7_VibeModeling_KOBATAKA_posiposi.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- `source/transcripts/S7.srt` / `S7.vtt` / `S7.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成。記事は `source/transcripts/20260927_S7_VibeModeling_KOBATAKA_posiposi.txt` の本編 17:30:00〜18:42 頃を要約して執筆。事前収録で、収録日は講演中の発言で 9/5）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
