using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シュリケンの攻撃ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/08
/// </summary>
namespace Kojima
{
    public class HandWeaponShurikenState : HandWeaponState
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public HandWeaponShurikenState(Hand owner) : base(owner) { weaponType = WeaponType.Shuriken; }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Debug.Log("WeaponShurikenに設定");
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            if (Input.GetButtonDown("Fire2"))
            {
                // ワイヤーを装備
                owner.EquipWire();
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        #endregion
    }
}