//  作成者：安藤 茂貴

namespace Ando
{
    /// <summary>
    /// 各シーンのenum
    /// </summary>
    //  遷移するシーンの名前
    public enum StageName
    {
        PlayerBase,
        StageTest1,
        StageTest2,
        Stage001,
        Stage002,
        Stage003,
        Stage004,
        Stage005,
        Stage006,

        BossStage01,

        //  ここより上に追加してね
        None,
    }

    /// <summary>
    /// Sceneの拡張メソッド
    /// </summary>
    static class SteageEx
    {
        /// </summary>
        /// <param name="aSceneName"></param>
        /// <returns></returns>
        public static string IsName(this StageName aStageName)
        {
            switch (aStageName)
            {
                case StageName.PlayerBase: return "PlayerBase";
                case StageName.StageTest1: return "StageTest1";
                case StageName.StageTest2: return "StageTest2";
                case StageName.Stage001: return "Stage001";
                case StageName.Stage002: return "Stage002";
                case StageName.Stage003: return "Stage003";
                case StageName.Stage004: return "Stage004";
                case StageName.Stage005: return "Stage005";
                case StageName.Stage006: return "Stage006";
                case StageName.BossStage01: return "BossStage01";

                default: return "error";
            }
        }
    }
}