using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// デモの趣旨を伝える説明オーバーレイ + 3D 部品ラベル。
    /// 「車にファンを足すほど推力が掛け算で増える」を、目的・手順・推力メーターで可視化する。
    /// </summary>
    public class GarageHUD : MonoBehaviour
    {
        public ArcadeDriver vehicle;
        public GarageInteractor interactor;
        public float perFanThrust = 850f;
        public int maxFansForBar = 4;

        GUIStyle _box, _title, _lead, _body, _meterLabel;
        Texture2D _barBg, _barFill, _panelBg;
        WorldLabel[] _labels;
        Camera _cam;

        void Start()
        {
            _cam = Camera.main;
            _labels = Object.FindObjectsByType<WorldLabel>(FindObjectsSortMode.None);
        }

        static Texture2D Solid(Color c)
        {
            var t = new Texture2D(1, 1);
            t.SetPixel(0, 0, c);
            t.Apply();
            return t;
        }

        void Init()
        {
            _panelBg = Solid(new Color(0.06f, 0.07f, 0.10f, 0.86f));
            _barBg = Solid(new Color(1f, 1f, 1f, 0.15f));
            _barFill = Solid(new Color(1f, 0.55f, 0.15f, 0.95f));

            _box = new GUIStyle(GUI.skin.box);
            _box.normal.background = _panelBg;

            _title = new GUIStyle(GUI.skin.label)
            { fontSize = 17, fontStyle = FontStyle.Bold, wordWrap = true };
            _title.normal.textColor = new Color(1f, 0.45f, 0.45f);

            _lead = new GUIStyle(GUI.skin.label) { fontSize = 13, wordWrap = true };
            _lead.normal.textColor = new Color(0.85f, 0.9f, 1f);

            _body = new GUIStyle(GUI.skin.label) { fontSize = 13, wordWrap = true };
            _body.normal.textColor = Color.white;

            _meterLabel = new GUIStyle(GUI.skin.label)
            { fontSize = 14, fontStyle = FontStyle.Bold };
            _meterLabel.normal.textColor = new Color(1f, 0.8f, 0.4f);
        }

        void OnGUI()
        {
            UIFont.Apply();
            if (_box == null) Init();
            if (_cam == null) _cam = Camera.main;

            DrawPanel();
            DrawWorldLabels();
        }

        void DrawPanel()
        {
            int welded = interactor != null ? interactor.WeldedFans : 0;
            int loose = interactor != null ? interactor.LooseCount : 0;
            float speed = vehicle != null ? vehicle.CurrentSpeedKmh : 0f;
            float total = welded * perFanThrust;

            GUILayout.BeginArea(new Rect(12, 12, 380, 360), _box);
            GUILayout.Space(8);
            GUILayout.Label("掛け算のゲームデザイン体験", _title);
            GUILayout.Label("部品は“足し算”ではなく“掛け算”。\n" +
                            "車（車輪付きの土台）にファンを足すほど、推力が積み上がります。",
                            _lead);
            GUILayout.Space(6);
            GUILayout.Label("やってみよう", _title);
            GUILayout.Label("① W/S・A/D で走る\n" +
                            "② Space でファンを噴射（前へ／浮く）\n" +
                            "③ 地面のファンを左クリックで車体に溶接して増やす\n" +
                            "→ ファンが増えるほど推力が掛け算で増え、ジャンプ台から遠くへ飛べます",
                            _body);
            GUILayout.Space(8);

            // 推力メーター（掛け算の可視化）
            GUILayout.Label($"推力 ＝ ファン {welded} 基 × {perFanThrust:F0} ＝ {total:F0} N", _meterLabel);
            Rect bar = GUILayoutUtility.GetRect(356, 16);
            GUI.DrawTexture(bar, _barBg);
            float ratio = Mathf.Clamp01((float)welded / Mathf.Max(1, maxFansForBar));
            GUI.DrawTexture(new Rect(bar.x, bar.y, bar.width * ratio, bar.height), _barFill);

            GUILayout.Space(6);
            GUILayout.Label($"速度 {speed:F1} km/h　／　残りファン {loose} 基\n" +
                            "右ドラッグ＝視点回転　ホイール＝ズーム", _body);
            GUILayout.EndArea();
        }

        void DrawWorldLabels()
        {
            if (_labels == null || _cam == null) return;
            foreach (var wl in _labels)
            {
                if (wl == null || string.IsNullOrEmpty(wl.text)) continue;
                Vector3 sp = _cam.WorldToScreenPoint(wl.transform.position + wl.worldOffset);
                if (sp.z <= 0f) continue; // カメラ後方は描かない

                var style = new GUIStyle(GUI.skin.label)
                { fontSize = wl.fontSize, alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
                Vector2 size = style.CalcSize(new GUIContent(wl.text));
                float x = sp.x - size.x * 0.5f;
                float y = Screen.height - sp.y - size.y * 0.5f;
                var rect = new Rect(x, y, size.x, size.y);

                // 黒縁取り＋本体色で 3D 背景でも読めるように
                style.normal.textColor = new Color(0f, 0f, 0f, 0.85f);
                foreach (var d in _outline)
                    GUI.Label(new Rect(rect.x + d.x, rect.y + d.y, rect.width, rect.height), wl.text, style);
                style.normal.textColor = wl.color;
                GUI.Label(rect, wl.text, style);
            }
        }

        static readonly Vector2[] _outline =
        {
            new Vector2(-1,0), new Vector2(1,0), new Vector2(0,-1), new Vector2(0,1)
        };
    }
}
