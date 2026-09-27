# S4 アニメルックのつくりかた（炭猫べべ）要点

- 登壇者は背景モデラーとして働きながら Blender で短編アニメ（「光を求めて」「ルナと猫のミミ」）を一人で制作している
- 画像生成 AI の登場を機に「監督としての勉強」（脚本・イメージボード・カメラワーク）へ舵を切った。SAVE THE CAT、StudioBinder などを紹介
- アニメルックを選んだ理由は見た目に加えてコスト削減。一人制作の制約から逆算して背景と出力のコストを削っている。古いノート PC 1 台で、EEVEE とビューポートレンダーを最終出力に使う
- 線はグリースペンシルのコレクションラインアートに Subdivide／Thickness（Custom Curve で入り抜き）／Simplify（Sample）／Noise を重ねて手描き風にする
- 顔の不要な線は、メッシュの頂点グループ → Line Art の Vertex Weight Transfer → Opacity モディファイアーで消す。中間ウェイトなら半透明になる
- 影とハイライトはカメラ固定でテクスチャに描き込み、画像テクスチャを Material Output に直結。View Transform は最初に Standard にする
- テクスチャは Illustrator のパスで作り、「ランダム・ひねり」とブラシで手描きの揺らぎを出す。解像度に縛られない
- シェーダー版は Diffuse／Glossy BSDF → Shader to RGB → Color Ramp を、比較（暗）と加算でテクスチャに重ねる（EEVEE 限定）
- 背景はイラストを箱に Project from View で投影。寸法の目安は Claude の画像解析で出し、下絵に合わせて微調整。手ブレは Camera Shakify
- AI は UV 着色や寸法出しの「最初のあたり」として使う。質疑はなく、司会はベクター製テクスチャの積み重ねが作風を支えていると評した
