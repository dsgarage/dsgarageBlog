using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 操作不要の自動推進。Play すると前方/上方へ押し続ける。
    /// forcePointLocal を非ゼロにすると重心からずれた位置に力をかけ、
    /// 「同じ力でも付け位置で勝手に旋回する」創発を見せられる。
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class AutoThruster : MonoBehaviour
    {
        public float forwardForce = 0f;
        public float upForce = 0f;
        public float maxSpeed = 8f;
        public float bladeRpm = 0f;
        public Vector3 forcePointLocal = Vector3.zero; // 非ゼロで AddForceAtPosition
        public Transform[] blades;

        Rigidbody _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.centerOfMass = new Vector3(0f, -0.3f, 0f);
        }

        void FixedUpdate()
        {
            Vector3 f = transform.forward * forwardForce + transform.up * upForce;
            if (f.sqrMagnitude <= 0.0001f) return;

            Vector3 flat = _rb.linearVelocity; flat.y = 0f;
            bool underSpeed = flat.magnitude < maxSpeed || upForce > 0f;
            if (!underSpeed) return;

            if (forcePointLocal == Vector3.zero)
                _rb.AddForce(f);
            else
                _rb.AddForceAtPosition(f, transform.position + transform.rotation * forcePointLocal);
        }

        void Update()
        {
            if (blades == null || bladeRpm <= 0f) return;
            float d = bladeRpm * Time.deltaTime;
            foreach (var b in blades)
                if (b != null) b.Rotate(0f, 0f, d, Space.Self);
        }
    }
}
