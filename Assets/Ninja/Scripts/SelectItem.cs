using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SelectItemのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/20
/// </summary>
namespace Kojima
{
    enum ItemType
    {
        Onigiri,
        Katon,
    }

    public class SelectItem : MonoBehaviour
    {
        #region メンバ変数

        [SerializeField,Tooltip("アイテムの種類")]
        private ItemType itemType;

        #endregion

        #region メソッド

        /// <summary>
        /// アイテムを使用する
        /// </summary>
        public void UseItem(Player aPlayer)
        {
            switch(itemType)
            {
                // オニギリを使用する
                case ItemType.Onigiri:
                    aPlayer.UseOnigiri();
                    break;
                // カトンを使用する
                case ItemType.Katon:
                    aPlayer.UseKaton();
                    break;
            }
        }

        #endregion
    }
}