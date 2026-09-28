# Hatsumi「How to make EASY Anime Hair in Blender」要点

- 頭にかぶせた Round Cube を裂いて房にする、メッシュ中心のアニメ髪チュートリアル（14:24）。カーブは使わない
- 前提は LoopTools と Extra Objects（Preferences の Community タブで有効化）。画面の Blender は 3.6.11
- Round Cube は Arc 6・Linear 1 で追加し、頭の形に合わせる。P で前後に分離し、片側を消して Mirror（X、Clipping オン）
- 縁を LoopTools の Relax でならして押し出し、ループカットを入れてから V のリップで房に裂き、毛先を V 字にとがらせる
- Solidify（後ろ髪 0.009 前後、前髪 0.006）を適用し、厚みの中央にループカットを入れて毛先を縮め、平たく細くする
- 内外の頂点を Ctrl+クリックで選んで Smooth Vertices。全体に Mesh → Normals → Reset Vectors をかけ、見えない内側の面を削除
- サブディビジョンを足すなら毛先の中央と下端に Shift+E のクリースを入れて先端を保つ（動画では不使用）
- 密度を上げるには、髪を複製して内側に重ねるか、房を複製して V 字とナイフの切れ目で土台につなぐ
