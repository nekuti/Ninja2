﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

/// <summary>
/// 敵(突撃)の攻撃ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/16
/// </summary>
namespace Kojima
{
    public class EnemyAssaultAttackState : State<Enemy>
    {
        #region メンバ変数

        float toTimer;
        float afterTimer;
        bool attackFlg;     // 攻撃の発生フラグ
        private Vector3 target;
        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemyAssaultAttackState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("敵(遊撃)が攻撃ステートへ遷移");

            // プレイヤーの方を向かせる
            //owner.LookTo(Enemy.player.transform.position);
            
            // タイマーをリセット
            toTimer = 0f;
            afterTimer = 0f;
            attackFlg = false;
            target = Enemy.player.transform.position;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            if (!attackFlg)
            {
                // 攻撃発生前の処理
                if (toTimer > owner.enemyData.AttackToTime)
                {
                    // 正面に攻撃を生成
                    Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_ASSALT_SHOT, owner.transform.position);
                    owner.ShotAttack(target + new Vector3(0, -0.3f, 0));
                    attackFlg = true;
                    //if(owner.enemyData.Level == 2)
                    //{
                    //    GameObject.Destroy(owner.gameObject);
                    //}
                }
                else
                {
                    // プレイヤーの方を向かせる
                    owner.LookTo(Enemy.player.transform.position);
                    toTimer += Time.deltaTime;
                }
            }
            else
            {
                // 攻撃発生後の処理
                if (afterTimer > owner.enemyData.AttackAfterTime)
                {
                    // ダメージステートへ移行
                    owner.ChangeState(EnemyStateType.Damage);
                }
                else
                {
                    afterTimer += Time.deltaTime;
                }
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            Debug.Log("敵(遊撃)が攻撃ステートを終了");
        }

        #endregion
    }
}