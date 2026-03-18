using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardView : MonoBehaviour
{

    public void SetCard(Card card)
    {
        
        _image.sprite = card?.GetCardFrontSprite();
        _text.text = card.GetCardBackText();
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
