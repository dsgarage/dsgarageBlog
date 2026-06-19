#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsPlayground.EditorTools
{
    /// <summary>
    /// 「物理の掛け合わせ」7機構デモを別シーンで生成する。
    /// いずれも AddForce で押さない。HingeJoint / ConfigurableJoint / 重力 / 衝突 が挙動を解く。
    /// ロジックは「モーターを回す」「初期角を与える」程度で、創発はソルバー任せ。
    ///
    /// 見せ方の方針（分かりやすさ）:
    ///   ・入力（動力）はオレンジ、出力（創発した結果）はシアンで色分け
    ///   ・支点（ヒンジ）は白い小球マーカーで明示
    ///   ・速くて追えない動きには残像トレイルを付ける
    ///   ・HUD に「注目ポイント」と「実況（数値）」を出す。T キーでスロー再生
    /// </summary>
    public static class PlaygroundMenu
    {
        const string SceneDir = "Assets/Scenes";
        const string M = "Tools/GDC Physics Playground/";

        [MenuItem(M + "1 SliderCrank")] public static void S1() => Build("01_SliderCrank", BuildSliderCrank);
        [MenuItem(M + "2 Whegs (LegWalk)")] public static void S2() => Build("02_Whegs", BuildWhegs);
        [MenuItem(M + "3 DoublePendulum")] public static void S3() => Build("03_DoublePendulum", BuildDoublePendulum);
        [MenuItem(M + "4 NewtonsCradle")] public static void S4() => Build("04_NewtonsCradle", BuildNewtonsCradle);
        [MenuItem(M + "5 PendulumWave")] public static void S5() => Build("05_PendulumWave", BuildPendulumWave);
        [MenuItem(M + "6 RimlessWheel")] public static void S6() => Build("06_RimlessWheel", BuildRimlessWheel);
        [MenuItem(M + "7 Catapult")] public static void S7() => Build("07_Catapult", BuildCatapult);

        [MenuItem(M + "Build ALL")]
        public static void All()
        {
            S1(); S2(); S3(); S4(); S5(); S6(); S7();
            Debug.Log("[物理の掛け合わせ] 全7機構シーンを生成しました");
        }

        static void Build(string file, System.Action body)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            Sky();
            body();
            // 各デモから「メニューへ戻る」（右上ボタン / Esc・M キー）
            new GameObject("ReturnToMenu").AddComponent<ReturnToMenu>();
            if (!AssetDatabase.IsValidFolder(SceneDir)) AssetDatabase.CreateFolder("Assets", "Scenes");
            EditorSceneManager.SaveScene(scene, SceneDir + "/" + file + ".unity");
            AssetDatabase.Refresh();
            Debug.Log("[物理の掛け合わせ] 生成: " + file);
        }

        // ===== 共通色 =====
        static readonly Color GroundCol = new Color(0.20f, 0.22f, 0.26f);
        static readonly Color Metal = new Color(0.55f, 0.58f, 0.65f);
        static readonly Color Wood = new Color(0.82f, 0.6f, 0.32f);
        // 意味づけの色（HUD の凡例と一致させる）
        static readonly Color InputCol = new Color(1f, 0.62f, 0.25f);   // 入力（動力）= オレンジ
        static readonly Color OutputCol = new Color(0.35f, 0.85f, 0.95f); // 出力（結果）= シアン
        static readonly Color PivotCol = new Color(0.97f, 0.97f, 1f);   // 支点 = 白

        static void Sky()
        {
            var l = new GameObject("Sun");
            var li = l.AddComponent<Light>();
            li.type = LightType.Directional; li.intensity = 1.1f; li.shadows = LightShadows.Soft;
            l.transform.rotation = Quaternion.Euler(48f, -28f, 0f);
            RenderSettings.ambientLight = new Color(0.45f, 0.47f, 0.52f);
        }

        static void Ground(float scale, float y = 0f)
        {
            var g = GameObject.CreatePrimitive(PrimitiveType.Plane);
            g.name = "Ground"; g.transform.position = new Vector3(0f, y, 0f);
            g.transform.localScale = new Vector3(scale, 1f, scale);
            Prim.Paint(g, GroundCol);
        }

        static void Cam(Vector3 pos, Vector3 look, float dist, float pitch, float yaw = 0f)
        {
            var go = new GameObject("PlaygroundCamera"); go.tag = "MainCamera";
            var cam = go.AddComponent<Camera>();
            cam.backgroundColor = new Color(0.10f, 0.12f, 0.16f);
            cam.clearFlags = CameraClearFlags.SolidColor;
            go.AddComponent<AudioListener>();
            go.transform.position = pos; go.transform.LookAt(look);
            var f = new GameObject("CamFocus"); f.transform.position = look;
            var o = go.AddComponent<OrbitCamera>();
            o.target = f.transform; o.distance = dist; o.yaw = yaw; o.pitch = pitch;
        }

        static void Hud(string title, string sub, string watch = "", bool legend = true)
        {
            var h = new GameObject("HUD");
            var hud = h.AddComponent<CrossbreedHUD>();
            hud.titleText = title; hud.subtitleText = sub;
            hud.watchText = watch; hud.showLegend = legend;
        }

        static void Label(GameObject go, string text, Color c, float y, int fs = 14)
        {
            var w = go.AddComponent<WorldLabel>();
            w.text = text; w.color = c; w.worldOffset = new Vector3(0f, y, 0f); w.fontSize = fs;
        }

        // ---- 可視化の小物 ----

        /// <summary>支点（ヒンジ位置）を示す白い小球マーカー（当たり判定なし）。</summary>
        static GameObject Pivot(Vector3 world, float dia = 0.2f)
        {
            var s = Prim.Sphere(null, "Pivot", world, dia, PivotCol);
            var c = s.GetComponent<Collider>(); if (c != null) Object.DestroyImmediate(c);
            return s;
        }

        /// <summary>動く部品に残像トレイルを付け、軌跡を見えるようにする。</summary>
        static void Trail(GameObject go, Color c, float width = 0.1f, float time = 1.3f)
        {
            var t = go.AddComponent<TrailSetup>();
            t.color = c; t.width = width; t.time = time;
        }

        /// <summary>HUD に出す「実況」（対象の速さ・角速度・高さ）を登録する。</summary>
        static void Readout(Rigidbody target, LiveReadout.Kind kind, string label, int order = 0)
        {
            if (target == null) return;
            var lr = target.gameObject.AddComponent<LiveReadout>();
            lr.target = target; lr.kind = kind; lr.label = label; lr.order = order;
        }

        // 静止アンカー(キネマティック剛体)
        static Rigidbody Frame(string name, Vector3 pos, Vector3 size, Color c)
        {
            var b = Prim.Box(null, name, pos, size, c);
            var rb = b.AddComponent<Rigidbody>(); rb.isKinematic = true;
            return rb;
        }

        // 振り子(bob + 見た目ロッド + ヒンジ)。parent=null で世界に固定。
        static Rigidbody Pendulum(string name, Vector3 pivot, Vector3 bobPos, float dia,
                                  float mass, Color col, Rigidbody parent,
                                  bool bobCollider, PhysicsMaterial mat = null)
        {
            var bob = Prim.Sphere(null, name, bobPos, dia, col);
            var rb = Prim.AddBody(bob, mass);
            var col0 = bob.GetComponent<Collider>();
            if (!bobCollider) { if (col0 != null) Object.DestroyImmediate(col0); }
            else if (mat != null && col0 != null) col0.material = mat;

            // 見た目ロッド(bob の子。剛体振り子なので一緒に動く)
            Vector3 mid = (pivot + bobPos) * 0.5f;
            float len = (bobPos - pivot).magnitude;
            var rod = Prim.Box(bob.transform, name + "_Rod", mid,
                               new Vector3(0.06f, len, 0.06f), new Color(0.7f, 0.7f, 0.75f));
            rod.transform.position = mid;
            rod.transform.rotation = Quaternion.FromToRotation(Vector3.up, (bobPos - pivot).normalized);
            var rc = rod.GetComponent<Collider>(); if (rc != null) Object.DestroyImmediate(rc);

            var h = bob.AddComponent<HingeJoint>();
            h.axis = new Vector3(0f, 0f, 1f);
            h.autoConfigureConnectedAnchor = true;
            h.anchor = bob.transform.InverseTransformPoint(pivot);
            if (parent != null) h.connectedBody = parent;
            return rb;
        }

        // ===== ① スライダークランク(回転→往復) =====
        static void BuildSliderCrank()
        {
            Hud("① スライダークランク ― 回転 → 往復",
                "クランクは一定速度で回すだけ。上下の往復は“リンク幾何”が生む",
                "オレンジの回転が、ロッドを介してシアンの上下往復に変換される");
            Cam(new Vector3(5f, 3.2f, -7f), new Vector3(0f, 3.5f, 0f), 11f, 8f, -12f);
            Ground(6f);

            var baseRb = Frame("Base", new Vector3(0f, 1.5f, 0f), new Vector3(0.5f, 3f, 0.5f), Metal);

            // クランク = 入力。キネマティック剛体を Z 軸で一定回転（モーター閉ループの噛みを回避）
            Vector3 C = new Vector3(0f, 3.0f, 0f);
            const float r = 0.6f;                 // 偏心ピン半径
            var crankGo = new GameObject("Crank");
            crankGo.transform.position = C;
            var crankRb = crankGo.AddComponent<Rigidbody>();
            var spin = crankGo.AddComponent<KinematicSpin>();
            spin.axis = Vector3.forward; spin.degPerSec = 120f;
            // 見た目の円盤（子。回転は見た目だけ）
            var disc = Prim.Cylinder(crankGo.transform, "Disc", C, new Vector3(1.3f, 0.16f, 1.3f), InputCol);
            disc.transform.localPosition = Vector3.zero;
            disc.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            var dc = disc.GetComponent<Collider>(); if (dc != null) Object.DestroyImmediate(dc);
            // 見た目の偏心ピン（子）
            var pinViz = Prim.Sphere(crankGo.transform, "Pin", C + new Vector3(r, 0f, 0f), 0.22f,
                                     new Color(1f, 0.85f, 0.5f));
            var pvc = pinViz.GetComponent<Collider>(); if (pvc != null) Object.DestroyImmediate(pvc);

            // ピストン(垂直スライド) = 出力
            Vector3 pistonPos = new Vector3(0f, 4.7f, 0f);
            var piston = Prim.Box(null, "Piston", pistonPos, new Vector3(0.7f, 0.5f, 0.7f), OutputCol);
            var pistonRb = Prim.AddBody(piston, 1f);
            var slide = piston.AddComponent<ConfigurableJoint>();
            slide.connectedBody = baseRb;
            slide.autoConfigureConnectedAnchor = true;
            slide.xMotion = ConfigurableJointMotion.Locked;
            slide.yMotion = ConfigurableJointMotion.Free;
            slide.zMotion = ConfigurableJointMotion.Locked;
            slide.angularXMotion = slide.angularYMotion = slide.angularZMotion = ConfigurableJointMotion.Locked;

            // コンロッド: 下端=クランクの偏心ピン、上端=ピストン（アンカーは InverseTransformPoint で安全に）
            Vector3 pinWorld = C + new Vector3(r, 0f, 0f);
            Vector3 mid = (pinWorld + pistonPos) * 0.5f;
            float len = (pistonPos - pinWorld).magnitude;
            var rod = Prim.Box(null, "Rod", mid, new Vector3(0.14f, len, 0.14f), Wood);
            rod.transform.position = mid;
            rod.transform.rotation = Quaternion.FromToRotation(Vector3.up, (pistonPos - pinWorld).normalized);
            var rodRb = Prim.AddBody(rod, 0.3f);
            var rc = rod.GetComponent<Collider>(); if (rc != null) Object.DestroyImmediate(rc);

            var jLow = rod.AddComponent<HingeJoint>();
            jLow.axis = new Vector3(0f, 0f, 1f);
            jLow.connectedBody = crankRb;
            jLow.autoConfigureConnectedAnchor = false;
            jLow.anchor = rod.transform.InverseTransformPoint(pinWorld);
            jLow.connectedAnchor = crankRb.transform.InverseTransformPoint(pinWorld);
            var jHigh = rod.AddComponent<HingeJoint>();
            jHigh.axis = new Vector3(0f, 0f, 1f);
            jHigh.connectedBody = pistonRb;
            jHigh.autoConfigureConnectedAnchor = false;
            jHigh.anchor = rod.transform.InverseTransformPoint(pistonPos);
            jHigh.connectedAnchor = pistonRb.transform.InverseTransformPoint(pistonPos);

            Pivot(C, 0.18f);
            Trail(piston, OutputCol, 0.1f, 0.7f);
            Readout(pistonRb, LiveReadout.Kind.Speed, "出力 往復", 0);
            Label(piston, "出力：上下に往復", OutputCol, 0.7f, 13);
            Label(crankGo, "入力：回転", InputCol, 1.2f, 13);
        }

        // ===== ② Whegs(回転 → 歩行) =====
        static void BuildWhegs()
        {
            Hud("② 回転 → 歩行(Whegs)",
                "丸い車輪でなく“3本脚の車輪”。モーターの回転が、脚の接地で歩行に変わる",
                "オレンジの脚車輪が回ると、シアンの車体が歩いて前進する",
                legend: true);
            Cam(new Vector3(7f, 4f, -7f), new Vector3(2f, 0.8f, 2f), 16f, 16f, -18f);
            Ground(10f);

            var chassis = Prim.Box(null, "Chassis", new Vector3(-3f, 1.0f, 0f),
                                   new Vector3(1.2f, 0.4f, 2.0f), OutputCol);
            var crb = Prim.AddBody(chassis, 4f); crb.centerOfMass = new Vector3(0f, -0.2f, 0f);

            // 4つの「脚車輪」(3本スポーク)をモーターで回す
            var pos = new[]
            {
                new Vector3(-0.75f, -0.1f,  0.7f), new Vector3(0.75f, -0.1f,  0.7f),
                new Vector3(-0.75f, -0.1f, -0.7f), new Vector3(0.75f, -0.1f, -0.7f),
            };
            for (int i = 0; i < 4; i++)
                Wheg(crb, pos[i], (i % 2 == 0) ? 0f : 60f); // 左右で位相をずらす

            Trail(chassis, OutputCol, 0.08f, 1.6f);
            Readout(crb, LiveReadout.Kind.Speed, "出力 前進", 0);
            Label(chassis, "出力：脚で歩いて前進", OutputCol, 1.0f, 13);
        }

        static void Wheg(Rigidbody chassis, Vector3 localPos, float phase)
        {
            Vector3 world = chassis.transform.position + chassis.transform.rotation * localPos;
            var hub = new GameObject("Wheg");
            hub.transform.position = world;
            hub.transform.rotation = chassis.transform.rotation * Quaternion.Euler(0f, 0f, phase);
            var rb = hub.AddComponent<Rigidbody>(); rb.mass = 1.5f;

            for (int s = 0; s < 3; s++)
            {
                float a = s * 120f;
                Vector3 dir = Quaternion.AngleAxis(a, Vector3.right) * Vector3.down; // YZ面に放射
                var leg = Prim.Capsule(hub.transform, "Leg", world,
                                       new Vector3(0.14f, 0.45f, 0.14f), InputCol);
                leg.transform.localPosition = dir * 0.45f;
                leg.transform.localRotation = Quaternion.FromToRotation(Vector3.up, dir);
            }
            var hinge = hub.AddComponent<HingeJoint>();
            hinge.connectedBody = chassis; hinge.axis = new Vector3(1f, 0f, 0f);
            hinge.anchor = Vector3.zero; hinge.autoConfigureConnectedAnchor = true;
            var hm = hub.AddComponent<HingeMotor>(); hm.targetVelocity = 220f; hm.force = 250f;
        }

        // ===== ③ 二重振り子(カオス) =====
        static void BuildDoublePendulum()
        {
            Hud("③ 二重振り子 ― 結合が生むカオス",
                "力ゼロ。2つのヒンジを繋ぐだけで、二度と同じにならない軌道が生まれる",
                "先端（赤）の軌跡トレイルに注目。毎回ちがう＝カオス。T でスローに",
                legend: false);
            Cam(new Vector3(0f, 4.5f, -8f), new Vector3(0f, 3.5f, 0f), 9f, 4f);

            var anchor = Frame("Anchor", new Vector3(0f, 6f, 0f), new Vector3(0.4f, 0.4f, 0.4f), Metal);
            // 水平から落とす(初期角=横) → カオス
            var a = Pendulum("LinkA", new Vector3(0f, 6f, 0f), new Vector3(2.2f, 6f, 0f),
                             0.5f, 1.5f, new Color(0.3f, 0.6f, 0.9f), anchor, false);
            var b = Pendulum("LinkB", new Vector3(2.2f, 6f, 0f), new Vector3(4.4f, 6f, 0f),
                     0.5f, 1.0f, new Color(0.9f, 0.35f, 0.35f), a, false);

            Pivot(new Vector3(0f, 6f, 0f));            // 固定支点
            Trail(b.gameObject, new Color(0.95f, 0.4f, 0.4f), 0.12f, 2.5f); // 先端の軌跡
            Trail(a.gameObject, new Color(0.4f, 0.7f, 1f), 0.07f, 1.2f);    // 第1リンク
            Readout(b, LiveReadout.Kind.Speed, "先端の速さ", 0);
            Label(anchor.gameObject, "重力だけ → 予測不能", new Color(1f, 0.9f, 0.7f), 0.6f, 13);
        }

        // ===== ④ ニュートンのゆりかご(運動量) =====
        static void BuildNewtonsCradle()
        {
            Hud("④ ニュートンのゆりかご ― 運動量の伝達",
                "端の1球が、間の球を素通りして反対端の1球だけを弾く(衝突＝物理が解く)",
                "オレンジの1球を放すと、止まっている間を飛ばしてシアンの1球だけが弾かれる");
            Cam(new Vector3(0f, 3f, -7f), new Vector3(0f, 2.5f, 0f), 8f, 6f);

            var top = Frame("Top", new Vector3(0f, 4.2f, 0f), new Vector3(5f, 0.2f, 0.6f), Metal);
            var bouncy = new PhysicsMaterial("Steel")
            {
                bounciness = 1f, dynamicFriction = 0f, staticFriction = 0f,
                bounceCombine = PhysicsMaterialCombine.Maximum, frictionCombine = PhysicsMaterialCombine.Minimum
            };

            float d = 0.7f; // 球径=間隔(接触)
            int n = 5; float x0 = -(n - 1) * 0.5f * d;
            for (int i = 0; i < n; i++)
            {
                float x = x0 + i * d;
                Vector3 pivot = new Vector3(x, 4.1f, 0f);
                Vector3 bobPos;
                Color col;
                if (i == 0) { bobPos = pivot + new Vector3(-1.6f, -1.0f, 0f); col = InputCol; }      // 入力：端を持ち上げて放す
                else if (i == n - 1) { bobPos = pivot + new Vector3(0f, -1.9f, 0f); col = OutputCol; } // 出力：弾かれる端
                else { bobPos = pivot + new Vector3(0f, -1.9f, 0f); col = Metal; }
                var rb = Pendulum("Ball" + i, pivot, bobPos, d, 1f, col, top, true, bouncy);
                rb.solverIterations = 30; rb.solverVelocityIterations = 30;
                rb.maxAngularVelocity = 50f;
                Pivot(pivot, 0.14f);
                if (i == 0) Trail(rb.gameObject, InputCol, 0.08f, 0.7f);
                if (i == n - 1) Trail(rb.gameObject, OutputCol, 0.08f, 0.7f);
            }
            Label(top.gameObject, "1球 → 1球へ", new Color(1f, 0.9f, 0.7f), 0.5f, 13);
        }

        // ===== ⑤ 振り子ウェーブ =====
        static void BuildPendulumWave()
        {
            Hud("⑤ 振り子ウェーブ ― 長さの差が生む波",
                "力ゼロ。長さを少しずつ変えた振り子が、揃って離れて再び揃い、波に見える",
                "全部を同時に放すだけ。長さの差だけで“波”が走り、やがて再び揃う",
                legend: false);
            Cam(new Vector3(0f, 5f, -9f), new Vector3(0f, 3.2f, 1.5f), 12f, 14f);

            var bar = Frame("Bar", new Vector3(0f, 5f, 0f), new Vector3(0.3f, 0.3f, 7f), Metal);
            int n = 14;
            for (int i = 0; i < n; i++)
            {
                float z = -3f + i * (6f / (n - 1));
                // 周期 ∝ √L。i 本目が一定時間に (k+i) 回振れるよう長さを設定
                float osc = 20f + i;            // 共通サイクルでの振動回数
                float L = 9.81f * Mathf.Pow(60f / (2f * Mathf.PI * osc), 2f); // T=60/osc
                L = Mathf.Clamp(L, 0.8f, 4.2f);
                Vector3 pivot = new Vector3(0f, 5f, z);
                Vector3 bob = pivot + new Vector3(2.0f, -L, 0f); // 同じ初期角(横へ)
                Color c = Color.HSVToRGB(i / (float)n, 0.6f, 0.95f);
                var rb = Pendulum("P" + i, pivot, bob, 0.35f, 0.6f, c, bar, false);
                Trail(rb.gameObject, c, 0.05f, 0.45f);
            }
            Label(bar.gameObject, "長さの差 → 波", new Color(1f, 0.9f, 0.7f), 0.6f, 13);
        }

        // ===== ⑥ 受動歩行(リムレスホイール) =====
        static void BuildRimlessWheel()
        {
            Hud("⑥ 受動歩行 ― 重力だけで歩く",
                "動力なし。スポークだけの車輪が、坂で重力に任せてカタカタ降りる",
                "モーターは無い。坂の重力だけで、シアンの車輪が一歩ずつ転がり降りる",
                legend: false);
            Cam(new Vector3(10f, 5f, 0f), new Vector3(0f, 2f, 0f), 18f, 14f, -35f);

            // 幅広の緩い坂(脱輪しないよう広め)。+z 端を上げる → 下りは -z 方向(手前)
            StaticBoxRot("Slope", new Vector3(0f, 0f, 0f), new Vector3(7f, 0.4f, 30f),
                         new Color(0.24f, 0.26f, 0.30f), Quaternion.Euler(-9f, 0f, 0f));

            int spokes = 8; float R = 1.0f;
            var hub = new GameObject("RimlessWheel");
            // 坂の高い側(+z)に接地配置 → 重力で -z へ歩いて降りる
            hub.transform.position = new Vector3(0f, 2.7f, 9f);
            var hubMesh = Prim.Cylinder(hub.transform, "Hub", hub.transform.position,
                                        new Vector3(0.22f, 0.16f, 0.22f), OutputCol);
            hubMesh.transform.localPosition = Vector3.zero;
            hubMesh.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            var hmc = hubMesh.GetComponent<Collider>(); if (hmc != null) Object.DestroyImmediate(hmc);
            for (int i = 0; i < spokes; i++)
            {
                float a = i * (360f / spokes);
                Vector3 dir = Quaternion.AngleAxis(a, Vector3.right) * Vector3.down;
                var leg = Prim.Capsule(hub.transform, "Spoke", hub.transform.position,
                                       new Vector3(0.12f, R * 0.5f, 0.12f), new Color(0.8f, 0.55f, 0.28f));
                leg.transform.localPosition = dir * (R * 0.5f);
                leg.transform.localRotation = Quaternion.FromToRotation(Vector3.up, dir);
            }
            var rb = hub.AddComponent<Rigidbody>(); rb.mass = 4f; rb.angularDamping = 0.05f;
            // 横倒れ・脱輪を防ぐ(進行=Z, 回転軸=X のみ許可)
            rb.constraints = RigidbodyConstraints.FreezePositionX
                           | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            Trail(hubMesh, OutputCol, 0.1f, 2.0f);
            Readout(rb, LiveReadout.Kind.Speed, "車輪の速さ", 0);
            Label(hub, "動力ゼロ → 歩いて降りる", new Color(1f, 0.9f, 0.6f), 1.5f, 14);
        }

        // ===== ⑦ カタパルト(てこ×拘束開放) =====
        static void BuildCatapult()
        {
            Hud("⑦ カタパルト ― てこ × 拘束開放",
                "①玉をのせて構える → ②腕を振り上げて発射 → ③放物線で着地（数秒ごとに繰り返す）",
                "オレンジの腕がゆっくり振り上がり、シアンの玉が放物線を描いて飛ぶ");
            Cam(new Vector3(10f, 5.5f, -9f), new Vector3(2.5f, 3.5f, 1.5f), 23f, 14f, -16f);
            Ground(16f);

            var postRb = Frame("Post", new Vector3(0f, 1.3f, 0f), new Vector3(0.5f, 2.6f, 0.5f), Metal);

            // 腕(てこ)。支点は左寄り → 右が長い投擲腕。長く・幅広にして玉が落ちないように
            Vector3 pivot = new Vector3(0f, 2.6f, 0f);
            var arm = Prim.Box(null, "Arm", new Vector3(1.0f, 2.6f, 0f), new Vector3(5.4f, 0.24f, 0.9f), InputCol);
            var arb = Prim.AddBody(arm, 3f);
            var hinge = arm.AddComponent<HingeJoint>();
            hinge.axis = new Vector3(0f, 0f, 1f);
            hinge.connectedBody = postRb;
            hinge.autoConfigureConnectedAnchor = false;
            hinge.anchor = arm.transform.InverseTransformPoint(pivot);
            hinge.connectedAnchor = postRb.transform.InverseTransformPoint(pivot);
            hinge.useLimits = true;
            var lim = hinge.limits; lim.min = -6f; lim.max = 58f; hinge.limits = lim;

            // 見た目の重り(短い側・子)
            var weight = Prim.Box(arm.transform, "Counterweight", arm.transform.position,
                                  new Vector3(0.8f, 0.8f, 0.8f), new Color(0.55f, 0.58f, 0.65f));
            weight.transform.localPosition = new Vector3(-2.0f, -0.1f, 0f);
            var wc = weight.GetComponent<Collider>(); if (wc != null) Object.DestroyImmediate(wc);

            // 玉を受けるカップ（玉がすっぽり収まる幅・深さの壁。前後左右に壁を置く）
            void CupWall(string n, Vector3 lp, Vector3 sz)
            {
                var w = Prim.Box(arm.transform, n, arm.transform.position, sz, new Color(0.9f, 0.7f, 0.4f));
                w.transform.localPosition = lp;
            }
            CupWall("CupIn", new Vector3(0.40f, 0.36f, 0f), new Vector3(0.14f, 0.6f, 0.9f));   // 支点側の壁
            CupWall("CupOut", new Vector3(1.95f, 0.36f, 0f), new Vector3(0.14f, 0.6f, 0.9f));  // 先端側の壁
            CupWall("CupZ1", new Vector3(1.18f, 0.34f, 0.42f), new Vector3(1.7f, 0.5f, 0.1f)); // 横壁
            CupWall("CupZ2", new Vector3(1.18f, 0.34f, -0.42f), new Vector3(1.7f, 0.5f, 0.1f));// 横壁

            // 玉(カップの中) = 出力
            var ball = Prim.Sphere(null, "Projectile", new Vector3(2.18f, 3.15f, 0f), 0.4f, OutputCol);
            var ballRb = Prim.AddBody(ball, 0.35f);

            // 段階制御で「構える→発射→放物線→リセット」を繰り返す
            var ctrlGo = new GameObject("CatapultDemo");
            var ctrl = ctrlGo.AddComponent<CatapultDemo>();
            ctrl.hinge = hinge; ctrl.arm = arb; ctrl.ball = ballRb;
            ctrl.holdTime = 1.7f; ctrl.fireTime = 1.9f;
            ctrl.holdVel = -45f; ctrl.fireVel = 200f; ctrl.motorForce = 2600f;

            Pivot(pivot, 0.22f);
            Trail(ball, OutputCol, 0.12f, 1.3f);          // 放物線の軌跡（リセット前に消える長さ）
            Readout(ballRb, LiveReadout.Kind.Height, "玉の高さ", 0);
            Label(ball, "出力：玉が飛ぶ", OutputCol, 0.8f, 13);
            Label(arm, "入力：腕を振り上げる", InputCol, 0.6f, 12);
        }

        // ---- 小物 ----
        static GameObject StaticBoxRot(string name, Vector3 pos, Vector3 size, Color c, Quaternion rot)
        {
            var b = Prim.Box(null, name, pos, size, c);
            b.transform.rotation = rot;
            return b;
        }
    }
}
#endif
