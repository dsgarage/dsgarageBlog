using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// WebGL ビルドには IMGUI 既定フォント（Arial）が含まれず、さらに日本語グリフも
    /// 無いため OnGUI のテキストが一切描画されない。これを防ぐため、Resources に置いた
    /// 日本語フォント（Noto Sans JP）を読み込み、各 OnGUI の先頭で GUI スキンへ適用する。
    /// </summary>
    public static class UIFont
    {
        static Font _font;
        static bool _tried;

        public static Font Get()
        {
            if (_font == null && !_tried)
            {
                _tried = true;
                _font = Resources.Load<Font>("PlaygroundFont");
            }
            return _font;
        }

        /// <summary>OnGUI の先頭で呼ぶ。スキンのフォントを日本語フォントへ差し替える。</summary>
        public static void Apply()
        {
            var f = Get();
            if (f != null) GUI.skin.font = f;
        }
    }
}
