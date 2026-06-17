# GDC 2024 物理演算プレイグラウンド（Unity サンプル）

記事「[GDC 2024に学ぶ、物理演算で「創発するゲームプレイ」をつくる](../)」で解説した物理実装を、**実際に手を動かして体験できる** Unity サンプルです。シーンの手組みは不要で、コードがシーンを丸ごと組み立てます。

## 動作環境

- Unity **2021.3 LTS 以降**（Built-in / URP どちらでも色が付きます）
- Input は **旧 Input Manager** を使用します
  - 新規プロジェクトで動かないときは `Edit > Project Settings > Player > Active Input Handling` を **「Both」** または **「Input Manager (Old)」** にしてください

## 使い方（30秒）

### 方法A: メニューから生成（おすすめ）

1. `UnitySample/Assets/PhysicsPlayground/` を Unity プロジェクトの `Assets/` 配下にコピー
2. メニュー **`Tools > GDC Physics Playground > Create Demo Scene`** を実行
3. **Play** を押す

### 方法B: 手動

1. 同じくフォルダをコピー
2. 空のシーンに空の GameObject を作り、**`PhysicsPlaygroundBootstrap`** を 1 つだけアタッチ
3. **Play** を押す

地面・ライト・カメラ・HUD・デモは自動生成されます。

## 操作

| キー | 動作 |
|------|------|
| **1** | デモ1「掛け算ガレージ」に切替 |
| **2** | デモ2「構造崩壊」に切替 |
| **R** | 現在のデモをリセット |
| 右ドラッグ | カメラ旋回 |
| ホイール | ズーム |

### デモ1: 掛け算ガレージ（Tears of the Kingdom）

| キー | 動作 |
|------|------|
| W / S・↑↓ | 前後 |
| A / D・←→ | 操舵 |
| Space | ファン噴射 |
| 左クリック | 地面のファンを車体に溶接（取り付け） |

車 + 4 輪で走る土台に、ファンを溶接していきます。ファンを増やすほど推力が積み上がり、やがて車体が前のめりに浮き上がります。部品の組み合わせから挙動が生まれる「掛け算の創発」を体験できます。

### デモ2: 構造崩壊（Red Faction / The Finals）

| キー | 動作 |
|------|------|
| 左クリック | その方向へ鉄球を発射 |

ブロックは隣接ブロックと `FixedJoint`（破断強度つき）で接続されています。鉄球を撃ち込むと、力が限界を超えた接続から破断し、支えを失った上部が連鎖的に崩落します。HUD の「健全な接続数」が減っていく様子も見られます。

## 記事のどのコードに対応するか

| スクリプト | 記事の対応箇所 |
|------------|----------------|
| `Core/AttachmentSystem.cs` | 掛け算のゲームデザイン → `ConfigurableJoint` による接続 |
| `Functions/VehicleController.cs` | WheelFunction（`WheelCollider` で駆動・操舵） |
| `Functions/PropellerThruster.cs` | PropellerFunction（推力・反トルク） |
| `Demos/StructuralBlock.cs` + `StructuralCollapseDemo.cs` | StructuralIntegrity（接続強度を超えると破断） |

> 記事ではアルゴリズムの中身（応力の再分配、Voronoi 分割など）を直接書いていましたが、本サンプルでは体験しやすさと安定性を優先し、その役割を Unity の拘束ソルバー（Joint の `breakForce`）に委ねています。記事のコードと読み比べると、「自前で計算する版」と「エンジンに委ねる版」の対応が見えてきます。

## ファイル構成

```
Assets/PhysicsPlayground/
  Scripts/
    Core/        AttachmentSystem.cs, Prim.cs
    Functions/   VehicleController.cs, WheelVisual.cs, PropellerThruster.cs
    Demos/       MultiplicativeGarageDemo.cs, StructuralCollapseDemo.cs, StructuralBlock.cs
    Infra/       PhysicsPlaygroundBootstrap.cs, OrbitCamera.cs, PlaygroundHUD.cs, IPlaygroundDemo.cs
  Editor/        PlaygroundMenu.cs
```

## うまく動かないとき

- **キー入力が効かない** → Active Input Handling を「Both」に（上記）
- **車輪が地面にめり込む / 跳ねる** → `MultiplicativeGarageDemo` の `suspensionSpring`（spring / damper）や `Chassis` の `centerOfMass` を調整
- **崩れない / 崩れすぎる** → `StructuralCollapseDemo` の `BreakForce`（既定 350）と鉄球の質量・速度を調整
