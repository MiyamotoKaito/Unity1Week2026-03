using UnityEngine;

public abstract class HealBase : ICardEffect
{
    [SerializeField]
    protected float _healAmount;
    public abstract void Exucute();
}
