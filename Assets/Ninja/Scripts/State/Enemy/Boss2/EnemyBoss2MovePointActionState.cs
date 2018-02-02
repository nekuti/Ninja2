using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss2MovePointActionState : State<EnemyBoss>
{
    private Vector3 target;

    private Ando.SoundEffectObject seObj;
    public EnemyBoss2MovePointActionState(EnemyBoss owner) : base(owner) { }
	// Use this for initialization
	public override void Enter() {
        target = owner.Point(Random.Range(0, 360),10) + owner.transform.position;
        if ((target - owner.transform.position).magnitude > owner.enemyData.AttackableRange)
        {
            if (owner.ObjectWhere("Left", "Right"))
            {
                owner.animator.SetFloat("TurnFactor", 0f);
            }
            else
            {
                owner.animator.SetFloat("TurnFactor", 1f);
            }
        }
        else
        {
            owner.animator.SetFloat("TurnFactor", 0.5f);
        }
       
    }

    // Update is called once per frame
    public override void Execute()
    {
        target.y = owner.transform.position.y;
        if (LookTo(target,3f))
        {
            owner.animator.SetFloat("TurnFactor", 0.5f);
            owner.animator.SetBool("IsRunning", true);
            if (seObj == null)
            {
                seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS2_MOVE, owner.transform.position);
                seObj.transform.parent = owner.transform;
            }
            if (owner.QuickMoveTo(target))
            {
                owner.ChangeState(EnemyBossStateType.B2StalkingAction);
            }
            else
            {
                if (owner.CollisionObject)
                {
                    if (owner.FlameWaitTime(2))
                    {
                        owner.ChangeState(EnemyBossStateType.B2StalkingAction);
                    }
                }
            }
        }
 

    }

    public override void Exit()
    {
        owner.animator.SetBool("IsRunning", false);
        if (seObj != null)
        {
            seObj.SoundStop();
        }
    }

    public bool LookTo(Vector3 aPos, float speed)
    {
        Quaternion lookRotate = Quaternion.LookRotation(aPos - owner.myRigidbody.position);
        Quaternion rotation = Quaternion.RotateTowards(owner.transform.rotation, lookRotate, speed);
        owner.transform.rotation = rotation;

        if (owner.transform.rotation == lookRotate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
