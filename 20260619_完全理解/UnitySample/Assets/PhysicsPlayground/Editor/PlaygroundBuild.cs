#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsPlayground.EditorTools
{
    /// <summary>
    /// メニューシーンの生成・ビルド設定への登録・WebGL ビルドをまとめて行う。
    ///
    ///   Tools/GDC Physics Playground/▶ Setup Menu + Scenes   … 全シーン生成＋ビルド設定登録
    ///   Tools/GDC Physics Playground/▶ Build WebGL           … WebGL を Build/WebGL に出力
    ///
    /// シーン構成: 00_Menu（トップ）→ 01〜07（機構デモ）＋ 08_Garage / 09_Collapse（体験デモ）
    /// </summary>
    public static class PlaygroundBuild
    {
        const string SceneDir = "Assets/Scenes";
        const string M = "Tools/GDC Physics Playground/";

        // ===== ① 全シーン生成 + ビルド設定登録 =====
        [MenuItem(M + "▶ Setup Menu + Scenes", priority = 100)]
        public static void SetupAll()
        {
            // 1) 7 機構デモ（01〜07）を生成（各シーンに ReturnToMenu 付き）
            PlaygroundMenu.All();

            // 2) 体験デモ（Bootstrap）シーン 08 / 09
            BuildBootstrapScene("08_Garage", 0);
            BuildBootstrapScene("09_Collapse", 1);

            // 3) トップメニュー 00_Menu（最後に生成して開いたままにする）
            BuildMenuScene();

            // 4) ビルド設定に登録（00_Menu を index 0 に）
            RegisterBuildScenes();

            Debug.Log("[Playground] メニュー＋全シーンを生成し、ビルド設定に登録しました（00_Menu を含む全 "
                      + (DemoCatalog.All.Length + 1) + " シーン）");
        }

        static void EnsureSceneDir()
        {
            if (!AssetDatabase.IsValidFolder(SceneDir)) AssetDatabase.CreateFolder("Assets", "Scenes");
        }

        static void BuildMenuScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var camGo = new GameObject("MenuCamera"); camGo.tag = "MainCamera";
            var cam = camGo.AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.08f, 0.09f, 0.12f);
            camGo.AddComponent<AudioListener>();

            new GameObject("Menu").AddComponent<MenuController>();

            EnsureSceneDir();
            EditorSceneManager.SaveScene(scene, SceneDir + "/" + DemoCatalog.MenuScene + ".unity");
        }

        static void BuildBootstrapScene(string file, int startDemo)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var go = new GameObject("PlaygroundBootstrap");
            var boot = go.AddComponent<PhysicsPlaygroundBootstrap>();
            boot.startDemo = startDemo;

            new GameObject("ReturnToMenu").AddComponent<ReturnToMenu>();

            EnsureSceneDir();
            EditorSceneManager.SaveScene(scene, SceneDir + "/" + file + ".unity");
        }

        static void RegisterBuildScenes()
        {
            var list = new List<EditorBuildSettingsScene>
            {
                new EditorBuildSettingsScene($"{SceneDir}/{DemoCatalog.MenuScene}.unity", true)
            };
            foreach (var e in DemoCatalog.All)
                list.Add(new EditorBuildSettingsScene($"{SceneDir}/{e.scene}.unity", true));
            EditorBuildSettings.scenes = list.ToArray();
        }

        // ===== ② WebGL ビルド =====
        [MenuItem(M + "▶ Build WebGL", priority = 101)]
        public static void BuildWebGL()
        {
            if (!BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.WebGL, BuildTarget.WebGL))
            {
                Debug.LogError("[Playground] WebGL モジュールが未インストールです。Unity Hub から "
                               + "「WebGL Build Support」を追加してください。");
                return;
            }

            // シーンが未登録ならセットアップ
            if (EditorBuildSettings.scenes == null || EditorBuildSettings.scenes.Length == 0)
                SetupAll();

            // WebGL 向け Player 設定（ローカルでそのまま開きやすい構成）
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.WebGL, ScriptingImplementation.IL2CPP);
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
            PlayerSettings.WebGL.template = "APPLICATION:Default";
            PlayerSettings.WebGL.dataCaching = false;
            PlayerSettings.runInBackground = true;
            PlayerSettings.productName = "PhysicsPlayground";

            // アクティブターゲットを WebGL に
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);

            string root = Directory.GetParent(Application.dataPath).FullName;
            string outDir = Path.Combine(root, "Build", "WebGL");
            Directory.CreateDirectory(outDir);

            var scenes = new List<string>();
            foreach (var s in EditorBuildSettings.scenes)
                if (s.enabled) scenes.Add(s.path);

            Debug.Log($"[Playground] WebGL ビルド開始 → {outDir}（{scenes.Count} シーン）");

            var opts = new BuildPlayerOptions
            {
                scenes = scenes.ToArray(),
                locationPathName = outDir,
                target = BuildTarget.WebGL,
                targetGroup = BuildTargetGroup.WebGL,
                options = BuildOptions.None
            };
            var report = BuildPipeline.BuildPlayer(opts);
            var s2 = report.summary;
            if (s2.result == BuildResult.Succeeded)
                Debug.Log($"[Playground] WebGL ビルド成功: {s2.totalSize / (1024 * 1024)} MB / "
                          + $"{s2.totalTime.TotalSeconds:0} 秒 → {outDir}/index.html");
            else
                Debug.LogError($"[Playground] WebGL ビルド失敗: {s2.result} / errors={s2.totalErrors}");
        }
    }
}
#endif
