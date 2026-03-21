using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     被ダメージ時の画面演出を担当するViewクラス。
/// </summary>
public class DamageFeedbackView : MonoBehaviour
{
    /// <summary>
    ///     被ダメージ演出を再生する。
    /// </summary>
    public void PlayDamageFeedback()
    {
        PlayFlush();
        PlayShake();
    }

    [Header("Flush")]
    [SerializeField] private Image _damageFlushImage;
    [SerializeField] private float _flashPeekAlpha;
    [SerializeField] private float _flashfadeInDuration;
    [SerializeField] private float _flashFadeOutDuration;

    [Header("Shake")]
    [SerializeField] private Transform _shakeTarget;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _shakeVibrato;
    [SerializeField] private float _shakeRandomness;

    private Tween _flashTween;
    private Tween _shakeTween;

    /// <summary>
    ///     Flush演出を再生する。
    /// </summary>
    private void PlayFlush()
    {
        if(_damageFlushImage == null)
        {
            return;
        }

        _flashTween?.Kill();

        Color color = _damageFlushImage.color;
        color.a = 0f;
        _damageFlushImage.color = color;

        _flashTween = DOTween.Sequence()
            .Append(_damageFlushImage.DOFade(_flashPeekAlpha,_flashfadeInDuration))
            .Append(_damageFlushImage.DOFade(0f, _flashFadeOutDuration));
    }

    /// <summary>
    ///     画面揺れを再生する。
    /// </summary>
    private void PlayShake()
    {
        if(_shakeTarget == null)
        {
            return;
        }
        _shakeTween?.Kill();

        _shakeTween = _shakeTarget.DOShakePosition(
            _shakeDuration,
            _shakeStrength, 
            _shakeVibrato,
            _shakeRandomness,
            false,
            true);
    }

    private void Awake()
    {
        if(_damageFlushImage != null)
        {
            Color color = _damageFlushImage.color;
            color.a = 0f;
            _damageFlushImage.color = color;
        }
    }
}
