using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回復アイテムのデータクラス
/// 作成者:小嶋 佑太
/// 最終更新:2017/12/19
/// </summary>
namespace Kojima
{
    [CreateAssetMenu]
    public class ItemHealDataTable : Ando.ItemDataTable
    {
        #region メンバ変数
        [SerializeField, Tooltip("回復割合"), Range(0f, 100f)]
        private float healPoint = 5f;

        #endregion

        #region プロパティ
        public float HealPoint { get { return healPoint; } }
        #endregion
    }
}