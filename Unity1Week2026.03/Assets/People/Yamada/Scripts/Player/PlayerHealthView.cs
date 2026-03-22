using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week.URA.Player
{
    /// <summary>
    ///     Player HP view.
    /// </summary>
    public class PlayerHealthView : MonoBehaviour
    {
        /// <summary>
        ///     Update HP bar.
        /// </summary>
        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            float ratio = (float)currentHealth / maxHealth;
            float prevRatio = _healthBarFill.fillAmount;

            _healthBarFill.DOKill();
            _healthBarFill.DOFillAmount(ratio, _frontFillDuration);

            if (_damageBarFill != null)
            {
                _damageBarFill.DOKill();

                if (ratio < prevRatio)
                {
                    // Damage: keep the red bar at the old value, then shrink after a delay.
                    _damageBarFill.fillAmount = prevRatio;
                    _damageBarFill.DOFillAmount(ratio, _damageFillDuration).SetDelay(_damageFillDelay);
                }
                else
                {
                    // Heal: follow immediately.
                    _damageBarFill.fillAmount = ratio;
                }
            }
        }

        [SerializeField] private Image _healthBarFill;
        [SerializeField] private Image _damageBarFill;
        [SerializeField] private float _frontFillDuration = 0.2f;
        [SerializeField] private float _damageFillDelay = 0.1f;
        [SerializeField] private float _damageFillDuration = 0.5f;
    }
}
