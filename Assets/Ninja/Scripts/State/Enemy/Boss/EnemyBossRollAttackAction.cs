using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossRollAttackAction : State<EnemyBoss> {

    public float Speed = 3.0f;
    public float angle = 360f;

    public EnemyBossRollAttackAction(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        
    }

    public override void Execute()
    {
       owner.transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime * Speed, Space.Self);
        if(owner.FlameWaitTime(10))
        {
            owner.ShotAttack(owner.transform.position + new Vector3(0,1,0) ,owner.Point(Random.Range(0,360),10) + new Vector3(0,Random.Range(0,5),0));
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
