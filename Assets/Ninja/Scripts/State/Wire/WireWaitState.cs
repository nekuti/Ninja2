using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireWaitStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
/// </summary>
namespace Kojima
{
    public class WireWaitState : State<WireControl>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WireWaitState(WireControl owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("Wireの待機");
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // トリガーのクリック
            if(InputDevice.ClickDownTrriger(owner.MyHand.HandType))
            {
                // ワイヤー射出へ移行
                owner.ChangeState(WireStateType.Shot);
            }
            if(Input.GetButtonDown("Fire2"))
            {
                // ワイヤー射出へ移行
                owner.ChangeState(WireStateType.Shot);
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
