using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3NearActionState : State<EnemyBoss> {
    private Vector3 targetPos;
    private Vector3 targetLook;

    private bool lookFlag;
    public EnemyBoss3NearActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("近距離攻撃ステート");
        targetPos = Enemy.player.transform.position;
        targetLook = new Vector3(Enemy.player.transform.position.x,owner.transform.position.y,Enemy.player.transform.position.z);
        lookFlag = false;
    }

    public override void Execute()
    {
        targetPos.y = owner.transform.position.y;
        targetLook.y = owner.transform.position.y;
       
        if (owner.LookTo(targetLook))
        {
            lookFlag = true;
        }
        if(lookFlag)
        {
            owner.MoveTo(targetPos);
            if((Enemy.player.transform.position - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
            {
                owner.ChangeState(EnemyBossStateType.B3NearAttackAction);
            }

        }
    }

    public override void Exit()
    {

    }
}
