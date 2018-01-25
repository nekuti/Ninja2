using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss2DieState : State<EnemyBoss>
{
    public EnemyBoss2DieState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("bossが死亡ステートへ遷移");
        //owner.gameObject.SetActive(false);
        //GameObject.Destroy(owner.gameObject);
        ParticleEffect.Create(ParticleEffectType.Explosion02, owner.transform.position);

    }

    public override void Execute()
    {
        GameObject.Destroy(owner.gameObject);
    }

    public override void Exit()
    {

    }
}
