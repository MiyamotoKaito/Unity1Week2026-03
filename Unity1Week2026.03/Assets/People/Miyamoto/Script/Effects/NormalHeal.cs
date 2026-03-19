[System.Serializable]
public class NormalHeal : HealBase
{
    public override void Exucute()
    {
        EffectManager.Instance.PlayHeal(_healAmount);
    }
}
