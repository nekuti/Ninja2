using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3WaitState : State<EnemyBoss> {

    public EnemyBoss3WaitState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        if (owner.SecondWaitime(1))
        {
            owner.ChangeState(EnemyBossStateType.Choose);
        }
    }

    public override void Exit()
    {

    }
}
