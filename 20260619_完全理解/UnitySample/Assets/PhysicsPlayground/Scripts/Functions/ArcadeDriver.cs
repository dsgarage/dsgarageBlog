using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 堅牢なアーケード駆動。WheelCollider を使わず、地面に乗った車体 Rigidbody に
    /// 前後の推進力とヨートルクを与えて走らせる。WheelCollider 特有の暴発がなく安定。
    /// 記事の「車輪で走る土台」を、安定性を優先して単純化したもの。
    ///
    /// 操作: 上下/W,S = 前後、左右/A,D = 操舵
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ArcadeDriver : MonoBehaviour
    {
        public float drivePower = 1600f;
        public float turnTorque = 700f;
        public float maxSpeed = 16f;          // m/s
        public Vector3 centerOfMass = new Vector3(0f, -0.4f, 0f);

        Rigidbody _rb;
        public float CurrentSpeedKmh { get; private set; }

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.centerOfMass = centerOfMass; // 低重心で転倒しにくく
        }

        void FixedUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Vector3 fwd = transform.forward;
            fwd.y = 0f;
            if (fwd.sqrMagnitude > 0.0001f) fwd.Normalize();

            Vector3 flat = _rb.linearVelocity;
            flat.y = 0f;

            // 前後推進（最高速以下のときだけ）
            if (flat.magnitude < maxSpeed)
                _rb.AddForce(fwd * v * drivePower);

            // 操舵（ヨートルク）
            _rb.AddTorque(Vector3.up * h * turnTorque);

            // スピン暴走を抑制
            var av = _rb.angularVelocity;
            av.y = Mathf.Clamp(av.y, -2.5f, 2.5f);
            _rb.angularVelocity = av;

            CurrentSpeedKmh = flat.magnitude * 3.6f;
        }
    }
}
