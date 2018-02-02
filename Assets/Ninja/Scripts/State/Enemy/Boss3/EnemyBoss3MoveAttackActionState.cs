using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss3MoveAttackActionState : State<EnemyBoss>
{
    private Vector3 targetLook;

    GameObject attackLeft;
    GameObject attackRight;

    private bool lookFlag;

    private int burstCount;

    private int freamCount;

    public EnemyBoss3MoveAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        attackLeft = GameObject.Find("AttackPosLeft");
        attackRight = GameObject.Find("AttackPosRight");
        targetLook = new Vector3(Enemy.player.transform.position.x, owner.transform.position.y, Enemy.player.transform.position.z);
        lookFlag = false;
        burstCount = 0;
        freamCount = 0;
    }

    public override void Execute()
    {
        if(owner.LookTo(targetLook))
        {
            lookFlag = true;
        }
        if(lookFlag)
        {
            freamCount++;
            if (freamCount > 18)
            {
                owner.ShotAttack(attackLeft.transform.position, Enemy.player.transform.position);
                owner.ShotAttack(attackRight.transform.position, Enemy.player.transform.position);
                Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_ASSALT_SHOT, owner.transform.position);
                burstCount++;
                freamCount = 0;
            }
 
            }

            if (burstCount >= 3)
            {
                owner.ChangeState(EnemyBossStateType.Wait);
            }
    }

    public override void Exit()
    {

    }
}
