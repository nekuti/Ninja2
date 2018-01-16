using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class MenuSceneManager : MonoBehaviour
    {
        //  シーン遷移マネージャ
        public static SceneTransitionManager sceneTransitionManager;

        //  プレイシーンマネージャ
        public static PlaySceneManager playSceneManager;

        //  次に遷移するシーン
        [SerializeField]
        private SceneName nextScene = SceneName.None;

        //  シーン開始時のプレイヤーの初期位置
        [SerializeField]
        private GameObject startPos;

        //  プレイヤーの元の位置
        private static Vector3 oldPlayerPos;

        void Start()
        {
            Kojima.Player player = FindObjectOfType<Kojima.Player>();

            if (player != null)
            {
                oldPlayerPos = player.transform.position;
                player.ResetPosition(startPos.transform.position);
            }
            else
            {
                Debug.Log("プレイヤーがいません");
            }
        }

        void Update()
        {

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
        /// プレイシーンマネージャの登録
        /// </summary>
        /// <param name="aSceneTransitionManager"></param>
        public static void RgtrPlaySceneManager(PlaySceneManager aPlaySceneManager)
        {
            if (aPlaySceneManager != null)
            {
                playSceneManager = aPlaySceneManager;
            }
            else
            {
                Debug.Log("プレイシーンマネージャがありません");
            }
        }

        /// <summary>
        /// プレイヤーの拠点へ戻る
        /// </summary>
        public void ReturnPlayerBace()
        {
            if (sceneTransitionManager.SearchScene(SceneName.PlayScene))
            {
                PlaySceneManager.SetStageTransition(StageTransition.ReturnPlayBase);
            }
            else
            {
                Debug.Log("プレイシーンマネージャがありません");
            }
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        public void GameEnd()
        {
            nextScene = SceneName.End;

            Debug.Log("遷移シーンを" + nextScene + "に設定");

            sceneTransitionManager.ChangeSceneSingle(nextScene);
        }

        //  シーンを削除
        public static void RevocationScene()
        {
            Kojima.Player player = FindObjectOfType<Kojima.Player>();

            if (player != null)
            {
                player.ResetPosition(oldPlayerPos);
            }
            else
            {
                Debug.Log("プレイヤーがいません");
            }

            sceneTransitionManager.RevocationScene(SceneName.MenuScene);
        }
    }
}