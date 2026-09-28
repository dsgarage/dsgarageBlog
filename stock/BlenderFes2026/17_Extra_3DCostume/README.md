# 【Blender Fes Extra】3D衣装の作り方(前半 + 後編)

## 講座情報

| 項目 | 内容 |
|:---|:---|
| タイトル | 【Blender Fes Extra】3D衣装の作り方 |
| 提供 | CGWORLD ONLINE ACADEMY |
| 形式 | オンライン講座(録画動画 + 後編末尾にライブの質疑応答) |
| 前半 | https://academy.cgworld.jp/contents/2152(1:41:31) |
| 後編 | https://academy.cgworld.jp/contents/2181(1:45:48) |
| 講師 | アレイラさん(講座内の呼称。正式な表記は要確認) |
| 題材 | バラと骨をあしらったゴシック調の衣装 1 着(VRChat 向けアバター「マヌカちゃん」用、無料配布) |
| 関連 Issue | dsgarage/dsgarageBlog#14 |

## 概要

3D 衣装 1 着の制作を、デザイン決め、モデリング、UV 展開、ハイモデル作成(前半)と、ベイク、テクスチャ、ボーンとウェイト、Unity でのチェック(後編)の順に解説する講座です。使用ツールは Blender 5.1、Substance 3D Painter、Unity + lilToon + VRChat SDK が中心で、ZBrush、Marvelous Designer、Substance 3D Designer、CLIP STUDIO PAINT が補助に使われます。

## 素材の状態

- **文字起こし**: 前半・後編とも whisper の出力を校正済みです(`source/transcripts/*_clean.txt`)。直した語と残した「要確認」は `*_corrections.md` に一覧があります。
- **前半の録画欠落**: 校正版 57:03 付近(講座動画の 54:18 付近)で、講座動画の約 2 分ぶんが録画されていません。三面図の優先順位の説明の途中です。
- **前半の時刻**: 校正版の時刻は講座動画プレーヤーの時刻とずれています。記事とキャプションでは、校正版 02:41〜57:03 は「約 2 分 45 秒を引く」、57:06 以降は「約 45 秒を引く」で換算した動画時刻を使っています(根拠は `source/navi/2152_zenhan_navi.md` 末尾)。
- **後編の時刻**: 校正版の時刻のまま使っています。講座動画プレーヤーの時刻との対応は確認できていません(要確認)。後編の録画は 2 本の結合で、重複区間は校正版で整理済みです(`source/transcripts/2181_kouhen_outline.md`)。
- **スクリーンショット**: 前半は 1080p です。後編は 360p 録画由来の低解像度で、後で 1080p に差し替える予定です。前半の一部(ツール一覧、T/A ポーズ、平面トレース)は画面遷移中のフレームで半透明に写っています。後編の一部(グランジ、ベルトの縫い目)には録画時の macOS メニューバーが写り込んでいます。差し替え時に撮り直す候補です。

## 成果物

| ファイル | 内容 |
|:---|:---|
| `20260928_BlenderFesExtra_3D衣装の作り方.md` | 前半と後編を統合した記事ドラフト(本文約 13,600 字、表を除く) |
| `summary.md` | 要点 |
| `skill_notes.md` | 工程順の技術メモ(Skills 化 claude-code-config#23 の入力) |
| `book_outline.md` | Re:VIEW 書籍(単巻)の章立て案 |
| `images/` | 記事で使う画像 20 枚(`source/images/` から選んでコピー。元は残置) |

### 画像の対応

記事用の名前の時刻は、前半が講座動画の時刻(換算値)、後編が校正版の時刻です。

| 記事用ファイル | 元ファイル |
|:---|:---|
| `bfx_zenhan_1352_tools_slide.jpg` | `source/images/zenhan/navi_1637_slide_tools.jpg` |
| `bfx_zenhan_2006_t_a_pose.jpg` | `source/images/zenhan/navi_2251_slide_t_a_pose.jpg` |
| `bfx_zenhan_2618_pureref.jpg` | `source/images/zenhan/navi_2903_pureref.jpg` |
| `bfx_zenhan_3544_cylinder_skirt.jpg` | `source/images/zenhan/navi_3829_blender_cylinder_trace.jpg` |
| `bfx_zenhan_4553_trace_plane.jpg` | `source/images/zenhan/navi_4838_blender_trace_plane.jpg` |
| `bfx_zenhan_5359_three_view_priority.jpg` | `source/images/zenhan/navi_5644_slide_three_view_priority.jpg` |
| `bfx_zenhan_6639_select_similar_sharp.jpg` | `source/images/zenhan/navi_6724_blender_select_similar_sharp.jpg` |
| `bfx_zenhan_7519_mio3_gridify.jpg` | `source/images/zenhan/navi_7604_blender_mio3_gridify.jpg` |
| `bfx_zenhan_8602_uv_six_tiles.jpg` | `source/images/zenhan/navi_8647_blender_uv_six_tiles.jpg` |
| `bfx_zenhan_8959_multires.jpg` | `source/images/zenhan/navi_9044_blender_multires.jpg` |
| `bfx_zenhan_9522_dyntopo.jpg` | `source/images/zenhan/navi_9607_blender_dyntopo.jpg` |
| `bfx_kouhen_0614_normal_parts.jpg` | `source/images/kouhen/navi_0614_blender_costume_normal_parts.jpg` |
| `bfx_kouhen_1017_sp_baker_normal.jpg` | `source/images/kouhen/navi_1017_sp_baker_normal.jpg` |
| `bfx_kouhen_1149_bake_error_fixes.jpg` | `source/images/kouhen/navi_1149_slide_bake_error_fixes.jpg` |
| `bfx_kouhen_1603_sp_baker_ao.jpg` | `source/images/kouhen/navi_1603_sp_baker_ao_settings.jpg` |
| `bfx_kouhen_2310_texture_project_bake.jpg` | `source/images/kouhen/navi_2310_sp_texture_project_bake.jpg` |
| `bfx_kouhen_3923_grunge_color_mura.jpg` | `source/images/kouhen/navi_3923_sp_grunge_color_mura.jpg` |
| `bfx_kouhen_4005_belt_stitch.jpg` | `source/images/kouhen/navi_4005_sp_belt_stitch.jpg` |
| `bfx_kouhen_4345_branch_bone.jpg` | `source/images/kouhen/navi_4345_blender_branch_bone.jpg` |
| `bfx_kouhen_5434_yuzu_weight_editor.jpg` | `source/images/kouhen/navi_5434_blender_yuzu_weight_editor.jpg` |

## トランスクリプトファイル

- `source/transcripts/2152_zenhan_clean.txt` — 前半の校正版(時刻は録画基準。動画時刻への換算はヘッダと操作ナビを参照)
- `source/transcripts/2152_zenhan_corrections.md` — 前半の校正対応表(確定表記、残した要確認、時刻の注記)
- `source/transcripts/2181_kouhen_clean.txt` — 後編の校正版
- `source/transcripts/2181_kouhen_corrections.md` — 後編の校正対応表
- `source/transcripts/2181_kouhen_outline.md` — 後編の節構成と録画の重複区間の説明
- `source/navi/2152_zenhan_navi.md` / `source/navi/2181_kouhen_navi.md` — 工程ごとの操作ナビ(「開く場所」、要撮影一覧)
- `transcripts/` — 記事用に整形した transcript の置き場(未作成)

`source/` 配下は Git 管理外です(有料講座のため非公開)。記事は文字起こしを要約・言い換えて書き、逐語の引用は 7 箇所(各 2 文以内)に抑えています。
