using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public class StartPopCreate : TutorialElement
    {

        [SerializeField]
        private GameObject StartPopPrefab;

        private GameObject StartPopObj;

        // Use this for initialization
        void Start()
        {
            StartPopObj = Instantiate(StartPopPrefab);
            Debug.Log("StartPopCreate");
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(StartPopObj);
                OnChangedSequence();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //TutorialManager.instance.ChangeScene(NextScene.WireTutorial);
            }
        }

    }
}


