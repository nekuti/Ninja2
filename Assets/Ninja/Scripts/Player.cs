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
        Free,
    }
    
    [RequireComponent(typeof(Rigidbody),typeof(Collider))]
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

        [SerializeField, Tooltip("ワイヤーのデータ")]
        private WireDataTable wireData;
        [SerializeField, Tooltip("武器のデータ")]
        private WeaponDataTable weaponData;

        [System.NonSerialized]
        public Rigidbody myRigidbody;

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
        public WireDataTable WireData { get { return wireData; } }
        public WeaponDataTable WeaponData { get { return weaponData; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Awake()
        {
            // Rigidbodyの取得
            myRigidbody = GetComponent<Rigidbody>();

            // 手を取得
            if (leftHand == null)leftHand = transform.Find("LeftHand").GetComponent<Hand>();
            if (rightHand == null)rightHand = transform.Find("RightHand").GetComponent<Hand>();
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
            Energy -= aDamage;
        }

        /// <summary>
        /// 武器を変更する
        /// </summary>
        /// <param name="aWeaponData"></param>
        /// <returns></returns>
        public bool ChangeWeapon(WeaponDataTable aWeaponData)
        {
            weaponData = aWeaponData;

            // Handにも変更を適応
            leftHand.ChangeWeapon(aWeaponData);
            rightHand.ChangeWeapon(aWeaponData);

            return true;
        }

        /// <summary>
        /// プレイヤーを飛ばす
        /// </summary>
        /// <param name="aForce">飛ばす力</param>
        public void PullPlayer(Vector3 aForce, float aMaxVelocity)
        {
            // 力を加える
            myRigidbody.AddForce(aForce,ForceMode.Acceleration);

            if(myRigidbody.velocity.sqrMagnitude > aMaxVelocity * aMaxVelocity)
            {
                myRigidbody.velocity -= myRigidbody.velocity - (myRigidbody.velocity.normalized * aMaxVelocity);
            }

        }


        #endregion
    }
}