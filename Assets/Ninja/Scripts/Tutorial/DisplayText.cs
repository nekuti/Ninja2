using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Kojima;

namespace Kondo
{
    public class DisplayText : MonoBehaviour
    {
        public enum DisplaySequence
        {
            moveDisplay,
            stopDisplay,
            returnDisplay,
        }

        public Text mainText;
        public Text headLineText;
        public Image image;
        public SelectWindow selectWindow = new SelectWindow();


        private CanvasHide canvasHide;
        private UnityEngine.Events.UnityAction tempData;


        // Use this for initialization
        void Start()
        {
            Debug.Log("ディスプレイテキスト Start()");

            mainText = transform.Find("Main").GetComponent<Text>();
            headLineText = transform.Find("HeadLine").GetComponent<Text>();
            image = transform.Find("Image").GetComponent<Image>();
            selectWindow = transform.Find("SelectButton").GetComponent<SelectWindow>();
            Debug.Log("ディスプレイテキスト image :"+image);

            // DisplayTextの表示操作用変数
            canvasHide = transform.GetComponentInChildren<CanvasHide>();
            SetSelectEvent(test);
        }




        // Update is called once per frame
        void Update()
        {

        }

        public void test()
        {
            Debug.Log("Displaytest test()");
        }



        /// <summary>
        /// SelectWindow選択時に実行する関数をセットする
        /// </summary>
        /// <param name="aFuncName">関数の名前</param>
        public void SetSelectEvent(UnityEngine.Events.UnityAction aFuncsion)
        {

            selectWindow.SetDynamicSelectEvent(aFuncsion);
            //tempData = aFuncsion;
            //selectWindow.SelectEvent.AddListener(aFuncsion);
            //// selectWindow.SelectEvent.Invoke();
            //Debug.Log("自作した方のevent数 : "+selectWindow.SelectEvent.GetPersistentEventCount());
        }


        /// <summary>
        /// 登録したすべてのイベントを削除
        /// </summary>
        public void DeleteSelectEvent()
        {
            //selectWindow.SelectEvent.RemoveAllListeners();
        }



        /// <summary>
        /// ディスプレイを表示する
        /// </summary>
        /// <param name="aLayout"></param>
        /// <param name="aPos"></param>
        public void RequestDisplay(DisplayLayout aLayout, Transform aPos)
        {
            Debug.Log("ディスプレイテキスト RequestDisplay()");
            canvasHide.HideON();
            headLineText.text = aLayout.headLine;
            mainText.text = aLayout.mainText;
            transform.parent.position = aPos.position;
            image.sprite = aLayout.sprite;

            Debug.Log("ディスプレイテキスト image.sprite :" + aLayout.sprite);
        }



        public void HideDisplay()
        {
            canvasHide.HideOFF();
        }




        //private void InstructMovement()
        //{

        //    switch (sequence)
        //    {
        //        case DisplaySequence.moveDisplay:

        //            GoDisplay();
        //            break;

        //        case DisplaySequence.stopDisplay:

        //            StopDisplay();
        //            break;

        //        case DisplaySequence.returnDisplay:

        //            ReturnDisplay();
        //            break;
        //    }
        //}


        //private void GoDisplay()
        //{
        //    // 目標座標に移動させる
        //    canvasTransfome.DOMove(targetPos, moveTime)
        //        .OnComplete(() =>
        //        {
        //                // 移動が終了時に一時停止シーケンスに移行
        //                sequence = NoticeSequence.stopNotice;
        //        });
        //    canvasTransfome.rotation = eye.transform.rotation;
        //}


        //private void StopDisplay()
        //{
        //    countTime += Time.deltaTime;
        //    if (countTime >= displayTime)
        //    {
        //        countTime = 0.0f;
        //        // Noticeを戻すシーケンスに移行
        //        sequence = NoticeSequence.returnNotice;

        //        // Noticeの元の位置を取得
        //        targetPos = eye.transform.position + eye.transform.rotation * (new Vector3(0, -1, 1) * targetDistance);
        //    }
        //}


        //private void ReturnDisplay()
        //{
        //    // 目標座標に移動させる
        //    canvasTransfome.DOMove(targetPos, moveTime)
        //        .OnComplete(() =>
        //        {
        //            sequence = NoticeSequence.none;
        //            canvasTransfome.position = new Vector3(0, -10, 0);

        //        });



        //    canvasTransfome.rotation = eye.transform.rotation;

        //}
    }
}


