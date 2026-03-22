using UnityEngine;

public abstract class TimeBase : ICardEffect
{
    [SerializeField]
    protected float _timeToAdd;

    public float TimeToAdd => _timeToAdd;

    public abstract void Exucute();
}
