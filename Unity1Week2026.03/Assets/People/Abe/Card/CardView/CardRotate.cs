using UnityEngine;
using System.Collections;

public class CardRotate : MonoBehaviour
{
    public event System.Action OnFlipCompleted;
    public bool IsFlipping => _flipRoutine != null;

    public void OpenCard()
    {
        if(!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"カードがアクチE��ブではありません: {gameObject.name}");
            return;
        }
        FlipTo(true);
    }

    public void CloseCard()
    {
        if(!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"カードがアクチE��ブではありません: {gameObject.name}");
            return;
        }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipTo(!_isBackActive);
        }
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
        float duration = _flipDuration / 180;

        _flipAngle = 0;
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(duration);
            transform.Rotate(_x, _y, _z);
            _flipAngle++;
            if (_flipAngle == 90 || _flipAngle == -90)
            {
                ToggleFace();
            }
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
}
