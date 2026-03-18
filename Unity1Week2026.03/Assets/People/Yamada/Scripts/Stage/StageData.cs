using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.URA.Stage
{
    /// <summary>
    ///     ステージのデータを管理するクラス。
    ///     ステージ名と、ステージ内のラウンドデータのリストを持つ。
    /// </summary>
    [CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
    public class StageData : ScriptableObject
    {
        public string Stagename => _stagename;
        public IReadOnlyList<RoundData> RoundDatas => _roundDatas;

        [SerializeField] private string _stagename;
        [SerializeField] private List<RoundData> _roundDatas;
    }
}
