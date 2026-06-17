using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 記事「掛け算のゲームデザイン」対応。
    /// 2つの剛体を ConfigurableJoint で剛結合（溶接）するためのヘルパ。
    ///
    /// 記事では 6 自由度を Limited にして「ほどよい接続」を作っていますが、
    /// 乗り物として安定させたいので、ここでは全自由度を Locked にした
    /// 剛結合（= ほぼ FixedJoint 相当）を ConfigurableJoint で表現しています。
    /// Limited に変えると、軋んでしなる接続も同じコードで作れます。
    /// </summary>
    public static class AttachmentSystem
    {
        /// <summary>body を baseBody に剛結合する。生成した Joint を返す。</summary>
        public static ConfigurableJoint Weld(Rigidbody body, Rigidbody baseBody)
        {
            var joint = body.gameObject.AddComponent<ConfigurableJoint>();
            joint.connectedBody = baseBody;

            // 6 自由度をすべて固定 → 剛結合
            joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Locked;
            joint.angularXMotion = joint.angularYMotion = joint.angularZMotion
                = ConfigurableJointMotion.Locked;

            // 接続を一定以上の力で壊したい場合はここを有効化する
            // joint.breakForce = 8000f;
            // joint.breakTorque = 8000f;

            joint.enablePreprocessing = false; // 多段接続時の安定性向上
            return joint;
        }
    }
}
