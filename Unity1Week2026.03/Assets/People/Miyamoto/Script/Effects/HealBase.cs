using UnityEngine;

public abstract class HealBase : ICardEffect
{
    [SerializeField]
    protected int _healAmount;
    public int HealAmount => _healAmount;
    public abstract void Exucute();
}
