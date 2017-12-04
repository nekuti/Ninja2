using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{


    public class ConTipsCreate : TutorialElement
    {
        [SerializeField]
        public GameObject testCube;

        [SerializeField]
        private List<GameObject> TipsList = new List<GameObject>();
        [SerializeField]
        private List<string> FindNameList = new List<string>();

        private List<GameObject> conTipsObj = new List<GameObject>();


       // private Transform trans;


        // Use this for initialization
        void Start()
        {
            if(TipsList.Count == FindNameList.Count)
            {
                for (int count = 0; count < TipsList.Count; count++)
                {
                    // 破棄用に保存
                    conTipsObj[count] = Instantiate(TipsList[count]);

                    //Transform trans = findModel.tLeft.transform.Find(FindNameList[count]);
                    //if (trans != null)
                    //{
                    //    trans.parent = testCube.transform;
                    //    Debug.Log(trans + " : 探索成功");
                    //}
                    //else
                    //{
                    //    Debug.Log(trans + " : 探索失敗");
                    //}
                }
            }
            else
            {
                Debug.Log("Tipsオブジェクトの要素数とFindする要素数が合いません");
            }
           
           
            // findModel.tLeft.transform;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                foreach(var obj in conTipsObj)
                {
                    Destroy(obj);
                }
                OnChangedSequence();
            }

        }

 
    }
}

