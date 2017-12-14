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
        AttackContrrolerPop,
        AttackPlayPop,
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
            currentSequence = TutorialSequence.AttackContrrolerPop;
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
                case TutorialSequence.AttackContrrolerPop:
                    Debug.Log("現在の順序 : " + currentSequence);
                   // tManager.SetEnabledTips(true, HandType.Left, PartsType.Trigger);
                   // tManager.SetEnabledTips(true, HandType.Right, PartsType.Trigger);

                   // TutorialManager.instance.ShowDisplay(displeyPos[0]);
              

                    break;


                case TutorialSequence.AttackPlayPop:
                    Debug.Log("現在の順序 : " + currentSequence);
                  
                    // TutorialManager.instance.ShowDisplay(displeyPos[0]);
                    // currentElement = Instantiate(sequenceList[sequenceNum]);

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



