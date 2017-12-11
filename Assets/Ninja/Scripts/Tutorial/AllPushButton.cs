using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;


namespace Kondo
{
    public class AllPushButton : MonoBehaviour
    {

        private int count = 0;
        private int maxNum = 1;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            IsAllPushButton();

            if(count > maxNum)
            {
                TutorialManager.instance.SetEnabledAllTips(true);
                WireTutorialManager.instance.NextSequenceChanged();

            }
        }

        private void IsAllPushButton()
        {
            if(InputDevice.ClickDownTrriger(Kojima.HandType.Left))
            {
                TutorialManager.instance.SetEnabledTips(false, HandType.Left, PartsType.Trigger);
            }

            //if(InputDevice.PressDown())
            //{

            //}

        }
    }
}

