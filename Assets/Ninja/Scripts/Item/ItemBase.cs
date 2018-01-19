using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Itemのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/19
/// </summary>
namespace Kojima
{
    public class ItemBase : MonoBehaviour
    {
        #region メンバ変数

        [SerializeField, Tooltip("アイテムの個数")]
        protected int itemCount = 1;

        #endregion

        #region プロパティ
        public int ItemCount { get { return itemCount; } }
        #endregion

        #region メソッド

        /// <summary>
        /// アイテム個数の設定
        /// </summary>
        /// <param name="aNum"></param>
        public void SetItemCount(int aNum)
        {
            itemCount = aNum;
        }

        #endregion
    }
}