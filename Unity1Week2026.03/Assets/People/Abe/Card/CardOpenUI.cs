using UnityEngine;

public class CardOpenUI : MonoBehaviour
{
    [SerializeField] private CardController _cardController;
    
    public void OpenCard(string cardText)
    {
        var card = _cardController.CardRepository.FindCardByText(cardText);
        if (card != null)
        {
            _cardController.CardRepository.OpenCard(card);
        }
        else
        {
            Debug.LogWarning($"No card found with text: {cardText}");
        }
    }

    
}
