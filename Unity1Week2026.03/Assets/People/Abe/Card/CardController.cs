
using System;
using UnityEngine;
[DefaultExecutionOrder(-111)]
public class CardController : MonoBehaviour
{
    public event Action OnCardsGenereted;
    public int MaxCardPairs => _maxCardPairs;
    public CardRepository CardRepository => _cardRepository;

    public void ClearCards()
    {
        _cardRepository.ClearCards();
    }
    public void ButtonSpawnCards()
    {
        SpawnCards();
    }
    [SerializeField] private CardData[] _cardDataArray;

    [SerializeField] private int _maxCardPairs = 2;
    private CardSpawnSystem _cardSpawnSystem;
    private CardRepository _cardRepository;

    private void Awake()
    {
        _cardRepository = new CardRepository();
        _cardSpawnSystem = new CardSpawnSystem(_cardRepository);
    }
    private void SpawnCards()
    {
        while (_cardSpawnSystem.CardCount(_maxCardPairs * 2))
        {
            SpawnCardPair(_cardDataArray[UnityEngine.Random.Range(0, _cardDataArray.Length)]);
        }
        OnCardsGenereted?.Invoke();
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
