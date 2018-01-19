using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2ChooseState : State<EnemyBoss> {

    public EnemyBoss2ChooseState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("選択ステート"); 
    }
    public override void Execute()
    {
        //owner.ChangeState(EnemyBossStateType.B2NearAction);
        owner.ChangeState(EnemyBossStateType.B2StalkingAction);
    }

    public override void Exit()
    {

    }
}
