using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBossStandByState : State<EnemyBoss>
{

    public EnemyBossStandByState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("目覚めていません");
        Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS1_STARTUP, owner.transform.position);
    }

    public override void Execute()
    {
        if (owner.SecondWaitime(2))
        {
            // 追跡ステートへ移行
            owner.ChangeState(EnemyBossStateType.Wait);
        }
    }

    public override void Exit()
    {
        owner.animator.SetBool("StandBy", false);
    }
}
