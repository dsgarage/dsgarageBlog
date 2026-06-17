using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 記事「掛け算のゲームデザイン」の WheelFunction に対応する駆動コントローラ。
    /// WheelCollider を使った Unity 標準パターン（4 輪を 1 つの車体 Rigidbody にぶら下げる）で、
    /// 後輪にモータートルク、前輪に操舵角を与えて走らせる。
    ///
    /// 操作: 上下キー / W,S = 前後、左右キー / A,D = 操舵
    /// </summary>
    public class VehicleController : MonoBehaviour
    {
        public WheelCollider[] driveWheels;  // 後輪（駆動）
        public WheelCollider[] steerWheels;  // 前輪（操舵）

        public float motorTorque = 600f;
        public float maxSteerAngle = 25f;
        public float brakeTorque = 400f;

        public float CurrentSpeedKmh { get; private set; }

        [Header("低重心化（転倒抑制）")]
        public bool overrideCenterOfMass = true;
        public Vector3 centerOfMass = new Vector3(0f, -0.6f, 0f);

        void Awake()
        {
            if (overrideCenterOfMass)
            {
                var rb = GetComponent<Rigidbody>();
                if (rb != null) rb.centerOfMass = centerOfMass;
            }
        }

        void FixedUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            if (driveWheels != null)
            {
                bool braking = Mathf.Abs(v) < 0.05f;
                foreach (var w in driveWheels)
                {
                    if (w == null) continue;
                    w.motorTorque = braking ? 0f : v * motorTorque;
                    w.brakeTorque = braking ? brakeTorque : 0f;
                }
            }

            if (steerWheels != null)
            {
                foreach (var w in steerWheels)
                    if (w != null) w.steerAngle = h * maxSteerAngle;
            }

            var rb = GetComponent<Rigidbody>();
            if (rb != null) CurrentSpeedKmh = rb.linearVelocity.magnitude * 3.6f;
        }
    }
}
