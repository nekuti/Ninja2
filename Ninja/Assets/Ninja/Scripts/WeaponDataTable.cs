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
        [SerializeField, Tooltip("武器名")]
        private string weaponName;
        
        [SerializeField, Tooltip("武器のプレハブ")]
        private GameObject weaponPrefab;
        
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
        
        [SerializeField, Tooltip("基本発射速度")]
        private int speed = 1;
        [SerializeField, Tooltip("速度レベル"), Range(1, 10)]
        private int speedLevel = 1;
        #endregion

        #region プロパティ
        public string WeaponName { get { return weaponName; } }
        public GameObject WeaponPrefab { get { return weaponPrefab; } }
        public WeaponType WeaponType { get { return weaponType; } }
        public int Power { get { return power; } }
        public int PowerLevel { get { return powerLevel; } }
        public int Many { get { return many; } }
        public int ManyLevel { get { return manyLevel; } }
        public int Speed { get { return speed; } }
        public int SpeedLevel { get { return speedLevel; } }
        #endregion

        #region メソッド

        #endregion
    }
}