using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HandNormalWireStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/07
/// </summary>
namespace Kojima
{
    [System.Serializable]
    public class HandNormalWireState : State<Hand>
    {
        #region メンバ変数

        public float wireSpeed = 10f;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public HandNormalWireState(Hand owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("WireNomalに設定");
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            base.Execute();
            if (Input.GetButtonDown("Fire2"))
            {
                // 武器を装備
                owner.EquipWeapon();
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