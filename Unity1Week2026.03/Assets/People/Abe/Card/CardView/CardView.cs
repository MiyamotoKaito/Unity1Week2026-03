using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardView : MonoBehaviour
{

    public void SetCard(Card card)
    {
        
        _image.sprite = card.GetCardFrontSprite();
        _text.text = card.GetCardBackText();
    }

    private Image _image;
    private TextMeshProUGUI _text;
    private const string CardBackImagePath = "CardBackImage";
    private const string CardBackTextPath = "CardBackImage/CardBackText";
    private void Awake()
    {
        _image = transform.Find(CardBackImagePath).GetComponent<Image>();
        _text = transform.Find(CardBackTextPath).GetComponent<TextMeshProUGUI>();
    }
}
