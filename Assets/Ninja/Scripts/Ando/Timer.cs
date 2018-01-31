using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ando
{
    //  時間計測用クラス(分～ミリ秒まで)
    public class Timer : MonoBehaviour
    {
        private int minite = 0;
        private int second = 0;
        private float milliSecond = 0.0f;

        //  タイマーが実行中かどうか
        private bool timerFlag = false;

        [SerializeField]
        public string text = "test";

        #region プロパティ
        public int Minite
        {
            get { return this.minite; }
            protected set { }
        }
        public int Second
        {
            get { return this.second; }
            protected set { }
        }
        public int MilliSecond
        {
            get { return (int)(this.milliSecond * 100.0f); }
            protected set { }
        }
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

            Debug.Log(timerFlag);

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
        /// タイマーが計測中がどうかを取得(true:実行中 false:停止)
        /// </summary>
        /// <returns></returns>
        public bool TimerRunCheck()
        {
            return timerFlag;
        }

        /// <summary>
        /// 計測結果をString型で取得
        /// </summary>
        /// <returns></returns>
        public string GetTimeString()
        {
            text = minite.ToString("00") + ":" + second.ToString("00") + "." + (milliSecond * 100).ToString("00");

            return text;
        }

        /// <summary>
        /// 計測結果をFloat型で取得
        /// </summary>
        /// <returns></returns>
        public float GetTimeFloat()
        {
            return ((minite * 100) + second + milliSecond);
        }

        /// <summary>
        /// 経過時間(float)をストリングに変換する
        /// </summary>
        /// <param name="aValue"></param>
        /// <returns></returns>
        public string FloatToString(float aValue)
        {
           int value = 0;

            //  分の計算
            value = (int)(aValue / 100);
            text = value.ToString("00") + ":";

            //  秒の計算
            aValue -= value;
            value = (int)aValue;
            text += value.ToString("00")+ ".";

            //  ミリ秒の計算(切り捨てされる数値は気にしない方向で)
            aValue -= value;
            value = (int)(aValue * 100);
            text += value.ToString("00");

            return text;
        }

        /// <summary>
        /// 計測結果を秒に変換したものを教える
        /// </summary>
        /// <returns></returns>
        public int GetTimeSecond()
        {
            return (minite * 60) + second;
        }
    }
}