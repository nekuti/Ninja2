using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kojima;

public class EnemyBoss2NearAttackActionState : State<EnemyBoss> {
    private Transform top;
    private bool endLook;

    GameObject attackHandL;
    GameObject attackHandR;
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
        attackHandL = GameObject.Find("hand.L");
        attackHandR = GameObject.Find("hand.R");
        Debug.Log(attackHandR.transform.position);

    }

    public override void Execute()
    {
        owner.UseGravity();
        if (endLook)
        {
            owner.animator.SetBool("Jump", true);
            if (owner.animator.GetAnimationProgress("Jump") > 0.6f)
            {
                //Enemy.Instantiate(owner.AttackPrefab, top.position, Quaternion.identity);
                var attack_L = Attack.Create(owner.AttackPrefab, attackHandL.transform.position, owner.transform.forward, owner.enemyData.Power, owner.tag);
                var attack_R = Attack.Create(owner.AttackPrefab, attackHandR.transform.position, owner.transform.forward, owner.enemyData.Power, owner.tag);
                attack_L.transform.parent = attackHandL.transform;
                attack_R.transform.parent = attackHandR.transform;

                //Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS2_ATTACK, owner.transform.position);

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
