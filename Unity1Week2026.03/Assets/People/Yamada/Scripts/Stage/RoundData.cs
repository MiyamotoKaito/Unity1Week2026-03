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
        public string RoundStartMessage => _roundStartMessage;
        public Sprite BackGroundSprite => _backGroundSprite;

        [SerializeField] private EnemyData _enemyData;
        [SerializeField, TextArea] private string _roundStartMessage;
        [SerializeField] private Sprite _backGroundSprite;
    }
}
