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

        //  武器の強化レベル
        public int kunaiLevel;
        public int throwingStarLevel;
        public int bombLevel;

        //  アイテムの所持数
        public int possessionOnigiri;
        public int possessionFireSkill;
        public int possessionSoilSkill;
        public int possessionSummonsSkill;

        /// <summary>
        /// プレイシーンマネージャが保存する情報を初期化
        /// </summary>
        public void Initialize()
        {
            player = null;
            startPos = new Vector3(0, 0, 0);
            stageEnd = StageState.None;

            //  所持金
            possessionMoney = 200;

            //  武器のレベル
            kunaiLevel = 1;
            throwingStarLevel = 1;
            bombLevel = 1;

            //  アイテムの所持数
            possessionOnigiri = 0;
            possessionFireSkill = 0;
            possessionSoilSkill = 0;
            possessionSummonsSkill = 0;
        }
    }
}
