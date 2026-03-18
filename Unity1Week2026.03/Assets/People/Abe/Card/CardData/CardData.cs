using UnityEngine;
[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    public int CardId => _cardId;
    public Sprite FrontSprite => _frontSprite;

    public ICardEffect CardEffect => _cardEffect;

    [SerializeField] private int _cardId;
    [SerializeField] private Sprite _frontSprite;
    [SerializeReference, SubclassSelector] private ICardEffect _cardEffect;
    //TODO: カードの効果を渡す方法。
}
