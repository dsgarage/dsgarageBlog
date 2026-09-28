# フォトリアルCGテクニック

## セッション情報

| 項目 | 内容 |
|:---|:---|
| タイトル | フォトリアルCGテクニック |
| イベント | Blender Fes 2026 AW Day2 |
| 形式 | オンライン配信（講演） |
| 日時 | 2026年9月27日（日）15:00–16:00 |
| チャンネル | https://cgworld.jp/special/blenderfes/vol7/?channel=channel-950 |
| アーカイブ | Vimeo event 6213809（配信内 18599〜22799 秒 = 壁時計 14:55:00–16:05:00） |

## 概要（チャンネルページより）

このセッションでは3DCGを簡単にフォトリアルに見せる方法を解説します。 フォトリアルCGにはさまざまな手法がありますが、このセッションでは特に「簡単」で汎用的なテクニックをモデリング、テクスチャリング、シェーディング、ライティング、コンポジティングの各工程ごとに解説します。 モデリング：リアルに見せるディテールの加え方や直線的な印象を崩してリアルに見せる方法 テクスチャリング：写真を使った表現や、汚れやデカールを自然に加える方法 シェーディング：環境に合わせた汚し方やノーマルマップやランダム値の活用方法 ライティング：HDRIを使って自然な印象を素早く作る方法 コンポジティング：ノイズや色収差などを使って手軽にCG感を抑える方法 などをご紹介します。 このセッションを通して、初心者の方が比較的簡単な工夫やテクニックの積み重ねで手軽にリアルな仕上がりに近づけられることを実感していただけるようになれば幸いです。

## スピーカー

| # | スピーカー | 所属 | 肩書 |
|:---|:---|:---|:---|
| 1 | イオリ | — | CGアーティスト |

## スライド画像

- `source/images/` にアーカイブ動画から抽出したスライド候補フレーム（`frame_NNNN.jpg`）と `frames.tsv`（切り出し内秒・配信内秒・壁時計）を置く
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/images_t015/` に残置）
  - `bf2026_d2s05_use_photo_as_is_slide.jpg` ← images_t015/frame_0015（15:01:03）
  - `bf2026_d2s05_distant_view_overlay.jpg` ← images_t015/frame_0018（15:02:24）
  - `bf2026_d2s05_model_from_photo_slide.jpg` ← images_t015/frame_0023（15:05:29）
  - `bf2026_d2s05_vending_machine_modeling.jpg` ← images_t015/frame_0030（15:08:39）
  - `bf2026_d2s05_house_photo_texture.jpg` ← images_t015/frame_0039（15:17:09）
  - `bf2026_d2s05_photo_beyond_basecolor_slide.jpg` ← images_t015/frame_0046（15:28:41）
  - `bf2026_d2s05_graffiti_wall.jpg` ← images_t015/frame_0050（15:34:44）
  - `bf2026_d2s05_offscreen_shadow.jpg` ← images_t015/frame_0056（15:40:27）
  - `bf2026_d2s05_compositor_nodes.jpg` ← images_t015/frame_0061（15:45:48）
  - `bf2026_d2s05_summary_slide.jpg` ← images_t015/frame_0062（15:46:29）

## 成果物

- 記事: `20260927_BlenderFes2026AW_S13_フォトリアルCGテクニック.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/20260927_S5_PhotorealCG_Iori.txt` — whisper 文字起こし（Talksribe 形式、壁時計付き）
- `source/transcripts/S5.srt` / `S5.vtt` / `S5.txt` — whisper-cli の元出力（切り出し開始からの相対時刻）
- `transcripts/` — 記事用に整形した transcript（未作成。記事は `source/transcripts/20260927_S5_PhotorealCG_Iori.txt` の本編 15:00:05〜15:47:15 を要約して執筆。以降は CM と待機画面）

`source/` 配下は Git 管理外（主催者方針により録画・録音の再配布は不可）。
