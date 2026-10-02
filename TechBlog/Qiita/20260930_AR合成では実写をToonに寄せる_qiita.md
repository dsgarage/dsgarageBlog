---
title: 'AR合成では実写をToonに寄せる ― V1nStudio の World モードと AR モード、2 つのシェーダー実装を比べる'
tags:
  - Unity
  - Shader
  - URP
  - AR
  - ARKit
private: true
updated_at: ''
id: null
organization_url_name: null
slide: false
ignorePublish: true
---

# AR合成では実写をToonに寄せる ― V1nStudio の World モードと AR モード、2 つのシェーダー実装を比べる

## はじめに

スマホの AR でトゥーン調のアバターを出すと、どうしても背景から浮いて見えます。アバターはノイズのないツルツルの CG で、背景はセンサーノイズやシャープネスの乗ったカメラ映像です。同じ画面に置いただけでは、写真の上にシールを貼ったような見た目になります。

自分が作っているアプリ「V1nStudio」でも、AR モードでこの浮き方がずっと気になっていました。V1nStudio は、動画から作ったダンスモーションを自分のアバターで踊らせるアプリです。3D 空間で踊らせる World モードと、カメラ映像に重ねる AR モードがあります。World モードは背景も CG なので、光も色も奥行きもこちらで決められます。AR モードではそれができません。

この記事では、AR モードのほうで取った方針と、その実装を紹介します。アバターを実写に寄せるのをやめて、実写の側をシェーダーでトゥーンに寄せました。URP（Universal Render Pipeline）の RenderGraph で自前のパスを 2 本足し、外光と色温度の反映、背景のカラコレ、被写界深度、空気層を順に重ねています。調整の途中で踏んだ失敗（Linear 色空間の罠や、実機で「汚い」と言われた粒）も書いておきます。

同じ内容を 2026-10-04 の LT でも話します。LT では時間の都合で省いた実装の詳細を、記事の後半の「LT の補足」にまとめました。

![合成処理なし（左）と、現在の既定値（右）](https://raw.githubusercontent.com/dsgarage/dsgarageBlog/main/TechBlog/images/ar_toon_before_after.jpg)

*左が合成処理なし、右が現在の既定値です。この比較はエディタ上の検証用素材で、アバターが焼き込まれた実機スクリーンショットに処理を通しているため、アバターにも処理がかかっています。実機ではアバターには Pass A がかかりません。*

:::note info
**Unity開発者の方へ**

Claude CodeのようなAIエージェントは強力ですが、Unity Editorを直接操作することはできません。
この溝を埋めるのが **UniMCP4CC**（Unity MCP Server for Claude Code）です。V1nStudio の World モードのライティングやポストエフェクトも、Claude Code から UniMCP4CC 経由で組みました。

- GitHub: [dsgarage/UniMCP4CC](https://github.com/dsgarage/UniMCP4CC)
- 対応Unity: 2021.3 LTS以降
- ライセンス: MIT
:::

---

## World モードの絵作り

先に、比較対象の World モードを簡単に紹介します。

World モードは、ネオン管を張りめぐらせたスタジオのような部屋でアバターが踊ります。背景を含めてすべてが 3D 空間にあるので、URP の標準機能をそのまま使えます。

| 要素 | 実装 |
|:---|:---|
| 被写界深度 | URP の Depth of Field（Bokeh モード）。自前の `P2cAutoFocus` が毎フレーム、カメラからアバターの胸までの距離をピント位置に書き込みます |
| ネオン | Unlit の HDR 発光（強さ ×2.0）の管を壁と天井に這わせて、Bloom でにじませます。床と壁にはリフレクションプローブで映り込みます |
| 仕上げ | Bloom、Neutral トーンマップ、コントラストと彩度、ビネット |

トーンマップを Neutral にしたのは、ACES だとトゥーンの彩度が沈んだためです。ネオンの強さも 3.2 では白飛びして色が乗らず、2.0 に落としました。どれも空間の情報がそろっている前提で、ライトとポストエフェクトの値を詰めていく作業です。

AR モードではこの前提が崩れます。

---

## AR モードでアバターが浮く理由

AR モードの背景は、AR Foundation の `ARBackgroundRendererFeature` が不透明物を描く前（`BeforeRenderingOpaques`）にカメラ映像をカラーバッファへ直接描いています。つまり背景は光と色がすでに決まった 1 枚の画像で、こちらからライトで照らし直すことはできません。

実装を調べると、もう 1 つ問題が見つかりました。World モードのポストエフェクト（`P2cLookDev`）は AR カメラに配線されていませんでした。AR モードには、背景とアバターをまとめて整える処理が何もなかったことになります。

浮いて見える原因を分けると、次のようになります。

| 差 | アバター（V1nToon） | カメラ映像 |
|:---|:---|:---|
| 質感 | 面で塗られた陰、ディテールが少ない | 細かいディテール、スマホ特有の強いシャープネス |
| ノイズ | なし | 暗部にセンサーノイズ |
| 黒 | 純黒まで落ちる | 少し浮いた黒 |
| 陰の色 | 青紫寄りの陰色（`_ShadeColor`） | 環境光しだい |

---

## 方針：実写の側を加工する

よくある解決は、アバターのライティングをリアルにして実写に合わせる方向です。V1nStudio ではアバターが主役で、トゥーンの見た目を崩したくなかったので、逆向きにしました。アバターのシェーダー（V1nToon）には手を入れず、実写の側を加工します。

この方針が取れるのは、先ほどの描画順のおかげです。AR 背景を描いた直後のカラーバッファには実写しか入っていません。そこでシェーダーを 1 本通せば、アバターのマスクを用意しなくても背景だけを加工できます。

![AR 合成のパイプライン](https://raw.githubusercontent.com/dsgarage/dsgarageBlog/main/TechBlog/images/ar_toon_pipeline.png)

足したパスは 2 本です。

- **Pass A（P2cRealToToon）**：AR 背景の直後に、実写だけを整えます
- **Pass B（P2cAirLayer）**：最後に、背景とアバターの両方へ空気層を 1 枚かけます

どちらも `ScriptableRendererFeature` を 1 つ作って RenderGraph に差し込んでいます。URP 標準の `FullScreenPassRendererFeature` は差し込み位置を `BeforeRenderingOpaques` の直後に指定できなかったため、自前にしました。AR カメラにだけマーカーのコンポーネントを付け、マーカーの無いカメラでは何もしないので、World モードには影響しません。録画は最終画面をそのまま掴む作りなので、パスを足せば録画にも自動で入ります。

---

## ① 外光と色温度の反映

1 つだけ、アバターを実写に寄せる処理も残しています。ARKit の光推定から明るさと色温度を取り、アバターの `_BaseColor` に乗算する `ArLightMatch` です。

- 再ライティングはせず、色味の雰囲気合わせにとどめています。乗算の強さは既定で 50% です
- 推定値をそのまま使うと、ちらつきました。明るさは 0.5 秒、色は 2 秒の指数移動平均でならし、閾値未満の変化では書き込みを止めています

アバターを少しだけ実写に寄せ、実写をそれより大きくアバターに寄せる、という配分です。実機では「色合いはちょうどいい」という評価で、この 2 つが両方効いた状態が今の既定値になっています。

---

## ② 実写のカラコレ（Pass A）

Pass A では、実写に次の処理を順にかけます。

1. **質感の簡略化**：半解像度で 4 セクタの Kuwahara フィルタ（半径 2）をかけます。Kuwahara は、周囲をいくつかの領域に分けて一番ばらつきの小さい領域の平均を取るフィルタで、エッジを残したまま細部を絵筆で塗ったように平らにします。元のディテールを 25% だけ戻しています
2. **背景ぼかし**：被写界深度の代わりです（次の章で説明します）
3. **陰の段階化**：暗い領域の輝度を 3 段に寄せて、トゥーンの陰の出方に近づけます
4. **陰色の転写**：アバターの陰色を、実写の暗部に移します
5. **彩度とコントラスト**：実写を少しだけアバター寄りの高彩度にします

### Linear 色空間の罠

最初の実装では、暗部がつぶれて画面のほぼ全域が陰として扱われました。

原因は色空間でした。このプロジェクトは Linear 色空間なので、シェーダーが受け取る色は線形値です。一方で、陰の閾値（0.15〜0.45）や 0.5 中心のコントラストは、画面で見た明るさ（知覚値）のつもりで決めた数値でした。線形値に知覚値の閾値を当てたため、次のようにずれていました。

- 陰の閾値 0.15〜0.45 が sRGB で 108〜179 に相当し、ほぼ全域が陰になった
- コントラスト 1.05 だけで、天井の暗部が 59 から 35 まで落ちた（約 −40%）

直し方は単純で、シェーダーの中でいったん知覚空間へ変換し、演算してから線形へ戻します。Kuwahara やぼかしは平均を取る処理なので、線形のままで構いません。

```hlsl
// P2cRealToToon のフラグメント（抜粋）。閾値・コントラストはすべて知覚（sRGB）値
half3 smoothed = LinearToSRGB(SampleBgBlur(input.uv));   // Kuwahara＋ぼかし済み
half3 orig = LinearToSRGB(saturate(SAMPLE_TEXTURE2D(_P2cOrig, sampler_P2cOrig, input.uv).rgb));
half3 c = lerp(smoothed, orig, (half)_DetailKeep);        // ディテールを少し戻す
half lum = Luminance(c);

// 陰色転写：_ShadeColor を輝度 1 に正規化して「色味」だけを使う
half3 shadeTint = _ShadeColor.rgb / max(Luminance(_ShadeColor.rgb), 1e-3h);
half w = 1.0h - smoothstep((half)_ShadeLo, (half)_ShadeHi, lum);
c = lerp(c, c * shadeTint * (1.0h - (half)_ShadeDarken), w * (half)_ShadeStrength);

// 彩度とコントラストのあと、線形に戻して返す
return half4(SRGBToLinear(saturate(c)), 1.0h);
```

陰色の転写でも 1 つ直しています。アバターの陰色（既定 0.62, 0.60, 0.72）をそのまま掛けると、陰色の輝度の分だけ暗部が一様に約 38% 暗くなります。これだと、色を移すつもりが暗くしているだけになります。そこで陰色を輝度 1 に正規化して色味だけを取り出し、暗さは `_ShadeDarken`（既定 0.15）で別に控えめに足すようにしました。

![陰色転写の強さ 0.3 / 0.5 / 0.8](https://raw.githubusercontent.com/dsgarage/dsgarageBlog/main/TechBlog/images/ar_toon_shade.jpg)

*陰色転写の強さを 0.3 / 0.5（既定）/ 0.8 で比べたものです。強くしても暗く沈まず、色味だけが青紫側に動きます。*

---

## ③ 被写界深度（World と AR の違い）

World モードでは、URP の Depth of Field が 3D の奥行きからぼかしを計算します。AR モードの背景は画像なので、同じ方法は使えません。

最初は、Pass A の中で背景を一律にぼかしました。9 タップの円形サンプルで、半径は画面の高さ 1080 px を基準に 3 px です。Pass A はアバターを描く前の背景にしかかからないので、アバターはシャープなまま背景だけがぼけます。

![アバターはシャープなまま、背景だけがぼける](https://raw.githubusercontent.com/dsgarage/dsgarageBlog/main/TechBlog/images/ar_toon_blur.jpg)

*左から合成処理なし、旧既定値（ぼかし 0、粒 0.06）、現在の既定値（ぼかし 3、粒 0）。後ろの人物だけが柔らかくなっています。*

その後、LiDAR 搭載機では距離に応じてぼかすようにしました。ARKit の背景シェーダーは、環境深度が有効なとき実世界の深度を深度バッファに書きます。これを読んで、アバターまでの距離をピント位置にし、遠いほど強くぼかします。

ここにも罠が 1 つありました。このプロジェクトは MSAA 4 なので、深度アタッチメントは MSAA テクスチャになっていて、普通の `TEXTURE2D` としては読めません。URP の `CopyDepthPass` で単一サンプルの `R32_SFloat` へコピーしてから渡しています。LiDAR が無い機種では深度が一定値になるので、自然に一律ぼかしへ戻ります。

![一律ぼかしと距離依存ぼかし](https://raw.githubusercontent.com/dsgarage/dsgarageBlog/main/TechBlog/images/ar_toon_depthblur.jpg)

*左から合成処理なし、一律 3 px、距離依存。距離依存では手前の床がシャープなまま、奥の扉がぼけます。*

---

## ④ 空気層（Pass B）

最後の Pass B は、背景とアバターの両方にかけます。CG の乾いた感じと実写の空気感の差を、1 枚のフィルターでまとめるためのパスです。

考え方の元にしたのは、セル画をフィルムで撮影していた頃の画です。ポジ（リバーサル）フィルムでは、粒子はハイライトに乗り、シャドウは締まります。これに合わせて、次の処理を入れました。

- **暗部の平滑**：スマホのノイズは暗部に集中しているので、暗いところだけを軽く平らにします。実写のノイズ除去と CG 側の整えが同じ処理で片付きます
- **ライトラップ**：背景の光を、アバターの輪郭の内側にほんの少し回り込ませます。アバターのマスクは別パスで描いています
- **ハレーション**：明るい部分のまわりに暖色のにじみを出します。最初は加算合成にしていて、アバターの白い衣装が飽和しました。スクリーン合成に変えて解決しています
- **黒の持ち上げ**：CG の純黒を、実写の少し浮いた黒にそろえます（既定 0.03）

![ライトラップで髪の縁に背景の色が回り込む](https://raw.githubusercontent.com/dsgarage/dsgarageBlog/main/TechBlog/images/ar_toon_wrap.jpg)

*ライトラップの強さを変えて、髪の縁を 3 倍に拡大したものです。強いほど輪郭の内側が背景色に寄り、貼り付けた印象が減ります。*

URP 標準の Film Grain は使いませんでした。パッケージのコードを確認すると、粒の重みが `1 - sqrt(lum)` で、暗部ほど粒が多いネガフィルム型だったためです。ポジ型の粒は、輝度マスクを付けて明部にだけ乗るように自前で書きました。

### 実機で言われて直したところ

見た目の塩梅には正解がないので、パラメータはすべて実行時に変えられるようにして、実機で試しました。

- **粒**：明部にだけ乗るようにしたものの、実機で見ると「汚い」という評価でした。既定値を 0 にしています。コードは残してあり、スライダーで戻せます
- **ダスト**：空間に漂う塵を 3D パーティクルで置きました。最初の大粒は「胞子みたい」と言われ、400 粒・1.5〜4 mm の細かい粒子が明滅する形に作り直しました。深度テストがかかるので、アバターや LiDAR で取った実物の裏に隠れます
- **黒の持ち上げ**：名目 0.03 のはずが、実効 0.0206 しか効いていませんでした。持ち上げのあとにコントラストをかけていたためで、順序を入れ替えています

最終的には、設定画面に「空気層の調整」シートを作り、ぼかし・ダストの量・陰色・粒・黒の持ち上げなどをスライダーで変えられるようにしました。

---

## LT の補足

この記事は、2026-10-04 の LT「AR合成では実写をToonに寄せる」と同じ内容を扱っています。10 分では省いた部分や、スライド 1 枚では伝えにくい部分をここにまとめます。

| LT の章 | この記事の対応箇所 |
|:---|:---|
| World モード | World モードの絵作り |
| AR モードの課題 | AR モードでアバターが浮く理由 |
| コンセプト | 方針：実写の側を加工する／補足：パスの差し込み位置 |
| 外光と色温度 | ① 外光と色温度の反映 |
| 実写のカラコレ | ② 実写のカラコレ／補足：知覚空間に変換する理由 |
| 被写界深度 | ③ 被写界深度 |
| 空気層 | ④ 空気層／補足：ポジ型とネガ型の粒 |

### パスの差し込み位置

LT では「アバターを描く前に実写だけを加工できる」と一言で済ませました。実装では、3 本のパスをそれぞれ別のタイミングで差し込んでいます。

| パス | タイミング（`RenderPassEvent`） | その時点のカラーバッファ |
|:---|:---|:---|
| 深度コピー（LiDAR 機） | `BeforeRenderingOpaques + 1` | 実写と、環境深度 |
| Pass A（P2cRealToToon） | `BeforeRenderingOpaques + 1` | 実写だけ |
| アバターのマスク | `AfterRenderingOpaques` | 実写とアバター |
| Pass B（P2cAirLayer） | `AfterRenderingPostProcessing` | 最終に近い画面 |

AR 背景も `BeforeRenderingOpaques` で描かれるので、その 1 つ後ろに Pass A を置いています。`RenderPassEvent` は整数の列挙型なので、`+ 1` で同じイベントの直後に並べられます。

```csharp
// ArCompositeFeature.Create()（抜粋）。AR 背景の直後に Pass A を置く
_passA = new ArCompositePassA
{
    renderPassEvent = (RenderPassEvent)((int)RenderPassEvent.BeforeRenderingOpaques + 1)
};
_passMask = new ArCompositePassMask { renderPassEvent = RenderPassEvent.AfterRenderingOpaques };
_passB = new ArCompositePassB { renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing };

// AddRenderPasses()（抜粋）。AR カメラのマーカーが無ければ何もしない＝World には影響しない
var target = renderingData.cameraData.camera.GetComponent<ArCompositeTarget>();
if (target == null || !target.IsActive) return;
renderer.EnqueuePass(_passA);
renderer.EnqueuePass(_passMask);
renderer.EnqueuePass(_passB);
```

Pass A の中身は 2 段です。まず現在の画面を半解像度のテクスチャにコピーして Kuwahara とぼかしをかけ、次にその結果と元の画面の 2 枚を入力にして、陰の段階化と陰色の転写をしながら元のバッファへ書き戻します。入力が 2 枚あるので、RenderGraph の `AddBlitPass` では足りず、`AddRasterRenderPass` でパスを手で組んでいます。

### 知覚空間に変換する理由

LT では「Linear のまま演算して暗部がつぶれた」とだけ話しました。数字で見ると、次のようにずれます。

| 決めた値（知覚値のつもり） | 線形値として当てたときの見た目（sRGB 8bit） |
|:---|:---|
| 陰の下限 0.15 | 108 |
| 陰の上限 0.45 | 179 |
| 粒の下限 0.35 | 160 |

陰の上限が 179 まで上がるので、室内の画面はほとんどが陰の範囲に入ります。逆に、粒の下限は 160 まで上がるので、明部にもほとんど粒が乗りませんでした。Linear 色空間のプロジェクトで「画面で見た明るさ」を基準に閾値を決めるなら、`LinearToSRGB` で変換してから比べ、最後に `SRGBToLinear` で戻すのが安全です。

### ポジ型とネガ型の粒

URP 標準の Film Grain と、Pass B の粒の重みを輝度ごとに並べると、向きが逆なのが分かります。

| 画面の輝度 | URP の Film Grain `1 - sqrt(lum)` | Pass B `smoothstep(0.35, 0.9, lum)` |
|:---|:---|:---|
| 0.1（暗部） | 0.68 | 0 |
| 0.5（中間） | 0.29 | 0.18 |
| 0.9（明部） | 0.05 | 1 |

URP 標準は暗部ほど粒が多く、ネガフィルムの見え方に近くなります。Pass B は暗部には一切乗らず、明部にだけ乗ります。もっとも、記事の本文に書いたとおり、実機では粒そのものが「汚い」という評価で、既定値は 0 にしています。

### 実機なしで塩梅を詰めた方法

見た目の調整を毎回 TestFlight に上げて確かめていると、ビルドと配信を待つ時間のほうが長くなります。そこで、エディタのバッチモードで動く確認用の仕組み（look-dev ハーネス）を作りました。

1. 実写の連番画像（プレート）を用意します。実機のスクリーンショットと、室内・夕方の街の動画から切り出したもの
2. エディタのバッチモードでプレートにアバターを重ね、実機と同じシェーダーを通します
3. 合成処理なし、Pass A だけ、Pass B だけ、両方の 4 通りを 2×2 に並べた画像と動画を書き出します

パラメータはコマンドライン引数で上書きできるので、ぼかし 0 / 3 / 6 のような比較を一度に回せます。この記事の比較画像も、すべてこのハーネスの出力です。実機に上げるのは、ハーネスで方向が決まってからにしました。

### 性能について

Pass A は半解像度で Kuwahara（36 タップ）とぼかし（9 タップ）、Pass B は 12 タップです。これにアバターのマスクとダスト 400 粒が加わります。GPU 時間を HUD と CSV に出す仕組みと、Pass A を 1/4 解像度に落とすスイッチは入れてありますが、実機での合成処理あり・なしの差はまだ計測していません。目標は、iPhone 15 Pro 級の端末で +3 ms 以内、録画中も 60fps の維持です。計測できたらこの節に追記します。

<!--
### 当日いただいた質問
LT のあとに、質問と回答をここに追記する（公開前にコメントを外す）
-->

---

## まとめ

- World モードは背景も CG なので、URP の Depth of Field や Bloom で空間を描けます。AR モードは背景が実写なので、同じ考え方では絵がそろいません
- AR モードでは、アバターには手を入れず、実写の側をシェーダーでトゥーンに寄せました。AR 背景の直後に実写だけを整える Pass A と、最後に全体へ空気層をかける Pass B の 2 本です
- Linear 色空間のプロジェクトで知覚値の閾値を使うときは、シェーダーの中で sRGB に変換してから演算します。これを忘れると暗部がつぶれます
- 被写界深度は、World は 3D の奥行きから、AR は背景だけのぼかしと LiDAR の深度から作りました

LT のスライドは、公開したらここにリンクを追記します。

---

## 参考リンク

- [Kuwahara filter（Wikipedia）](https://en.wikipedia.org/wiki/Kuwahara_filter)
- [URP: Scriptable Renderer Features](https://docs.unity3d.com/6000.2/Documentation/Manual/urp/renderer-features/scriptable-renderer-features/intro-to-scriptable-renderer-features.html)
- [AR Foundation: Camera and light estimation](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@6.0/manual/features/camera/camera-components.html)
