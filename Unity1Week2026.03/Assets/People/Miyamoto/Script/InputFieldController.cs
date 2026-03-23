using TMPro;
using UnityEngine;
using WebGLSupport;

/// <summary>
/// InputFieldを制御するクラス
/// </summary>
public class InputFieldController : MonoBehaviour
{
    #region 変数
    [SerializeField]
    private TMP_InputField _inputField;
    private WebGLInput _webGLInput;
    #endregion


    #region メソッド
    private void OnEnable()
    {
        _inputField.onSubmit.AddListener(OnSubmit);
        _inputField.onDeselect.AddListener(OnDeselect);
        _inputField.onSelect.AddListener(OnSelect);
    }
    private void OnDisable()
    {
        _inputField.onSubmit.RemoveListener(OnSubmit);
        _inputField.onDeselect.RemoveListener(OnDeselect);
        _inputField.onSelect.RemoveListener(OnSelect);
    }
    private void Start()
    {
        _webGLInput = GetComponent<WebGLInput>();
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
    /// 入力を受け付けられるようにする
    /// </summary>
    public void EnableInputField()
    {
        _inputField.interactable = true;
    }
    /// <summary>
    /// 入力を受け付けないようにする
    /// </summary>
    public void DisableInputField()
    {
        _inputField.interactable = false;
    }
    /// <summary>
    /// Enterが押されたときに呼ぶ
    /// </summary>
    /// <param name="text"></param>
    private void OnSubmit(string text)
    {
        AudioManager.Instance.PlaySE("InputSubmit");
        ClearTextField();
        _webGLInput?.SyncText(0);
        Invoke(nameof(ActiveInput), 0.1f);
    }
    /// <summary>
    /// インプットフィールドのフォーカスが外れた時に呼ぶ
    /// </summary>
    /// <param name="text"></param>
    private void OnDeselect(string text)
    {
        ActiveInput();
    }
    /// <summary>
    /// テキストボックスが選択されているときに呼び出す
    /// </summary>
    /// <param name="text"></param>
    private void OnSelect(string text)
    {
        Input.imeCompositionMode = IMECompositionMode.On;
    }
    #endregion
}
