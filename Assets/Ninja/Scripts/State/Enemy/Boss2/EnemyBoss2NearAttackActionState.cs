using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2NearAttackActionState : State<EnemyBoss> {

    public EnemyBoss2NearAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("近接攻撃");
        owner.animator.SetBool("Jump",true);
    }

    public override void Execute()
    {
        owner.ChangeState(EnemyBossStateType.Wait);
    }

    public override void Exit()
    {
        owner.animator.SetBool("Jump", false);
    }
}
