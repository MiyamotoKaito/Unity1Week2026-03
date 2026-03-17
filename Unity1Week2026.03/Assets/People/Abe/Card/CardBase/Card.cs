using System;
using UnityEngine;
/// <summary>
/// Card entity
/// </summary>
public class Card
{
    public Card(int cardId, CardEffect cardEffect, Sprite frontSprite, string cardBackText)
    {
        _cardId = new CardId(cardId);
        _cardEffect = new CardEffect();//TODO: CardEffectの実装ができたら引数から渡すようにする
        _cardFrontSprite = new CardFrontSprite(frontSprite);
        _cardBackText = new CardBackText(cardBackText);
    }

    public event Action OnCardOpened;

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

    public void ExcuteEffect()
    {
        _cardEffect.Exucute();
    }

    private bool _isOpen;
    private CardId _cardId;
    private CardEffect _cardEffect;
    private CardFrontSprite _cardFrontSprite;
    private CardBackText _cardBackText;
}
