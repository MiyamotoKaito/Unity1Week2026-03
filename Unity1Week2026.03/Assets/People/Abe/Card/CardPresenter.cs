using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
        _cardController.OnReverseModeChanged += ReversTexts;
        SetEvent();
    }
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

        for (int i = texts.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (texts[i], texts[j]) = (texts[j], texts[i]);
        }

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
    private void SetEvent()
    {
        _cardController.CardRepository.OnClearCards += ClearCards;
        _cardController.CardRepository.OnMatchCard += HideMatchedCards;
        _cardController.CardRepository.OnMissMatchCard += OpenMissMatchedCards;
        _cardController.OnCardsGenereted += SetCards;
    }
    private void OnDisable()
    {
        _cardController.CardRepository.OnClearCards -= ClearCards;
        _cardController.CardRepository.OnMatchCard -= HideMatchedCards;
        _cardController.CardRepository.OnMissMatchCard -= OpenMissMatchedCards;
        _cardController.OnCardsGenereted -= SetCards;
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
            cardView.SetCard(card);
            card.OnCardOpened += cardRotate.OpenCard;
            card.OnCardClosed += cardRotate.CloseCard;
            _cardRotates.Add(cardRotate);
        }

        Debug.Log("カードがセットされました");
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
    }
    private void HideMatchedCards(Card a, Card b)
    {
        StartCoroutine(HideMatchedCardsAfterFlip(a, b));
    }
    private void OpenMissMatchedCards(Card a, Card b)
    {
        StartCoroutine(CloseMismatchAfterFlip(a, b));
    }
    private IEnumerator CloseMismatchAfterFlip(Card a, Card b)
    {
        yield return WaitUntilBothOpen(a, b);

        a.CloseCard();
        b.CloseCard();
    }
    private IEnumerator HideMatchedCardsAfterFlip(Card a, Card b)
    {
        yield return WaitUntilBothOpen(a, b);

        if (_hideDelayAfterFlip > 0f)
        {
            yield return new WaitForSeconds(_hideDelayAfterFlip);
        }

        if (_textToObject.TryGetValue(a.GetCardBackText(), out var objA))
        {
            objA.SetActive(false);
        }
        if (_textToObject.TryGetValue(b.GetCardBackText(), out var objB))
        {
            objB.SetActive(false);
        }
    }

    private IEnumerator WaitForFlipIfNeeded(Card card)
    {
        if (!_textToObject.TryGetValue(card.GetCardBackText(), out var obj))
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
        while (true)
        {
            var rotateA = GetRotate(a);
            var rotateB = GetRotate(b);
            if (rotateA == null || rotateB == null)
            {
                yield break;
            }

            if (!rotateA.IsFlipping && !rotateB.IsFlipping && rotateA.IsBackActive && rotateB.IsBackActive)
            {
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
        if (!_textToObject.TryGetValue(card.GetCardBackText(), out var obj))
        {
            return null;
        }
        return obj.GetComponentInChildren<CardRotate>(true);
    }

    private void RebuildTextIndex(IReadOnlyList<Card> cards)
    {
        _textToObject.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            _textToObject[cards[i].GetCardBackText()] = _cardObjects[i];
        }
    }
}
