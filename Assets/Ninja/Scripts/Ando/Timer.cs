using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ando
{
    public class Timer : MonoBehaviour
    {
        private int minite = 0;
        private int second = 0;
        private float milliSecond = 0.0f;
        private bool timerFlag = false;

        [SerializeField]
        public string text = "test";

        #region プロパティ
        #region プライベート変数　使うならコメント解除してどうぞ
        //public int Minite
        //{
        //    get { return this.minite; }
        //    protected set { }
        //}
        //public int Second
        //{
        //    get { return this.second; }
        //    protected set { }
        //}
        //public int MilliSecond
        //{
        //    get { return (int)(this.milliSecond * 100.0f); }
        //    protected set { }
        //}
        #endregion
        #endregion

        void Update()
        {
            if (Time.timeScale > 0)
            {
                if (timerFlag)
                {
                    milliSecond += Time.deltaTime;

                    if (milliSecond >= 1.0f)
                    {
                        second++;
                        milliSecond = milliSecond - 1.0f;
                    }
                    if (second >= 60)
                    {
                        minite++;
                        second = second - 60;
                    }
                }
            }
        }

        /// <summary>
        /// 計測開始
        /// </summary>
        public void TimerStart()
        {
            timerFlag = true;
        }

        /// <summary>
        /// 計測停止
        /// </summary>
        public void TimerStop()
        {
            timerFlag = false;
        }

        /// <summary>
        /// 計測結果の破棄
        /// </summary>
        public void TimerReset()
        {
            minite = 0;
            second = 0;
            milliSecond = 0.0f;
            timerFlag = false;
        }


        /// <summary>
        /// 計測結果を取得
        /// </summary>
        /// <returns></returns>
        public string GetTimeString()
        {
            text = minite.ToString("00") + ":" + second.ToString("00") + "." + (milliSecond * 100).ToString("00");

            return text;
        }
    }
}