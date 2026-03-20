using UnityEngine;

public class ReverseTextJammer : JammerCardBase
{
    public override void Exucute()
    {
        EffectManager.Instance.ApplyReverseText();
    }
}
