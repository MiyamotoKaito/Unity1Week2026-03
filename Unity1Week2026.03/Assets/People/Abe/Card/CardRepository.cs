using System.Collections.Generic;

/// <summary>
/// 盤面のカードを管理するクラス
/// </summary>
public class CardRepository
{
    public CardRepository()
    {

    }

    public Card FindCard(string text)
    {
        CardBackText inputText = new CardBackText(text);

        foreach (var card in _cards)
        {
            if (inputText.Equals(card.GetCardBackText()))
            {
                return card;
            }
        }

        return null;
    }


    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public bool CardExists(CardId id, CardBackText text)
    {
        foreach (var card in _cards)
        {
            if (id.Equals(card.GetCardId()) && text.Equals(card.GetCardBackText()))
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
