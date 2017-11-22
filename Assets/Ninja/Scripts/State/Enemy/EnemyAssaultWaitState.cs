using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵(遊撃)の待機ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/16
/// </summary>
namespace Kojima
{
    public class EnemyAssaultWaitState : State<Enemy>
    {
        #region メンバ変数

        private float WaitTime = 2f;
        private float ElapsedTime;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemyAssaultWaitState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("敵(遊撃)が待機ステートへ遷移");
            //時間を初期に戻す
            ElapsedTime = 0;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {

            // プレイヤーと自身の距離を求める
            // Vector3 distance = owner.player.transform.position - owner.transform.position;

            // 索敵範囲にプレイヤーが入った場合
            //if(distance.magnitude < owner.enemyData.SearchRange)
            // {

            //時間の加算
            ElapsedTime += Time.deltaTime;

            //"WaitTime"の間待機
            if (ElapsedTime > WaitTime)
            { 
                // 追跡ステートへ移行
                owner.ChangeState(EnemyStateType.Patrol);
            }
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            Debug.Log("敵(遊撃)が待機ステートを終了");
        }

        #endregion
    }
}