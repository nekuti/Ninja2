using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireTipReturnStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/13
/// </summary>
namespace Kojima
{
    public class WireTipReturnState : State<WireTip>
    {
        #region メンバ変数

        private float maxSpeed;
        private float maxSqrSpeed;

        private float timer;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WireTipReturnState(WireTip owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            maxSpeed = owner.ownerWireState.wireData.ReturnSpeed;
            maxSqrSpeed = maxSpeed * maxSpeed;

            // 速度をリセット
            owner.myRigidbody.velocity = Vector3.zero;
            owner.myRigidbody.useGravity = true;
            timer = 0f;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // 少し時間を空けてから巻き取り開始
            if (timer > 0.3f)
            {
                Vector3 dire = (owner.ownerTransform.position - owner.transform.position);

                // 巻き取る
                owner.myRigidbody.AddForce(dire.normalized * (owner.ownerWireState.wireData.ReturnSpeed), ForceMode.Acceleration);

                if (owner.myRigidbody.velocity.sqrMagnitude > maxSqrSpeed)
                {
                    Vector3 vec = owner.myRigidbody.velocity - (owner.myRigidbody.velocity.normalized * maxSpeed);
                    owner.myRigidbody.velocity -= vec;
                }

                float percent = dire.magnitude / owner.ownerWireState.wireData.ShotRange;
                if (percent > 1f) percent = 1f;
                owner.myRigidbody.velocity = owner.myRigidbody.velocity * (percent)
                    + (dire.normalized * owner.myRigidbody.velocity.magnitude) * (1f - percent);
                
            }
            // 3秒以上掛かった場合強制的に終了
            if (timer > 3f)
            {
                owner.EndReturnWireTip();
            }
            timer += Time.deltaTime;
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