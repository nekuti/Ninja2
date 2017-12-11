using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

using Kojima;

namespace Kondo
{
    /// <summary>
    /// チュートリアルの順序
    /// </summary>
    public enum WireTutorialSequence
    {
        WireStartPop,
        WireControllerPop,
        WirePlayPop,
        WireMovePop,
        WireEnd,
        MaxSequence
    }


    public class WireTutorialManager : MonoBehaviour
    {


        // 外部から操作用
        public static WireTutorialManager instance;
        // ディスプレイ表示ポジション
        public List<Transform> displeyPos = new List<Transform>();

        // prefab
        [SerializeField]
        private List<GameObject> sequenceList = new List<GameObject>();

        // 動く壁
        public List<GameObject> moveWall = new List<GameObject>();




        //private MoveNotice notice;

        // 外部からmodelの操作用
        //public static FindModel conModel = null;
        // 現在のシーン
        //public NextScene nextScene;
        // 現在の状態
        //private WireTutorialSequence currentState;

        public WireTutorialSequence currentSequence;

        // チュートリアルの動作順序
        private int sequenceNum;

        // 現在の要素
        private GameObject currentElement;


        void Awake()
        {
            instance = this;

            // 最初のシーンを設定
            //nextScene = NextScene.WireTutorial;

            // WireStartPopに設定
            //sequenceNum = (int)WireTutorialSequence.WireStartPop;

            // 始めの要素生成し要素を保存
            currentElement = Instantiate(sequenceList[sequenceNum]);


        }


        void Start()
        {
            Ando.PlaySceneManager.GetStartPos();
            Quaternion direction = InputTracking.GetLocalRotation(VRNode.Head);
            Vector3 trm = InputTracking.GetLocalPosition(VRNode.Head);
            currentSequence = WireTutorialSequence.WireStartPop;
            SequenceChange();

        }


        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                NextSequenceChanged();

            }
        }


        private void SequenceChange()
        {
            TutorialManager tManager = TutorialManager.instance;
            switch(currentSequence)
            {
                case WireTutorialSequence.WireStartPop:
                    tManager.SetEnabledAllTips(false);
                    tManager.ShowNotice();
                    tManager.ShowDisplay(displeyPos[0]);
                    break;

                case WireTutorialSequence.WireControllerPop:
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.ShowNotice();

                    tManager.SetEnabledTips(true,HandType.Left, PartsType.Trigger);
                    tManager.SetEnabledTips(true, HandType.Right, PartsType.Trigger);

                    tManager.SetTipsText("ワイヤー", HandType.Left, PartsType.Trigger);
                    tManager.SetTipsText("ワイヤー", HandType.Right, PartsType.Trigger);

                    //tManager.RemoveDisplay();
                    //moveWall[0].transform.GetComponentInChildren<WallMove>().StartMove();
                    break;


                case WireTutorialSequence.WirePlayPop:
                    tManager.ShowDisplay(displeyPos[0]);
                    NextElementChanged();
                    break;


                case WireTutorialSequence.WireMovePop:
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.ShowNotice();

                    break;
            }
        }



        public void NextElementChanged()
        {
            DestoroyCurrentElement();

             sequenceNum++;
            currentElement = Instantiate(sequenceList[sequenceNum]);
        }




        public void DestoroyCurrentElement()
        {
            if (currentElement != null)
            {
                Destroy(currentElement);
            }
        }



        /// <summary>
        /// 状態を次に移行する
        /// </summary>
        public void NextSequenceChanged()
        {
            currentSequence++;
            SequenceChange();
        }
    }
}


