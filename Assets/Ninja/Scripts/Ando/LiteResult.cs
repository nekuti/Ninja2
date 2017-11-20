using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{

    public class LiteResult : SceneBace
    {
        [SerializeField]
        private ResultContainer resultContainer;

        [SerializeField]
        private Text playTimeValue;

        private void Awake()
        {
            #region テストコード
            //resultContainer = new ResultContainer();

            //this.gameObject.AddComponent<Timer>();

            //resultContainer.playTime = this.GetComponent<Timer>();
            #endregion
        }

        void Start()
        {
            //playTimeValue.text = resultContainer.playTime.GetTimeString();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetResultContainer(ResultContainer aResultContainer)
        {
            resultContainer = aResultContainer;
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