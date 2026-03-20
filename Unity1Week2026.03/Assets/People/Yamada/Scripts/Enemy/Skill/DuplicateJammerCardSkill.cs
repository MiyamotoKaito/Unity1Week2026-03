using System.Collections.Generic;
using Unity1Week.URA.Enemy;
using UnityEngine;

public class DuplicateJammerCardSkill : EnemySkillBase
{
    public override void Execute(EnemySkillContext enemySkillContext)
    {
        EffectManager.Instance.DuplicateJammerPair(_dataList);
    }

    [SerializeField] private List<CardData> _dataList;
}
