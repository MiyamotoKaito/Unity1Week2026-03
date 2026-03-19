
using System.Collections.Generic;
using UnityEngine;

public class CardViewSpawn : MonoBehaviour
{
    public List<GameObject> GenerateCard(int count)
    {
        var cards = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var card = Instantiate(_cardPrefab, _cardParent);
            cards.Add(card);
        }
        Debug.Log($"カードを{count}枚生成しました。");
        return cards;
    }
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardParent;


}
