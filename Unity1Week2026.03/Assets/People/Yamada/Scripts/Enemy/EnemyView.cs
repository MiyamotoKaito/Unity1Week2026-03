using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵のHPなど表示用のビュークラス。
    /// </summary>
    public class EnemyView : MonoBehaviour
    {
        /// <summary>
        ///     初期化する。
        /// </summary>
        /// <param name="enemySr"></param>
        /// <param name="currentHp"></param>
        /// <param name="maxHp"></param>
        public void InitializeView(Sprite enemySr, int currentHp, int maxHp)
        {
            _enemySr.sprite = enemySr;
        }

        /// <summary>
        ///     HPバーの表示を更新する。
        /// </summary>
        /// <param name="curentHp"></param>
        /// <param name="maxHp"></param>
        public void UpdateHealthBar(int curentHp, int maxHp)
        {
            float ratio = (float)curentHp / maxHp;
            _hpFillImage.fillAmount = ratio;
        }

        /// <summary>
        ///     攻撃の残り時間を更新する。
        /// </summary>
        /// <param name="remainingTimer"></param>
        public void UpdateAttackTimer(float remainingTimer)
        {
            _attackTimerText.text = remainingTimer.ToString();
        }

        /// <summary>
        ///     スキルの残りターンを更新する。
        /// </summary>
        /// <param name="remainingTurns"></param>
        public void UpdateSkillTurn(int remainingTurns)
        {
            _skillCountText.text = remainingTurns.ToString();
        }

        [SerializeField] private Image _hpFillImage;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _attackTimerText;
        [SerializeField] private TMP_Text _skillCountText;

        private SpriteRenderer _enemySr;

        private void Awake()
        {
            _enemySr = GetComponent<SpriteRenderer>();
        }
    }
}
