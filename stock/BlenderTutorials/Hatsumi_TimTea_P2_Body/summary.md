# Hatsumi「Making A 3D Vtuber Avatar for TimTea! Part ~2 Body」要点

- 依頼制作の記録連載の第 2 回。ナレーションはなく、画面の設定値とテロップから手順を読み取った。画面の Blender は 2.93.18
- 体は別オブジェクトにせず、第 1 回の頭のメッシュ（head.002）の首から円筒を伸ばして作る
- Mirror（X、Clipping）で左右対称。側面の参照に合わせて Y 方向に 1.204 倍し、体の厚みを出す
- 胸と肩の辺は Knife Topology Tool（Occlude Geometry オン）で引き直す。男性の体に不慣れで、辺の流れを何度も試したと作者は述べている
- ひざに辺を足し、お尻から太ももへ辺を回り込ませる。途中から Subdivision Surface を追加
- 手は別オブジェクト。円柱の指を作り、Merge（At Center）と Grid Fill（Span 3）で手のひらとつなぐ
