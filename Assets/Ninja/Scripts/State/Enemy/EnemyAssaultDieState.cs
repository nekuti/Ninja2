using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵(遊撃)の死亡ステート
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/16
/// </summary>
namespace Kojima
{
    public class EnemyAssaultDieState : State<Enemy>
    {
        #region メンバ変数

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public EnemyAssaultDieState(Enemy owner) : base(owner) { }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public override void Enter()
        {
            Debug.Log("敵(遊撃)が死亡ステートへ遷移");
            ParticleEffect.Create(ParticleEffectType.Explosion01, owner.transform.position);
            GameObject.Destroy(owner.gameObject);
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
            Debug.Log("敵(遊撃)が死亡ステートを終了");
        }

        #endregion
    }
}