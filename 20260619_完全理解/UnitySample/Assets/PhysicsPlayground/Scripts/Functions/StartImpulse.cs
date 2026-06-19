using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>Play 開始から delay 秒後に一度だけ力積/トルク積を与える。カタパルトや連鎖の起点用。</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class StartImpulse : MonoBehaviour
    {
        public Vector3 linear;
        public Vector3 torque;
        public float delay = 0f;

        Rigidbody _rb;
        float _t;
        bool _done;

        void Awake() => _rb = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            if (_done) return;
            _t += Time.fixedDeltaTime;
            if (_t < delay) return;
            if (linear != Vector3.zero) _rb.AddForce(linear, ForceMode.Impulse);
            if (torque != Vector3.zero) _rb.AddTorque(torque, ForceMode.Impulse);
            _done = true;
        }
    }
}
