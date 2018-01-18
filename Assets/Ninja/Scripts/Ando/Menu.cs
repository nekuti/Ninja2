using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class Menu : MonoBehaviour {

        private SceneTransitionManager sceneTransitionManager;
        private static bool pauseSwitch = false;


        // Use this for initialization
        void Start() {
            sceneTransitionManager = GetComponent<SceneTransitionManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Kojima.InputDevice.PressDown(ButtonType.ApplicationMenu, Kojima.HandType.Left) ||
            Kojima.InputDevice.PressDown(ButtonType.ApplicationMenu, Kojima.HandType.Right) || Input.GetKeyDown(KeyCode.P))
            {
                if (!SteamVR_FadeEx.RunCheck())
                {

                    if (!pauseSwitch)
                    {
                        sceneTransitionManager.ChangeSceneAdd(SceneName.MenuScene, false);
                        pauseSwitch = true;
                        Debug.Log("メニューへ" + pauseSwitch);
                    }
                    else
                    {
                        MenuEnd();
                    }
                }
            }
        }

        public static void MenuEnd()
        {
            MenuSceneManager.RevocationScene();

            pauseSwitch = false;
            Debug.Log("メニュー解除" + pauseSwitch);
        }
    }
}