using UnityEngine;

public class ReduceSkillTurnJammer : JammerCardBase
{
    public override void Exucute()
    {
        EffectManager.Instance.ReduceEnemySkillTurn(_reduceTurnValue);
    }

    [SerializeField] private int _reduceTurnValue;
}
