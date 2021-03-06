﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ResultScene : SceneBace
    {
        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.ResultScene;

            //  シーン遷移スクリプトを追加
            RgtrSceneTransitionManager(GetComponent<SceneTransitionManager>());

            //  リザルトシーンマネージャにシーン遷移マネージャを登録
            ResultSceneManager.RgtrSceneTransitionManager(sceneTransitionManager);
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