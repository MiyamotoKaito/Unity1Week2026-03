
using UnityEngine;
using Unity1Week.URA.Stage;
public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }
    private StageProgressController _stageProgressController;
    public void Initialize(StageProgressController stageProgressController)
    {
        _stageProgressController = stageProgressController;
    }

    public void PlayAttack(int damage)
    {
        _stageProgressController?.DamageCurrentEnemy(damage);
    }
    public void PlayHeal(int healAmount)
    {
        _stageProgressController?.HealPlayer(healAmount);
    }

    public void ReduceEnemySkillTurn()
    {
        _stageProgressController?.ReduceEnemySkillTurn();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
