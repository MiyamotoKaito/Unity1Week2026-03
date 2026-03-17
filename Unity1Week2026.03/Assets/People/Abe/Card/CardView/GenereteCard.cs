using Unity.VisualScripting;
using UnityEngine;

public class GenerateCardView : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardParent;
    [SerializeField] private CardController _cardController;

    private void OnEnable()
    {
        GenerateCard();
    }
    private void GenerateCard()
    {
        for (int i = 0; i < _cardController.MaxCardPairs * 2; i++)
        {
           Instantiate(_cardPrefab, _cardParent);
        }
    }
}
