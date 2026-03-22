using System;
using Unity1Week.URA.Enemy;
using UnityEngine;
/// <summary>
/// Card entity
/// </summary>
public class Card
{
    public Card(int cardId, ICardEffect effect, Sprite frontSprite, Sprite powerSprite, string cardBackText, CardTriggerMode triggerMode)
    {
        _cardId = new CardId(cardId);
        _cardEffect = effect;//TODO: CardEffectの実裁E��できたら引数から渡すよぁE��する
        _cardFrontSprite = new CardFrontSprite(frontSprite);
        _cardPowerSprite = powerSprite;
        _cardBackText = new CardBackText(cardBackText);
        _triggerMode = triggerMode;
    }

    public event Action OnCardOpened;
    public event Action OnCardClosed;

    public void OpenCard()
    {
        if (_isOpen)
        {
            Debug.LogWarning("カード�Eすでに開いてぁE��ぁE " + _cardBackText.GetText());
            return;
        }
        _isOpen = true;
        Debug.Log("カードを開きました: " + _cardBackText.GetText());
        OnCardOpened?.Invoke();
        
    }

    public void CloseCard()
    {
        if (!_isOpen)
        {
            Debug.LogWarning("カード�Eすでに閉じてぁE��ぁE " + _cardBackText.GetText());
            return;
        }
        _isOpen = false;
        Debug.Log("カードを閉じました: " + _cardBackText.GetText());
        OnCardClosed?.Invoke();
    }

    public bool IsOpen()
    {
        return _isOpen;
    }

    public bool MatchCardText(string text)
    {
        if (GetCardBackText() == text)
        {
            return true;
        }

        return false;
    }

    public int GetCardId()
    {
        return _cardId.GetId();
    }

    public Sprite GetCardFrontSprite()
    {
        return _cardFrontSprite.GetSprite();
    }

    public Sprite GetCardPowerSprite()
    {
        return _cardPowerSprite;
    }

    public string GetCardBackText()
    {
        return _cardBackText.GetText();
    }

    public void SetCardBackText(string text)
    {
        _cardBackText = new CardBackText(text);
    }

    public CardTriggerMode GetTriggerMode()
    {
        return _triggerMode;
    }

    public void ApplyCardData(CardData data)
    {
        ApplyCardData(data, data != null ? data.CreateEffectInstance() : null);
    }

    public void ApplyCardData(CardData data, ICardEffect effectInstance)
    {
        if (data == null)
        {
            return;
        }
        _cardId = new CardId(data.CardId);
        _cardEffect = effectInstance;
        _cardFrontSprite = new CardFrontSprite(data.FrontSprite);
        _cardPowerSprite = data.PowerSprite;
        _triggerMode = data.TriggerMode;
    }

    public void ExcuteEffect()
    {
        _cardEffect.Exucute();
    }

    public bool TryGetAttackBase(out AttackBase attackBase)
    {
        if (_cardEffect is AttackBase attack)
        {
            attackBase = attack;
            return true;
        }
        attackBase = null;
        return false;
    }

    public bool TryGetHealBase(out HealBase healBase)
    {
        if (_cardEffect is HealBase heal)
        {
            healBase = heal;
            return true;
        }
        healBase = null;
        return false;
    }

    public bool TryGetTimeBase(out TimeBase timeBase)
    {
        if (_cardEffect is TimeBase time)
        {
            timeBase = time;
            return true;
        }
        timeBase = null;
        return false;
    }

    public bool TryGetTurnBase(out TurnBase turnBase)
    {
        if (_cardEffect is TurnBase turn)
        {
            turnBase = turn;
            return true;
        }
        turnBase = null;
        return false;
    }
    public bool TryClairvoyance(out Clairvoyance clairvoyance)
    {
        if (_cardEffect is Clairvoyance clairvoyanceEffect)
        {
            clairvoyance = clairvoyanceEffect;
            return true;
        }
        clairvoyance = null;
        return false;
    }
    public bool TryGetAllTextShuffleSkill(out AllTextShuffleSkill allTextShuffleSkill)
    {
        if (_cardEffect is AllTextShuffleSkill skill)
        {
            allTextShuffleSkill = skill;
            return true;
        }
        allTextShuffleSkill = null;
        return false;
    }
    public bool TryGetDuplicateJammerSkill(out DuplicateJammerCardSkill duplicateJammerPairSkill)
    {
        if (_cardEffect is DuplicateJammerCardSkill skill)
        {
            duplicateJammerPairSkill = skill;
            return true;
        }
        duplicateJammerPairSkill = null;
        return false;
    }
    public bool TryGetReducePowerSkill(out ReduceAttackPowerSkill reducePowerSkill)
    {
        if (_cardEffect is ReduceAttackPowerSkill skill)
        {
            reducePowerSkill = skill;
            return true;
        }
        reducePowerSkill = null;
        return false;
    }
    public bool TryGetShieldEnemySkill(out ShieldEnemySkill shieldEnemySkill)
    {
        if (_cardEffect is ShieldEnemySkill skill)
        {
            shieldEnemySkill = skill;
            return true;
        }
        shieldEnemySkill = null;
        return false;
    }
    public bool TryGetShuffleCardPositionSkill(out ShuffleCardPositionsSkill shuffleCardPositionSkill)
    {
        if (_cardEffect is ShuffleCardPositionsSkill skill)
        {
            shuffleCardPositionSkill = skill;
            return true;
        }
        shuffleCardPositionSkill = null;
        return false;
    }

    private bool _isOpen;
    private CardId _cardId;
    private ICardEffect _cardEffect;
    private CardFrontSprite _cardFrontSprite;
    private Sprite _cardPowerSprite;
    private CardBackText _cardBackText;
    private CardTriggerMode _triggerMode;
}


