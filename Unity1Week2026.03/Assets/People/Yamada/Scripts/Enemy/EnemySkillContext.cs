using Unity1Week.URA.Player;
using UnityEngine;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵スキルの実行に必要な情報をまとめたクラス。
    /// </summary>
    public class EnemySkillContext
    {
        public EnemySkillContext(EnemyRuntimeModel enemyRuntimeModel,
            PlayerHealthModel playerHealthModel,
            EnemySkillTurnTracker enemySkillTurnTracker)
        {
            EnemyRuntimeModel = enemyRuntimeModel;
            PlayerHealthModel = playerHealthModel;
            EnemySkillTurnTracker = enemySkillTurnTracker;
        }

        public EnemyRuntimeModel EnemyRuntimeModel { get; }
        public PlayerHealthModel PlayerHealthModel { get; }
        public EnemySkillTurnTracker EnemySkillTurnTracker { get; }
    }
}
