using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class PlayerBaseManager : MonoBehaviour
    {
        //  プレイシーンマネージャ
        public static PlaySceneManager playSceneManager;

        //  ステージ移動用オブジェクト
        [SerializeField]
        private List<GameObject> stageSwitches = new List<GameObject>();

        //  ドアの方向を向いているか確認用
        private Ray ray;

        // Use this for initialization
        void Start()
        {
            //ray = new Ray(PlaySceneManager.GetPlayer().position, PlaySceneManager.GetPlayer().transform.rotation);
            foreach (GameObject stageSwitch in stageSwitches)
            {
                stageSwitch.SetActive(false);
            }

            DoorAnime.SetDoorAnimeState(DoorAnimeState.End);

        }

        // Update is called once per frame
        void Update()
        {
            int i = 0;
           foreach(bool clearFloorLavel in playSceneManager.clearFloorLevel)
            {
                var stageSwitch = stageSwitches[i]/*.GetComponent<StageSwitch>()*/;

                if (clearFloorLavel)
                {
                    stageSwitch.SetActive(true);
                }
                else
                {
                    stageSwitch.SetActive(false);
                    //stageSwitch.ParticleActive(false);
                }
                i++;
            }

           foreach(GameObject stageSwitch in stageSwitches)
            {
                var stageSwitchScript = stageSwitch.GetComponent<StageSwitch>();

                if (stageSwitchScript.ClickFlag)
                {
                    playSceneManager.StageChange((int)stageSwitchScript.myFloorLevel);
                }
            }

            if(Kojima.InputDevice.Press(ButtonType.Touchpad, Kojima.HandType.Left))
            {
                DoorAnime.SetDoorAnimeState(DoorAnimeState.Start);
            }
        }

        /// <summary>
        /// プレイシーンマネージャの登録
        /// </summary>
        /// <param name="aSceneTransitionManager"></param>
        public static void RgtrPlaySceneManager(PlaySceneManager aPlaySceneManager)
        {
            playSceneManager = aPlaySceneManager;
        }
    }
}
