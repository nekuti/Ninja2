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
    public class WireTip : StatefulObjectBase<WireTip,WireTipStateType>
    {
        #region メンバ変数

        [Tooltip("発射主のWireState")]
        public HandNormalWireState ownerWireState;

        [System.NonSerialized]
        public Transform ownerTransform;// 発射主のTransform

        [System.NonSerialized]
        public Vector3 shotDirection;   // ワイヤーを飛ばす向き

        [System.NonSerialized]
        public Rigidbody myRigidbody;   // 自身のRigidbody

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

            // 回転を止める
            if(IsCurrentState(WireTipStateType.Stop))
            {
                PropellerRot propeller = GetComponentInChildren<PropellerRot>();
                if(propeller!=null)
                {
                    Destroy(propeller);
                }
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
                }
                else
                {
                    // ワイヤーを付けれないオブジェクトであった場合
                    // ワイヤーの巻き取りを行う
                    ReturnWireTip();
                }
            }
        }

        /// <summary>
        /// トリガーにぶつかったとき
        /// </summary>
        /// <param name="other"></param>
        void OnTriggerStay(Collider other)
        {
            // 巻き戻し状態の場合
            if(IsCurrentState(WireTipStateType.Return))
            {
                // Handに当たった場合巻き取りを完了
                if(other.gameObject.CompareTag(TagName.Hand))
                {
                    EndReturnWireTip();
                }
            }
        }

        /// <summary>
        /// WireTipの生成と同時に初期化して発射
        /// </summary>
        /// <param name="aWireDataTable">Wireのデータ</param>
        /// <param name="aHandWireState">生成主のState</param>
        /// <param name="aTransform">生成主のTransform</param>
        /// <param name="aDirection">発射する向き</param>
        /// <returns></returns>
        public static WireTip Create(WireDataTable aWireDataTable , HandNormalWireState aHandWireState, Hand aHand,Vector3 aDirection)
        {
            return WireTip.Create(aWireDataTable.WirePrefab, aHandWireState, aHand,aDirection);
        }
        /// <summary>
        /// WireTipの生成と同時に初期化して発射
        /// </summary>
        /// <param name="aPrefab">プレハブ</param>
        /// <param name="aHandWireState">生成主のState</param>
        /// <param name="aTransform">生成主のTransform</param>
        /// <param name="aDirection">発射する向き</param>
        /// <returns></returns>
        public static WireTip Create(WireTip aPrefab,HandNormalWireState aHandWireState, Hand aHand,Vector3 aDirection)
        {
            WireTip obj = Instantiate(aPrefab,aHand.shotPos.transform.position,aHand.transform.rotation) as WireTip;
            obj.ownerWireState = aHandWireState;
            obj.ownerTransform = aHand.transform;
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
            // 巻き取りステートへ移行
            ChangeState(WireTipStateType.Return);
        }

        /// <summary>
        /// ワイヤーの巻き取りが完了
        /// </summary>
        public void EndReturnWireTip()
        {
            ownerWireState.ResetWireTip();
        }

        #endregion
    }
}