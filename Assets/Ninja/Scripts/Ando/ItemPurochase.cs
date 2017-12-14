using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public class ItemPurochase : MonoBehaviour
    {
        //  アイテムの名前
        [SerializeField]
        private Text itemName;
        //  アイテムの説明
        [SerializeField]
        private Text itemExplanation;
        //  アイテムの購入個数
        [SerializeField]
        private Text itemNum;
        //  アイテムの値段
        [SerializeField]
        private Text itemPrice;

        //  最初のアイテム購入個数
        [SerializeField]
        private const int FIRSTITEMBUYNUM = 1;

        //  アイテムの購入個数
        [SerializeField]
        private int itemBuyNum = FIRSTITEMBUYNUM;

        //  選択されたアイテム
        [SerializeField]
        private Item item;

        //  購入成功時の文
        [SerializeField]
        private GameObject buyAccept;
        //  購入不可時の警告文
        [SerializeField]
        private GameObject buyWarning;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //  nullチェック
            if (ShopItem.GetSelectItem() == null)
            {
                return;
            }

            //  選択されたアイテムを設定
            item = ShopItem.GetSelectItem();

            //  固定値を入力
            itemName.text = item.GetItemName();
            itemExplanation.text = item.GetItemExplanation();
            itemNum.text = itemBuyNum + "個";
            itemPrice.text = (itemBuyNum * item.GetItemPrice()).ToString() + "両";
        }

        /// <summary>
        /// 変動する値を更新
        /// </summary>
        private void DisplayTextUpdate()
        {
            //  変動する値を更新
            itemNum.text = itemBuyNum + "個";
            itemPrice.text = (itemBuyNum * item.GetItemPrice()).ToString() + "両";
        }

        /// <summary>
        /// アイテムの購入数を加算
        /// </summary>
        public void AddItemBuyNum()
        {
            //  所持数上限を超えてないか確認
            if (itemBuyNum <= (item.GetItemMaxPossessionNum() - item.GetItemPossessionNum()))
            {
                itemBuyNum++;

                //  表示情報を更新
                DisplayTextUpdate();
            }
        }

        /// <summary>
        /// アイテムの購入数を減算
        /// </summary>
        public void SubItemBuyNum()
        {
            //  購入数が1以下になっていないか確認
            if (itemBuyNum > 1)
            {
                itemBuyNum--;

                //  表示情報を更新
                DisplayTextUpdate();
            }
        }

        /// <summary>
        /// アイテムを購入
        /// </summary>
        public void BuyItem()
        {
            //  減少する金額
            var subMoney = itemBuyNum * item.GetItemPrice();

            //  所持金を超えていないか確認
            if(PlaySceneManager.GetPossessionMoney() >= subMoney)
            {
                Debug.Log(subMoney + "を支払い");
                PlaySceneManager.SubPossessionMoney(subMoney);

                Debug.Log(itemBuyNum + "個" + item.GetItemName() + "を購入");
                item.AddPossessionItem(itemBuyNum);              

                //  購入成功時の文を表示
                buyAccept.SetActive(true);          
            }
            else
            {
                //  購入不可時の警告文を表示
                buyWarning.SetActive(true);
            }
        }

        /// <summary>
        /// 購入個数を初期化
        /// </summary>
        public void InitItemBuyNum()
        {
            //  購入個数を初期化
            itemBuyNum = FIRSTITEMBUYNUM;
        }

    }
}
