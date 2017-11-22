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

        //  学園祭用実装
        private static bool textChangeFlag = false;
        [SerializeField]
        private Text message;
        [SerializeField]
        private Text operation;


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

            if (textChangeFlag)
            {
                message.text = "クリアおめでとう！";
                operation.text = "トリガー → タイトル \nグリップボタン → 次のステージへ";
            }
            else
            {
                message.text = "GameOver";
                operation.text = "トリガー → タイトル";
            }
        }

        public static void SetTextChangeFlag(bool aFlag)
        {
            textChangeFlag = aFlag;
        }

        public static void SetLiteResult(ResultContainer aResultContainer)
        {
            resultContainer = aResultContainer;
        }

    }
}
