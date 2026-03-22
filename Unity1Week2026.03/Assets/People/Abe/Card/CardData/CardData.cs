using UnityEngine;
[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    public int CardId => _cardId;
    public Sprite FrontSprite => _frontSprite;

    public ICardEffect CardEffect => _cardEffect;
    public CardTriggerMode TriggerMode => _triggerMode;
    public Sprite PowerSprite => _powerSprite;

    public ICardEffect CreateEffectInstance()
    {
        return CloneEffect(_cardEffect);
    }

    [SerializeField] private int _cardId;
    [SerializeField] private Sprite _frontSprite;
    [SerializeReference, SubclassSelector] private ICardEffect _cardEffect;
    [SerializeField] private CardTriggerMode _triggerMode = CardTriggerMode.Pair;
    [SerializeField] private Sprite _powerSprite;

    private static ICardEffect CloneEffect(ICardEffect effect)
    {
        if (effect == null)
        {
            return null;
        }
        var json = JsonUtility.ToJson(effect);
        return JsonUtility.FromJson(json, effect.GetType()) as ICardEffect;
    }
}

public enum CardTriggerMode
{
    Pair = 0,
    Single = 1
}
