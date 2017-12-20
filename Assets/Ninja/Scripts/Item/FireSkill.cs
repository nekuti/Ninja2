using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火遁アイテムのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/19
/// </summary>
namespace Kojima
{
    public class FireSkill : Item<ItemAttackDataTable>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// アイテムが拾われた時の処理
        /// </summary>
        public override void PickUpItem()
        {
            Debug.Log(dataTable.itemName + "が拾われた");

            // 取得金額をPlaySceneManagerに登録
            if (Ando.PlaySceneManager.CheckEmpty())
            {
                Ando.PlaySceneManager.AddPossessionFireSkill(itemCount);
            }
            // オブジェクトを削除
            Destroy(this.gameObject);
        }

        #endregion
    }
}
