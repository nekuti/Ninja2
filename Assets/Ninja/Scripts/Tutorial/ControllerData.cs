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



    public class ControllerData : MonoBehaviour
    {
        

        public static ControllerData instance;

        private bool isEndFind = false;


        public bool IsEndFind

        {
            set { isEndFind = value; }
            get { return isEndFind; }
        }


        [SerializeField]
        private List<Transform> lController = new List<Transform>();
        [SerializeField]
        private List<Transform> rController = new List<Transform>();



        // Use this for initialization
        void Start()
        {
            instance = this;

        }

        // Update is called once per frame
        void Update()
        {

        }






        /// <summary>
        /// パーツをセットする
        /// </summary>
        /// <param name="obj"></param>
        public void SetPartsList(HandType aHand, List<Transform> aList)
        {
            if (aHand == HandType.Left)
            {
                lController = aList;
            }
            else
            {
                rController = aList;
            }
        }




        /// <summary>
        /// PartsのTransformを取得する
        /// </summary>
        /// <param name="aHand"></param>
        /// <param name="aParts"></param>
        /// <returns></returns>
        public Transform GetPartsTransform(HandType aHand,PartsType aParts)
        {

            List<Transform> list = new List<Transform>();
            if (aHand == HandType.Left)
            {
                list = lController;
            }
            else
            {
               list =  rController;
            }

            return list[(int)aParts];
        }

    }

}