
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
    private bool _isInitialized = false;
    public void SpawnCards()
    {
        EnsureTextDataInitialized();
        if (_reverseMode)
        {
            _reverseMode = false;
            OnReverseModeChanged?.Invoke();
        }
        if(!_isInitialized)
        {
           EffectManager.Instance.ReduceEnemySkillTurn(1);
        }
        else
        {
            _isInitialized = false;
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
        _isInitialized = true;
        _cardRepository = new CardRepository();
        _cardSpawnSystem = new CardSpawnSystem(_cardRepository);
    }
    [SerializeField] private CardData[] _cardDataArray;

    [SerializeField] private int _maxCardPairs = 9;
    private CardSpawnSystem _cardSpawnSystem;
    private CardRepository _cardRepository;
    [SerializeField]
    private bool _reverseMode = false;
    [SerializeField]
    private ResetAnimation _resetAnimation;
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
            _resetAnimation?.PlayResetAnimation(_holdTime / _longPressThreshold);
            if (!_longPressTriggered && _holdTime > _longPressThreshold)
            {
                _longPressTriggered = true;
                SpawnCards();
                Debug.Log("Reverse mode toggled: " + IsReverseMode);
               
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _holdTime = 0f;
            _longPressTriggered = false;
            _resetAnimation?.ResetFillAmount();
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
        if (nonSingleList.Count == 0)
        {
            Debug.LogError("CardData array has no non-single entries.");
            return;
        }

        LogCardDataDebug(nonSingleList);

        var available = new System.Collections.Generic.List<CardData>(nonSingleList);
        ShuffleList(available);
        var pairTarget = _maxCardPairs;

        for (int i = 0; i < pairTarget && _cardSpawnSystem.CardCount(_maxCardPairs * 2); i++)
        {
            if (available.Count == 0)
            {
                available = new System.Collections.Generic.List<CardData>(nonSingleList);
                ShuffleList(available);
            }

            var lastIndex = available.Count - 1;
            var cardData = available[lastIndex];
            available.RemoveAt(lastIndex);

            Debug.Log($"[CardSpawn] Pick {i + 1}/{pairTarget}: name={cardData.name} id={cardData.CardId} instanceId={cardData.GetInstanceID()}");

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

    private void ShuffleList<T>(System.Collections.Generic.List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            var j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private void LogCardDataDebug(System.Collections.Generic.List<CardData> nonSingleList)
    {
        Debug.Log($"[CardSpawn] CardData total={_cardDataArray.Length}, nonSingle={nonSingleList.Count}, maxPairs={_maxCardPairs}");

        var idCount = new System.Collections.Generic.Dictionary<int, int>();
        var nameCount = new System.Collections.Generic.Dictionary<string, int>();
        var instanceCount = new System.Collections.Generic.Dictionary<int, int>();

        foreach (var data in nonSingleList)
        {
            if (data == null)
            {
                continue;
            }

            var id = data.CardId;
            idCount[id] = idCount.TryGetValue(id, out var c) ? c + 1 : 1;

            var name = data.name ?? string.Empty;
            nameCount[name] = nameCount.TryGetValue(name, out var n) ? n + 1 : 1;

            var instanceId = data.GetInstanceID();
            instanceCount[instanceId] = instanceCount.TryGetValue(instanceId, out var i) ? i + 1 : 1;
        }

        foreach (var kvp in idCount)
        {
            if (kvp.Value > 1)
            {
                Debug.LogWarning($"[CardSpawn] Duplicate CardId found: id={kvp.Key} count={kvp.Value}");
            }
        }
        foreach (var kvp in nameCount)
        {
            if (kvp.Value > 1)
            {
                Debug.LogWarning($"[CardSpawn] Duplicate CardData name found: name={kvp.Key} count={kvp.Value}");
            }
        }
        foreach (var kvp in instanceCount)
        {
            if (kvp.Value > 1)
            {
                Debug.LogWarning($"[CardSpawn] Duplicate CardData reference found: instanceId={kvp.Key} count={kvp.Value}");
            }
        }
    }
}
