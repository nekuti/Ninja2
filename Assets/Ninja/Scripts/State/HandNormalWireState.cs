using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HandNormalWireStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/10
/// </summary>
namespace Kojima
{
    public class HandNormalWireState : State<Hand>
    {
        #region メンバ変数

        public WireDataTable wireData;

        private WireTip wireTip;

        private bool hitFlg;
        private Vector3 hitHandPos;

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
            wireData = owner.WireData;
            hitFlg = false;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                ShotWireTip();
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                ReturnWireTip();
            }
            if(owner.trackdObject != null && owner.device != null)
            {
                float value = owner.device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
                if (value > 0.89f)
                {
                    ShotWireTip();
                }
                if (value < 0.15f && wireTip != null)
                {
                    wireTip.ReturnWireTip();
                }
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            if(hitFlg)
            {

            }
        }

        /// <summary>
        /// ワイヤーを発射する
        /// </summary>
        /// <returns></returns>
        public void ShotWireTip()
        {
            if(wireTip == null)
            {
                // ワイヤーを生成
                wireTip = WireTip.Create(wireData, this, owner.transform, owner.transform.rotation * Vector3.forward);
            }
        }

        public void ReturnWireTip()
        {
            if (wireTip != null)
            {
                if (!wireTip.IsCurrentState(WireTipStateType.Return))
                {
                    wireTip.ReturnWireTip();
                }
            }
        }

        /// <summary>
        /// ワイヤーがオブジェクトについた
        /// </summary>
        public void HitWireTip()
        {
            Debug.Log("ワイヤーが当たった");
            hitFlg = true;
            hitHandPos = owner.transform.position;
        }

        /// <summary>
        /// 既存のワイヤーをリセットして再度発射可能な状態にする
        /// </summary>
        public void ResetWireTip()
        {
            if(wireTip != null)
            {
                GameObject.Destroy(wireTip.gameObject);
                wireTip = null;
            }
        }

        #endregion
    }
}