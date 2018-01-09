using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    [CreateAssetMenu]
    public class ResultAssessmentDataTable : ScriptableObject
    {
        [Tooltip("１階層目のプレイ時間の評価値（値が小さいほど評価が高い）")]
        public List<int> level1PlayTime;
        [Tooltip("２階層目のプレイ時間の評価値（値が小さいほど評価が高い）")]
        public List<int> level2PlayTime;
        [Tooltip("３階層目のプレイ時間のプレイ時間の評価値（値が小さいほど評価が高い）")]
        public List<int> level3PlayTime;

        [Tooltip("１階層目の取得金額の評価値（値が大きいほど評価が高い）")]
        public List<int> level1GetMoney;
        [Tooltip("２階層目のプレイ時間の取得金額の評価値（値が大きいほど評価が高い）")]
        public List<int> level2GetMoney;
        [Tooltip("３階層目のプレイ時間の取得金額の評価値（値が大きいほど評価が高い）")]
        public List<int> level3GetMoney; 

        [Tooltip("１階層目の消費エネルギーの評価値（値が小さいほど評価が高い）")]
        public List<int> level1LostEnergy;
        [Tooltip("２階層目のプレイ時間の消費エネルギーの評価値（値が小さいほど評価が高い）")]
        public List<int> level2LostEnergy;
        [Tooltip("３階層目のプレイ時間の消費エネルギーの評価値（値が小さいほど評価が高い）")]
        public List<int> level3LostEnergy;   
    }
}
