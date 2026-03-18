
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
        TempCardTextData.ResetUsage();
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
        if (_cardDataArray == null || _cardDataArray.Length == 0)
        {
            Debug.LogError("CardData array is empty. Please assign CardData in the inspector.");
            return;
        }
        while (_cardSpawnSystem.CardCount(_maxCardPairs * 2))
        {
            if (!SpawnCardPair(_cardDataArray[UnityEngine.Random.Range(0, _cardDataArray.Length)]))
            {
                Debug.LogWarning("Failed to spawn a card pair. Stopping further attempts.");
                break;
            }
        }
        OnCardsGenereted?.Invoke();
    }

    private bool SpawnCardPair(CardData cardData)
    {
        if (!TempCardTextData.TryGetNextPair(out var textA, out var textB))
        {
            Debug.LogError("TempCardTextData needs at least 2 unique entries.");
            return false;
        }

        var spawned = _cardSpawnSystem.SpawnCardPair(cardData, textA, textB);
        if (!spawned)
        {
            Debug.LogWarning("Failed to spawn card pair. Texts may be duplicated or invalid.");
        }
        return spawned;
    }
}
