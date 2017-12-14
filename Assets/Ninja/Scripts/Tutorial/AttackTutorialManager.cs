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
    public enum TutorialSequence
    {
        Attack01,
        Attack02,
        Attack03,
        Attack04,
        AttackEnd,
        MaxSequence
    }




    public class AttackTutorialManager : BaseTutoralManager<AttackTutorialManager>
    {


        public TutorialSequence currentSequence;


        private void Awake()
        {
            instance = this;
        }



        // Use this for initialization
        void Start()
        {
            Debug.Log("アタックチュートリアル　strat()");
            currentSequence = TutorialSequence.Attack01;
            Debug.Log("currentSequence : " + currentSequence);
            SequenceChange();
        }



        // Update is called once per frame
        void Update()
        {
            // test
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextSequenceChanged();

            }
        }




        protected override void SequenceChange()
        {
            Debug.Log("アタックチュートリアル　SquenceChange()");
            TutorialManager tManager = TutorialManager.instance;

            switch (currentSequence)
            {
                case TutorialSequence.Attack01:
                    Debug.Log("現在の順序 : " + currentSequence);
                    // tManager.SetEnabledTips(true, HandType.Left, PartsType.Trigger);
                    // tManager.SetEnabledTips(true, HandType.Right, PartsType.Trigger);
                    tManager.SetEnabledTips(true, HandType.Left, PartsType.Trackpad);
                    tManager.SetEnabledTips(true, HandType.Right, PartsType.Trackpad);

                    tManager.ChangeMenuSelect();
                    tManager.ShowDisplay(displeyPos[0]);

                    break;


                case TutorialSequence.Attack02:
                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ChangePlay();
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.SetTipsText("攻撃", HandType.Left, PartsType.Trackpad);
                    tManager.SetTipsText("攻撃", HandType.Right, PartsType.Trackpad);

                    currentElement = Instantiate(sequenceList[sequenceNum]);

                    break;



                case TutorialSequence.Attack03:
                    Debug.Log("現在の順序 : " + currentSequence);

                    DestoroyCurrentElement();
                    tManager.ChangeMenuSelect();
                    tManager.ShowDisplay(displeyPos[0]);

                    tManager.SetEnabledTips(false, HandType.Left, PartsType.Trackpad);
                    tManager.SetEnabledTips(false, HandType.Right, PartsType.Trackpad);
                

                    break;



                case TutorialSequence.Attack04:
                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ChangeMenuSelect();

                    break;



                case TutorialSequence.AttackEnd:
                    Debug.Log("現在の順序 : " + currentSequence);

                    //tManager.ChangeMenuSelect();

                    break;


            }
        }





        public override void NextSequenceChanged()
        {
            currentSequence++;
            base.NextSequenceChanged();
        }

    }
 
}



