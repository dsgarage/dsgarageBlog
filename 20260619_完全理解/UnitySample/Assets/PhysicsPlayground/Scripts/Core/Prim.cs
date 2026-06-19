using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// デモシーンをコードだけで組み立てるためのプリミティブ生成ヘルパ。
    /// プレハブ不要・手組み不要で「Play を押すだけ」を実現するための土台。
    /// </summary>
    public static class Prim
    {
        public static GameObject Box(Transform parent, string name, Vector3 pos,
                                     Vector3 size, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name;
            go.transform.SetParent(parent, false);
            go.transform.position = pos;
            go.transform.localScale = size;
            Paint(go, color);
            return go;
        }

        public static GameObject Cylinder(Transform parent, string name, Vector3 pos,
                                          Vector3 size, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.name = name;
            go.transform.SetParent(parent, false);
            go.transform.position = pos;
            go.transform.localScale = size;
            Paint(go, color);
            return go;
        }

        public static GameObject Sphere(Transform parent, string name, Vector3 pos,
                                        float diameter, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = name;
            go.transform.SetParent(parent, false);
            go.transform.position = pos;
            go.transform.localScale = Vector3.one * diameter;
            Paint(go, color);
            return go;
        }

        public static GameObject Capsule(Transform parent, string name, Vector3 pos,
                                         Vector3 size, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.name = name;
            go.transform.SetParent(parent, false);
            go.transform.position = pos;
            go.transform.localScale = size;
            Paint(go, color);
            return go;
        }

        public static Rigidbody AddBody(GameObject go, float mass)
        {
            var rb = go.GetComponent<Rigidbody>();
            if (rb == null) rb = go.AddComponent<Rigidbody>();
            rb.mass = mass;
            return rb;
        }

        /// <summary>Standard / URP どちらでも色が付くようにマテリアル色を設定する。</summary>
        public static void Paint(GameObject go, Color color)
        {
            var r = go.GetComponent<Renderer>();
            if (r == null) return;
            var mat = r.material; // インスタンス化されたマテリアル
            mat.color = color;
            if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", color); // URP
        }
    }
}
