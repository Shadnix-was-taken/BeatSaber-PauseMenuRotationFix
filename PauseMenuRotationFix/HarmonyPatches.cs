using IPA.Utilities;
using Harmony;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace PauseMenuRotationFix.HarmonyPatches
{
    [HarmonyPatch(typeof(PauseMenuManager))]
    [HarmonyPatch("ShowMenu")]
    class PauseMenuManagerShowMenuPatch
    {
        static void Postfix(PauseMenuManager __instance)
        {
            if (SceneManager.GetActiveScene().name != "GameCore")
            {
                return;
            }

            GameObject root = SceneManager.GetSceneByName("StandardGameplay").GetRootGameObjects().First();
            if (root == null)
            {
                return;
            }

            Transform menuControllers = root.transform.Find("PauseMenu")?.Find("Wrapper")?.Find("MenuControllers");
            if (menuControllers != null)
            {
                Plugin.menuControllers = menuControllers;
                EnvironmentSpawnRotation envSpawnRotation = ReflectionUtil.GetPrivateField<EnvironmentSpawnRotation>(__instance, "_environmentSpawnRotation");
                Plugin.rotation = envSpawnRotation.targetRotation;
                Plugin.menuControllers.transform.Rotate(new Vector3(0f, (Plugin.rotation * -1), 0f));
            }
        }
    }

    [HarmonyPatch(typeof(PauseMenuManager))]
    [HarmonyPatch("StartResumeAnimation")]
    class PauseMenuManagerStartResumeAnimationPatch
    {
        static void Prefix()
        {
            if (Plugin.menuControllers != null)
            {
                Plugin.menuControllers.transform.Rotate(new Vector3(0f, Plugin.rotation, 0f));
            }
        }
    }
}
