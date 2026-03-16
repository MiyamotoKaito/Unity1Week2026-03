using UnityEngine;

public class CardBackText
{
    public CardBackText(string text)
    {
        _text = text;
    }

    public string GetText()
    {
        return _text;
    }
    private string _text;
}
