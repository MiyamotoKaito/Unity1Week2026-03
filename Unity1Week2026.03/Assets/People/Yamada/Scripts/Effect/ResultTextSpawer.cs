using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
///     文字を1文字ずつ生成するクラス。
/// </summary>
public class ResultTextSpawer : MonoBehaviour
{
    /// <summary>
    ///     文字を生成する
    /// </summary>
    public List<TextMeshProUGUI> Spawn(string text)
    {
        Clear();

        float offset = 0f;

        foreach (char c in text)
        {
            TextMeshProUGUI charObj = Instantiate(_charPrefab, _parent);
            charObj.text = c.ToString();
            charObj.ForceMeshUpdate();

            RectTransform rect = charObj.rectTransform;
            rect.anchoredPosition = new Vector2(offset, 0f);

            offset += charObj.preferredWidth;

            _chars.Add(charObj);
        }

        return _chars;
    }

    public void Clear()
    {
        foreach (var c in _chars)
        {
            if (c != null) Destroy(c.gameObject);
        }

        _chars.Clear();
    }

    [SerializeField] private TextMeshProUGUI _charPrefab;
    [SerializeField] private Transform _parent;

    private List<TextMeshProUGUI> _chars = new();
}
