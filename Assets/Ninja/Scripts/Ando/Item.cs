using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public abstract class Item : MonoBehaviour
    {

        //  アイテムのデータテーブルを保存
        [SerializeField]
        protected Ando.ItemDataTable itemData;

        //  アイテムの名前
        [SerializeField]
        protected Text itemName;
        //  アイテムの値段
        [SerializeField]
        protected Text itemPrice;
        //  アイテムの所持数
        [SerializeField]
        protected Text itemPossession;

        protected virtual void Start()
        {
            //  テキストにデータを挿入
            itemName.text = GetItemName();
            itemPrice.text = GetItemPrice().ToString() + "両";
            itemPossession.text = "エラー";
        }

        /// <summary>
        /// アイテムの名前を取得
        /// </summary>
        /// <returns></returns>
        public string GetItemName()
        {
            return itemData.itemName;
        }

        /// <summary>
        /// アイテムの説明を取得
        /// </summary>
        /// <returns></returns>
        public string GetItemExplanation()
        {
            return itemData.explanation;
        }

        /// <summary>
        /// アイテムの金額を取得
        /// </summary>
        /// <returns></returns>
        public int GetItemPrice()
        {
            return itemData.price;
        }

        /// <summary>
        /// アイテムの所持数上限を取得
        /// </summary>
        /// <returns></returns>
        public int GetItemMaxPossessionNum()
        {
            return itemData.maxPossessionNum;
        }

        /// <summary>
        /// アイテムの所持数を取得
        /// </summary>
        /// <returns></returns>
        public abstract int GetItemPossessionNum();

        /// <summary>
        /// アイテムの所持数を加算
        /// </summary>
        public abstract void AddPossessionItem(int anAddNum);

        /// <summary>
        /// 選択されたアイテムを設定
        /// </summary>
        public abstract void SetSelectItem();
    }
}
