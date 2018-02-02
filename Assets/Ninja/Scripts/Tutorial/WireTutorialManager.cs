using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Wire09,
            WireEnd,
            MaxSequence
        }

        // 動く壁
        public List<GameObject> moveWall = new List<GameObject>();
        [SerializeField]
        private  TutorialSequence currentSequence;




        new void Start()
        {
            base.Start();
            instance = this;
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
                SequenceChange();
            }

            // test
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextSequenceChanged();
            }
        }


        protected override void SequenceChange()
        {

            Debug.Log("現在の順序 : " + currentSequence);
            switch (currentSequence)
            {
                case TutorialSequence.Wire01:
                    {
                        tManager.SetEnabledAllTips(false);
                        tManager.SetEnabledTips(true, HandType.Left, PartsType.Trackpad);
                        tManager.SetEnabledTips(true, HandType.Right, PartsType.Trackpad);
                        tManager.EnabledSelectEffect();
                        tManager.ChangeMenuSelect();
                        // tManager.ShowNotice();
                        tManager.ShowDisplay(displeyPos[0]);

                        break;
                    }

                case TutorialSequence.Wire02:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        break;
                    }

                case TutorialSequence.Wire03:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        //tManager.ShowNotice();

                        tManager.SetEnabledTips(true, HandType.Left, PartsType.Trigger);
                        tManager.SetEnabledTips(true, HandType.Right, PartsType.Trigger);

                        tManager.SetTipsText("ワイヤー", HandType.Left, PartsType.Trigger);
                        tManager.SetTipsText("ワイヤー", HandType.Right, PartsType.Trigger);
                        break;
                    }

                case TutorialSequence.Wire04:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        base.NextElementChange();
                        break;
                    }

                case TutorialSequence.Wire05:
                    {
                        tManager.ChangePlay();
                        tManager.HideSelectButton(false);
                        tManager.ShowDisplay(displeyPos[0]);
                        // tManager.ShowNotice();
                        break;
                    }

                case TutorialSequence.Wire06:
                    {
                        tManager.ChangeMenuSelect();
                        tManager.EnabledSelectEffect();
                        tManager.ShowDisplay(displeyPos[0]);
                        tManager.HideSelectButton(true);

                        break;
                    }

                case TutorialSequence.Wire07:
                    {
                        tManager.ChangePlay();
                        tManager.RemoveDisplay(false);
                        moveWall[0].GetComponentInChildren<WallMove>().StartMove();
                        break;
                    }

                case TutorialSequence.Wire08:
                    {
                        moveWall[1].GetComponentInChildren<WallMove>().StartMove();
                        tManager.ChangeMenuSelect();
                        tManager.EnabledSelectEffect();
                        base.DestoroyCurrentElement();
                        tManager.SetEnabledTips(false, HandType.Left, PartsType.Trigger);
                        tManager.SetEnabledTips(false, HandType.Right, PartsType.Trigger);
                        tManager.ShowDisplay(displeyPos[1]);

                        break;
                    }

                case TutorialSequence.Wire09:
                    {
                        tManager.ChangePlay();
                        tManager.RemoveDisplay(false);

                        break;
                    }

                case TutorialSequence.WireEnd:
                    {
                        tManager.DeleteSelectEvent();
                       // tManager.ResetPlayerTransfome();
                        tManager.NextSceneRequest();
                        break;
                    }

            }
        }
            
    


        /// <summary>
        /// 次のシーケンスに移行する
        /// </summary>
        public override void NextSequenceChanged()
        {
            currentSequence++;
            SequenceChange();
        }


    }
}


