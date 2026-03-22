using UnityEngine;

public abstract class TurnBase : ICardEffect
{
    [SerializeField]
    protected int _turnToAdd;

    public int TurnToAdd => _turnToAdd;

    public abstract void Exucute();
}
