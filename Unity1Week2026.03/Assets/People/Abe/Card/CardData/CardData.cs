using UnityEngine;
[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    public int CardId => _cardId;
    public Sprite FrontSprite => _frontSprite;

    public ICardEffect CardEffect => _cardEffect;
    public CardTriggerMode TriggerMode => _triggerMode;

    [SerializeField] private int _cardId;
    [SerializeField] private Sprite _frontSprite;
    [SerializeReference, SubclassSelector] private ICardEffect _cardEffect;
    [SerializeField] private CardTriggerMode _triggerMode = CardTriggerMode.Pair;
    //TODO: カードの効果を渡す方法。
}

public enum CardTriggerMode
{
    Pair = 0,
    Single = 1
}
