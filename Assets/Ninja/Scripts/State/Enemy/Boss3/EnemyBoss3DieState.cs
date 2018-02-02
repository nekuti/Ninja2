using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3DieState : State<EnemyBoss> {

    public EnemyBoss3DieState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
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
