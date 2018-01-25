using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public enum EnemyBossStateType
{
    StandBy,
    Wait,
    Choose,
    //Boss1
    JumpAction,
    SummonAction,
    MoveAttackAction,
    MoveBackAction,
    RollAttackAction,
    //Boss2
    B2NearAction,
    B2FarAttackAction,
    B2NearAttackAction,
    B2StalkingAction,
    B2MovePointAction,
    //Boss3
    B3NearAction,
    B3FarAttackAction,
    B3NearAttackAction,
    Damage,
    Die,
}

public class EnemyBoss : StatefulObjectBase<EnemyBoss, EnemyBossStateType>, IDamageable
{
    #region メンバ変数

    // プレイヤーのオブジェクト
    public static GameObject player;

    [SerializeField, Tooltip("体力の最大値")]
    private float maxHp = 10f;
    [SerializeField, Tooltip("現在の体力")]
    private float hp = 10f;

    [SerializeField, Tooltip("攻撃のプレハブ")]
    public Attack attackPrefab;

    private static float flameCount = 0;

    private static float secondCount = 0;

    private static float timerCount = 0;

    // 敵のデータ
    public EnemyDataTable enemyData;

    [System.NonSerialized]
    public Rigidbody myRigidbody;

    private bool collisionObject = false;

    private bool collisionFloor = false;


    //アニメーター
    [System.NonSerialized]
    public Animator animator;

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
    public bool CollisionObject { get { return collisionObject; } }
    public bool CollisionFloor { get{ return collisionFloor; } }
    #endregion

    #region メソッド

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected void Awake()
    {
        if (enemyData == null) Debug.Log("敵データが未設定です");
        if (attackPrefab == null) Debug.Log("攻撃のプレハブが未設定です");

        myRigidbody = GetComponent<Rigidbody>();

        animator = GetComponentInChildren<Animator>();

        // ステートマシンのインスタンス化
        stateMachine = new StateMachine<EnemyBoss>();

        // ステートリストにステートを追加
        stateList.Add(enemyData.EnemyType.CreateBossStandByState(this));
        stateList.Add(enemyData.EnemyType.CreateBossWaitState(this));
        stateList.Add(enemyData.EnemyType.CreateBossChooseState(this));
        //Boss1
        stateList.Add(enemyData.EnemyType.CreateBossJumpActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBossSummonActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBossMoveAttackActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBossMoveBackActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBossRollAttackAction(this));
        //Boss2
        stateList.Add(enemyData.EnemyType.CreateBoss2NearActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBoss2FarAttackActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBoss2NearAttackActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBoss2StalkingActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBoss2MovePointActionState(this));
        //Boss3
        stateList.Add(enemyData.EnemyType.CreateBoss3NearActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBoss3FarAttackActionState(this));
        stateList.Add(enemyData.EnemyType.CreateBoss3NearAttackActionState(this));

        stateList.Add(enemyData.EnemyType.CreateBossDamageState(this));
        stateList.Add(enemyData.EnemyType.CreateBossDieState(this));
    }

    /// <summary>
    /// 更新前処理
    /// </summary>
    protected void Start()
    {
        // 初期のステートを設定
        ChangeState(EnemyBossStateType.StandBy);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected override void Update()
    {
        myRigidbody.velocity = Vector3.zero;
        base.Update();

        // HPが0の場合死亡ステートへ
        if (Hp <= 0 && !IsCurrentState(EnemyBossStateType.Die))
        {
            Debug.Log("死んで");
            ChangeState(EnemyBossStateType.Die);

        }
        collisionObject = false;
        collisionFloor = false;

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
                ChangeState(EnemyBossStateType.Damage);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// プレイヤーを登録
    /// </summary>
    /// <param name="aPlayer"></param>
    public static void EntryPlayer(GameObject aPlayer)
    {
        player = aPlayer;
    }

    /// <summary>
    /// 物理挙動で自身の移動速度に合わせて移動させる
    /// </summary>
    /// <param name="aPos">移動先座標</param>
    /// <returns></returns>
    public bool MoveTo(Vector3 aPos)
    {
        return MoveTo(aPos, enemyData.MoveSpeed);
    }
    private bool MoveTo(Vector3 aPos, float aSpeed)
    {
        Vector3 vec = (aPos - transform.position).normalized * aSpeed;

        // Rigidbodyに力を加える
        myRigidbody.AddForce(vec, ForceMode.VelocityChange);

        // 目的座標に到着したらtrueを返す
        //if ((aPos - transform.position).magnitude < enemyData.MoveSpeed * Time.deltaTime)
        if ((aPos - transform.position).magnitude < 0.5f)
        //if ((aPos - transform.position).magnitude < vec.magnitude)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool QuickMoveTo(Vector3 aPos)
    {
        return MoveTo(aPos, enemyData.AttackMoveSpeed);
    }

    /// <summary>
    /// 指定座標を向くように回転させる
    /// </summary>
    /// <param name="aPos"></param>
    /// <returns></returns>
    public bool LookTo(Vector3 aPos)
    {
        //transform.LookAt(aPos);
        float angle = 180f;
        Quaternion lookRotate = Quaternion.LookRotation(aPos - myRigidbody.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotate, angle * Time.deltaTime) ;

        if (transform.rotation == lookRotate)
        {
            return true;
        }
        else
        {
            return false;
        }


    }


    /// <summary>
    /// 正面に攻撃を生成
    /// </summary>
    /// <param name="aPos"></param>
    /// <returns></returns>
    public Attack ShotAttackForward()
    {
        return ShotAttack(transform.position + transform.forward);
    }

    /// <summary>
    /// 指定座標へ向けて攻撃を生成
    /// </summary>
    /// <param name="aPos"></param>
    /// <returns></returns>
    public Attack ShotAttack(Vector3 aPos)
    {
        return ShotAttack(transform.position, aPos);
    }
    public Attack ShotAttack(Vector3 from, Vector3 to)
    {
        Vector3 vec = to - transform.position;
        return Attack.Create(AttackPrefab, from, transform.position + vec, enemyData.Power, tag);
    }
    //壁と当たっているか
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == TagName.Object)
        {
            collisionObject = true;
        }
        else if (collision.gameObject.tag == TagName.WireableObject)
        {
            collisionObject = true;
        }
        else if (collision.gameObject.tag == TagName.Floor)
        {
            collisionFloor = true;
        }
        else
        {
            collisionObject = false;
            collisionFloor = false;
        }
    }

    public void UseGravity(float localGravity = -9.8f)
    {
        myRigidbody.AddForce((new Vector3(0f, localGravity, 0f)), ForceMode.VelocityChange);
    }

    public bool ObjectWhere(string name1, string name2)
    {
        GameObject obj1 = GameObject.Find(name1);

        GameObject obj2 = GameObject.Find(name2);

        Vector3 obj1Dis = obj1.transform.position - Enemy.player.transform.position;
        Vector3 obj2Dis = obj2.transform.position - Enemy.player.transform.position;

        if (obj1Dis.magnitude > obj2Dis.magnitude)
        {
            Debug.Log(name1 + "のが近い");
            return true;
        }
        else
        {
            Debug.Log(name2 + "のが近い");
            return false;
        }
    }

    public bool FlameWaitTime(float flame)
    {
        if (flameCount > flame)
        {
            flameCount = 0;
            return true;
        }
        else
        {
            flameCount++;
            return false;
        }
    }


    public bool SecondWaitime(float second)
    {
        if (timerCount >= second)
        {
            timerCount = 0;
            secondCount = 0;
            return true;
        }
        else
        {
            timerCount += Time.deltaTime;
            return false;
        }
    }

    public Vector3 Point(float angle, float radius)
    {
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        return new Vector3(x, 0, z);
    }

    #endregion
}
