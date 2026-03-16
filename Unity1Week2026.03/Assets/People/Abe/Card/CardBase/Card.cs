using Unity.VectorGraphics;
using UnityEngine;
/// <summary>
/// Card entity
/// </summary>
public class Card
{
    public Card(int cardId, CardEffect cardEffect, Sprite frontSprite, string cardBackText)
    {
        _cardId = new CardId(cardId);
        _cardEffect = cardEffect;
        _cardFrontSprite = new CardFrontSprite(frontSprite);
        _cardBackText = new CardBackText(cardBackText);
    }
    
    public override bool Equals(object card)
    {
        if(card is not Card other) return false;
        return _cardId.GetId() == other._cardId.GetId();
    }

    public void OpenCard()
    {
        _isOpen = true;
    }

    public void CloseCard()
    {
        _isOpen = false;
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
