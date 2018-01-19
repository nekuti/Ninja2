using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2StandByState : State<EnemyBoss> {

    public EnemyBoss2StandByState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("準備ステート");
    }

    public override void Execute()
    {
        owner.ChangeState(EnemyBossStateType.Wait);
    }

    public override void Exit()
    {

    }
}
