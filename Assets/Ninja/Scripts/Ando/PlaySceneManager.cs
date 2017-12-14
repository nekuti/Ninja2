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
        //  実行するステージのリスト(0番目は拠点を入れる)
        [SerializeField]
        private List<StageName> stageList = new List<StageName>() { StageName.PlayerBase };

        //  次に遷移するシーン
        [SerializeField]
        private SceneName nextScene = SceneName.TitleTest;

        //  ポーズボタンが押されたときにロードされるシーン
        [SerializeField]
        private SceneName pauseScene = SceneName.PauseTest;

        //  一番最初にスタートする配列番号
        [SerializeField, Tooltip("最初に実行するステージの配列番号を入力してください")]
        private int firstStage = 1;

        //  現在の実行中の配列番号
        [SerializeField]
        private int nowStageNum = 0;

        //  １階層のステージ数
        private const int FLOORNUM = 5;

        //  シーン遷移マネージャ
        public static SceneTransitionManager sceneTransitionManager;

        //  シーン遷移の方法
        private static StageTransition stageTransition = StageTransition.None;

        //  プレイシーンマネージャーで管理する情報
        private static PlayData playData = new PlayData();

        //  ステージをクリアしたか判別
        private static bool stageUnloadFlag = false;

        //  クリアした階層
        public List<bool> clearFloorLevel = new List<bool>() { false, false, false };

        //  シーン遷移時にフェードアウトをする時間(保存用)
        private float fadeOutTime = 0.0f;
        //  フェード後の経過時間
        private float fadeElapsedTime = 0.0f;
        //  フェードインの時間
        private float fadeInTime = 1.0f;


        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();
        }

        private void Start()
        {
            //  ステージの遷移をNoneに設定
            stageTransition = StageTransition.None;

            //  現在のステージを初回起動ステージに設定
            nowStageNum = firstStage;

            //  最初のステージを読み込む
            StageAdd(false);

            playData.Initialize();

        }

        private void Update()
        {
            //  リザルト表示するためにステージを消すか
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

            //  フェードの解除関数
            this.FadeRelease();
        }

        /// <summary>
        /// フェードの解除を行う
        /// </summary>
        private void FadeRelease()
        {
            //  経過時間がフェードアウトの時間を超えたか
            if (fadeElapsedTime < fadeOutTime)
            {
                //  経過時間を加算
                fadeElapsedTime += Time.deltaTime;
            }
            else
            {
                //  フェードアウトの時間が設定されているか
                if (fadeOutTime > 0)
                {
                    //  フェードを解除する
                    SteamVR_Fade.Start(Color.clear, fadeInTime);
                }
                //  使用した変数を初期化
                fadeOutTime = 0.0f;
                fadeElapsedTime = 0.0f;
            }
        }

        /// <summary>
        /// ステージを追加する
        /// </summary>
        private void StageAdd(bool aFade = true, Color aFadeColor = new Color(), float aFadeTime = 1.0f)
        {
            if (aFade)
            {
                //  フェイドの色が透明か設定されていないので黒を設定
                if (aFadeColor == new Color(0, 0, 0, 0))
                {
                    aFadeColor = new Color(0, 0, 0, 1);
                }

                //  指定色、指定時間でフェード開始
                SteamVR_Fade.Start(aFadeColor, aFadeTime);

                //  フェイドの時間を保存
                fadeOutTime = aFadeTime;
            }

            //  ステージのスクリプト追加
            this.gameObject.AddComponent(Type.GetType("Ando." + stageList[nowStageNum].ToString()));

            //  ステージの読み込み
            SceneManager.LoadScene(stageList[nowStageNum].ToString(), LoadSceneMode.Additive);            
        }

        /// <summary>
        /// ステージの削除
        /// </summary>
        private void StageUnload()
        {
            //  ステージを削除
            SceneManager.UnloadSceneAsync(stageList[nowStageNum].ToString());
            //  ステージのスクリプトの削除
            Destroy(GetComponent(stageList[nowStageNum].ToString()));
        }

        /// <summary>
        /// ステージの変更（現在のステージ配列の次を実行）
        /// </summary>
        /// <param name="aFade">フェードを行うか</param>
        /// <param name="aFadeColor">フェードアウトの時の画面色</param>
        /// <param name="aFadeTime">フェードアウトの時間</param>
        public void StageChange(bool aFade = true, Color aFadeColor = new Color(), float aFadeTime = 1.0f)
        {
            //  ステージを削除
            SceneManager.UnloadSceneAsync(stageList[nowStageNum].ToString());
            //  ステージのスクリプトの削除
            Destroy(GetComponent(stageList[nowStageNum].ToString()));

            //  簡易リザルトを削除
            //sceneTransitionManager.RevocationScene(SceneName.LiteResult);

            //  ステージをクリアしたら最初のステージから
            if (stageList.Count - 1 > nowStageNum)
            {
                nowStageNum++;
            }
            else
            {
                nowStageNum = 0;
            }

            //  ステージを追加
            StageAdd(aFade,aFadeColor,aFadeTime);

            //  プレイヤーの操作をプレイ用に切り替え
            playData.player.ChangeHandState(Kojima.HandStateType.Play);
        }

        /// <param name="aStageNumber"></param>
        /// <summary>
        /// 指定階層のステージから開始する(0:拠点)
        /// </summary>
        /// <param name="aFloorLevel">ステージの階層番号</param>
        /// <param name="aFade">フェードを行うか</param>
        /// <param name="aFadeColor">フェードアウトの時の画面色</param>
        /// <param name="aFadeTime">フェードアウトの時間</param>
        public void StageChange(int aFloorLevel, bool aFade = true, Color aFadeColor = new Color(), float aFadeTime = 1.0f)
        {
            //  ステージを削除
            SceneManager.UnloadSceneAsync(stageList[nowStageNum].ToString());
            //  ステージのスクリプトの削除
            Destroy(GetComponent(stageList[nowStageNum].ToString()));

            //  ステージ番号を指定階層の最初にする
            nowStageNum = aFloorLevel + ((aFloorLevel - 1) * FLOORNUM);

            //  簡易リザルトを削除
            //sceneTransitionManager.RevocationScene(SceneName.LiteResult);

            //  ステージを追加
            StageAdd(aFade, aFadeColor, aFadeTime);

            //  プレイヤーの操作をプレイ用に切り替え
            playData.player.ChangeHandState(Kojima.HandStateType.Play);
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
            stageTransition = StageTransition.StageChange;
            return;

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
        /// 該当シーンが存在するか
        /// </summary>
        /// <param name="aSceneName"></param>
        /// <returns></returns>
        public bool SearchStage(StageName aStageName)
        {
            foreach (StageName stageBace in stageList)
            {
                if (stageBace == aStageName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// プレイシーン内の遷移
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
        /// プレイシーンマネージャにプレイヤーの所持金情報を設定
        /// </summary>
        /// <param name="aMoney"></param>
        public static void SetPossessionMoney(int aMoney)
        {
            playData.possessionMoney = aMoney;
        }

        /// <summary>
        /// 金額を加算
        /// </summary>
        /// <param name="anAddMoney"></param>
        public static void AddPossessionMoney(int anAddMoney)
        {
            playData.possessionMoney += anAddMoney;
        }


        /// <summary>
        /// 金額を減算
        /// </summary>
        /// <param name="aSubMoney"></param>
        public static void SubPossessionMoney(int aSubMoney)
        {
            playData.possessionMoney -= aSubMoney;
        }

        /// <summary>
        /// おにぎりの所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionOnigili(int anAddNum)
        {
            playData.possessionOnigiri += anAddNum;
        }
        /// <summary>
        /// おにぎりの所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionOnigili(int anSubNum)
        {
            playData.possessionOnigiri -= anSubNum;
        }

        /// <summary>
        /// 火遁の術の所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionFireSkill(int anAddNum)
        {
            playData.possessionFireSkill += anAddNum;
        }
        /// <summary>
        /// 火遁の術の所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionFireSkill(int anSubNum)
        {
            playData.possessionFireSkill -= anSubNum;
        }

        /// <summary>
        /// 土遁の術の所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionSoilSkill(int anAddNum)
        {
            playData.possessionSoilSkill += anAddNum;
        }
        /// <summary>
        /// 土遁の術の所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionSoilSkill(int anSubNum)
        {
            playData.possessionSoilSkill -= anSubNum;        
        }

        /// <summary>
        /// 召喚の術の所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionSummonsSkill(int anAddNum)
        {
            playData.possessionSummonsSkill += anAddNum;
        }
        /// <summary>
        /// 召喚の術の所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionSummonsSkill(int anSubNum)
        {
            playData.possessionSummonsSkill -= anSubNum;
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
        /// プレイシーンマネージャにステージが終了したかを設定
        /// </summary>
        /// <param name="aStageEnd"></param>
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
        /// プレイヤーの所持金情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static int GetPossessionMoney()
        {
            return playData.possessionMoney;
        }

        /// <summary>
        /// プレイヤーのおにぎり所持数を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static int GetPossessionOnigiri()
        {
            return playData.possessionOnigiri;
        }

        /// <summary>
        /// プレイヤーの火遁の術所持数を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static int GetPossessionFireSkill()
        {
            return playData.possessionFireSkill;
        }

        /// <summary>
        /// プレイヤーの土遁の術所持数を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static int GetPossessionSoilSkill()
        {
            return playData.possessionSoilSkill;
        }

        /// <summary>
        /// プレイヤーの召喚の術所持数を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static int GetPossessionSummonsSkill()
        {
            return playData.possessionSummonsSkill;
        }


        /// <summary>
        /// ステージの開始位置を教える
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetStartPos()
        {
            return playData.startPos;
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
