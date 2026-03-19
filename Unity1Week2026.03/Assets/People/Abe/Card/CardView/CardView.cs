using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class CardView : MonoBehaviour
{

    public void SetCard(Card card)
    {
        
        _image.sprite = card?.GetCardFrontSprite();
        _text.text = card.GetCardBackText();
    }
    public void ReverseText()
    {
        _text.text = new string(_text.text.Reverse().ToArray());
    }

    public void SetFont(TMP_FontAsset fontAsset)
    {
        _text.font = fontAsset;
    }
    private Image _image;
    private TextMeshProUGUI _text;
    private const string CardBackImagePath = "Text/BackGround/BackImage";
    private const string CardBackTextPath = "Text";
    private void Awake()
    {
        _image = transform.Find(CardBackImagePath).GetComponent<Image>();
        _text = transform.Find(CardBackTextPath).GetComponent<TextMeshProUGUI>();
    }
}
