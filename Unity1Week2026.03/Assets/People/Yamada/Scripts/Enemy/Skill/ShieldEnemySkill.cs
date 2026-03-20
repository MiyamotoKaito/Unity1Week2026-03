using UnityEngine;
using System;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵自身にシールドを張るスキル。
    /// </summary>
    public class ShieldEnemySkill : EnemySkillBase
    {
        public override void Execute(EnemySkillContext enemySkillContext)
        {
            enemySkillContext.EnemyRuntimeModel.SetShield(true);
        }
    }
}
