using UnityEngine;

public class Clairvoyance : ICardEffect
{
    public void Exucute()
    {
       EffectManager.Instance.Clairvoyance();
    }
}
