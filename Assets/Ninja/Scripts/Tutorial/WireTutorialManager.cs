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
            Wire01,
            Wire02,
            Wire03,
            Wire04,
            Wire05,
            Wire06,
            Wire07,
            Wire08,
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
            tManager.LoadText(loadTextName);

            Debug.Log("ワイヤーチュートリアル　strat()");
            // セレクトボタンが実行する関数を設定
            // ワイヤーチュートリアルを進める
            tManager.SetSelectEven(NextSequenceChanged);
        }


        // Update is called once per frame
        void Update()
        {
            // Debug.Log("ワイヤーチュートリアル　Update()");

            if(base.StartSequence())
            {
                NextSequenceChanged();
            }

            // test
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextSequenceChanged();
            }
        }


        protected override void SequenceChange()
        {

            Debug.Log("ワイヤーチュートリアル　SquenceChange()");
            switch (currentSequence)
            {
                case TutorialSequence.Wire01:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.SetEnabledTips(true, HandType.Left, PartsType.Trackpad);
                    tManager.SetEnabledTips(true, HandType.Right, PartsType.Trackpad);

                    tManager.ChangeMenuSelect();
                    tManager.ShowNotice();
                    tManager.ShowDisplay(displeyPos[0]);

                    break;


                case TutorialSequence.Wire02:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ShowDisplay(displeyPos[0]);
                    break;


                case TutorialSequence.Wire03:
                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.ShowNotice();

                    tManager.SetEnabledTips(true, HandType.Left, PartsType.Trigger);
                    tManager.SetEnabledTips(true, HandType.Right, PartsType.Trigger);

                    tManager.SetTipsText("ワイヤー", HandType.Left, PartsType.Trigger);
                    tManager.SetTipsText("ワイヤー", HandType.Right, PartsType.Trigger);

                    //tManager.RemoveDisplay();
                    break;


                case TutorialSequence.Wire04:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ShowDisplay(displeyPos[0]);
                    base.NextElementChanged();
                    break;


                case TutorialSequence.Wire05:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ChangePlay();
                    tManager.HideSelectButton(false);
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.ShowNotice();

                    break;



                case TutorialSequence.Wire06:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ChangeMenuSelect();
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.HideSelectButton(true);

                    break;




                case TutorialSequence.Wire07:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ChangePlay();
                    tManager.RemoveDisplay(false);
                    moveWall[0].GetComponentInChildren<WallMove>().StartMove();
                    break;



                case TutorialSequence.Wire08:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.ChangeMenuSelect();
                    base.DestoroyCurrentElement();
                    tManager.SetEnabledTips(false, HandType.Left, PartsType.Trigger);
                    tManager.SetEnabledTips(false, HandType.Right, PartsType.Trigger);
                    tManager.ShowDisplay(displeyPos[1]);

                    break;



                case TutorialSequence.WireEnd:
                    Debug.Log("現在の順序 : " + currentSequence);
                    tManager.RemoveDisplay(false);
                    tManager.DeleteSelectEvent();
                    tManager.ResetPlayerTransfome();
                    tManager.NextSceneRequest();
                    break;
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


