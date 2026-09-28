# Hatsumi「Making A 3D Vtuber Avatar for TimTea! Part 4 Shoes」要点
- 依頼作品の靴づくりの作業記録（23 分、ナレーションなし）。靴は前半約 11 分で、後半はパンツ装飾とジャケット細部
- 靴は足の複製ではなく Plane から作る。足を囲む粗いケージを押し出し、Grab ブラシ（Strength 0.400）でハイカットの形に寄せる
- モディファイアーは Subdivision（1/2）→ Mirror。Mirror Object に素体（base re）を指定し、原点がずれていても左右対称にする
- ソールは別パーツにせず、底の近くに辺ループを 2 本寄せて溝の線を締める
- ハトメは Circle から作ったリングを複製して履き口に並べる（shoe holes）
- 靴ひもは NURBS カーブ（NurbsPath）を交差させて通し、結び目の輪を横に垂らす（laces）
- 後半は Edge Crease（-0.256 / 0.643）で角の締まりを調整し、部品ごとに名前付きオブジェクトで管理
