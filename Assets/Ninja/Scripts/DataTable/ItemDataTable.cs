using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    [CreateAssetMenu]
    public class ItemDataTable : ScriptableObject
    {
        public string itemName = "アイテムの名前";
        public string explanation = "アイテムの説明を記述";
        public int maxPossessionNum = 99;
        public int price = 999;
    }
}
