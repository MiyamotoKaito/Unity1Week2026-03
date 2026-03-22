using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResetAnimation : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void PlayResetAnimation(float value)
    {
        _image.DOKill();
        _image.DOFillAmount(value, 0.5f)
              .SetEase(Ease.OutCubic);
    }
    public void ResetFillAmount()
    {
        _image.DOKill();
        _image.fillAmount = 0f;
    }
}
