using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WeaponDataのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/09
/// </summary>

namespace Kojima
{
    [CreateAssetMenu]
    public class WeaponDataTable : ScriptableObject
    {
        #region メンバ変数
        [SerializeField, Tooltip("名前")]
        private string weaponName;
        
        [SerializeField, Tooltip("プレハブ")]
        private Attack weaponPrefab;
        
        [SerializeField, Tooltip("武器の種類")]
        private WeaponType weaponType;
        
        [SerializeField, Tooltip("基本攻撃力")]
        private int power = 1;
        [SerializeField, Tooltip("攻撃力レベル"), Range(1,10)]
        private int powerLevel = 1;
        
        [SerializeField, Tooltip("基本発射数")]
        private int many = 1;
        [SerializeField, Tooltip("発射数レベル"), Range(1, 10)]
        private int manyLevel = 1;
        
        [SerializeField, Tooltip("基本連射速度(1回の発射までにかかる時間)"), Range(0f, 10f)]
        private float speed = 1;
        [SerializeField, Tooltip("連射レベル")]
        private int speedLevel = 1;

        [SerializeField, Tooltip("反動の硬直時間"), Range(0f,10f)]
        private float recoil = 0.5f;

        #endregion

        #region プロパティ
        public string WeaponName { get { return weaponName; } }
        public Attack WeaponPrefab { get { return weaponPrefab; } }
        public WeaponType WeaponType { get { return weaponType; } }
        public int Power { get { return power; } }
        public int PowerLevel { get { return powerLevel; } }
        public int Many { get { return many; } }
        public int ManyLevel { get { return manyLevel; } }
        public float Speed { get { return speed; } }
        public int SpeedLevel { get { return speedLevel; } }
        public float Recoil { get { return recoil; } }
        #endregion

        #region メソッド

        #endregion
    }
}