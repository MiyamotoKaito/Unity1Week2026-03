using System;
using UnityEngine;
/// <summary>
/// Card entity
/// </summary>
public class Card
{
    public Card(int cardId, ICardEffect effect, Sprite frontSprite, string cardBackText, CardTriggerMode triggerMode)
    {
        _cardId = new CardId(cardId);
        _cardEffect = effect;//TODO: CardEffectの実装ができたら引数から渡すようにする
        _cardFrontSprite = new CardFrontSprite(frontSprite);
        _cardBackText = new CardBackText(cardBackText);
        _triggerMode = triggerMode;
    }

    public event Action OnCardOpened;
    public event Action OnCardClosed;

    public void OpenCard()
    {
        if (_isOpen)
        {
            Debug.LogWarning("カードはすでに開いています: " + _cardBackText.GetText());
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
            Debug.LogWarning("カードはすでに閉じています: " + _cardBackText.GetText());
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
        if (data == null)
        {
            return;
        }
        _cardId = new CardId(data.CardId);
        _cardEffect = data.CardEffect;
        _cardFrontSprite = new CardFrontSprite(data.FrontSprite);
        _triggerMode = data.TriggerMode;
    }

    public void ExcuteEffect()
    {
        _cardEffect.Exucute();
    }

    private bool _isOpen;
    private CardId _cardId;
    private ICardEffect _cardEffect;
    private CardFrontSprite _cardFrontSprite;
    private CardBackText _cardBackText;
    private CardTriggerMode _triggerMode;
}
