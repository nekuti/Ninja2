using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵データのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/14
/// </summary>
namespace Kojima
{
    [CreateAssetMenu]
    public class EnemyDataTable : ScriptableObject
    {
        #region メンバ変数

        [SerializeField,Tooltip("名前")]
        private string name;

        [SerializeField,Tooltip("プレハブ")]
        private Enemy enemyPrefab;

        [SerializeField, Tooltip("敵の種類")]
        private EnemyType enemyType;

        [SerializeField, Tooltip("飛行可能か")]
        private bool flying = false;

        [SerializeField,Tooltip("ノックバックするか")]
        private bool knockBack = true;

        [SerializeField,Tooltip("体力")]
        private float hp = 10f;

        [SerializeField, Tooltip("攻撃力")]
        private float power = 2f;

        [SerializeField, Tooltip("移動速度")]
        private float moveSpeed = 3f;

        [SerializeField,Tooltip("索敵範囲")]
        private float searchRange = 6f;

        [SerializeField,Tooltip("攻撃開始範囲")]
        private float attackableRange = 3f;

        [SerializeField,Tooltip("攻撃までに掛かる時間")]
        private float attackToTime = 1f;

        [SerializeField,Tooltip("攻撃後の硬直時間")]
        private float attackAfterTime = 1f;

        [SerializeField,Tooltip("ノックバックの硬直時間")]
        private float knockBackTime = 1f;

        #endregion

        #region プロパティ
        public string Name { get { return name; } }
        public Enemy EnemyPrefab { get { return enemyPrefab; } }
        public EnemyType EnemyType { get { return enemyType; } }
        public bool Flying { get { return flying; } }
        public bool KnockBack { get { return knockBack; } }
        public float Hp { get { return hp; } }
        public float Power { get { return power; } }
        public float MoveSpeed { get { return moveSpeed; } }
        public float SearchRange { get { return searchRange; } }
        public float AttackableRange { get { return attackableRange; } }
        public float AttackToTime { get { return attackToTime; } }
        public float AttackAfterTime { get { return attackAfterTime; } }
        public float KnockBackTime { get { return KnockBackTime; } }
        #endregion

        #region メソッド

        #endregion
    }
}