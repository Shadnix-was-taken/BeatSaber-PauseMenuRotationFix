using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PauseMenuRotationFix
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class PauseMenuRotationFix : MonoBehaviour
    {
        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            Logger.log?.Debug($"{name}: Awake()");

        }
        /// <summary>
        /// Only ever called once on the first frame the script is Enabled. Start is called after any other script's Awake() and before Update().
        /// </summary>
        private void Start()
        {
            Logger.log?.Debug($"{name}: Start()");

            GameObject pauseMenu = GameObject.Find("PauseMenu");
            if (pauseMenu == null)
            {
                Logger.log?.Debug($"{name}: Unable to find PauseMenu. Game structure may have changed.");
            } else
            {
                PauseMenuManager pauseMenuManager = pauseMenu.GetComponent<PauseMenuManager>();
                if (pauseMenuManager == null)
                {
                    Logger.log?.Debug($"{name}: Unable to find PauseMenuManager. Game structure may have changed.");
                }
            }

            GameObject menuControllers = GameObject.Find("MenuControllers");
            if (menuControllers == null)
            {
                Logger.log?.Debug($"{name}: Unable to find MenuControllers. Game structure may have changed.");
                return;
            }
            Plugin.menuControllers = menuControllers.transform;
        }

        public void OnDidPause()
        {
            Logger.log.Debug("Pause event called.");
        }

        public void OnDidResume()
        {
            Logger.log.Debug("Resume event called.");
        }


        /// <summary>
        /// Called every frame if the script is enabled.
        /// </summary>
        private void Update()
        {

        }

        /// <summary>
        /// Called every frame after every other enabled script's Update().
        /// </summary>
        private void LateUpdate()
        {

        }

        /// <summary>
        /// Called when the script becomes enabled and active
        /// </summary>
        private void OnEnable()
        {

        }

        /// <summary>
        /// Called when the script becomes disabled or when it is being destroyed.
        /// </summary>
        private void OnDisable()
        {

        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {

        }
        #endregion
    }
}
