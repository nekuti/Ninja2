namespace Ando
{
    /// <summary>
    /// 各シーンのenum
    /// </summary>
    //  遷移するシーンの名前
    public enum SceneName
    {
        None,
        TitleTest,
        PlayTest,
        ResultTest,
        PauseTest,
        LiteResult,
        TitleScene,
        TutorialMainScene,
        PlayScene,
        ResultScene,
        MenuScene,
        End,
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
                case SceneName.None: return "None";
                case SceneName.TitleTest: return "TitleTest";
                case SceneName.PlayTest: return "PlayTest";
                case SceneName.ResultTest: return "ResultTest";
                case SceneName.PauseTest: return "PauseTest";
                case SceneName.LiteResult: return "LiteResult";
                case SceneName.TitleScene: return "TitleScene";
                case SceneName.TutorialMainScene: return "TutorialMainScene";
                case SceneName.PlayScene: return "PlayScene";
                case SceneName.ResultScene: return "ResultScene";
                case SceneName.MenuScene: return "MenuScene";
                case SceneName.End: return "End";
                default: return "error";
            }
        }
    }
}