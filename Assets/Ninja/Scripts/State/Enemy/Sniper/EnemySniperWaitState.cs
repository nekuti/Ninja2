using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵(狙撃)の待機ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/16
/// </summary>
namespace Kojima
{
    public class EnemySniperWaitState : State<Enemy>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemySniperWaitState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
        }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public override void Execute()
        {
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