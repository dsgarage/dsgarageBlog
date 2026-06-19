using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 3D オブジェクトに付ける「浮き文字ラベル」のデータ。
    /// 実際の描画は GarageHUD が WorldToScreenPoint で画面に重ねて行う
    /// （フォントアセット不要で確実に表示するため）。
    /// </summary>
    public class WorldLabel : MonoBehaviour
    {
        [TextArea] public string text = "";
        public Color color = Color.white;
        public Vector3 worldOffset = new Vector3(0f, 1.2f, 0f);
        public int fontSize = 14;
    }
}
