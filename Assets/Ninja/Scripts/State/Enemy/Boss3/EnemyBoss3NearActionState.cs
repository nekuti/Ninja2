using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss3NearActionState : State<EnemyBoss> {
    private Vector3 target;

    private bool lookFlag;

    private Ando.SoundEffectObject seObj;
    public EnemyBoss3NearActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("近距離攻撃ステート");
        target = Enemy.player.transform.position;
        lookFlag = false;
    }

    public override void Execute()
    {
        target.y = owner.transform.position.y;

        if((target - owner.transform.position).magnitude < 5)
        {
            owner.ChangeState(EnemyBossStateType.B3NearAttackAction);
        }

        if ((Enemy.player.transform.position - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
        {
            owner.ChangeState(EnemyBossStateType.B3NearAttackAction);
        }
        else
        {
            if (owner.LookTo(target))
            {
                lookFlag = true;
            }
            if (lookFlag)
            {
                if (seObj == null)
                {
                    seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_BOSS3_MOVE, owner.transform.position);
                    seObj.transform.parent = owner.transform;
                }
                owner.MoveTo(target);

                if (owner.CollisionObject)
                {
                    if (owner.FlameWaitTime(2))
                    {
                        owner.ChangeState(EnemyBossStateType.B2MovePointAction);
                    }
                }
            }
        }
    }

    public override void Exit()
    {
        if (seObj != null)
        {
            seObj.SoundStop();
        }
    }
}
