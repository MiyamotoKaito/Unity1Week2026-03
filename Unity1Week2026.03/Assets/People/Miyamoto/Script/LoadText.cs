using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.URA.typing
{
    /// <summary>
    /// CSVファイルをロードして組み立てるクラス
    /// </summary>
    public static class LoadText
    {
        #region 変数
        public static List<string> WordList { get; } = new();
        #endregion

        #region メソッド
        /// <summary>
        /// CSVファイルを読み込んで単語ずつに分ける
        /// </summary>
        /// <param name="textAsset"></param>
        public static void AssemblyWords(TextAsset textAsset)
        {
            // 行単位で分ける
            var lines = textAsset.text.Split("\n");
            foreach (var line in lines)
            {
                // カンマ単位で区切る
                var words = line.Split(',');
                foreach (var word in words)
                {
                    //　単語をリストに追加
                    WordList.Add(word);
                }
            }
        }
        /// <summary>
        /// ResourecesフォルダからCSVファイルを取得する
        /// </summary>
        /// <returns></returns>
        public static TextAsset GetCSVFile()
        {
            var csv = Resources.Load<TextAsset>("WordList");

            if (csv == null)
            {
                Debug.LogError("CSVファイルが見つからない");
            }

            return csv;
        }
        #endregion
    }
}
