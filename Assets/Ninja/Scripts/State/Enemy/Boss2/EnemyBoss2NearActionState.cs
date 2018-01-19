using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2NearActionState :  State<EnemyBoss>{
    Vector3 target;

    bool waitFlag = false;

    
    public EnemyBoss2NearActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("接近");
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
        //target = Enemy.player.transform.position - owner.transform.position;
    }

    public override void Execute()
    {
        Vector3 distance = Enemy.player.transform.position - owner.transform.position;
        target.y = owner.transform.position.y;

        if (owner.LookTo(target))
        {
            owner.animator.SetFloat("TurnFactor", 0.5f);
            if (owner.SecondWaitime(2))
            {
                waitFlag = true;
            }

            if ((target - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
            {
                owner.animator.SetBool("IsRunning", false);
                waitFlag = false;
                owner.ChangeState(EnemyBossStateType.B2NearAttackAction);
            }

            if (waitFlag)
            {
                owner.animator.SetBool("IsRunning", true);
                owner.QuickMoveTo(target);
            }
        }
        
    }

    public override void Exit()
    {
        
    }

    private bool Where()
    {
        GameObject left = GameObject.Find("Left");

        GameObject right = GameObject.Find("Right");

        Vector3 leftDis = left.transform.position - Enemy.player.transform.position;
        Vector3 rightDis = right.transform.position - Enemy.player.transform.position;

        if(leftDis.magnitude > rightDis.magnitude)
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


}
