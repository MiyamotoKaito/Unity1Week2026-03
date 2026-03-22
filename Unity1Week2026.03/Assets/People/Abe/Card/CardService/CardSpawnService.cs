using UnityEngine;
/// <summary>
/// カードのペアを生成するクラス
/// </summary>
public class CardSpawnSystem
{
    public CardSpawnSystem(CardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public void ResetSpawnFlags()
    {
        _singlePairSpawned = false;
    }

    public bool SpawnCardPair(CardData cardData, string textA, string textB)
    {
        if (string.IsNullOrWhiteSpace(textA) || string.IsNullOrWhiteSpace(textB))
        {
            return false;
        }

        if (cardData != null && cardData.TriggerMode == CardTriggerMode.Single)
        {
            if (_singlePairSpawned)
            {
                return false;
            }
            _singlePairSpawned = true;
        }

        if (textA == textB)
        {
            return false;
        }

        if (_cardRepository.TextExists(textA) || _cardRepository.TextExists(textB))
        {
            return false;
        }

        var effectInstance = cardData.CreateEffectInstance();
        var cardA = new Card(cardData.CardId, effectInstance, cardData.FrontSprite, cardData.PowerSprite, textA, cardData.TriggerMode);
        var cardB = new Card(cardData.CardId, effectInstance, cardData.FrontSprite, cardData.PowerSprite, textB, cardData.TriggerMode);

        _cardRepository.AddCard(cardA);
        _cardRepository.AddCard(cardB);

        return true;
    }
    public bool CardCount(int count)
    {
        var cards = _cardRepository.GetCards();
        if (cards.Count >= count)
        {
            Debug.Log($"カードの数が上限に達しています。{cards.Count}枚のカードが存在します。");
            return false;
        }
        return true;
    }
    private CardRepository _cardRepository;
    private bool _singlePairSpawned;
}
