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


    #region メソッド
    private void OnEnable()
    {
        _inputField.onSubmit.AddListener(OnSubmit);
        _inputField.onDeselect.AddListener(OnDeselect);
    }
    private void OnDisable()
    {
        _inputField.onSubmit.RemoveListener(OnSubmit);
        _inputField.onDeselect.RemoveListener(OnDeselect);
    }
    private void Start()
    {
        ActiveInput();
        ClearTextField();
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
    /// <summary>
    /// Enterが押されたときに呼ぶ
    /// </summary>
    /// <param name="text"></param>
    private void OnSubmit(string text)
    {
        ClearTextField();
        ActiveInput();
    }
    /// <summary>
    /// フォーカスが外れた時の処理
    /// </summary>
    /// <param name="text"></param>
    private void OnDeselect(string text)
    {
        ActiveInput();
    }
    #endregion
}
