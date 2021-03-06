﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss2StajkingActionState : State<EnemyBoss>
{
    private Vector3 target;

    private Vector3 afterTarget;

    private bool actionFlag;

    private int count;

    private float rotSpeed = 1.5f;

    private bool setPos;

    private Vector3 enmey;

    private int stopCount;

    public Ando.SoundEffectObject seObj;

    public EnemyBoss2StajkingActionState(EnemyBoss owner) : base(owner) { }
    // Use this for initialization
    public override void Enter()
    {
        Debug.Log("追いかけステート");
        target = Enemy.player.transform.position;
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
        actionFlag = true;
        count = 0;
        setPos = true;
        stopCount = 0;
        afterTarget = Vector3.zero;

    }
    public override void Execute()
    {
        if (StopEnemy())
        {
            owner.transform.position = new Vector3(owner.transform.position.x, 3.0f, owner.transform.position.z);
        }
        else
        {
            target.y = owner.transform.position.y;
        }
        if (actionFlag)
        {
            if (owner.LookTo(target))
            {
                owner.animator.SetFloat("TurnFactor", 0.5f);
                owner.animator.SetBool("IsRunning", true);
                if ((target - owner.transform.position).magnitude > owner.enemyData.AttackableRange)
                {
                    if (seObj == null)
                    {
                        seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS2_MOVE, owner.transform.position);
                        seObj.transform.parent = owner.transform;
                    }
                    if (owner.QuickMoveTo(target))
                    {
                        if (seObj != null)
                        {
                            seObj.SoundStop();
                        }
                    }
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
            LookTo(afterTarget, rotSpeed);

            if (seObj == null)
            {
                seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS2_MOVE, owner.transform.position);
                seObj.transform.parent = owner.transform;
            }

            owner.QuickMoveTo(owner.transform.position + owner.transform.forward);
            if((owner.transform.position - afterTarget).magnitude < owner.enemyData.AttackableRange)
            {
                count++;
                afterTarget = Enemy.player.transform.position;
                Debug.Log("目的地" + afterTarget + "count" + count);
            }
        }


        if ((Enemy.player.transform.position - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
        {
            owner.ChangeState(EnemyBossStateType.B2NearAttackAction);
        }

        if (count >= 3)
        {
            owner.ChangeState(EnemyBossStateType.B2NearAttackAction);
        }

        if(owner.CollisionObject)
        {
            owner.ChangeState(EnemyBossStateType.B2MovePointAction);
        }
        //敵が止まった時の処理
        StopEnemy();

        owner.UseGravity();

    }

    public override void Exit()
    {
        if (seObj != null)
        {
            seObj.SoundStop();
        }
        owner.animator.SetBool("IsRunning", false);
    }



    //private void LookTo(Vector3 aPos,float speed)
    //{
    //    float step = speed * Time.deltaTime;
    //    Quaternion rotation = Quaternion.RotateTowards(owner, aPos, step);
    //}

    private bool LookTo(Vector3 aPos, float speed)
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

    private bool StopEnemy()
    {

        if (setPos)
        {
            enmey = owner.transform.position;
            setPos = false;
        }
        
        if(enmey == owner.transform.position)
        {
            Debug.Log("止まってます");
            stopCount ++;
        }

        if(stopCount > 60)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}