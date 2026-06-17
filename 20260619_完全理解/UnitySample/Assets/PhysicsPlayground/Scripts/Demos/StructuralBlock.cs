using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 記事「大規模破壊と構造的完全性」対応。
    /// ブロック同士は FixedJoint（breakForce 付き）で接続されている。
    /// 接続にかかる力が breakForce を超えると Unity が Joint を破断し、
    /// OnJointBreak が呼ばれる。これを数えることで「健全な接続数」を可視化する。
    ///
    /// これは記事の StructuralIntegrity（応力 > 強度で破断）を、
    /// 物理エンジンの拘束ソルバーに委ねたミニ実装に相当する。
    /// </summary>
    public class StructuralBlock : MonoBehaviour
    {
        public System.Action onJointBroken;

        void OnJointBreak(float breakForce)
        {
            onJointBroken?.Invoke();
        }
    }
}
