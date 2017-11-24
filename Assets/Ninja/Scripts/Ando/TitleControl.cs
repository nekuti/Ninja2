using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class TitleControl : MonoBehaviour
    {
        public SteamVR_TrackedObject trackdObject;
        public SteamVR_Controller.Device device;

        //  ゲームの開始判定フラグ
        private static bool gameStart = false;

        //  コントローラーの入力取得を許可するか
        private bool controllerDecisionAccept = true;

       void Awake()
        {
            controllerDecisionAccept = true;
        }
        // Use this for initialization
        void Start()
        {
            //  VRコントローラの入力関係の初期化
            trackdObject = GetComponent<SteamVR_TrackedObject>();
            if (trackdObject != null) device = SteamVR_Controller.Input((int)trackdObject.index);

            //  ゲームの開始フラグを初期化
            gameStart = false;

            //  シーンに入ったときにトリガーが押されている場合は入力取得をしない
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                controllerDecisionAccept = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //  コントローラーの入力取得が許可されているか
            if (controllerDecisionAccept)
            {
                //  VRコントローラのトリガー処理の判定
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    Debug.Log("トリガーが押されました");

                    //  ゲーム開始フラグをゲーム開始可能にする
                    gameStart = true;
                }
            }
            //  トリガーが押されていない場合は入力取得を許可する
            else if (!device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                controllerDecisionAccept = true;
            }
        }

        /// <summary>
        /// ゲームの開始フラグを取得
        /// </summary>
        /// <returns></returns>
        public static bool GetGameStart()
        {
            return gameStart;
        }
    }
}