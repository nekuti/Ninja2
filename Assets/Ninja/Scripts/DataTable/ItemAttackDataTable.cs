using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃アイテムのデータクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/19
/// </summary>
namespace Kojima
{
    [CreateAssetMenu]
    public class ItemAttackDataTable : Ando.ItemDataTable
    {
        #region メンバ変数
        [SerializeField, Tooltip("攻撃のプレハブ")]
        private Attack prefab;

        [SerializeField, Tooltip("攻撃力"), Range(1, 50)]
        private int power = 5;

        [SerializeField, Tooltip("攻撃範囲"),Range(1,50)]
        private int range = 5;

        [SerializeField,Tooltip("消えるまでの時間"),Range(0f,50f)]
        private float destroyTime = 5f;

        [SerializeField,Tooltip("弾丸速度"),Range(0f,300f)]
        private float bulletSpeed = 16f;
        #endregion

        #region プロパティ
        public Attack Prefab { get { return prefab; } }
        public int Power { get { return power; } }
        public int Range { get { return range; } }
        public float DestroyTime { get { return destroyTime; } }
        public float BulletSpeed { get { return bulletSpeed; } }
        #endregion
    }
}