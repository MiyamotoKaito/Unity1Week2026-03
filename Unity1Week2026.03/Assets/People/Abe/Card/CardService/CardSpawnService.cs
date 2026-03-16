using UnityEngine;
/// <summary>
/// 同じカードが複数枚存在しないようにするためのクラス
/// </summary>
public class CardSpawnSystem 
{
    private CardRepository _cardRepository;
    private GameObject _cardParentObject;

    public CardSpawnSystem(CardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
 
    public void SpawnCard(Card card)
    {
        if (!_cardRepository.CardExists(card.GetCardId(), card.GetCardBackText()))
        {
            _cardRepository.AddCard(card);
            //TODO カードの裏面のテキストをタイピングシステムから受け取る。
            var cardObject = Object.Instantiate(new GameObject("Card"), _cardParentObject.transform);
            var cardView = cardObject.AddComponent<CardView>();
            cardView.SetCard(card);
            //TODO カードの位置を決める。
            //TODO カードを２枚生成する。

            //カードとカードViewを紐づける。
        }

        
    }
}
