using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Объект должен реагировать на события покупки и очистки корзины
public class ShopCart
{
    private Dictionary<BuyableItem, int> _itemsToBuy;
    private EventBus _bus;
    public ShopCart(EventBus bus)
    {
        _bus = bus;
        _itemsToBuy = new Dictionary<BuyableItem, int>();
    }
    public ShopCart(EventBus bus, Dictionary<BuyableItem, int> itemsToBuy)
    {
        _bus = bus;
        _itemsToBuy = itemsToBuy;
    }
    public IReadOnlyDictionary<BuyableItem, int> BuyableItems => _itemsToBuy;
    public void Add(BuyableItem item)
    {
        if (_itemsToBuy.ContainsKey(item))
        {
            _itemsToBuy[item] += 1;
        }
        else
        {
            _itemsToBuy.Add(item, 1);
        }
    }
    public void Remove(BuyableItem item)
    {
        if (_itemsToBuy.ContainsKey(item)) 
        { 
            _itemsToBuy[item]--;
            if (_itemsToBuy[item] == 0)
            {
                _itemsToBuy.Remove(item);
            }
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogWarning($"Item {item} not in shop cart");
        }
#endif
    }
    public void Clear()
    {
        _itemsToBuy.Clear();
    }
}
