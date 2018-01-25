using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireShotStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
/// </summary>
namespace Kojima
{
    public class WireShotState : State<WireControl>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WireShotState(WireControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("Wireの射出");

            // ワイヤー発射のエネルギーを消費
            owner.MyHand.MyPlayer.ExpenseEnergy(owner.MyHand.WireData.EnergyCost);
            // ワイヤーを生成
            owner.wireTip = WireTip.Create(owner.MyHand.WireData, owner, owner.transform.rotation * Vector3.forward);
            // コントローラーを振動させる
            InputDevice.Pulse(3000, owner.MyHand.HandType);

            // SEを再生
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_WIRE_SHOT, owner.transform.position);

        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // トリガーを離す
            if(InputDevice.TouchUp(ButtonType.Trigger,owner.MyHand.HandType))
            {
                // ワイヤー巻き取りへ移行
                owner.ChangeState(WireStateType.Return);
            }
            if(Input.GetButtonUp("Fire2"))
            {
                // ワイヤー巻き取りへ移行
                owner.ChangeState(WireStateType.Return);
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
