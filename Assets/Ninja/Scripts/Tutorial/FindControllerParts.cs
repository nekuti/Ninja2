using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class FindControllerParts : MonoBehaviour
    {

        // Find用配列
        public string[] partsName =
        {
            "base",
             "button",
             "lgrip",
             "rgrip",
             "trackpad",
             "trigger"
        };


        [SerializeField]
        private HandType hand;

        [SerializeField]
        private bool isFindEnd = false;

        public List<Transform> list = new List<Transform>();

        private const int CHILD_NUM = 16;
        private const float WAIT_TIME = 1f;


        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!isFindEnd)
            {
                isFindEnd = FindAllParts(this.transform);
            }
        }


        private bool FindAllParts(Transform aTrans)
        {
           

            if (Time.time < WAIT_TIME)
            {
                return false;
            }

            // 子がいなければ処理を飛ばす
            if (aTrans.childCount != CHILD_NUM)
            {
                Debug.Log("ModelChild : "+aTrans.childCount);
                return false;
            }

            

            // パーツをfindしlistに追加
            for (int count = 0; count < (int)PartsType.MaxParts; count++)
            {
                if(aTrans.Find(partsName[count]) != null)
                list.Add(aTrans.Find(partsName[count]));
                Debug.Log(hand + "の探索したパーツ : " + aTrans.Find(partsName[count]));

            }

            Debug.Log(hand + "のコントローラーパーツ探索完了");

            ControllerData.instance.IsEndFind = true; 

            // listをControllerDataに保存
            ControllerData.instance.SetPartsList(hand, list);
            return true;
        }
    }
}


