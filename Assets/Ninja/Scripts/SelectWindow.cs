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


        private bool isSetHitRay = false;
        private bool isSetOutRay = false;
        private bool isSetSelect = false;



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
            if(hitRayEvent.GetPersistentEventCount() > 0)
            {
                hitRayEvent.Invoke();
                Debug.Log("レイが当たった時のイベントを実行");
            }
            else
            {
                Debug.Log("レイが当たった時のイベントが未登録");
                DynamicHitRayObject();
            }
        }

        /// <summary>
        /// レイが外れた
        /// </summary>
        public void OutRayObject()
        {
            if (outRayEvent.GetPersistentEventCount() > 0)
            {
                outRayEvent.Invoke();
                Debug.Log("レイがはずれた時のイベントを実行");
            }
            else
            {
                Debug.Log("レイがはずれた時のイベントが未登録");
                DynamicOutRayObject();
            }
        }

        /// <summary>
        /// 決定がされた
        /// </summary>
        public void SelectObject()
        {

            Debug.Log("selectEvet数 : "+selectEvent.GetPersistentEventCount());
            if (selectEvent.GetPersistentEventCount() > 0)
            {
                selectEvent.Invoke();
                Debug.Log("決定時のイベントを実行");
            }
            else
            {
                Debug.Log("決定時のイベントが未登録");
                DynamicSelectObject();
            }
        }



        public void DynamicHitRayObject()
        {
            if (isSetHitRay)
            {
                hitRayEvent.Invoke();
                Debug.Log("動的に設定されたレイが当たった時のイベントを実行");
            }
            else
            {
                Debug.Log("動的に設定されたレイが当たった時のイベントが未登録");
                DynamicSelectObject();
            }
        }

        public void DynamicOutRayObject()
        {
            if (isSetOutRay)
            {
                outRayEvent.Invoke();
                Debug.Log("動的に設定されたレイがはずれた時のイベントを実行");
            }
            else
            {
                Debug.Log("動的に設定されたレイがはずれた時のイベントが未登録");
            }
        }

        public void DynamicSelectObject()
        {
            if (isSetSelect)
            {
                selectEvent.Invoke();
                Debug.Log("動的に設定された決定時のイベントを実行");
            }
            else
            {
                Debug.Log("動的に設定された決定時のイベントが未登録");
            }
        }


        public void SetDynamicHitRayEvent(UnityEngine.Events.UnityAction aFuncName)
        {
            hitRayEvent.AddListener(aFuncName);
            isSetHitRay = true;
        }

        public void SetDynamicOutRayEvent(UnityEngine.Events.UnityAction aFuncName)
        {
            outRayEvent.AddListener(aFuncName);
            isSetOutRay = true;
        }

        public void SetDynamicSelectEvent(UnityEngine.Events.UnityAction aFuncName)
        {
            selectEvent.AddListener(aFuncName);
            isSetSelect = true;
        }


        #endregion
    }
}