using UnityEngine;

public class CardBackText
{
    public CardBackText(string text)
    {
        if(text == null)
        {
            Debug.LogError("カードの裏面テキストはnullにできません");
            _text = string.Empty;
            return;
        }
        _text = text;
    }

    public string GetText()
    {
        return _text;
    }
    private string _text;
}
