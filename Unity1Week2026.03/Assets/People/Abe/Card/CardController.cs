
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
    public event Action OnReverseModeChanged;

    public void SpawnCards()
    {
        EnsureTextDataInitialized();
        if (_reverseMode)
        {
            _reverseMode = false;
            OnReverseModeChanged?.Invoke();
        }
        _cardRepository.ClearCards();
        TempCardTextData.ResetUsage();
        _cardSpawnSystem.ResetSpawnFlags();
        TrySpawnCards();
    }
    public void ReverseTexts()
    {
        if (!IsReverseMode)
        {
            _reverseMode = true;
        }
        else
        {
            _reverseMode = false;
        }
        OnReverseModeChanged?.Invoke();
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
    private float _holdTime = 0f;
    private bool _longPressTriggered = false;
    private float _longPressThreshold = 1f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _holdTime = 0f;
            _longPressTriggered = false;
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            _holdTime += Time.deltaTime;
            if (!_longPressTriggered && _holdTime > _longPressThreshold)
            {
                _longPressTriggered = true;
                SpawnCards();
                Debug.Log("Reverse mode toggled: " + IsReverseMode);
               
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                _holdTime = 0f;
                _longPressTriggered = false;
            }
        }
    }
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
        var nonSingleList = BuildNonSingleList();

        while (_cardSpawnSystem.CardCount(_maxCardPairs * 2))
        {
            var cardData = nonSingleList[UnityEngine.Random.Range(0, nonSingleList.Count)];
            if (!SpawnCardPair(cardData))
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

    private CardData FindSingleCardData()
    {
        foreach (var data in _cardDataArray)
        {
            if (data != null && data.TriggerMode == CardTriggerMode.Single)
            {
                return data;
            }
        }
        return null;
    }

    private System.Collections.Generic.List<CardData> BuildNonSingleList()
    {
        var list = new System.Collections.Generic.List<CardData>();
        foreach (var data in _cardDataArray)
        {
            if (data != null && data.TriggerMode != CardTriggerMode.Single)
            {
                list.Add(data);
            }
        }
        return list;
    }
}
