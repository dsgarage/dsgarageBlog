using UnityEngine;

namespace PhysicsPlayground
{
    /// <summary>各デモが満たすインターフェース。Bootstrap が生成・破棄・HUD 取得に使う。</summary>
    public interface IPlaygroundDemo
    {
        string Title { get; }
        Transform CameraTarget { get; }
        string GetHudText();   // 操作説明 + ライブ状態
        void ResetDemo();      // R キーでのリセット
    }
}
