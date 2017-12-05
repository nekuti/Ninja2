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

    public enum StageState
    {
        None,
        Run,
        GameClear,
        GameOver,
    }

    public class PlaySceneManager : SingletonMonoBehaviour<PlaySceneManager>
    {
        //  実行するステージのリスト
        [SerializeField]
        private List<StageName> stageList = new List<StageName>();

        //  次に遷移するシーン
        [SerializeField]
        private SceneName nextScene = SceneName.TitleTest;
        //  ポーズボタンが押されたときにロードされるシーン
        [SerializeField]
        private SceneName pauseScene = SceneName.PauseTest;

        //  現在のステージ
        private StageName nowStage = StageName.StageTest1;

        //  シーン遷移マネージャ
        public static SceneTransitionManager sceneTransitionManager;

        //  シーン遷移の方法
        private static StageTransition stageTransition = StageTransition.None;

        //  プレイシーンマネージャーで管理する情報
        private static PlayData playData = new PlayData();

        //  ステージをクリアしたか判別
        private static bool stageUnloadFlag = false;

        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();
        }

        private void Start()
        {
            stageTransition = StageTransition.None;

            //  最初のステージを読み込む
            StageAdd();
        }

        private void Update()
        {
            if (stageUnloadFlag)
            {
                StageUnload();
            }

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
                    stageTransition = StageTransition.None;
                    break;
                case StageTransition.TitleBack:
                    //  タイトルへ戻る
                    sceneTransitionManager.ChangeSceneSingle(nextScene);
                    stageTransition = StageTransition.None;
                    break;
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    a = !a;
            //    LiteResultText.SetTextChangeFlag(a);
            //}
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

        /// <summary>
        /// ステージを追加する
        /// </summary>
        private void StageAdd()
        {
            //  ステージのスクリプト追加
            this.gameObject.AddComponent(Type.GetType("Ando." + nowStage.ToString()));

            //  ステージの読み込み
            SceneManager.LoadScene(stageList[(int)nowStage].ToString(), LoadSceneMode.Additive);
        }

        /// <summary>
        /// ステージの削除
        /// </summary>
        private void StageUnload()
        {
            //  ステージを削除
            SceneManager.UnloadSceneAsync(stageList[(int)nowStage].ToString());
            //  ステージのスクリプトの削除
            Destroy(GetComponent(nowStage.ToString()));
        }
        /// <summary>
        /// ステージの変更
        /// </summary>
        public void StageChange()
        {
            //  ステージを削除
            SceneManager.UnloadSceneAsync(stageList[(int)nowStage].ToString());
            //  ステージのスクリプトの削除
            Destroy(GetComponent(nowStage.ToString()));

            //  簡易リザルトを削除
            sceneTransitionManager.RevocationScene(SceneName.LiteResult);

            //  ステージをクリアしたら最初のステージから
            if (stageList.Count - 1 > (int)nowStage)
            {
                nowStage++;
            }
            else
            {
                nowStage = 0;
            }

            //  ステージを追加
            StageAdd();
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
            stageUnloadFlag = true;

            //var a = new ResultContainer();

            //a.totalPlayTime = "test";
            //a.playTime = "test";
            //a.totalGetMoneyValue = 1;
            //a.getMoneyValue = 1;
            //a.totalLostEnergyValue = 0;
            //a.lostEnergyValue = 1;
            //a.killEnemyValue = 1;
            //a.useItemValue = 1;

            //  簡易リザルトがすでにあるか確認
            if (!sceneTransitionManager.SearchScene(SceneName.LiteResult))
            {
                //LiteResultText.SetLiteResult(a);

                sceneTransitionManager.ChangeSceneAdd(SceneName.LiteResult);

                Debug.Log(stageTransition.ToString());

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

        /// <summary>
        /// プレイシーンマネージャにプレイヤーの情報を設定
        /// </summary>
        /// <param name="aPlayer"></param>
        public static void SetPlayer(Kojima.Player aPlayer)
        {
            playData.player = aPlayer;
        }

        /// <summary>
        /// プレイシーンマネージャにステージのスタート位置を設定
        /// </summary>
        /// <param name="aStartPos"></param>
        public static void SetStartPos(Vector3 aStartPos)
        {
            playData.startPos = aStartPos;
        }

        /// <summary>
        /// プレイシーンマネージャにコントローラーの入力情報を設定
        /// </summary>
        public static void SetContlollerInfo(Kojima.InputDevice aInputDevice)
        {
            playData.inputDevice = aInputDevice;
        }

        /// <summary>
        /// プレイシーンマネージャにステージが終了したかを設定
        /// </summary>
        /// <returns></returns>
        public static void SetStageEnd(StageState aStageEnd)
        {
            playData.stageEnd = aStageEnd;
        }

        /// <summary>
        /// プレイヤーの情報を教える
        /// </summary>
        /// <returns></returns>
        public static Kojima.Player GetPlayer()
        {
            return playData.player;
        }

        /// <summary>
        /// ステージの開始位置を教える
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetStartPos()
        {
            return playData.startPos;
        }

        public static Kojima.InputDevice GetInputDevice()
        {
            return playData.inputDevice;
        }

        #region ステージの状態を取得(ここ以外で使わないと思うのでコメントアウト中)
        /// <summary>
        /// ステージをクリアしたか教える
        /// </summary>
        /// <returns></returns>
        //public static StageState GetStageEnd()
        //{
        //    return playData.stageEnd;
        //}
        #endregion
    }
}
