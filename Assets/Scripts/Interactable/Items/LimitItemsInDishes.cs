using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class LimitItemsInDishes
{
    public ItemType type;
    public int maxCountPerPlace;
    public List<GameObject> wherePlace = new();

    public LimitItemsInDishes(ItemType type, int maxCountPerPlace)
    {
        this.type = type;
        this.maxCountPerPlace = maxCountPerPlace;
    }

}
