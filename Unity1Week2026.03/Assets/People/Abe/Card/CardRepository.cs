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

    public bool CardExists(int id,string text)
    {
        foreach (var card in _cards)
        {
            if (id == card.GetCardId() && text == card.GetCardBackText())
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
