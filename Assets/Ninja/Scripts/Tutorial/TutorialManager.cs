﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    /// <summary>
    /// チュートリアルの順序
    /// </summary>
    public enum TutorialSequence
    {
        WireStartPop,
        WireControllerPop,
        WireControllerTips,
        WireEnd,
        MaxSequence
    }

    public enum NextScene
    {
        WireTutorial,
        AttackTutorial,
        BattalTutorial,
        GoToBase,
    }

    public class TutorialManager : MonoBehaviour
    {
        // prefab
        [SerializeField]
        private List<GameObject> sequenceList = new List<GameObject>();

        // 外部から操作用
        public static TutorialManager instance;
        // 外部からmodelの操作用
        //public static FindModel conModel = null;

        // 現在のシーン
        public NextScene nextScene;

        // 現在の状態
        //private TutorialSequence currentState;

        // チュートリアルの動作順序
        private int sequenceNum;

        // 現在の要素
        private GameObject currentElement;


        void Awake()
        {
            instance = this;

            // 最初のシーンを設定
            // nullにしないため
            nextScene = NextScene.WireTutorial;

            // WireStartPopに設定
            sequenceNum = (int)TutorialSequence.WireStartPop;

            // 始めの要素生成し要素を保存
            currentElement = Instantiate(sequenceList[sequenceNum]);
        }


        void Start()
        {
            
        }


        // Update is called once per frame
        void Update()
        {
            // 要素が削除されていた場合
            // シーケンスが最後の時でない場合
            if (currentElement == null && sequenceNum != (int)TutorialSequence.MaxSequence)
            {
                // 次の要素を実行
                currentElement =  Instantiate(sequenceList[sequenceNum]);
                Debug.Log(sequenceList[sequenceNum]+"Scene");
            }
        }

        
        /// <summary>
        /// 状態を次に移行する
        /// </summary>
        public void NextStateChanged()
        {
            if(currentElement != null)
            {
                Debug.Log(sequenceNum + "エレメント削除");
                Destroy(currentElement);
                sequenceNum++;
            }
            else
            {
                Debug.Log("エレメントが空");
            }
        }


        /// <summary>
        /// 次のシーンに遷移する
        /// </summary>
        /// <param name="aScene"></param>
        public void ChangeScene(NextScene aScene)
        {
            switch(aScene)
            {
                case NextScene.WireTutorial:
                    {
                        // アタックシ―ンへ
                        nextScene = NextScene.AttackTutorial;
                        Debug.Log("アタックシーン");
                        break;
                    }
                case NextScene.AttackTutorial:
                    {
                        // バトルシ―ンへ
                        nextScene = NextScene.BattalTutorial;
                        Debug.Log("バトルシーン");
                        break;
                    }
                case NextScene.BattalTutorial:
                    {
                        // ベースシ―ンへ
                        nextScene = NextScene.GoToBase;
                        Debug.Log("ベースシーン");
                        break;
                    }
            }
        }
    }
}


