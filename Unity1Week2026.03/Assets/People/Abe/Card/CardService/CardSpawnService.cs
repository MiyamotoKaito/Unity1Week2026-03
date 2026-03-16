using UnityEngine;
/// <summary>
/// カードのペアを生成するクラス
/// </summary>
public class CardSpawnSystem
{
    private CardRepository _cardRepository;
    private GameObject _cardParent;
    public CardSpawnSystem(CardRepository cardRepository, GameObject cardParent)
    {
        _cardRepository = cardRepository;
        _cardParent = cardParent;
    }

    public bool SpawnCardPair(CardData cardData, string textA, string textB)
    {
        if (string.IsNullOrWhiteSpace(textA) || string.IsNullOrWhiteSpace(textB))
        {
            return false;
        }

        if (textA == textB)
        {
            return false;
        }

        if (_cardRepository.TextExists(textA) || _cardRepository.TextExists(textB))
        {
            return false;
        }

        var cardA = new Card(cardData.CardId, cardData.CardEffect, cardData.FrontSprite, textA);
        var cardB = new Card(cardData.CardId, cardData.CardEffect, cardData.FrontSprite, textB);

        _cardRepository.AddCard(cardA);
        _cardRepository.AddCard(cardB);

        CreateCardObject(cardA);
        CreateCardObject(cardB);
        return true;
    }

    private void CreateCardObject(Card card)
    {
        var cardObject = new GameObject("Card");
        cardObject.transform.SetParent(_cardParent.transform, false);
        var cardView = cardObject.AddComponent<CardView>();
        cardView.SetCard(card);
    }
}
