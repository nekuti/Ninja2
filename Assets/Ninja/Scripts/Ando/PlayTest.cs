using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class PlayTest : SceneBace
    {
        //  ポーズ中かどうか
        private bool PauseFlag = false;

        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.PlayTest;

            //  シーン遷移スクリプトを追加
            RgtrSceneTransition(GetComponent<SceneTransitionManager>());
        }

        protected override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!PauseFlag)
                {
                    sceneTransitionManager.ChangeSceneSingle<ResultTest>();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (!PauseFlag)
                {
                    sceneTransitionManager.ChangeSceneAdd<PauseTest>();
                    PauseFlag = true;
                    Time.timeScale = 0.0f;
                }
            }
            if (PauseFlag)
            {
                if (Input.GetMouseButtonDown(2))
                {
                    PauseFlag = false;
                    Time.timeScale = 1.0f;
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