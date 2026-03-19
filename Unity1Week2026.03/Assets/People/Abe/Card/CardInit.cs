using UnityEngine;

public class CardInit : MonoBehaviour
{
    [SerializeField] private CardController _cardController;

    private void Awake()
    {
        _cardController.Init();
    }
    private void Start()
    {
        _cardController.SpawnCards();
    }
}
