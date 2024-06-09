using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _placeToSpawn;
    public bool TryBuy()
    {
        GameObject item = Instantiate(_prefab);
        item.transform.position = _placeToSpawn.transform.position;
        return true;
    }
}
