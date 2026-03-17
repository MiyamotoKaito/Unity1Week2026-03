using UnityEngine;

public class GridCard : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private int width = 7;
    [SerializeField] private float margin = 0.2f;

    void Update()
    {
        int count = parent.childCount;

        if (count == 0) return;

        // 子1つ目からサイズ取得
        var first = parent.GetChild(0);
        var rect = first.GetComponent<RectTransform>();

        float itemWidth = rect.rect.width * first.localScale.x;
        float itemHeight = rect.rect.height * first.localScale.y;

        float spacingX = itemWidth + margin;
        float spacingY = itemHeight + margin;

        int height = Mathf.CeilToInt((float)count / width);

        float offsetX = (width - 1) * spacingX * 0.5f;
        float offsetY = (height - 1) * spacingY * 0.5f;

        for (int i = 0; i < count; i++)
        {
            int x = i % width;
            int y = i / width;

            var child = parent.GetChild(i);

            Vector3 pos = new Vector3(
                x * spacingX - offsetX,
                -y * spacingY + offsetY,
                0
            );

            child.localPosition = pos;
        }
    }
}