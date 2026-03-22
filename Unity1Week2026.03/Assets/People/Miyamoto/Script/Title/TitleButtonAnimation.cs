using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtonAnimation : MonoBehaviour
{
    [Header("ボタンの参照")]
    [SerializeField] private Button _startButton; 
    [SerializeField] private List<Button>  _stageButtons;

    [Header("アニメーション設定")]
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private float _outButtonPosition;
    
    private List<float> _defaultPositions = new List<float>();
    private void Start()
    {
        foreach (var button in _stageButtons)
        {
            button.interactable = false;
            
            _defaultPositions.Add(button.transform.position.x);
            
            button.transform.position = new  Vector3(_outButtonPosition,
                                                    button.transform.position.y,
                                                    button.transform.position.z);
        }
        ButtonAnimation().Forget();
    }

    /// <summary>
    /// ボタンのアニメーション
    /// </summary>
    private async UniTask ButtonAnimation()
    {
        await WaitForButtonClick();

        await _startButton.transform.DOMoveX(_outButtonPosition, _animationDuration).AsyncWaitForCompletion();
        
        await StageButtonAnimation();
        
        _startButton.interactable = false;
        foreach (var button in _stageButtons)
        {
            button.interactable = true;
        }
    }

    /// <summary>
    /// ステージボタンのアニメーション
    /// </summary>
    private async UniTask StageButtonAnimation()
    {
        for (int i = 0; i < _stageButtons.Count; i++)
        {
           await _stageButtons[i].transform.DOMoveX(_defaultPositions[i],  _animationDuration).AsyncWaitForCompletion();
        }
    }
    
    /// <summary>
    /// Startボタンが押されるのを待つ
    /// </summary>
    private async UniTask WaitForButtonClick()
    {
        //Awaitを自分で決めるためのもの
        var tcs = new UniTaskCompletionSource();

        void ButtonClick()
        {
            //タスクを完了させる
            tcs.TrySetResult();
        }

        _startButton.onClick.AddListener(ButtonClick);

        //InputFieldでEnterが押されるまで待つ
        await tcs.Task;

        _startButton.onClick.RemoveListener(ButtonClick);
    }
}