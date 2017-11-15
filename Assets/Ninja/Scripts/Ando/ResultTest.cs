using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ResultTest : SceneBace
    {
        [SerializeField]
        private SceneName nextScene = SceneName.TitleTest;

        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.ResultTest;

            //  シーン遷移スクリプトを追加
            RgtrSceneTransition(GetComponent<SceneTransitionManager>());
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