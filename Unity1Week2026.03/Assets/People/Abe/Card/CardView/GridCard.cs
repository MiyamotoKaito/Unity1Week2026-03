using UnityEngine;

public class GridCard : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private int _width = 7;
    [SerializeField] private float _margin = 0.2f;

    void Update()
    {
        int count = _parent.childCount;

        if (count == 0) return;

        // 子1つ目からサイズ取得
        var first = _parent.GetChild(0);
        var rect = first.GetComponent<RectTransform>();

        float itemWidth = rect.rect.width * first.localScale.x;
        float itemHeight = rect.rect.height * first.localScale.y;

        float spacingX = itemWidth + _margin;
        float spacingY = itemHeight + _margin;

        int height = Mathf.CeilToInt((float)count / _width);

        float offsetX = (_width - 1) * spacingX * 0.5f;
        float offsetY = (height - 1) * spacingY * 0.5f;

        for (int i = 0; i < count; i++)
        {
            int x = i % _width;
            int y = i / _width;

            var child = _parent.GetChild(i);

            Vector3 pos = new Vector3(
                x * spacingX - offsetX,
                -y * spacingY + offsetY,
                0
            );

            child.localPosition = pos;
        }
    }
}