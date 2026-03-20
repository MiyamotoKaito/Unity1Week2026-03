using Unity1Week.URA.Enemy;
using UnityEngine;

public class ReduceAttackPowerSkill : EnemySkillBase
{
    public override void Execute(EnemySkillContext enemySkillContext)
    {
        EffectManager.Instance.ReduceCurrentPower(_reduceAmount);
    }

    [SerializeField] private int _reduceAmount;
}
