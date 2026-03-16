using Unity.VectorGraphics;
using UnityEngine;
/// <summary>
/// カードを管理するクラス
/// </summary>
public class Card 
{
   public Card(CardId cardId, CardEffect cardEffect, CardFrontSprite cardFrontSprite, CardBackText cardBackText)
   {
       _cardId = cardId;
       _cardEffect = cardEffect;
       _cardFrontSprite = cardFrontSprite;
       _cardBackText = cardBackText;
   }

   public int GetCardId()
   {
       return _cardId.GetId();
   }

   public Sprite GetCardFrontSprite()
   {
       return _cardFrontSprite.GetSprite();
   }

    public string GetCardBackText()
    {
         return _cardBackText.GetText();
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
