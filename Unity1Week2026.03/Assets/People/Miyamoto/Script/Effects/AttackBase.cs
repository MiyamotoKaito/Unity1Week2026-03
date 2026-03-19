using UnityEngine;

public abstract class AttackBase : ICardEffect
{
    [SerializeField]
    protected float _attackPower;
    public abstract void Exucute();
}