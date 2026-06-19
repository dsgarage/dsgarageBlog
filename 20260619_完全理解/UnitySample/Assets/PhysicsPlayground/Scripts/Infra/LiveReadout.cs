using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// HUD に出す「実況」を 1 行で供給する。対象 Rigidbody の速さ・角速度・高さを
    /// 毎フレーム読み取って文字列にする。CrossbreedHUD がシーン内の全 LiveReadout を
    /// 集めてまとめて表示する。
    /// </summary>
    public class LiveReadout : MonoBehaviour
    {
        public enum Kind { Speed, AngularSpeedDeg, Height }

        public Kind kind = Kind.Speed;
        public Rigidbody target;
        public string label = "速さ";
        public int order = 0;   // 表示順（小さいほど上）

        public string Line()
        {
            if (target == null) return null;
            switch (kind)
            {
                case Kind.Speed:
                    return $"{label}: {target.linearVelocity.magnitude:0.0} m/s";
                case Kind.AngularSpeedDeg:
                    return $"{label}: {target.angularVelocity.magnitude * Mathf.Rad2Deg:0} °/s";
                case Kind.Height:
                    return $"{label}: {target.position.y:0.0} m";
                default:
                    return null;
            }
        }
    }
}
