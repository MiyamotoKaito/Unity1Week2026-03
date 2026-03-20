using UnityEngine;

public class ReverseTextJammer : ICardEffect
{
    public void Exucute()
    {
        EffectManager.Instance.ApplyReverseText();
    }
}
