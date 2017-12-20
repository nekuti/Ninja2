using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WeaponDataのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/11
/// </summary>

namespace Kojima
{
    [CreateAssetMenu]
    public class WeaponDataTable : ScriptableObject
    {
        #region メンバ変数
        [SerializeField, Tooltip("名前")]
        private string weaponName;

        [SerializeField, Tooltip("説明"),MultilineAttribute(3)]
        private string weaponText;

        [SerializeField, Tooltip("武器の種類")]
        private WeaponType weaponType;

        [SerializeField, Tooltip("プレハブ")]
        private Attack weaponPrefab;
        
        [SerializeField, Tooltip("基本攻撃力"), Range(1f,50f)]
        private int power = 1;

        [SerializeField, Tooltip("消えるまでの時間"), Range(0f, 50f)]
        private float destroyTime = 5f;

        [SerializeField, Tooltip("基本弾丸速度"), Range(0f, 300f)]
        private float bulletSpeed = 16f;

        [SerializeField, Tooltip("基本連射速度"), Range(0f, 10f)]
        private float fireSpeed = 1;

        [SerializeField, Tooltip("基本発射数"), Range(1,50)]
        private int many = 1;

        [SerializeField,Tooltip("弾の拡散度(値が大きいほど拡散する)"),Range(0f,90f)]
        private float diffusion = 0f;

        [SerializeField, Tooltip("反動の硬直時間"), Range(0f,10f)]
        private float recoil = 0.5f;

        [SerializeField, Tooltip("基本強化費用")]
        private int cost = 400;

        #endregion

        #region プロパティ
        public string WeaponName { get { return weaponName; } }
        public string WeaponText { get { return weaponText; } }
        public WeaponType WeaponType { get { return weaponType; } }
        public Attack WeaponPrefab { get { return weaponPrefab; } }
        public int Power { get { return power; } }
        public float DestroyTime { get { return destroyTime; } }
        public float BulletSpeed { get { return bulletSpeed; } }
        public float FireSpeed { get { return fireSpeed; } }
        public int Many { get { return many; } }
        public float Diffusion { get { return diffusion; } }
        public float Recoil { get { return recoil; } }
        public int Cost { get { return cost; } }
        #endregion

        #region メソッド

        #endregion
    }
}