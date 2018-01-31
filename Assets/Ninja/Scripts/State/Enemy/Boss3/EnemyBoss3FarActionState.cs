using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss3FarActionState : State<EnemyBoss>
{
    private Vector3 targetPos;
    private Vector3 targetLook;

    private bool lookFlag;
    private bool posFlag;
    public EnemyBoss3FarActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("遠距離攻撃ステート");
        //targetPos = owner.transform.position + owner.transform.rotation * Vector3.back * 5;
        targetLook = new Vector3(Enemy.player.transform.position.x, owner.transform.position.y, Enemy.player.transform.position.z);
        lookFlag = false;
        posFlag = true;
    }

    public override void Execute()
    {
        targetLook.y = owner.transform.position.y;
        if (owner.LookTo(targetLook))
        {
            lookFlag = true;
            if (posFlag)
            {
                targetPos = owner.transform.position + owner.transform.rotation * Vector3.back * 5;
                posFlag = false;
            }
        }
        if (lookFlag)
        {
            if (owner.MoveTo(targetPos))
            {
               owner.ChangeState(EnemyBossStateType.B3FarAttackAction);
            }
            else if (owner.CollisionObject)
            {
                owner.ChangeState(EnemyBossStateType.B3FarAttackAction);
            }
        }



    }

    public override void Exit()
    {

    }
}
