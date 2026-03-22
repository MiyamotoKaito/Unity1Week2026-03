using UnityEngine;

public class CardInit : MonoBehaviour
{
    [SerializeField] private CardController _cardController;
    [SerializeField] private CardPresenter _cardPresenter;

    private void Awake()
    {
        _cardController.Init();
        _cardPresenter.StartEvent();

    }
    private void Start()
    {
        _cardController.SpawnCards();
    }
}
