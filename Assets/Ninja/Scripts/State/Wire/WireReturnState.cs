using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireReturnStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
/// </summary>
namespace Kojima
{
    public class WireReturnState : State<WireControl>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WireReturnState(WireControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("Wireの巻き取り");

            // ワイヤーチップに巻き取り指示
            owner.wireTip.ReturnWireTip();
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // 振動させる
            InputDevice.Pulse(250, owner.MyHand.HandType);
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
