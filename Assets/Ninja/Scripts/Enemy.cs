using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/14
/// </summary>
namespace Kojima
{
    public enum EnemyStateType
    {
        Wait,
        Patrol,
        Chase,
        Attack,
        Damage,
        Die,
    }
    public class Enemy : StatefulObjectBase<Enemy, EnemyStateType>, IDamageable
    {
        #region メンバ変数

        // プレイヤーのオブジェクト
        public GameObject player;

        [SerializeField, Tooltip("体力の最大値")]
        private float maxHp = 10f;
        [SerializeField, Tooltip("現在の体力")]
        private float hp = 10f;

        [SerializeField,Tooltip("攻撃のプレハブ")]
        public Attack attackPrefab;

        // 敵のデータ
        public EnemyDataTable enemyData;

        private Rigidbody myRigidbody;

        #endregion

        #region プロパティ
        public float MaxHp { get { return maxHp; } }
        public float Hp
        {
            get { return hp; }
            private set
            {
                hp = value;
                // エネルギーが上限、下限を超えないようにする
                if (hp > maxHp) hp = maxHp;
                if (hp < 0) hp = 0;
            }
        }
        public Attack AttackPrefab { get { return attackPrefab; } }
        public Rigidbody MyRigidbody { get { return myRigidbody; } }
        #endregion

        #region メソッド

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Awake()
        {
            if (enemyData == null) Debug.Log("敵データが未設定です");
            if (attackPrefab == null) Debug.Log("攻撃のプレハブが未設定です");

            myRigidbody = GetComponent<Rigidbody>();

            // ステートマシンのインスタンス化
            stateMachine = new StateMachine<Enemy>();

            // ステートリストにステートを追加
            stateList.Add(enemyData.EnemyType.CreateWaitState(this));
            stateList.Add(enemyData.EnemyType.CreatePatrolState(this));
            stateList.Add(enemyData.EnemyType.CreateChaseState(this));
            stateList.Add(enemyData.EnemyType.CreateAttackState(this));
            stateList.Add(enemyData.EnemyType.CreateDamageState(this));
            stateList.Add(enemyData.EnemyType.CreateDieState(this));
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        void Start()
        {
            // 初期のステートを設定
            ChangeState(EnemyStateType.Wait);
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update()
        {
            myRigidbody.velocity = Vector3.zero;
            base.Update();

            // HPが0の場合死亡ステートへ
            if(Hp <= 0 && !IsCurrentState(EnemyStateType.Die))
            {
                ChangeState(EnemyStateType.Die);
            }
        }

        /// <summary>
        /// 攻撃を受ける
        /// </summary>
        /// <param name="aDamage">攻撃のダメージ量</param>
        public bool TakeAttack(Attack anAttack)
        {
            Debug.Log("敵が攻撃に当たった");
            // 自身が発射した攻撃でなければ
            if (anAttack.parentTagName != gameObject.tag)
            {
                Hp -= anAttack.power;

                // ノックバックする設定の場合ダメージステートへ移行
                if (enemyData.KnockBack)
                {
                    ChangeState(EnemyStateType.Damage);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 物理挙動で自身の移動速度に合わせて移動させる
        /// </summary>
        /// <param name="aPos">移動先座標</param>
        /// <returns></returns>
        public bool MoveTo(Vector3 aPos)
        {
            // 
            Vector3 vec = (aPos - transform.position).normalized * enemyData.MoveSpeed;

            // Rigidbodyに力を加える
            myRigidbody.AddForce(vec,ForceMode.VelocityChange);

            // 目的座標に到着したらtrueを返す
            if ((aPos - transform.position).magnitude < enemyData.MoveSpeed * Time.deltaTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 指定座標を向くように回転させる
        /// </summary>
        /// <param name="aPos"></param>
        /// <returns></returns>
        public bool LookTo(Vector3 aPos)
        {
            transform.LookAt(aPos);
            return true;
        }

        #endregion
    }
}