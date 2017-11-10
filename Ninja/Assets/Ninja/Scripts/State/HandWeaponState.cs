using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HandWeaponStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/08
/// </summary>
namespace Kojima
{
    public abstract class HandWeaponState : State<Hand>
    {
        #region メンバ変数

        public WeaponType weaponType;

        public WeaponDataTable weaponData;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public HandWeaponState(Hand owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            // 武器データを取得
            weaponData = owner.WeaponData;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
        }

        #endregion
    }
}