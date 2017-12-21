using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{

public class Stage001 : StageBace {

        // Use this for initialization
        private void Awake()
        {
            //  シーン名を入れる
            myStage = StageName.Stage001;

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
