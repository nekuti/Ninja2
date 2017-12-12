﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        public Sprite qqqq;


        private CanvasHide canvasHide;



        // Use this for initialization
        void Start()
        {
            Debug.Log("ディスプレイテキスト Start()");

            mainText = transform.Find("Main").GetComponent<Text>();
            headLineText = transform.Find("HeadLine").GetComponent<Text>();
            image = transform.Find("Image").GetComponent<Image>();
            Debug.Log("ディスプレイテキスト image :"+image);

            // DisplayTextの表示操作用変数
            canvasHide = transform.GetComponentInChildren<CanvasHide>();
        }

        // Update is called once per frame
        void Update()
        {

        }



        public void RequestDisplay(DisplayLayout aLayout, Transform aPos)
        {
            Debug.Log("ディスプレイテキスト RequestDisplay()");
            canvasHide.HideON();
            headLineText.text = aLayout.headLine;
            mainText.text = aLayout.mainText;
            transform.position = aPos.position;
            //qqqq = aLayout.sprite;
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


