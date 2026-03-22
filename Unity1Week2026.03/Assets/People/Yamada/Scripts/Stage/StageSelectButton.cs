using Unity1Week.URA.System;
using UnityEngine;

namespace Unity1Week.URA.Stage
{
    /// <summary>
    ///     ステージ選択ボタンを管理するクラス。
    /// </summary>
    public class StageSelectButton : MonoBehaviour
    {
        /// <summary>
        ///     ステージ選択ボタンが押されたときの処理。
        /// </summary>
        public void SelectStage()
        {
            AudioManager.Instance.Stop();
            StageSelectionContext.SetSelectedStageData(_stageData);
            SceneLoader.LoadScene(_battleSceneName);
        }

        [SerializeField] private StageData _stageData;
        [SerializeField] private string _battleSceneName;
    }
}
