using UnityEngine.UI;
using UnityEngine;

public class ClairvoyanceView : MonoBehaviour
{
   [SerializeField] private Image _clairvoyanceImage;
   [SerializeField] private GameObject _clairvoyanceObject;
   private bool _isClairvoyanceActive = false;

    public bool IsClairvoyanceActive => _isClairvoyanceActive;
    public void ShowClairvoyance()
    {
        _isClairvoyanceActive = true;
        _clairvoyanceObject.SetActive(true);
        _clairvoyanceImage.color = new Color(1, 1, 1, 0.5f);
        // 先見の明の効果をここに実装
        Debug.Log("先見の明の効果が発動しました。");
    }
    public void HideClairvoyance()
    {
        _clairvoyanceImage.color = new Color(1, 1, 1, 1f);
        _isClairvoyanceActive = false;
        _clairvoyanceObject.SetActive(false);
    }
    public void SetSprite(Sprite sprite)
    {
        
        var image = _clairvoyanceObject.GetComponent<Image>();
        image.sprite = sprite;
    }

}
