
using System;
using UnityEngine;
using Unity1Week.URA.typing;
[DefaultExecutionOrder(-111)]
public class CardController : MonoBehaviour
{
    public bool IsReverseMode => _reverseMode;
    public event Action OnCardsGenereted;
    public int MaxCardPairs => _maxCardPairs;
    public CardRepository CardRepository => _cardRepository;

    public void SpawnCards()
    {
        EnsureTextDataInitialized();
        _cardRepository.ClearCards();
        TempCardTextData.ResetUsage();
        TrySpawnCards();
    }
    public void ReverseTexts()
    {
        _reverseMode = true;
    }
    public void Init()
    {
        _cardRepository = new CardRepository();
        _cardSpawnSystem = new CardSpawnSystem(_cardRepository);
    }
    [SerializeField] private CardData[] _cardDataArray;

    [SerializeField] private int _maxCardPairs = 9;
    private CardSpawnSystem _cardSpawnSystem;
    private CardRepository _cardRepository;
    [SerializeField]
    private bool _reverseMode = false;

    private void EnsureTextDataInitialized()
    {
        if (LoadText.WordList.Count == 0)
        {
            Debug.Log("GetCSV");
            var csv = LoadText.GetCSVFile();
            if (csv != null)
            {
                LoadText.AssemblyWords(csv);
            }
        }

        TempCardTextData.SetTexts(LoadText.WordList);
    }
    private void TrySpawnCards()
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
