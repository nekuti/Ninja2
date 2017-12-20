using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ando
{
    public abstract class Item : MonoBehaviour
    {
        //  アイテムのデータテーブル
        protected ItemDataTable itemData;

        //  アイテムの名前
        [SerializeField]
        protected Text itemName;
        //  アイテムの値段
        [SerializeField]
        protected Text itemPrice;
        //  アイテムの所持数
        [SerializeField]
        protected Text itemPossession;

        /// <summary>
        /// アイテムの名前を取得
        /// </summary>
        /// <returns></returns>
        public abstract string GetItemName();

        /// <summary>
        /// アイテムの説明を取得
        /// </summary>
        /// <returns></returns>
        public abstract string GetItemExplanation();

        /// <summary>
        /// アイテムの金額を取得
        /// </summary>
        /// <returns></returns>
        public abstract int GetItemPrice();

        /// <summary>
        /// アイテムの所持数上限を取得
        /// </summary>
        /// <returns></returns>
        public abstract int GetItemMaxPossessionNum();

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
