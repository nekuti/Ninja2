using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public class ResultSceneManager : SingletonMonoBehaviour<ResultSceneManager>
    {
        //  シーン遷移マネージャ
        public static SceneTransitionManager sceneTransitionManager;

        //  開始地点
        [SerializeField]
        private GameObject startObj;

        //  評価用データテーブル
        [SerializeField]
        private ResultAssessmentDataTable resultAssessmentDataTable;

        //  描画テキスト
        [SerializeField]
        private Text playTime;
        [SerializeField]
        private Text getMoney;
        [SerializeField]
        private Text lostEnergy;

        //  評価を表示するイメージ
        [SerializeField]
        private Image playTimeRankImage;
        [SerializeField]
        private Image getMoneyRankImage;
        [SerializeField]
        private Image lostEnergyRankImage;
        [SerializeField]
        private Image totalRankImage;

        //  評価イメージ
        [SerializeField]
        private List<Sprite> imageList;
        [SerializeField]
        private List<Sprite> totalImageList;

        //  評価を保存
        [SerializeField]
        private int playTimeAssessment;
        [SerializeField]
        private int getMoneyAssessment;
        [SerializeField]
        private int lostEnergyAssessment;

        //  次に遷移するシーン
        [SerializeField]
        private SceneName nextScene = SceneName.None;

        //  評価基準
        [SerializeField]
        private List<int> playTimeReferenceValue;
        [SerializeField]
        private List<int> getMoneyReferenceValue;
        [SerializeField]
        private List<int> lostEnergyeReferenceValue;

        //  評価の値
        private const int YUU = 4;
        private const int RYOU = 3;
        private const int KA = 2;
        private const int FU = 1;
        private const int NONE = 0;
        private const int ASSESSMENT_MAX = 11;
        private const int ASSESSMENT_MID = 7;
        private const int ASSESSMENT_MIN = 4;

        //  コンテナの受け渡し用
        private static ResultContainer deliveryResultContainer;
        //  リザルトシーンで使用する情報
        private ResultContainer resultContainer;

        //  フェードの開始フラグ
        private bool fadeFlag = false;
        //  フェード時間
        private float fadeCount = 1.0f;

        // Use this for initialization
        　new void Awake()
        {
            base.Awake();

            //  開始地点を設定
            PlaySceneManager.SetStartPos(startObj.gameObject.transform.position);

            Debug.Log("設定された値：" + startObj.gameObject.transform.position + "　所持している値：" + PlaySceneManager.GetStartPos());

            //  フェードを解除する
            SteamVR_FadeEx.Start(Color.clear, 1.0f);
            Debug.Log("フェードを解除");

            //  インスペクターで設定するもの以外を設定
            playTimeAssessment = 0;
            getMoneyAssessment = 0;
            lostEnergyAssessment = 0;

            //  プレイヤーの操作をメニュー用に切り替え
            PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.MenuSelect);

            //  受け渡し用リザルトからコンテナへ情報を渡す
            resultContainer = deliveryResultContainer;
            deliveryResultContainer = null;

            //  フェードフラグをfalseへ
            fadeFlag = false;
            //  フェード時間を設定
            fadeCount = 1.0f;

            //  階層の分岐
            switch (resultContainer.floorLevel)
            {
                case 1:
                    playTimeReferenceValue = resultAssessmentDataTable.level1PlayTime;
                    getMoneyReferenceValue = resultAssessmentDataTable.level1GetMoney;
                    lostEnergyeReferenceValue = resultAssessmentDataTable.level1LostEnergy;

                    Debug.Log("階層1");
                    break;
                case 2:
                    playTimeReferenceValue = resultAssessmentDataTable.level2PlayTime;
                    getMoneyReferenceValue = resultAssessmentDataTable.level2GetMoney;
                    lostEnergyeReferenceValue = resultAssessmentDataTable.level2LostEnergy;

                    Debug.Log("階層2");
                    break;
                case 3:
                    playTimeReferenceValue = resultAssessmentDataTable.level3PlayTime;
                    getMoneyReferenceValue = resultAssessmentDataTable.level3GetMoney;
                    lostEnergyeReferenceValue = resultAssessmentDataTable.level3LostEnergy;

                    Debug.Log("階層3");
                    break;
                default:
                    Debug.LogError("リザルトシーン：階層の値がおかしいので評価項目を設定できませんでした。");
                    sceneTransitionManager.ChangeSceneSingle(SceneName.TitleScene);
                    break;
            }

            //  テスト用に作成
            //resultContainer = new ResultContainer();
            //resultContainer.Initialize(this.gameObject);

            //Time.timeScale = 0;

            //  リザルト用曲を再生
            AudioManager.Instance.PlayBGM(AudioName.BGM_RESULT01);

            if (resultContainer.clearFlag)
            {
                //  クリアジングルを再生
                AudioManager.Instance.PlaySE(AudioName.SE_GAMECLEAR01,this.gameObject.transform.position);
            }
            else
            {
                //  ゲームオーバージングルを再生
                AudioManager.Instance.PlaySE(AudioName.SE_GAMEOVER01, this.gameObject.transform.position);
            }
        }

        void Start()
        {
            playTime.text = resultContainer.playTimer.GetTimeString();
            getMoney.text = resultContainer.getMoneyValue.ToString();
            lostEnergy.text = resultContainer.lostEnergyValue.ToString();
        }

        void Update()
        {
            playTime.text = resultContainer.playTimer.GetTimeString();
            getMoney.text = resultContainer.getMoneyValue.ToString();
            lostEnergy.text = resultContainer.lostEnergyValue.ToString();

            #region デバック用
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                resultContainer.PlayTimerStart();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                resultContainer.PlayTimerStop();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                resultContainer.getMoneyValue++;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                resultContainer.lostEnergyValue += 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ReturnPlayBase();
                ResetResultContainer();
            }
            #endregion
            //  各項目の評価
            #region 時間
            if (resultContainer.clearFlag)
            {
                if (resultContainer.playTimer.GetTimeSecond() < playTimeReferenceValue[0])
                {
                    playTimeRankImage.sprite = imageList[0];
                    playTimeAssessment = YUU;
                }
                else if (resultContainer.playTimer.GetTimeSecond() < playTimeReferenceValue[1])
                {
                    playTimeRankImage.sprite = imageList[1];
                    playTimeAssessment = RYOU;
                }
                else if (resultContainer.playTimer.GetTimeSecond() < playTimeReferenceValue[2])
                {
                    playTimeRankImage.sprite = imageList[2];
                    playTimeAssessment = KA;
                }
                else
                {
                    playTimeRankImage.sprite = imageList[3];
                    playTimeAssessment = FU;
                }
            }
            else
            {
                playTimeRankImage.sprite = imageList[3];
                playTimeAssessment = FU;
            }
            Debug.LogWarning(playTimeReferenceValue[0] + "," + playTimeReferenceValue[1] +"," +playTimeReferenceValue[2]);
            Debug.LogWarning("時間" + resultContainer.playTimer.Second + "評価"+playTimeAssessment);
            #endregion

            #region お金
            if (resultContainer.getMoneyValue >= getMoneyReferenceValue[2])
            {
                if (resultContainer.getMoneyValue >= getMoneyReferenceValue[1])
                {
                    if (resultContainer.getMoneyValue >= getMoneyReferenceValue[0])
                    {
                        getMoneyRankImage.sprite = imageList[0];
                        getMoneyAssessment = YUU;
                    }
                    else
                    {
                        getMoneyRankImage.sprite = imageList[1];
                        getMoneyAssessment =RYOU;
                    }
                }
                else
                {
                    getMoneyRankImage.sprite = imageList[2];
                    getMoneyAssessment = KA;
                }
            }
            else 
            {
                getMoneyRankImage.sprite = imageList[3];
                getMoneyAssessment = FU;
            }
            #endregion

            #region エネルギー
            if (resultContainer.lostEnergyValue < lostEnergyeReferenceValue[0])
            {
                lostEnergyRankImage.sprite = imageList[0];
                lostEnergyAssessment = YUU;
            }
            else if (resultContainer.lostEnergyValue < lostEnergyeReferenceValue[1])
            {
                lostEnergyRankImage.sprite = imageList[1];
                lostEnergyAssessment = RYOU;
            }
            else if (resultContainer.lostEnergyValue < lostEnergyeReferenceValue[2])
            {
                lostEnergyRankImage.sprite = imageList[2];
                lostEnergyAssessment = KA;
            }
            else
            {
                lostEnergyRankImage.sprite = imageList[3];
                lostEnergyAssessment = FU;
            }
            #endregion

            #region トータル
            var totalScore = playTimeAssessment + getMoneyAssessment + lostEnergyAssessment;

            if (totalScore > ASSESSMENT_MIN)
            {
                if (totalScore >= ASSESSMENT_MID)
                {
                    if (totalScore >= ASSESSMENT_MAX)
                    {
                        totalRankImage.sprite = totalImageList[0];
                    }
                    else
                    {
                        totalRankImage.sprite = totalImageList[1];
                    }
                }
                else
                {
                    totalRankImage.sprite = totalImageList[2];
                }
            }
            else
            {
                totalRankImage.sprite = totalImageList[3];
            }
            #endregion

            if (fadeFlag)
            {
                fadeCount -= Time.deltaTime;
            }

            if(fadeCount<0)
            {
                ReturnPlayBase();
                ResetResultContainer();
            }
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
        /// コンテナに情報を登録する
        /// </summary>
        /// <param name="aResultContainer"></param>
       public static void RgtrResultContainer(ResultContainer aResultContainer)
        {
            deliveryResultContainer = aResultContainer;

            Debug.Log("リザルトコンテナを登録しました");

            Debug.Log("プレイ時間" + deliveryResultContainer.playTimer.GetTimeString());
        }

        /// <summary>
        /// 拠点に戻る
        /// </summary>
        public void ReturnPlayBase()
        {
            PlaySceneManager.SetStageTransition(StageTransition.ReturnPlayBase);

            sceneTransitionManager.RevocationScene(SceneName.ResultScene);
            //  コンテナの情報をリセット
            ResetResultContainer();

            Time.timeScale = 1;
        }

        /// <summary>
        /// リザルトコンテナの情報をリセット
        /// </summary>
        public void ResetResultContainer()
        {
            Debug.Log("データのリセットを呼び出し");

            //  トータルの情報に登録
            resultContainer.oldTotalPlayTime += resultContainer.playTimer.GetTimeFloat();
            resultContainer.totalGetMoneyValue += resultContainer.getMoneyValue;
            resultContainer.totalLostEnergyValue += resultContainer.lostEnergyValue;

            //  データのリセット
            resultContainer.PlayTimerReset();
            resultContainer.getMoneyValue = 0;
            resultContainer.lostEnergyValue = 0;
            resultContainer.clearFlag = false;

            Debug.Log("データのリセット" + resultContainer.playTimer.GetTimeString());
        }

        /// <summary>
        /// フェードの開始
        /// </summary>
        public void FadeStart()
        {
            SteamVR_FadeEx.Start(Color.black, 1.0f);
            fadeFlag = true;
        }
    }
}