using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>HingeJoint をモーター駆動して回し続ける。クランク/フライホイール用。</summary>
    [RequireComponent(typeof(HingeJoint))]
    public class HingeMotor : MonoBehaviour
    {
        public float targetVelocity = 120f; // deg/sec
        public float force = 300f;

        void Start()
        {
            var h = GetComponent<HingeJoint>();
            h.useMotor = true;
            var m = h.motor;
            m.targetVelocity = targetVelocity;
            m.force = force;
            m.freeSpin = false;
            h.motor = m;
        }
    }
}
