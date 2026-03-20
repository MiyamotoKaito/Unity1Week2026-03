using UnityEngine;

public abstract class AttackBase : ICardEffect
{
    [SerializeField]
    protected int _attackPower;
    public int AttackPower => _attackPower;

    public void AddAttackPower(int delta)
    {
        _attackPower = Mathf.Max(0, _attackPower + delta);
    }
    public abstract void Exucute();
}
