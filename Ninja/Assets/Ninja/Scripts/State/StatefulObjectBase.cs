using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stateをもつクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/11/07
/// </summary>
namespace Kojima
{
    public abstract class StatefulObjectBase<T, TStateType> : MonoBehaviour
        where T : class
        where TStateType : struct
    {
        #region メンバ変数
        // 所持するステートのリスト
        [SerializeField]
        protected List<State<T>> stateList = new List<State<T>>();

        // ステートマシン
        [SerializeField]
        protected StateMachine<T> stateMachine;
        #endregion

        #region メソッド

        /// <summary>
        /// Stateの変更
        /// </summary>
        /// <param name="aState"></param>
        public virtual void ChangeState(TStateType aState)
        {
            if (stateMachine == null)
            {
                return;
            }
            stateMachine.ChangeState(stateList[((System.IConvertible)aState).ToInt32(null)]);
        }

        /// <summary>
        /// 現行のステートであるか
        /// </summary>
        /// <param name="aState"></param>
        /// <returns></returns>
        public virtual bool IsCurrentState(TStateType aState)
        {
            if (stateMachine == null)
            {
                return false;
            }
            return stateMachine.CurrentState == stateList[((System.IConvertible)aState).ToInt32(null)];
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        protected virtual void Update()
        {
            if (stateMachine != null)
            {
                stateMachine.Update();
            }
        }

        #endregion
    }
}