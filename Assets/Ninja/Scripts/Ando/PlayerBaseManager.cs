using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class PlayerBaseManager : SingletonMonoBehaviour<PlayerBaseManager>
    {
        //  プレイシーンマネージャ
        public static PlaySceneManager playSceneManager;

        //  ステージ移動用オブジェクト
        [SerializeField]
        private List<GameObject> stageSwitches = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            //  プレイヤーの操作をメニュー用に切り替え
            PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.MenuSelect);

            //ray = new Ray(PlaySceneManager.GetPlayer().position, PlaySceneManager.GetPlayer().transform.rotation);
            foreach (GameObject stageSwitch in stageSwitches)
            {
                stageSwitch.SetActive(false);
            }

            HiddenDoor.SetDoorAnimeState(DoorAnimeState.None);

            /*β版用処理*/
            for (int i = 0; i < 20; i++)
            {
                PlaySceneManager.GetPlayer().UseOnigiri();
            }
            /*β版用処理*/
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

            //   ドアのアニメーションが開始になった場合
            if(HiddenDoor.GetDoorAnimeState() == DoorAnimeState.Start)
            {
                SteamVR_Fade.Start(Color.black, 2);
            }

            if(HiddenDoor.GetDoorAnimeState() == DoorAnimeState.End)
            {
                playSceneManager.StageChange();

                PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.Play);
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
