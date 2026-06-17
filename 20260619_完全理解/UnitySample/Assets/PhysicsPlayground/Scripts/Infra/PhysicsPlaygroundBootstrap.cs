using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// プレイグラウンド全体の起点。空の GameObject にこれ 1 つを付けて Play すれば、
    /// 地面・ライト・カメラ・HUD を生成し、デモを読み込む。シーンの手組みは不要。
    ///
    /// 切り替え: [1] 掛け算ガレージ / [2] 構造崩壊 / [R] リセット
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class PhysicsPlaygroundBootstrap : MonoBehaviour
    {
        public int startDemo = 0;

        OrbitCamera _cam;
        GameObject _demoRoot;
        MonoBehaviour _demoBehaviour;
        int _current = -1;

        public IPlaygroundDemo CurrentDemo => _demoBehaviour as IPlaygroundDemo;

        void Start()
        {
            BuildEnvironment();
            LoadDemo(Mathf.Clamp(startDemo, 0, 1));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) LoadDemo(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) LoadDemo(1);
            else if (Input.GetKeyDown(KeyCode.R)) CurrentDemo?.ResetDemo();

            // カメラ追従ターゲットを毎フレーム反映
            if (_cam != null && CurrentDemo != null)
                _cam.target = CurrentDemo.CameraTarget;
        }

        void BuildEnvironment()
        {
            // 地面
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.localScale = new Vector3(12f, 1f, 12f); // 120m 四方
            Prim.Paint(ground, new Color(0.20f, 0.22f, 0.26f));

            // ライト
            var lightGo = new GameObject("Sun");
            var light = lightGo.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.1f;
            light.shadows = LightShadows.Soft;
            lightGo.transform.rotation = Quaternion.Euler(48f, -28f, 0f);
            RenderSettings.ambientLight = new Color(0.45f, 0.47f, 0.52f);

            // カメラ
            var camGo = new GameObject("PlaygroundCamera");
            camGo.tag = "MainCamera";
            var cam = camGo.AddComponent<Camera>();
            cam.backgroundColor = new Color(0.10f, 0.12f, 0.16f);
            cam.clearFlags = CameraClearFlags.SolidColor;
            camGo.AddComponent<AudioListener>();
            _cam = camGo.AddComponent<OrbitCamera>();

            // HUD
            var hudGo = new GameObject("HUD");
            var hud = hudGo.AddComponent<PlaygroundHUD>();
            hud.bootstrap = this;
        }

        void LoadDemo(int index)
        {
            if (index == _current) return;
            _current = index;

            if (_demoRoot != null) Destroy(_demoRoot);

            _demoRoot = new GameObject(index == 0 ? "Demo_Garage" : "Demo_Collapse");
            switch (index)
            {
                case 0:  _demoBehaviour = _demoRoot.AddComponent<MultiplicativeGarageDemo>(); break;
                default: _demoBehaviour = _demoRoot.AddComponent<StructuralCollapseDemo>();  break;
            }
        }
    }
}
