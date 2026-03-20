using UnityEngine;

public class EnemyAttackTimerReduceJammer : JammerCardBase
{
    public override void Exucute()
    {
        EffectManager.Instance.ReduceEnemyAttackTimer(_reduceTime);
    }

    [SerializeField] private float _reduceTime;
}
