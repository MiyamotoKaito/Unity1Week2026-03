using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TimePlus : ICardEffect
{

    public void Exucute()
    {
        Debug.Log("時間を止める効果が発動しました。");
        EffectManager.Instance.AddEnemyAttackTimer(_timeToAdd);
    }

    [SerializeField] private float _timeToAdd;
}
