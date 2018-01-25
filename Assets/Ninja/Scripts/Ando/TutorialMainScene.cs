using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class TutorialMainScene : SceneBace
    {
        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.TutorialMainScene;

            //  シーン遷移スクリプトを追加
            RgtrSceneTransitionManager(GetComponent<SceneTransitionManager>());
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