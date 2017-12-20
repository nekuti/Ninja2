using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵(遊撃)のダメージステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/16
/// </summary>
namespace Kojima
{
    public class EnemyAssaultDamageState : State<Enemy>
    {
        #region メンバ変数

        private float timer;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemyAssaultDamageState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("敵(遊撃)がダメージステートへ遷移");

            // タイマーをリセット
            timer = 0f;
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
            // ダメージ時の硬直時間が終われば待機ステートへ戻す
            //if (timer > owner.enemyData.KnockBackTime)
            //{
                owner.ChangeState(EnemyStateType.Wait);
            //
            //else
            //{
            //    timer += Time.deltaTime;
            //}
        }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public override void Exit()
        {
            Debug.Log("敵(遊撃)がダメージステートを終了");
        }

        #endregion
    }
}