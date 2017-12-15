using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AttackBlastのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/15
/// </summary>
namespace Kojima
{
    public class AttackBlast : Attack
    {
        #region メンバ変数
        [SerializeField]
        private ParticleEffect myParticle;

        #endregion

        #region メソッド

        /// <summary>
        /// 更新前処理
        /// </summary>
        protected override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            // 寿命が来たらパーティクルを解放してから自身を消す
            if (TimerCount())
            {
                myParticle.transform.parent = null;
                Destroy(this.gameObject);
            }
        }

        #endregion
    }
}