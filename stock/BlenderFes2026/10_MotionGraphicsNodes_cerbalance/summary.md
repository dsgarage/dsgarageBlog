# D2 S2 ノードで楽しむモーショングラフィックス表現（cerbalance）要点

- 講演は全編韓国語。After Effects での自動化経験を持つモーションデザイナーが、Blender 1 年目の習作と代表作のノード構成を開いて解説した
- ノードをつなぐ前に、画面を点・線・面に分解し「どれから始めたか」を仮説として立て、作って確かめて選ぶ
- 最初の習作では Mesh Boolean の多用で重くなり、Instance on Points と For Each に置き換えた。自分で試し、違えば検索する学び方
- DJMAX「Dreamscape」MV は World シェーダー（Voronoi の星、自作 RaySphere）と Dot Product の柱シェーダーで非現実的な画面を作った
- 霧とハーフトーンは Shader の Raycast。Geometry の Incoming を -1 倍して Direction に入れ、マテリアルは必ず Blended にする
- 破片は Distribute Points on Faces と Mesh Boolean で自作。小さく割ってから拡大し、色とスケールで不規則さを出す
- Blob Tracking は自作 Trail Capture の 2D Convert（Active Camera と Raycast で点をカメラ前の平面に移す）で線の太さをそろえる
- Compositor では Image Coordinates の Pixel と Fraction で UV を区切り、Map UV と自作 String Sprite で数字の UI を作った
- 動きの型は Float Curve、Motion Fraction、BPM Time、Vector Mapping で部品化し、クロスシミュレーションは Mesh Line で試してから文字に移す
