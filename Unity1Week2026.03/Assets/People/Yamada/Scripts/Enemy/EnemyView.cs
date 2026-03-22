using DG.Tweening;
using System.Collections.Generic;
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
            _enemySr = GetComponent<SpriteRenderer>();
            _enemySr.sprite = enemySr;
        }

        /// <summary>
        ///     演出を止める。
        /// </summary>
        public void StopPulse()
        {
            _attackTimerText.transform.DOKill();
            _attackTimerText.transform.localScale = Vector3.one;
        }

        /// <summary>
        ///     HPバーの表示を更新する。
        /// </summary>
        /// <param name="currentHp"></param>
        /// <param name="maxHp"></param>
        public void UpdateHealth(int currentHealth, int maxHealth)
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

        /// <summary>
        ///     攻撃の残り時間を更新する。
        /// </summary>
        /// <param name="remainingTimer"></param>
        public void UpdateAttackTimer(float remainingTimer)
        {
            _attackTimerText.text = Mathf.CeilToInt(remainingTimer).ToString();

            UpdateTimerColor(remainingTimer);
            UpdatePulse(remainingTimer);
        }

        /// <summary>
        ///     スキルの残りターンを更新する。
        /// </summary>
        /// <param name="remainingTurns"></param>
        public void UpdateSkillTurn(int remainingTurns, int startTurns)
        {
            _skillCountText.text = remainingTurns.ToString();
            float ratio = 1f - (float)remainingTurns / startTurns;
            _skillFillImage.fillAmount = ratio;
        }

        [Header("HP表示設定")]
        [SerializeField] private float _frontFillDuration = 0.2f;
        [SerializeField] private float _damageFillDelay = 0.1f;
        [SerializeField] private float _damageFillDuration = 0.5f;
        [SerializeField] private Image _healthBarFill;
        [SerializeField] private Image _damageBarFill;
        [SerializeField] private Image _skillFillImage;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _attackTimerText;
        [SerializeField] private TMP_Text _skillCountText;

        [Header("アニメーション設定")]
        [SerializeField] private List<TimerColorSetting> _timerColorSettings;
        [SerializeField] private float _pulseScale;
        [SerializeField] private float _pulseScaleSecond;
        [SerializeField] private float _pulseDuration;
        [SerializeField] private float _pulseStartTime;
        [SerializeField] private float _pulseSecondStartTime;
        [SerializeField] private Ease _pulseEase;

        private SpriteRenderer _enemySr;
        private bool _hasPulsed;
        private float _pulseTimer;
        private Color _defaultColor;

        /// <summary>
        ///     タイマーの色を変える。
        /// </summary>
        /// <param name="remainingTimer"></param>
        private void UpdateTimerColor(float remainingTimer)
        {
            foreach (var setting in _timerColorSettings)
            {
                if (remainingTimer > setting.Thresold)
                {
                    _attackTimerText.color = _defaultColor;
                    continue;
                }

                if (remainingTimer <= setting.Thresold)
                {
                    _attackTimerText.color = setting.Color;
                    break;
                }
            }
        }

        /// <summary>
        ///     テキスト演出を条件をチェックして再生する。
        /// </summary>
        /// <param name="remainingTimer"></param>
        private void UpdatePulse(float remainingTimer)
        {
            if (remainingTimer > _pulseStartTime)
            {
                _pulseTimer = 0f;
                return;
            }

            _pulseTimer += Time.deltaTime;

            if (_pulseTimer >= 1f)
            {
                _pulseTimer = 0f;
                PlayPulse(remainingTimer);
            }
        }

        /// <summary>
        ///     演出を再生する。
        /// </summary>
        /// <param name="remainingTimer"></param>
        private void PlayPulse(float remainingTimer)
        {
            float scale = remainingTimer <= _pulseSecondStartTime ? _pulseScaleSecond : _pulseScale;
            _attackTimerText.transform.DOKill();

            _attackTimerText.transform
                .DOScale(scale, _pulseDuration)
                .SetEase(_pulseEase)
                .OnComplete(() =>
                {
                    _attackTimerText.transform
                        .DOScale(1f, _pulseDuration)
                        .SetEase(_pulseEase);
                });
        }

        private void Awake()
        {
            _enemySr = GetComponent<SpriteRenderer>();
            _timerColorSettings.Sort((a, b) => a.Thresold.CompareTo(b.Thresold));
            _defaultColor = _attackTimerText.color;
        }
    }
}
