using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2NearAttackActionState : State<EnemyBoss> {
    private Transform top;
    private bool endLook;
    public EnemyBoss2NearAttackActionState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("近接攻撃");
        if ((Enemy.player.transform.position - owner.transform.position).magnitude > owner.enemyData.AttackableRange)
        {
            if (owner.ObjectWhere("Left", "Right"))
            {
                owner.animator.SetFloat("TurnFactor", 0f);
            }
            else
            {
                owner.animator.SetFloat("TurnFactor", 1f);
            }
        }
        else
        {
            owner.animator.SetFloat("TurnFactor", 0.5f);
        }
        top = owner.transform.Find("Hunter/Top");
        endLook = false;
    }

    public override void Execute()
    {
        owner.UseGravity();
        if (endLook)
        {
            owner.animator.SetBool("Jump", true);
            if (owner.animator.GetAnimationProgress("Jump") > 0.6f)
            {
                Enemy.Instantiate(owner.AttackPrefab, top.position, Quaternion.identity);
                owner.ChangeState(EnemyBossStateType.Wait);
                Debug.Log("こうげきいいいい");
            }
        }
        if ((Enemy.player.transform.position - owner.transform.position).magnitude < owner.enemyData.AttackableRange)
        {
            if (owner.LookTo(new Vector3 (Enemy.player.transform.position.x,owner.transform.position.y,Enemy.player.transform.position.z)))
            {
                endLook = true;
                owner.animator.SetFloat("TurnFactor", 0.5f);

            }
        }
        else
        {
            owner.ChangeState(EnemyBossStateType.Wait);
        }

    }

    public override void Exit()
    {
        owner.animator.SetBool("Jump", false);
    }
}
