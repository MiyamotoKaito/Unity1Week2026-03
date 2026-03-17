using UnityEngine;
using System.Collections;

public class CardRotate : MonoBehaviour
{
    [SerializeField]
    private GameObject _backCard;
    [SerializeField]
    private float _x, _y, _z;
    [SerializeField]
    private float _flipDuration = 0;
    private bool _isBackActive = false;
    private int _flipAngle = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CalculateFlip());
        }
    }
    private void Flip()
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

    private IEnumerator CalculateFlip()
    {
        _flipDuration /= 180;
        
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(_flipDuration);
            transform.Rotate(_x, _y, _z);
            _flipAngle++;
            if (_flipAngle == 90 || _flipAngle == -90)
            {
                Flip();
            }
        }

        _flipAngle = 0;
    }

}
