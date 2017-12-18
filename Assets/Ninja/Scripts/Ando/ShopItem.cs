using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ShopItem : ShopMenu
    {
        //  選択されたアイテム
        static Item selectItem;

        /// <summary>
        /// その他のボタンが押されたときの処理
        /// </summary>
        public override void SelectOther()
        {
            base.SelectOther();
        }

        /// <summary>
        /// 選択されたアイテムを設定
        /// </summary>
        /// <param name="aSelectItem">アイテムの名称</param>
        public static void SetSelectItem(Item aSelectItem)
        {
            selectItem = aSelectItem;
        }

        /// <summary>
        /// 選択されたアイテムを取得
        /// </summary>
        /// <param name="aSelectItem">アイテムの名称</param>
        public static Item GetSelectItem()
        {
            return selectItem;
        }
    }
}
