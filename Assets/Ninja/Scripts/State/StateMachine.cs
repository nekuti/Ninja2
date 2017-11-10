using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stateを管理するクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/06
/// </summary>
namespace Kojima
{
    public class StateMachine<T>
    {
        #region メンバ変数

        // 現在のState
        private State<T> currentState;

        #endregion

        #region メソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StateMachine()
        {
            currentState = null;
        }

        public State<T> CurrentState
        {
            get { return currentState; }
        }

        /// <summary>
        /// Stateを遷移
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State<T> state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = state;
            currentState.Enter();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
        }

        #endregion
    }
}