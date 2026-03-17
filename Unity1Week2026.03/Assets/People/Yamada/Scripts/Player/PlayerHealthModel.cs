using System;
using UnityEngine;

namespace Unity1Week.URA.Player
{
    /// <summary>
    ///     プレイヤーのHPを管理するモデルクラス。
    /// </summary>
    public class PlayerHealthModel
    {
        public PlayerHealthModel(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public event Action<int,int> OnHealthChanged;
        public event Action OnPlayerDied;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;

        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if(_currentHealth == 0)
            {
                OnPlayerDied?.Invoke();
            }
        }

        public void Heal(int healAmount)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        private int _currentHealth;
        private int _maxHealth;
    }
}
