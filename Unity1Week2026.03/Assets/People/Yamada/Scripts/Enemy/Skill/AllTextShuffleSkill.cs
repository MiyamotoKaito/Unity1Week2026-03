using Unity1Week.URA.Enemy;
using UnityEngine;

public class AllTextShuffleSkill : EnemySkillBase
{
    public override void Execute(EnemySkillContext enemySkillContext)
    {
        EffectManager.Instance.ShuffleSomeCardTexts(_shuffleCount);
    }

    [SerializeField] private int _shuffleCount;
}
