using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワイヤーの先端のクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/23
/// </summary>
namespace Kojima
{
    public enum WireTipStateType
    {
        Stop,
        Shot,
        Return,
    }
    public class WireTip : StatefulObjectBase<WireTip, WireTipStateType>
    {
        #region メンバ変数
        // 生成主のWireControl
        private WireControl controller;

        // ワイヤーを飛ばす向き
        private Vector3 shotDirection;

        [System.NonSerialized]
        public Rigidbody myRigidbody;   // 自身のRigidbody

        private GameObject item;

        #endregion

        #region プロパティ
        public WireControl Controller { get { return controller; } }
        public Vector3 ShotDirection { get { return shotDirection; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // Rigidbodyの取得
            myRigidbody = GetComponent<Rigidbody>();

            // ステートマシンのインスタンス化
            stateMachine = new StateMachine<WireTip>();

            // ステートリストにステートを追加
            stateList.Add(new WireTipStopState(this));
            stateList.Add(new WireTipShotState(this));
            stateList.Add(new WireTipReturnState(this));
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {
            // 初期のステートを設定
            ChangeState(WireTipStateType.Shot);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        private void OnDestroy()
        {
            if(item != null)
            {
                item.transform.parent = null;
            }
        }

        /// <summary>
        /// 何かにぶつかったときの処理
        /// </summary>
        /// <param name="coll"></param>
        void OnCollisionEnter(Collision other)
        {
            // 発射状態の場合
            if (IsCurrentState(WireTipStateType.Shot))
            {
                if (other.gameObject.CompareTag(TagName.WireableObject))
                {
                    // ワイヤーを付けられるオブジェクトであった場合
                    // WireTipを停止してオブジェクトの子オブジェクトにする(当たった場所にくっつく)
                    myRigidbody.velocity = Vector3.zero;
                    myRigidbody.isKinematic = true;

                    //transform.SetParent(other.transform,false);

                    // 停止ステートへ移行
                    ChangeState(WireTipStateType.Stop);

                    // SEを再生
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_WIRE_HIT, transform.position);
                }
                else
                {
                    // ワイヤーを付けれないオブジェクトであった場合
                    // パーティクルを生成
                    ParticleEffect.Create(ParticleEffectType.Flash_small01, transform.position);
                    // ワイヤーの巻き取りを行う
                    controller.ChangeState(WireStateType.Return);

                    // SEを再生
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_WIRE_FAULURE, transform.position);

                }
            }
        }

        /// <summary>
        /// トリガーにぶつかったとき
        /// </summary>
        /// <param name="other"></param>
        void OnTriggerStay(Collider other)
        {
            // 発射状態の場合
            if (IsCurrentState(WireTipStateType.Shot))
            {
                if (other.gameObject.CompareTag(TagName.Item))
                {
                    // アイテムをワイヤーにくっつける
                    item = other.gameObject;
                    item.transform.parent = transform;
                    Debug.Log(item + "をワイヤーで引っ掛ける");

                    // ワイヤーの巻き取りを行う
                    ChangeState(WireTipStateType.Return);

                    // SEを再生
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_WIRE_HIT, transform.position);
                }
            }
            // 巻き戻し状態の場合
            if (IsCurrentState(WireTipStateType.Return))
            {
                // Handに当たった場合巻き取りを完了
                if(other.gameObject.CompareTag(TagName.Hand))
                {
                    EndReturnWireTip();
                }
            }
        }

        /// <summary>
        /// WireTipの生成と同時に初期化
        /// </summary>
        /// <param name="aWireDataTable">ワイヤーのデータ</param>
        /// <param name="aController">生成主のWireControl</param>
        /// <param name="aDirection">発射する向き</param>
        /// <returns></returns>
        public static WireTip Create(WireDataTable aWireDataTable , WireControl aController,Vector3 aDirection)
        {
            return WireTip.Create(aWireDataTable.WirePrefab, aController,aDirection);
        }
        /// <summary>
        /// WireTipの生成と同時に初期化
        /// </summary>
        /// <param name="aPrefab">ワイヤーのプレハブ</param>
        /// <param name="aController">生成主のWireControl</param>
        /// <param name="aDirection">発射する向き</param>
        /// <returns></returns>
        public static WireTip Create(WireTip aPrefab,WireControl aController,Vector3 aDirection)
        {
            WireTip obj = Instantiate(aPrefab,aController.MyHand.shotPos.transform.position,aController.transform.rotation) as WireTip;
            obj.controller = aController;
            obj.shotDirection = aDirection;
            return obj;
        }

        /// <summary>
        /// 発射主のもとへ戻る
        /// </summary>
        /// <returns></returns>
        public void ReturnWireTip()
        {
            transform.parent = null;
            myRigidbody.isKinematic = false;
            if (!IsCurrentState(WireTipStateType.Return))
            {
                // 巻き取りステートへ移行
                ChangeState(WireTipStateType.Return);
            }
        }

        /// <summary>
        /// ワイヤーの巻き取りが完了
        /// </summary>
        public void EndReturnWireTip()
        {
            controller.ReturnedWireTip();
        }

        #endregion
    }
}