using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3StandByState : State<EnemyBoss> {

    public EnemyBoss3StandByState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        if (owner.SecondWaitime(2))
        { 
            owner.ChangeState(EnemyBossStateType.Wait);
        }
    }

    public override void Exit()
    {

    }
}
