using UnityEngine;

namespace Ando
{
    public struct PlayData
    {
        public Kojima.Player player;
        public Vector3 startPos;
        public StageState stageEnd;

        //  所持金
        public int possessionMoney;
        //  武器のデータテーブル
        public Kojima.WeaponDataTable weponData;
        //  アイテムの所持数
        public int possessionOnigiri;
        public int possessionFireSkill;
        public int possessionSoilSkill;
        public int possessionSummonsSkill;

        public void Initialize()
        {
            player = null;
            startPos = new Vector3(0, 0, 0);
            stageEnd = StageState.None;

            possessionMoney = 0;
            weponData = null;
            possessionOnigiri = 0;
            possessionFireSkill = 0;
            possessionSoilSkill = 0;
            possessionSummonsSkill = 0;
        }
    }
}
