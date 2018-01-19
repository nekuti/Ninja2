using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireControlのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2018/01/19
/// </summary>
namespace Kojima
{
    public enum WireStateType
    {
        Wait,
        Shot,
        Hook,
        Return,
    }
    public class WireControl : StatefulObjectBase<WireControl,WireStateType>
    {
        #region メンバ変数

        private Hand myHand;

        public WireTip wireTip;

        #endregion

        #region プロパティ
        public Hand MyHand { get { return myHand; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            // 生成主の手を取得
            myHand = GetComponent<Hand>();

            // ステートマシンのインスタンス化
            stateMachine = new StateMachine<WireControl>();
            // ステートリストにステートを追加
            stateList.Add(new WireWaitState(this));
            stateList.Add(new WireShotState(this));
            stateList.Add(new WireHookState(this));
            stateList.Add(new WireReturnState(this));
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {
            // 初期のステートを設定
            ChangeState(WireStateType.Wait);
            Debug.Log("ワイヤー管理クラスができました");
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            base.Update();

            // ワイヤー部分の表示
            if (!IsCurrentState(WireStateType.Wait))
            {
                float length = (wireTip.transform.position - myHand.shotPos.transform.position).magnitude;
                // ワイヤーチップに向けてワイヤー(紐)の向きと長さを変える
                myHand.wireObject.transform.position = myHand.shotPos.transform.position;
                myHand.wireObject.transform.localScale = new Vector3(1f, 1f, length);
                myHand.wireObject.transform.LookAt(wireTip.transform.position);
                myHand.wireObject.SetActive(true);
            }
            else
            {
                // ワイヤー未使用時は非アクティブにする
                myHand.wireObject.SetActive(false);
            }
        }
        
        /// <summary>
        /// このスクリプトが破棄されるとき
        /// </summary>
        private void OnDestroy()
        {
            if(wireTip != null)
            {
                // 持っているワイヤーチップを削除
                Destroy(wireTip.gameObject);
            }
        }

        /// <summary>
        /// ワイヤーがオブジェクトについた
        /// </summary>
        public void HitWireTip()
        {
            ChangeState(WireStateType.Hook);
        }

        /// <summary>
        /// ワイヤーの巻き取りを開始
        /// </summary>
        public void ReturnWireTip()
        {
            ChangeState(WireStateType.Return);
        }

        /// <summary>
        /// ワイヤーの巻き取りが完了
        /// </summary>
        public void ReturnedWireTip()
        {
            if (wireTip != null)
            {
                GameObject.Destroy(wireTip.gameObject);
                wireTip = null;
            }
            ChangeState(WireStateType.Wait);
        }

        #endregion
    }
}