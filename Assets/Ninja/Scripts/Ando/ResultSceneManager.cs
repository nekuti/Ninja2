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

        //  プレイ時間の描画テキスト
        [SerializeField]
        private Text playTime;

        [SerializeField]
        private List<int> referenceValue;

        //  評価を表示するイメージ
        [SerializeField]
        private Image rankImage;
        //  評価イメージ
        [SerializeField]
        private List<Sprite> imageList;

        //  次に遷移するシーン
        [SerializeField]
        private SceneName nextScene = SceneName.PlayScene;

        //  リザルトシーンで使用する情報
        private static ResultContainer resultContainer;
        int i = 0;
        // Use this for initialization
        void Start()
        {
            //resultContainer = new ResultContainer();
            //resultContainer.Initialize(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            playTime.text = resultContainer.totalPlayTimer.GetTimeString();
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


            if (resultContainer.totalPlayTimer.Second < referenceValue[0])
            {
                rankImage.sprite = imageList[0];
            }
            else if (resultContainer.totalPlayTimer.Second < referenceValue[1])
            {
                rankImage.sprite = imageList[1];
            }
            else if (resultContainer.totalPlayTimer.Second < referenceValue[2])
            {
                rankImage.sprite = imageList[2];
            }
            else
            {
                rankImage.sprite = imageList[3];
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