using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スリケンの待機ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/12
/// </summary>
namespace Kojima
{
    public class WeaponShurikenWaitState : State<WeaponControl>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WeaponShurikenWaitState(WeaponControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("WeaponShurikenに設定");
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // 攻撃ステートへ移行
            if (Input.GetButtonDown("Fire1"))
            {
                owner.ChangeState(WeaponStateType.Shot);
            }
            if (InputDevice.PressDown(ButtonType.Touchpad, owner.MyHand.HandType))
            {
                owner.ChangeState(WeaponStateType.Shot);
            }
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