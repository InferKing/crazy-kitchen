using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftController : SignalReceiver, IInitializable, IService
{
    [SerializeField] private List<CraftableItemData> _craftableList;
    public void Initialize()
    {
        ServiceLocator.Instance.Register(this);
    }
    public bool TryCraftItem(ref List<Ingredient> ingredients, out GameObject craftableItem)
    {
        craftableItem = null;
        foreach (var itemData in _craftableList)
        {
            List<string> dishIngredients = ingredients.Select(item => item.ObjectName).ToList();
            int countDelete = 0;
            List<string> craftIngredients = new();
            foreach (var item in itemData.Elements)
            {
                for (int i = 0; i < item.Amount; i++)
                {
                    craftIngredients.Add(item.Ingredient.ObjectName);
                }
            }
            foreach (var item in craftIngredients)
            {
                if (dishIngredients.Remove(item))
                {
                    countDelete++;
                }
            }
            if (countDelete == craftIngredients.Count)
            {
                foreach (var item in craftIngredients)
                {
                    Ingredient toRemove = ingredients.Find(it => it.ObjectName == item);
                    ingredients.Remove(toRemove);
                    Destroy(toRemove.gameObject);
                }
                craftableItem = itemData.Prefab;
                return true;
            }
        }
        return false;
    }
}
