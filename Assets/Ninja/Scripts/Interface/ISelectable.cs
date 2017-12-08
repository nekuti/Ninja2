using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プライヤーが選択するボタンのインターフェース
/// </summary>
namespace Kojima
{
    public interface ISelectable
    {
        #region メソッド

        /// <summary>
        /// レイが当たった
        /// </summary>
        void HitRayObject();

        /// <summary>
        /// レイが外れた
        /// </summary>
        void OutRayObject();

        /// <summary>
        /// 決定がされた
        /// </summary>
        void SelectObject();

        #endregion
    }
}