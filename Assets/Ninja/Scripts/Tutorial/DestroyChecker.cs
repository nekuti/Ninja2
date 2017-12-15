using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kondo
{
    public class DestroyChecker : MonoBehaviour
    {

        private List<GameObject> checkList = new List<GameObject>();

        public UnityEngine.Events.UnityEvent EndFunc = new UnityEngine.Events.UnityEvent();


        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

            int count = 0;

            foreach (var list in checkList)
            {
                if (list == null)
                {
                    count++;
                }
            }

            // すべてnullなら設定された関数を実行
            if (count == checkList.Count)
            {
                EndFunc.Invoke();
            }
        }


        /// <summary>
        /// チェックするListを設定する
        /// </summary>
        /// <param name="aList"></param>
        public void SetCheckList(List<GameObject> aList)
        {
            checkList = aList;
            //EndFanc.AddListener(() => CheckList());
        }

    }
}

    