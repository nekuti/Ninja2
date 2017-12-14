using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public class ShopPossessionMoney : MonoBehaviour
    {
        //  所持金を表示するテキスト
        private Text shopPossessionMoney;
        //  所持金の表示限界
        private const int DISPLAYLIMIT = 9999;

        // Use this for initialization
        void Start()
        {
            var money = PlaySceneManager.GetPossessionMoney();

            //  テキストを取得
            shopPossessionMoney = this.gameObject.GetComponent<Text>();

            //  所持金が表示限界を超えた場合は表示限界で止める
            if (money <= DISPLAYLIMIT)
            {
                //  所持金を適応
                shopPossessionMoney.text = PlaySceneManager.GetPossessionMoney().ToString() + "両";
            }
            else
            {
                shopPossessionMoney.text = DISPLAYLIMIT + "両";
            }
        }
    }
}