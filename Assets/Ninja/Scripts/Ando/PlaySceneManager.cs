//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace Ando
{
    public enum StageTransition
    {
        None,
        ResultGameClear,
        ResultGameOver,
        StageChange,
        TitleBack,
    }

    public class PlaySceneManager : SingletonMonoBehaviour<PlaySceneManager>
    {
        //  ステージがすでに生成しているか
        private bool stageExist = false;

        [SerializeField]
        private List<StageName> stageList = new List<StageName>();

        [SerializeField]
        private SceneName nextScene = SceneName.TitleTest;
        [SerializeField]
        private SceneName pauseScene = SceneName.PauseTest;

        private StageName nowStage = StageName.StageTest1;

        public static SceneTransitionManager sceneTransitionManager;

        private static StageTransition stageTransition = StageTransition.None;
        public bool a = false;
        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();
        }

        private void Start()
        {
            StageChange();
        }

        private void Update()
        {
            switch (stageTransition)
            {
                case StageTransition.ResultGameClear:
                case StageTransition.ResultGameOver:
                    //  リザルトを追加
                    AddLiteResult();
                    break;
                case StageTransition.StageChange:
                    //  ステージ変更
                    StageChange();
                    break;
                case StageTransition.TitleBack:
                    //  タイトルへ戻る
                    sceneTransitionManager.ChangeSceneSingle(nextScene);
                    break;
            }
            if (Input.GetMouseButtonDown(0))
            {
                a = !a;
                LiteResultText.SetTextChangeFlag(a);
            }
            ////  タイムスケールの設定
            //if (sceneTransitionManager.GetComponent<PlayTest>().PauseFlag)
            //{
            //    Time.timeScale = 0.0f;
            //}
            //else
            //{
            //    Time.timeScale = 1.0f;
            //}
            //if (!sceneTransitionManager.GetComponent<PlayTest>().PauseFlag)
            //{
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        sceneTransitionManager.ChangeSceneSingle(nextScene);
            //    }
            //    if (Input.GetMouseButtonDown(1))
            //    {
            //        sceneTransitionManager.GetComponent<PlayTest>().PauseFlag = true;
            //        sceneTransitionManager.ChangeSceneAdd(pauseScene);
            //    }
            //    if (Input.GetKeyDown(KeyCode.A))
            //    {
            //        StageChange();
            //    }
            //}
            //else if (sceneTransitionManager.GetComponent<PlayTest>().PauseFlag)
            //{
            //    if (Input.GetMouseButtonDown(2))
            //    {
            //        sceneTransitionManager.GetComponent<PlayTest>().PauseFlag = false;
            //    }
            //}
        }

        public void StageChange()
        {
            //  初回起動時に分岐
            if (stageExist)
            {
                //  ステージを削除
                SceneManager.UnloadSceneAsync(stageList[(int)nowStage].ToString());
                //  ステージのスクリプトの削除
                Destroy(GetComponent(nowStage.ToString()));
                //  簡易リザルトを削除
                sceneTransitionManager.RevocationScene(SceneName.LiteResult);

                if (stageList.Count - 1 > (int)nowStage)
                {
                    nowStage++;
                }
                else
                {
                    nowStage = 0;
                }
            }
            else
            {
                stageExist = true;
            }

            //  ステージのスクリプト追加
            var newStage = this.gameObject.AddComponent(Type.GetType("Ando." + nowStage.ToString()));

            //  シーンの読み込み
            SceneManager.LoadScene(stageList[(int)nowStage].ToString(), LoadSceneMode.Additive);

        }

        /// <summary>
        /// シーン遷移マネージャの登録
        /// </summary>
        /// <param name="aSceneTransitionManager"></param>
        public static void RgtrSceneTransitionManager(SceneTransitionManager aSceneTransitionManager)
        {
            sceneTransitionManager = aSceneTransitionManager;
        }

        /// <summary>
        /// 簡易リザルトをステージに追加する
        /// </summary>
        public static void AddLiteResult()
        {
            var a = new ResultContainer();

            a.totalPlayTime = "test";
            a.playTime = "test";
            a.totalGetMoneyValue = 1;
            a.getMoneyValue = 1;
            a.totalLostEnergyValue = 0;
            a.lostEnergyValue = 1;
            a.killEnemyValue = 1;
            a.useItemValue = 1;

            //  簡易リザルトがすでにあるか確認
            if (!sceneTransitionManager.SearchScene(SceneName.LiteResult))
            {
                sceneTransitionManager.ChangeSceneAdd(SceneName.LiteResult);
                LiteResultText.SetLiteResult(a);

                if (stageTransition == StageTransition.ResultGameClear)
                {
                    LiteResultText.SetTextChangeFlag(true);
                }
                else if (stageTransition == StageTransition.ResultGameOver)
                {
                    LiteResultText.SetTextChangeFlag(false);
                }
            }
        }

        /// <summary>
        ///   プレイシーン内の遷移
        /// </summary>
        /// <param name="aStageTransition"></param>
        public static void SetStageTransition(StageTransition aStageTransition)
        {
            stageTransition = aStageTransition;
        }
    }
}
