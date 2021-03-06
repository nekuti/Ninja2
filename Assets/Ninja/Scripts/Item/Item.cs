﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Itemのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/19
/// </summary>
namespace Kojima
{
    public abstract class Item<T> : ItemBase
        where T : Ando.ItemDataTable
    {
        #region メンバ変数
        [SerializeField,Tooltip("アイテムのデータ")]
        protected T dataTable;

        #endregion

        #region プロパティ
        public T DataTable { get { return dataTable; } }
        #endregion

        #region メソッド


        /// <summary>
        /// アイテムが拾われた時の処理
        /// </summary>
        public abstract void PickUpItem();

        /// <summary>
        /// ぶつかった時の処理
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            // プレイヤーが触った時アイテムが拾われる
            if (other.CompareTag(TagName.Player) || other.CompareTag(TagName.Hand))
            {
                PickUpItem();

                // SEを再生
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_ITEM_PICKUP, transform.position);
            }
        }

        #endregion
    }
}