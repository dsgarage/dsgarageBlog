# Hatsumi「Making A 3D Vtuber Avatar for TimTea! Part ~1 Head and Hair」要点

- 依頼制作の記録連載の第 1 回。ナレーションはなく、画面の設定値から手順を読み取った
- 顔のアップと全身三面図の参照画像を置き、Mirror（X、Clipping）と Subdivision Surface（Viewport 1 / Render 2）を最初から付ける
- 顔は目のまわりの輪から面を広げて参照の線に辺を沿わせ、頭を閉じる。まつ毛・眉は帯状の面、輪郭はスカルプトの Grab で整える
- 顔のテクスチャは Texture Paint で描く。明るい部分と影を別画像に分け、Smear でなじませる
- 髪は束ごとに別オブジェクト。Mirror Object に頭を指定し、Connected のプロポーショナル編集で束を個別に動かす
- EP1 の立方体から作る素体とは出発点が異なり、第 2 回ではこの頭の首から体を伸ばす
