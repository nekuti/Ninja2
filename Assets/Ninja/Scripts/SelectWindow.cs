using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SelectWindowのクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/11
/// </summary>
namespace Kojima
{
    public class SelectWindow : MonoBehaviour , ISelectable
    {
        #region メンバ変数

        // レイが当たった時に呼び出されるイベント
        [SerializeField]
        private UnityEngine.Events.UnityEvent hitRayEvent = new UnityEngine.Events.UnityEvent();

        // レイが外れた時に呼び出されるイベント
        [SerializeField]
        private UnityEngine.Events.UnityEvent outRayEvent = new UnityEngine.Events.UnityEvent();

        // 決定された時に呼ばれるイベント
        [SerializeField]
        private UnityEngine.Events.UnityEvent selectEvent = new UnityEngine.Events.UnityEvent();



        #endregion

        #region プロパティ
        public UnityEngine.Events.UnityEvent HitRayEvent { get { return hitRayEvent; } }
        public UnityEngine.Events.UnityEvent OutRayEvent { get { return outRayEvent; } }
        public UnityEngine.Events.UnityEvent SelectEvent { get { return selectEvent; } }
        #endregion

        #region メソッド

        /// <summary>
        /// レイが当たった
        /// </summary>
        public void HitRayObject()
        {
            hitRayEvent.Invoke();
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
            outRayEvent.Invoke();

        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {

            selectEvent.Invoke();

        }


        /// <summary>
        /// 動的にレイが当たった時のイベントを登録
        /// </summary>
        /// <param name="aFuncName"></param>
        public void SetDynamicHitRayEvent(UnityEngine.Events.UnityAction aFuncName)
        {
            hitRayEvent.AddListener(aFuncName);
        }

        /// <summary>
        /// 動的にレイが外れた時のイベントを登録
        /// </summary>
        /// <param name="aFuncName"></param>
        public void SetDynamicOutRayEvent(UnityEngine.Events.UnityAction aFuncName)
        {
            outRayEvent.AddListener(aFuncName);
        }

        /// <summary>
        /// 動的に選択された時のイベントを登録
        /// </summary>
        /// <param name="aFuncName"></param>
        public void SetDynamicSelectEvent(UnityEngine.Events.UnityAction aFuncName)
        {
            selectEvent.AddListener(aFuncName);
        }


        #endregion
    }
}