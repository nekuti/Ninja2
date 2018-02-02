using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kojima;

public class EnemyBoss2DieState : State<EnemyBoss>
{
    public EnemyBoss2DieState(EnemyBoss owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("bossが死亡ステートへ遷移");
        //owner.gameObject.SetActive(false);
        //GameObject.Destroy(owner.gameObject);
        ParticleEffect.Create(ParticleEffectType.Explosion02, owner.transform.position);
        Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_BOSS2_EXPLODE, owner.transform.position);
    }

    public override void Execute()
    {
        GameObject.Destroy(owner.gameObject);
    }

    public override void Exit()
    {

    }
}
