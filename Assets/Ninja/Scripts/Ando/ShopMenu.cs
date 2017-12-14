using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ShopMenu : MonoBehaviour
    {

        //  選択されたときのウィンドウオブジェクト
        [SerializeField]
        protected GameObject select;
        //  選択されなかったときのウィンドウオブジェクト
        [SerializeField]
        protected GameObject noSelect;
        //  選択されたときのフレーム
        [SerializeField]
        protected GameObject menuFlame;

        //  選択時に最初に表示されるメニュー
        [SerializeField]
        protected GameObject firstMenu;

        //  何か選択されたときに表示されるメニュー
        [SerializeField]
        protected GameObject otherMenus;

        /// <summary>
        /// 選択されたときの処理
        /// </summary>
        public void Select()
        {
            select.SetActive(true);
            noSelect.SetActive(false);
            menuFlame.SetActive(true);
            firstMenu.SetActive(true);
            otherMenus.SetActive(false);
        }

        /// <summary>
        /// 選択されなかったときの処理
        /// </summary>
        public void NoSelect()
        {
            select.SetActive(false);
            noSelect.SetActive(true);
            menuFlame.SetActive(false);
            firstMenu.SetActive(false);

            otherMenus.SetActive(false);
        }

        /// <summary>
        /// その他のボタンが押されたときの処理
        /// </summary>
        public virtual void SelectOther()
        {
            firstMenu.SetActive(false);
            otherMenus.SetActive(true);
        }

        /// <summary>
        /// 戻るボタンが押されたとき
        /// </summary>
        public virtual void SelectBack()
        {
            firstMenu.SetActive(true);
            otherMenus.SetActive(false);
        }
    }
}
