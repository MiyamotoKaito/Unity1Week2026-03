using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵のデータを管理するScriptableObjectクラス。
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string EnemyName => _enemyName;
        public Sprite Sprite => _sprite;
        public int MaxHealth => _maxHealth;
        public int AttackPower => _attackPower;
        public float AttackIntervalSeconds => _attackIntervalSeconds;
        public int SkillTurnInterval => _skillTurnInterval;
        public IReadOnlyList<EnemySkillBase> EnemySkills => _enemySkills;

        [Header("基本情報")]
        [SerializeField] private string _enemyName;
        [SerializeField] private Sprite _sprite;

        [Header("ステータス")]
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _attackPower;

        [Header("行動設定")]
        [SerializeField] private float _attackIntervalSeconds;

        [Header("スキル設定")]
        [SerializeField] private int _skillTurnInterval;
        [SerializeReference, SubclassSelector] private List<EnemySkillBase> _enemySkills;
    }
}