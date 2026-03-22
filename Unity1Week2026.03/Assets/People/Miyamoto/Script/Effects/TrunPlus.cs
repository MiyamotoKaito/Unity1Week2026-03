using UnityEngine;

public class TurnPlus : ICardEffect
{
    public void Exucute()
    {
        Debug.Log("ターンを1増やす効果が発動しました。");
        EffectManager.Instance.AddAtackTurn(_turnToAdd);
    }

    [SerializeField] private int _turnToAdd;
}
