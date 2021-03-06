﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HandNormalWireStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/23
/// </summary>
namespace Kojima
{
    public class HandNormalWireState : State<Hand>
    {
        #region メンバ変数

        public WireDataTable wireData;

        private WireTip wireTip;
        
        private bool hitFlg;            // ワイヤーが壁に刺さった状態かのフラグ
        private Vector3 hitHandPos;     // ワイヤーが壁に刺さった時点での手の座標


        private bool attackedFlg;

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
            attackedFlg = false;
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

            
            if(wireTip != null)
            {
                if(wireTip.IsCurrentState(WireTipStateType.Shot))
                {
                    InputDevice.Pulse(500,owner.HandType);
                }
            }

            if (InputDevice.ClickDownTrriger(owner.HandType))
            {
                ShotWireTip();
            }
            if (!InputDevice.Touch(ButtonType.Trigger,owner.HandType) && wireTip != null)
            {
                ReturnWireTip();
            }

            // ======================================================================
            // 文化祭の仮処理でクナイを発射する======================================
            // (ここから)============================================================
            //if (owner.device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)||
            //    owner.device.GetPress(SteamVR_Controller.ButtonMask.Grip))
            //{
            //    if (!attackedFlg)
            //    {
            //        // クナイを発射
            //        Attack.Create(owner.WeaponData.WeaponPrefab, owner.shotPos.transform.position, owner.transform.position + owner.transform.forward, owner.WeaponData.Power, owner.tag);
            //        attackedFlg = true;
            //        // コントローラーを振動させる
            //        owner.device.TriggerHapticPulse(3999);
            //    }
            //}
            //else
            //{
            //    attackedFlg = false;
            //}
            // (ここまで)============================================================
            // ======================================================================
            // ======================================================================
            
            // ワイヤーがオブジェクトについている間の処理
            if (hitFlg)
            {
                // 力を加える割合
                float percent = (hitHandPos - owner.transform.position).magnitude;
                if (!InputDevice.IsDeviceRegisterd(owner.HandType)) percent = 1f;
                if (percent > 1) percent = 1f;

                Vector3 vec = wireTip.transform.position - owner.transform.position;

                owner.MyPlayer.PullPlayer(vec.normalized * wireData.PullSpeed * percent, wireData.PullSpeed);
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
                //wireTip = WireTip.Create(wireData, this, owner, owner.transform.rotation * Vector3.forward);
                // コントローラーを振動させる
                InputDevice.Pulse(3000, owner.HandType);
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
            // コントローラーを振動させる
            InputDevice.Pulse(1000, owner.HandType);
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