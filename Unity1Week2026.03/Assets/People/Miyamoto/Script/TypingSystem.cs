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

    private LoadText _loadText;
    #endregion

    #region　メソッド
    private void Start()
    {
        _loadText = new(GetCSVFile());
        _InputFieldController = new(_InputField);
        GetText();
    }
    /// <summary>
    /// ResourecesフォルダからCSVファイルを取得する
    /// </summary>
    /// <returns></returns>
    private TextAsset GetCSVFile()
    {
        var csv = Resources.Load<TextAsset>("WordList");

        if (csv == null)
        {
            Debug.LogError("CSVファイルが見つからない");
        }

        return csv;
    }

    private void GetText()
    {
        var strings = _loadText.WordList[Random.Range(0, _loadText.WordList.Count)];
        _text.text = strings;
    }
    #endregion
}
