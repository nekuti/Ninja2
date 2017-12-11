using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WireTipStopStateのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/12
/// </summary>
namespace Kojima
{
    public class WireTipStopState : State<WireTip>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public WireTipStopState(WireTip owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            owner.Controller.HitWireTip();

            // 回転を止める
            PropellerRot propeller = owner.GetComponentInChildren<PropellerRot>();
            if (propeller != null)
            {
                GameObject.Destroy(propeller);
            }

            // パーティクルを生成
            ParticleEffect.Create(ParticleEffectType.Ring01, owner.transform.position);
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
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