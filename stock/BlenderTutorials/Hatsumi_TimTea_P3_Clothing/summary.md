# Hatsumi「Making A 3D Vtuber Avatar for TimTea! Part ~3/ Clothing」要点
- 依頼作品の服づくりの作業記録（29 分、ナレーションなし）。内容は画面の設定値とオブジェクト名から読み取った
- 服は素体を複製して作る。複製を Clothing コレクションへ移し、Mirror（X、Clipping・Merge 0.001 m）と Subdivision（1/2）をそのまま使う
- パーカー・ジャケット内側・外側・パンツをオブジェクトごとに分け、Grab・Smooth・Clay ブラシでゆとりやポケットを出す
- 布の厚みは Solidify ではなく、袖口などの見える端を内側へ折り返して見せている（要確認）
- 手袋は手のメッシュを選び、プロポーショナル編集付きで 1.012 倍に拡大して層を作る
- ひも・ファスナー・ベルトは別オブジェクト。中央の 1 本部品は Mirror を外し、独立部品は Clipping を切る
- Knife で色の切り替え位置に辺を足し、Dissolve Edges で不要な辺を減らす
