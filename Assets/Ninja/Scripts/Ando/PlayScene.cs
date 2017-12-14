using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class PlayScene : SceneBace
    {
        //  ポーズ中かどうか
        public bool PauseFlag = false;

        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.PlayScene;

            //  シーン遷移スクリプトを取得
            RgtrSceneTransitionManager(GetComponent<SceneTransitionManager>());
            
            //  プレイシーンマネージャにシーン遷移マネージャを登録
            PlaySceneManager.RgtrSceneTransitionManager(sceneTransitionManager);
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