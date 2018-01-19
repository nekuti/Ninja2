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
        StageClear,
        ResultStart,
        ResultRun,
        ResultGameClear,
        ResultGameOver,
        StageChange,
        ReturnPlayBase,
        TitleBack,
    }

    public enum StageState
    {
        None,
        LoadStart,
        LoadComplete,
        Run,
    }

    public class PlaySceneManager : SingletonMonoBehaviour<PlaySceneManager>
    {
        //  実行するステージのリスト(0番目は拠点を入れる)
        [SerializeField]
        private List<StageName> stageList = new List<StageName>() { StageName.PlayerBase };

        //  次に遷移するシーン
        [SerializeField]
        private SceneName nextScene = SceneName.ResultScene;

        //  ポーズボタンが押されたときにロードされるシーン
        [SerializeField]
        private SceneName pauseScene = SceneName.PauseTest;

        //  一番最初にスタートする配列番号
        [SerializeField, Tooltip("最初に実行するステージの配列番号を入力してください")]
        private int firstStage = 1;

        //  １階層のステージ数
        [SerializeField]
        private int FLOORNUM = 3;

        //  現在の実行中の配列番号
        [SerializeField]
        private int nowStageNum = 0;
        
        //  シーン遷移マネージャ
        public static SceneTransitionManager sceneTransitionManager;

        //  シーン遷移の方法
        private static StageTransition stageTransition = StageTransition.None;

        //  PlayDataの初期化用
        public PlayData initPlayData;
        //  プレイシーンマネージャーで管理する情報
        private static PlayData playData = new PlayData();

        //  クリアした階層
        public List<bool> clearFloorLevel = new List<bool>() { false, false, false };

        ////  シーン遷移時にフェードアウトをする時間(保存用)
        //private float fadeOutTime = 0.0f;
        ////  フェード後の経過時間
        //private float fadeElapsedTime = 0.0f;
        ////  フェードインの時間
        //private float fadeInTime = 1.0f;

        //  リザルトに渡す情報
        private ResultContainer resultContainer;

        //  ステージの状態
        private StageState stageState = StageState.None;

        //  ステージが存在しているか
        private bool stageExist = false;

        //  消費エネルギー(ゲーム終了時に保存しなくてもいい情報なのでこっち)
        private static float lostEnergy = 0f;

        new void Awake()
        {
            //  継承元のAwakeを実行(インスタンスが生成されているかの確認)
            base.Awake();
        }

        private void Start()
        {
            //  PlayDataに初期化用データを入れる
            playData = initPlayData;
            //  リザルトに渡す情報の初期化
            resultContainer = new ResultContainer();
            resultContainer.Initialize(this.gameObject);

            //  ステージの遷移をNoneに設定
            stageTransition = StageTransition.None;

            //  現在のステージを初回起動ステージに設定
            nowStageNum = firstStage;

            //  最初のステージを読み込む
            StageAdd(/*false*/);

            //  消費エネルギーを0に
            lostEnergy = 0f;
        }

        private void Update()
        {
            if (stageState == StageState.LoadStart)
            {
                //  ステージのロードが完了したか確認
                if (SceneManager.GetSceneByName(stageList[nowStageNum].IsName().ToString()) != null)
                {                      
                    //  ステートをロード完了に変更
                    stageState = StageState.LoadComplete;

                    //  ループカウント(0がプレイヤーベースなので1から計算開始)
                    var i = 1;

                    while (true)
                    {
                        if (nowStageNum == (i * FLOORNUM) + 1 || nowStageNum == 1)
                        {
                            //  プレイ時間の計測開始
                            resultContainer.PlayTimerStart();
                            //  稼いだ金額を現在の所持金分マイナスした値に設定
                            resultContainer.InitMoneyValue(playData.possessionMoney);
                            //  プレイする階層を設定
                            resultContainer.SetFloorLevel(i);
                            Debug.Log("階層を" + i + "に設定");

                            //  消費エネルギーを0に
                            lostEnergy = 0f;

                            //  計測を開始したのでループを出る
                            break;
                        }

                        i++;

                        //  ステージの数を超えたか確認
                        if (FLOORNUM * i > stageList.Count - 1)
                        {
                            //  階層の開始ステージではないのでループを出る
                            break;
                        }
                    }
                 
                    //  プレイヤーの座標をステージの開始位置へ
                    playData.player.ResetPosition(playData.startPos);

                    Debug.Log("プレイヤーに設定した値" + playData.startPos);

                    //  フェードを解除する
                    SteamVR_FadeEx.Start(Color.clear, 1.0f);
                    Debug.Log("フェードを解除");
                }          
            }

            //  ロードが完了したか
            if(stageState == StageState.LoadComplete)
            {
                //  ステートを実行中に変更
                stageState = StageState.Run;
            }

            //  実行中か
            if(stageState == StageState.Run)
            {
                //  シーンの遷移
                switch (stageTransition)
                {
                    case StageTransition.StageClear:
                        //  ステージ変更
                        StageChange();
                        stageTransition = StageTransition.None;
                        Debug.Log("ステージクリア");

                        break;
                    case StageTransition.ResultStart:
                        //  プレイヤーの座標をステージの開始位置へ
                        playData.player.ResetPosition(playData.startPos);
                        Debug.Log("プレイヤーに設定した値" + playData.startPos);

                        stageTransition = StageTransition.ResultRun;
                        break;
                    case StageTransition.ResultRun:
                        break;
                    case StageTransition.ResultGameClear:
                        //  クリアしたフロアのフラグをtrueへ
                        clearFloorLevel[resultContainer.floorLevel - 1] = true;
                        //  リザルトを追加
                        AddResult();
                        stageTransition = StageTransition.ResultStart;
                        Debug.Log("クリアリザルトへ");

                        break;
                    case StageTransition.ResultGameOver:
                        //  リザルトを追加
                        AddResult();
                        stageTransition = StageTransition.ResultStart;
                        Debug.Log("ゲームオーバーリザルトへ");

                        break;
                    case StageTransition.StageChange:
                        //  ステージ変更
                        StageChange();
                        stageTransition = StageTransition.None;
                        break;
                    case StageTransition.ReturnPlayBase:
                        StageChange(0);
                        Debug.Log("拠点に戻ります");

                        break;
                    case StageTransition.TitleBack:
                        //  タイトルへ戻る
                        sceneTransitionManager.ChangeSceneSingle(nextScene);
                        stageTransition = StageTransition.None;
                        Debug.Log("タイトルに戻ります");

                        break;
                }
            }    
        }

        /// <summary>
        /// ステージを追加する
        /// </summary>
        private void StageAdd()
        {
            //  ステージのスクリプト追加
            this.gameObject.AddComponent(Type.GetType("Ando." + stageList[nowStageNum].ToString()));

            //  ステージの読み込み
            SceneManager.LoadScene(stageList[nowStageNum].ToString(), LoadSceneMode.Additive);

            Debug.Log(stageList[nowStageNum] + "を読み込み");

            //  ステージの状態をシーンの読み込み開始に設定
            stageState = StageState.LoadStart;

            //  ステージの存在フラグをtrueへ
            stageExist = true;
        }

        /// <summary>
        /// ステージの削除
        /// </summary>
        private void StageUnload()
        {
            //  ステージが存在しているか
            if (stageExist)
            {
                //  ステージを削除
                SceneManager.UnloadSceneAsync(stageList[nowStageNum].ToString());
                //  ステージのスクリプトの削除
                Destroy(GetComponent(stageList[nowStageNum].ToString()));

                //  ステージの存在フラグをfalseへ
                stageExist = false;
            }
        }

        /// <summary>
        /// ステージの変更（現在のステージ配列の次を実行）
        /// </summary>
        /// <param name="aFade">フェードを行うか</param>
        /// <param name="aFadeColor">フェードアウトの時の画面色</param>
        /// <param name="aFadeTime">フェードアウトの時間</param>
        public void StageChange()
        {
            //  ステージを削除
            StageUnload();

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
            StageAdd();
        }

        /// <param name="aStageNumber"></param>
        /// <summary>
        /// 指定階層のステージから開始する(0:拠点)
        /// </summary>
        /// <param name="aFloorLevel">ステージの階層番号</param>
        /// <param name="aFade">フェードを行うか</param>
        /// <param name="aFadeColor">フェードアウトの時の画面色</param>
        /// <param name="aFadeTime">フェードアウトの時間</param>
        public void StageChange(int aFloorLevel)
        {
            //  ステージを削除
            StageUnload();

            //  ステージ番号を指定階層の最初にする
            nowStageNum = ((aFloorLevel - 1) * FLOORNUM) + 1;
            if(nowStageNum < 0)
            {
                nowStageNum = 0;
            }

            //  ステージを追加
            StageAdd();

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
        public void AddResult()
        {
            //  ステージを削除
            StageUnload();
           
            //  リザルトコンテナに値を設定
            resultContainer.PlayTimerStop();
            resultContainer.SetMoneyValue(playData.possessionMoney);
            resultContainer.lostEnergyValue = lostEnergy;

            //  リザルトがすでにあるか確認
            if (!sceneTransitionManager.SearchScene(SceneName.ResultScene))
            {
                //LiteResultText.SetLiteResult(a);
                
                //  リザルトシーンを追加
                sceneTransitionManager.ChangeSceneAdd(SceneName.ResultScene);

                Debug.Log(stageTransition.ToString());

                //  クリアしたか否かで分岐
                if (stageTransition == StageTransition.ResultGameClear)
                {
                    resultContainer.SetClearFlag(true);
                }
                else if (stageTransition == StageTransition.ResultGameOver)
                {
                    resultContainer.SetClearFlag(false);
                }

                //  リザルトコンテナを登録
                ResultSceneManager.RgtrResultContainer(resultContainer);
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
            if (playData.kunai.strengthenLevel > 0)
            {
                playData.possessionMoney -= aSubMoney;
            }
            else
            {
                Debug.Log("所持金が0なのに減算されました。");
            }
        }

        /// <summary>
        /// 消費エネルギーを加算する
        /// </summary>
        /// <param name="anAddValue"></param>
        public static void AddLostEnergy(float anAddValue)
        {
            Debug.Log("現在の消費エネルギー:" + lostEnergy +  "に" + anAddValue + "を加算");

            lostEnergy += anAddValue;
        }

        /// <summary>
        /// クナイの武器レベルを加算
        /// </summary>
        /// <param name="anAddnum"></param>
        public static void AddKunaiLevel(int anAddnum)
        {
            playData.kunai.strengthenLevel += anAddnum;
        }

        /// <summary>
        /// クナイの武器レベルを減算
        /// </summary>
        /// <param name="anAddnum"></param>
        public static void SubKunaiLevel(int aSubnum)
        {
            if (playData.kunai.strengthenLevel > 1)
            {
                playData.kunai.strengthenLevel -= aSubnum;
            }
            else
            {
                Debug.Log("武器レベルが1なのに減算されました。");
            }
        }

        /// <summary>
        /// 手裏剣の武器レベルを加算
        /// </summary>
        /// <param name="anAddnum"></param>
        public static void AddThrowingStarLevel(int anAddnum)
        {
            playData.throwingStar.strengthenLevel += anAddnum;
        }

        /// <summary>
        /// 手裏剣の武器レベルを減算
        /// </summary>
        /// <param name="anAddnum"></param>
        public static void SubThrowingStarLevel(int aSubnum)
        {
            if (playData.throwingStar.strengthenLevel > 1)
            {
                playData.throwingStar.strengthenLevel -= aSubnum;
            }
            else
            {
                Debug.Log("武器レベルが1なのに減算されました。");
            }
        }

        /// <summary>
        /// 爆弾の武器レベルを加算
        /// </summary>
        /// <param name="anAddnum"></param>
        public static void AddBumbLevel(int anAddnum)
        {
            playData.bomb.strengthenLevel += anAddnum;
        }

        /// <summary>
        /// 爆弾の武器レベルを減算
        /// </summary>
        /// <param name="anAddnum"></param>
        public static void SubBumbLevel(int aSubnum)
        {
            if (playData.bomb.strengthenLevel > 1)
            {
                playData.bomb.strengthenLevel -= aSubnum;
            }
            else
            {
                Debug.Log("武器レベルが1なのに減算されました。");
            }
        }

        /// <summary>
        /// おにぎりの所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionOnigiri(int anAddNum)
        {
            playData.onigiri.possession += anAddNum;
        }

        /// <summary>
        /// おにぎりの所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionOnigiri(int aSubnum)
        {
            if (playData.onigiri.possession > 0)
            {
                playData.onigiri.possession -= aSubnum;
            }
            else
            {
                Debug.Log("所持数が0なのに減算されました。");
            }
        }

        /// <summary>
        /// 火遁の術の所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionFireSkill(int anAddNum)
        {
            playData.fireSkill.possession += anAddNum;
        }
        
        /// <summary>
        /// 火遁の術の所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionFireSkill(int aSubnum)
        {
            if (playData.fireSkill.possession > 0)
            {
                playData.fireSkill.possession -= aSubnum;
            }
            else
            {
                Debug.Log("所持数が0なのに減算されました。");
            }
        }

        /// <summary>
        /// 土遁の術の所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionSoilSkill(int anAddNum)
        {
            playData.soilSkill.possession += anAddNum;
        }
        
        /// <summary>
        /// 土遁の術の所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionSoilSkill(int aSubnum)
        {
            if (playData.soilSkill.possession > 0)
            {
                playData.soilSkill.possession -= aSubnum;
            }
            else
            {
                Debug.Log("所持数が0なのに減算されました。");
            }
        }

        /// <summary>
        /// 召喚の術の所持数を加算
        /// </summary>
        /// <param name="anAddNum"></param>
        public static void AddPossessionSummonsSkill(int anAddNum)
        {
            playData.summonsSkill.possession += anAddNum;
        }
       
        /// <summary>
        /// 召喚の術の所持数を減算
        /// </summary>
        /// <param name="anSubNum"></param>
        public static void SubPossessionSummonsSkill(int aSubnum)
        {
            if (playData.summonsSkill.possession > 0)
            {
                playData.summonsSkill.possession -= aSubnum;
            }
            else
            {
                Debug.Log("所持数が0なのに減算されました。");
            }
        }

        #region Get
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

        /// <summary>
        /// プレイヤーの所持金情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static int GetPossessionMoney()
        {
            return playData.possessionMoney;
        }

        /// <summary>
        /// 武器強化の上限を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static int GetWeaponStrengthenMaxLevel()
        {
            return playData.weaponStrengthenMaxLevel;
        }

        /// <summary>
        /// クナイの情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static WeaponData GetKunai()
        {
            return playData.kunai;
        }

        /// <summary>
        /// 手裏剣の情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static WeaponData GetThrowingStar()
        {
            return playData.throwingStar;
        }

        /// <summary>
        /// 爆弾の情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static WeaponData GetBomb()
        {
            return playData.bomb;
        }

        /// <summary>
        /// おにぎりの情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static ItemData GetOnigiri()
        {
            return playData.onigiri;
        }

        /// <summary>
        /// 火遁の術の情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static ItemData GetFireSkill()
        {
            return playData.fireSkill;
        }

        /// <summary>
        /// 土遁の術の情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static ItemData GetSoilSkill()
        {
            return playData.soilSkill;
        }

        /// <summary>
        /// 召喚の術の情報を教える
        /// </summary>
        /// <param name="aPlayer"></param>
        public static ItemData GetSummonsSkill()
        {
            return playData.summonsSkill;
        }

        #endregion


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
