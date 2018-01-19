using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossRollAttackActionState : State<EnemyBoss> {

    public float speed = 3.0f;
    public float angle = 360f;

    public EnemyBossRollAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        
    }

    public override void Execute()
    {
       owner.transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime * speed, Space.Self);
        //if(owner.FlameWaitTime(1))
        {
            owner.ShotAttack(owner.transform.position + new Vector3(0,3,0) , owner.Point(Random.Range(0,360),10) + owner.transform.position + new Vector3(0,Random.Range(3,8),0));
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
