using UnityEngine;

public class FontChangeJammer : ICardEffect
{
    public void Exucute()
    {
        EffectManager.Instance.ApplyMirrorFont();
    }
}
