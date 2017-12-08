using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando {
    //  プレイヤーの拠点
    public class PlayerBase : StageBace
    {
        private void Awake()
        {
            //  シーン名を入れる
            myStage = StageName.PlayerBase;

            //  プレイシーンマネージャを取得
            RgtrPlaySceneManager(GetComponent<PlaySceneManager>());

            //  プレイシーンマネージャを登録
            PlayerBaseManager.RgtrPlaySceneManager(playSceneManager);

            goalFlag = false;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
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
