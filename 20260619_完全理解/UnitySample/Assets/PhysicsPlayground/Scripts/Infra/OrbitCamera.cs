using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// ターゲットを中心に周回するカメラ。
    /// 右ドラッグで旋回、ホイールでズーム。ターゲットは Bootstrap が毎フレーム差し替える。
    /// </summary>
    public class OrbitCamera : MonoBehaviour
    {
        public Transform target;
        public float distance = 16f;
        public float yaw = 30f;
        public float pitch = 22f;
        public float orbitSpeed = 4f;
        public float zoomSpeed = 12f;

        Vector3 _focus;

        void LateUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * orbitSpeed;
                pitch -= Input.GetAxis("Mouse Y") * orbitSpeed;
                pitch = Mathf.Clamp(pitch, -5f, 80f);
            }
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed,
                                   5f, 45f);

            Vector3 wanted = target != null ? target.position + Vector3.up * 1.0f : Vector3.up;
            _focus = Vector3.Lerp(_focus, wanted, Time.deltaTime * 6f);

            Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
            transform.position = _focus + rot * new Vector3(0f, 0f, -distance);
            transform.LookAt(_focus);
        }
    }
}
