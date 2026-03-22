using Unity1Week.URA.Stage;
using UnityEngine;
[System.Serializable]
public class NormalAttack : AttackBase
{
    public override void Exucute()
    {
        AudioManager.Instance.PlaySE("CardAttack");
        EffectManager.Instance.PlayAttack(_attackPower);
    }
}
