using UnityEngine;

namespace Unity1Week.URA.Player
{
    /// <summary>
    ///     プレイヤーのHPを管理するプレゼンタークラス。
    /// </summary>
    public class PlayerHealthPresenter : MonoBehaviour
    {
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

        private void Awake()
        {
            _healthModel = new PlayerHealthModel(_maxHealth);
            _healthModel.OnHealthChanged += HandleHealthChanged;
            _healthModel.OnPlayerDied += HandleDied;

            _healthView.UpdateHealthBar(_healthModel.CurrentHealth, _healthModel.MaxHealth);
        }

        private void OnDestroy()
        {
            _healthModel.OnHealthChanged -= HandleHealthChanged;
            _healthModel.OnPlayerDied -= HandleDied;
        }

        [SerializeField] private PlayerHealthView _healthView;
        [SerializeField] private int _maxHealth;

        private PlayerHealthModel _healthModel;
    }
}
