using TMPro;
using Unity1Week.URA.typing;
using UnityEngine;

public class TypingSystem : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TMP_InputField _InputField;
    private InputFieldController _InputFieldController;

    #endregion

    #region　メソッド
    private void Start()
    {
        LoadText.AssemblyWords(LoadText.GetCSVFile());
        _InputFieldController = new(_InputField);
        GetText();
    }


    private void GetText()
    {
        var strings = LoadText.WordList[Random.Range(0, LoadText.WordList.Count)];
        _text.text = strings;
    }
    #endregion
}
