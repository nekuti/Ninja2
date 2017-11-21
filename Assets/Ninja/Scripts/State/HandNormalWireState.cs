using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HandNormalWireStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/14
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
            // ワイヤー(紐)部分の表示
            if (wireTip != null)
            {
                float length = (wireTip.transform.position - owner.shotPos.transform.position).magnitude;
                // ワイヤーチップに向けてワイヤー(紐)の向きと長さを変える
                owner.wireObject.transform.position = owner.shotPos.transform.position;
                owner.wireObject.transform.localScale = new Vector3(1f, 1f, length);
                owner.wireObject.transform.LookAt(wireTip.transform.position);
                owner.wireObject.SetActive(true);
            }
            else
            {
                owner.wireObject.SetActive(false);
            }

            // キーボードでの入力処理
            if (Input.GetButtonDown("Fire2"))
            {
                ShotWireTip();
            }
            if (Input.GetButtonUp("Fire2"))
            {
                ReturnWireTip();
            }

            // VIVEでの入力処理
            if(owner.trackdObject != null && owner.device != null)
            {
                float value = owner.device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
                if (value > 0.89f)
                {
                    ShotWireTip();
                }
                if (value < 0.15f && wireTip != null)
                {
                    ReturnWireTip();
                }
            }
            // ワイヤーがオブジェクトについている間の処理
            if (hitFlg)
            {
                // 力を加える割合
                float percent = (hitHandPos - owner.transform.position).magnitude;
                if (owner.trackdObject == null) percent = 1f;
                if (percent > 1) percent = 1f;

                Vector3 vec = wireTip.transform.position - owner.transform.position;

                owner.owner.PullPlayer(vec.normalized * wireData.PullSpeed * percent, wireData.PullSpeed);
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            owner.wireObject.SetActive(false);
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
                wireTip = WireTip.Create(wireData, this, owner, owner.transform.rotation * Vector3.forward);
            }
        }

        /// <summary>
        /// ワイヤーを巻き取る
        /// </summary>
        public void ReturnWireTip()
        {
            hitFlg = false;

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
            hitFlg = false;

            if (wireTip != null)
            {
                GameObject.Destroy(wireTip.gameObject);
                wireTip = null;
            }
        }

        #endregion
    }
}