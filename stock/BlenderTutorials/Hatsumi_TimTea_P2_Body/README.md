# Making A 3D Vtuber Avatar for TimTea! Part ~2 Body『Blender』

## 動画情報

| 項目 | 内容 |
|:---|:---|
| タイトル | Making A 3D Vtuber Avatar for TimTea! Part ~2 Body『Blender』 |
| チャンネル | Hatsumi（https://www.youtube.com/channel/UCrCvqy0qIO278DeVzOrBK0g） |
| 公開日 | 2023年7月28日 |
| URL | https://www.youtube.com/watch?v=EZ40p2Ia8-0 |
| 長さ | 22:02 |
| 言語 | 英語（ナレーションなし。BGM と画面テロップのみ） |

## 概要（動画説明文より）

VTuber の TimTea 氏から依頼を受けた 3D モデルの制作記録の第 2 回。このキャラクターの体を作る。完璧にしたくて何度も取り消しと修正を繰り返したこと、男性キャラクターのトポロジーにはあまり慣れていないことが書かれている。依頼の情報は作者の X（旧 Twitter）、3D モデルの申し込みは作者のサイト（hatsumimodels.com）で案内している。

説明文にタイムスタンプはない。

## 作者

| # | 名前 | 情報（info.json の範囲） |
|:---|:---|:---|
| 1 | Hatsumi | YouTube チャンネル Hatsumi。説明文に X（旧 Twitter）アカウント @-hatsumi_17 とサイト hatsumimodels.com の記載あり |

## フレーム画像

- `source/images/` に動画から抽出したフレーム（`frame_NNNN.jpg`、63 枚、幅 1280px）と `frames.tsv`（file / 動画内秒）を置く。最初のフレームは 01:07
- 読み取りに使ったのは 30 枚（frame_0001〜0037 は 1 枚おき、以降 0040〜0063 を 2〜3 枚おき）
- 記事で使う画像は `images/` に選別コピー済み（元フレームは `source/images/` に残置）。キャプションに「出典: Hatsumi 氏の動画」を明記
  - `yt_hatsumi_timtea_p2_back_torso.jpg` ← frame_0009（04:35）
  - `yt_hatsumi_timtea_p2_hip_topology.jpg` ← frame_0042（14:29）
  - `yt_hatsumi_timtea_p2_body_front.jpg` ← frame_0046（16:22）。背景に依頼者のキャラクターデザイン（参照画像）が写る。この動画で使うデザイン入りの 1 枚
  - `yt_hatsumi_timtea_p2_finger_merge.jpg` ← frame_0052（18:46）
- frame_0035（作者のアバターが大きく写る）、frame_0001・0007・0013・0050 など（参照画像のキャラクターが大きく写る）、frame_0055〜0061（参照の靴が写る）は不使用

## 成果物

- 記事: `20230728_YT_Hatsumi_TimTea_P2_体.md`（ドラフト）
- 要点: `summary.md`
- Skill 化用ノート: `skill_notes.md`

## トランスクリプトファイル

- `source/transcripts/transcript_en.txt` / `transcript_en.srt` / `transcript_en.vtt` — whisper 文字起こし。ナレーションがないため BGM を誤認識した定型句しか入っておらず、内容の根拠には使っていない
- `source/transcripts/TimTea_P2_Body.en.vtt` — YouTube 自動字幕（「[Music]」などのみ）
- `source/TimTea_P2_Body.info.json` — 動画メタデータ（タイトル・公開日・説明文）

記事と skill_notes の手順は、フレームの画面表示（ツール名・モディファイアー設定・オペレーターパネルの値・アウトライナー・画面テロップ）から読み取った。

`source/` 配下は Git 管理外（個人クリエイターの動画のため、動画・音声・全文文字起こしは再配布しない）。
