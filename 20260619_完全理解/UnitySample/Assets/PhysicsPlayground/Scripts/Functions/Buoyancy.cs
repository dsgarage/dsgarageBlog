using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>水面より沈んだ深さに比例した浮力を加える簡易ブイ。船デモ用。</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Buoyancy : MonoBehaviour
    {
        public float waterLevel = 0f;
        public float floatOffset = 0.25f;   // この高さで釣り合う
        public float strength = 25f;
        public float verticalDamp = 3f;
        public float horizontalDamp = 0.8f;

        Rigidbody _rb;
        void Awake() => _rb = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            float submersion = (waterLevel + floatOffset) - transform.position.y;
            if (submersion <= 0f) return;

            _rb.AddForce(Vector3.up * submersion * strength, ForceMode.Acceleration);
            _rb.AddForce(-_rb.linearVelocity.y * Vector3.up * verticalDamp, ForceMode.Acceleration);
            // 水の抵抗（水平）
            var h = _rb.linearVelocity; h.y = 0f;
            _rb.AddForce(-h * horizontalDamp, ForceMode.Acceleration);
        }
    }
}
