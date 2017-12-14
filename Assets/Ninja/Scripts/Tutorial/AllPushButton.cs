using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;


namespace Kondo
{
    public class AllPushButton : MonoBehaviour
    {

        private float time = 0.0f;
        private int maxNum = 1;
        [SerializeField]
        private bool[] isPushList = new bool[8];

        // Use this for initialization
        void Start()
        {
            for(int i = 0;i<isPushList.Length;i++)
            {
                isPushList[i] = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            IsAllPushButton();

            if(Input.GetKeyDown(KeyCode.F1))
            {
                TutorialManager.instance.SetEnabledAllTips(false);
            }

            time += Time.deltaTime;
            if(time > maxNum)
            {
                time = 0.0f;
                int count = 0;
                foreach (var list in isPushList)
                {
                    if (list) break;
                    count++;
                }

               // if (count >= isPushList.Length - 1) ;
               // WireTutorialManager.instance.NextSequenceChanged();


            }
        }




        private void IsAllPushButton()
        {
            #region 左手フラグセット

            // メニューボタン
            if (InputDevice.PressDown(ButtonType.ApplicationMenu, Kojima.HandType.Left))
            {
                isPushList[0] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Left, PartsType.Button);
            }
            
            // グリップ
            if (InputDevice.PressDown(ButtonType.Grip, Kojima.HandType.Left))
            {
                isPushList[1] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Left, PartsType.Lgrip);
                TutorialManager.instance.SetEnabledTips(false, HandType.Left, PartsType.Rgrip);
            }

            // トラックパッド
            if (InputDevice.PressDown(ButtonType.Touchpad, Kojima.HandType.Left))
            {
                isPushList[2] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Left, PartsType.Trackpad);
            }

            // トリガー
            if (InputDevice.ClickDownTrriger(Kojima.HandType.Left))
            {
                isPushList[3] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Left, PartsType.Trigger);
            }





            #endregion


            #region 右手フラグセット

            // メニューボタン -----------------------------------------------------------------------
            if (InputDevice.PressDown(ButtonType.ApplicationMenu, Kojima.HandType.Right))
            {
                isPushList[4] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Right, PartsType.Button);
            }

            // グリップ -----------------------------------------------------------------------------
            if (InputDevice.PressDown(ButtonType.Grip, Kojima.HandType.Right))
            {
                isPushList[5] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Right, PartsType.Lgrip);
                TutorialManager.instance.SetEnabledTips(false, HandType.Right, PartsType.Rgrip);
            }

            // トラックパッド -----------------------------------------------------------------------
            if (InputDevice.PressDown(ButtonType.Touchpad, Kojima.HandType.Right))
            {
                isPushList[6] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Right, PartsType.Trackpad);
            }

            // トリガー -----------------------------------------------------------------------------
            if (InputDevice.ClickDownTrriger(Kojima.HandType.Right))
            {
                isPushList[7] = true;
                TutorialManager.instance.SetEnabledTips(false, HandType.Right, PartsType.Trigger);
            }

            #endregion

        }
    }
}

