using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// キネマティック剛体を一定角速度で回し続ける。クランク（スライダークランク）など、
    /// モーター駆動の閉ループが噛んで止まるのを避け、確実に回転を与えたいときに使う。
    /// 連結された動的なロッド/ピストンは、この回転に追従して動く。
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicSpin : MonoBehaviour
    {
        public Vector3 axis = Vector3.forward; // 回転軸（ローカル）
        public float degPerSec = 90f;

        Rigidbody _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        void FixedUpdate()
        {
            Quaternion delta = Quaternion.AngleAxis(degPerSec * Time.fixedDeltaTime, axis.normalized);
            _rb.MoveRotation(_rb.rotation * delta);
        }
    }
}
