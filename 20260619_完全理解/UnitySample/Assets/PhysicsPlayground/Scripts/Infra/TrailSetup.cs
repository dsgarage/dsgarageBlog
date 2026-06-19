using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// 動く部品に「残像トレイル」を付けて、挙動の軌跡を目で追えるようにする。
    /// 二重振り子・カタパルトの玉・ニュートンのゆりかごなど、速くて追いきれない
    /// 動きを「線」として残し、何が起きているかを読み取れるようにするのが狙い。
    ///
    /// 実行時に TrailRenderer を構築する（マテリアルアセット不要・Built-in / URP 両対応）。
    /// </summary>
    [DisallowMultipleComponent]
    public class TrailSetup : MonoBehaviour
    {
        public Color color = new Color(0.35f, 0.85f, 0.95f);
        public float width = 0.12f;
        public float time = 1.4f;

        void Start()
        {
            var tr = GetComponent<TrailRenderer>();
            if (tr == null) tr = gameObject.AddComponent<TrailRenderer>();

            tr.time = time;
            tr.startWidth = width;
            tr.endWidth = 0f;
            tr.minVertexDistance = 0.03f;
            tr.numCapVertices = 4;
            tr.numCornerVertices = 4;
            tr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            tr.receiveShadows = false;
            tr.alignment = LineAlignment.View;

            // フォント/マテリアルアセットを使わず、両パイプラインで色が出るシェーダを探す
            var shader = Shader.Find("Sprites/Default");
            if (shader == null) shader = Shader.Find("Unlit/Color");
            if (shader == null) shader = Shader.Find("Legacy Shaders/Particles/Alpha Blended");
            if (shader != null) tr.material = new Material(shader);

            var grad = new Gradient();
            grad.SetKeys(
                new[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) },
                new[] { new GradientAlphaKey(0.9f, 0f), new GradientAlphaKey(0f, 1f) });
            tr.colorGradient = grad;
        }
    }
}
