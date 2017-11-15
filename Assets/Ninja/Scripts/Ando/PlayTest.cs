using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class PlayTest : SceneBace
    {
        //  ポーズ中かどうか
        public bool PauseFlag = false;

        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.PlayTest;

            //  シーン遷移スクリプトを追加
            RgtrSceneTransition(GetComponent<SceneTransitionManager>());
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