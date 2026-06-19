using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsPlayground
{
    /// <summary>
    /// 各デモシーンに置く「メニューへ戻る」操作。右上のボタン、または [Esc] / [M] キーで
    /// メニューシーンへ戻る。IMGUI なので Canvas 不要・WebGL でも動く。
    /// </summary>
    public class ReturnToMenu : MonoBehaviour
    {
        GUIStyle _btn;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
                Back();
        }

        void Back()
        {
            // メニューがビルドに含まれていない場合（単体シーン再生）でも落ちないように保護
            if (Application.CanStreamedLevelBeLoaded(DemoCatalog.MenuScene))
                SceneManager.LoadScene(DemoCatalog.MenuScene);
        }

        void OnGUI()
        {
            UIFont.Apply();
            if (_btn == null)
            {
                _btn = new GUIStyle(GUI.skin.button) { fontSize = 14, fontStyle = FontStyle.Bold };
                _btn.normal.textColor = new Color(0.95f, 0.97f, 1f);
            }
            float w = 130f, h = 30f;
            var r = new Rect(Screen.width - w - 12f, 12f, w, h);
            if (GUI.Button(r, "≡ メニュー", _btn)) Back();
        }
    }
}
