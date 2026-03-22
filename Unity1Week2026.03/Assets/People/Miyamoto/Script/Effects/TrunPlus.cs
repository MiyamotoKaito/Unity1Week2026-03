[System.Serializable]
public class TurnPlus : TurnBase
{
    public override void Exucute()
    {
        EffectManager.Instance.AddAtackTurn(_turnToAdd);
    }
}
