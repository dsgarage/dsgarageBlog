# Making A 3D Vtuber Avatar for TimTea! Part 5 UV Mapping/Texturing『Blender』

## 動画情報

| 項目 | 内容 |
|:---|:---|
| タイトル | Making A 3D Vtuber Avatar for TimTea! Part 5 UV Mapping/Texturing『Blender』 |
| チャンネル | Hatsumi（https://www.youtube.com/channel/UCrCvqy0qIO278DeVzOrBK0g） |
| 公開日 | 2024年5月14日 |
| URL | https://www.youtube.com/watch?v=fMM4BXnj9xM |
| 長さ | 37:34 |
| 言語 | 英語（冒頭テロップのみ。ナレーションなし、BGM のみ） |

## 概要（動画説明文より）

VTuber の TimTea さんから依頼された 3D モデルの制作過程を記録する連載の第 5 回。本編は UV 展開とテクスチャペイント（体・衣装・靴・髪）。説明文には「この動画ではキャラクターの靴を作る」とあるが、本編と合わないため前回の説明文の名残と思われる（要確認）。説明文にチャプターはない。

## 作者

| # | 名前 | 情報（info.json の範囲） |
|:---|:---|:---|
| 1 | Hatsumi | YouTube チャンネル Hatsumi。説明文に X（@-hatsumi_17 と表記）と VGen（https://vgen.co/hatsumi）の案内あり |

## フレーム画像

- `source/images/` に動画から抽出したフレーム（`frame_NNNN.jpg`、118 枚、1280x720）と `frames.tsv`（file / 動画内秒）を置く。うち 30 枚を確認した
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/images/` に残置）。キャプションに「出典: Hatsumi 氏の動画」を明記
  - `yt_hatsumi_timtea_p5_body_uv_islands.jpg` ← frame_0005（01:34）
  - `yt_hatsumi_timtea_p5_clothing_uv_squares.jpg` ← frame_0011（05:41）
  - `yt_hatsumi_timtea_p5_ucupaint_layers.jpg` ← frame_0035（16:42）
  - `yt_hatsumi_timtea_p5_shadow_soften.jpg` ← frame_0055（20:51）
  - `yt_hatsumi_timtea_p5_shoes_unwrap.jpg` ← frame_0080（27:45）
- frame_0075 は暗転。frame_0029・0101・0108・0116 などはアバターの顔が大きく写るため不使用

## 成果物

- 記事: `20240514_YT_Hatsumi_TimTeaP5_UV展開とテクスチャ.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/transcript_en.txt` / `.srt` / `.vtt` — whisper の出力。ナレーションがないため、中身は BGM に対する誤認識（「Thank you.」の繰り返しなど）で、内容の根拠には使っていない
- YouTube 自動字幕（`<name>.en.vtt`）は取得されていない（音声が BGM のみのため）
- `source/TimTea_P5_UVTexturing.info.json` — 動画メタデータ（タイトル・公開日・説明文・タグ）

記事と skill_notes の内容は、すべて画面（フレーム）から読み取ったもの。`source/` 配下は Git 管理外（個人クリエイターの動画のため、動画・音声・全文文字起こしは再配布しない）。
