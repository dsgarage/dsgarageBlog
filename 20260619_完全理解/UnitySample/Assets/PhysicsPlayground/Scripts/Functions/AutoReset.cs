using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 指定した剛体群を一定周期で初期位置・姿勢へ戻し、速度をゼロにする。
    /// カタパルトのような「一発で終わる」機構を、繰り返し動かして見せるために使う。
    /// </summary>
    public class AutoReset : MonoBehaviour
    {
        public float period = 2.6f;
        public float firstDelay = 0.4f;  // 最初の発射までの待ち
        public Rigidbody[] bodies;

        Vector3[] _pos;
        Quaternion[] _rot;
        float _t;

        void Start()
        {
            if (bodies == null) return;
            _pos = new Vector3[bodies.Length];
            _rot = new Quaternion[bodies.Length];
            for (int i = 0; i < bodies.Length; i++)
            {
                if (bodies[i] == null) continue;
                _pos[i] = bodies[i].position;
                _rot[i] = bodies[i].rotation;
            }
            _t = -firstDelay;
        }

        void FixedUpdate()
        {
            _t += Time.fixedDeltaTime;
            if (_t < period) return;
            _t = 0f;
            for (int i = 0; i < bodies.Length; i++)
            {
                var b = bodies[i];
                if (b == null) continue;
                b.linearVelocity = Vector3.zero;
                b.angularVelocity = Vector3.zero;
                b.position = _pos[i];
                b.rotation = _rot[i];
            }
        }
    }
}
