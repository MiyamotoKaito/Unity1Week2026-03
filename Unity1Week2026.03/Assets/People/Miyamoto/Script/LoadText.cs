using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.URA.typing
{
    /// <summary>
    /// CSVファイルをロードして組み立てるクラス
    /// </summary>
    public class LoadText
    {
        #region 変数
        public List<string> WordList => _wordList;
        private List<string> _wordList = new();
        #endregion

        #region　コンストラクタ
        public LoadText(TextAsset textAsset)
        {
            AssemblyWords(textAsset);
        }
        #endregion

        #region メソッド
        /// <summary>
        /// CSVファイルを読み込んで単語ずつに分ける
        /// </summary>
        /// <param name="textAsset"></param>
        private void AssemblyWords(TextAsset textAsset)
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
                    _wordList.Add(word);
                }
            }
        }
        #endregion
    }
}
