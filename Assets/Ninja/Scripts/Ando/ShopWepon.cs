using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ShopWepon : ShopMenu
    {
        //  選択された武器
        static Weapon selectWeapon;

        /// <summary>
        /// その他のボタンが押されたときの処理
        /// </summary>
        public override void SelectOther()
        {
            base.SelectOther();
        }

        /// <summary>
        /// 選択された武器を設定
        /// </summary>
        /// <param name="aSelectItem">アイテムの名称</param>
        public static void SetSelectWeapon(Weapon aSelectWeapon)
        {
            selectWeapon = aSelectWeapon;
        }

        /// <summary>
        /// 選択された武器を取得
        /// </summary>
        /// <param name="aSelectItem">アイテムの名称</param>
        public static Weapon GetSelectWeapon()
        {
            return selectWeapon;
        }

    }
}
