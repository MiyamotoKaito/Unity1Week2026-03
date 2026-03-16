using System.Collections.Generic;
using UnityEngine;

public class CardPresenter : MonoBehaviour
{
   public void AddCard(Card card,CardView cardView)
   {
       _cards.Add(card);
       _cardViews.Add(cardView);
   }

   private List<Card> _cards = new List<Card>();
   private List<CardView> _cardViews = new List<CardView>();
}
