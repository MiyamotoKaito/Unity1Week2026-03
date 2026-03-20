using UnityEngine;

public class EnemyAttackTimerReduceJammer : ICardEffect
{
    public void Exucute()
    {
        EffectManager.Instance.ReduceEnemyAttackTimer(_reduceTime);
    }

    [SerializeField] private float _reduceTime;
}
