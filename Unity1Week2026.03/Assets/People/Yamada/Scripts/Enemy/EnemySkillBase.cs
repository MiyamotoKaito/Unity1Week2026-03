using System;
using UnityEngine;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵スキルの基底クラス。
    /// </summary>
    [Serializable]
    public abstract class EnemySkillBase
    {
        /// <summary>
        ///     スキルを実行する。
        /// </summary>
        /// <param name="enemySkillContext"></param>
        public abstract void Execute(EnemySkillContext enemySkillContext);
    }
}
