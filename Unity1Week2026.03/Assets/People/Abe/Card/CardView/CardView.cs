using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class CardView : MonoBehaviour
{

    public void SetCard(Card card)
    {
        
        _image.sprite = card?.GetCardFrontSprite();
        _text.text = card?.GetCardBackText() ?? string.Empty;
        UpdateEffectValue(card);
    }

    public void PlayDebuffFx()
    {
        if (_animator != null && !string.IsNullOrEmpty(_debuffTrigger))
        {
            _animator.SetTrigger(_debuffTrigger);
        }
        if (_debuffParticle != null)
        {
            _debuffParticle.Play(true);
        }
    }
    public void ReverseText()
    {
        _text.text = new string(_text.text.Reverse().ToArray());
    }

    public void SetFont(TMP_FontAsset fontAsset)
    {
        _text.font = fontAsset;
    }
    private Image _image;
    private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _effectValueText;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _debuffTrigger = "Debuff";
    [SerializeField] private ParticleSystem _debuffParticle;
    private const string CardBackImagePath = "Text/BackGround/BackImage";
    private const string CardBackTextPath = "Text";
    private const string EffectValuePath = "EffectValue";
    private void Awake()
    {
        _image = transform.Find(CardBackImagePath).GetComponent<Image>();
        _text = transform.Find(CardBackTextPath).GetComponent<TextMeshProUGUI>();
        if (_effectValueText == null)
        {
            var effectValue = transform.Find(EffectValuePath);
            if (effectValue == null)
            {
                effectValue = transform.Find($"{CardBackTextPath}/{EffectValuePath}");
            }
            if (effectValue != null)
            {
                _effectValueText = effectValue.GetComponent<TextMeshProUGUI>();
            }
        }
        if (_animator == null)
        {
            _animator = GetComponentInChildren<Animator>(true);
        }
        if (_debuffParticle == null)
        {
            _debuffParticle = GetComponentInChildren<ParticleSystem>(true);
        }
    }

    private void UpdateEffectValue(Card card)
    {
        string effectText = string.Empty;
        if (card != null)
        {
            if (card.TryGetAttackBase(out var attack))
            {
                effectText = $"{attack.AttackPower}";
            }
            else if (card.TryGetHealBase(out var heal))
            {
                effectText = $"{heal.HealAmount}";
            }
        }

        if (_effectValueText != null)
        {
            _effectValueText.text = effectText;
            _effectValueText.gameObject.SetActive(!string.IsNullOrEmpty(effectText));
            return;
        }

        if (!string.IsNullOrEmpty(effectText))
        {
            _text.text = string.IsNullOrEmpty(_text.text) ? effectText : $"{_text.text}\n{effectText}";
        }
    }
}
