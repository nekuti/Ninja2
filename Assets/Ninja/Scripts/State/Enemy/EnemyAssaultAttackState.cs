using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            // タイマーをリセット
            toTimer = 0f;
            afterTimer = 0f;
            attackFlg = false;
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
                    Attack.Create(owner.AttackPrefab, owner.transform.position, owner.transform.position + owner.transform.forward, owner.enemyData.Power, owner.tag);
                    attackFlg = true;
                }
                else
                {
                    toTimer += Time.deltaTime;
                }
            }
            else
            {
                // 攻撃発生後の処理
                if(afterTimer > owner.enemyData.AttackAfterTime)
                {
                    // 待機ステートへ移行
                    owner.ChangeState(EnemyStateType.Wait);
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