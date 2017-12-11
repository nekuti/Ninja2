using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireHookStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
/// </summary>
namespace Kojima
{
    public class WireHookState : State<WireControl>
    {
        #region メンバ変数

        private Vector3 hitHandPos;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WireHookState(WireControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("Wireの引っかかり");

            hitHandPos = owner.transform.position;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // トリガーを離す
            if (InputDevice.TouchUp(ButtonType.Trigger, owner.MyHand.HandType))
            {
                // ワイヤー巻き取りへ移行
                owner.ChangeState(WireStateType.Return);
            }
            if(Input.GetButtonUp("Fire2"))
            {
                // ワイヤー巻き取りへ移行
                owner.ChangeState(WireStateType.Return);
            }

            // 力を加える割合
            float percent = (hitHandPos - owner.transform.position).magnitude;
            if (!InputDevice.IsDeviceRegisterd(owner.MyHand.HandType)) percent = 1f;
            if (percent > 1) percent = 1f;

            Vector3 vec = owner.wireTip.transform.position - owner.transform.position;
            // プレイヤーを飛ばす
            owner.MyHand.Owner.PullPlayer(vec.normalized * owner.MyHand.WireData.PullSpeed * percent, owner.MyHand.WireData.PullSpeed);
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
