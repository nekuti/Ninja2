using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossDamageState : State<EnemyBoss>
{
    public EnemyBossDamageState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        ParticleEffect.Create(ParticleEffectType.Explosion_small01, owner.transform.position);
       /*β版で操作しましたｂｙ安藤*/
        //GameObject.Destroy(owner.gameObject);
    }

    public override void Exit()
    {

    }
}
