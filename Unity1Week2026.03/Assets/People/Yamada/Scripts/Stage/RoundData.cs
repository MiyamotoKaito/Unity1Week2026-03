using UnityEngine;
using Unity1Week.URA.Enemy;

namespace Unity1Week.URA.Stage
{
    /// <summary>
    ///     ラウンドごとのデータを管理するクラス。
    /// </summary>
    [CreateAssetMenu(fileName = "RoundData", menuName = "Scriptable Objects/RoundData")]
    public class RoundData : ScriptableObject
    {
        public EnemyData EnemyData => _enemyData;

        [SerializeField] private EnemyData _enemyData;
        //TODO: 背景とかラウンドごとに必要なデータがあればここに追加
    }
}
