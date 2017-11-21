using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵(遊撃)の巡回ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/15
/// </summary>
namespace Kojima
{
    public class EnemyAssaultPatrolState : State<Enemy>
    {
        #region メンバ変数

        private Vector3 Target;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemyAssaultPatrolState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("敵(遊撃)が巡回ステートへ遷移");
            //目的地の設定
            Target = Point(Random.Range(0, 360), owner.enemyData.PatrolArea) + owner.transform.position;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            Target.y = owner.transform.position.y;
            //目的地に着いたら待機に遷移
            if (owner.MoveTo(Target))
            {
                //待機ステートへ移行
                owner.ChangeState(EnemyStateType.Wait);
            }
            else
            {
                //目的地まで移動
                owner.MoveTo(Target);
                // 目的地の方を向かせる
                owner.LookTo(Target);
            }

          

            // プレイヤーと自身の距離を求める
            Vector3 distance = owner.player.transform.position - owner.transform.position;

            // 索敵範囲にプレイヤーが入った場合
            if (distance.magnitude < owner.enemyData.SearchRange)
            {
                // 追跡ステートへ移行
                owner.ChangeState(EnemyStateType.Chase);
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            Debug.Log("敵(遊撃)が巡回ステートを終了");
        }

        public Vector3 Point(float angle,float radius)
        {
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            return new Vector3(x,0,z);
        }

        #endregion
    }
}