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
            case EnemyType.Boss2:
                return new EnemyBoss2StandByState(aOwner);
            case EnemyType.Boss3:
                return new EnemyBoss3StandByState(aOwner);
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
            case EnemyType.Boss2:
                return new EnemyBoss2WaitState(aOwner);
            case EnemyType.Boss3:
                return new EnemyBoss3WaitState(aOwner);
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
            case EnemyType.Boss2:
                return new EnemyBoss2ChooseState(aOwner);
            case EnemyType.Boss3:
                return new EnemyBoss3ChooseState(aOwner);
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
                return new EnemyBossBackMoveActionState(aOwner);
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
                return new EnemyBossRollAttackActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBoss2NearActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss2:
                return new EnemyBoss2NearActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBoss2FarAttackActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss2:
                return new EnemyBoss2FarAttackActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBoss2NearAttackActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss2:
                return new EnemyBoss2NearAttackActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }

    public static State<EnemyBoss> CreateBoss2StalkingActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss2:
                return new EnemyBoss2StajkingActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }

    public static State<EnemyBoss> CreateBoss2MovePointActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss2:
                return new EnemyBoss2MovePointActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }

    public static State<EnemyBoss> CreateBoss3NearActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss3:
                return new EnemyBoss3NearActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBoss3FarAttackActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss3:
                return new EnemyBoss3FarAttackActionState(aOwner);
            default:
                Debug.Log("DieStateが未設定の敵");
                return null;
        }
    }
    public static State<EnemyBoss> CreateBoss3NearAttackActionState(this EnemyType aSelf, EnemyBoss aOwner)
    {
        switch (aSelf)
        {
            case EnemyType.Boss3:
                return new EnemyBoss3NearAttackActionState(aOwner);
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