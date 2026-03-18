
using TMPro;
using UnityEngine;

public class CardOpenUI : MonoBehaviour
{
    [SerializeField] private CardController _cardController;
    [SerializeField] private TMP_InputField _inputField;

    private void OnEnable()
    {
        _inputField.onSubmit.AddListener(OpenCard);
    }
    private void OnDisable()
    {
        _inputField.onSubmit.RemoveListener(OpenCard);
    }
    private void OpenCard(string cardText)
    {
        var card = _cardController.CardRepository.FindCardByText(cardText);
        if (card != null)
        {
            card.OpenCard();
        }
        else
        {
            Debug.LogWarning($"No card found with text: {cardText}");
        }
    }

    
}
