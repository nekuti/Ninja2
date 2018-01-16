using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class Menu : MonoBehaviour {

        private SceneTransitionManager sceneTransitionManager;
        private bool pauseSwitch = false;


        // Use this for initialization
        void Start() {
            sceneTransitionManager = GetComponent<SceneTransitionManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Kojima.InputDevice.Press(ButtonType.ApplicationMenu, Kojima.HandType.Left) ||
                Kojima.InputDevice.Press(ButtonType.ApplicationMenu, Kojima.HandType.Right) || Input.GetKeyDown(KeyCode.P))
            {
                if (!pauseSwitch)
                {
                    sceneTransitionManager.ChangeSceneAdd(SceneName.MenuScene,false);
                    pauseSwitch = true;
                }
                else
                {
                    sceneTransitionManager.RevocationScene(SceneName.MenuScene);
                    pauseSwitch = false;
                }
            }
        }
    }
}