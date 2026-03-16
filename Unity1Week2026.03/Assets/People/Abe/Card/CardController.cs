using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardRepository CardRepository => _cardRepository;
    [SerializeField] private CardData[] _cardDataArray;
    private CardSpawnSystem _cardSpawnSystem;
    private CardRepository _cardRepository;

    private void Start()
    {
        _cardRepository = new CardRepository();
        _cardSpawnSystem = new CardSpawnSystem(_cardRepository);
        SpawnCardPair(_cardDataArray[0]); 
        SpawnCardPair(_cardDataArray[1]);
    }

    private void SpawnCardPair(CardData cardData)
    {
        if (!TempCardTextData.TryGetNextPair(out var textA, out var textB))
        {
            Debug.LogError("TempCardTextData needs at least 2 unique entries.");
            return;
        }

        var spawned = _cardSpawnSystem.SpawnCardPair(cardData, textA, textB);
        if (!spawned)
        {
            Debug.LogWarning("Failed to spawn card pair. Texts may be duplicated or invalid.");
        }
    }
}
