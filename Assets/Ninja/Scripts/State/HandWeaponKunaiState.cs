using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クナイの攻撃ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/08
/// </summary>
namespace Kojima
{
    public class HandWeaponKunaiState : HandWeaponState
    {
        #region メンバ変数

        private bool attackedFlg;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public HandWeaponKunaiState(Hand owner) : base(owner) { weaponType = WeaponType.Kunai; }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Debug.Log("WeaponKunaiに設定");

            attackedFlg = false;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            // VIVEでの入力処理
            if (owner.trackdObject != null && owner.device != null)
            {
                float value = owner.device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
                if (value > 0.89f && !attackedFlg)
                {
                    // クナイを発射
                    Attack.Create(owner.WeaponData.WeaponPrefab, owner.shotPos.transform.position, owner.transform.position + owner.transform.forward, owner.WeaponData.Power, owner.tag);
                    attackedFlg = true;
                }
                else
                {
                    attackedFlg = false;
                }
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