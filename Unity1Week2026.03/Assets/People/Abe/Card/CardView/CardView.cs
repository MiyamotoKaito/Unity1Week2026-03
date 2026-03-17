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

    private void Awake()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>(true);
    }
}
