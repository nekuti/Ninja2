using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2DieState : State<EnemyBoss> {

    public EnemyBoss2DieState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("死亡ステート");
        ParticleEffect.Create(ParticleEffectType.Explosion02, owner.transform.position);

        GameObject.Destroy(owner.gameObject);
    }

    public override void Execute()
    {
        owner.UseGravity();
    }

    public override void Exit()
    {

    }
}
