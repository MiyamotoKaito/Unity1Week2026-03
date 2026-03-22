using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     ゲームクリア・ゲームオーバー演出を管理するクラス
/// </summary>
public class ResultView : MonoBehaviour
{
    [Header("表示ルート")]
    [SerializeField] private GameObject _resultRoot;

    [Header("参照")]
    [SerializeField] private Image _fadePanel;
    [SerializeField] private ResultTextSpawer _textSpawner;
    [SerializeField] private Button _backToTitleButton;

    [Header("共通設定")]
    [SerializeField] private float _fadeDuration = 0.3f;

    [Header("GameClear設定")]
    [SerializeField] private float _clearCharDelay = 0.05f;
    [SerializeField] private float _clearMoveY = 50f;

    [Header("GameOver設定")]
    [SerializeField] private float _overCharDelay = 0.08f;
    [SerializeField] private float _dropHeight = 300f;
    [SerializeField] private float _bounceHeight = 20f;

    private readonly List<Tween> _playingTweens = new();

    private void Awake()
    {
        if (_resultRoot != null)
        {
            _resultRoot.SetActive(false);
        }
    }

    /// <summary>
    ///     ゲームクリア演出を再生する
    /// </summary>
    public void PlayGameClear()
    {
        ShowResultRoot();
        PrepareCommonState();

        if (_fadePanel != null)
        {
            _fadePanel.DOFade(0.6f, _fadeDuration);
        }

        List<TextMeshProUGUI> chars = _textSpawner.Spawn("GAME CLEAR");

        for (int i = 0; i < chars.Count; i++)
        {
            TextMeshProUGUI c = chars[i];
            RectTransform rect = c.rectTransform;

            Vector2 basePos = rect.anchoredPosition;
            rect.anchoredPosition = basePos + Vector2.down * _clearMoveY;
            rect.localScale = Vector3.zero;

            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(i * _clearCharDelay);
            seq.Append(rect.DOAnchorPos(basePos, 0.2f).SetEase(Ease.OutBack));
            seq.Join(rect.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack));
            seq.Append(rect.DOScale(1f, 0.08f));

            _playingTweens.Add(seq);
        }

        ShowButtonWithDelay(chars.Count * _clearCharDelay + 0.35f);
    }

    /// <summary>
    ///     ゲームオーバー演出を再生する
    /// </summary>
    public void PlayGameOver()
    {
        ShowResultRoot();
        PrepareCommonState();

        if (_fadePanel != null)
        {
            _fadePanel.DOFade(0.8f, _fadeDuration);
        }

        List<TextMeshProUGUI> chars = _textSpawner.Spawn("GAME OVER");

        for (int i = 0; i < chars.Count; i++)
        {
            TextMeshProUGUI c = chars[i];
            RectTransform rect = c.rectTransform;

            Vector2 basePos = rect.anchoredPosition;
            rect.anchoredPosition = basePos + Vector2.up * _dropHeight;
            rect.localScale = Vector3.one;
            rect.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-20f, 20f));

            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(i * _overCharDelay);
            seq.Append(rect.DOAnchorPos(basePos, 0.45f).SetEase(Ease.InQuad));
            seq.Join(rect.DORotate(Vector3.zero, 0.25f).SetEase(Ease.OutQuad));
            seq.Append(rect.DOAnchorPosY(basePos.y + _bounceHeight, 0.12f).SetEase(Ease.OutQuad));
            seq.Append(rect.DOAnchorPosY(basePos.y, 0.08f).SetEase(Ease.InQuad));

            _playingTweens.Add(seq);
        }

        ShowButtonWithDelay(chars.Count * _overCharDelay + 0.55f);
    }

    /// <summary>
    ///     結果UIを表示する
    /// </summary>
    private void ShowResultRoot()
    {
        if (_resultRoot != null)
        {
            _resultRoot.SetActive(true);
        }
    }

    /// <summary>
    ///     共通の初期状態を整える
    /// </summary>
    private void PrepareCommonState()
    {
        KillTweens();

        _textSpawner.Clear();

        if (_fadePanel != null)
        {
            Color color = _fadePanel.color;
            color.a = 0f;
            _fadePanel.color = color;
        }

        if (_backToTitleButton != null)
        {
            _backToTitleButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    ///     ボタンを少し遅れて表示する
    /// </summary>
    private void ShowButtonWithDelay(float delay)
    {
        if (_backToTitleButton == null)
        {
            return;
        }

        DOVirtual.DelayedCall(delay, () =>
        {
            if (_backToTitleButton != null)
            {
                _backToTitleButton.gameObject.SetActive(true);
            }
        });
    }

    /// <summary>
    ///     再生中Tweenを停止する
    /// </summary>
    private void KillTweens()
    {
        foreach (Tween tween in _playingTweens)
        {
            tween?.Kill();
        }

        _playingTweens.Clear();
    }
}