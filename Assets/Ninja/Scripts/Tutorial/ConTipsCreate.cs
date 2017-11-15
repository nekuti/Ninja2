using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class ConTipsCreate : TutorialElement
    {

        public GameObject testCube;

        [SerializeField]
        private GameObject conTipsPrefab;

        private GameObject conTipsObj;

        private Transform transLTrigger, transRTrigger;


        // Use this for initialization
        void Start()
        {
            conTipsObj = Instantiate(conTipsPrefab);
            transLTrigger = findModel.tLeft.transform.Find("trigger");
            if(transLTrigger != null)
            {
                transLTrigger.parent = testCube.transform;
                Debug.Log("探索成功!!!!");
            }
            // findModel.tLeft.transform;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(conTipsObj);
                OnChangedSequence();
            }

        }

 
    }
}

