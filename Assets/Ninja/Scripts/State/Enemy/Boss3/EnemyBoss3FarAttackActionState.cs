using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3FarAttackActionState : State<EnemyBoss> {
    Vector3 targetPos;
    Vector3 targetLook;

    private bool lookFlag;

    GameObject attackLeft;
    GameObject attackRight;
    public EnemyBoss3FarAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        attackLeft = GameObject.Find("AttackPosLeft");
        attackRight = GameObject.Find("AttackPosRight");

        targetPos = Enemy.player.transform.position;
        targetLook = new Vector3(Enemy.player.transform.position.x, owner.transform.position.y, Enemy.player.transform.position.z);
        lookFlag = false;
    }

    public override void Execute()
    {
        if (owner.LookTo(targetLook))
        {
            lookFlag = true;
        }
        if (lookFlag)
        {
            owner.ShotAttack(attackLeft.transform.position, targetPos);
            owner.ShotAttack(attackRight.transform.position, targetPos);
            owner.ChangeState(EnemyBossStateType.Wait);
        }
    }

    public override void Exit()
    {

    }
}
