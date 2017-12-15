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
        Attack05,
        AttackEnd,
        MaxSequence
    }




    public class AttackTutorialManager : BaseTutoralManager<AttackTutorialManager>
    {

       // [SerializeField]
        public List<GameObject> enemyList = new List<GameObject>();
        public List<GameObject> enemyTypeList = new List<GameObject>();

        [SerializeField]
        private Transform enemyPos;

        public TutorialSequence currentSequence;


        private void Awake()
        {
            instance = this;
        }



        // Use this for initialization
        void Start()
        {
            base.SetTutorialManger();
            tManager.LoadText(loadTextName);

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

            switch (currentSequence)
            {
                case TutorialSequence.Attack01:
                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ChangeMenuSelect();
                    tManager.SetEnabledTips(true, HandType.Left, PartsType.Trackpad);
                    tManager.SetEnabledTips(true, HandType.Right, PartsType.Trackpad);

                    tManager.ShowDisplay(displeyPos[0]);

                    break;


                case TutorialSequence.Attack02:
                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ChangePlay();
                    tManager.ShowDisplay(displeyPos[0]);
                    tManager.SetTipsText("攻撃", HandType.Left, PartsType.Trackpad);
                    tManager.SetTipsText("攻撃", HandType.Right, PartsType.Trackpad);

                    enemyList.Add(Instantiate(enemyTypeList[0]));
                    enemyList[0].transform.position = enemyPos.position;
                    currentElement = Instantiate(sequenceList[sequenceNum],transform.position,Quaternion.identity);
                  
                    // 敵の生存チェック用Listを設定
                    currentElement.GetComponentInChildren<DestroyChecker>().SetCheckList(enemyList);
                    // すべての敵がnullの時呼び出される関数を設定
                    currentElement.GetComponentInChildren<DestroyChecker>().EndFunc.AddListener(this.NextSequenceChanged);
                    break;



                case TutorialSequence.Attack03:
                    enemyList.Add(Instantiate(enemyTypeList[1]));
                    enemyList[1].transform.position = enemyPos.position;

                    // 敵の生存チェック用Listを設定
                    currentElement.GetComponentInChildren<DestroyChecker>().SetCheckList(enemyList);

                    break;



                case TutorialSequence.Attack04:
                    Debug.Log("現在の順序 : " + currentSequence);

                    DestoroyCurrentElement();
                    tManager.ChangeMenuSelect();
                    tManager.ShowDisplay(displeyPos[0]);

                    tManager.SetEnabledTips(false, HandType.Left, PartsType.Trackpad);
                    tManager.SetEnabledTips(false, HandType.Right, PartsType.Trackpad);

                    break;




                case TutorialSequence.Attack05:

                    Debug.Log("現在の順序 : " + currentSequence);

                    tManager.ChangeMenuSelect();
                    tManager.ShowDisplay(displeyPos[0]);


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



