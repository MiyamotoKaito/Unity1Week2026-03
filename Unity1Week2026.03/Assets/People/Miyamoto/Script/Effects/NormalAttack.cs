using Unity1Week.URA.Stage;
using UnityEngine;
[System.Serializable]
public class NormalAttack : AttackBase
{
    public override void Exucute()
    {
        EffectManager.Instance.PlayAttack(_attackPower);
    }
}
