using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kondo
{
    public class DestroyChecker : MonoBehaviour
    {

        private List<GameObject> checkList = new List<GameObject>();

        private UnityEngine.Events.UnityEvent EndFunc = new UnityEngine.Events.UnityEvent();



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
        }

        /// <summary>
        /// 終了時に実行する関数を登録する
        /// </summary>
        /// <param name="aFuncName"></param>
        public void SetEvent(UnityEngine.Events.UnityAction aFuncName)
        {
            EndFunc.AddListener(aFuncName);
        }

    }
}

    