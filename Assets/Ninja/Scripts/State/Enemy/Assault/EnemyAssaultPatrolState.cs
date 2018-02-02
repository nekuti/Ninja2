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

        private Vector3 target;

        private Ando.SoundEffectObject seObj;

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
            target = Point(Random.Range(0, 360), owner.enemyData.PatrolArea) + owner.transform.position;
            if (owner.CollisioDecision)
            {
                target = owner.transform.position + owner.transform.rotation * Vector3.back * owner.enemyData.PatrolArea;
                //owner.transform.LookAt(Target);
            }
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // プレイヤーと自身の距離を求める
            Vector3 distance = Enemy.player.transform.position - owner.transform.position;

            if (seObj == null)
            {
                seObj = Ando.AudioManager.Instance.PlaySE(AudioName.SE_ENEMY_ASSALT_WAIT, owner.transform.position);
                seObj.transform.parent = owner.transform;
            }

            // 索敵範囲にプレイヤーが入った場合
            if (distance.magnitude < owner.enemyData.SearchRange)
            {
                // 追跡ステートへ移行
                owner.ChangeState(EnemyStateType.Chase);
            }
            else
            {
                if (owner.LookTo(new Vector3(target.x, owner.transform.position.y, target.z)))
                {
                    Debug.Log("回転おわった");
                    //target.y = owner.transform.position.y;
                    //目的地に着いたら待機に遷移
                    if (owner.MoveTo(target))
                    {
                        Debug.Log("移動おわり");
                        //待機ステートへ移行
                        owner.ChangeState(EnemyStateType.Wait);
                    }
                    if (owner.FlameWaitTime(1))
                    {
                        if (owner.CollisioDecision)
                        {
                            Debug.Log("壁");
                            owner.ChangeState(EnemyStateType.Wait);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            Debug.Log("敵(遊撃)が巡回ステートを終了");
            if (seObj != null)
            {
                seObj.SoundStop();
            }
        }

        public Vector3 Point(float angle, float radius)
        {
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            return new Vector3(x, 0, z);
        }
        #endregion
    }
}