using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public class LiteResultText : MonoBehaviour
    {
        private static ResultContainer resultContainer;

        [SerializeField]
        private Text playTimeValue;
        [SerializeField]
        private Text getMoneyValue;
        [SerializeField]
        private Text lostEnergyValue;

        [SerializeField]
        private Text text;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            playTimeValue.text =resultContainer.playTime;

            getMoneyValue.text = resultContainer.getMoneyValue.ToString();

            lostEnergyValue.text = resultContainer.lostEnergyValue.ToString();

        }

        public static void SetLiteResult(ResultContainer aResultContainer)
        {
            resultContainer = aResultContainer;
        }

    }
}
