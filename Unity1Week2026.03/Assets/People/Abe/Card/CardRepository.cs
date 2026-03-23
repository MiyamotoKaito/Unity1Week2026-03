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
    public event Action<Card, Card> OnMissMatchCard;
    public event Action OnAllCardsRemoved;

    public void AddCard(Card card)
    {
        _cards.Add(card);
        card.OnCardOpened += TryResolveOpenPair;
    }

    public IReadOnlyList<Card> GetCards()
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
        Debug.Log("[CardRepository] TryResolveOpenPair called.");
        var first = GetFirstOpenCard();

        if (first == null)
        {
            Debug.Log("[CardRepository] No open card found.");
            return;
        }
        Debug.Log($"[CardRepository] First open: id={first.GetCardId()}, text={first.GetCardBackText()}, mode={first.GetTriggerMode()}");

        if (first.GetTriggerMode() == CardTriggerMode.Single)
        {
            OnMatchCard?.Invoke(first, first);
            RemoveMatchCard(first, first);
            AudioManager.Instance.PlaySE("EnemySkill");
            Debug.Log("[CardRepository] Single card resolved (first).");
            return;
        }

        var second = GetSecondOpenCard(first);
        if (second == null)
        {
            Debug.Log("[CardRepository] Second open card not found.");
            return;
        }
        Debug.Log($"[CardRepository] Second open: id={second.GetCardId()}, text={second.GetCardBackText()}, mode={second.GetTriggerMode()}");
        if (second.GetTriggerMode() == CardTriggerMode.Single)
        {
            OnMatchCard?.Invoke(second, second);
            AudioManager.Instance.PlaySE("EnemySkill");
            Debug.Log("[CardRepository] Single card resolved (second).");
            return;
        }

        var match = first.GetCardId() == second.GetCardId();
        if (match)
        {
            OnMatchCard?.Invoke(first, second);
            RemoveMatchCard(first, second);
            Debug.Log("[CardRepository] Pair matched and resolved.");
            return;
        }

        OnMissMatchCard?.Invoke(first, second);

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

        if (_cards.Count == 0)
        {
            OnAllCardsRemoved?.Invoke();
        }
    }
}
