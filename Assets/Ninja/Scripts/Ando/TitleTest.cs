using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class TitleTest : SceneBace
    {
        private bool animeSwitch = false;
        private void Awake()
        {
            //  シーン名を入れる
            myScene = SceneName.TitleTest;

            //  次に遷移するシーンを設定
            nextScene = SceneName.PlayTest;
            
            //  シーン遷移スクリプトを追加
            RgtrSceneTransitionManager(GetComponent<SceneTransitionManager>());
        }

        protected override void Update()
        {
            if (TitleControl.GetGameStart())
            {
                if (!animeSwitch)
                {
                    DoorAnime.SetDoorAnimeState(DoorAnimeState.Start);
                    animeSwitch = true;
                }
                if (DoorAnime.GetDoorAnimeState() == DoorAnimeState.End)
                {
                    sceneTransitionManager.ChangeSceneSingle(nextScene);
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