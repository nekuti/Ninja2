//  作成者：安藤 茂貴

namespace Ando
{
    /// <summary>
    /// 各シーンのenum
    /// </summary>
    //  遷移するシーンの名前
    public enum StageName
    {
        StageTest1,
        StageTest2,
        Stage001,
        Stage002,
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
                case StageName.StageTest1: return "StageTest1";
                case StageName.StageTest2: return "StageTest2";
                case StageName.Stage001: return "Stage001";
                case StageName.Stage002: return "Stage002";

                default: return "error";
            }
        }
    }
}