# Unity Netcode for GameObjects vs Photon 徹底比較

## はじめに

Unityでマルチプレイヤーゲームを開発する際、ネットワークライブラリの選定は重要な意思決定のひとつです。本記事では、Unity公式の **Netcode for GameObjects** と、長年デファクトスタンダードだった **Photon（PUN2 / Fusion）** を比較します。

---

## Netcode for GameObjects とは

Unity公式のネットワークマルチプレイヤーライブラリです。もともとOSSの **MLAPI** をUnityが買収・統合し、公式パッケージ（`com.unity.netcode.gameobjects`）として提供しています。旧 UNet（Unity Networking）の後継にあたります。

### 主な特徴

- **サーバー権威型アーキテクチャ** — ホスト型またはデディケイテッドサーバーをサポート
- **NetworkVariable** — 値の変更を自動的にクライアント間で同期
- **RPC** — `ServerRpc` / `ClientRpc` でサーバー⇔クライアント間のメソッド呼び出し
- **NetworkObject / NetworkBehaviour** — MonoBehaviourを拡張したネットワーク対応コンポーネント
- **Unity Transport (UTP)** ベースの通信レイヤー

### 基本的なコード例

```csharp
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    // 全クライアントに自動同期される変数
    private NetworkVariable<int> score = new NetworkVariable<int>(0);

    // クライアント → サーバーへの呼び出し
    [ServerRpc]
    private void MoveServerRpc(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * 5f;
    }

    // サーバー → 全クライアントへの呼び出し
    [ClientRpc]
    private void NotifyScoreClientRpc(int newScore)
    {
        Debug.Log($"スコア更新: {newScore}");
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Debug.Log("自分のプレイヤーがスポーンしました");
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move != Vector3.zero)
        {
            MoveServerRpc(move);
        }
    }
}
```

---

## Photon とは

Exit Games が提供するサードパーティのネットワークライブラリ群です。**PUN2（Photon Unity Networking 2）** が長らく主流でしたが、現在は次世代SDKの **Photon Fusion** も登場しています。

---

## 比較表

### アーキテクチャ

| | Netcode for GameObjects | Photon (PUN2 / Fusion) |
|---|---|---|
| **提供元** | Unity公式 | Exit Games（サードパーティ） |
| **サーバーモデル** | ホスト型 or デディケイテッドサーバー（自前運用） | クラウドサーバー（Photon Cloud） |
| **インフラ** | 自分で用意（または Unity Game Server Hosting） | Photon Cloud が標準付属 |
| **プロトコル** | Unity Transport (UDP) | 独自プロトコル (UDP/信頼性付きUDP) |
| **ライセンス** | 無料・OSS | CCU課金制（無料枠あり） |

### 機能比較

| 機能 | Netcode for GameObjects | Photon |
|---|---|---|
| マッチメイキング | 別途 Unity Lobby が必要 | 標準搭載（ルーム/ロビー） |
| リレー | 別途 Unity Relay が必要 | Photon Cloud に内蔵 |
| サーバー権威型 | ネイティブ対応 | PUN2: 非対応 / Fusion: 対応 |
| クロスプラットフォーム | Unity限定 | Unity, Unreal 等に対応 |
| エディタ統合 | 深い（公式） | 良好（長年の実績） |

---

## それぞれの強み・弱み

### Netcode for GameObjects の強み

- **無料・OSS** — CCU課金なし。大規模でもライセンスコストがかからない
- **Unity深層統合** — エディタとの連携が最も自然
- **サーバー権威型** — チート対策がしやすいアーキテクチャ
- **カスタマイズ性** — トランスポート層を自由に差し替え可能

### Netcode for GameObjects の弱み

- **インフラ自前** — サーバーのホスティング・スケーリングは自分で解決する必要がある
- **成熟度** — Photonに比べると歴史が浅く、ナレッジも少ない
- **マッチメイキング未搭載** — Unity Lobby / Relay を別途導入する必要がある

### Photon の強み

- **インフラ不要** — Photon Cloud ですぐにマルチプレイが動く
- **実績と安定性** — 長年の運用実績、豊富な事例・ドキュメント
- **マッチメイキング** — ルーム管理・ロビーが標準装備
- **クロスプラットフォーム** — Unity以外のエンジンでも利用可能

### Photon の弱み

- **CCU課金** — 同時接続数に応じた従量課金（規模拡大でコスト増）
- **Photon Cloud 依存** — サーバーロジックのカスタマイズに制限がある
- **チート対策（PUN2）** — リレー型のためサーバー権威型ではなく、チート対策が難しい

---

## ユースケース別おすすめ

| ユースケース | おすすめ | 理由 |
|---|---|---|
| 小〜中規模、素早くリリースしたい | **Photon Fusion / PUN2** | インフラ込みですぐ動く |
| サーバーロジックを完全制御したい | **Netcode for GameObjects** | 自前サーバーで自由にカスタマイズ |
| Unity公式エコシステムで統一したい | **Netcode + Unity Relay/Lobby** | 公式サービスで一気通貫 |
| CCU課金を避けたい大規模タイトル | **Netcode for GameObjects** | ライセンスコストゼロ |
| チート対策を重視したい | **Netcode or Photon Fusion** | サーバー権威型アーキテクチャ |

---

## まとめ

Photonは「すぐ動く・インフラ不要」で今でも有力な選択肢です。一方、Netcode for GameObjectsは「Unity公式・無料・サーバー権威型」が強みで、特にデディケイテッドサーバーを自前運用するタイトルや、CCU課金を避けたい場合に適しています。

また、Photon側も Photon Fusion でサーバー権威型に対応してきており、両者の差は縮まりつつあります。最終的にはプロジェクトの **規模・予算・チート対策要件・チームの習熟度** を踏まえて選定するのが良いでしょう。
