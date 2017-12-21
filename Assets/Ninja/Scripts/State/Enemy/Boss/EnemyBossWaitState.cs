using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

// 敵(ボス)待機ステート
//
//
//
public class EnemyBossWaitState : State<EnemyBoss>
{
    public EnemyBossWaitState(EnemyBoss owner) : base(owner) { }
    // 初期
    public override void Enter()
    {
        Debug.Log("待機ステート");
    }
    
    // 更新
    public override void Execute()
    {
        owner.animator.SetBool("MoveForward", false);
        owner.animator.SetBool("Jump", false);
        // 待機時間
        if (owner.FlameWaitTime(50))
        {
            // 探索ステートに移行
            owner.ChangeState(EnemyBossStateType.Choose);
        }
    }
    
    // 終了
    public override void Exit()
    {
       
    }
}
