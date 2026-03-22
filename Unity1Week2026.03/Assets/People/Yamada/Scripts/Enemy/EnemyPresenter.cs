using Unity1Week.URA.Enemy;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    /// <summary>
    ///     初期化する。
    /// </summary>
    /// <param name="enemyRuntimeModel"></param>
    /// <param name="enemyAttackTimer"></param>
    /// <param name="enemySkillTurnTracker"></param>
    public void Initialize(
        EnemyRuntimeModel enemyRuntimeModel,
        EnemyAttackTimer enemyAttackTimer,
        EnemySkillTurnTracker enemySkillTurnTracker)
    {
        if (_enemyRuntimeModel != null)
        {
            _enemyRuntimeModel.OnHealthChanged -= HandleHealthChanged;
        }

        _enemyRuntimeModel = enemyRuntimeModel;
        _enemyAttackTimer = enemyAttackTimer;
        _enemySkillTurnTracker = enemySkillTurnTracker;

        // イベントを登録する。
        _enemyRuntimeModel.OnHealthChanged += HandleHealthChanged;

        // ビューを初期化して、それぞれの値を反映する。
        _enemyView.InitializeView(
            _enemyRuntimeModel.Data.Sprite,
            _enemyRuntimeModel.CurrentHealth,
            _enemyRuntimeModel.MaxHealth
            );

        _previousHealth = _enemyRuntimeModel.CurrentHealth;
        _enemyView.UpdateHealth(_enemyRuntimeModel.CurrentHealth, _enemyRuntimeModel.MaxHealth);
        _enemyView.UpdateAttackTimer(_enemyAttackTimer.RemainingTime);
        _enemyView.UpdateSkillTurn(_enemySkillTurnTracker.RemainingTurns, _enemySkillTurnTracker.StartTurns);
    }

    public void StopEffect()
    {
        _enemyView.StopPulse();
    }

    /// <summary>
    ///     HPが変更されたときの処理。EnemyViewのHPバーを更新する。
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <param name="maxHealth"></param>
    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        if (currentHealth < _previousHealth)
        {
            _enemyDamageFeedbackView?.PlayFlush();
        }

        _previousHealth = currentHealth;
        _enemyView.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        // 毎フレーム、攻撃タイマーとスキルターンをビューに反映する。
        if (_enemyAttackTimer != null)
        {
            _enemyView.UpdateAttackTimer(_enemyAttackTimer.RemainingTime);
        }
        if (_enemySkillTurnTracker != null)
        {
            _enemyView.UpdateSkillTurn(_enemySkillTurnTracker.RemainingTurns, _enemySkillTurnTracker.StartTurns);
        }
    }

    private void OnDestroy()
    {
        if (_enemyRuntimeModel != null)
        {
            _enemyRuntimeModel.OnHealthChanged -= HandleHealthChanged;
        }
    }

    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private EnemyDamegeFeedbackView _enemyDamageFeedbackView;

    private EnemyRuntimeModel _enemyRuntimeModel;
    private EnemyAttackTimer _enemyAttackTimer;
    private EnemySkillTurnTracker _enemySkillTurnTracker;
    private float _previousHealth;
}
