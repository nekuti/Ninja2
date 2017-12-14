using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TestEventのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/11
/// </summary>
namespace Kojima
{
    public class TestEvent : MonoBehaviour
    {
        #region メンバ変数

        [SerializeField]
        private ParticleEffectType particle;

        private bool turnFlg;
        private float turnNum;

        #endregion

        #region メソッド

        /// <summary>
        /// 更新前処理
        /// </summary>
        private void Start()
        {
            turnFlg = false;
            turnNum = 0f;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            if(turnFlg)
            {
                transform.Rotate(new Vector3(0f, turnNum * 90f, 0f));
                turnNum += Time.deltaTime;
            }
        }

        /// <summary>
        /// 回転フラグの設定
        /// </summary>
        /// <param name="aFlg"></param>
        public void SetTurnFlg(bool aFlg)
        {
            turnFlg = aFlg;
        }

        /// <summary>
        /// パーティクルを生成
        /// </summary>
        public void Partcle()
        {
            ParticleEffect.Create(particle, transform.position);
        }

        #endregion
    }
}