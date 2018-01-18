using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class TitleScroll : MonoBehaviour
    {

        //  Sin波計算用
        private float time;

        //  移動速度の倍率
        [SerializeField]
        private float moveScale = 1.0f;

        //  巻物の停止位置
        private Vector3 stopPos;

        //  移動許可スイッチ(true:許可, false:停止)
        [SerializeField]
        private bool moveSwitch = true;

        //  停止時間保存用
        [SerializeField]
        private float waitTime = 5.0f;

        //  停止時間実行用
        private float waitTimeRun = 0.0f;

        //  移動量
        private float moveValue = 0.0f;

        // Use this for initialization
        void Start()
        {
            //  停止位置を保存
            stopPos = this.gameObject.transform.position;

            //  巻物を画面外へ移動させる
            this.gameObject.transform.position += new Vector3(0, 34, 0);

            //  停止時間を実行用変数  
            waitTimeRun = waitTime;

            //  変数を初期化
            time = 0.0f;
            moveValue = 0.0f;

            AudioManager.Instance.PlaySE("SE01", gameObject.transform.position);

        }

        // Update is called once per frame
        void Update()
        {

            //  移動可能か
            if (moveSwitch)
            {
                moveValue = (Mathf.Sin(time) * moveScale);

                transform.position -= new Vector3(0f, moveValue, 0f);
                //  計算用の変数を加算
                time += 0.01f;
            }
            else
            {
                //  停止時間を減少
                waitTimeRun -= Time.deltaTime;
            }

            //  停止位置を超えたか確認
            if (transform.position.y <= stopPos.y)
            {
                //  移動可能スイッチを停止に変更
                moveSwitch = false;
            }

            //  停止時間を超えたか
            if (0.0f > waitTimeRun)
            {
                //  移動可能スイッチを移動可能に変更
                moveSwitch = true;

                //  移動量がマイナスになったか
                if (moveValue <= 0)
                {
                    //  gameObjectを削除
                    Destroy(this.gameObject);
                }
            }

        }
    }
}