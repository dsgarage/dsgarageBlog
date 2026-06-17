using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 掛け算ガレージの操作説明とライブ状態を表示する自己完結 HUD（OnGUI）。
    /// Bootstrap には依存せず、ビルダーが vehicle / interactor を結線する。
    /// </summary>
    public class GarageHUD : MonoBehaviour
    {
        public VehicleController vehicle;
        public GarageInteractor interactor;

        GUIStyle _box, _title, _body;

        void InitStyles()
        {
            _box = new GUIStyle(GUI.skin.box);
            var bg = new Texture2D(1, 1);
            bg.SetPixel(0, 0, new Color(0.06f, 0.07f, 0.10f, 0.82f));
            bg.Apply();
            _box.normal.background = bg;

            _title = new GUIStyle(GUI.skin.label)
            { fontSize = 16, fontStyle = FontStyle.Bold, wordWrap = true };
            _title.normal.textColor = new Color(1f, 0.42f, 0.42f);

            _body = new GUIStyle(GUI.skin.label) { fontSize = 13, wordWrap = true };
            _body.normal.textColor = Color.white;
        }

        void OnGUI()
        {
            if (_box == null) InitStyles();

            GUILayout.BeginArea(new Rect(12, 12, 360, Screen.height - 24), _box);
            GUILayout.Space(6);
            GUILayout.Label("掛け算ガレージ（Multiplicative Gameplay）", _title);
            GUILayout.Space(4);
            GUILayout.Label(
                "W/S・↑↓ = 前後\n" +
                "A/D・←→ = 操舵\n" +
                "Space = ファン噴射\n" +
                "左クリック = 地面のファンを溶接\n" +
                "右ドラッグ = 旋回   ホイール = ズーム", _body);
            GUILayout.Space(8);

            float speed = vehicle != null ? vehicle.CurrentSpeedKmh : 0f;
            int welded = interactor != null ? interactor.WeldedFans : 0;
            int loose = interactor != null ? interactor.LooseCount : 0;
            GUILayout.Label(
                $"速度: {speed:F1} km/h\n" +
                $"搭載ファン: {welded} 基   残り: {loose} 基\n\n" +
                "ファンを増やすほど推力が積み上がり、\nやがて車体が浮きます（掛け算の創発）。", _body);

            GUILayout.EndArea();
        }
    }
}
