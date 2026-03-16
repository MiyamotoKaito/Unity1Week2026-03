using Unity.VectorGraphics;
using UnityEngine;
/// <summary>
/// カードを管理するクラス
/// </summary>
public class Card 
{
   public Card(int cardId, CardEffect cardEffect, Sprite frontSprite, string cardBackText)
   {
       _cardId = new CardId(cardId);
       _cardEffect = cardEffect;
       _cardFrontSprite = new CardFrontSprite(frontSprite);
       _cardBackText = new CardBackText(cardBackText);
   }
　　
　　public void OpenCard()
   {
       // カードを開く処理
   }
   public void CloseCard()
   {
       // カードを閉じる処理
   }

   public bool MatchCard(Card card)
   {
      if (GetCardId() == card.GetCardId())
      {
          return true;
      }

      return false;
   }
   public CardId GetCardId()
   {
       return _cardId;
   }

   public Sprite GetCardFrontSprite()
   {
       return _cardFrontSprite.GetSprite();
   }

    public CardBackText GetCardBackText()
    {
         return _cardBackText;
    }

   public void ExcuteEffect()
   {
       _cardEffect.Exucute();
   }

   private CardId _cardId;
   private CardEffect _cardEffect;
   private CardFrontSprite _cardFrontSprite;

   private CardBackText _cardBackText;
}
