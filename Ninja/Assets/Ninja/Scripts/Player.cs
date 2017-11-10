using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/10
/// </summary>
namespace Kojima
{
    /// <summary>
    /// Playerのステートタイプ
    /// </summary>
    public enum PlayerStateType
    {
        Wait,
    }
    
    public class Player : StatefulObjectBase<Player,PlayerStateType>,IDamageable
    {
        #region メンバ変数
        [SerializeField, Tooltip("エネルギーの最大値")]
        private float maxEnergy;

        [SerializeField, Tooltip("現在のエネルギー")]
        private float energy;

        [SerializeField,Tooltip("左手")]
        private Hand leftHand;
        [SerializeField,Tooltip("右手")]
        private Hand rightHand;

        #endregion

        #region プロパティ
        public float MaxEnergy { get { return maxEnergy; } }
        public float Energy
        {
            get { return energy; }
            private set
            {
                energy = value;
                // エネルギーが上限、下限を超えないようにする
                if (energy > maxEnergy) energy = maxEnergy;
                if (energy < 0)         energy = 0;
            }
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Awake()
        {
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 攻撃を受ける
        /// </summary>
        /// <param name="aDamage">攻撃のダメージ量</param>
        public void TakeAttack(int aDamage)
        {

        }

        #endregion
    }
}