using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossJumpActionState : State<EnemyBoss>
{
    public float localGravity = -9.81f * 2.5f;

    private float jumpForce;

    public EnemyBossJumpActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("ジャンプ");
        owner.animator.SetBool("Jump", true);
        jumpForce = 30f;
    }

    public override void Execute()
    {

        if (owner.FlameWaitTime(30))
        {
            owner.animator.SetBool("Jump", false);
        }

        if (!owner.animator.GetBool("Jump"))
        {
            owner.myRigidbody.AddForce((new Vector3(0f, jumpForce, 0f)), ForceMode.VelocityChange);
            jumpForce += Time.deltaTime * localGravity;
            if (owner.FlameWaitTime(2))
            {
                owner.animator.SetBool("Landing", true);
                if (owner.CollisionFloor)
                {
                    owner.ChangeState(EnemyBossStateType.Wait);
                }
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("発射");
        for (int i = 0; i < 30; i++)
        {
            owner.ShotAttack(new Vector3(0,1,0) + owner.transform.position ,owner.Point(i * 12, 10) + owner.transform.position + new Vector3 (0,1,0));
        }
        owner.animator.SetBool("Landing", false);
    }
}
