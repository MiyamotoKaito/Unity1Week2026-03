using UnityEngine;

public class TextShuffleJammer : JammerCardBase
{
    public override void Exucute()
    {
        EffectManager.Instance.ShuffleSomeCardTexts(_value);
    }

    [SerializeField] private int _value;
}
