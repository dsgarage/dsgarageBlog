using System.Collections.Generic;
using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>ファン部品が「まだ取り付けられていない」ことを示すマーカー。</summary>
    public class LooseFan : MonoBehaviour
    {
        public PropellerThruster thruster;
        public Transform blade;
    }

    /// <summary>
    /// デモ1: 掛け算のゲームデザイン（Tears of the Kingdom）。
    ///
    /// 車体 + 4 輪（WheelCollider）で走れる土台に、ファン（PropellerThruster）を
    /// ConfigurableJoint で溶接していく。ファンを足すほど推力が積み上がり、
    /// やがて車体が浮き上がる。「部品の組み合わせから挙動が創発する」体験。
    ///
    /// 操作:
    ///   W/S・上下 = 前後、A/D・左右 = 操舵
    ///   Space     = ファン噴射
    ///   左クリック = 地面のファンを車体に溶接（取り付け）
    /// </summary>
    public class MultiplicativeGarageDemo : MonoBehaviour, IPlaygroundDemo
    {
        Rigidbody _chassis;
        VehicleController _vehicle;
        readonly List<LooseFan> _looseFans = new List<LooseFan>();
        int _weldedFans;
        Camera _cam;

        public string Title => "掛け算ガレージ";
        public Transform CameraTarget => _chassis != null ? _chassis.transform : transform;

        void Start()
        {
            _cam = Camera.main;
            Build();
        }

        public void ResetDemo()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
            _looseFans.Clear();
            _weldedFans = 0;
            Build();
        }

        void Build()
        {
            BuildVehicle();
            // 初期搭載のファン 1 基（Space ですぐ体験できるように）
            MountFan(BuildFanBody("Fan_Preinstalled"), slotIndex: 0);
            // 取り付け待ちのファンを 3 基、地面に置く
            for (int i = 0; i < 3; i++)
            {
                var fan = BuildFanBody($"LooseFan_{i}");
                fan.transform.position = new Vector3(-6f + i * 2.2f, 0.6f, 4f);
                var loose = fan.gameObject.AddComponent<LooseFan>();
                loose.thruster = fan.GetComponent<PropellerThruster>();
                loose.thruster.Active = false;
                loose.blade = fan.transform.Find("Blade");
                _looseFans.Add(loose);
            }
        }

        void BuildVehicle()
        {
            var chassisGo = Prim.Box(transform, "Chassis", new Vector3(0f, 1f, 0f),
                                     new Vector3(1.7f, 0.4f, 2.8f), new Color(0.85f, 0.78f, 0.35f));
            _chassis = Prim.AddBody(chassisGo, 150f);
            _chassis.linearDamping = 0.05f;
            _chassis.angularDamping = 0.3f;
            _chassis.centerOfMass = new Vector3(0f, -0.6f, 0f); // 低重心で転倒しにくく

            var wheelDefs = new (string name, Vector3 local, bool drive, bool steer)[]
            {
                ("FL", new Vector3(-0.98f, -0.25f,  1.05f), false, true),
                ("FR", new Vector3( 0.98f, -0.25f,  1.05f), false, true),
                ("RL", new Vector3(-0.98f, -0.25f, -1.05f), true,  false),
                ("RR", new Vector3( 0.98f, -0.25f, -1.05f), true,  false),
            };

            var drive = new List<WheelCollider>();
            var steer = new List<WheelCollider>();
            foreach (var d in wheelDefs)
            {
                var pivot = new GameObject("Wheel_" + d.name);
                pivot.transform.SetParent(chassisGo.transform, false);
                pivot.transform.localPosition = d.local;
                var wc = pivot.AddComponent<WheelCollider>();
                wc.radius = 0.42f;
                wc.suspensionDistance = 0.3f;
                wc.mass = 20f;
                wc.wheelDampingRate = 0.25f;
                wc.forceAppPointDistance = 0f;
                var spring = wc.suspensionSpring;
                spring.spring = 35000f; spring.damper = 4500f; spring.targetPosition = 0.5f;
                wc.suspensionSpring = spring;
                var fwd = wc.forwardFriction; fwd.stiffness = 2f; wc.forwardFriction = fwd;
                var side = wc.sidewaysFriction; side.stiffness = 2.2f; wc.sidewaysFriction = side;

                // 見た目の車輪（物理とは別に世界座標で同期）
                var meshGo = Prim.Cylinder(transform, "WheelMesh_" + d.name, Vector3.zero,
                                           new Vector3(0.84f, 0.16f, 0.84f),
                                           new Color(0.15f, 0.15f, 0.17f));
                var mc = meshGo.GetComponent<Collider>();
                if (mc != null) Destroy(mc);
                var vis = meshGo.AddComponent<WheelVisual>();
                vis.wheel = wc; vis.mesh = meshGo.transform;

                if (d.drive) drive.Add(wc);
                if (d.steer) steer.Add(wc);
            }

            _vehicle = chassisGo.AddComponent<VehicleController>();
            _vehicle.driveWheels = drive.ToArray();
            _vehicle.steerWheels = steer.ToArray();
        }

        Rigidbody BuildFanBody(string name)
        {
            var housing = Prim.Box(transform, name, new Vector3(0f, 0.6f, 0f),
                                   new Vector3(0.9f, 0.9f, 0.22f), new Color(0.30f, 0.55f, 0.85f));
            var rb = Prim.AddBody(housing, 8f);

            var blade = Prim.Cylinder(housing.transform, "Blade", housing.transform.position,
                                      new Vector3(0.7f, 0.04f, 0.7f), new Color(0.9f, 0.9f, 0.95f));
            blade.transform.localPosition = new Vector3(0f, 0f, -0.15f);
            blade.transform.localRotation = Quaternion.Euler(90f, 0f, 0f); // 円盤面を前後に向ける
            var bc = blade.GetComponent<Collider>();
            if (bc != null) Destroy(bc);

            var thruster = housing.AddComponent<PropellerThruster>();
            thruster.bladeVisual = blade.transform;
            return rb;
        }

        // 車体上のマウント位置（ローカル）。前から押すよう forward を車体前方に合わせる
        Vector3 SlotLocalPos(int index)
        {
            // 後部に横並び → さらに上へ積む
            int row = index / 3;
            int col = index % 3;
            return new Vector3(-0.6f + col * 0.6f, 0.55f + row * 0.95f, -1.2f);
        }

        void MountFan(Rigidbody fanRb, int slotIndex)
        {
            fanRb.transform.position = _chassis.transform.TransformPoint(SlotLocalPos(slotIndex));
            fanRb.transform.rotation = _chassis.transform.rotation; // forward を車体前方へ
            fanRb.linearVelocity = Vector3.zero;
            fanRb.angularVelocity = Vector3.zero;
            AttachmentSystem.Weld(fanRb, _chassis);
            _weldedFans++;
        }

        void Update()
        {
            if (_cam == null) _cam = Camera.main;
            if (Input.GetMouseButtonDown(0) && _cam != null && _looseFans.Count > 0)
            {
                var ray = _cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 200f))
                {
                    var loose = hit.collider.GetComponentInParent<LooseFan>();
                    if (loose != null && _looseFans.Contains(loose))
                    {
                        _looseFans.Remove(loose);
                        var rb = loose.GetComponent<Rigidbody>();
                        loose.thruster.Active = true;
                        Destroy(loose); // マーカーを外して通常のファンにする
                        MountFan(rb, _weldedFans);
                    }
                }
            }
        }

        public string GetHudText()
        {
            float speed = _vehicle != null ? _vehicle.CurrentSpeedKmh : 0f;
            return
                "W/S・↑↓ = 前後\n" +
                "A/D・←→ = 操舵\n" +
                "Space = ファン噴射\n" +
                "左クリック = 地面のファンを溶接\n\n" +
                $"速度: {speed:F1} km/h\n" +
                $"搭載ファン: {_weldedFans} 基  /  残り: {_looseFans.Count} 基\n\n" +
                "ファンを増やすほど推力が積み上がり、\n" +
                "やがて車体が浮きます（= 掛け算の創発）。";
        }
    }
}
