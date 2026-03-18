using UnityEngine.SceneManagement;

namespace Unity1Week.URA.System
{
    /// <summary>
    ///     SceneLoadをするクラス
    /// </summary>
    public static class SceneLoader
    {
        /// <summary>
        ///     シーンをロードする
        /// </summary>
        /// <param name="sceneName">シーンの名前</param>
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}