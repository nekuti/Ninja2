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

        //　選択可能な拠点に戻るボタン
        [SerializeField]
        private GameObject selectReturnButton;
        //  選択不可な拠点に戻るボタン
        [SerializeField]
        private GameObject noSelectReturnButton;

        //  プレイヤーの元の位置
        private static Vector3 oldPlayerPos;
        //  プレイヤーの元のHandState
        private static Kojima.HandStateType oldHandState;

        void Start()
        {
            //  プレイヤーを検索
            Kojima.Player player = FindObjectOfType<Kojima.Player>();

            if (player != null)
            {
                //  プレイヤーの座標を保存
                oldPlayerPos = player.transform.position;
                Debug.Log("保存された座標" + oldPlayerPos);

                //  ハンドステートを保存
                if (player.RightHand.IsCurrentState(Kojima.HandStateType.MenuSelect))
                {
                    oldHandState = Kojima.HandStateType.MenuSelect;
                }
                else
                {
                    oldHandState = Kojima.HandStateType.Play;
                }
                Debug.Log("保存されたHandState" + oldHandState);

                //  座標を更新
                player.ResetPosition(startPos.transform.position);
                Debug.Log("プレイヤー座標" + startPos.transform.position + "に設定");
                //  HandStateを更新
                player.ChangeHandState(Kojima.HandStateType.MenuSelect);
                Debug.Log("HandStateをMenuSelectへ");
            }
            else
            {
                Debug.Log("プレイヤーがいません");
            }

            //  プレイシーンマネージャがあるかで選択ボタンを変更
            if(playSceneManager == null)
            {
                selectReturnButton.SetActive(false);
                noSelectReturnButton.SetActive(true);
            }
            else
            {
                selectReturnButton.SetActive(true);
                noSelectReturnButton.SetActive(false);
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
                Debug.Log("プレイシーンマネージャを設定しました");
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
                player.ChangeHandState(oldHandState);
            }
            else
            {
                Debug.Log("プレイヤーがいません");
            }

            sceneTransitionManager.RevocationScene(SceneName.MenuScene);
        }
    }
}