using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盤面のカードを管理するクラス
/// </summary>
public class CardRepository
{
    public CardRepository()
    {

    }
    public event Action OnClearCards;
    public event Action<Card, Card> OnMatchCard;

    public void AddCard(Card card)
    {
        _cards.Add(card);
        card.OnCardOpened += TryResolveOpenPair;
    }

    public List<Card> GetCards()
    {
        return _cards;
    }

    public Card FindCardByText(string text)
    {
        foreach (var card in _cards)
        {
            if (card.MatchCardText(text))
            {
                return card;
            }
        }

        return null;
    }

    public bool TextExists(string text)
    {
        foreach (var card in _cards)
        {
            if (card.MatchCardText(text))
            {
                return true;
            }
        }

        return false;
    }

    public void TryResolveOpenPair()
    {
        var first = GetFirstOpenCard();
        if (first == null)
        {
            return;
        }

        var second = GetSecondOpenCard(first);
        if (second == null)
        {
            return;
        }

        var match = first.Equals(second);
        if (match)
        {
            first.ExcuteEffect(); 
            OnMatchCard?.Invoke(first, second);
            RemoveMatchCard(first, second);
            return;
        }

        first.CloseCard();
        second.CloseCard();
        Debug.Log("カードのペアが一致しませんでした: " + first.GetCardBackText() + " - " + second.GetCardBackText());
        return;
    }

    public Card GetFirstOpenCard()
    {
        foreach (var card in _cards)
        {
            if (card.IsOpen())
            {
                return card;
            }
        }
        return null;
    }

    public void ClearCards()
    {
        foreach (var card in _cards)
        {
            card.OnCardOpened -= TryResolveOpenPair;
        }
        OnClearCards?.Invoke();
        _cards.Clear();
    }

    private List<Card> _cards = new List<Card>();

    private Card GetSecondOpenCard(Card first)
    {
        foreach (var card in _cards)
        {
            if (card != first && card.IsOpen())
            {
                return card;
            }
        }

        return null;
    }


    private void RemoveMatchCard(Card first, Card second)
    {
        foreach (var card in _cards)
        {
            if (card.MatchCardText(first.GetCardBackText()) ||
                card.MatchCardText(second.GetCardBackText()))
            {
                card.OnCardOpened -= TryResolveOpenPair;
            }
        }

        _cards.RemoveAll(card =>
            card.MatchCardText(first.GetCardBackText()) ||
            card.MatchCardText(second.GetCardBackText()));
    }
}
