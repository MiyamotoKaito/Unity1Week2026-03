using TMPro;
using UnityEngine;

/// <summary>
/// InputFieldを制御するクラス
/// </summary>
public class InputFieldController : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private TMP_InputField _inputField;
    #endregion

    #region コンストラクタ
    public InputFieldController(TMP_InputField inputField)
    {
        _inputField = inputField;

        ClearTextField();
        ActioveInput();
    }
    #endregion

    #region メソッド
    /// <summary>
    /// InputFieldに書かれている文字を全て削除する
    /// </summary>
    public void ClearTextField()
    {
        _inputField.text = string.Empty;
    }
    /// <summary>
    /// InputFieldに文字を打てるようにする
    /// </summary>
    public void ActioveInput()
    {
        _inputField.ActivateInputField();
    }
    #endregion
}
