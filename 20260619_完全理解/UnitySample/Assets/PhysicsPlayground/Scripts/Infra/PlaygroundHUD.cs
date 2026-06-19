using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// OnGUI による画面オーバーレイ。UI プレハブの手組みが不要なので、
    /// 「Play を押すだけ」で操作説明とライブ状態が出る。
    /// </summary>
    public class PlaygroundHUD : MonoBehaviour
    {
        public PhysicsPlaygroundBootstrap bootstrap;

        GUIStyle _box, _title, _body;

        void InitStyles()
        {
            _box = new GUIStyle(GUI.skin.box);
            var bg = new Texture2D(1, 1);
            bg.SetPixel(0, 0, new Color(0.06f, 0.07f, 0.10f, 0.82f));
            bg.Apply();
            _box.normal.background = bg;

            _title = new GUIStyle(GUI.skin.label)
            {
                fontSize = 16, fontStyle = FontStyle.Bold, wordWrap = true
            };
            _title.normal.textColor = new Color(1f, 0.42f, 0.42f);

            _body = new GUIStyle(GUI.skin.label) { fontSize = 13, wordWrap = true };
            _body.normal.textColor = Color.white;
        }

        void OnGUI()
        {
            UIFont.Apply();
            if (_box == null) InitStyles();
            if (bootstrap == null) return;

            const float w = 360f;
            GUILayout.BeginArea(new Rect(12, 12, w, Screen.height - 24), _box);
            GUILayout.Space(6);

            GUILayout.Label("GDC 2024 物理演算プレイグラウンド", _title);
            GUILayout.Space(2);
            GUILayout.Label("[1] 掛け算ガレージ   [2] 構造崩壊\n[R] リセット   右ドラッグ=旋回  ホイール=ズーム",
                            _body);
            GUILayout.Space(8);

            var demo = bootstrap.CurrentDemo;
            if (demo != null)
            {
                GUILayout.Label("▼ " + demo.Title, _title);
                GUILayout.Space(2);
                GUILayout.Label(demo.GetHudText(), _body);
            }

            GUILayout.EndArea();
        }
    }
}
