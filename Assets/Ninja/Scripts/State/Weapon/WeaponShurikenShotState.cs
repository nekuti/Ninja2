﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スリケンの攻撃ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/07
/// </summary>
namespace Kojima
{
    public class WeaponShurikenShotState : State<WeaponControl>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WeaponShurikenShotState(WeaponControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("WeaponShurikenの攻撃");
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