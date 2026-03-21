using UnityEngine;

namespace Unity1Week.URA.Player
{
    /// <summary>
    ///     プレイヤーのHPを管理するプレゼンタークラス。
    /// </summary>
    public class PlayerHealthPresenter : MonoBehaviour
    {
        public void Initialize(PlayerHealthModel healthModel)
        {
            _healthModel = healthModel;
            _previousHealth = _healthModel.CurrentHealth;

            _healthModel.OnHealthChanged += HandleHealthChanged;
            _healthModel.OnPlayerDied += HandleDied;

            _healthView.UpdateHealthBar(_healthModel.CurrentHealth, _healthModel.MaxHealth);
        }

        [SerializeField] private PlayerHealthView _healthView;
        [SerializeField] private DamageFeedbackView _damageFeedBackView;

        private PlayerHealthModel _healthModel;
        private int _previousHealth;

        /// <summary>
        ///     HPが変更されたときViewの更新を呼び出す。
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="maxHealth"></param>
        private void HandleHealthChanged(int currentHealth, int maxHealth)
        {
            if(currentHealth < _previousHealth)
            {
                _damageFeedBackView?.PlayDamageFeedback();
            }

            _previousHealth = currentHealth;
            _healthView.UpdateHealthBar(currentHealth, maxHealth);
        }

        /// <summary>
        ///     プレイヤーが死亡したときの処理。
        /// </summary>
        private void HandleDied()
        {
            Debug.Log("プレイヤー死亡！");
        }

        private void OnDestroy()
        {
            _healthModel.OnHealthChanged -= HandleHealthChanged;
            _healthModel.OnPlayerDied -= HandleDied;
        }
    }
}
