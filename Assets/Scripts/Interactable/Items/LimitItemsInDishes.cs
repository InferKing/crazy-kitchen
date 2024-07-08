using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class LimitItemsInDishes
{
    public ItemType type;
    public int maxCount;
    public List<GameObject> wherePlace = new();

    public LimitItemsInDishes(ItemType type, int maxCount)
    {
        this.type = type;
        this.maxCount = maxCount;
    }
}
