using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class PlayerBaseManager : StageManager
    {
        //  プレイシーンマネージャ
        public static PlaySceneManager playSceneManager;

        //  ステージ移動用オブジェクト
        [SerializeField]
        private List<GameObject> stageSwitches = new List<GameObject>();

        // Use this for initialization
        new void Start()
        {
            //  プレイヤーの操作をメニュー用に切り替え
            PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.MenuSelect);

            foreach (GameObject stageSwitch in stageSwitches)
            {
                stageSwitch.SetActive(false);
            }

            HiddenDoor.SetDoorAnimeState(DoorAnimeState.None);

            //  拠点BGMを再生
            AudioManager.Instance.PlayBGM(AudioName.BGM_BASE01);
        }

        // Update is called once per frame
        new void Update()
        {
            int i = 0;
            int clearCount = 1;

           foreach(bool clearFloorLavel in playSceneManager.clearFloorLevel)
            {
                var stageSwitch = stageSwitches[i];

                if (clearFloorLavel)
                {
                    stageSwitch.SetActive(true);
                    clearCount++;


                    Debug.LogWarning("カウント加算" + clearCount);
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
                FadeStart();
            }

            if(HiddenDoor.GetDoorAnimeState() == DoorAnimeState.End)
            {
                if (clearCount > stageSwitches.Count)
                {
                    clearCount--;

                    Debug.LogWarning("カウント減算" + clearCount);
                }

                playSceneManager.StageChange(clearCount);

                Debug.LogWarning("カウント" + clearCount);
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
