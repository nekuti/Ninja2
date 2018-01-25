using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;


public class EnemyBossChooseState : State<EnemyBoss>
{
    private int chooseState;

    public EnemyBossChooseState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Random.Range(0, 80);
        chooseState = Random.Range(0, 80);
        Debug.Log(chooseState);
    }

    public override void Execute()
    {
        //if (chooseState >= 0 && chooseState < 40) { owner.ChangeState(EnemyBossStateType.MoveAttackAction); }
        //if (chooseState >= 40 && chooseState < 60) { owner.ChangeState(EnemyBossStateType.MoveBackAction); }
        //if (chooseState >= 40 && chooseState < 60) { owner.ChangeState(EnemyBossStateType.MoveBackAction); }
        //if (chooseState >= 60 && chooseState < 70) { owner.ChangeState(EnemyBossStateType.JumpAction); }
        //if (chooseState >= 70 && chooseState <= 80) { owner.ChangeState(EnemyBossStateType.RollAttackAction); }

        owner.ChangeState(EnemyBossStateType.JumpAction);
    }

    public override void Exit()
    {

    }
}
