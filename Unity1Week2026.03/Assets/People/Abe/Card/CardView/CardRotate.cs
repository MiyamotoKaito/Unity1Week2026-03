using UnityEngine;
using System.Collections;

public class CardRotate : MonoBehaviour
{
    public event System.Action OnFlipCompleted;
    public bool IsFlipping => _flipRoutine != null;
    public bool IsBackActive => _isBackActive;

    public void OpenCard()
    {
        if(!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"カードがアクチE��ブではありません: {gameObject.name}");
            return;
        }
        AudioManager.Instance.PlaySE("CardFlip");
        FlipTo(true);
    }

    public void CloseCard()
    {
        if(!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"カードがアクチE��ブではありません: {gameObject.name}");
            return;
        }
        AudioManager.Instance.PlaySE("CardFlip");
        FlipTo(false);
    }


    [SerializeField]
    private GameObject _backCard;
    [SerializeField, Tooltip("回転方向を持たせるための軸")]
    private float _x, _y, _z;
    [SerializeField, Tooltip("反転にかかる時間（秒）")]
    private float _flipDuration = 0;
    private bool _isBackActive = false;
    private int _flipAngle = 0;
    private Coroutine _flipRoutine;
    private bool _hasPendingFlip = false;
    private bool _pendingBackActive = false;

    private void OnEnable()
    {
        SyncBackState();
    }

    private void OnDisable()
    {
        if (_flipRoutine != null)
        {
            StopCoroutine(_flipRoutine);
            _flipRoutine = null;
        }
        _hasPendingFlip = false;
        _pendingBackActive = false;
        OnFlipCompleted?.Invoke();
    }

    private void ToggleFace()
    {
        if (!_isBackActive)
        {
            _isBackActive = true;
            _backCard.SetActive(true);
        }
        else
        {
            _isBackActive = false;
            _backCard.SetActive(false);

        }
    }
    private IEnumerator ToggleCard()
    {
        float elapsedTime = 0f;
        bool faceSwitched = false;

        while (elapsedTime < _flipDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / _flipDuration;
            
            // 180度回転させる
            float rotationAmount = 180f * (Time.deltaTime / _flipDuration);
            transform.Rotate(_x * rotationAmount, _y * rotationAmount, _z * rotationAmount);
            
            // 50%の地点で表裏を切り替え
            if (!faceSwitched && progress >= 0.5f)
            {
                ToggleFace();
                faceSwitched = true;
            }
            
            yield return null;
        }

        _flipAngle = 0;
        _flipRoutine = null;
        OnFlipCompleted?.Invoke();
        if (_hasPendingFlip && _pendingBackActive != _isBackActive)
        {
            _hasPendingFlip = false;
            _flipRoutine = StartCoroutine(ToggleCard());
        }
        else
        {
            _hasPendingFlip = false;
        }
    }

    private void FlipTo(bool backActive)
    {
        SyncBackState();
        if (_flipRoutine != null)
        {
            _hasPendingFlip = true;
            _pendingBackActive = backActive;
            return;
        }

        if (_isBackActive == backActive)
        {
            return;
        }

        _hasPendingFlip = false;
        _flipRoutine = StartCoroutine(ToggleCard());
    }

    private void SyncBackState()
    {
        if (_backCard == null)
        {
            return;
        }
        _isBackActive = _backCard.activeSelf;
    }
}
