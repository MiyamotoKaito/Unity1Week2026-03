using UnityEngine;

public class ReduceSkillTurnJammer : ICardEffect
{
    public void Exucute()
    {
        EffectManager.Instance.ReduceEnemySkillTurn(_reduceTurnValue);
    }

    [SerializeField] private int _reduceTurnValue;
}
