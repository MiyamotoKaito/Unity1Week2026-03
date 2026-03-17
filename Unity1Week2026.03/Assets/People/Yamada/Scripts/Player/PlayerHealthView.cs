using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week.URA.Player
{
    /// <summary>
    ///     プレイヤーのHPを表示するビュークラス。
    /// </summary>
    public class PlayerHealthView : MonoBehaviour
    {
        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            float ratio = (float)currentHealth / maxHealth;
            _healthBarFill.fillAmount = ratio;
        }

        [SerializeField] private Image _healthBarFill;
    }
}
