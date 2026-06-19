using System.Collections.Generic;
using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 掛け算ガレージのランタイム操作。左クリックで地面のファンを車体に溶接する。
    /// クリックが外れても「次の1基」を溶接するので、確実に推力が増えていく。
    /// </summary>
    public class GarageInteractor : MonoBehaviour
    {
        public Rigidbody chassis;
        public List<PropellerThruster> looseFans = new List<PropellerThruster>();

        [SerializeField] int _weldedFans = 1;        // 初期搭載 1 基（シリアライズして再生時も保持）
        public int WeldedFans => _weldedFans;
        public int LooseCount => looseFans.Count;

        Camera _cam;

        public void SetInitialWelded(int n) => _weldedFans = n;

        void Start() => _cam = Camera.main;

        void Update()
        {
            if (chassis == null || looseFans.Count == 0) return;
            if (!Input.GetMouseButtonDown(0)) return;
            if (_cam == null) _cam = Camera.main;

            // クリック先にファンがあればそれを、無ければ残りの先頭を溶接（確実に増える）
            PropellerThruster target = null;
            if (_cam != null)
            {
                var ray = _cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 500f))
                {
                    var t = hit.collider.GetComponentInParent<PropellerThruster>();
                    if (t != null && looseFans.Contains(t)) target = t;
                }
            }
            if (target == null) target = looseFans[0];

            looseFans.Remove(target);
            MountFan(target);
        }

        /// <summary>MCP / テスト用：残りの先頭ファンを 1 基溶接する。</summary>
        public void WeldNextLooseFan()
        {
            if (looseFans.Count == 0) return;
            var t = looseFans[0];
            looseFans.RemoveAt(0);
            MountFan(t);
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
            // 車体 box の非等倍スケールを無視して配置（TransformPoint はスケールを掛けてしまう）
            rb.transform.position = chassis.transform.position
                + chassis.transform.rotation * SlotLocalPos(_weldedFans);
            rb.transform.rotation = chassis.transform.rotation; // forward を車体前方へ
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            AttachmentSystem.Weld(rb, chassis);
            // 溶接後は当たり判定不要。めり込み暴れを防ぐため Collider を無効化
            var col = rb.GetComponent<Collider>();
            if (col != null) col.enabled = false;
            t.Active = true;
            _weldedFans++;
        }
    }
}
