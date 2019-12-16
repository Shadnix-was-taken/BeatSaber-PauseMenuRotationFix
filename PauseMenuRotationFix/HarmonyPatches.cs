using IPA.Utilities;
using Harmony;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace PauseMenuRotationFix.HarmonyPatches
{
    /** 
    * So, here is the issue: The object containing the pause menu gets rotated towards the last spawn location of an object.
    * The menu controllers are a sub-object of the pause menu and gets the additional rotation as well.
    * So if you don't pause looking straight, then it will add the rotation of the pause menu on top of the controllers.
    * 
    * The fix: Get the rotation of the pause menu and rotate the controller objects in the other direction.
    * **/
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

    /**
     * This just reverts the previous rotation to bring it back to a state which the game expects.
     * **/
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
