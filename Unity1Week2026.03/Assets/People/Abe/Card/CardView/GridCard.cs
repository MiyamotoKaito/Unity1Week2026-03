using UnityEngine;
using System.Collections.Generic;

public class GridCard : MonoBehaviour
{
    public void GridCards()
    {
        RandomGrid();
    }
    [SerializeField] private Transform _parent;
    [SerializeField] private int _width = 7;
    [SerializeField] private float _margin;

    private void RandomGrid()
    {
        int count = _parent.childCount;
        if (count == 0) return;

        // 子をリストにコピー
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < count; i++)
        {
            children.Add(_parent.GetChild(i));
        }

        // シャッフル
        for (int i = count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = children[i];
            children[i] = children[j];
            children[j] = temp;
        }

        // サイズ取得
        var rect = children[0].GetComponent<RectTransform>();
        float itemWidth = rect.rect.width * children[0].localScale.x;
        float itemHeight = rect.rect.height * children[0].localScale.y;

        float spacingX = itemWidth + _margin;
        float spacingY = itemHeight + _margin;

        int height = Mathf.CeilToInt((float)count / _width);

        float offsetX = (_width - 1) * spacingX * 0.5f;
        float offsetY = (height - 1) * spacingY * 0.5f;

        // シャッフルされた順で配置
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
}