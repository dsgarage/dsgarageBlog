using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// WheelCollider の物理的な位置・回転を、見た目の車輪メッシュに反映する。
    /// 記事の WheelFunction.UpdateWheelVisual に対応。
    /// </summary>
    public class WheelVisual : MonoBehaviour
    {
        public WheelCollider wheel;
        public Transform mesh;

        void Update()
        {
            if (wheel == null || mesh == null) return;
            wheel.GetWorldPose(out var pos, out var rot);
            mesh.position = pos;
            // 既定のシリンダーは Y 軸が高さなので、車軸（X 方向）に合わせて 90 度起こす
            mesh.rotation = rot * Quaternion.Euler(0f, 0f, 90f);
        }
    }
}
