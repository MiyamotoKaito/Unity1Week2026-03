using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GridCard : MonoBehaviour
{
    public void GridCards()
    {
        if (_gridRoutine != null)
        {
            StopCoroutine(_gridRoutine);
        }
        _gridRoutine = StartCoroutine(GridCardsNextFrame());
    }
    [SerializeField] private CardController _cardController;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _width = 7;
    [SerializeField] private float _xSpace;
    [SerializeField] private float _ySpace;
    [Header("Spawn Drop Effect")]
    [SerializeField] private bool _useSpawnDropEffect = true;
    [SerializeField] private float _spawnDropSeconds = 0.4f;
    [SerializeField] private float _spawnDropHeight = 80f;
    [SerializeField] private float _spawnFadeSeconds = 0.25f;
    [SerializeField] private float _spawnStaggerSeconds = 0.03f;
    private Coroutine _gridRoutine;
    private Coroutine _spawnRoutine;

    private void OnEnable()
    {
        _cardController.OnCardsGenereted += GridCards;
    }
    private void OnDisable()
    {
        _cardController.OnCardsGenereted -= GridCards;
        if (_gridRoutine != null)
        {
            StopCoroutine(_gridRoutine);
            _gridRoutine = null;
        }
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }
    private IEnumerator GridCardsNextFrame()
    {
        yield return new WaitForEndOfFrame();
        RandomGrid();
        if (_useSpawnDropEffect)
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
            }
            _spawnRoutine = StartCoroutine(PlaySpawnDropEffect());
        }
        _gridRoutine = null;
    }
    /// <summary>
    /// �J�[�h���O���b�h��Ƀ����_���z�u���� shuffle + grid layout
    /// </summary>
    public void RandomGrid()
    {
        int count = _parent.childCount;
        if (count == 0) return;

        // �q�����X�g�ɃR�s�[
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < count; i++)
        {
            children.Add(_parent.GetChild(i));
        }

        // �V���b�t��
        for (int i = count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = children[i];
            children[i] = children[j];
            children[j] = temp;
        }

        // �T�C�Y�擾
        var rect = children[0].GetComponent<RectTransform>();
        float itemWidth = rect.rect.width * children[0].localScale.x;
        float itemHeight = rect.rect.height * children[0].localScale.y;

        float spacingX = itemWidth + _xSpace;
        float spacingY = itemHeight + _ySpace;

        int height = Mathf.CeilToInt((float)count / _width);

        float offsetX = (_width - 1) * spacingX * 0.5f;
        float offsetY = (height - 1) * spacingY * 0.5f;

        // �V���b�t�����ꂽ���Ŕz�u
        for (int i = 0; i < count; i++)
        {
            int x = i % _width;
            int y = i / _width;

            var child = children[i];

            Vector3 pos = new Vector3(
                x * spacingX - offsetX,
                -y * spacingY + offsetY,
                0
            );

            child.localPosition = pos;
        }
    }

    private IEnumerator PlaySpawnDropEffect()
    {
        if (_parent == null || _parent.childCount == 0)
        {
            yield break;
        }

        int count = _parent.childCount;
        var items = new List<Transform>(count);
        var targets = new Vector3[count];
        var groups = new CanvasGroup[count];

        for (int i = 0; i < count; i++)
        {
            var child = _parent.GetChild(i);
            items.Add(child);
            targets[i] = child.localPosition;
            var group = child.GetComponent<CanvasGroup>();
            if (group == null)
            {
                group = child.gameObject.AddComponent<CanvasGroup>();
            }
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;
            groups[i] = group;

            child.localPosition = targets[i] + Vector3.up * _spawnDropHeight;
        }

        float moveDuration = Mathf.Max(0.01f, _spawnDropSeconds);
        float fadeDuration = Mathf.Max(0.01f, _spawnFadeSeconds);
        float stepDelay = Mathf.Max(0f, _spawnStaggerSeconds);
        float maxDelay = stepDelay * Mathf.Max(0, items.Count - 1);
        float total = Mathf.Max(moveDuration, fadeDuration) + maxDelay;

        float elapsed = 0f;

        while (elapsed < total)
        {
            elapsed += Time.deltaTime;

            for (int i = 0; i < items.Count; i++)
            {
                var tr = items[i];
                if (tr == null)
                {
                    continue;
                }
                float delay = stepDelay * i;
                float moveT = Mathf.Clamp01((elapsed - delay) / moveDuration);
                float fadeT = Mathf.Clamp01((elapsed - delay) / fadeDuration);
                tr.localPosition = Vector3.Lerp(
                    targets[i] + Vector3.up * _spawnDropHeight,
                    targets[i],
                    moveT);
                if (groups[i] != null)
                {
                    groups[i].alpha = fadeT;
                }
            }
            yield return null;
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                items[i].localPosition = targets[i];
            }
            if (groups[i] != null)
            {
                groups[i].alpha = 1f;
            }
        }
    }
}
