using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵(突撃)の待機ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/16
/// </summary>
namespace Kojima
{
    public class EnemyStrikeWaitState : State<Enemy>
    {
        #region メンバ変数

        private float waitTime = 2f;
        private float elapsedTime;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemyStrikeWaitState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            elapsedTime = 0;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            Debug.Log("WAIT");
            //時間の加算
            elapsedTime += Time.deltaTime;

            //"WaitTime"の間待機s
            if (elapsedTime > waitTime)
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
        }

        #endregion
    }
}