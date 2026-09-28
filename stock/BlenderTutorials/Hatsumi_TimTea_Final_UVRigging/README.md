# Making A 3D Vtuber Avatar for TimTea! Final Part UV Rigging/Blendshapes『Blender』

## 動画情報

| 項目 | 内容 |
|:---|:---|
| タイトル | Making A 3D Vtuber Avatar for TimTea! Final Part UV Rigging/Blendshapes『Blender』 |
| チャンネル | Hatsumi（https://www.youtube.com/channel/UCrCvqy0qIO278DeVzOrBK0g） |
| 公開日 | 2024年9月19日 |
| URL | https://www.youtube.com/watch?v=AF4N-22qkkU |
| 長さ | 24:44 |
| 言語 | 英語（冒頭テロップのみ。ナレーションなし、BGM のみ） |

## 概要（動画説明文より）

VTuber の TimTea さんから依頼された 3D モデルの制作過程を記録する連載の最終回。本編はアーマチュアの調整（衣装・髪の揺れ物ボーン）、ウェイトペイント、表情用シェイプキー（ARKit 系の名前）。タイトルに「UV」とあるが UV 編集の場面は確認できず、説明文の「靴を作る」も本編と合わない（いずれも前回からの名残と思われる、要確認）。説明文にチャプターはない。

## 作者

| # | 名前 | 情報（info.json の範囲） |
|:---|:---|:---|
| 1 | Hatsumi | YouTube チャンネル Hatsumi。説明文に X（@-hatsumi_17 と表記）と VGen（https://vgen.co/hatsumi）の案内あり |

## フレーム画像

- `source/images/` に動画から抽出したフレーム（`frame_NNNN.jpg`、61 枚、1280x720、最後は 24:29）と `frames.tsv`（file / 動画内秒）を置く。うち 30 枚を確認した
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/images/` に残置）。キャプションに「出典: Hatsumi 氏の動画」を明記
  - `yt_hatsumi_timtea_final_accessory_bones.jpg` ← frame_0004（00:34）
  - `yt_hatsumi_timtea_final_weight_upper_arm.jpg` ← frame_0013（02:22）
  - `yt_hatsumi_timtea_final_weight_ponytail.jpg` ← frame_0024（16:47）
  - `yt_hatsumi_timtea_final_shapekey_eyeblink.jpg` ← frame_0028（18:22）
  - `yt_hatsumi_timtea_final_shapekey_mouth_list.jpg` ← frame_0052（22:53）
- frame_0022 は暗転。frame_0012・0034・0044・0048・0060 などはアバターの顔が大きく写るため不使用
- 07:20〜17:46 の区間はフレームが 7 枚しかなく、この区間の手順は大まかにしか読み取れていない

## 成果物

- 記事: `20240919_YT_Hatsumi_TimTeaFinal_リギングとブレンドシェイプ.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/transcript_en.txt` / `.srt` / `.vtt` — whisper の出力。ナレーションがないため、中身は BGM に対する誤認識（「Thank you.」の繰り返しなど）で、内容の根拠には使っていない
- YouTube 自動字幕（`<name>.en.vtt`）は取得されていない（音声が BGM のみのため）
- `source/TimTea_Final_UVRigging.info.json` — 動画メタデータ（タイトル・公開日・説明文・タグ）

記事と skill_notes の内容は、すべて画面（フレーム）から読み取ったもの。`source/` 配下は Git 管理外（個人クリエイターの動画のため、動画・音声・全文文字起こしは再配布しない）。
