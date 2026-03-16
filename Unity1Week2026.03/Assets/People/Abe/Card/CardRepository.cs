using System.Collections.Generic;

/// <summary>
/// 盤面のカードを管理するクラス
/// </summary>
public class CardRepository
{
    public CardRepository()
    {

    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
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

    public void OpenCard(Card card)
    {
        foreach (var c in _cards)
        {
            if (c.GetCardBackText().Equals(card.GetCardBackText()))
            {
                c.OpenCard();
                TryResolveOpenPair();
                return;
            }
        }
    }
    public bool TryResolveOpenPair()
    {
        var first = GetFirstOpenCard();
        if (first == null)
        {
            return false;
        }

        var second = GetSecondOpenCard(first);
        if (second == null)
        {
            return false;
        }

        var match = first.Equals(second);
        if (match)
        {
            first.ExcuteEffect(); //どらちからの効果を発動させればよい。
           // second.ExcuteEffect();
            RemoveMatchCard(first, second);
            return true;
        }

        first.CloseCard();
        second.CloseCard();
        return false;
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
        _cards.RemoveAll(card => card.MatchCardText(first.GetCardBackText()) ||
         card.MatchCardText(second.GetCardBackText()));
    }
}
