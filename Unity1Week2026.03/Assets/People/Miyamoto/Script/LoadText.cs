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
            if (textAsset == null)
            {
                Debug.LogError("CSVテキストが空です。UTF-8で保存されたCSVを用意してください。");
                return;
            }
            if (textAsset.text.Contains("\uFFFD"))
            {
                Debug.LogWarning("文字化けの可能性があります。CSVをUTF-8（BOM付き推奨）で保存してください。");
            }
            // 行単位で分ける
            var lines = textAsset.text.Split('\n');
            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim(); // ← これ重要
                var words = line.Split(',');
                foreach (var word in words)
                {
                    WordList.Add(word.Trim()); // ← これもやる
                }
            }
        }
        /// <summary>
        /// ResourecesフォルダからCSVファイルを取得する
        /// </summary>
        /// <returns></returns>
        public static TextAsset GetCSVFile()
        {
            var csv = Resources.Load<TextAsset>("WordListV3");

            if (csv == null)
            {
                Debug.LogError("CSVファイルが見つからない");
            }

            return csv;
        }
        #endregion
    }
}
