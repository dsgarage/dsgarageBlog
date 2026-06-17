#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PhysicsPlayground.EditorTools
{
    /// <summary>
    /// 掛け算ガレージのデモを「編集時に実オブジェクトとして配置」してシーンに保存する。
    /// 開いた時点で車・地面・ファンが見え、そのまま Play で動く。
    /// </summary>
    public static class PlaygroundMenu
    {
        const string SceneDir = "Assets/Scenes";
        const string ScenePath = SceneDir + "/PhysicsPlayground.unity";

        [MenuItem("Tools/GDC Physics Playground/Build Demo Scene")]
        public static void BuildDemoScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                    NewSceneMode.Single);

            BuildEnvironment(out var orbit);
            var chassis = BuildVehicle();
            orbit.target = chassis.transform;

            // 初期搭載のファン 1 基（Space ですぐ体験できる）
            var fan0 = BuildFan("Fan_Preinstalled");
            MountFan(fan0, chassis, 0);

            // 取り付け待ちのファン 3 基を地面に置く
            var loose = new List<PropellerThruster>();
            for (int i = 0; i < 3; i++)
            {
                var fan = BuildFan($"LooseFan_{i}");
                fan.transform.position = new Vector3(-6f + i * 2.2f, 0.6f, 4f);
                fan.GetComponent<PropellerThruster>().Active = false;
                loose.Add(fan.GetComponent<PropellerThruster>());
            }

            // ランタイム操作（クリック溶接）と HUD を結線
            var systems = new GameObject("Systems");
            var interactor = systems.AddComponent<GarageInteractor>();
            interactor.chassis = chassis;
            interactor.looseFans = loose;
            interactor.SetInitialWelded(1);

            var hudGo = new GameObject("HUD");
            var hud = hudGo.AddComponent<GarageHUD>();
            hud.vehicle = chassis.GetComponent<VehicleController>();
            hud.interactor = interactor;

            // 保存
            if (!AssetDatabase.IsValidFolder(SceneDir))
                AssetDatabase.CreateFolder("Assets", "Scenes");
            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.Refresh();

            Debug.Log($"[GDC Physics Playground] デモシーンを配置・保存しました: {ScenePath} " +
                      "（車・地面・ファンを配置。Play で走行 / W,S=前後 A,D=操舵 Space=噴射 左クリック=ファン溶接）");
        }

        [MenuItem("Tools/GDC Physics Playground/Open Demo Scene")]
        public static void OpenDemoScene()
        {
            if (System.IO.File.Exists(ScenePath))
                EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            else
                BuildDemoScene();
        }

        // ---- 構築ヘルパ ----

        static void BuildEnvironment(out OrbitCamera orbit)
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.localScale = new Vector3(12f, 1f, 12f);
            Prim.Paint(ground, new Color(0.20f, 0.22f, 0.26f));

            var lightGo = new GameObject("Sun");
            var light = lightGo.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.1f;
            light.shadows = LightShadows.Soft;
            lightGo.transform.rotation = Quaternion.Euler(48f, -28f, 0f);
            RenderSettings.ambientLight = new Color(0.45f, 0.47f, 0.52f);

            var camGo = new GameObject("PlaygroundCamera");
            camGo.tag = "MainCamera";
            var cam = camGo.AddComponent<Camera>();
            cam.backgroundColor = new Color(0.10f, 0.12f, 0.16f);
            cam.clearFlags = CameraClearFlags.SolidColor;
            camGo.AddComponent<AudioListener>();
            // 編集時にも見えるよう初期姿勢を与える（Play では OrbitCamera が制御）
            camGo.transform.position = new Vector3(8f, 7f, -12f);
            camGo.transform.LookAt(new Vector3(0f, 1f, 0f));
            orbit = camGo.AddComponent<OrbitCamera>();
        }

        static Rigidbody BuildVehicle()
        {
            var chassisGo = Prim.Box(null, "Chassis", new Vector3(0f, 1f, 0f),
                                     new Vector3(1.7f, 0.4f, 2.8f), new Color(0.85f, 0.78f, 0.35f));
            var chassis = Prim.AddBody(chassisGo, 150f);
            chassis.linearDamping = 0.05f;
            chassis.angularDamping = 0.3f;
            chassis.centerOfMass = new Vector3(0f, -0.6f, 0f);

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

                var meshGo = Prim.Cylinder(null, "WheelMesh_" + d.name, pivot.transform.position,
                                           new Vector3(0.84f, 0.16f, 0.84f),
                                           new Color(0.15f, 0.15f, 0.17f));
                var mc = meshGo.GetComponent<Collider>();
                if (mc != null) Object.DestroyImmediate(mc);
                // 編集時の見た目用に初期姿勢を合わせる（Play では WheelVisual が同期）
                meshGo.transform.rotation = chassisGo.transform.rotation * Quaternion.Euler(0f, 0f, 90f);
                var vis = meshGo.AddComponent<WheelVisual>();
                vis.wheel = wc; vis.mesh = meshGo.transform;

                if (d.drive) drive.Add(wc);
                if (d.steer) steer.Add(wc);
            }

            var vehicle = chassisGo.AddComponent<VehicleController>();
            vehicle.driveWheels = drive.ToArray();
            vehicle.steerWheels = steer.ToArray();
            return chassis;
        }

        static Rigidbody BuildFan(string name)
        {
            var housing = Prim.Box(null, name, new Vector3(0f, 0.6f, 0f),
                                   new Vector3(0.9f, 0.9f, 0.22f), new Color(0.30f, 0.55f, 0.85f));
            var rb = Prim.AddBody(housing, 8f);

            var blade = Prim.Cylinder(housing.transform, "Blade", housing.transform.position,
                                      new Vector3(0.7f, 0.04f, 0.7f), new Color(0.9f, 0.9f, 0.95f));
            blade.transform.localPosition = new Vector3(0f, 0f, -0.15f);
            blade.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            var bc = blade.GetComponent<Collider>();
            if (bc != null) Object.DestroyImmediate(bc);

            var thruster = housing.AddComponent<PropellerThruster>();
            thruster.bladeVisual = blade.transform;
            return rb;
        }

        static Vector3 SlotLocalPos(int index)
        {
            int row = index / 3;
            int col = index % 3;
            return new Vector3(-0.6f + col * 0.6f, 0.85f + row * 0.95f, -1.2f);
        }

        static void MountFan(Rigidbody fanRb, Rigidbody chassis, int slotIndex)
        {
            fanRb.transform.position = chassis.transform.TransformPoint(SlotLocalPos(slotIndex));
            fanRb.transform.rotation = chassis.transform.rotation;
            AttachmentSystem.Weld(fanRb, chassis);
            // 溶接後は当たり判定不要。車体や他ファンとのめり込み暴れを防ぐため Collider を無効化
            var col = fanRb.GetComponent<Collider>();
            if (col != null) col.enabled = false;
            fanRb.GetComponent<PropellerThruster>().Active = true;
        }
    }
}
#endif
