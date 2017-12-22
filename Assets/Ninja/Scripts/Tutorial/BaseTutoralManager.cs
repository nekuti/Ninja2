using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

using Kojima;
namespace Kondo
{

    public class BaseTutoralManager<T> : MonoBehaviour
    {



        // 外部から操作用
        public static T instance;

        [SerializeField]
        protected string loadTextName;

        // ディスプレイ表示ポジション
        public List<Transform> displeyPos = new List<Transform>();
        [SerializeField]
        protected List<GameObject> sequenceList = new List<GameObject>();

        protected TutorialManager tManager;

        // チュートリアルの動作順序
        protected int sequenceNum;

        // 現在の要素
        protected GameObject currentElement;

        private bool firstFlag = true;

        void Start()
        {
            tManager = TutorialManager.instance;
        }


        protected void SetStart()
        {
        }



        protected bool StartSequence()
        {
            if (firstFlag)
            {
                if (ControllerData.instance.IsEndFind)
                {
                    firstFlag = false;
                    SteamVR_Fade.Start(Color.clear, 1f);
                    return true;
                }

            }

            return false;
        }




        protected virtual void SequenceChange()
        {
        }



        /// <summary>
        /// 現在のPrefab要素を消して次のPrefab要素を出す
        /// </summary>
        public void NextElementChanged()
        {
            DestoroyCurrentElement();

            currentElement = Instantiate(sequenceList[sequenceNum]);
            sequenceNum++;
        }




        /// <summary>
        /// 次のシーケンスに移行する
        /// </summary>
        public virtual void NextSequenceChanged()
        {
        }





        /// <summary>
        /// 現在の要素を消す
        /// </summary>
        public void DestoroyCurrentElement()
        {
            if (currentElement != null)
            {
                Destroy(currentElement);
            }
        }

    }

}

