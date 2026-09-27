# Blender Fes 2026 AW Stock

CGWORLD 主催のオンラインイベント「Blender Fes 2026 AW」の素材置き場。構成は `stock/GDC2026/` と同じ規約に従う（[GDC2026/README.md](../GDC2026/README.md)）。

## イベント概要

| 項目 | 内容 |
|:---|:---|
| イベント | Blender Fes 2026 AW（Blender Fes vol.7） |
| 開催日 | 2026年9月26日（土）Day1 / 9月27日（日）Day2 |
| 形式 | オンライン配信（チャンネル制） |
| 公式ページ | https://cgworld.jp/special/blenderfes/vol7/ |
| アーカイブ | Vimeo event 6144090（Day1、長さ 36901 秒） |
| GitHub Issue | dsgarage/dsgarageBlog#2 |

### 配信内時刻と壁時計の対応

- アーカイブの `info.json` の timestamp は 2026-09-26 20:00:02（配信終了時刻）、duration は 36901 秒
- 配信開始 = 20:00:02 − 36901 秒 = **2026-09-26 09:45:01**
- 配信内 t 秒 → 壁時計 = 09:45:01 + t
- 試し書き起こしで、13:30〜17:30 付近に司会アナウンスと 1 番目セッションの開始が入っていることを確認済み（10:00 開始と整合）

## タイムテーブル

データの正本は `timetable.json`（チャンネルページの説明文 `paragraphs` 入り）。

### Day1（2026-09-26）— 素材整理対象

| # | 時間 | タイトル | 登壇者 | ディレクトリ |
|:---|:---|:---|:---|:---|
| S1 | 10:00–11:00 | [Blenderで作る『超かぐや姫！』のCG背景](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-952) | 中尾陽子・草間徹也・高橋舞（キューン・プラント） | `01_KaguyahimeCGBackground_QoonPlant/` |
| S2 | 11:15–12:15 | [絵作りと効率を両立させる、キャラクターシェーディング技法 自主制作アニメ映画『砂塵ノ中デ』メイキング](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-946) | 三縁エイト（MC 藤田将） | `02_CharacterShading_Eight/` |
| S3 | 12:30–13:30 | [現実より「盛る」！ゥチらの3D制作～ Blenderでリアル²の世界へ～](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-948) | ジェニーカオリ（MC 東條あずさ） | `03_MoruReal2_JennyKaori/` |
| S4 | 13:45–14:45 | [アニメルックのつくりかた](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-944) | 炭猫べべ（MC 藤田将） | `04_AnimeLook_SuminekoBebe/` |
| S5 | 15:00–16:00 | [「キレ」を意識した2Dアニメ的アクション演出](https://cgworld.jp/special/blenderfes/vol7/?channel=16) | 龍村某 | `05_KireAction_Tatsumura/` |
| S6 | 16:15–17:15 | [エフェクトを作ろう](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-941) | 牛乳瓶 | `06_Effects_Gyunyubin/` |
| S7 | 17:30–18:30 | [ボーン1本から始めるキャラクターリギング](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-943) | minusT | `07_Rigging_minusT/` |
| S8 | 18:45–19:45 | [＜第7回＞3D人と選ぶ、注目のBlenderアドオン！](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-949) | 3D人・ますく・Land-Y（MC 涌井嶺） | `08_Addons_3Dnin/` |

### Day2（2026-09-27）— 未着手

| 時間 | タイトル | 登壇者 |
|:---|:---|:---|
| 10:00–11:00 | [短時間で心を掴む！自主制作から学ぶ、魅力的なショート映像制作](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-945) | 未確認 |
| 11:15–12:15 | [ノードで楽しむモーショングラフィックス表現](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-947) | 未確認 |
| 12:30–13:30 | [Blenderから広がる、体験型リアルタイムコンテンツのつくり方](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-951) | 未確認 |
| 13:45–14:45 | [Blender × 3Dプリント クリエイターが語る制作の裏側](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-940) | 未確認 |
| 15:00–16:00 | [フォトリアルCGテクニック](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-950) | 未確認 |
| 16:15–17:15 | [VRChatワールドができるまで BlenderとUnityをつなぐ空間制作の基本ワークフロー](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-991) | 未確認 |
| 17:30–18:30 | [AIエージェント × Blenderで挑む Vibe Modeling](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-942) | 未確認 |
| 18:45–19:45 | [b3d創作祭「スザンヌ」 結果発表&講評セッション](https://cgworld.jp/special/blenderfes/vol7/?channel=channel-919) | 未確認 |

## Directory Rules

各セッションは GDC2026 と同じ構成を基本とする。

```text
NN_<slug>/
├── source/            # Git 管理外（.gitignore: stock/BlenderFes2026/*/source/）
│   ├── audio/         # 元音声
│   ├── images/        # アーカイブから抽出したスライド候補フレーム + frames.tsv
│   ├── docs/          # 配布資料、原本ドキュメント
│   └── transcripts/   # whisper の生文字起こし（Talksribe 形式 + srt/vtt/txt）
├── images/            # ブログ記事向けに作成した補完画像
├── transcripts/       # ブログ用に整形した transcript Markdown
└── README.md          # セッション情報、進行管理
```

## Separation Policy

- `source/`
  - アーカイブ配信から得た元資料を置く。
  - 命名は元ファイル名を優先し、無理にリネームしない。
  - **Git にコミットしない。** このリポジトリは PUBLIC のため、生の文字起こし・音声・フレーム画像は公開しない。
- `images/`
  - 記事本文で直接使うために作った図、清書画像、切り出し画像を置く。
- `transcripts/`
  - 読みやすく整えた Markdown transcript、話者別 transcript を置く。
- セッション直下
  - 記事本文、要約、進行管理ファイルだけを置く。

## 主催者の撮影・録音ポリシー

- 主催者アナウンスでは「録画・録音は禁止、SNS へのスクリーンショット投稿は可」とされている。
- そのため音声・動画・生文字起こし・フレーム画像は `source/`（非公開）に留め、記事での引用は要約と、選別したスクリーンショットに限る。

## パイプライン

素材の置き場所:

| 素材 | 場所 |
|:---|:---|
| 1080p アーカイブ全体 | exia `/Volumes/Disk4TB/BlenderFes2026AW/BlenderFes2026AW_Day1_1080p.mp4` |
| セッション別動画 | exia `/Volumes/Disk4TB/BlenderFes2026AW/Day1/NN_<slug>.mp4` |
| セッション別フレーム | exia `/Volumes/Disk4TB/BlenderFes2026AW/Day1/frames/NN_<slug>/` |
| 音声切り出し・whisper 出力 | ローカル `~/Downloads/vimeo_event_6144090/sessions/S{n}.{wav,srt,vtt,txt}` |

各セッションの切り出しは「壁時計で開始 5 分前 〜 終了 5 分後」（4200 秒）。配信内オフセットは S1=599, S2=5099, S3=9599, S4=14099, S5=18599, S6=23099, S7=27599, S8=32099。

1. **exia で分割**（`-c copy` の無劣化分割。開始はキーフレーム境界で数秒前後する）

   ```bash
   scp stock/BlenderFes2026/scripts/split_video_exia.sh exiamac-mini:/Volumes/Disk4TB/BlenderFes2026AW/
   ssh exiamac-mini 'zsh /Volumes/Disk4TB/BlenderFes2026AW/split_video_exia.sh'
   ```

2. **exia でスライド候補フレームを抽出して取り込む**（キーフレームのシーン変化 > 0.3、最短間隔 15 秒、300 枚を超えたら閾値を上げる。幅 1280 の JPEG）

   ```bash
   scp stock/BlenderFes2026/scripts/extract_frames_exia.sh exiamac-mini:/Volumes/Disk4TB/BlenderFes2026AW/
   ssh exiamac-mini 'zsh /Volumes/Disk4TB/BlenderFes2026AW/extract_frames_exia.sh'
   for d in stock/BlenderFes2026/0?_*/; do n=$(basename $d)
     rsync -a exiamac-mini:/Volumes/Disk4TB/BlenderFes2026AW/Day1/frames/$n/ $d/source/images/; done
   ```

3. **文字起こしを Talksribe 形式に変換**（whisper-cli large-v3-turbo, `-l ja` の srt を壁時計付きに変換し、元ファイルもコピー）

   ```bash
   python3 stock/BlenderFes2026/scripts/srt_to_talksribe.py \
     --src ~/Downloads/vimeo_event_6144090/sessions --stock stock/BlenderFes2026 --sessions 1-8
   ```

4. **記事化** — `source/transcripts/` とスライドを元に、セッション直下に記事ドラフトを書く（`transcripts/` に整形版、`images/` に記事用画像）
5. **Re:VIEW 変換** — GDC2026 と同じく `md2review` で電子書籍原稿に変換する（`stock/GDC2026/md2review.py` を参照）

## 注意

- whisper の出力には無音区間のハルシネーション（「ご視聴ありがとうございました」の反復など）が含まれる。生データとして残し、整形時に取り除く。
- 登壇者の所属・肩書は未確認の箇所がある。記事化の前にチャンネルページで確認する。
