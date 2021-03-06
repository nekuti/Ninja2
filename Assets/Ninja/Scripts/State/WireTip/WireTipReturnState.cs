﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireTipReturnStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/08
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
            maxSpeed = owner.Controller.MyHand.WireData.ReturnSpeed;
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
                // トリガーにする
                owner.GetComponent<Collider>().isTrigger = true;

                Vector3 dire = (owner.Controller.transform.position - owner.transform.position);

                // 巻き取る
                owner.myRigidbody.AddForce(dire.normalized * (owner.Controller.MyHand.WireData.ReturnSpeed), ForceMode.Acceleration);

                if (owner.myRigidbody.velocity.sqrMagnitude > maxSqrSpeed)
                {
                    Vector3 vec = owner.myRigidbody.velocity - (owner.myRigidbody.velocity.normalized * maxSpeed);
                    owner.myRigidbody.velocity -= vec;
                }

                float percent = dire.magnitude / owner.Controller.MyHand.WireData.ShotRange;
                if (percent > 1f) percent = 1f;
                owner.myRigidbody.velocity = owner.myRigidbody.velocity * (percent)
                    + (dire.normalized * owner.myRigidbody.velocity.magnitude) * (1f - percent);

                if(owner.myRigidbody.velocity.magnitude*Time.deltaTime > dire.magnitude)
                {
                    owner.EndReturnWireTip();
                }
                
            }
            // 一定時間掛かった場合強制的に終了
            if (timer > 2f)
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