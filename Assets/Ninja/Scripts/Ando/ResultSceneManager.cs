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
        private SceneName nextScene = SceneName.PlayScene;

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

        //  リザルトシーンで使用する情報
        private static ResultContainer resultContainer;
        int i = 0;

        // Use this for initialization
        void Start()
        {
            //  インスペクターで設定するもの以外を設定
            playTimeAssessment = 0;
            getMoneyAssessment = 0;
            lostEnergyAssessment = 0;

            //  プレイヤーの操作をメニュー用に切り替え
            PlaySceneManager.GetPlayer().ChangeHandState(Kojima.HandStateType.MenuSelect);

            //  階層の分岐
            switch (resultContainer.floorLevel)
            {
                case 1:
                    playTimeReferenceValue = resultAssessmentDataTable.level1PlayTime;
                    getMoneyReferenceValue = resultAssessmentDataTable.level1GetMoney;
                    lostEnergyeReferenceValue = resultAssessmentDataTable.level1LostEnergy;
                    break;
                case 2:
                    playTimeReferenceValue = resultAssessmentDataTable.level2PlayTime;
                    getMoneyReferenceValue = resultAssessmentDataTable.level2GetMoney;
                    lostEnergyeReferenceValue = resultAssessmentDataTable.level2LostEnergy;
                    break;
                case 3:
                    playTimeReferenceValue = resultAssessmentDataTable.level3PlayTime;
                    getMoneyReferenceValue = resultAssessmentDataTable.level3GetMoney;
                    lostEnergyeReferenceValue = resultAssessmentDataTable.level3LostEnergy;
                    break;
                default:
                    Debug.Log("リザルトシーン：階層の値がおかしいので評価項目を設定できませんでした。");
                    break;
            }

            //  テスト用に作成
            //resultContainer = new ResultContainer();
            //resultContainer.Initialize(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            playTime.text = resultContainer.totalPlayTimer.GetTimeString();
            getMoney.text = resultContainer.totalGetMoneyValue.ToString();
            lostEnergy.text = resultContainer.totalLostEnergyValue.ToString();

            /*デバック用////////////////////////////////////////*/
            if (Input.GetKeyDown(KeyCode.A))
            {
                resultContainer.TotalTimerStart();
                Debug.Log("A");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                resultContainer.TotalTimerStop();
                Debug.Log("B");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                resultContainer.totalGetMoneyValue++;
                Debug.Log("C");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                resultContainer.totalLostEnergyValue += 5;
                Debug.Log("D");
            }
            /*///////////////////////////////////////////////////*/

            //  各項目の評価
            #region 時間
            if (resultContainer.clearFlag)
            {
                if (resultContainer.totalPlayTimer.Second < playTimeReferenceValue[0])
                {
                    playTimeRankImage.sprite = imageList[0];
                    playTimeAssessment = YUU;
                }
                else if (resultContainer.totalPlayTimer.Second < playTimeReferenceValue[1])
                {
                    playTimeRankImage.sprite = imageList[1];
                    playTimeAssessment = RYOU;
                }
                else if (resultContainer.totalPlayTimer.Second < playTimeReferenceValue[2])
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
            #endregion

            #region お金
            if (resultContainer.totalGetMoneyValue >= getMoneyReferenceValue[2])
            {
                if (resultContainer.totalGetMoneyValue >= getMoneyReferenceValue[1])
                {
                    if (resultContainer.totalGetMoneyValue >= getMoneyReferenceValue[0])
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
            if (resultContainer.totalLostEnergyValue < lostEnergyeReferenceValue[0])
            {
                lostEnergyRankImage.sprite = imageList[0];
                lostEnergyAssessment = YUU;
            }
            else if (resultContainer.totalLostEnergyValue < lostEnergyeReferenceValue[1])
            {
                lostEnergyRankImage.sprite = imageList[1];
                lostEnergyAssessment = RYOU;
            }
            else if (resultContainer.totalLostEnergyValue < lostEnergyeReferenceValue[2])
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
            resultContainer = aResultContainer;
        }

        /// <summary>
        /// 拠点に戻る
        /// </summary>
        public void ReturnPlayBase()
        {
            PlaySceneManager.SetStageTransition(StageTransition.ReturnPlayBase);

            sceneTransitionManager.RevocationScene(SceneName.ResultScene);
        }
    }
}