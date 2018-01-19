using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class MenuScene : SceneBace
    {
        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.MenuScene;

            //  シーン遷移スクリプトを取得
            sceneTransitionManager = GetComponent<SceneTransitionManager>();

            //  シーン遷移スクリプトを追加
            RgtrSceneTransitionManager(sceneTransitionManager);

            //  リザルトシーンマネージャにシーン遷移マネージャを登録
            MenuSceneManager.RgtrSceneTransitionManager(sceneTransitionManager);

            //  プレイシーンがあるか確認
            if (sceneTransitionManager.SearchScene(SceneName.PlayScene))
            {
                var playSceneManager = FindObjectOfType<PlaySceneManager>();

                if (playSceneManager != null)
                {
                    MenuSceneManager.RgtrPlaySceneManager(playSceneManager);
                }
            }
        }

        /// <summary>
        /// 継承先の型を取得
        /// </summary>
        /// <returns></returns>
        public override System.Type GetTypeInheritance()
        {
            return this.GetType();
        }

    }
}