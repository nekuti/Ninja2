using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss2StajkingActionState : State<EnemyBoss>
{
    private Vector3 target;

    private Vector3 afterTarget;

    private bool actionFlag;

    private int count;

    public EnemyBoss2StajkingActionState(EnemyBoss owner) : base(owner) { }
    // Use this for initialization
    public override void Enter()
    {
        target = Enemy.player.transform.position;
        if ((target - owner.transform.position).magnitude > owner.enemyData.AttackableRange)
        {
            if (Where())
            {
                owner.animator.SetFloat("TurnFactor", 1f);
            }
            else
            {
                owner.animator.SetFloat("TurnFactor", 0f);
            }
        }
        else
        {
            owner.animator.SetFloat("TurnFactor", 0.5f);
        }
        actionFlag = true;
        count = 0;
        afterTarget = Vector3.zero;
    }
    public override void Execute()
    {
        target.y = owner.transform.position.y;
        if (actionFlag)
        {
            if (owner.LookTo(target))
            {
                owner.animator.SetFloat("TurnFactor", 0.5f);
                owner.animator.SetBool("IsRunning", true);
                if ((target - owner.transform.position).magnitude > owner.enemyData.AttackableRange)
                {
                    owner.QuickMoveTo(target);
                    afterTarget = Enemy.player.transform.position;
                    
                }
                else
                {
                    actionFlag = false;
                }
            }
        }
        else
        {
            afterTarget.y = owner.transform.position.y;
            LookTo(afterTarget, 1.5f);

            owner.QuickMoveTo(owner.transform.position + owner.transform.forward);
            if((afterTarget - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
            {
                count++;
                afterTarget = Enemy.player.transform.position;
            }
        }



        if ((Enemy.player.transform.position - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
        {
            owner.ChangeState(EnemyBossStateType.B2NearAttackAction);
        }

        if(count == 3)
        {
            owner.ChangeState(EnemyBossStateType.B2NearAttackAction);
        }


    }

    public override void Exit()
    {
        owner.animator.SetBool("IsRunning", false);
    }

    private bool Where()
    {
        GameObject left = GameObject.Find("Left");

        GameObject right = GameObject.Find("Right");

        Vector3 leftDis = left.transform.position - Enemy.player.transform.position;
        Vector3 rightDis = right.transform.position - Enemy.player.transform.position;

        if (leftDis.magnitude > rightDis.magnitude)
        {
            //右のが近い
            Debug.Log("右");
            return false;
        }
        else
        {
            //左のが近い
            Debug.Log("左");
            return true;
        }
    }


    //private void LookTo(Vector3 aPos,float speed)
    //{
    //    float step = speed * Time.deltaTime;
    //    Quaternion rotation = Quaternion.RotateTowards(owner, aPos, step);
    //}

    public bool LookTo(Vector3 aPos, float speed)
    {
        Quaternion lookRotate = Quaternion.LookRotation(aPos - owner.myRigidbody.position);
        Quaternion rotation = Quaternion.RotateTowards(owner.transform.rotation,lookRotate, speed);
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