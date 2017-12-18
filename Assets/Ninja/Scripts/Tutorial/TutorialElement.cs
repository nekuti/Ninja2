using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class TutorialElement : MonoBehaviour
    {

        //public static FindModel findModel;


        // Use this for initialization
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {

        }


        public void OnChangedSequence()
        {
            TutorialManager.instance.NextSceneRequest();
        }

    }
}

