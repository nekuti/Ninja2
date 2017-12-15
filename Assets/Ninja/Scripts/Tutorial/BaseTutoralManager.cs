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

        // ディスプレイ表示ポジション
        public List<Transform> displeyPos = new List<Transform>();
        [SerializeField]
        protected List<GameObject> sequenceList = new List<GameObject>();

        [SerializeField]
        protected string loadTextName;

        protected TutorialManager tManager;

        // チュートリアルの動作順序
        protected int sequenceNum;

        // 現在の要素
        protected GameObject currentElement;


     


        protected void SetTutorialManger()
        {
            tManager = TutorialManager.instance;
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

            sequenceNum++;
            currentElement = Instantiate(sequenceList[sequenceNum]);
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





        /// <summary>
        /// 次のシーケンスに移行する
        /// </summary>
        public virtual void NextSequenceChanged()
        {
            //currentSequence++;
            SequenceChange();
        }

    }
}

