using System.Reflection;
using Harmony;
using IPA;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace PauseMenuRotationFix
{
    public class Plugin : IBeatSaberPlugin
    {
        internal static string Name => "PauseMenuRotationFix";
        
        internal static Transform menuControllers;
        internal static float rotation;

        internal static bool harmonyPatchesLoaded = false;
        internal static HarmonyInstance harmonyInstance = HarmonyInstance.Create("com.shadnix.BeatSaber.PauseMenuRotationFix");

        public void Init(IPALogger logger)
        {
            Logger.log = logger;
        }

        public void OnApplicationStart()
        {
            // Check game version, only apply fixes on game versions actually having this problem.
            // This should prevent people using outdated mods from braking their game, once this issue is fixed in the base game.
            string gameVersion = IPA.Utilities.BeatSaber.GameVersion.ToString();
            if (gameVersion == "1.6.0")
            {
                Logger.log.Info("Detected game version 1.6.0. Loading Harmony patches.");
                LoadHarmonyPatch();
            }
            else
            {
                Logger.log.Info("Detected game version " + gameVersion + ". Won't start. This mod was made to fix an issue in a particular version of the base game.");
                Logger.log.Info("If you think this issue still persists in newer game versions than 1.6.0, please update this mod.");
            }
            
        }

        public void OnApplicationQuit()
        {
            UnloadHarmonyPatches();
        }

        /// <summary>
        /// Runs at a fixed intervalue, generally used for physics calculations. 
        /// </summary>
        public void OnFixedUpdate()
        {

        }

        /// <summary>
        /// This is called every frame.
        /// </summary>
        public void OnUpdate()
        {

        }

        /// <summary>
        /// Called when the active scene is changed.
        /// </summary>
        /// <param name="prevScene">The scene you are transitioning from.</param>
        /// <param name="nextScene">The scene you are transitioning to.</param>
        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {

        }

        /// <summary>
        /// Called when the a scene's assets are loaded.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="sceneMode"></param>
        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {

        }

        public void OnSceneUnloaded(Scene scene)
        {

        }

        internal void LoadHarmonyPatch()
        {
            if (!harmonyPatchesLoaded)
            {
                harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
                Logger.log.Info("Loaded Harmony patches.");
            }
            harmonyPatchesLoaded = true;
        }

        internal void UnloadHarmonyPatches()
        {
            if (harmonyPatchesLoaded)
            {
                harmonyInstance.UnpatchAll("com.shadnix.BeatSaber.PauseMenuRotationFix");
                Logger.log.Info("Unloaded Harmony patches.");
            }
            harmonyPatchesLoaded = false;
        }
    }
}
