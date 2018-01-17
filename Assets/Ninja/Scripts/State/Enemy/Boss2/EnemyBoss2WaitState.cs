using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2WaitState : State<EnemyBoss> {

    public EnemyBoss2WaitState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("待機ステート");
    }

    public override void Execute()
    {
        if (owner.FlameWaitTime(50))
        {
            owner.ChangeState(EnemyBossStateType.Choose);
        }
    }

    public override void Exit()
    {

    }
}
