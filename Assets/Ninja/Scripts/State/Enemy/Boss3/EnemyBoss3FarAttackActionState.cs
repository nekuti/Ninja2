using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3FarAttackActionState : State<EnemyBoss> {

    public EnemyBoss3FarAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        owner.ChangeState(EnemyBossStateType.Choose);
    }

    public override void Exit()
    {

    }
}
