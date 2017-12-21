using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossStandByState : State<EnemyBoss>
{
    public EnemyBossStandByState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("目覚めていません");
    }

    public override void Execute()
    {
        Vector3 distance = Enemy.player.transform.position - owner.transform.position;
        //if (distance.magnitude < owner.enemyData.SearchRange)
        {
            // 追跡ステートへ移行
            owner.ChangeState(EnemyBossStateType.Wait);
        }
    }

    public override void Exit()
    {
        owner.animator.SetBool("StandBy", false);
    }
}
