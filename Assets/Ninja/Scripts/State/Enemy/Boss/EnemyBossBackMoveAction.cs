using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossBackMoveAction : State<EnemyBoss>
{

    private Vector3 target;
    private float jumpForce;
    private float localGravity = -9.81f * 2.5f;

    private bool jumpFlag;

    public EnemyBossBackMoveAction(EnemyBoss owner) : base(owner) { }
    public override void Enter()
    {
        target = owner.transform.position + owner.transform.rotation * Vector3.back * 3;
        Debug.Log("back");
        jumpFlag = false;
        owner.animator.SetBool("MoveBack", true);
    }

    public override void Execute()
    {
        if (owner.LookTo(new Vector3(Enemy.player.transform.position.x,owner.transform.position.y,Enemy.player.transform.position.z)))
        {
            if (owner.MoveTo(target))
            {
                owner.ChangeState(EnemyBossStateType.Wait);
            }

            if (owner.CollisionObject)
            {
                owner.animator.SetBool("MoveBack", false);
                jumpFlag = true;
                jumpForce = 30f;
            }
        }
        OnStartPosJump();
    }

    public override void Exit()
    {
        //jumpFlag = false;
    }

    public void OnStartPosJump()
    {
        if(jumpFlag)
        {
            owner.myRigidbody.AddForce((new Vector3(0f, jumpForce, 0f)), ForceMode.VelocityChange);
            jumpForce += Time.deltaTime * localGravity;

            owner.MoveTo(owner.transform.forward + Enemy.player.transform.position);
            if (owner.CollisionFloor)
            {
                if (owner.FlameWaitTime(2))
                {
                    owner.ChangeState(EnemyBossStateType.Wait);
                }
            }


        }
    }
}
