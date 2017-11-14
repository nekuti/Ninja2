using System.Collections;
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
        public List<GameObject> prefabList = new List<GameObject>();

        // 外部から操作用
        public static TutorialManager Instance;

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
            Instance = this;

            // 最初のシーンを設定
            // nullにしないため
            nextScene = NextScene.WireTutorial;

            // WireStartPopに設定
            sequenceNum = (int)TutorialSequence.WireStartPop;

            // リストを生成
            //prefabList = new List<GameObject>();

            // 始めの要素生成し要素を保存
            currentElement = Instantiate(prefabList[sequenceNum]);
        }


        void Start()
        {
            
        }


        // Update is called once per frame
        void Update()
        {
            // 要素が空の場合
            if (currentElement == null)
            {
                currentElement =  Instantiate(prefabList[sequenceNum]);
                Debug.Log(prefabList[sequenceNum]+"Scene");
            }
        }

        
        /// <summary>
        /// 状態変更時の動作
        /// </summary>
        public void NextStateChanged()
        {
            Debug.Log(sequenceNum+"エレメント削除");
            Destroy(currentElement);
            sequenceNum++;
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


