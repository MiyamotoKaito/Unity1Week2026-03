using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトルの文字入力のセットアップを管理するクラス
/// </summary>
public class SetUpSystem : MonoBehaviour
{
    [Header("パネルの参照")] 
    [SerializeField] private Image _fadePanel;
    [SerializeField] private Image _setUpPanel;

    [Header("アニメーション設定")]
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private float _fadeInDuration;

    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = FindAnyObjectByType<TMP_InputField>();

        _setUpPanel.gameObject.SetActive(true);
        _fadePanel.gameObject.SetActive(true);
        
        SetUpAnimation().Forget();
    }

    /// <summary>
    /// タイトルの入場からInputFieldのEnterを押すまでのアニメーション
    /// </summary>
    private async UniTask SetUpAnimation()
    {
        await _fadePanel.DOFade(0f, _fadeOutDuration).AsyncWaitForCompletion();

        await WaitForSubmit();

        //フェードイン→フェードアウト→タイトルシーンに移動
        await _fadePanel.DOFade(1f, _fadeInDuration).AsyncWaitForCompletion();
        _setUpPanel.gameObject.SetActive(false);
        
        await _fadePanel.DOFade(0f, _fadeOutDuration).AsyncWaitForCompletion();
        _fadePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Enterが入力されるまでまつ
    /// </summary>
    private async UniTask WaitForSubmit()
    {
        //Awaitを自分で決めるためのもの
        var tcs = new UniTaskCompletionSource();


        void Submit(string task)
        {
            //タスクを完了させる
            tcs.TrySetResult();
        }

        _inputField.onSubmit.AddListener(Submit);

        //InputFieldでEnterが押されるまで待つ
        await tcs.Task;

        _inputField.onSubmit.RemoveListener(Submit);
    }
}