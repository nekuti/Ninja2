using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kondo
{
    public enum TutorialSequence
    {
        StartPop,
        ControllerPop,
        ControllerTips,
        End
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

        public static TutorialManager Instance;

        //public NextScene nextscene;

        // 現在の状態
        private TutorialSequence currentState;
        // 現在のシーン
        private NextScene currentScene;

        private TutorialSequence tests = TutorialSequence.StartPop;


        void Awake()
        {
            Instance = this;
           // SetCurrentState(TutorialSequence.StartPop);
            Instantiate(prefabList[(int)TutorialSequence.StartPop]);
        }


        // Use this for initialization
        void Start()
        {
           
        }


        // Update is called once per frame
        void Update()
        {
            //Instantiate(prefab);
        }


        /// <summary>
        /// 外部から状態を変更
        /// </summary>
        /// <param name="aState"></param>
        public void SetCurrentState(TutorialSequence aState)
        {
            currentState = aState;
            OnStateChanged(currentState);
        }


        /// <summary>
        /// 状態変更時の動作
        /// </summary>
        /// <param name="aState"></param>
        private void OnStateChanged(TutorialSequence aState)
        {
            switch (aState)
            {
                case TutorialSequence.StartPop:
                    Debug.Log("スタートポップ");
                    break;

                case TutorialSequence.ControllerPop:
                    Debug.Log("コントローラポップ");
                    break;

                case TutorialSequence.ControllerTips:
                    Debug.Log("コントローラtips");
                    break;

                case TutorialSequence.End:
                    Debug.Log("おわり");
                    break;

            }
        }


        /// <summary>
        /// 外部からシーンを変更
        /// </summary>
        /// <param name="aScene"></param>
        public void SetCurrentScene(NextScene aScene)
        {
            currentScene = aScene;
        }
    }
}


