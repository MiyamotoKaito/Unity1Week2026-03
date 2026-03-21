using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //シーン移行時に出る画像
    [SerializeField] private Image _blackOutImage;
    //シーン移行するステージの名前
    [SerializeField] private Text _stageName;
    public void SceneFadeIn()
    {
        _blackOutImage.enabled = false;
    }
    public void SceneFadeOut()
    {

    }
}
