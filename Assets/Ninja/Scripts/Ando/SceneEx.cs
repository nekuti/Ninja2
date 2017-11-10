//  作成者：小嶋 佑太

namespace Ando
{
    /// <summary>
    /// 各シーンのenum
    /// </summary>
    //  遷移するシーンの名前
    public enum SceneName
    {
        Title_A,
        Play_A,
        Result_A,
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
                case SceneName.Title_A: return "Title";
                case SceneName.Play_A: return "Play";
                case SceneName.Result_A: return "Result";
                default: return "error";
            }
        }
    }
}