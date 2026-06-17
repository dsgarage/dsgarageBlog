using System.Collections.Generic;
using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 掛け算ガレージのランタイム操作。地面のファンを左クリックで車体に溶接する。
    /// シーンには実オブジェクトとして配置され、参照は Editor ビルダーが結線する。
    /// </summary>
    public class GarageInteractor : MonoBehaviour
    {
        public Rigidbody chassis;
        public List<PropellerThruster> looseFans = new List<PropellerThruster>();

        public int WeldedFans { get; private set; }
        public int LooseCount => looseFans.Count;

        Camera _cam;

        /// <summary>初期搭載済みファン数（ビルド時に設定）。</summary>
        public void SetInitialWelded(int n) => WeldedFans = n;

        void Start() => _cam = Camera.main;

        void Update()
        {
            if (_cam == null) _cam = Camera.main;
            if (chassis == null || _cam == null) return;

            if (Input.GetMouseButtonDown(0) && looseFans.Count > 0)
            {
                var ray = _cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 200f))
                {
                    var t = hit.collider.GetComponentInParent<PropellerThruster>();
                    if (t != null && looseFans.Contains(t))
                    {
                        looseFans.Remove(t);
                        MountFan(t);
                    }
                }
            }
        }

        Vector3 SlotLocalPos(int index)
        {
            int row = index / 3;
            int col = index % 3;
            return new Vector3(-0.6f + col * 0.6f, 0.85f + row * 0.95f, -1.2f);
        }

        void MountFan(PropellerThruster t)
        {
            var rb = t.GetComponent<Rigidbody>();
            rb.transform.position = chassis.transform.TransformPoint(SlotLocalPos(WeldedFans));
            rb.transform.rotation = chassis.transform.rotation; // forward を車体前方へ
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            AttachmentSystem.Weld(rb, chassis);
            // 溶接後は当たり判定不要。めり込み暴れを防ぐため Collider を無効化
            var col = rb.GetComponent<Collider>();
            if (col != null) col.enabled = false;
            t.Active = true;
            WeldedFans++;
        }
    }
}
