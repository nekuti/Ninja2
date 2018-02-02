using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss3MoveActionState : State<EnemyBoss>
{
    private Vector3 targetPos; 

    private bool lookFlag;

    private Ando.SoundEffectObject seObj;

    public EnemyBoss3MoveActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        targetPos = owner.Point(Random.Range(0, 360), 10) + owner.transform.position;
        lookFlag = false;
    }

    public override void Execute()
    {
        Debug.Log("移動攻撃ステート");
        targetPos.y = owner.transform.position.y;
        if ((Enemy.player.transform.position - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
        {
            owner.ChangeState(EnemyBossStateType.B3MoveAttackAction);
        }
        else
        {
            if (owner.LookTo(targetPos))
            {
                lookFlag = true;
            }
            if(lookFlag)
            {
                if (seObj == null)
                {
                    seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_BOSS3_MOVE, owner.transform.position);
                    seObj.transform.parent = owner.transform;
                }
                if (owner.MoveTo(targetPos))
                {
                    owner.ChangeState(EnemyBossStateType.B3MoveAttackAction);
                }


                if (owner.CollisionObject)
                {
                    if (owner.FlameWaitTime(2))
                    {
                        owner.ChangeState(EnemyBossStateType.B3MoveAttackAction);
                    }
                }
            }
            
        }

    }

    public override void Exit()
    {
        if (seObj != null)
        {
            seObj.SoundStop();
        }
    }
}
