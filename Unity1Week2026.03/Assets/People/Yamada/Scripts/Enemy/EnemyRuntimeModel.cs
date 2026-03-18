using System;
using Unity1Week.URA.Enemy;
using UnityEngine;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵のRuntimeModelクラス。
    ///     EnemyDataを元に、戦闘中の敵の状態を管理するクラス。
    /// </summary>
    public class EnemyRuntimeModel
    {
        public EnemyRuntimeModel(EnemyData data)
        {
            _data = data;
            _currentHealth = data.MaxHealth;
            _maxHealth = data.MaxHealth;
            _attackPower = data.AttackPower;
        }

        public event Action<int, int> OnHealthChanged;
        public event Action OnEnemyDied;

        public EnemyData Data => _data;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public int CurrentAttackPower => _attackPower;
        public bool HasShield => _hasShield;

        /// <summary>
        ///     ダメージ処理を行う。
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage)
        {
            // シールドがある場合は、ダメージを無効化してシールドを消費する。
            // まだ仮の実装。
            if (_hasShield)
            {
                _hasShield = false;
                return;
            }

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth == 0)
            {
                OnEnemyDied?.Invoke();
            }
        }

        /// <summary>
        ///     HPを回復する。
        /// </summary>
        /// <param name="healAmount"></param>
        public void Heal(int healAmount)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        /// <summary>
        ///     シールドの有無を設定する。
        /// </summary>
        /// <param name="value"></param>
        public void SetShield(bool value)
        {
            _hasShield = value;
        }

        private EnemyData _data;
        private int _currentHealth;
        private int _maxHealth;
        private int _attackPower;
        private bool _hasShield;
    }
}
