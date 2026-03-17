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

    public void ClearCard()
    {
        _image.sprite = null;
        _text.text = string.Empty;
        Debug.Log("カードがクリアされました");
        gameObject.SetActive(false);
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
