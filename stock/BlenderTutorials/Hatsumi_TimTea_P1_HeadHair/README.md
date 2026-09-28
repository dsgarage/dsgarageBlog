# Making A 3D Vtuber Avatar for TimTea! Part ~1 Head and Hair『Blender』

## 動画情報

| 項目 | 内容 |
|:---|:---|
| タイトル | Making A 3D Vtuber Avatar for TimTea! Part ~1 Head and Hair『Blender』 |
| チャンネル | Hatsumi（https://www.youtube.com/channel/UCrCvqy0qIO278DeVzOrBK0g） |
| 公開日 | 2023年7月23日 |
| URL | https://www.youtube.com/watch?v=tCU6VFnH5Jo |
| 長さ | 26:20 |
| 言語 | 英語（ナレーションなし。BGM と画面テロップのみ） |

## 概要（動画説明文より）

VTuber の TimTea 氏から依頼を受けて制作した 3D モデルの制作過程を記録する連載の第 1 回。頭の基本的なモデリングと髪のモデリング、顔のテクスチャ作成を扱う。全何回になるかは未定と書かれている。依頼（コミッション）の情報は作者の X（旧 Twitter）で案内している。

説明文にタイムスタンプはない。動画冒頭（00:30）の画面タイトルは「W1 Day 1: Head Modeling and Texturing」。

## 作者

| # | 名前 | 情報（info.json の範囲） |
|:---|:---|:---|
| 1 | Hatsumi | YouTube チャンネル Hatsumi。説明文に X（旧 Twitter）アカウント @-hatsumi_17 の記載あり |

## フレーム画像

- `source/images/` に動画から抽出したフレーム（`frame_NNNN.jpg`、130 枚、幅 1280px）と `frames.tsv`（file / 動画内秒）を置く
- 読み取りに使ったのは 30 枚（frame_0001・0003・0006、以降 0010〜0125 を 4〜5 枚おき）
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/images/` に残置）。キャプションに「出典: Hatsumi 氏の動画」を明記
  - `yt_hatsumi_timtea_p1_face_mask.jpg` ← frame_0010（01:51）
  - `yt_hatsumi_timtea_p1_eyes_lashes.jpg` ← frame_0030（05:44）。背景に依頼者のキャラクターデザイン（参照画像）が写る。この動画で使うデザイン入りの 1 枚
  - `yt_hatsumi_timtea_p1_mouth_texture.jpg` ← frame_0066（13:18）
  - `yt_hatsumi_timtea_p1_back_hair_side.jpg` ← frame_0095（19:03）
  - `yt_hatsumi_timtea_p1_hair_shaded.jpg` ← frame_0110（22:13）
- frame_0001（完成モデルの全身）、frame_0003・0014・0038 など（参照画像のキャラクターが大きく写る）は不使用

## 成果物

- 記事: `20230723_YT_Hatsumi_TimTea_P1_頭と髪.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/transcript_en.txt` / `transcript_en.srt` / `transcript_en.vtt` — whisper 文字起こし。ナレーションがないため BGM を誤認識した定型句（「Thank you.」など）しか入っておらず、内容の根拠には使っていない
- `source/transcripts/TimTea_P1_HeadHair.en.vtt` — YouTube 自動字幕（「[Music]」などのみ）
- `source/TimTea_P1_HeadHair.info.json` — 動画メタデータ（タイトル・公開日・説明文）

記事と skill_notes の手順は、フレームの画面表示（ツール名・モディファイアー設定・オペレーターパネルの値・アウトライナー）から読み取った。

`source/` 配下は Git 管理外（個人クリエイターの動画のため、動画・音声・全文文字起こしは再配布しない）。
