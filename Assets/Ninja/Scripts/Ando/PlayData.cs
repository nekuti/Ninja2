using UnityEngine;

namespace Ando
{

    /// <summary>
    /// 武器のデータ
    /// </summary>
    [System.Serializable]
    public class WeaponData
    {
        public Kojima.WeaponDataTable weaponData;
        public int strengthenLevel;
    }

    /// <summary>
    /// アイテムのデータ
    /// </summary>
    [System.Serializable]
    public class ItemData
    {
        //  アイテムのデータテーブル
        public ItemDataTable itemData;

        //  所持数
        public int possession;
    }

    [System.Serializable]
    public class PlayData
    {
        public Kojima.Player player;
        public Vector3 startPos;
        public StageState stageEnd;

        //  所持金
        public int possessionMoney;

        //  武器の強化上限
        public int weaponStrengthenMaxLevel;

        //  武器のデータ
        public WeaponData kunai;
        public WeaponData throwingStar;
        public WeaponData bomb;

        //  アイテムのデータ
        public ItemData onigiri;
        public ItemData fireSkill;
        public ItemData soilSkill;
        public ItemData summonsSkill;
    }
}
