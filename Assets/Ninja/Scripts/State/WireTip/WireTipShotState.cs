using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireTipShotStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/12
/// </summary>
namespace Kojima
{
    public class WireTipShotState : State<WireTip>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WireTipShotState(WireTip owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            if(owner.myRigidbody != null)
            {
                // ShotSpeed分の力を加えてワイヤーを発射する
                owner.myRigidbody.AddForce(owner.ShotDirection.normalized * owner.Controller.MyHand.WireData.ShotSpeed, ForceMode.VelocityChange);
            }
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // Wireの射程を超えた場合
            if(Vector3.Distance(owner.transform.position,owner.Controller.transform.position) > owner.Controller.MyHand.WireData.ShotRange)
            {
                // 巻き取りステートへ移行
                owner.ReturnWireTip();
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