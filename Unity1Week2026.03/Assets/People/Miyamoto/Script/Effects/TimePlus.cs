[System.Serializable]
public class TimePlus : TimeBase
{
    public override void Exucute()
    {
        EffectManager.Instance.AddEnemyAttackTimer(_timeToAdd);
    }
}
