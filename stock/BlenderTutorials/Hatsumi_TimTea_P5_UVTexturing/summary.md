# Hatsumi「TimTea Part 5 UV Mapping/Texturing」要点

- 依頼制作の記録連載の第 5 回。ナレーションなしで、体・衣装・靴・髪の UV 展開と塗りを Blender 内で完結させる
- 体はシームを入れて展開し、専用画像（PNG、RGBA、8 bit）を作って先に保存。塗りは土台・soft shade・Light・Shadow の 4 枚に分ける
- 衣装は複数オブジェクトをまとめて 1 枚の画像へ展開し、UV Squares で帯状のアイランドを長方形にそろえる
- 衣装と髪は Ucupaint 1.0.12 の 4 レイヤーで塗る。影は乗算、TexDraw で描き Erase Alpha で消し Soften で縁をぼかす
- 靴はあとから Angle Based（Fill Holes・Correct Aspect オン、Margin 0.001）で展開し、衣装画像の空きに入れる
- 髪は房ごとに格子化して並べ、Emission＋ColorRamp のマテリアルと Outline Helper のアウトラインで仕上げる
