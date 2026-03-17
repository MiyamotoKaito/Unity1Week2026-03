using System.Collections.Generic;
using UnityEngine;

public class CardPresenter : MonoBehaviour
{
    [SerializeField]
    private CardController _cardController;
    private List<GameObject> _cardObjects = new List<GameObject>();
    private List<CardRotate> _cardRotates = new List<CardRotate>();
    private Dictionary<string, GameObject> _textToObject = new();

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            _cardObjects.Add(child.gameObject);
        }
        _cardController.CardRepository.OnClearCards += ClearCards;
        _cardController.CardRepository.OnMatchCard += HideMatchedCards;
        _cardController.OnCardsGenereted += SetCards;
    }
    private void SetCards()
    {
        var cards = _cardController.CardRepository.GetCards();

        if (cards.Count != _cardObjects.Count)
        {
            Debug.LogError($"Card数({cards.Count})とObject数({_cardObjects.Count})が一致していません");
            return;
        }

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
            _cardRotates.Add(cardRotate);
        }

        Debug.Log("カードがセットされました");
    }

    private void ClearCards()
    {
        _cardController.CardRepository.GetCards().ForEach(card =>
         {

             _cardRotates.ForEach(cardRotate =>
             {
                 card.OnCardOpened -= cardRotate.OpenCard;
             });
         });
        _textToObject.Clear();
    }
    private void HideMatchedCards(Card a, Card b)
    {
        if (_textToObject.TryGetValue(a.GetCardBackText(), out var objA))
        {
            var rotate = objA.GetComponentInChildren<CardRotate>(true);
            if (rotate != null)
            {
                rotate.StopCard();
            }
            objA.SetActive(false);
        }
        if (_textToObject.TryGetValue(b.GetCardBackText(), out var objB))
        {
            var rotate = objB.GetComponentInChildren<CardRotate>(true);
            if (rotate != null)
            {
                rotate.StopCard();
            }
            objB.SetActive(false);
        }
    }
}
