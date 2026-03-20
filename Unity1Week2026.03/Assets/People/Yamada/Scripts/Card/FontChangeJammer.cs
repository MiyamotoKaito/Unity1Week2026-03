using UnityEngine;

public class FontChangeJammer : JammerCardBase
{
    public override void Exucute()
    {
        EffectManager.Instance.ApplyMirrorFont();
    }
}
