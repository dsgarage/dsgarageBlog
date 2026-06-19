using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// カタパルトを「構える → 発射 → 放物線 → リセット」の段階で繰り返し動かす。
    /// 一瞬で終わらないよう、構えの“間”をはっきり見せ、発射もゆっくりにする。
    /// HingeJoint のモーターを段階ごとに切り替えて制御する。
    /// </summary>
    public class CatapultDemo : MonoBehaviour
    {
        public HingeJoint hinge;
        public Rigidbody arm;
        public Rigidbody ball;

        public float holdTime = 1.6f;   // 構えて見せる時間
        public float fireTime = 1.8f;   // 発射～放物線を見せる時間
        public float holdVel = -45f;    // 構え（下限へ軽く押し当て）
        public float fireVel = 220f;    // 発射（振り上げ）
        public float motorForce = 2400f;

        Vector3 _armP, _ballP;
        Quaternion _armR, _ballR;
        float _t;
        bool _firing;

        void Start()
        {
            if (arm != null) { _armP = arm.position; _armR = arm.rotation; }
            if (ball != null) { _ballP = ball.position; _ballR = ball.rotation; }
            SetMotor(holdVel);
        }

        void SetMotor(float v)
        {
            if (hinge == null) return;
            hinge.useMotor = true;
            var m = hinge.motor;
            m.targetVelocity = v;
            m.force = motorForce;
            m.freeSpin = false;
            hinge.motor = m;
        }

        void FixedUpdate()
        {
            _t += Time.fixedDeltaTime;
            if (!_firing)
            {
                if (_t >= holdTime) { _firing = true; _t = 0f; SetMotor(fireVel); }
            }
            else
            {
                if (_t >= fireTime) { _firing = false; _t = 0f; ResetBodies(); SetMotor(holdVel); }
            }
        }

        void ResetBodies()
        {
            if (arm != null)
            {
                arm.linearVelocity = Vector3.zero; arm.angularVelocity = Vector3.zero;
                arm.position = _armP; arm.rotation = _armR;
            }
            if (ball != null)
            {
                ball.linearVelocity = Vector3.zero; ball.angularVelocity = Vector3.zero;
                ball.position = _ballP; ball.rotation = _ballR;
            }
        }
    }
}
