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

    public bool CardExists(int id,string text)
    {
        foreach (var card in _cards)
        {
            if (card.MatchCardId(id) && card.MatchCardText(text))
            {
                return true;
            }
        }

        return false;
    }

    public void ClearCards()
    {
        _cards.Clear();
    }

    private List<Card> _cards = new List<Card>();
}
