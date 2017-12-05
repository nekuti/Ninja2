using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kondo
{
    public enum PartsType
    {
        Base,
        Button,
        Lgrip,
        Rgrip,
        Trackpad,
        Trigger,
        MaxParts,
    }

    public enum HandType
    {
        Left,
        Right
    }



    public class FindControllerParts : MonoBehaviour
    {

        // 手を指定
        public HandType handType;

        // Find用配列
        private string[] partsName = 
            {"base",
             "button",
             "lgrip",
             "rgrip",
             "trackpad",
             "trigger"};


        // 親を保存する
        // コントローラパーツのtransform取得用
        private static Transform[][] controller = new Transform[2][];

        // Findが終了しているかどうか
        private bool isFindEnd = false;


        


        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(!isFindEnd)
            {
                FindAllParts(this.gameObject);
            }
        }



        /// <summary>
        /// アタッチされたオブジェクトからパーツを探す
        /// </summary>
        /// <param name="obj"></param>
       private void FindAllParts(GameObject obj)
        {
            Transform parent = obj.transform;

            // 子がいなければ処理を飛ばす
            if (parent.childCount == 0)
            {
                Debug.Log(obj + "の子無いのでFindできません");
                return;
            }

            // 指定された手の各パーツをFindしていく
            for(int count = 0; count < (int)PartsType.MaxParts;count++)
            {
                controller[(int)handType][count] = parent.Find(partsName[count]).GetComponent<Transform>();
            }

            if(controller != null)
            {
                isFindEnd = true;
            }

        }


        /// <summary>
        /// 左右を指定しコントローラーのベースを取得する
        /// </summary>
        /// <param name="aHand"></param>
        /// <returns></returns>
        public static Transform GetTransformBase(HandType aHand)
        {
            Transform t = controller[(int)aHand][(int)PartsType.Base];
            if (t == null)
            {
                Debug.Log(PartsType.Base.ToString() + "の取得に失敗しました。");
                return null;
            }
            return t;
        }



        /// <summary>
        /// 手とパーツを指定しそのパーツのtransfomeを取得
        /// </summary>
        /// <param name="aHand">どちらの手か</param>
        /// <param name="aParts">どのパーツか</param>
        /// <returns></returns>
        public static Transform GetTransfomeParts(HandType aHand, PartsType aParts)
        {
            Transform t = controller[(int)aHand][(int)aParts];
            if(t == null)
            {
                Debug.Log(aParts.ToString() + "の取得に失敗しました。");
                return null;
            }
            return t;
        }


    }

}
