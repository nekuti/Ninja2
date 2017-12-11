using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

using Kojima;



namespace Kondo
{
    public class MoveNotice : MonoBehaviour
    {

         enum NoticeSequence
        {
            none,
            moveNotice,
            stopNotice,
            returnNotice,
        }


        public GameObject eye;

        [SerializeField]
        private float moveTime = 1.0f;
        [SerializeField]
        private float targetDistance = 5.0f;
        [SerializeField]
        private float displayTime = 3.0f;

        public Text noticeText;
        private float countTime;
        private Transform canvasTransfome;
        private Vector3 targetPos;
        private NoticeSequence sequence;


        // Use this for initialization
        void Start()
        {
            //noticeCanvas = GetComponent<Canvas>();
           
            Text t = GetComponent<Text>();

            // キャンバスの座標を取得
            canvasTransfome = t.transform.root.gameObject.transform;

        }

        // Update is called once per frame
        void Update()
        {

            InstructMovement();
        }



         public void RequestDisplay(string aText, float aDisplayTime = 3.0f, float aMoveTime = 1.0f, float aTargetDistance = 3.0f)
        {
            displayTime = aDisplayTime;
            moveTime = aMoveTime;
            targetDistance = aTargetDistance;

            // eyeの正面の位置を取得
            targetPos = eye.transform.position + eye.transform.rotation * (Vector3.forward * targetDistance);

            // Noticeを表示用座標に移動
            canvasTransfome.transform.position = eye.transform.position + eye.transform.rotation * (new Vector3(0, -1, 1) * targetDistance);

            // テキストを書き換える
            noticeText = GetComponent<Text>();
            noticeText.text = aText;

            sequence = NoticeSequence.moveNotice;
        } 


         private  void InstructMovement()
        {

            switch(sequence)
            {
                case NoticeSequence.moveNotice :

                    GoNotice();
                    break;

                case NoticeSequence.stopNotice:

                    StopNotice();
                    break;

                case NoticeSequence.returnNotice:

                    ReturnNotice();
                    break;
            }
        }


        private void GoNotice()
        {
            // 目標座標に移動させる
            canvasTransfome.DOMove(targetPos, moveTime)
                .OnComplete(() =>
                {
                    // 移動が終了時に一時停止シーケンスに移行
                    sequence = NoticeSequence.stopNotice;
                });

            Quaternion q = eye.transform.rotation;
            q.x = 0;
            q.z = 0;
            canvasTransfome.rotation = q;
        }


        private void StopNotice()
        {
            countTime += Time.deltaTime;
            if(countTime >= displayTime)
            {
                countTime = 0.0f;
                // Noticeを戻すシーケンスに移行
                sequence = NoticeSequence.returnNotice;

                // Noticeの元の位置を取得
                targetPos = eye.transform.position + eye.transform.rotation * (new Vector3(0, -1, 1) * targetDistance);
            }
        }


        private void ReturnNotice()
        {
            // 目標座標に移動させる
            canvasTransfome.DOMove(targetPos, moveTime)
                .OnComplete(() =>
                {
                    sequence = NoticeSequence.none;
                    canvasTransfome.position = new  Vector3(0, -5, 0); 

                });

            

            canvasTransfome.rotation = eye.transform.rotation;

        }
    }
}




