using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3NearAttackActionState : State<EnemyBoss> {
    private Vector3 targetPos;
    private Vector3 targetLook;

    private bool lookFlag;

    GameObject attackLeft;
    GameObject attackRight;

    private Ando.SoundEffectObject seObj;
    public EnemyBoss3NearAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("Nea");
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

            if (seObj == null)
            {
                seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_ASSALT_SHOT, owner.transform.position);
                seObj.transform.parent = owner.transform;
            }

            owner.ChangeState(EnemyBossStateType.Wait);
        }
    }

    public override void Exit()
    {
        if(seObj != null)
        {
            seObj.SoundStop();
        }
    }
}
