using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingEnemy : Kojima.StatefulObjectBase<NothingEnemy, Kojima.EnemyStateType>, Kojima.IDamageable
{

    // プレイヤーのオブジェクト
    public static GameObject player;

    [SerializeField, Tooltip("体力の最大値")]
    private float maxHp = 10f;
    [SerializeField, Tooltip("現在の体力")]
    private float hp = 10f;

    [SerializeField, Tooltip("攻撃のプレハブ")]
    public Kojima.Attack attackPrefab;

    private static float flameCount = 0;

    private static float secondCount = 0;

    // 敵のデータ
    public NothingEnemyData enemyData;

    private Rigidbody myRigidbody;

    private bool collisionDecision = false;

    public float MaxHp { get { return maxHp; } }
    public float Hp
    {
        get { return hp; }
        private set
        {
            hp = value;
            if (hp > maxHp) hp = maxHp;
            if (hp < 0) hp = 0;
        }
    }
    public Kojima.Attack AttackPrefab { get { return attackPrefab; } }
    public Rigidbody MyRigidbody { get { return myRigidbody; } }
    public bool CollisioDecision { get { return collisionDecision; } }

    void Awake()
    {
        if (enemyData == null) Debug.Log("敵データが未設定です");
        if (attackPrefab == null) Debug.Log("攻撃のプレハブが未設定です");

        myRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    protected override void Update()
    {
        myRigidbody.velocity = Vector3.zero;
        base.Update();

        // HPが0の場合死亡ステートへ
        if (Hp <= 0)
        {
            Kojima.ParticleEffect.Create(Kojima.ParticleEffectType.Explosion01, transform.position);
            GameObject.Destroy(gameObject);
        }
        collisionDecision = false;

    }

    /// <summary>
    /// 攻撃を受ける
    /// </summary>
    /// <param name="aDamage">攻撃のダメージ量</param>
    public bool TakeAttack(Kojima.Attack anAttack)
    {
        Debug.Log("敵が攻撃に当たった");
        // 自身が発射した攻撃でなければ
        if (anAttack.parentTagName != gameObject.tag)
        {
            Hp -= anAttack.power;
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
}
