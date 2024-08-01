using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCardSpawner : MonoBehaviour
{
    [SerializeField] private List<BuyableItem> _items;
    [SerializeField] private GameObject _cardUIPrefab, _parentToSpawn;
    private void Start()
    {
        _items.ForEach(item =>
        {
            GameObject go = Instantiate(_cardUIPrefab);
            go.transform.parent = _parentToSpawn.transform;
            var card = go.GetComponent<ShopCard>();
            card.Item = item;
            card.UpdateUI();
        });
    }
}
