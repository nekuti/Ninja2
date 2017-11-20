using System.Collections.Generic;

namespace Ando
{
    public struct ResultContainer
    {
        public Timer totalPlayTime;
        public Timer playTime;
        public int totalGetMoneyValue;
        public int getMoneyValue;
        public int totalLostEnergyValue;
        public int lostEnergyValue;
        public int killEnemyValue;
        public int useItemValue;
        public List<int> stageEvaluation;

        /// <summary>
        /// ステージ評価の追加(優:4良:3可:2不:1)
        /// </summary>
        /// <param name="aValue"></param>
        public void SetStageEvaluation(int aValue)
        {
            stageEvaluation.Add(aValue);
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