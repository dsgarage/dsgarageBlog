using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 記事「掛け算のゲームデザイン」の PropellerFunction に対応するファン（推進装置）。
    /// 自分自身の Rigidbody に推力を加え、それが接続（ConfigurableJoint）を通じて
    /// 車体全体へ伝わる。ファンを足すほど推力が積み上がる = 掛け算の体験。
    ///
    /// 推力は高い位置・後方にかかるので、強く吹かすと車体が前のめりに浮く。
    /// 「想定していなかった挙動が組み合わせから生まれる」創発の手触りを意図している。
    ///
    /// 操作: Space で噴射
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PropellerThruster : MonoBehaviour
    {
        public KeyCode throttleKey = KeyCode.Space;
        public float maxThrust = 850f;
        public Transform bladeVisual;

        public bool Active { get; set; } = true;

        Rigidbody _rb;
        float _rpm;

        void Awake() => _rb = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            float throttle = (Active && Input.GetKey(throttleKey)) ? 1f : 0f;
            _rpm = Mathf.Lerp(_rpm, throttle, Time.fixedDeltaTime * 4f);

            if (_rpm > 0.001f)
            {
                Vector3 dir = transform.forward; // ファンが向く方向へ押す
                _rb.AddForce(dir * maxThrust * _rpm, ForceMode.Force);
                _rb.AddTorque(-dir * maxThrust * 0.015f * _rpm, ForceMode.Force); // 反トルク
            }
        }

        void Update()
        {
            if (bladeVisual != null)
                bladeVisual.Rotate(Vector3.up, _rpm * 1800f * Time.deltaTime, Space.Self);
        }
    }
}
