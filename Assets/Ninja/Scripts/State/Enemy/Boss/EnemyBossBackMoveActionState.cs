using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossBackMoveActionState : State<EnemyBoss>
{
    private Vector3 target;
    private float jumpForce;
    private float localGravity = -9.81f * 2.5f;

    private bool jumpFlag;

    private bool colFlag;

    private Ando.SoundEffectObject seObj;

    public EnemyBossBackMoveActionState(EnemyBoss owner) : base(owner) { }
    public override void Enter()
    {
        target = owner.transform.position + owner.transform.rotation * Vector3.back * 3;
        Debug.Log("back");
        colFlag = false;
        jumpForce = 0;
        owner.animator.SetBool("MoveBack", true);
    }

    public override void Execute()
    {
        //if (!jumpFlag)
        //{
        //    if (owner.LookTo(new Vector3(Enemy.player.transform.position.x, owner.transform.position.y, Enemy.player.transform.position.z)))
        //    {
        //        if (owner.MoveTo(target))
        //        {
        //            owner.ChangeState(EnemyBossStateType.Wait);
        //        }
        //    }
        //}

        //if (!colFlag)
        //{
        //    if (owner.CollisionObject)
        //    {
        //        jumpFlag = true;
        //        colFlag = true;
        //        jumpForce = 20f;
        //    }
        //}

        //OnStartPosJump();
        //owner.UseGravity();
        if (seObj == null)
        {
            //  SEを再生
            seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS1_MOVE, owner.transform.position);
            seObj.transform.parent = owner.transform;
        }
        if (!colFlag)
        {
            if (owner.LookTo(new Vector3(Enemy.player.transform.position.x, owner.transform.position.y, Enemy.player.transform.position.z)))
            {


                if (owner.MoveTo(target))
                {
                    if(seObj!= null)
                    {
                        //  SEを停止
                        seObj.SoundStop();
                    }
                    owner.ChangeState(EnemyBossStateType.Wait);
                }
            }
            if (owner.CollisionObject)
            {
                jumpForce = 30f;
                colFlag = true;
            }
        }

        if(colFlag)
        {
            OnStartPosJump();
        }

        owner.UseGravity();
    }

    public override void Exit()
    {
        owner.animator.SetBool("MoveBack", false);
    }

    public void OnStartPosJump()
    {
            owner.myRigidbody.AddForce((new Vector3(0f, jumpForce, 0f)), ForceMode.VelocityChange);
            jumpForce += Time.deltaTime * localGravity;

            owner.MoveTo(owner.transform.forward + Enemy.player.transform.position);
            if (owner.CollisionFloor)
            {
                if (owner.FlameWaitTime(3))
                {
                    owner.ChangeState(EnemyBossStateType.MoveAttackAction);
                }
            }
    }
}


