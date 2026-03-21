using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

/// <summary>
///     ラウンド開始の演出を管理するクラス。
/// </summary>
public class RoundTransitionView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _black;
    [SerializeField] private CanvasGroup _text;
    [SerializeField] private TMP_Text _roundText;

    [Header("時間設定")]
    [SerializeField] private float _fadeIn;
    [SerializeField] private float _wait;
    [SerializeField] private float _blackOut;
    [SerializeField] private float _textDelay;
    [SerializeField] private float _textOut;

    public void Play(string message, Action mid, Action complete, bool sceneStarted)
    {
        gameObject.SetActive(true);

        _roundText.text = message;

        _black.alpha = 0f;
        _text.alpha = 0f;

        Sequence sequence = DOTween.Sequence();

        // フェードイン処理
        if (sceneStarted)
        {
            sequence.Append(_black.DOFade(1f, 0f))
                .Join(_text.DOFade(1f, 0f));
        }
        else
        {
            sequence.Append(_black.DOFade(1f, _fadeIn))
                .Join(_text.DOFade(1f, _fadeIn));
        }

        // 背景変更タイミング
        sequence.AppendCallback(() => mid?.Invoke());

        // 黒背景フェードアウト
        sequence.AppendInterval(_wait);
        sequence.Append(_black.DOFade(0f, _blackOut));

        // テキストフェードアウト
        sequence.AppendInterval(_textDelay);
        sequence.Append(_text.DOFade(0f, _textOut));

        // 完了時にイベント発火
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            complete?.Invoke();
        });
    }
}
