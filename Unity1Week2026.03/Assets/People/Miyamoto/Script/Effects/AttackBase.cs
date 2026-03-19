using UnityEngine;

public abstract class AttackBase : ICardEffect
{
    [SerializeField]
    protected int _attackPower;
    public abstract void Exucute();
}