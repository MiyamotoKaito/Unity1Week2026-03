using UnityEngine;

public class CardFrontSprite 
{
    public  CardFrontSprite(Sprite sprite)
    {
        _sprite = sprite;
    }
    public Sprite GetSprite()
    {
        return _sprite;
    }

    private Sprite _sprite;
}
