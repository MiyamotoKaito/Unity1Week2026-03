using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;
public class CardPresenter : MonoBehaviour
{
    [SerializeField]
    private CardController _cardController;
    [SerializeField]
    private CardViewSpawn _generateCardView;
    [SerializeField]
    private float _hideDelayAfterFlip = 0.0f;
    [SerializeField]
    private TMP_FontAsset[] _fontAsssetArray;
    private bool _isReverseMode = false;
    private List<GameObject> _cardObjects = new List<GameObject>();
    private List<CardRotate> _cardRotates = new List<CardRotate>();
    private Dictionary<string, GameObject> _textToObject = new();
    private Dictionary<Card, GameObject> _cardToObject = new();

    /// <summary>
    /// テキストを鏡文字表示にする
    /// </summary>
    public void MirrorTexts()
    {
        foreach (var text in _textToObject.Keys)
        {
            var obj = _textToObject[text];
            var cardView = obj.GetComponentInChildren<CardView>(true);
            if (cardView != null)
            {
                cardView.SetFont(_fontAsssetArray[1]);
            }
        }
    }
    public void MirrorTextTime(int seconds)
    {
        StartCoroutine(MirrorTextTimeCoroutine(seconds));
    }
    private IEnumerator MirrorTextTimeCoroutine(int seconds)
    {
        MirrorTexts();
        yield return new WaitForSeconds(seconds);
        NormalTexts();
    }
    /// <summary>
    /// テキストを通常表示に戻す
    /// </summary>
    public void NormalTexts()
    {
        foreach (var text in _textToObject.Keys)
        {
            var obj = _textToObject[text];
            var cardView = obj.GetComponentInChildren<CardView>(true);
            if (cardView != null)
            {
                cardView.SetFont(_fontAsssetArray[0]);
            }
        }
    }

    private void Start()
    {
        SetEvent();
        TrySyncExistingCards();
    }
    // テキストを逆文字にする
    public void ReversTexts()
    {
        foreach (var text in _textToObject.Keys)
        {
            var obj = _textToObject[text];
            var cardView = obj.GetComponentInChildren<CardView>(true);
            if (cardView != null)
            {
                cardView.ReverseText();
            }
        }
    }
    // カードのテキストのみをランダムに入れ替える（カードの内容は変わらない）
    public void ShuffleSomeCards(int shuffleCount)
    {
        var cards = _cardController.CardRepository.GetCards();
        if (cards == null || cards.Count < 2)
        {
            return;
        }
        if (_cardObjects == null || _cardObjects.Count != cards.Count)
        {
            return;
        }
        if (shuffleCount < 2)
        {
            return;
        }

        var count = Mathf.Clamp(shuffleCount, 2, cards.Count);
        var indices = new List<int>(cards.Count);
        for (int i = 0; i < cards.Count; i++)
        {
            indices.Add(i);
        }

        // Fisher–Yates to get random subset
        for (int i = indices.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (indices[i], indices[j]) = (indices[j], indices[i]);
        }

        var targetIndices = indices.GetRange(0, count);
        var texts = new List<string>(count);
        foreach (var index in targetIndices)
        {
            texts.Add(cards[index].GetCardBackText());
        }

        // Shuffle texts but avoid any card staying in the same position (derangement within targetIndices)
        var maxAttempts = 20;
        var attempts = 0;
        do
        {
            for (int i = texts.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (texts[i], texts[j]) = (texts[j], texts[i]);
            }
            attempts++;
        }
        while (attempts < maxAttempts && HasSamePosition(cards, targetIndices, texts));

        for (int i = 0; i < targetIndices.Count; i++)
        {
            var cardIndex = targetIndices[i];
            var card = cards[cardIndex];
            card.SetCardBackText(texts[i]);

            var cardObject = _cardObjects[cardIndex];
            cardObject.name = $"Card_{card.GetCardBackText()}";

            var cardView = cardObject.GetComponentInChildren<CardView>(true);
            if (cardView != null)
            {
                cardView.SetCard(card);
            }
        }

        RebuildTextIndex(cards);
    }
    [SerializeField]
    private CardData cardData;

    // テスト用: カード内容を置き換える
    public void ReplaceTest()
    {
        ReplaceCardContents(new List<CardData> { cardData });
    }
    // カード内容を置き換える（カードのIDに基づいて内容を更新する）
    public void ReplaceCardContents(IReadOnlyList<CardData> dataList)
    {
        if (dataList == null || dataList.Count == 0)
        {
            Debug.LogWarning("ReplaceCardContents: dataList is null or empty.");
            return;
        }

        var cards = _cardController.CardRepository.GetCards();
        if (cards == null || cards.Count == 0)
        {
            Debug.LogWarning("ReplaceCardContents: cards are not ready.");
            return;
        }
        if (_cardObjects == null || _cardObjects.Count != cards.Count)
        {
            Debug.LogWarning("ReplaceCardContents: card objects are not ready.");
            return;
        }

        var pairs = BuildPairs(cards);
        if (pairs.Count == 0)
        {
            Debug.LogWarning("ReplaceCardContents: no valid pairs found.");
            return;
        }

        ShuffleList(pairs);
        var applied = 0;
        for (int i = 0; i < dataList.Count && applied < pairs.Count; i++)
        {
            var data = dataList[i];
            if (data == null)
            {
                continue;
            }
            while (applied < pairs.Count)
            {
                var pair = pairs[applied];
                var currentId = cards[pair.IndexA].GetCardId();
                if (currentId != data.CardId)
                {
                    ApplyPairData(cards, pair, data);
                    applied++;
                    break;
                }
                applied++;
            }
        }
        Debug.Log("カード内容が置き換えられました");
    }
    private void SetEvent()
    {
        _cardController.CardRepository.OnClearCards += ClearCards;
        _cardController.CardRepository.OnMatchCard += HideMatchedCards;
        _cardController.CardRepository.OnMissMatchCard += OpenMissMatchedCards;
        _cardController.OnCardsGenereted += SetCards;
        _cardController.OnReverseModeChanged += ReversTexts;
        Debug.Log("[CardPresenter] Events subscribed.");
    }
    private void OnDisable()
    {
        if (_cardController == null || _cardController.CardRepository == null)
        {
            return;
        }
        _cardController.CardRepository.OnClearCards -= ClearCards;
        _cardController.CardRepository.OnMatchCard -= HideMatchedCards;
        _cardController.CardRepository.OnMissMatchCard -= OpenMissMatchedCards;
        _cardController.OnCardsGenereted -= SetCards;
        _cardController.OnReverseModeChanged -= ReversTexts;
        Debug.Log("[CardPresenter] Events unsubscribed.");
    }
    private void SetCards()
    {
        var cards = _cardController.CardRepository.GetCards();
        ClearCards();
        _cardObjects = _generateCardView.GenerateCard(cards.Count);

        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            var cardObject = _cardObjects[i];

            cardObject.name = $"Card_{card.GetCardBackText()}"; // デバッグ用にカードオブジェクトの名前を設定

            var cardView = cardObject.GetComponentInChildren<CardView>(true);
            var cardRotate = cardObject.GetComponentInChildren<CardRotate>(true);

            if (cardView == null || cardRotate == null)
            {
                Debug.LogError($"CardViewまたはCardRotateが見つかりません: {cardObject.name}");
                continue;
            }
            _textToObject[card.GetCardBackText()] = cardObject;
            _cardToObject[card] = cardObject;
            cardView.SetCard(card);
            card.OnCardOpened += cardRotate.OpenCard;
            card.OnCardClosed += cardRotate.CloseCard;
            _cardRotates.Add(cardRotate);
        }

    }

    public bool TryGetCardObject(Card card, out GameObject obj)
    {
        return _cardToObject.TryGetValue(card, out obj);
    }

    //ランダムに選ばれたペアの攻撃力を減少させる。
    public bool TryReduceAttackPowerForAnyPair(int amount, out GameObject objA, out GameObject objB)
    {
        objA = null;
        objB = null;
        if (_cardController == null || _cardController.CardRepository == null)
        {
            return false;
        }

        var cards = _cardController.CardRepository.GetCards();
        if (cards == null || cards.Count < 2)
        {
            return false;
        }

        var idToCard = new Dictionary<int, Card>();
        foreach (var card in cards)
        {
            if (card == null || !card.TryGetAttackBase(out _))
            {
                continue;
            }

            var id = card.GetCardId();
            if (idToCard.TryGetValue(id, out var first))
            {
                if (first.TryGetAttackBase(out var effectA) &&
                    card.TryGetAttackBase(out var effectB))
                {
                    if (ReferenceEquals(effectA, effectB))
                    {
                        effectA.AddAttackPower(-amount);
                    }
                    else
                    {
                        effectA.AddAttackPower(-amount);
                        effectB.AddAttackPower(-amount);
                    }
                }

                TryGetCardObject(first, out objA);
                TryGetCardObject(card, out objB);
                UpdateEffectText(first, objA);
                UpdateEffectText(card, objB);
                return true;
            }

            idToCard[id] = card;
        }

        return false;
    }
    // 全ての攻撃カードの攻撃力を減少させる。減少させたカードの数を返す。
    public int ReduceAttackPowerForAllAttackCards(int amount)
    {
        if (_cardController == null || _cardController.CardRepository == null)
        {
            return 0;
        }

        var cards = _cardController.CardRepository.GetCards();
        if (cards == null || cards.Count == 0)
        {
            return 0;
        }

        var handledEffects = new HashSet<AttackBase>();
        var updatedCount = 0;

        foreach (var card in cards)
        {
            if (card == null || !card.TryGetAttackBase(out var attack))
            {
                continue;
            }

            if (handledEffects.Add(attack))
            {
                // Same effect instance may be shared by pair
                attack.AddAttackPower(-amount);
            }

            if (TryGetCardObject(card, out var cardObject))
            {
                UpdateEffectText(card, cardObject);
                updatedCount++;
            }
        }

        return updatedCount;
    }

    private void ClearCards()
    {
        _cardObjects.ForEach(obj =>
        {
            Destroy(obj);
        });
        _cardObjects.Clear();
        _cardRotates.Clear();
        _textToObject.Clear();
        _cardToObject.Clear();
    }

    private void TrySyncExistingCards()
    {
        if (_cardController == null || _cardController.CardRepository == null)
        {
            return;
        }
        var cards = _cardController.CardRepository.GetCards();
        if (cards == null || cards.Count == 0)
        {
            return;
        }
        if (_cardObjects != null && _cardObjects.Count == cards.Count)
        {
            return;
        }
        SetCards();
    }
    private void HideMatchedCards(Card a, Card b)
    {
        Debug.Log($"[CardPresenter] OnMatchCard: a={a?.GetCardBackText()} id={a?.GetCardId()} b={b?.GetCardBackText()} id={b?.GetCardId()}");
        StartCoroutine(HideMatchedCardsAfterFlip(a, b));
    }
    private void OpenMissMatchedCards(Card a, Card b)
    {
        Debug.Log($"[CardPresenter] OnMissMatchCard: a={a?.GetCardBackText()} id={a?.GetCardId()} b={b?.GetCardBackText()} id={b?.GetCardId()}");
        StartCoroutine(CloseMismatchAfterFlip(a, b));
    }
    private IEnumerator CloseMismatchAfterFlip(Card a, Card b)
    {
        yield return WaitUntilBothOpen(a, b);

        a.CloseCard();
        b.CloseCard();
        
        yield return new WaitForSeconds(0.8f);
        EffectManager.Instance.ReduceEnemySkillTurn();

    }
    private IEnumerator HideMatchedCardsAfterFlip(Card a, Card b)
    {
        Debug.Log("[CardPresenter] HideMatchedCardsAfterFlip start.");
        yield return WaitUntilBothOpen(a, b);

        if (_hideDelayAfterFlip > 0f)
        {
            yield return new WaitForSeconds(_hideDelayAfterFlip);
        }

        if (_cardToObject.TryGetValue(a, out var objA))
        {
            Debug.Log("[CardPresenter] Hiding matched card A.");
            objA.SetActive(false);
        }
        if (_cardToObject.TryGetValue(b, out var objB))
        {
            Debug.Log("[CardPresenter] Hiding matched card B.");
            objB.SetActive(false);
        }
    }

    private IEnumerator WaitForFlipIfNeeded(Card card)
    {
        if (!_cardToObject.TryGetValue(card, out var obj))
        {
            yield break;
        }

        var rotate = obj.GetComponentInChildren<CardRotate>(true);
        if (rotate == null || !rotate.IsFlipping)
        {
            yield break;
        }

        bool done = false;
        void OnDone() => done = true;

        rotate.OnFlipCompleted += OnDone;
        while (!done && rotate != null && rotate.IsFlipping)
        {
            yield return null;
        }
        if (rotate != null)
        {
            rotate.OnFlipCompleted -= OnDone;
        }
    }

    private IEnumerator WaitUntilBothOpen(Card a, Card b)
    {
        Debug.Log("[CardPresenter] WaitUntilBothOpen start.");
        while (true)
        {
            var rotateA = GetRotate(a);
            var rotateB = GetRotate(b);
            if (rotateA == null || rotateB == null)
            {
                Debug.Log("[CardPresenter] WaitUntilBothOpen aborted: rotate not found.");
                yield break;
            }

            if (!rotateA.IsFlipping && !rotateB.IsFlipping && rotateA.IsBackActive && rotateB.IsBackActive)
            {
                Debug.Log("[CardPresenter] WaitUntilBothOpen complete.");
                yield break;
            }
            yield return null;
        }
    }

    private CardRotate GetRotate(Card card)
    {
        if (card == null)
        {
            return null;
        }
        if (!_cardToObject.TryGetValue(card, out var obj))
        {
            return null;
        }
        return obj.GetComponentInChildren<CardRotate>(true);
    }

    private void RebuildTextIndex(IReadOnlyList<Card> cards)
    {
        _textToObject.Clear();
        _cardToObject.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            _textToObject[cards[i].GetCardBackText()] = _cardObjects[i];
            _cardToObject[cards[i]] = _cardObjects[i];
        }
    }

    private void UpdateEffectText(Card card, GameObject cardObject)
    {
        if (card == null || cardObject == null)
        {
            return;
        }
        var view = cardObject.GetComponentInChildren<CardView>(true);
        if (view != null)
        {
            view.RefreshEffectValue(card);
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private List<PairInfo> BuildPairs(IReadOnlyList<Card> cards)
    {
        var byId = new Dictionary<int, List<int>>();
        for (int i = 0; i < cards.Count; i++)
        {
            var id = cards[i].GetCardId();
            if (!byId.TryGetValue(id, out var list))
            {
                list = new List<int>(2);
                byId.Add(id, list);
            }
            list.Add(i);
        }

        var pairs = new List<PairInfo>();
        foreach (var kvp in byId)
        {
            var indices = kvp.Value;
            for (int i = 0; i + 1 < indices.Count; i += 2)
            {
                pairs.Add(new PairInfo(indices[i], indices[i + 1]));
            }
        }
        return pairs;
    }

    private void ApplyPairData(IReadOnlyList<Card> cards, PairInfo pair, CardData data)
    {
        if (data == null)
        {
            return;
        }
        cards[pair.IndexA].ApplyCardData(data);
        cards[pair.IndexB].ApplyCardData(data);
        RefreshView(pair.IndexA, cards[pair.IndexA]);
        RefreshView(pair.IndexB, cards[pair.IndexB]);
    }

    private void RefreshView(int index, Card card)
    {
        var cardObject = _cardObjects[index];
        cardObject.name = $"Card_{card.GetCardBackText()}";

        var cardView = cardObject.GetComponentInChildren<CardView>(true);
        if (cardView != null)
        {
            cardView.SetCard(card);
        }
    }

    private readonly struct PairInfo
    {
        public readonly int IndexA;
        public readonly int IndexB;

        public PairInfo(int indexA, int indexB)
        {
            IndexA = indexA;
            IndexB = indexB;
        }
    }

    private bool HasSamePosition(IReadOnlyList<Card> cards, List<int> targetIndices, List<string> shuffledTexts)
    {
        for (int i = 0; i < targetIndices.Count; i++)
        {
            var cardIndex = targetIndices[i];
            if (cards[cardIndex].GetCardBackText() == shuffledTexts[i])
            {
                return true;
            }
        }
        return false;
    }
}
