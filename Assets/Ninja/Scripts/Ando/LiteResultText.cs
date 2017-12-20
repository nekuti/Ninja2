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

        GameObject player;


        // Use this for initialization
        void Start()
        {
            player = GameObject.Find("[Player]");

        }

        // Update is called once per frame
        void Update()
        {
            this.gameObject.transform.position = player.transform.position + new Vector3(0, 1, 2.0f);

            playTimeValue.text =resultContainer.playTimer.GetTimeString();

            getMoneyValue.text = resultContainer.getMoneyValue.ToString();

            lostEnergyValue.text = resultContainer.lostEnergyValue.ToString();

            if (textChangeFlag)
            {
                message.text = "クリアおめでとう！";
                operation.text = "トリガー → 次のステージへ \nグリップボタン → タイトル";
            }
            else
            {
                message.text = "GameOver";
                operation.text = "グリップボタン → タイトル";
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
