using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stateのクラス
/// 各ステートが継承して使用
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/06
/// </summary>
namespace Kojima
{
    public class State<T>
    {
        #region メンバ変数

        // このステートを利用するインスタンス
        protected T owner;

        #endregion

        #region メソッド
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner"></param>
        public State(T owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// このステートに遷移する時に一度だけ呼ばれる
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// このステートである間呼ばれ続ける
        /// </summary>
        public virtual void Execute() { }

        /// <summary>
        /// このステートから他のステートに遷移するときに一度だけ呼ばれる
        /// </summary>
        public virtual void Exit() { }

        #endregion
    }
}