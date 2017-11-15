using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public class ButtonTest : MonoBehaviour
    {
        //private int num;
        static float CHENGE_TIME = 3.0f;
        private float time;


        // Use this for initialization
        void Start()
        {
            //num = 0;
            time = 0f;
            Debug.Log("ButtonTest");
        }


        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time > CHENGE_TIME)
            {
                time = 0f;
                OnChangedButton();
            }

        }


        private void OnChangedButton()
        {
           TutorialManager.instance.NextStateChanged();
        }
    }
}
