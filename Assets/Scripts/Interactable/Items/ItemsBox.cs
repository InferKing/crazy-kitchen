using System.Collections.Generic;
using UnityEngine;

public class ItemsBox : Dishes
{
    [SerializeField] private List<Ingredient> _itemsToPlace;
    protected override void Start()
    {
        base.Start();
        _itemsToPlace.ForEach(item =>
        {
            if (!TryCombine(item, out _))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Item {item.ObjectName} can't be placed in box");
#endif
            }
        });
    }
}
