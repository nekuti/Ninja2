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


            if (Time.time < 1)
            {
                //Debug.Log("タイム : " + Time.time);
                return false;
            }

            // 子がいなければ処理を飛ばす
            if (aTrans.childCount != 16)
            {
                Debug.Log("ModelChild : "+aTrans.childCount);
                return false;
            }

            

            // パーツをfindしlistに追加
            for (int count = 0; count < (int)PartsType.MaxParts; count++)
            {
                if(aTrans.Find(partsName[count]) != null)
                list.Add(aTrans.Find(partsName[count]));
                Debug.Log(hand + "探索したパーツ : " + aTrans.Find(partsName[count]));

            }

            Debug.Log(hand + "のコントローラーパーツ探索完了");

            ControllerData.instance.IsEndFind = true; 

            // listをControllerDataに保存
            ControllerData.instance.SetPartsList(hand, list);
            return true;
        }
    }
}


