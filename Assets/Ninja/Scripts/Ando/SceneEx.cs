//  作成者：小嶋 佑太

namespace Ando
{
    /// <summary>
    /// 各シーンのenum
    /// </summary>
    //  遷移するシーンの名前
    public enum SceneName
    {
        TitleTest,
        PlayTest,
        ResultTest,
        PauseTest,
        LiteResult,
        Tutorial,
    }

    /// <summary>
    /// Sceneの拡張メソッド
    /// </summary>
    static class SceneEx
    {
        /// </summary>
        /// <param name="aSceneName"></param>
        /// <returns></returns>
        public static string IsName(this SceneName aSceneName)
        {
            switch (aSceneName)
            {
                case SceneName.TitleTest: return "TitleTest";
                case SceneName.PlayTest: return "PlayTest";
                case SceneName.ResultTest: return "ResultTest";
                case SceneName.PauseTest: return "PauseTest";
                case SceneName.LiteResult: return "LiteResult";
                case SceneName.Tutorial: return "Tutorial";
                default: return "error";
            }
        }
    }
}