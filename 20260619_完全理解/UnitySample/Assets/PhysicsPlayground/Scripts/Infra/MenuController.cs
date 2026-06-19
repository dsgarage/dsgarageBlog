using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsPlayground
{
    /// <summary>
    /// トップのメニュー画面。DemoCatalog の一覧をボタンで並べ、クリックで各シーンへ遷移する。
    /// IMGUI（OnGUI）で描くので Canvas / EventSystem は不要。WebGL でもそのまま動く。
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        public string titleText = "物理の掛け合わせ プレイグラウンド";
        public string subtitleText = "GDC 2024 に学ぶ「創発するゲームプレイ」体験デモ集";

        Texture2D _bg, _card;
        GUIStyle _title, _sub, _btn, _btnDesc, _foot;

        static Texture2D Solid(Color c)
        {
            var t = new Texture2D(1, 1);
            t.SetPixel(0, 0, c); t.Apply();
            return t;
        }

        void Init()
        {
            _bg = Solid(new Color(0.08f, 0.09f, 0.12f, 1f));
            _card = Solid(new Color(0.14f, 0.16f, 0.21f, 1f));

            _title = new GUIStyle(GUI.skin.label)
            { fontSize = 30, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
            _title.normal.textColor = new Color(1f, 0.62f, 0.4f);

            _sub = new GUIStyle(GUI.skin.label)
            { fontSize = 15, alignment = TextAnchor.MiddleCenter };
            _sub.normal.textColor = new Color(0.85f, 0.88f, 0.96f);

            _btn = new GUIStyle(GUI.skin.button)
            { fontSize = 17, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleLeft };
            _btn.normal.textColor = new Color(0.95f, 0.97f, 1f);
            _btn.padding = new RectOffset(18, 10, 8, 8);

            _btnDesc = new GUIStyle(GUI.skin.label)
            { fontSize = 12, alignment = TextAnchor.MiddleRight };
            _btnDesc.normal.textColor = new Color(0.55f, 0.85f, 0.95f);

            _foot = new GUIStyle(GUI.skin.label)
            { fontSize = 12, alignment = TextAnchor.MiddleCenter };
            _foot.normal.textColor = new Color(0.6f, 0.64f, 0.72f);
        }

        void OnGUI()
        {
            UIFont.Apply();
            if (_bg == null) Init();

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _bg);

            float w = Mathf.Min(560f, Screen.width - 40f);
            float x = (Screen.width - w) * 0.5f;

            GUI.Label(new Rect(x, 28f, w, 40f), titleText, _title);
            GUI.Label(new Rect(x, 70f, w, 24f), subtitleText, _sub);

            float y = 112f;
            float rowH = 46f, gap = 8f;
            var list = DemoCatalog.All;
            for (int i = 0; i < list.Length; i++)
            {
                var e = list[i];
                var rect = new Rect(x, y, w, rowH);
                GUI.DrawTexture(rect, _card);
                if (GUI.Button(rect, "  " + e.label, _btn))
                    SceneManager.LoadScene(e.scene);
                GUI.Label(new Rect(rect.x, rect.y, rect.width - 16f, rect.height), e.desc + "  ", _btnDesc);
                y += rowH + gap;

                // 7機構デモと体験デモの間に区切りの余白
                if (i == 6) y += 6f;
            }

            GUI.Label(new Rect(x, y + 4f, w, 22f),
                      "クリックで開始 ／ 各デモ中は [Esc] か右上ボタンでここへ戻る", _foot);
        }
    }
}
