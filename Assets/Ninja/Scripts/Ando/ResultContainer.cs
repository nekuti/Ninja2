﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    public class ResultContainer : MonoBehaviour
    {
        public Timer totalPlayTimer;
        public float oldTotalPlayTime;
        public Timer playTimer;
        public int totalGetMoneyValue;
        public int getMoneyValue;
        public int totalLostEnergyValue;
        public int lostEnergyValue;
        public int killEnemyValue;
        public int useItemValue;
        public List<int> stageEvaluation;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize(GameObject aGameObject)
        {
            totalPlayTimer = aGameObject.AddComponent<Timer>();
            oldTotalPlayTime = 0;
            playTimer = aGameObject.AddComponent<Timer>();
            totalGetMoneyValue = 0;
            getMoneyValue = 0;
            totalLostEnergyValue = 0;
            lostEnergyValue = 0;
            killEnemyValue = 0;
            useItemValue = 0;
            stageEvaluation = new List<int>();

        }

        /// <summary>
        /// 総プレイ時間の計測を開始する  
        /// </summary>
        public void TotalTimerStart()
        {
            totalPlayTimer.TimerStart();
        }

        /// <summary>
        /// ステージのプレイ時間の計測を開始する
        /// </summary>
        public void PlayTimerStart()
        {
            playTimer.TimerStart();
        }

        /// <summary>
        /// 総プレイ時間の計測を停止する
        /// </summary>
        public void TotalTimerStop()
        {
            totalPlayTimer.TimerStop();

            //  過去の計測結果に足す
            oldTotalPlayTime += totalPlayTimer.GetTimeFloat();
        }

        /// <summary>
        /// ステージ評価の追加(優:3良:2可:1不:0)
        /// </summary>
        /// <param name="aValue"></param>
        public void SetStageEvaluation(int aValue)
        {
            if (aValue <= 3)
            {
                stageEvaluation.Add(aValue);
            }
            else
            {
                stageEvaluation.Add(-1);
                Debug.Log("ステージ評価の値に規定値が入った" + aValue);
            }
        }

        /// <summary>
        /// ステージ評価の削除
        /// </summary>
        public void ResetStageEvaluation()
        {
            stageEvaluation.Clear();
        }

    }
}