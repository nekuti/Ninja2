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

        #region メソッド

        /// <summary>
        /// レイが当たった
        /// </summary>
        public void HitRayObject()
        {
            if(hitRayEvent.GetPersistentEventCount() > 0)
            {
                hitRayEvent.Invoke();
                Debug.Log("レイが当たった時のイベントを実行");
            }
            else
            {
                Debug.Log("レイが当たった時のイベントが未登録");
            }
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
            if (outRayEvent.GetPersistentEventCount() > 0)
            {
                hitRayEvent.Invoke();
                Debug.Log("レイがはずれた時のイベントを実行");
            }
            else
            {
                Debug.Log("レイがはずれた時のイベントが未登録");
            }
        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {
            if (selectEvent.GetPersistentEventCount() > 0)
            {
                hitRayEvent.Invoke();
                Debug.Log("決定時のイベントを実行");
            }
            else
            {
                Debug.Log("決定時のイベントが未登録");
            }
        }

        #endregion
    }
}