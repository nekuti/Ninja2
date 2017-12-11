using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{
    public class FindControllerParts : MonoBehaviour
    {

        // Find用配列
        public  string[] partsName =
            {"base",
             "button",
             "lgrip",
             "rgrip",
             "trackpad",
             "trigger"};

        [SerializeField]
        private HandType hand;

        [SerializeField]
        private bool isFindEnd = false;

        public List<Transform> list = new List<Transform>();

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
            
            // 子がいなければ処理を飛ばす
            if (aTrans.childCount == 0)
            {
                Debug.Log(hand + "の" + aTrans.gameObject + "の子が無いのでFindできません");
                return false;
            }

            

            // パーツをfindしlistに追加
            for (int count = 0; count < (int)PartsType.MaxParts; count++)
            {
                if(aTrans.Find(partsName[count]) != null)
                list.Add(aTrans.Find(partsName[count]));
            }

            // listをControllerDataに保存
            ControllerData.instance.SetPartsList(hand, list);
            return true;
        }
    }
}


