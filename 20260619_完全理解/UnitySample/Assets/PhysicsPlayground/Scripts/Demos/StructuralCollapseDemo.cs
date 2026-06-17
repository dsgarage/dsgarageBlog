using System.Collections.Generic;
using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>
    /// デモ2: 大規模破壊と構造的完全性（Red Faction / The Finals）。
    ///
    /// ブロックを格子状に積み、隣接ブロックを FixedJoint（breakForce 付き）で接続する。
    /// 鉄球を撃ち込むと、接続にかかる力が限界を超えた箇所から破断し、
    /// 支えを失った上部が連鎖的に崩落する。
    /// 「壊れ方を固定演出でなく接続の強度から計算する」体験。
    ///
    /// 操作: 左クリック = その方向へ鉄球を発射
    /// </summary>
    public class StructuralCollapseDemo : MonoBehaviour, IPlaygroundDemo
    {
        const int Width = 6;
        const int Height = 8;
        const float Size = 1f;
        const float BreakForce = 350f;

        Transform _wallCenter;
        Camera _cam;
        int _totalJoints;
        int _brokenJoints;
        int _ballsFired;
        readonly List<GameObject> _balls = new List<GameObject>();

        public string Title => "構造崩壊";
        public Transform CameraTarget => _wallCenter != null ? _wallCenter : transform;

        void Start()
        {
            _cam = Camera.main;
            Build();
        }

        public void ResetDemo()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
            _balls.Clear();
            _totalJoints = _brokenJoints = _ballsFired = 0;
            Build();
        }

        void Build()
        {
            var centerGo = new GameObject("WallCenter");
            centerGo.transform.SetParent(transform, false);
            centerGo.transform.position = new Vector3(0f, Height * Size * 0.5f, 0f);
            _wallCenter = centerGo.transform;

            var grid = new Rigidbody[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Vector3 pos = new Vector3((x - (Width - 1) * 0.5f) * Size,
                                              0.5f * Size + y * Size, 0f);
                    float t = (float)y / Height;
                    var color = Color.Lerp(new Color(0.7f, 0.45f, 0.35f),
                                           new Color(0.55f, 0.6f, 0.7f), t);
                    var go = Prim.Box(transform, $"Block_{x}_{y}", pos,
                                      Vector3.one * (Size * 0.98f), color);
                    var rb = Prim.AddBody(go, 3f);

                    bool foundation = (y == 0);
                    rb.isKinematic = foundation; // 最下段は基礎として固定
                    grid[x, y] = rb;

                    if (foundation) continue;

                    var block = go.AddComponent<StructuralBlock>();
                    block.onJointBroken = () => _brokenJoints++;

                    // 下のブロックと接続
                    Connect(rb, grid[x, y - 1]);
                    // 左のブロックと接続
                    if (x > 0) Connect(rb, grid[x - 1, y]);
                }
            }
        }

        void Connect(Rigidbody body, Rigidbody other)
        {
            if (other == null) return;
            var joint = body.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = other;
            joint.breakForce = BreakForce;
            joint.breakTorque = BreakForce;
            joint.enablePreprocessing = false;
            _totalJoints++;
        }

        void Update()
        {
            if (_cam == null) _cam = Camera.main;
            if (Input.GetMouseButtonDown(0) && _cam != null)
                FireBall();

            // 古い鉄球を片付ける
            for (int i = _balls.Count - 1; i >= 0; i--)
                if (_balls[i] == null) _balls.RemoveAt(i);
        }

        void FireBall()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            var ballGo = Prim.Sphere(transform, "WreckingBall",
                                     ray.origin + ray.direction * 2f, 1.2f,
                                     new Color(0.15f, 0.15f, 0.18f));
            var rb = Prim.AddBody(ballGo, 45f);
            rb.linearVelocity = ray.direction * 22f;
            _balls.Add(ballGo);
            _ballsFired++;
            Destroy(ballGo, 8f);
        }

        public string GetHudText()
        {
            int intact = Mathf.Max(0, _totalJoints - _brokenJoints);
            float ratio = _totalJoints > 0 ? 100f * intact / _totalJoints : 0f;
            return
                "左クリック = 鉄球を発射\n" +
                "右ドラッグ = 視点を回す\n\n" +
                $"健全な接続: {intact} / {_totalJoints}  ({ratio:F0}%)\n" +
                $"発射した鉄球: {_ballsFired}\n\n" +
                "接続が切れて支えを失うと、\n" +
                "上部が連鎖的に崩落します。\n" +
                "[R] で組み直し。";
        }
    }
}
