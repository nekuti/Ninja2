using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3ChooseState : State<EnemyBoss> {
    private int action;
    public EnemyBoss3ChooseState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        action = 0;
        action = Random.Range(1, 30);
    }

    public override void Execute()
    {
        if(action >= 1 && action < 10)
        { owner.ChangeState(EnemyBossStateType.B3FarAction); }
        if (action >= 10 && action < 20)
        { owner.ChangeState(EnemyBossStateType.B3NearAction); }
        if (action >= 20 && action <= 30)
        { owner.ChangeState(EnemyBossStateType.B3MoveAction); }
    }

    public override void Exit()
    {

    }
}
