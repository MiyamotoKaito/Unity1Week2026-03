using UnityEngine;

public class TextShuffleJammer : ICardEffect
{
    public void Exucute()
    {
        EffectManager.Instance.ShuffleSomeCardTexts(_value);
    }

    [SerializeField] private int _value;
}
