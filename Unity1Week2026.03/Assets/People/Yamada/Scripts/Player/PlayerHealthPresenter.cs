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

            _healthModel.OnHealthChanged += HandleHealthChanged;
            _healthModel.OnPlayerDied += HandleDied;

            _healthView.UpdateHealthBar(_healthModel.CurrentHealth, _healthModel.MaxHealth);
        }

        [SerializeField] private PlayerHealthView _healthView;

        private PlayerHealthModel _healthModel;

        /// <summary>
        ///     HPが変更されたときViewの更新を呼び出す。
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="maxHealth"></param>
        private void HandleHealthChanged(int currentHealth, int maxHealth)
        {
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
