using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

using Kojima;

namespace Kondo
{

    public class WireTutorialManager : BaseTutoralManager<WireTutorialManager>
    {
        /// <summary>
        /// チュートリアルの順序
        /// </summary>
        public enum TutorialSequence
        {
            WireStartPop,
            WireControllerPop,
            WirePlayPop,
            WireMovePop,
            WirePlayMovePop1,
            WirePlayMovePop2,
            WireGoal,
            WireEnd,
            MaxSequence
        }

        // 動く壁
        public List<GameObject> moveWall = new List<GameObject>();
        [SerializeField]
        private  TutorialSequence currentSequence;




        void Awake()
        {
            
            instance = this;

            // 始めの要素生成し要素を保存
            //currentElement = Instantiate(sequenceList[sequenceNum]);

            Debug.Log("ワイヤーチュートリアル　Awake()");

        }


        void Start()
        {
            //Ando.PlaySceneManager.GetStartPos();
            //Quaternion direction = InputTracking.GetLocalRotation(VRNode.Head);
            //Vector3 trm = InputTracking.GetLocalPosition(VRNode.Head);
            Debug.Log("ワイヤーチュートリアル　strat()");
            //currentSequence =  TutorialSequence.WireStartPop;
            //Debug.Log("currentSequence : " + currentSequence);
            SequenceChange();

        }


        // Update is called once per frame
        void Update()
        {
            Debug.Log("ワイヤーチュートリアル　Update()");

            // test
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextSequenceChanged();

            }
        }


        protected override void SequenceChange()
        {

            Debug.Log("ワイヤーチュートリアル　SquenceChange()");
            TutorialManager tManager = TutorialManager.instance;
            switch (currentSequence)
            {
                case TutorialSequence.WireStartPop:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ShowNotice();
                    tManager.ShowDisplay(displeyPos[0]);
                    break;

                case TutorialSequence.WireControllerPop:
                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.ShowNotice();

                    tManager.SetEnabledTips(true, HandType.Left, PartsType.Trigger);
                    tManager.SetEnabledTips(true, HandType.Right, PartsType.Trigger);

                    tManager.SetTipsText("ワイヤー", HandType.Left, PartsType.Trigger);
                    tManager.SetTipsText("ワイヤー", HandType.Right, PartsType.Trigger);

                    //tManager.RemoveDisplay();
                    break;


                case TutorialSequence.WirePlayPop:
                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ShowDisplay(displeyPos[0]);
                    currentElement = Instantiate(sequenceList[sequenceNum]);
                    break;


                case TutorialSequence.WireMovePop:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.HideSelectButton(false);
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.ShowNotice();

                    break;



                case TutorialSequence.WirePlayMovePop1:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.HideSelectButton(true);

                    break;




                case TutorialSequence.WirePlayMovePop2:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.RemoveDisplay(false);
                    moveWall[0].GetComponentInChildren<WallMove>().StartMove();
                    break;



                case TutorialSequence.WireGoal:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.SetEnabledTips(false, HandType.Left, PartsType.Trigger);
                    tManager.SetEnabledTips(false, HandType.Right, PartsType.Trigger);
                    tManager.ShowDisplay(displeyPos[1]);

                    break;



                case TutorialSequence.WireEnd:
                    Debug.Log("現在の順序 : " + currentSequence);
  

                    TutorialManager.instance.NextSceneChanged();
                    break;
            }
        {
            
            }
        }




        /// <summary>
        /// 次のシーケンスに移行する
        /// </summary>
        public override void NextSequenceChanged()
        {
            currentSequence++;
            base.NextSequenceChanged();
        }


    }
}


