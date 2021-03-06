﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ResultTest : SceneBace
    {
        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.ResultTest;

            //  次に遷移するシーンを設定
            nextScene = SceneName.TitleTest;

            //  シーン遷移スクリプトを追加
            RgtrSceneTransitionManager(GetComponent<SceneTransitionManager>());
        }

        protected override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                sceneTransitionManager.ChangeSceneSingle(nextScene);
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