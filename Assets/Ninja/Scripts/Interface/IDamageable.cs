using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージを受けるオブジェクトのインターフェイス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/09
/// </summary>
namespace Kojima
{
    public interface IDamageable
    {
        #region メソッド

        /// <summary>
        /// 攻撃を受ける
        /// </summary>
        /// <param name="aDamage">攻撃のダメージ量</param>
        void TakeAttack(int aAttack);

        #endregion
    }
}