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
        chooseState = Random.Range(0, 100);
        Debug.Log(chooseState);
    }

    public override void Execute()
    {
        if (chooseState >= 0 && chooseState < 60) { owner.ChangeState(EnemyBossStateType.MoveAttackAction); }  // 60%
        if (chooseState >= 60 && chooseState <= 76) { owner.ChangeState(EnemyBossStateType.MoveBackAction); }     // 16%
        if (chooseState > 76 && chooseState <= 88) { owner.ChangeState(EnemyBossStateType.JumpAction); }        // 12%
        if (chooseState > 88 && chooseState <= 100) { owner.ChangeState(EnemyBossStateType.RollAttackAction); } // 12%
    }

    public override void Exit()
    {
        
    }
}
