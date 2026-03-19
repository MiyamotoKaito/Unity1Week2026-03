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
    public void Start()
    {
        ClearTextField();
        ActiveInput();
    }
    #endregion


    #region メソッド
    private void Start()
    {
        ClearTextField();
    }
    private void Update()
    {
        ActiveInput();
    }
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
    public void ActiveInput()
    {
        _inputField.ActivateInputField();
    }
    #endregion
}
