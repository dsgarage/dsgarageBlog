using System.Collections.Generic;
using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 「物理の掛け合わせ」デモのタイトルバナー・注目ポイント・凡例・実況・3D ラベルを描く。
    /// ラベルは WorldToScreenPoint で画面に重ねる（フォントアセット不要）。
    ///
    /// 何が「入力（動力）」で、何が「出力（創発した結果）」なのかを、色の凡例と
    /// 注目ポイントの一文で示し、画面だけで仕組みが読み取れるようにする。
    /// T キーでスロー再生（0.25x）に切り替えられる。
    /// </summary>
    public class CrossbreedHUD : MonoBehaviour
    {
        public string titleText = "物理の掛け合わせ";
        [TextArea] public string subtitleText =
            "同じ部品でも、掛け合わせ方で“できること”が変わる";
        [TextArea] public string watchText = "";   // 👀 注目ポイント（1〜2行）
        public bool showLegend = true;              // 入力=オレンジ / 出力=シアン の凡例

        // 凡例の色（PlaygroundMenu の InputCol / OutputCol と合わせる）
        static readonly Color InputCol = new Color(1f, 0.62f, 0.25f);
        static readonly Color OutputCol = new Color(0.35f, 0.85f, 0.95f);

        WorldLabel[] _labels;
        LiveReadout[] _readouts;
        Camera _cam;
        GUIStyle _title, _sub, _watch, _panel, _row, _hint;
        Texture2D _bg, _bgPanel, _swatch;
        bool _slow;

        static readonly Vector2[] Outline =
        { new Vector2(-1,0), new Vector2(1,0), new Vector2(0,-1), new Vector2(0,1) };

        void Start()
        {
            _cam = Camera.main;
            _labels = Object.FindObjectsByType<WorldLabel>(FindObjectsSortMode.None);
            _readouts = Object.FindObjectsByType<LiveReadout>(FindObjectsSortMode.None);
            System.Array.Sort(_readouts, (a, b) => a.order.CompareTo(b.order));
        }

        void Update()
        {
            // T キーでスロー再生（速い/カオスな動きを追いやすくする）
            if (Input.GetKeyDown(KeyCode.T))
            {
                _slow = !_slow;
                Time.timeScale = _slow ? 0.25f : 1f;
            }
        }

        void OnDisable()
        {
            // 後始末（他シーンに timeScale を持ち込まない）
            Time.timeScale = 1f;
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
            _bg = Solid(new Color(0.06f, 0.07f, 0.10f, 0.88f));
            _bgPanel = Solid(new Color(0.06f, 0.07f, 0.10f, 0.78f));
            _swatch = Solid(Color.white);

            _title = new GUIStyle(GUI.skin.label)
            { fontSize = 22, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
            _title.normal.textColor = new Color(1f, 0.55f, 0.4f);

            _sub = new GUIStyle(GUI.skin.label)
            { fontSize = 14, alignment = TextAnchor.MiddleCenter, wordWrap = true };
            _sub.normal.textColor = new Color(0.9f, 0.92f, 1f);

            _watch = new GUIStyle(GUI.skin.label)
            { fontSize = 14, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter, wordWrap = true };
            _watch.normal.textColor = new Color(1f, 0.85f, 0.45f);

            _panel = new GUIStyle();
            _row = new GUIStyle(GUI.skin.label) { fontSize = 13 };
            _row.normal.textColor = new Color(0.92f, 0.94f, 1f);
            _hint = new GUIStyle(GUI.skin.label) { fontSize = 12 };
            _hint.normal.textColor = new Color(0.7f, 0.74f, 0.82f);
        }

        void OnGUI()
        {
            UIFont.Apply();
            if (_bg == null) Init();
            if (_cam == null) _cam = Camera.main;

            // ===== 上部バナー（タイトル・サブ・注目ポイント）=====
            bool hasWatch = !string.IsNullOrEmpty(watchText);
            float w = 780f, h = hasWatch ? 104f : 72f, x = (Screen.width - w) * 0.5f;
            GUI.DrawTexture(new Rect(x, 8f, w, h), _bg);
            GUI.Label(new Rect(x, 14f, w, 30f), titleText, _title);
            GUI.Label(new Rect(x, 44f, w, 26f), subtitleText, _sub);
            if (hasWatch)
                GUI.Label(new Rect(x + 16f, 70f, w - 32f, 30f), "👀 " + watchText, _watch);

            // ===== 左上パネル（凡例 + 実況 + 操作ヒント）=====
            DrawLeftPanel();

            // ===== 3D ラベル =====
            DrawWorldLabels();
        }

        void DrawLeftPanel()
        {
            var rows = new List<(string text, Color col)>();
            if (showLegend)
            {
                rows.Add(("■ 入力（動力）", InputCol));
                rows.Add(("■ 出力（結果）", OutputCol));
            }
            if (_readouts != null)
            {
                foreach (var r in _readouts)
                {
                    if (r == null) continue;
                    var line = r.Line();
                    if (!string.IsNullOrEmpty(line)) rows.Add((line, new Color(0.92f, 0.94f, 1f)));
                }
            }

            float pad = 10f, lineH = 20f;
            float pw = 230f;
            float ph = pad * 2f + rows.Count * lineH + 24f; // +24: 操作ヒント
            float px = 12f, py = 120f;

            GUI.DrawTexture(new Rect(px, py, pw, ph), _bgPanel);
            float cy = py + pad;
            foreach (var (text, col) in rows)
            {
                if (text.StartsWith("■"))
                {
                    // 色見本＋ラベル
                    var prev = GUI.color; GUI.color = col;
                    GUI.DrawTexture(new Rect(px + pad, cy + 3f, 12f, 12f), _swatch);
                    GUI.color = prev;
                    _row.normal.textColor = new Color(0.92f, 0.94f, 1f);
                    GUI.Label(new Rect(px + pad + 18f, cy, pw - pad * 2f - 18f, lineH), text.Substring(1).TrimStart(), _row);
                }
                else
                {
                    _row.normal.textColor = col;
                    GUI.Label(new Rect(px + pad, cy, pw - pad * 2f, lineH), text, _row);
                }
                cy += lineH;
            }
            GUI.Label(new Rect(px + pad, cy + 2f, pw - pad * 2f, lineH),
                      _slow ? "[T] スロー: ON (0.25x)" : "[T] スロー: OFF", _hint);
        }

        void DrawWorldLabels()
        {
            if (_labels == null || _cam == null) return;
            foreach (var wl in _labels)
            {
                if (wl == null || string.IsNullOrEmpty(wl.text)) continue;
                Vector3 sp = _cam.WorldToScreenPoint(wl.transform.position + wl.worldOffset);
                if (sp.z <= 0f) continue;

                var style = new GUIStyle(GUI.skin.label)
                { fontSize = wl.fontSize, alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
                Vector2 size = style.CalcSize(new GUIContent(wl.text));
                size.x += 8f; size.y += 4f;
                var rect = new Rect(sp.x - size.x * 0.5f, Screen.height - sp.y - size.y * 0.5f, size.x, size.y);

                style.normal.textColor = new Color(0f, 0f, 0f, 0.85f);
                foreach (var d in Outline)
                    GUI.Label(new Rect(rect.x + d.x, rect.y + d.y, rect.width, rect.height), wl.text, style);
                style.normal.textColor = wl.color;
                GUI.Label(rect, wl.text, style);
            }
        }
    }
}
