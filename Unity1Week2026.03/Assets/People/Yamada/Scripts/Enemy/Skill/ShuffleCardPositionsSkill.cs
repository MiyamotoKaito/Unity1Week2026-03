using Unity1Week.URA.Enemy;

public class ShuffleCardPositionsSkill : EnemySkillBase
{
    public override void Execute(EnemySkillContext enemySkillContext)
    {
        EffectManager.Instance.ShuffleAllCardPositions();
    }
}
