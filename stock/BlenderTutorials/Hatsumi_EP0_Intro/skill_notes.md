# Hatsumi「VTuber Modeling Series EP0 Intro」Skill 化用ノート

出典: YouTube「Blender Vtuber Modeling Series: EP 0 Intro」（Hatsumi、2026-08-31 公開、5:42、英語）。
凡例: 【話者】= 作者の意見・運用上の判断、【事実】= Blender やアドオンの一般的な仕様、【画面】= 動画の画面で確認した値。聞き取りが不確かなものは「(要確認)」。時刻は動画内の時刻。
この回はモデリング操作を含まない導入回。シリーズ全体の工程と前提（アドオン・想定バージョン・完成像）を Skill の前提条件として使う想定でまとめる。

## シリーズ全体の工程と完成像

- 【話者】複数回の連載。Blender の高度な技術がなくても 3D VTuber モデルを作れるようにするのが目的（00:30）
- 【話者】全員が同じ完成品を作るのではない。参照画像は作者が用意し、自由に改変してよい。自分の参照画像を使ってもよい（00:40〜00:59）
- 【画面】参照画像の例は T ポーズの全身画（01:10、frame_0001）。EP1 の参照画像も正面・側面の T ポーズ線画
- 【話者】同じテーマの回を 1 本にせず複数用意する。肌の色・髪型・衣装を組み合わせられるようにし、耳・しっぽ・翼などのアクセサリーも扱う（00:59〜01:20）
- 【話者】すべての要素について男性版も作る（01:20）
- 【話者】扱う工程: モデリング → テクスチャ → リギング → 表情 → Unity でのセットアップ（01:23〜01:33。whisper の「raking」は YouTube 字幕で「rigging」）
- 【話者】VTuber モデルを作らない人にも手法は応用できる（01:48）
- 【話者】ガイド・スターターメッシュ・参照画像は Patreon で無料配布（01:40、説明文にも記載）
- 【事実】info.json のタグに VRM・Unity・Warudo があり、VRM 形式で書き出して配信ソフトで使う流れが想定される（シリーズ内での明言は EP0 ではなし）
- 【話者】EP1 以降の実際の工程順（参考、EP1 の予告より）: EP1 素体 → 次回に手・足・頭

## 前提: Blender のバージョン

- 【話者】作者は Blender 3.6 を使用。好きなバージョンでよい（01:59）
- 【話者】入手先は公式サイトまたは Steam（02:03）
- 【画面】EP1 のステータスバー表示は 3.6.11
- 【事実】Blender 4.2 以降は LoopTools や Extra Objects が同梱アドオンではなく、拡張機能（Extensions）として別途取得する形に変わっている。4.2 以降で追う場合は Get Extensions から入れる（本動画の手順は 3.6 前提）

## 前提: 外部から入れるアドオン（4 つ）

- 【話者】VRM Add-on for Blender（口頭では「VRM tools for Blender」。正式名称は要確認）（02:10、02:32）
- 【話者】Robust Weight Transfer。GitHub から取得（02:38）
  - 【画面】リポジトリ sentfromspacevr/robust-weight-transfer、Latest は v1.1.9（02:46、frame_0004）
  - 【画面】README の説明: 体からのウェイト転送をワンクリックで行い、股や胸の間・脇などのウェイト手直しを不要にする
- 【話者】Mio3 UV。使うかどうかは未定だが、あると便利かもしれない（02:45。whisper「Mule 3 / Mio 3」、YouTube 字幕「Meal Three」。名称は Mio3 UV と判断、要確認）
  - 【話者】配布ページで「Get Add-on」→ ファイルを Blender へドラッグ＆ドロップ
- 【話者】Ucupaint（テクスチャペイント用）。「Get Add-on」→ ドラッグ＆ドロップでもよいが、GitHub からの取得をすすめる（03:06）
  - 【画面】extensions.blender.org の Ucupaint ページ: Version 2.4.9、Compatibility「Blender 4.2 LTS and newer」、Report Issues は github.com/ucupumar/ucupaint（03:10、frame_0005）
  - 【話者】作者の 3.6 と拡張機能ページの対応バージョン（4.2 以降）が食い違う。3.6 で使う場合は GitHub で 3.6 対応版を選ぶ必要がありそう（動画では説明なし、要確認）

## アドオンのインストール手順（03:21〜）

- Edit → Preferences → Add-ons → Install → 保存先の zip を選ぶ → Install Add-on
- 【話者】zip は解凍しない。zip のまま選ぶ（03:38）
- 【話者】インストール後は一覧でチェックが入った状態になる
- 同梱アドオン: 検索欄に「LoopTools」と入れてチェック → 「Extra Objects」も同様（03:57〜04:06）
- Save Preferences で保存（04:10）
- 【事実】Extra Objects は Add Mesh 版と Add Curve 版の 2 つがある。アニメ髪の動画では両方にチェックが入っていた（Round Cube は Add Mesh: Extra Objects 側）

## 作者がよく使うツール（04:12〜）

- 【話者】編集モード: 移動ツール、拡大縮小ツール、回転
- 【話者】Smooth Vertices を D キーに割り当てている（04:24）
  - 手順: 編集モードの Vertex メニュー → Smooth Vertices を右クリック → Change Shortcut → 割り当てたいキーを押す
  - 【事実】既定の D キーは Annotate（アノテーション）系の操作に使われているため、上書きになる点に注意（要確認）
- 【話者】ベベル、ループカット、頂点スライド（04:48）
  - 頂点スライドのキーは whisper「should be」、YouTube 字幕「shift B」。EP1 の画面テロップは「shift + v」で、Shift+V が正しいと判断
- 【話者】スカルプトモード: Smooth ブラシ、簡単な細部には Grab ブラシ（04:55〜05:01）
- 【話者】プロポーショナル編集を多用する。動画を見るときに意識しておくとよい（05:04）
- 【話者】ビューポートの Overlays → Wireframe をオンにするのが好み（05:14）

## Skill 化するときのチェック項目

- 着手前に Blender のバージョンを確認する。3.6 と 4.2 以降でアドオンの入手方法が違う
- 外部アドオン 4 つと同梱アドオン 2 つが有効か、Preferences の一覧で確認する
- Smooth Vertices のショートカットを登録しておく（以降の回で作者はメニューを開かない）
- Overlays の Wireframe をオンにして、作者の画面と同じ見え方にそろえる
- 参照画像とスターターメッシュは作者の Patreon から取得する（再配布しない）

## ショートカット早見

- D: Smooth Vertices（作者の独自割り当て。既定ではない）
- Shift+V: 頂点スライド（EP1 テロップより）
- O: プロポーショナル編集の切り替え（【事実】動画では口頭で「proportional editing」とのみ）
