using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossRollAttackActionState : State<EnemyBoss> {

    private float speed = 3.0f;
    private float angle = 360f;

    private bool playerAttack;

    public EnemyBossRollAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        playerAttack = true;
    }

    public override void Execute()
    {
       owner.transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime * speed, Space.Self);
        {
            owner.ShotAttack(owner.transform.position + new Vector3(0,3,0) , owner.Point(Random.Range(0,360),10) + owner.transform.position + new Vector3(0,Random.Range(3,8),0));
        }
        if(playerAttack)
        {
            owner.ShotAttack(owner.transform.position + new Vector3(0, 3, 0),Enemy.player.transform.position);
            Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS1_ATTACK2, owner.transform.position);
            playerAttack = false;
        }

        if (owner.SecondWaitime(2))
        {
            owner.ChangeState(EnemyBossStateType.Wait);
        }
    }

    public override void Exit()
    {
        
    }

}
