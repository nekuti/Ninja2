using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;
////////////////////////////
//敵(ボス)の各ステート生成//
////////////////////////////
static class EnemyTypeBoss
{
    /// <summary>
    /// Enemyのボスステートを生成
    /// </summary>
    /// <param name="aSelf"></param>
    /// <param name="aOwner"></param>
    /// <returns></returns>
    public static State<EnemyBoss> CreateBossStandByState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossStandByState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBossWaitState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossWaitState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBossChooseState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossChooseState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBossJumpActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossJumpActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBossSummonActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossSummonActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBossMoveAttackActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossMoveAttackActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }

    public static State<EnemyBoss> CreateBossMoveBackActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossBackMoveAction(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }

    public static State<EnemyBoss> CreateBossRollAttackAction(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossRollAttackAction(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }

    public static State<EnemyBoss> CreateBossDamageState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossDamageState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBossDieState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss:
                return new EnemyBossDieState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
}