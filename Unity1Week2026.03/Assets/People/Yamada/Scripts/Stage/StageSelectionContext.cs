using UnityEngine;

namespace Unity1Week.URA.Stage
{
    /// <summary>
    ///     ステージ選択結果を管理するクラス。
    /// </summary>
    public static class StageSelectionContext
    {
        public static StageData SelectedStageData { get; private set; }

        /// <summary>
        ///     ステージ選択結果を設定する。
        /// </summary>
        /// <param name="stageData"></param>
        public static void SetSelectedStageData(StageData stageData)
        {
            SelectedStageData = stageData;
        }

        /// <summary>
        ///     ステージ選択結果をクリアする。
        /// </summary>
        public static void ClearSelectedStageData()
        {
            SelectedStageData = null;
        }
    }
}
