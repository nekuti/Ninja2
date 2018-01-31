using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss3MoveActionState : State<EnemyBoss>
{
    public Vector3 targetPos; 

    public EnemyBoss3MoveActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        targetPos = owner.Point(Random.Range(0, 360), 10) + owner.transform.position;
    }

    public override void Execute()
    {
        Debug.Log("移動攻撃ステート");
        targetPos.y = owner.transform.position.y;
        if ((Enemy.player.transform.position - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
        {
            owner.ChangeState(EnemyBossStateType.B3MoveAttackAction);
        }
        if (owner.LookTo(targetPos))
        {
            if (owner.MoveTo(targetPos))
            {
                owner.ChangeState(EnemyBossStateType.B3MoveAttackAction);
            }
        }
        if (owner.FlameWaitTime(10))
        {
            if (owner.CollisionObject)
            {
                owner.ChangeState(EnemyBossStateType.B3MoveAttackAction);
            }
        }
    }

    public override void Exit()
    {

    }
}
