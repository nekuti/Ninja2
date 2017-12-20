using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossMoveAttackActionState : State<EnemyBoss>
{
    private Vector3 target;

    private bool shotFlag = true;

    private Vector3 attackSpace; 

    public EnemyBossMoveAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        target = owner.Point(Random.Range(0, 360), owner.enemyData.PatrolArea) + owner.transform.position;
        attackSpace = target - owner.transform.position;
        shotFlag = true;
    }

    public override void Execute()
    {
        owner.LookTo(new Vector3(Enemy.player.transform.position.x,owner.transform.position.y,Enemy.player.transform.position.z));

        if (owner.MoveTo(target))
        {
            owner.ChangeState(EnemyBossStateType.Wait);
        }
        else
        {
            if (owner.FlameWaitTime(1))
            {
                if (owner.CollisionObject)
                {
                    owner.ChangeState(EnemyBossStateType.Wait);
                }
            }

            owner.animator.SetBool("MoveForward", true);

            if (shotFlag)
            {
                if ((attackSpace / 2).magnitude >= (target - owner.transform.position).magnitude)
                {
                    Debug.Log("半分");
                    owner.ShotAttack(Enemy.player.transform.position);
                    shotFlag = false;
                }
            }
        }
    }

    public override void Exit()
    {

    }
}
