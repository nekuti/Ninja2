using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kondo
{



    public class ItemTutorialManager :BaseTutoralManager<ItemTutorialManager>
    {

        /// <summary>
        /// チュートリアルの順序
        /// </summary>
        public enum TutorialSequence
        {
            Item01,
            Item02,
            Item03,
            Item04,
            Item05,
            Item06,
            ItemEnd,
            MaxSequence
        }



        public TutorialSequence currentSequence;



        // Use this for initialization
        new void Start()
        {
            base.Start();
            instance = this;
            tManager.LoadText(loadTextName);

            tManager.SetSelectEven(NextSequenceChanged);
        }

        // Update is called once per frame
        void Update()
        {
            if (base.StartSequence())
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
                case TutorialSequence.Item01:
                    {

                        tManager.SetEnabledTips(true, HandType.Left, PartsType.Lgrip);
                        tManager.SetEnabledTips(true, HandType.Right, PartsType.Rgrip);

                        tManager.ChangeMenuSelect();

                        tManager.ShowDisplay(displeyPos[0]);
                        break;
                    }

                case TutorialSequence.Item02:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        
                        tManager.SetTipsText("アイテム", HandType.Left, PartsType.Lgrip);
                        tManager.SetTipsText("アイテム", HandType.Right, PartsType.Rgrip);
                        break;
                    }

                case TutorialSequence.Item03:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        break;
                    }

                case TutorialSequence.Item04:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        break;
                    }

                case TutorialSequence.Item05:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        tManager.ChangePlay();
                        // アイテムの使用を取得する処理
                        base.NextElementChange();
                        break;
                    }

                case TutorialSequence.Item06:
                    {
                        tManager.ShowDisplay(displeyPos[0]);
                        tManager.ChangeMenuSelect();
                        base.DestoroyCurrentElement();
                        break;
                    }

                case TutorialSequence.ItemEnd:
                    {
                        tManager.DeleteSelectEvent();
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

