# GDC 2026: 3年4ヶ月のグラフィックス最適化 — Tony Hawk's Pro Skater 3+4 マルチプラットフォーム戦記

スケートゲームの描画は、一見シンプルに見えてエンジニアリング上の難題を抱えている。プレイヤーがトリックを決めながら高速に移動するカメラは、毎フレーム大量の新しいジオメトリを描画しなければならない。開放的なスケートパークには数百のダイナミックライト、複雑なフォリッジ、遠景まで広がるステージが存在する。それを Switch 1 のような低スペック機から、PS5 / Xbox Series X の 120fps モードまで、**6つのプラットフォーム × 数十ステージの全組み合わせ**で安定させる必要がある。

Blizzard Albany（旧 Vicarious Visions）の John Moore 氏は、このセッションで「うまくいった最適化」と「期待外れだった最適化」の両方を率直に共有した。30分という短い講演ながら、GPU の最深部にあるアセンブリ命令の違いから、アーティストとの協業プロセスまで、レンダリングエンジニアの3年4ヶ月が凝縮されていた。

トランスクリプトとスライド写真をもとにレポートする。

> **GDC（Game Developers Conference）**: 毎年3月にサンフランシスコで開催される世界最大のゲーム開発者向けカンファレンス。

### 本セッションの技術的位置づけ

本セッションは **Unreal Engine のレンダリングパイプラインを深くカスタマイズ**した事例の発表だ。Unreal Engine はソースコードが公開されており、エンジンの描画パイプライン（ディファードシェーディング、ライティング、シャドウ計算など）に直接手を入れることができる。Moore 氏は Unreal のディファードライトシェーダーのパーミュテーション構造を変更し、クラスタードライティングのデータ構造を独自に書き換え、さらに GPU アーキテクチャごとにアセンブリレベルのチューニングを行っている。

Unity エンジニアにとっては、**SRP（Scriptable Render Pipeline）のカスタマイズ + カスタムシェーダーの低レベル最適化 + エディタ拡張によるコンテンツバリデーション** に相当する範囲の話だ。ただし Unreal はエンジンソースに直接アクセスできるため、Unity の SRP カスタマイズより深い階層まで踏み込んでいる点に留意されたい。記事末尾に、各最適化手法の Unity における対応策をまとめた。

---

## セッション概要

| 項目 | 内容 |
|:---|:---|
| タイトル | 3 Years and 4 Months of Graphics Optimization in 'Tony Hawk's Pro Skater 3+4' |
| スピーカー | John Moore, Principal Rendering Engineer |
| 所属 | Blizzard Albany（旧 Vicarious Visions） |
| トラック | Programming |
| 形式 | Lecture（30分） |
| 日時 | 2026年3月12日（木）16:30 - 17:00 |
| 対象レベル | Intermediate〜Advanced |

Moore 氏は前作 Tony Hawk's Pro Skater 1+2 でも GDC 2020 で「3D + GPU Optimization」というセッションを発表しており、今回はその「続編」にあたる。前作で確立したシャドウ分類や環境リフレクション最適化をベースに、今作ではクラスタードライティングの根本的再設計、GCN アセンブリレベルのチューニング、Switch 2 という新プラットフォームへの対応、そしてデータドリブンなパフォーマンス管理体制の構築まで踏み込んだ。

---

### この記事で読めること

- **プロジェクトの背景**
- **タイルベースシャドウ分類**
- **クラスタードライティングの高速化**
- **コンピュートバリアの排除**
- **GCN アセンブリマイクロ最適化**
- **Switch 2 対応**
- **シェーダーパーミュテーション削減**
- ...ほか全16セクション

> **本記事のボリューム**: 約47,177文字 / スライド画像13枚
> スピーカーのトランスクリプト（発言の文字起こし）を原文・日本語訳つきで完全収録しています。

---

<!-- ===== ここから有料エリア（Note エディタで有料ラインを設定） ===== -->

## 1. プロジェクトの背景 -- なぜ6プラットフォーム対応は大変なのか

### スケートゲーム特有の描画課題

Tony Hawk シリーズの描画が難しい理由は、一般的な TPS（Third-Person Shooter）とは異なるカメラの動きにある:

- **高速なカメラ移動**: トリック中のカメラはスケートパーク全体を一瞬で見渡す。遠景のカリング（描画対象から除外する処理）が効きにくい
- **開放的なレベルデザイン**: 壁や建物で視界を遮る FPS と異なり、スケートパークは見通しが良く、一度に描画するオブジェクト数が多い
- **120fps モード**: Gen9（PS5 / Xbox Series X）では 120fps をサポート。フレームバジェット（1フレームに使える時間）が通常の 16.6ms から **8.3ms** に半減し、最適化の圧力が倍増する

### 6つのプラットフォーム

| プラットフォーム | GPU アーキテクチャ | 主な制約 |
|:---|:---|:---|
| **Xbox One / PS4** | AMD GCN（Wave64） | メモリ・GPU 性能が最もタイト。前世代だが依然としてユーザーベースが大きい |
| **Xbox Series S** | AMD RDNA 2 | Gen9 のエントリーモデル。解像度と性能のバランス |
| **Xbox Series X / PS5** | AMD RDNA 2 | 120fps モードの維持が最大の課題 |
| **Switch 1** | NVIDIA Maxwell 系 | 全プラットフォーム中で最も低スペック。Dynamic Resolution（動的解像度調整）必須 |
| **Switch 2** | NVIDIA 次世代（Wave32） | 開発サイクル後半に公表。既存のAMDベース最適化が通用しない |
| **PC** | 多種多様 | 古い GCN から最新 GPU まで幅広いハードウェアレンジ |

Moore 氏は冒頭で重要な前提を述べた:

> 「この講演は最適化の話だが、ゲーム固有のコンテキストが重要だ。**どんなエンジンも、そのゲームが必要とする形では出荷されない。** ライセンスエンジンの共有コードベースには効率的なメリットがあるが、タイトル固有のカスタマイズは常に必要だ。」

### 前作からの引き継ぎ

GDC 2020 の前作セッションでは、シャドウバッファによるタイル分類、環境リフレクションの最適化、SSR（Screen Space Reflections：画面上の情報だけで映り込みを計算する手法）の効率化などを発表した。今作ではこれらを Unreal Engine のディファードシェーディングパイプライン（Deferred Shading：ジオメトリ描画とライティング計算を分離する描画手法）にさらに深く統合し、全プラットフォームで推し進めた。

---

## 2. タイルベースシャドウ分類 -- 「日向のピクセルにシャドウ計算は無駄」

### どんな問題か

太陽光（ディレクショナルライト）の影を計算するには、画面上の全ピクセルに対して「このピクセルは影の中か？」を判定する必要がある。これは**シャドウバッファ**（光源の視点から見た深度情報を格納したテクスチャ）を参照する処理で、フレームバジェットの中で大きな比重を占める。

しかし、実際の画面を見ると:
- 空や屋根の上面のように**確実に日向**のエリアが大量にある
- 建物の影が完全にかかった**確実に日陰**のエリアもある
- 影の境目に位置する**混在エリア**だけが、本当にシャドウ計算が必要

前作では独立したパスでタイル分類を行っていたが、今作ではクラスタードライティングのクラシファイヤシェーダー（後述）に統合し、シャドウバッファの解析を追加。各タイルを3つに分類する。

### スライドの詳細

![タイル分類の可視化](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_0994.jpg)

スライドのタイトルは **「TILE CLASSIFICATION: DIRECT LIGHTING」**。左右に2つの画面が並んでいる。

**左側**: ゲーム内のスケートパーク -- 前作でも使われた Warehouse ステージの可視化。シャドウバッファの値がシアン（水色）のトーンで表示されており、影の境界線がくっきりと見える。中央にスケーターのシルエットが小さく映っている。

**右側**: 同じシーンにタイル分類の結果をオーバーレイした可視化。ゲームの HUD（SCORE: 0、SPECIAL、タイマー 1:23）が見え、実際のゲームプレイ中のフレームであることがわかる。スライド上に赤いラベルと矢印で以下の注釈が付いている:

- **「FULLY LIT (SKIP SHADOW BUFFER SAMPLE)」** -- 画面上部・右側の**黄色〜緑のタイル群**を指す。完全に日向と判定されたタイルで、シャドウバッファのサンプリングを完全にスキップする
- **「FULLY SHADOWED (SKIP TILE)」** -- 画面下部・左側の**暗い領域のタイル**を指す。完全に日陰と判定されたタイルで、タイル全体のシャドウ計算をスキップする

黄色と緑の間にある**くすんだ色のタイル**が Mixed（混在）で、これだけが通常のシャドウ計算を実行する。影の不連続面（影の境界線）に沿った薄いバンドのみが Mixed になっていることがわかる。

### トレードオフ: コンピュートベース分類の利点と欠点

前作のトップダウン分類パスを完全に廃止せず、クラシファイヤシェーダーに統合する形をとった理由は、コンピュートベースの分類には以下のトレードオフがあったためだ:

- **利点**: クラシファイヤとの統合でパス数が減り、GPU タイムラインの空きスロットを活用できる
- **欠点**: メモリバリア（GPU のメモリ同期命令）のコスト、ランダムアクセスパターンによるキャッシュ効率の低下
- **GCN 固有の問題**: `DeviceMemoryBarrier`（UAVメモリバリア）はワークグループ内の同期しか保証せず、異なるディスパッチグループ間ではフルGPUフラッシュが必要

### 結果: 「期待ほどではなかった」

| 項目 | 数値 |
|:---|:---|
| 分類処理のコスト | ~0.25ms |
| ディレクショナルライティングの改善 | ~0.06ms |

Moore 氏は正直に「期待ほどの改善ではなかった」と述べた。分類自体は0.25ms で終わるが、得られる改善が 0.06ms では投資対効果が薄い。原因は、Unreal Engine のライティングシェーダーが多数の**パーミュテーション**（ライトの種類や設定の組み合わせごとに生成される別バージョンのシェーダー）に分かれており、各パーミュテーション内の分岐処理が依然として重かったためだ。

この「期待外れ」が、次のクラスタードライティング改善への直接の動機となった。

---

## 3. クラスタードライティングの高速化 -- Flat Bit Array とスカラー化

### どんな問題か

本作のステージには1ビューあたり最大 **264 個のダイナミックライト**（街灯、ネオン、車のヘッドライトなど）、1クラスタあたり最大 **30 ライト**が存在する。各ピクセルに「どのライトが影響するか」を判定するために、**クラスタードライティング**（3D空間をブロックに分割し、各ブロックに影響するライトリストを事前計算する手法）を使っている。

前作ではライトリストを**リンクリスト**（各ノードが次のノードを指すチェーン構造）で管理していた。Gen8 / Gen9 では機能していたが、Switch 1 の 30fps ターゲットや Gen9 の 120fps モードでは、以下の問題が顕著になった:

- **メモリアクセスがバラバラ**: リストのノードを辿るたびにメモリの異なる位置にジャンプする。GPU は大量のスレッドが同じアドレスを読むときに最も効率的なので、不規則なアクセスは帯域の無駄遣い
- **スカラー化ができない**: GPU は通常 32〜64 スレッドを束（**Wave / ウェーブ**）にして同時実行する。束の全員が同じデータを読むなら、1回の読み込みで済む（「スカラー化」）。しかしリンクリストでは各スレッドが異なるノードを参照するため、スカラー化が効かない

![リンクリスト vs Flat Bit Array の比較](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/diagram_flat_bit_array.png)
*左: リンクリスト方式ではメモリアクセスが不規則でキャッシュが効かない。右: Flat Bit Array では 2×uint32 の固定サイズで管理し、WaveAllBitOr() で全スレッドのビットマスクを統合してスカラー化する*

### スライドの詳細: Flat Bit Array Culling

![Flat Bit Array カリング](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_0997.jpg)

スライドのタイトルは **「FLAT BIT ARRAY CULLING」**。背景にシェーダーのソースコードが表示され、3つの赤いラベルが矢印で重要箇所を示している。コードを上から順に読み解く:

#### ラベル1: 「SINGLE PROBE FAST PATH」（上部）

```hlsl
// fast path when there is only 1 probe in scene
#if !(USE_FLAT_BIT_ARRAY_CULLING) && defined(USE_SINGLE_REFLECTION_PROBE)
    && USE_SINGLE_REFLECTION_PROBE
    NumCapturesAffectingTile = 1;
#endif
```

リフレクションプローブ（環境の映り込みを計算するためのキャプチャポイント）が1つだけの場合、クラスタデータの読み込み自体をスキップする高速パス。Gen9 のシーンではこのケースが多い。`NumCapturesAffectingTile = 1` とハードコードすることで、クラスタ構造の走査を完全に省略する。

#### 中央部: 2つのコードパスの対比

```hlsl
#if USE_FLAT_BIT_ARRAY_CULLING
    // iterate each word in the bitmask array
    LOOP
    for (uint BitMaskIndex = 0; BitMaskIndex < NUM_CULLED_LIGHTS_GRID_STRIDE;
         BitMaskIndex++)
#else
    // Accumulate reflections from captures affecting this tile,
    // applying largest captures first so that the smallest ones
    // display on top
    LOOP
    for (uint TileCaptureIndex = 0;
         TileCaptureIndex < NumCapturesAffectingTile;
         TileCaptureIndex++)
#endif
```

`#if USE_FLAT_BIT_ARRAY_CULLING` で新方式（ビットマスク配列のイテレーション）と旧方式（リンクリストの走査）を切り替えている。新方式の `NUM_CULLED_LIGHTS_GRID_STRIDE` は **2**（2つの 32-bit 整数 = 64 ライト分）。

#### ラベル2: 「FIXED SIZE: TWO 32-BIT ARRAYS」

```hlsl
uint BitMask = FlatArrayBitMasks[BitMaskIndex];
```

各クラスタのライト情報を、たった **2つの uint32**（合計 64 ビット）で管理する。各ビットが1つのライトに対応し、ビットが立っていれば「そのライトは影響する」。リンクリストと比較して固定サイズなのでメモリ割り当てが予測可能で、キャッシュに乗りやすい。

#### ラベル3: 「SCALARIZED LOOP」（下部）

```hlsl
#if COMPILER_SUPPORTS_TO_SCALAR_MEMORY && COMPILER_SUPPORTS_WAVE_BIT_ORAND
    // unify to scalar loads / and registers across tile in the
    // light bitmask, even if that means a few extra probes on
    // some pixels
    BitMask = ToScalarMemory(WaveAllBitOr(BitMask));
#endif
    while ( BitMask != 0 )
    {
        uint BitIndex = firstbitlow( BitMask );
        uint CaptureIndex = 32 * BitMaskIndex + BitIndex;
        BitMask ^= ( 1 << BitIndex );
```

ここが Flat Bit Array の核心だ。`WaveAllBitOr(BitMask)` は Wave 内の**全スレッドのビットマスクを OR 演算で統合**する組み込み関数。これにより:

1. Wave 内の全スレッドが**同じ BitMask** を持つことになる
2. 全員が同じライトを同じ順番で処理するため、ループが**スカラー化**される
3. メモリロードがベクターレジスタ（各スレッド個別）からスカラーレジスタ（Wave 全体で共有）に昇格し、帯域使用量が劇的に減る

コメントに「even if that means a few extra probes on some pixels（一部のピクセルで余分なプローブを処理することになっても）」とあるのは重要な設計判断だ。隣のスレッドが必要としないライトも処理することになるが、スカラー化による帯域削減のほうが遥かに大きいというトレードオフだ。

`firstbitlow()` でセットされている最下位ビットのインデックスを取得し、`BitMask ^= (1 << BitIndex)` でそのビットをクリアして次のライトに進む。

### 効果: タイル分類と組み合わせて「期待通り」に

| 項目 | 改善量 | 解説 |
|:---|:---|:---|
| リフレクションパス | ~0.25ms 改善 | Wave のオクピュパンシー（稼働率）向上による |
| Single Probe Fast Path | 0.05〜0.14ms 節約 | タイル分類との複合効果 |
| コードパス | 統一化 | 全パーミュテーションが同じ Flat Bit Array のコードに一本化 |

Moore 氏は「タイル分類単体では物足りなかったが、Flat Bit Array と組み合わせて初めて期待通りの結果になった」と述べた。**複数の最適化の相乗効果で目標を達成する** -- これがマルチプラットフォーム最適化の現実だ。

---

## 4. コンピュートバリアの排除 -- リングバッファ方式

### どんな問題か

クラスタードライティングでは、複数のコンピュートシェーダーが連鎖的に実行される。前段の出力を後段が参照するため、「前段が書き終わるまで後段は待て」という同期（**コンピュートバリア**）が必要だ。しかし:

- `DeviceMemoryBarrier` はワークグループ（GPU の小さな実行単位）内のメモリ同期しか保証しない
- 異なるディスパッチグループ間の同期には Full GPU Flush が必要で、パイプライン全体がストールする
- 特に Xbox One / PS4 世代では、このストールがフレームバジェットを圧迫していた

### 解決策: リングバッファ

バリアの代わりに**リングバッファ**（循環バッファ）を導入。書き込み位置をフレームごとにずらすことで、前段の出力と後段の入力が同じメモリ位置を参照しない構造にした:

```
フレーム 0: スロット A に書き込み → スロット C を読む
フレーム 1: スロット B に書き込み → スロット A を読む
フレーム 2: スロット C に書き込み → スロット B を読む
// 書き込みと読み込みが絶対に衝突しないので、バリア不要
```

少量の追加メモリ（リングサイズ分のバッファ）と引き換えに、GPU の占有率を維持できる。別のディスパッチや適切なクリア処理との組み合わせも可能。Moore 氏はこの手法を「Rumbleverse 時代から改善を重ねてきた」と述べた。

![リングバッファによるバリア排除](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/diagram_ring_buffer.png)
*上: 従来方式ではディスパッチ間にバリア（GPU Flush）が挟まりパイプラインがストールする。下: リングバッファ方式では書き込みと読み込みが常に異なるスロットを参照するため、バリアが不要*

---

## 5. GCN アセンブリマイクロ最適化 -- 「同じコードでも、書き方で速度が変わる」

### どんな問題か

HLSL（GPU のシェーダー言語）で同じ処理を書いても、**バッファの型宣言**によってコンパイラが生成するアセンブリ（GPU が実際に実行する機械語）が大きく変わることがある。Moore 氏は「Rumbleverse 時代に学んだ教訓だが、前作 1+2 で本格的に着手し、3+4 でようやく大きなマイルストーンに達した」と述べた。

### スライドの詳細: One More Micro Optimization

![GCN アセンブリ比較](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1003.jpg)

スライドのタイトルは **「ONE MORE MICRO OPTIMIZATION」**。左右に2つの GCN アセンブリコード列が並列表示されている。

**上部のヘッダ情報**（両列共通）:
```
// CS running as Hu CS (Click here for instruction set d)
// Shader Address: 0x00031C000
// Shader Size: 960 bytes（左）/ 916 bytes（右）
// VGPRs: 24, SGPRs: 32, User SGPRs: 7
// shader-main // DOP: Shader(EndDirect)
```

シェーダーサイズが左（非最適）960 bytes → 右（最適）**916 bytes** と44バイト削減されていることがわかる。

**左列（非最適パス）** -- 赤いラベルが2つの命令を指す:

- **「BUFFER_LOAD_FORMAT_X」**: `buffer_load_format_x v3, v[8:11], s[24:27], 0 idxen` -- これは**ベクターメモリ命令**。ライトインデックスバッファからデータを読み、結果をベクターレジスタ（v3）に格納する。ベクターレジスタは Wave 内の各スレッドが個別に持つレジスタ
- **「V_READFIRSTLANE_B32」**: `v_readfirstlane_b32 s0, v3` -- ベクターレジスタ v3 の値をスカラーレジスタ s0 にコピーする命令。Wave 内の全スレッドが同じ値を参照するはずなので、先頭レーンの値をスカラーに昇格させる

合計 **2命令**。ベクター → スカラーの2段階で、1命令分のレイテンシと命令スロットが無駄になっている。

**右列（最適パス）** -- 赤いラベルが1つの命令を指す:

- **「S_BUFFER_LOAD_DWORD」**: `s_buffer_load_dword s1, s[8:11], s0` -- **スカラーメモリ命令**。最初からスカラーレジスタ（s1）に直接ロードする。中間のベクター操作が不要

合計 **1命令**。同じデータを取得するのに、命令数が半減している。

### なぜ1命令の差が重要なのか

この処理は**ディファードライティング**の中で実行される -- つまり画面上の全ピクセルで走るシェーダーだ:

| 解像度 | ピクセル数 | 120fps バジェット |
|:---|:---|:---|
| 1080p | ~200万 | 8.3ms |
| 4K | ~800万 | 8.3ms |

1命令の差が200万〜800万回掛け算される。さらにディファードライティングにはライトタイプごとの複数パーミュテーションがあるため、差は累積する。

### 原因: HLSL の型宣言

| HLSL での書き方 | コンパイラが生成するアセンブリ | 命令数 |
|:---|:---|:---|
| `StructuredBuffer<uint>` | `buffer_load_format_x` + `v_readfirstlane_b32` | 2命令 |
| `ByteAddressBuffer` + 手動オフセット | `s_buffer_load_dword` | 1命令 |

`StructuredBuffer<uint>` はコンパイラに「各要素が構造化データ」と解釈され、ベクターロードが選択されやすい。`ByteAddressBuffer` はバイト単位の生アクセスで、コンパイラがスカラーロードを選びやすい。同じデータを読んでいるのに、型宣言だけでアセンブリが変わる。

> **パフォーマンスクリティカルなシェーダーでは、必ずコンパイラのアセンブリ出力を確認せよ。HLSL の書き方が「正しい」かどうかと、最適なアセンブリが生成されるかどうかは別問題だ。**

---

## 6. Switch 2 対応 -- 「遅れてきた新プラットフォーム」と Rasterized Quads

### Switch 2 がもたらした新たな課題

Switch 2 は開発サイクルの後半に公表された。Moore 氏は「遅いスタートだったが、ローンチ日に高品質で出荷するという強い意志があった」と述べている。

問題は、Switch 2 がそれまでの全コンソール（AMD GCN / RDNA ベース）とは根本的に異なるアーキテクチャだったことだ:

| 特性 | PS4 / Xbox One（GCN） | Switch 2（NVIDIA） | 影響 |
|:---|:---|:---|:---|
| GPU メーカー | AMD | NVIDIA | シェーダーの挙動が根本的に異なる |
| Wave サイズ | 64（Wave64） | 32（Warp） | ビット操作のパターンが変わる |
| VK_NV_fill_rectangle | 非対応 | 対応 | 新しい描画最適化が可能に |

多くの最適化コードは Wave サイズの変更だけで動作したが、**SSR（Screen Space Reflections）** が最も重いビューで **3倍遅く** なるという深刻な問題が発生。これを解決したのが Rasterized Quads だ。

### スライドの詳細: Rasterized Quads

![Rasterized Quads の比較図](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1006.jpg)

スライドのタイトルは **「RASTERIZED QUADS」**。上部に5つのポイント、下部に視覚的な比較図がある。

**スライド上部のテキスト**:

1. 「Rasterizing quads with **DrawIndirect** instead of DispatchIndirect allows efficient usage of blending hardware and thread ordering of the hardware rasterizer」-- DrawIndirect（ラスタライザ経由の描画）を使うことで、ハードウェアのブレンディング機能とスレッドオーダリングを活用できる
2. 「2-triangle quads results in overshading along triangle edge – **problem becomes worse the smaller the quads are**」-- 2三角形方式のオーバーシェーディングは、タイルサイズが小さくなるほど悪化する（エッジ比率が増えるため）
3. 「NVIDIA GPUs have supported quad primitives for some time – see **VK_NV_fill_rectangle**」-- NVIDIA は VK_NV_fill_rectangle 拡張でこの機能をサポート
4. 「Works as a new rasterization mode where each triangle has its **bounding rectangle rasterized**」-- 三角形の代わりにバウンディング矩形をラスタライズする新モード
5. 「**Switch 2 rendering API conveniently exposes this**」-- Switch 2 の描画 API がこの機能を提供

**下部の比較図**:

**左図**: 8×8 のタイルを2つの三角形で描画。対角線のエッジに沿って**黄色のセル**（ヘルパーピクセル）が表示されている。GPU はピクセルシェーダーを 2×2 の Quad 単位で実行するため、三角形のエッジが Quad の一部しかカバーしない場合でも全4ピクセルが実行される。エッジ外のピクセルが「ヘルパーレーン」で、計算結果は破棄されるが GPU リソースは消費する。

```
8X8 2-TRIANGLE TILE
78 PIXELS
(16 HELPER PIXELS)
25% OVERSHADING
```

**右図**: 同じ 8×8 タイルを1つの矩形で描画。全セルが**緑色**（有効ピクセル）で、黄色のヘルパーピクセルがゼロ。

```
8X8 1-RECT TILE
64 PIXELS
(0 HELPER PIXELS)
0% OVERSHADING
```

### 技術的メカニズム

`VK_NV_fill_rectangle` は、三角形のバウンディング矩形をそのまま塗りつぶすラスタライゼーションモードだ。通常のラスタライザは三角形の内側だけを塗るが、このモードでは矩形全体を塗る。結果として:

1. 三角形のエッジが存在しないので、ヘルパーレーンが**完全にゼロ**になる
2. `DrawIndirect`（GPU駆動の描画命令）と組み合わせることで、ハードウェアラスタライザのブレンディングとスレッドオーダリングをそのまま活用できる
3. 手動のブレンド処理やリング状のアーティファクトが発生しない

### 16×16 タイル化との組み合わせ

オーバーシェーディングが 0% になったことで、タイルサイズを 8×8 → **16×16** に拡大できた。スライドのポイント2にある通り「タイルが小さいほどオーバーシェーディングが悪化する」ので、逆にタイルを大きくすることでクラシファイヤパスの実行量が **1/4** に削減される。

### Switch 2 での実測結果

| 項目 | 結果 |
|:---|:---|
| Rasterized Quads の効果 | パスによって **20〜70% 改善**（SSR で最大効果） |
| 出荷品質 | Day 1 のローンチタイトルとして十分なパフォーマンス |
| 唯一出荷しなかった最適化 | `WaveAllBitOr` を使った Flat Bit Array のスカラー化。Switch 2 ではクロスレーン操作が非効率だった |

Moore 氏はプラットフォームごとに最適化の取捨選択を行った。GCN で効果的だった Wave64 スカラー化は、Switch 2 の Wave32 では逆効果になる場合があり、出荷しない判断をしている。

---

## 7. シェーダーパーミュテーション削減 -- 「バリエーションが多すぎると GPU が遊ぶ」

### どんな問題か

Unreal Engine のディファードライトシェーダーは、条件の組み合わせごとに別バージョン（パーミュテーション）を生成する:

- ライトの種類: ポイント / スポット / ディレクショナル
- `USE_LIGHT_FUNCTION` の ON/OFF
- シャドウの有無
- その他多数

パーミュテーション数が多いと、各パーミュテーションを実行する GPU スレッド数が少なくなり、**GPU の実行ユニットが半分遊んでいる**状態（低オクピュパンシー）になる。さらに、タイル分類の効果が減殺される -- 多くのタイルが複数のパーミュテーションにまたがるため「未分類」に落ちてしまう。

### 解決策

ライトタイプごとに異なるパーミュテーションを生成するのではなく、ランタイムの `if` 分岐で処理を切り替えるようにシェーダーを統合。パーミュテーション数が減ることで、各パーミュテーションに割り当てられる GPU スレッド数が増え、稼働率が向上した。

### 効果

約 **0.4ms** の改善を確認。タイル分類で「未分類」ピクセルの割合が高いビューほど効果が大きかった。Moore 氏は「前作 1+2 の時から見つけたかった最適化だが、3+4 でようやくたどり着いた」と述べた。

---

## 8. 自動パフォーマンスダッシュボード -- 「6プラットフォーム × 全ステージを毎晩チェック」

### どんな問題か

6プラットフォーム × 数十ステージの全組み合わせを人力で確認するのは不可能だ。レベルデザイナーが毎日コンテンツを追加する中で、「昨日のコミットで Tokyo ステージの Switch 1 だけ 3ms 遅くなった」といったリグレッション（性能劣化）を素早く検知する仕組みが必要だった。

### スライドの詳細: Data Collection and Visualization

![パフォーマンスダッシュボード](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1010.jpg)

スライドのタイトルは **「DATA COLLECTION AND VISUALIZATION」**。Moore 氏が Python で構築したブラウザベースのダッシュボード「**Combo Gauntlet Data Browser**」のスクリーンショットが全面に表示されている。

**左上の設定パネル**:
- Time Series: **GPU Time**
- Statistic: **95th Percentile**（上位5%の最悪フレーム）
- CL | Date: **CL: 47530 | Date: 11/22/2024**
- □ Show Delta To Current

**右上のパネル**: Current Selection で特定のセルが選択されている状態（Config: Xbox One, Map: College）。「**Plot Details**」「**Show History**」「**Play Video**」のボタンがあり、詳細な分析が可能。

**メインテーブル**: 行が**全マップ**（19ステージ）、列が**全プラットフォーム**の巨大なマトリクスだ。

列ヘッダ（左から）: Map/Config → **Xbox One** → **PS4** → **XSS Performance** → **XSS Quality** → **XSX Performance** → **XSX Quality** → **PS5 Performance** → **PS5 Quality** → **Switch** → **Switch 2**

行に並ぶマップ名を読み取ると:

| マップ | Xbox One | PS4 | Switch | Switch 2 |
|:---|:---|:---|:---|:---|
| Airport | 14.75 | 13.83 | 33.19 | 15.43 |
| Alcatraz | 15.90 | 13.95 | 32.90 | 19.29 |
| Canada | 16.40 | 14.55 | 43.36 | 18.34 |
| College | 19.21 | 16.49 | -- | 15.99 |
| CruiseShip | 15.89 | 14.63 | 35.42 | 15.82 |
| Foundry | 15.92 | 13.70 | 33.25 | 17.88 |
| Kona | 16.01 | 14.54 | 46.29 | 19.90 |
| London | 16.76 | 16.01 | 42.26 | 18.30 |
| LosAngeles | 16.50 | 14.43 | 39.98 | 17.05 |
| MovieStudio | 14.00 | -- | 32.19 | 16.29 |
| Pinball | 17.24 | 15.83 | 47.14 | 19.26 |
| Rio | 17.14 | 15.83 | -- | 19.26 |
| SanFrancisco | 16.29 | 14.84 | 42.86 | 19.14 |
| Shipyard | 15.63 | 13.75 | 33.26 | 18.70 |
| SkaterIsland | 16.25 | 15.37 | 33.02 | 19.24 |
| Suburbia | 15.00 | 12.89 | 33.09 | 18.34 |
| Tokyo | 16.05 | 12.83 | 32.51 | 15.83 |
| WaterPark | 16.19 | 14.39 | 37.48 | 18.15 |
| **Zoo** | **19.91** | **16.92** | **58.80** | **17.02** |

セルは色分けされている: **緑** = バジェット内、**黄色** = 注意、**赤/オレンジ** = バジェットオーバー。一目で分かるのは:

- **Switch 列が最も赤い**: 33〜58ms の範囲で、Zoo が 58.80ms と突出。16.6ms（60fps）の3倍以上で、Dynamic Resolution が限界に達している
- **Xbox One / PS4 は概ね 14〜19ms**: 16.6ms バジェットに対して微妙なラインの値が多い
- **Switch 2 は 15〜19ms**: Switch 1 より大幅に改善しているが、まだ最適化の余地がある

### ダッシュボードの運用

1. **毎晩の自動スモークテスト**: 全ステージを全プラットフォームで自動実行し、デザイナーが設定したカメラパスでプロファイルデータを収集。特定のコミット（CL番号）に対してトリガーでき、リグレッションの原因特定が可能
2. **95th パーセンタイル表示**: 平均値ではなく最悪ケースで評価。Moore 氏は「バジェット内フレーム率だけでは、ホットスポットがどれほど深刻か分からない」と述べた
3. **セルクリックで詳細展開**: Dynamic Resolution の下限に達しているフレーム、CPU スレッドによるブロック箇所がタイムラインで特定できる
4. **ローカル実行対応**: エンジニアがローカルで最適化した結果を同じブラウザに読み込み、ナイトリーデータと比較。「推測」ではなく「計測」で判断

---

## 9. Turning Analysis Into Action -- 「データを集めるだけでは最適化は進まない」

Moore 氏がセッション中で最も力を込めて語ったのが、このセクションだった。技術的な最適化よりも、**データをどう行動に変えるか**というプロセスの話だ。

![データドリブン最適化サイクル](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/diagram_optimization_cycle.png)
*Moore 氏の最適化プロセス全体像。データ収集→可視化→分析→ストライクプラン→協業実行→検証の循環サイクル。ストライクプランなしでアーティストに丸投げすると、出荷に必要以上にコンテンツが痩せるアンチパターンに陥る*

### スライドの詳細

![分析から行動へ](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1013.jpg)

スライドのタイトルは **「TURNING ANALYSIS INTO ACTION」**。4つのポイントが列挙されている:

1. **「Needed to efficiently communicate the work needing to be done on stages to get them shippable on all platforms and configurations」** -- 全ステージを全プラットフォーム・全設定でシッパブルにするための作業を、チームに効率的に伝達する必要があった

2. **「Direction is critical to content optimization. If artists merely cut until all the counters turn green →」** ここから**赤太字**で: **「the cuts will be more severe than needed to ship the stage」** -- **方針（Direction）がコンテンツ最適化には不可欠。もしアーティストがカウンターが緑になるまで切り続けるだけなら、出荷に必要以上の削減になってしまう。** これはスライド中で唯一赤太字で強調されたフレーズで、Moore 氏がこのセッションで最も伝えたかったメッセージだ

3. **「Highly effective to have engineers author strike plan docs for stages that described the problems the stage was facing and suggest potential paths to optimization」** -- エンジニアが各ステージの問題を記述し、最適化パスを提案する「ストライクプラン」ドキュメントが非常に効果的だった

4. **「In practice, optimization often is most effective as a joint effort of code and content modifications」** -- 実際にはコード修正とコンテンツ修正の協業が最も効果的

**スライド下部**: 8枚のストライクプランドキュメントのサムネイルが横に並んでいる。左から3枚はテキスト中心のドキュメント（問題の記述と分析）、中央には「More Studio Optimizers」というタイトルのものと「Set Importance/Final Targets on Client Platform (GPUTimePerFrame)」というタイトルのもの、右側にはステージのスクリーンショット付きの具体的なプランが表示されている。

### ストライクプランの運用

エンジニアがステージごとに作成する「最適化処方箋」:

1. **問題の診断**: ダッシュボードの数値をもとに「Tokyo ステージの Switch 1 では、ライトマップのインスタンシング効率が悪く CPU 時間が 2ms 超過」のように具体的に記述
2. **改善候補の提案**: 定量的な選択肢を複数提示（「フォリッジ密度 20% 減で 1ms 改善」「ライトマップ解像度変更で 0.5ms」など）
3. **判断はアーティストに委ねる**: どの選択肢を採用するか、品質との妥協点はアーティストが決める

この協業モデルが重要な理由: アーティストに「全部緑にしろ」と丸投げすると、最も削りやすいが最もビジュアルインパクトの大きい要素（フォリッジ、ライティング）から削っていくため、**出荷に必要以上にコンテンツが痩せる**。エンジニアが「何を削ればどれだけ軽くなるか」の技術的指針を示すことで、アーティストは最もインパクトの少ない削り方を選べる。

---

## 10. ケーススタディ: Tokyo ステージ -- 「最も困難なレベル」

Tokyo は本作の中で最もパフォーマンス的に困難なステージだった。密集した建物、大量のネオンサイン（ダイナミックライト）、複雑なフォリッジが組み合わさり、特に Switch 1 と Xbox One で深刻な問題を抱えていた。

### ダイナミック/ベイクドライティングの動的切り替え

プラットフォームの性能に応じて、同じライトを「ダイナミック（毎フレームリアルタイム計算）」と「ベイクド（事前計算してライトマップに焼き込み）」に切り替える仕組みを導入した:

> 「プラットフォームによって、より多くのライトをベイクドにすることでランタイムの計算コストを削減している。」

Switch 1 ではネオンサインの大部分をベイクドライトにすることで、Tokyo の夜景の雰囲気を保ちつつ GPU 負荷を抑えている。3段階のライティング LOD で、プラットフォームに応じて適切な品質レベルを選択する。

### スライドの詳細: ライトマップパッキング

![Tokyo ステージのライトマップパッキング](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1017.jpg)

スライドのタイトルは **「CASE STUDY: TOKYO」**、サブタイトルは **「Solution 2: Rework lightmap packing」**。

**3つのポイント**:

1. **「Increase atlas page size – can have a negative effect on texture streaming. Mitigated by clamping top mip on most memory constrained platforms」** -- アトラスページサイズを拡大。テクスチャストリーミングへの悪影響はメモリ制約のあるプラットフォーム（Switch 1）でトップ MIP（最高解像度のミップマップレベル）をクランプ（制限）することで緩和

2. **「Change packing logic to favor instancability of meshes within a page over spatial locality. Can result less optimal packing of charts!」** -- パッキングロジックを**空間的局所性より、インスタンサビリティ（同じメッシュをバッチ描画できる性質）**優先に変更。チャート（個々のライトマップ矩形）のパッキング効率は若干低下する可能性がある

3. **「Saved 2 ms of CPU time on Switch in Tokyo in our most challenging views」** -- 結果: **Switch 上で CPU 時間 2ms を削減**（最も困難なビューで計測）

**下部のソースコード**: C++ のライトマップマージロジックが表示されている。`//@lgs(lgsocr) - START` から `END` までのブロック:

```cpp
// merge lightmaps of identical meshes into the same group
for (int TargetIndex = 0; TargetIndex < PendingLightMaps.Num();
     TargetIndex++)
{
    for (int CandidateIndex = TargetIndex + 1;
         CandidateIndex < PendingLightMaps.Num(); CandidateIndex++)
    {
        if ((PendingLightMaps[CandidateIndex].Outer
                == PendingLightMaps[TargetIndex].Outer)
            && (PendingLightMaps[CandidateIndex].InstancingHash
                == PendingLightMaps[TargetIndex].InstancingHash)
            && (PendingLightMaps[CandidateIndex].LightmapFlags
                == PendingLightMaps[TargetIndex].LightmapFlags)
            && (PendingLightMaps[CandidateIndex].Allocations.Num()
                == 1))
        {
            const FBoxSphereBounds NewBounds =
                PendingLightMaps[TargetIndex].Bounds
                + PendingLightMaps[CandidateIndex].Bounds;

            // Don't pack together lightmaps that are too far apart
            float MaxDistance = MaxLightmapRadius * 2.0f;
            // but double the distance constraint to encourage instancing
            if (NewBounds.SphereRadius <= MaxDistance
                || NewBounds.SphereRadius
                   <= (PendingLightMaps[TargetIndex].Bounds
                       .SphereRadius + SMALL_NUMBER))
            {
                PendingLightMaps[TargetIndex].Bounds = NewBounds;
                PendingLightMaps[TargetIndex].Allocations.Add(
                    MoveTemp(PendingLightMaps[CandidateIndex]
                             .Allocations[0]));

                PendingLightMaps.RemoveAt(CandidateIndex);
                CandidateIndex--; // need to re-check at same index
                                  // after shrinking
            }
        }
    }
}
```

このコードのロジックを解説する:

1. **マージ条件**: `Outer`（所属アクター）、`InstancingHash`（メッシュの形状ハッシュ）、`LightmapFlags` が全て一致し、Allocation が1つだけのライトマップをマージ対象とする
2. **距離制約の緩和**: `MaxDistance = MaxLightmapRadius * 2.0f` -- コメントに「double the distance constraint **to encourage instancing**（インスタンシングを促進するために距離制約を2倍に緩和）」とある。従来は近接するメッシュだけをまとめていたが、離れたメッシュでも同じメッシュ型なら同じページに入れる
3. **バウンド統合**: `FBoxSphereBounds` の加算でマージ後のバウンディング球を計算し、距離制約をチェック
4. **配列の縮小**: `RemoveAt` でマージ済みの候補を除去し、`CandidateIndex--` でインデックスを再調整

従来は空間的局所性（近いメッシュを同じページに配置）を優先していたため、同じベンチが3脚あっても3枚の別ページに分散していた。改善後はインスタンサビリティ優先で、同じメッシュ型のライトマップを同じページに集約する。

---

## 11. 重複メッシュ検出 -- 「見た目は完璧、コストは倍」

### スライドの詳細

![重複メッシュ検出システム](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1024.jpg)

スライドのタイトルは **「DUPLICATED MESHES」**。5つのポイントが列挙されている:

1. **「I'm always warning content people that one of my least favorite content bugs is duplicated meshes placed directly on top of one another」** -- コンテンツチームに常に警告している。最も厄介なコンテンツバグの一つが、完全に同じ位置に重ねて配置されたメッシュだ

2. **「Visually correct results means QA will generally never find this type of issue」** -- 見た目が正しいため、QA は通常この種の問題を発見できない

3. **「I'm convinced every engine should have content validation that ensures this is not happening」** -- **あらゆるエンジンにこの問題を防ぐコンテンツバリデーションを実装すべき**だと確信している

4. **「Implemented system that identifies and removes duplicated placed foliage and static meshes that are fully static during save/map check」** -- 保存/マップチェック時に、重複配置されたフォリッジとフルスタティックメッシュを自動検出・除去するシステムを実装

5. **「Output warning for meshes that might be duplicated intentionally」** -- 意図的に重複配置されている可能性があるメッシュに対しては警告を出力

**スライド中央の3つのビジュアル要素**:

- **左**: Unreal Engine の InstancedFoliageActor 設定画面。「World Settings」パネルに「Default Update Overlaps: Every/Update/Mismatch?」の設定と、**「Clear Duplicate Instances」** ボタン（青枠でハイライト）が表示されている
- **中央**: フォリッジインスタンスのリスト画面。多数のインスタンスが並び、同一位置のものがハイライトされている
- **右上**: ゲーム内スクリーンショット。2つの同一メッシュが完全に重なり、**Z-Fighting**（深度値の競合による虹色のちらつき）が発生している。フェンスのレール部分に虹色のフリンジが見える

**スライド下部の検出ログ**（青枠内）:

```
⚠ SP_ALK_Chopper_2  Bare overridden materials (2) on static mesh
  component that are referenced (1) in source mesh
  'SM_OM_Rotor_Main_Bladeless'

⚠ SM_ALK_UpperCourtyard_0  Bare overridden materials (2) on static
  mesh component that are referenced (1) in source mesh
  'SM_ALK_UpperCourtyard_Railings_01'

⚠ SM_ALK_UpperCourtyard_0  SM_ALK_UpperCourtyard_03 (LOD 2) has
  hand-painted vertex colors that no longer match the original
  StaticMesh (SM_ALK_UpperCourtyard_10)

⚠ InstancedFoliageActor_0  contains foliage instances with
  identical transforms. This can be fixed by resaving or
  🔗 clicking here

⚠ SM_ALK_Restrooms_01_1  SM_ALK_Restrooms_01-2 (LOD 2) has
  hand-painted vertex colors that no longer match the original
  StaticMesh (SM_ALK_Restrooms_01)
```

各ログが検出した問題の種類:

- **「Bare overridden materials」**: ソースメッシュで参照されているマテリアル数と、コンポーネント上のオーバーライド数が不一致。不要なマテリアルオーバーライドが残っている
- **「hand-painted vertex colors that no longer match the original StaticMesh」**: アーティストが手動ペイントした頂点カラーがオリジナルメッシュと一致しない。インスタンシングバッチが分断される原因になる
- **「contains foliage instances with identical transforms」**: 同一トランスフォーム（位置・回転・スケール）のフォリッジインスタンスを検出。「resaving or clicking here」でワンクリック修正可能

### なぜ重要か

重複メッシュの原因はアーティストの作業フロー（コピー＆ペーストのミス、Undo/Redo 操作の不整合、フォリッジペイントツールの挙動）にあり、見た目が正しいため発見が困難。GPU は2つ描画するので**コストが倍**になる。検出システムの導入により、重複以外にも頂点カラーの不一致やマテリアルオーバーライドの残骸といった隠れた問題が芋づる式に発見された。

---

## 12. その他の最適化

### テクスチャアップロード最適化（コンソール UMA）

コンソール機は CPU と GPU が同じメモリを共有する「ユニファイドメモリアーキテクチャ（UMA）」を採用しており、PC のように PCI バス越しにデータを転送する必要がない。Unreal Engine のデフォルト設定ではこの最適化パスが完全には活用されておらず、正しく設定するだけで **Switch 1 で 4ms のフレーム時間を削減** -- フレームバジェットの約 25% に相当する改善だ。

> 「仕組みを理解していても、実際にコードが期待通り動いているか検証するまで安心できない。」

### Depth Bounds Test の有効化

Switch 1 で Z-Fighting が多発していた原因を調査したところ、`Depth Bounds Test`（深度範囲外のピクセルを早期に棄却する機能）が有効になっていないことが判明。有効化するだけで問題が解消。設定1つで解決するが、**そもそも問題に気づくには計測が必要**という典型例。

---

## 13. On the Cutting Room Floor -- 「やりたかったが、出荷に間に合わなかった」

### スライドの詳細

![Cutting Room Floor](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1025.jpg)

スライドのタイトルは **「ON THE CUTTING ROOM FLOOR」**（映画業界の用語で「編集室の床に落ちたフィルム」= 最終版に入らなかったもの）。3つの技術が箇条書きで列挙されている:

1. **「Raster vs compute tile classification on GCN」** -- GCN 上でラスタライズベースとコンピュートベースのタイル分類のトレードオフをさらに追求する余地があった。Switch 2 で効果があった Rasterized Quads のコンセプトを GCN にも適用できれば、旧世代機でも改善が見込めた

2. **「Improved static shadow caching – quad tree shadows, virtual shadowmaps?」** -- 疑問符付きで記載されているのが印象的。動かないオブジェクトのシャドウを毎フレーム再計算するのは無駄だが、Quad Tree シャドウ（四分木構造でシャドウマップを動的管理する手法）や Virtual Shadow Maps（Unreal Engine 5 の仮想シャドウマップ）への移行は、動的オブジェクトとの整合性やメモリコストの観点で検討段階に留まった

3. **「Anti-ghosting on Switch」** -- TAA（Temporal Anti-Aliasing：前フレームの情報を使ってジャギーを滑らかにする手法）が引き起こすゴースティング（高速移動するオブジェクトの残像）。スケートゲームでは高速カメラ移動が多いため特に目立つ。Switch 1 ではパフォーマンスバジェットが足りず実装できなかった

Moore 氏は最後にこう述べた:

> 「1つのプラットフォームだけのために、出荷スケジュールに影響を与える判断は難しい。」

---

## まとめ -- 3年4ヶ月から得られる5つの教訓

Moore 氏は最後に、プロジェクト全体を通じた教訓を共有した。

### 1. エンジンをゲームに合わせてカスタマイズせよ

> 「どんなエンジンも、そのゲームが必要とする形では出荷されない。」

Unreal Engine のような成熟したエンジンでも、タイトル固有の最適化は常に必要。最適化エンジニアの工数をプロジェクト開始時から計画に組み込むべきだ。

### 2. 仮定を検証せよ

パフォーマンスのベストプラクティスが**実際に**機能しているか、アセンブリ出力やプロファイラで必ず検証する。GCN アセンブリの事例のように、「正しいはず」のコードが非効率な機械語に変換されていることがある。

### 3. データ収集と行動のギャップを埋めよ

自動ダッシュボードでデータを集めるだけでは意味がない。ストライクプランを作成し、アーティストと協業して初めて最適化は進む。

### 4. コンテンツバリデーションは全エンジンに必須

重複メッシュ、Z-Fighting、不適切なライトマップパッキング -- QA では絶対に見つけられないコンテンツ問題を自動検出する仕組みがなければ、描画コストの無駄が積み上がっていく。

### 5. 最適化はチーム全体の責務

> 「全部門がパフォーマンスのために時間を割く必要があることを理解し、計画に組み込まなければならない。コミュニケーションが最も重要だ。」

パフォーマンス最適化はエンジニアだけの仕事ではない。アーティスト、デザイナー、プロダクション -- 全員が当事者意識を持つことで初めて、6プラットフォーム × 全ステージの「全てが緑」が達成される。

![最終ダッシュボード](stock/GDC2026/TonyHawk34GraphicsOpt_Moore/slides/IMG_1010.jpg)
*セッション終盤で再表示された出荷版ダッシュボード。全マップ × 全プラットフォームのセルが緑で埋め尽くされている。3年4ヶ月の最適化の集大成*

---

## Unity エンジニアのための対応ガイド -- 各最適化を Unity でどう実現するか

本セッションの各最適化手法は Unreal Engine 固有の実装に基づいているが、背景にある問題と解決アプローチは Unity でも共通だ。ここでは各セクションの内容を Unity のアーキテクチャに翻訳し、具体的にどう取り組むべきかを解説する。

### 対応表: Unreal → Unity

| セクション | Unreal での実装 | Unity での対応 |
|:---|:---|:---|
| タイルベースシャドウ分類 | ディファードシェーダー内でシャドウバッファ解析 | URP/HDRP の Additional Light / Shadow パスをカスタマイズ |
| Flat Bit Array クラスタードライティング | クラスタ構造をリンクリスト → ビット配列に変更 | HDRP の Light Cluster を拡張、または URP の ForwardPlus を改造 |
| コンピュートバリア排除 | UAV バリアをリングバッファに置換 | CommandBuffer / Compute Shader 間の同期設計 |
| GCN アセンブリ最適化 | HLSL のバッファ型変更でアセンブリ誘導 | Shader Variant の管理 + プラットフォーム別コンパイル確認 |
| Rasterized Quads | VK_NV_fill_rectangle 拡張 | SRP Batcher / GPU Instancing の最適化 |
| シェーダーパーミュテーション削減 | Unreal のライトシェーダー統合 | Shader Variant 爆発の防止（shader_feature / multi_compile 管理）|
| パフォーマンスダッシュボード | Python ブラウザベースの自動計測 | Unity Performance Testing + CI 連携 |
| ストライクプラン | エンジニア-アーティスト協業ドキュメント | Scene 単位のプロファイルレポート運用 |
| ライトマップパッキング | Unreal のライトマップビルドパイプライン改造 | Enlighten / Progressive GPU Lightmapper の設定最適化 |
| 重複メッシュ検出 | 保存時の自動バリデーション | EditorScript / AssetPostprocessor でのバリデーション |

---

### 1. タイルベースシャドウ分類 → Unity: Shadow のカスケード最適化

**Unreal で発生していた問題**: ディレクショナルライトのシャドウ計算を画面上の全ピクセルに対して実行していた。しかし実際のフレームでは、空や屋根の上面のように「確実に日向」のエリアや、建物の影が完全にかかった「確実に日陰」のエリアが大量に存在し、影の境界付近の混在エリアだけが本当にシャドウ計算を必要としていた。タイルごとに分類してスキップする仕組みを導入したが、分類コスト（0.25ms）に対して改善量（0.06ms）が小さく、**投資対効果が薄い**結果になった。原因は Unreal のライティングシェーダーが多数のパーミュテーションに分かれており、分岐処理が依然として重かったため。

**Unity で同様の問題が起きるケース**:

- URP/HDRP で**シャドウカスケードの分割数やシャドウ距離が不適切**なとき。例えば4カスケードを設定しているが、実際にはカスケード3〜4の遠方に影を落とすオブジェクトがほとんどないシーンでは、無駄なシャドウマップ描画とサンプリングが毎フレーム実行される
- **Screen Space Shadow** を有効にしていない URP プロジェクトでは、全ピクセルでカスケードシャドウマップをサンプリングするため、日向の広い屋外シーンで GPU 時間を浪費する
- モバイルプロジェクトで、カスケード分割を PC と同じ設定にしているケース。120fps モードでフレームバジェットが 8.3ms に半減する状況では、シャドウパスの 1ms が全体の 12% を占める

**Unity でのアプローチ**:

- **URP**: カスケードシャドウマップの分割数と距離設定で、遠方のシャドウ計算を省略。`MainLightShadowCasterPass` のカスタマイズで、タイルベースの分類に近いことが可能
- **HDRP**: 標準で**コンタクトシャドウ**（スクリーンスペースでの近接シャドウ）と**カスケードシャドウ**の2段階を持ち、`ScreenSpaceShadow` パスでピクセル単位の最適化が組み込まれている

**具体的なアクション**:

```csharp
// URP の RenderFeature で Screen Space Shadow を追加し、
// タイル単位でシャドウ計算をスキップする例
public class TileBasedShadowFeature : ScriptableRendererFeature
{
    // Compute Shader でタイル分類を実行し、
    // 分類結果をシャドウパスに渡す
}
```

Moore 氏の教訓「期待ほどではなかった」は Unity にも当てはまる。カスケード設定の調整だけで大幅な改善が見込めるため、まずは `QualitySettings` のシャドウ設定（カスケード数、シャドウ距離、解像度）を見直すことを推奨する。

---

### 2. クラスタードライティング → Unity: Forward+ / HDRP Light Cluster

**Unreal で発生していた問題**: 1ビューあたり最大 264 個のダイナミックライト、1クラスタあたり最大 30 ライトが存在するステージで、ライトリストを**リンクリスト**で管理していた。リンクリストの各ノードを辿るたびにメモリの異なる位置にジャンプするため、GPU のキャッシュが効かず帯域を浪費していた。さらに Wave（32〜64スレッドの束）内の各スレッドが異なるノードを参照するため、**スカラー化**（全スレッドで同じデータを共有する最適化）が効かなかった。Gen8/Gen9 では許容範囲だったが、Switch 1 の 30fps ターゲットや Gen9 の 120fps モード（バジェット 8.3ms）では帯域がボトルネックになった。

**Unity で同様の問題が起きるケース**:

- **URP Forward+ で大量のライトを使用するシーン**: URP の Forward+ はライトクラスタリングを内蔵しているが、ライト数がデスクトップの上限 256 に近づくとクラスタ構造の走査コストが増大する。特に狭い室内に多数のポイントライトが密集するシーン（バー、ネオン街、ゲームセンターなど）で顕著
- **HDRP のライトクラスタ**: クラスタあたりのライト数が増えると、`ClusterLighting.compute` の実行時間が延びる。`HDAdditionalLightData` の影響範囲（Range）が大きすぎるライトが多いと、広範囲のクラスタにライトが登録されてしまう
- **カスタム SRP**: 独自のクラスタードライティングをリンクリストで実装している場合、Moore 氏と全く同じ帯域問題が発生する

**Unity でのアプローチ**:

- **URP 6+ の Forward+**: Unity 2022 以降の URP は **Forward+ レンダリング**をサポートし、ライトクラスタリングが標準搭載されている。`AdditionalLightsForwardPlus` パスで、クラスタごとのライトリストを GPU 上で管理する
- **HDRP**: 独自の **Light Cluster** システムを持ち、`ClusterLighting.compute` でクラスタ化を実行

**具体的なアクション**:

- URP Forward+ のライト上限はデスクトップ/コンソールで **256**、モバイルで **32**。この値は URP Asset の Inspector からは変更できず、変更するには **URP Config Package**（`com.unity.render-pipelines.universal-config`）内の `ShaderConfig.cs` を編集する必要がある。なお `maxAdditionalLightsCount` プロパティは Forward+ モードでは**無視される**（Forward モードのみ有効）
- HDRP では `HDAdditionalLightData` の影響範囲を厳密に設定し、クラスタあたりのライト数を減らす
- カスタム SRP を書く場合、Moore 氏の Flat Bit Array 設計（2 × uint32 で 64 ライト管理 + `WaveAllBitOr` でスカラー化）はそのまま HLSL で実装可能

```hlsl
// Unity の Compute Shader でも WaveAllBitOr は使用可能（SM 6.0+）
// #pragma require waveops ディレクティブが必要
#pragma require waveops
uint mergedMask = WaveActiveBitOr(lightBitMask);
```

> **注意**: Unity で Wave Intrinsics を使用するには `#pragma require waveops` の宣言が必須。SM 6.0 以上が必要で、対応プラットフォームはデスクトップ/コンソールに限定される（モバイルでは使えない）。

---

### 3. コンピュートバリアの排除 → Unity: AsyncGPUReadback とバリア設計

**Unreal で発生していた問題**: クラスタードライティングでは、タイル分類 → ライトカリング → ライティング計算と複数のコンピュートシェーダーが連鎖実行される。前段の出力を後段が参照するため `DeviceMemoryBarrier` による同期が必要だが、このバリアはワークグループ内のメモリ同期しか保証しない。異なるディスパッチグループ間の同期には Full GPU Flush が必要で、パイプライン全体がストールした。特に Xbox One / PS4 世代ではこのストールがフレームバジェットを 0.5ms 以上圧迫するケースがあった。Moore 氏は「Rumbleverse 時代から改善を重ねてきた」とし、リングバッファで書き込みと読み込みのアドレスを分離することでバリア自体を不要にした。

**Unity で同様の問題が起きるケース**:

- **カスタム Render Feature で複数の Compute Shader を連鎖実行するとき**: 例えばスクリーンスペースのライト分類 → ライティング計算 → ポストエフェクトの3段パイプラインを `CommandBuffer` で実行すると、各ディスパッチ間に暗黙のバリアが挿入され、GPU がアイドル状態になる区間が発生する
- **SSAO / SSR / ボリュメトリックフォグなど複数パスの Compute 処理**: HDRP では標準でこれらが Compute 化されているが、カスタムポストエフェクトを追加すると意図しないバリアが差し込まれることがある
- **GPU Readback を同期的に待つコード**: `ComputeBuffer.GetData()` は CPU-GPU 間のフルバリアを発生させ、フレーム全体がストールする

**Unity でのアプローチ**:

Unity の `CommandBuffer` でコンピュートシェーダーを連鎖実行する場合も同様の問題が発生する:

```csharp
// バリアが暗黙的に挿入される例
cmd.DispatchCompute(classifyShader, ...);
// ← ここに暗黙のバリア
cmd.DispatchCompute(lightingShader, ...);
```

**具体的なアクション**:

- `CommandBuffer` のディスパッチ順序を意識し、不要なバリアを避ける
- Moore 氏のリングバッファ方式は `ComputeBuffer` の Ping-Pong（ダブルバッファ）で実現可能
- `AsyncGPUReadback` を活用して CPU-GPU 間の同期を最小化
- Unity 6 の **Render Graph**（`UnityEngine.Rendering.RenderGraphModule` 名前空間）では依存関係が明示的に記述でき、不要なバリアが自動的に排除される。Unity 2023 以前の `Experimental.Rendering` 名前空間からは移動しているので注意

---

### 4. GCN アセンブリ最適化 → Unity: Shader Variant とプラットフォーム別プロファイル

**Unreal で発生していた問題**: ディファードライティングシェーダー内でライトインデックスバッファを `StructuredBuffer<uint>` で宣言していたところ、GCN コンパイラが `buffer_load_format_x`（ベクターメモリ命令）+ `v_readfirstlane_b32`（ベクター→スカラー変換）の2命令を生成していた。同じデータを `ByteAddressBuffer` で読むと `s_buffer_load_dword`（スカラーメモリ命令）の1命令で済む。たった1命令の差だが、ディファードライティングは画面上の全ピクセル（1080p で約200万、4K で約800万）で走り、さらにパーミュテーションごとに繰り返されるため、差が累積して無視できないレベルになった。Moore 氏は「HLSL の書き方が『正しい』かどうかと、最適なアセンブリが生成されるかどうかは別問題」と強調した。

**Unity で同様の問題が起きるケース**:

- **カスタムシェーダーで `StructuredBuffer` を使っている全てのケース**: Unity も DXC / FXC / Metal Shader Compiler を経由するため、バッファ型宣言がアセンブリ出力に影響する。特にフルスクリーンパスや Compute Shader など画面全体を走る処理では1命令の差が数百万回掛け算される
- **URP / HDRP の Lit シェーダーをカスタマイズしたとき**: Unity のビルトインシェーダーは十分に最適化されているが、カスタムキーワードやバッファを追加すると、意図しないベクターロードが生成されることがある
- **プラットフォーム間でシェーダーパフォーマンスが大きく異なるとき**: PC では問題ないのに PS4 や Switch で遅い場合、アセンブリレベルの問題が原因であることが多い。Unity の Frame Debugger だけでは検出できない

**Unity でのアプローチ**:

Unity も同じ HLSL コンパイラチェーン（DXC / FXC / Metal Shader Compiler）を使うため、同様の問題が発生する:

- `StructuredBuffer<uint>` vs `ByteAddressBuffer` の選択は Unity でも有効
- **RenderDoc** / **PIX** / **Xcode GPU Profiler** でシェーダーのアセンブリ出力を確認

**具体的なアクション**:

```hlsl
// Unity ShaderLab でも ByteAddressBuffer は使用可能
ByteAddressBuffer _LightIndexBuffer;

uint lightIndex = _LightIndexBuffer.Load(offset);
// StructuredBuffer<uint> より効率的なスカラーロードが
// 生成される可能性がある（プラットフォーム依存）
// 注: StructuredBuffer のストライドは一部プラットフォームで
// 16バイトの倍数が推奨される（公式ドキュメント参照）
```

- **Frame Debugger** だけでなく、**RenderDoc** でシェーダーのディスアセンブリを確認する習慣をつける
- Unity の **Shader Compilation** ログで、各プラットフォーム向けのコンパイル結果を比較

---

### 5. Rasterized Quads → Unity: SRP Batcher / GPU Instancing の最適化

**Unreal で発生していた問題**: ディファードライティングのタイルベースパスでは、各8×8タイルをフルスクリーンクワッド（2三角形）で描画していた。GPU はピクセルシェーダーを2×2のQuad単位で実行するため、三角形のエッジ（対角線）をまたぐQuadでは、エッジ外のピクセルも「ヘルパーレーン」として実行される。計算結果は破棄されるが GPU リソースは消費する。8×8タイルでは **78ピクセル中16がヘルパー = 25%のオーバーシェーディング**。さらにタイルサイズが小さくなるほどエッジ比率が増えて悪化する。Switch 2 で SSR が最も重いビューで3倍遅くなった問題の直接原因がこれだった。`VK_NV_fill_rectangle` で三角形の代わりに矩形をラスタライズすることで、ヘルパーレーンを完全にゼロにし、タイルサイズを16×16に拡大してクラシファイヤの実行量を1/4に削減した。

**Unity で同様の問題が起きるケース**:

- **`Graphics.Blit` を使ったポストエフェクト**: Unity の `Blit` はフルスクリーンクワッド（4頂点2三角形）を使うため、全ピクセルの描画において対角線に沿ったヘルパーレーンの無駄が発生する。ポストエフェクトが軽い場合は無視できるが、レイマーチングや複雑なSSAO/SSRなど重いシェーダーでは影響が顕在化する
- **URP のカスタム Render Feature でタイルベースの描画を行うとき**: Moore 氏の事例と同様に、DrawIndirect で多数のタイルを2三角形で描画すると、タイル数×ヘルパーレーン分の無駄が蓄積する
- **モバイルの低解像度タイル処理**: タイルサイズが4×4のような小さい値の場合、オーバーシェーディング率がさらに悪化する

**Unity でのアプローチ**:

Unity のフルスクリーンパスも同じ2三角形方式を使っている（`Blit` 操作）。`VK_NV_fill_rectangle` は Unity からは直接利用しにくいが、関連する最適化は以下の通り:

- **URP の Full Screen Pass Renderer Feature**: フルスクリーンエフェクトの公式推奨方法。`Blit` の代わりに最適化された描画パスを使用する
- **HDRP**: `BlitFullscreenTriangle` メソッドで**1三角形**によるフルスクリーン描画を行う（2三角形のクワッドではなく、画面全体を覆う1つの大きな三角形を使うことでヘルパーレーンのオーバーヘッドを削減）。`Graphics.Blit` はフルスクリーンクワッド（4頂点2三角形）を使うため、HDRP では非推奨
- **コンピュートシェーダーでの代替**: フルスクリーンパスをラスタライザ経由ではなくコンピュートシェーダーで実行すれば、ヘルパーレーンの問題自体が存在しない。ただし、Unity 公式としてはフルスクリーンパスには **Full Screen Pass Renderer Feature** の使用が推奨されている

**具体的なアクション**:

```csharp
// URP でフルスクリーン Compute パスを使う例
cmd.DispatchCompute(fullScreenCS, kernel,
    (Screen.width + 7) / 8,
    (Screen.height + 7) / 8, 1);
// ラスタライザを経由しないので、ヘルパーレーンが発生しない
```

Moore 氏の「タイルサイズ 8×8 → 16×16 への拡大」は、Unity のコンピュートシェーダーでもスレッドグループサイズの選択として直接応用できる。

---

### 6. シェーダーパーミュテーション削減 → Unity: Shader Variant 爆発の防止

**Unreal で発生していた問題**: Unreal Engine のディファードライトシェーダーは、ライトの種類（ポイント/スポット/ディレクショナル）× `USE_LIGHT_FUNCTION` の ON/OFF × シャドウの有無 × その他多数の組み合わせごとに**別バージョンのシェーダー（パーミュテーション）** を生成していた。パーミュテーション数が多いと、各パーミュテーションを実行する GPU スレッド数が少なくなり、**GPU の実行ユニットが半分遊んでいる状態**（低オクピュパンシー）になる。さらに、タイル分類の効果も減殺された -- 多くのタイルが複数パーミュテーションにまたがるため「未分類」に落ちてしまい、分類によるスキップが効かなくなった。Moore 氏は「前作 1+2 の時から見つけたかった最適化だが、3+4 でようやくたどり着いた」と述べ、パーミュテーション統合で **0.4ms** を改善した。

**Unity で同様の問題が起きるケース**:

- **`multi_compile` の乱用によるバリアント爆発**: `multi_compile` を3つ追加するだけで 2×2×2 = 8 バリアントが生成され、他のキーワードとの掛け算でさらに膨れ上がる。URP/HDRP のビルトインキーワードだけでも数百バリアントになるプロジェクトがある
- **ビルドサイズの異常な膨張**: シェーダーバリアントはビルドサイズに直結する。数万バリアントが存在するプロジェクトでは、シェーダーだけで数百MBに達することがある
- **ロード時間の延長**: バリアントが多いとシェーダーの初回コンパイル（ウォームアップ）に時間がかかり、ゲームプレイ中にヒッチ（一瞬のフリーズ）が発生する。特にモバイルで深刻
- **GPU オクピュパンシーの低下**: Moore 氏と同じく、バリアントが多いとGPUの実行ユニットが分散し稼働率が下がる

**Unity でのアプローチ**:

Unity でも **Shader Variant 爆発** は深刻な問題だ。`multi_compile` と `shader_feature` の管理が不適切だと、数千〜数万のバリアントが生成される:

**具体的なアクション**:

- **`shader_feature`** を使い、使用していないバリアントをビルドから除外
- **Shader Variant Collection** でホワイトリスト管理
- `#pragma multi_compile_fragment` で頂点シェーダーのバリアントを減らす
- Unity 6 の **Shader Stripping** API で、ビルド時に不要バリアントを自動除去

```
// 悪い例: バリアント爆発
#pragma multi_compile _ _SHADOWS_SOFT
#pragma multi_compile _ _LIGHT_COOKIES
#pragma multi_compile _ _ADDITIONAL_LIGHTS
// → 2 × 2 × 2 = 8 バリアント（さらに他のキーワードと掛け算）

// 良い例: 必要なものだけ
#pragma shader_feature_fragment _SHADOWS_SOFT
#pragma shader_feature_fragment _LIGHT_COOKIES
// → 使用している組み合わせだけがビルドに含まれる
```

Moore 氏の「パーミュテーション統合 → 0.4ms 改善」は、Unity でも Shader Variant の整理で同等以上の効果が期待できる。

---

### 7. パフォーマンスダッシュボード → Unity: Performance Testing Framework + CI

**Unreal で発生していた問題**: 6プラットフォーム × 19ステージ × Performance/Quality モードの全組み合わせ（100以上のセル）を人力で確認するのは物理的に不可能だった。レベルデザイナーが毎日コンテンツを追加する中で、「昨日のコミットで Tokyo の Switch 1 だけ 3ms 遅くなった」といったリグレッション（性能劣化）が検知されないまま放置されていた。平均フレームタイムだけでは問題の深刻さがわからず、「バジェット内フレーム率 95% でも、残り5%がDynamic Resolutionの下限に張り付いている」状況を見逃していた。Moore 氏は Python でブラウザベースのダッシュボードを自作し、95th パーセンタイルの GPU Time をセルの色で可視化、セルクリックでタイムライン詳細を展開できる仕組みを構築した。

**Unity で同様の問題が起きるケース**:

- **マルチプラットフォーム対応プロジェクト**: iOS / Android / PC / コンソールの組み合わせで、特定プラットフォームだけのリグレッションを見逃す。「PC では問題ないのに、Android の低スペック端末でフレームレートが半分になった」がリリース直前に発覚するケース
- **大規模シーンの継続的な更新**: アーティストが毎日アセットを追加するプロジェクトで、「どのコミットでパフォーマンスが悪化したか」を特定できない。Unity Profiler の手動確認では、20シーン × 5プラットフォームを毎日チェックすることは現実的でない
- **Quality Settings のプリセット間の差異**: Low / Medium / High / Ultra の設定ごとにパフォーマンス特性が異なるが、開発チームが High でしかテストしていないケース

**Unity でのアプローチ**:

Unity には公式の **Performance Testing Framework**（`com.unity.test-framework.performance`）と **Unity Build Automation** がある:

**具体的なアクション**:

```csharp
// Unity Performance Testing Framework の例
[Test, Performance]
public void LightingPerformance_TokyoStage()
{
    Measure.Method(() =>
    {
        // Tokyo シーンの特定カメラパスを1フレーム描画
        SceneManager.LoadScene("Tokyo");
        Camera.main.Render();
    })
    .WarmupCount(5)
    .MeasurementCount(30)
    .Run();
}
```

- **Unity Cloud Build** / **Jenkins** で各プラットフォーム向けビルドを夜間実行
- **Grafana + InfluxDB** でパフォーマンスデータを可視化（Moore 氏のダッシュボードに相当）
- Unity 6 の **Profiler Counters API** でカスタムメトリクス（シャドウパス時間、ライティング時間など）を CI に送信

---

### 8. ストライクプラン → Unity: Scene 単位のプロファイルレポート

**Unreal で発生していた問題**: ダッシュボードでデータを集めても、それだけでは最適化は進まなかった。アーティストにダッシュボードを見せて「全部緑にしろ」と丸投げすると、最も削りやすいがビジュアルインパクトの大きい要素（フォリッジ、ライティング）から削っていくため、**出荷に必要以上にコンテンツが痩せた**。スライドでは「the cuts will be more severe than needed to ship the stage」が赤太字で強調されており、Moore 氏がセッション中最も力を込めて語ったポイントだった。エンジニアが「何を削ればどれだけ軽くなるか」の技術的指針をドキュメント化（ストライクプラン）し、判断はアーティストに委ねる協業モデルを確立した。

**Unity で同様の問題が起きるケース**:

- **「Profiler で赤いからとにかく減らして」というアーティストへの丸投げ**: フォリッジ密度を半分にすればパフォーマンスは改善するが、「LOD Group を追加すれば密度を維持したまま Draw Call だけ 40% 減らせる」というエンジニア側の知識が共有されていない
- **最適化の責任が不明確なプロジェクト**: エンジニアは「アセットが重い」、アーティストは「エンジンが遅い」と言い合い、具体的な改善アクションが決まらない
- **シーンごとのパフォーマンス特性の違いが考慮されていない**: 全シーンに同じ品質設定を適用しているが、実際には開放的な屋外シーンと密閉的な室内シーンでボトルネックが全く異なる

**Unity でのアプローチ**:

Moore 氏のストライクプランは技術的なドキュメントではなく、**エンジニアとアーティストのコミュニケーションツール**だ。Unity プロジェクトでも以下のように実践できる:

1. **Scene ごとの Profile Analyzer レポート**: Unity の Profile Analyzer で各シーンのフレーム内訳を取得し、問題箇所を特定
2. **具体的な改善提案**: 「このシーンの Tree Prefab を LOD Group 付きに変更すれば Draw Call が 40% 減る」のように定量的に提示
3. **アーティスト向けダッシュボード**: Frame Debugger のスクリーンショットを添え、何が重いのかを視覚的に説明

> Moore 氏の核心メッセージ: 「アーティストに『全部緑にしろ』と丸投げしてはいけない。」 これは Unity プロジェクトでも全く同じだ。

---

### 9. ライトマップパッキング → Unity: Lightmapper 設定の最適化

**Unreal で発生していた問題**: Tokyo ステージ（本作で最も困難なレベル）で、CPU 時間が深刻なボトルネックになっていた。原因を調査すると、ライトマップのアトラスパッキングが**空間的局所性**（近いメッシュを同じページに配置）を優先していたため、同じベンチが3脚あっても3枚の別アトラスページに分散されていた。結果としてインスタンシング（同一メッシュの一括描画）が効かず、Draw Call が増加し CPU 側のレンダリングスレッドが過負荷になった。パッキングロジックを**インスタンサビリティ優先**（同じメッシュ型のライトマップを同じページに集約）に変更し、距離制約を2倍に緩和。さらにアトラスページサイズを拡大（メモリ制約のある Switch 1 ではトップ MIP をクランプして緩和）。結果として **Switch 上で CPU 時間 2ms を削減**した。

**Unity で同様の問題が起きるケース**:

- **同一 Prefab の複数インスタンスが異なるライトマップアトラスに分散している**: Unity の Progressive Lightmapper も空間的局所性ベースでパッキングするため、同じベンチ Prefab が3つ配置されている場合、それぞれ別のライトマップインデックスに割り当てられることがある。異なるライトマップインデックスのオブジェクトは Static Batching でもバッチされない
- **Max Lightmap Size が不適切**: 小さすぎるとアトラスページ数が増え、バッチが分断される。大きすぎるとテクスチャストリーミングが非効率になり、モバイルでメモリ不足を引き起こす
- **Lightmap Parameters がオブジェクトごとにバラバラ**: 同一メッシュでも異なる `LightmapParameters` アセットが割り当てられていると、ライトマップの解像度やバイアス値が異なり、同じアトラスにパックされにくくなる
- **Scene View の Lightmap Indices 表示でアトラスの分散を確認していない**: 問題が起きていても視覚的に確認しないと気づけない

**Unity でのアプローチ**:

Unity の Progressive GPU Lightmapper でも同様の問題が発生する:

- Lighting ウィンドウの **Max Lightmap Size**（`LightingSettings.lightmapMaxSize`）を適切に設定（大きすぎるとストリーミング非効率、小さすぎるとバッチ分断）
- **同一 Prefab のインスタンス** には同じ Lightmap Parameters を使用し、バッチングを促進
- `StaticBatching` と Lightmap の相互作用に注意 -- Static Batching はライトマップ UV を保持するが、異なるライトマップインデックスのオブジェクトはバッチされない

**具体的なアクション**:

- Lighting ウィンドウの **Max Lightmap Size** を Scene 全体で統一
- `LightmapParameters` アセットを共有して、同一メッシュが同じアトラスに配置されやすくする
- **Lightmap Overlap** の可視化（Scene View → Lightmap Indices）でアトラスの分散状況を確認

---

### 10. 重複メッシュ検出 → Unity: EditorScript によるコンテンツバリデーション

**Unreal で発生していた問題**: アーティストの作業フロー（コピー＆ペーストのミス、Undo/Redo 操作の不整合、フォリッジペイントツールの挙動）により、完全に同じ位置に同じメッシュが重ねて配置されるケースが頻発していた。**見た目は完全に正しい**ため QA では絶対に発見できないが、GPU は2つ分を描画するので**コストが倍**になる。Moore 氏は「最も厄介なコンテンツバグの一つ」と述べ、保存/マップチェック時に自動検出・除去するシステムを実装した。さらに検出過程で、頂点カラーの不一致（インスタンシングバッチの分断原因）や不要なマテリアルオーバーライドの残骸といった**隠れた問題も芋づる式に発見**された。Moore 氏は「あらゆるエンジンにこの問題を防ぐコンテンツバリデーションを実装すべきだと確信している」と強く主張した。

**Unity で同様の問題が起きるケース**:

- **Prefab のコピー＆ペーストミス**: Scene 上で Prefab を Ctrl+D で複製した際、同じ位置に配置されたまま気づかないケース。特に大量のオブジェクトが密集するシーンでは、Hierarchy ウィンドウでも見落としやすい
- **Terrain のディテールメッシュやツリーの重複**: Terrain ペイントツールで同じ位置に複数回ペイントすると重複が発生する。草やツリーの密度が意図せず倍になり、Draw Call とポリゴン数が増大する
- **アセットインポート時の不整合**: FBX の再インポートで既存オブジェクトが残ったまま新しいオブジェクトが追加されるケース
- **Z-Fighting**: 重複メッシュの典型的な症状。2つのメッシュの深度値が完全に一致するため、虹色のちらつきやフリッカーが発生する。プレイ中にカメラ角度によって出たり消えたりするため、再現しにくい
- **Unity には Unreal のような「保存時自動バリデーション」が標準搭載されていない**: そのため問題が蓄積しやすい

**Unity でのアプローチ**:

Unity には Unreal の「保存時自動バリデーション」に相当する仕組みがないため、**自作が必要**:

```csharp
// EditorScript: シーン内の重複 Transform を検出
[MenuItem("Tools/Check Duplicate Meshes")]
static void CheckDuplicateMeshes()
{
    var renderers = FindObjectsOfType<MeshRenderer>();
    var groups = renderers
        .GroupBy(r => (
            r.GetComponent<MeshFilter>()?.sharedMesh,
            r.transform.position,
            r.transform.rotation,
            r.transform.lossyScale
        ))
        .Where(g => g.Count() > 1);

    foreach (var group in groups)
    {
        Debug.LogWarning(
            $"重複メッシュ検出: {group.First().name} " +
            $"({group.Count()}個が同一位置)",
            group.First().gameObject);
    }
}
```

**さらに堅牢にするには**:

- **`IProcessSceneWithReport`** を実装し、ビルド時に自動検出
- **Prefab の Variant** で頂点カラーが異なるインスタンスを検出
- **`AssetPostprocessor.OnPostprocessAllAssets`** でインポート時に警告

Moore 氏の「あらゆるエンジンにコンテンツバリデーションを実装すべき」は、Unity エンジニアが最も実践すべきアドバイスだ。

---

### Unity エンジニアへのまとめ

本セッションの教訓を Unity プロジェクトに適用する際の優先順位:

| 優先度 | 施策 | 難易度 | 期待効果 |
|:---|:---|:---|:---|
| **1（最優先）** | 重複メッシュ検出 EditorScript | 低 | QA で見つからないバグを根絶 |
| **2** | Shader Variant 整理 | 中 | ビルドサイズ縮小 + GPU 稼働率向上 |
| **3** | CI パフォーマンステスト構築 | 中 | リグレッションの早期検知 |
| **4** | Lightmap パラメータ統一 | 低 | バッチング効率 + ストリーミング改善 |
| **5** | ストライクプラン運用 | 低（技術より運用） | エンジニア-アーティスト協業の質向上 |
| **6** | カスタム SRP パスの最適化 | 高 | フレームバジェットの大幅改善 |
| **7** | Compute Shader でのフルスクリーンパス | 高 | ヘルパーレーン排除 |

難易度の低いものから始め、計測 → 分析 → 行動のサイクルを回すことが重要だ。Moore 氏の3年4ヶ月が教えてくれるのは、**銀の弾丸はなく、小さな改善の積み重ねがマルチプラットフォーム対応を可能にする**ということだ。

---

## 参考情報

| 項目 | 内容 |
|:---|:---|
| セッション | GDC 2026 - 3 Years and 4 Months of Graphics Optimization in 'Tony Hawk's Pro Skater 3+4' |
| スピーカー | John Moore, Principal Rendering Engineer, Blizzard Albany |
| 前作セッション | GDC 2020 - 3D + GPU Optimization in Tony Hawk's Pro Skater 1+2 |
| エンジン | Unreal Engine（カスタマイズ版） |
| 主要技術 | Tile Classification, Clustered Lighting, Flat Bit Array, GCN Assembly, Rasterized Quads (VK_NV_fill_rectangle), Performance Dashboard, Content Validation |
| 対応プラットフォーム | Xbox One, PS4, Xbox Series S/X, PS5, Switch 1, Switch 2, PC |

### Unity 公式ドキュメント参照先

| トピック | ドキュメント |
|:---|:---|
| URP Forward+ ライティング | Universal Render Pipeline > Lighting > Forward+ rendering path |
| ScriptableRendererFeature | URP API > ScriptableRendererFeature |
| HDRP Light Cluster | HDRP > Rendering > Light Cluster |
| Render Graph（Unity 6） | `UnityEngine.Rendering.RenderGraphModule` 名前空間 |
| Wave Intrinsics | Shader compilation > `#pragma require waveops` |
| Shader Variant Stripping | Shader compilation > Shader variant stripping |
| Performance Testing Framework | `com.unity.test-framework.performance` パッケージ |
| LightingSettings API | `UnityEngine.LightingSettings.lightmapMaxSize` |
| Profile Analyzer | `com.unity.performance.profile-analyzer` パッケージ |

---

## おわりに

最後までお読みいただきありがとうございます。GDC 2026 の他のセッションレポートも順次公開していますので、ぜひフォローしてお待ちください。

**dsgarage Games** | GDC 2026 現地レポート
