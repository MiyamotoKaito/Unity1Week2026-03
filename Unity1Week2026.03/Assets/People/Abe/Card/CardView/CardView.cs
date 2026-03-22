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
        UpdatePowerSprite(card);
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

    public void RefreshEffectValue(Card card)
    {
        UpdateEffectValue(card);
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
    [SerializeField] private Image _powerImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _debuffTrigger = "Debuff";
    [SerializeField] private ParticleSystem _debuffParticle;
    private const string CardBackImagePath = "Text/BackGround/BackImage";
    private const string CardBackTextPath = "Text";
    private const string EffectValuePath = "EffectValue";
    private const string PowerImagePath = "PowerImage";
    private const string PowerPath = "PowerText";
    private void Awake()
    {
        _image = transform.Find(CardBackImagePath).GetComponent<Image>();
        _text = transform.Find(CardBackTextPath).GetComponent<TextMeshProUGUI>();
        if (_effectValueText == null)
        {
            var effectValue = transform.Find($"{CardBackImagePath}/{PowerImagePath}/{PowerPath}");
            if (effectValue == null)
            {
                effectValue = transform.Find($"{CardBackTextPath}/{EffectValuePath}");
            }
            if (effectValue != null)
            {
                _effectValueText = effectValue.GetComponent<TextMeshProUGUI>();
            }
        }
        if (_powerImage == null)
        {
            var power = transform.Find($"{CardBackImagePath}/{PowerImagePath}");
            if (power == null)
            {
                power = transform.Find($"{CardBackImagePath}/{PowerImagePath}");
            }
            if (power == null)
            {
                power = transform.Find(PowerPath);
            }
            if (power == null)
            {
                power = transform.Find($"{CardBackTextPath}/{PowerPath}");
            }
            if (power != null)
            {
                _powerImage = power.GetComponent<Image>();
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

    private void UpdatePowerSprite(Card card)
    {
        if (_powerImage == null)
        {
            return;
        }
        var sprite = card?.GetCardPowerSprite();
        _powerImage.sprite = sprite;
        _powerImage.gameObject.SetActive(sprite != null);
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
            else if(card.TryGetTimeBase(out var time))
            {
                effectText = $"{time.TimeToAdd}";
            }
            else if(card.TryGetTurnBase(out var turn))
            {
                effectText = $"{turn.TurnToAdd}";
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
