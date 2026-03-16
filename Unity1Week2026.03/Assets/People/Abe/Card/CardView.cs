using UnityEngine;
using TMPro;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class CardView : MonoBehaviour
{

    public void SetCard(Card card)
    {
        _spriteRenderer.sprite = card.GetCardFrontSprite();
        _textMeshProUGUI.text = card.GetCardBackText().GetText();
    }
    private SpriteRenderer _spriteRenderer;
    private TextMeshProUGUI _textMeshProUGUI;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
}
