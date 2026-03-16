using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardData[] _cardDataArray;
    private CardSpawnSystem _cardSpawnSystem;
    private CardRepository _cardRepository;
    

    private void Start()
    {
        _cardRepository = new CardRepository();
        _cardSpawnSystem = new CardSpawnSystem(_cardRepository);
    }
   
    public void SpawnCard(CardData cardData)
    {
        var card = new Card(cardData.CardId, cardData.CardEffect, cardData.FrontSprite, "カードの裏面のテキスト");
        _cardSpawnSystem.SpawnCard(card);
    }

}
