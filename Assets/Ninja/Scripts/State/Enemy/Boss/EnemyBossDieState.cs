using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossDieState : State<EnemyBoss>
{
    public EnemyBossDieState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("敵(遊撃)が死亡ステートへ遷移");
        ParticleEffect.Create(ParticleEffectType.Explosion02, owner.transform.position);
        GameObject.Destroy(owner.gameObject);
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}
