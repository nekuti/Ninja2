using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossMoveAttackActionState : State<EnemyBoss>
{
    private Vector3 target;

    private bool shotFlag = true;

    private Vector3 attackSpace;

    private Ando.SoundEffectObject seObj;

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

        if (seObj == null)
        {
            seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS1_MOVE, owner.transform.position);
            seObj.transform.parent = owner.transform;
        }

        if (owner.MoveTo(target))
        {
            if(seObj!=null)
            {
                seObj.SoundStop();
            }
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
                    owner.ShotAttack(owner.transform.position + new Vector3 (0,3,0), Enemy.player.transform.position);
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS1_ATTACK1, owner.transform.position);
                    shotFlag = false;
                }
            }
        }
        owner.UseGravity();
    }

    public override void Exit()
    {

    }
}
