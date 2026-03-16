using UnityEngine;
/// <summary>
/// 蜷後§繧ｫ繝ｼ繝峨′隍・焚譫壼ｭ伜惠縺励↑縺・ｈ縺・↓縺吶ｋ縺溘ａ縺ｮ繧ｯ繝ｩ繧ｹ
/// </summary>
public class CardSpawnSystem
{
    private CardRepository _cardRepository;

    public CardSpawnSystem(CardRepository cardRepository)
    {
        _cardRepository = cardRepository;
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
        //TODO 繧ｫ繝ｼ繝峨・陬城擇縺ｮ繝・く繧ｹ繝医ｒ繧ｿ繧､繝斐Φ繧ｰ繧ｷ繧ｹ繝・Β縺九ｉ蜿励￠蜿悶ｋ縲・
        var cardObject = new GameObject("Card");
        var cardView = cardObject.AddComponent<CardView>();
        cardView.SetCard(card);
        //TODO 繧ｫ繝ｼ繝峨・菴咲ｽｮ繧呈ｱｺ繧√ｋ縲・
        //TODO 繧ｫ繝ｼ繝峨ｒ・呈椢逕滓・縺吶ｋ縲・
    }
}
