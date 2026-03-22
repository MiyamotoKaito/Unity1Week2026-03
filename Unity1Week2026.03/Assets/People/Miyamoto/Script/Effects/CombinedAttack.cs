using UnityEngine;
[System.Serializable]
public class CombinedAttack : AttackBase
{
    [SerializeField]
    private int _attackCount;

    public override void Exucute()
    {
        for (int i = 0; i < _attackCount; i++)
        {
            EffectManager.Instance.PlayAttack(_attackPower);
        }
    }
}
