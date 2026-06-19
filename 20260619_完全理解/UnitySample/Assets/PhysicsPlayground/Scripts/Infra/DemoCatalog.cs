namespace PhysicsPlayground
{
    /// <summary>
    /// メニュー・ビルド設定の両方が参照するデモ一覧。
    /// scene はビルド設定に登録するシーン名（拡張子なし）、label は画面表示名。
    /// ここを増やせば、メニュー・ビルド対象に自動で反映される。
    /// </summary>
    public static class DemoCatalog
    {
        public const string MenuScene = "00_Menu";

        public struct Entry
        {
            public string scene;
            public string label;
            public string desc;
            public Entry(string scene, string label, string desc)
            { this.scene = scene; this.label = label; this.desc = desc; }
        }

        public static readonly Entry[] All =
        {
            new Entry("01_SliderCrank",    "① スライダークランク", "回転 → 往復"),
            new Entry("02_Whegs",          "② Whegs（脚車輪）",     "回転 → 歩行"),
            new Entry("03_DoublePendulum", "③ 二重振り子",          "結合が生むカオス"),
            new Entry("04_NewtonsCradle",  "④ ニュートンのゆりかご", "運動量の伝達"),
            new Entry("05_PendulumWave",   "⑤ 振り子ウェーブ",      "長さの差が生む波"),
            new Entry("06_RimlessWheel",   "⑥ 受動歩行",            "重力だけで歩く"),
            new Entry("07_Catapult",       "⑦ カタパルト",          "てこ × 拘束開放"),
            new Entry("08_Garage",         "掛け算ガレージ",        "部品の掛け算で創発（TOTK）"),
            new Entry("09_Collapse",       "構造崩壊",              "接続の破断で連鎖崩落"),
        };
    }
}
