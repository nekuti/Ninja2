using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class StageTest1 : StageBace
    {
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                AddLiteResult();
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
