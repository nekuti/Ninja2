using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵(遊撃)の追跡ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/16
/// </summary>
namespace Kojima
{
    public class EnemyAssaultChaseState : State<Enemy>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemyAssaultChaseState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("敵(遊撃)が追跡ステートへ遷移");
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // プレイヤーと自身の距離を求める
            Vector3 distance = Enemy.player.transform.position - owner.transform.position;

            // 攻撃範囲にプレイヤーがいるか
            if(distance.magnitude < owner.enemyData.AttackableRange)
            {
                // いる場合攻撃ステートに移行
                owner.ChangeState(EnemyStateType.Attack);
            }
            else
            {
                if (owner.LookTo(Enemy.player.transform.position))
                {
                    // いない場合プレイヤーの座標へ向かって移動させる
                    owner.MoveTo(Enemy.player.transform.position);
                    // プレイヤーの方を向かせる
                    //owner.LookTo(Enemy.player.transform.position);
                }
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            Debug.Log("敵(遊撃)が追跡ステートを終了");
        }

        #endregion
    }
}