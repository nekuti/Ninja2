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
                        tManager.ShowDisplay(displeyPos[0]);
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

