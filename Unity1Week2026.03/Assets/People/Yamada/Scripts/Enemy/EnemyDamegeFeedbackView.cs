using DG.Tweening;
using UnityEngine;

/// <summary>
///     Enemyのダメージを受けたときの演出を管理するクラス。
/// </summary>
public class EnemyDamegeFeedbackView : MonoBehaviour
{
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashFadeInDuration;
    [SerializeField] private float _flashFadeOutDuration;

    private SpriteRenderer _enemy;
    private Tween _flashTween;
    private Color _defaultColor;

    public void PlayFlush()
    {
        if (_enemy == null)
        {
            return;
        }

        _flashTween?.Kill();

        _enemy.color = _defaultColor;

        _flashTween = DOTween.Sequence()
            .Append(_enemy.DOColor(_flashColor, _flashFadeInDuration))
            .Append(_enemy.DOColor(_defaultColor, _flashFadeOutDuration));
    }

    private void Awake()
    {
        _enemy = GetComponent<SpriteRenderer>();
        if(_enemy != null)
        {
            _defaultColor = _enemy.color;
        }
    }
}
