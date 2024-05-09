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
        List<string> dishIngredients = ingredients.Select(item => item.ObjectName).ToList();
        foreach (var itemData in _craftableList)
        {
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

//        var d_l = new List<Ingredient>();
//        foreach (var itemData in _craftableList)
//        {
//            if (itemData.Elements.Count > ingredients.Count) continue;
//            Dictionary<Ingredient, int> countDict = new();
//            foreach (var item in ingredients)
//            {
//                if (countDict.ContainsKey(item))
//                {
//                    countDict[item]++;
//                }
//                else
//                {
//                    countDict[item] = 1;
//                }
//            }
//            foreach (var item in itemData.Elements)
//            {
//                if (countDict.ContainsKey(item.Ingredient))
//                {
//                    countDict[item.Ingredient] -= item.Amount;
//                    if (countDict[item.Ingredient] == 0)
//                    {
//                        countDict.Remove(item.Ingredient);
//                        d_l.Add(item.Ingredient);
//                    }
//                    else if (countDict[item.Ingredient] < 0)
//                    {
//                        continue;
//                    }
//                }
//            }
//            try
//            {
//                List<Ingredient> differenceList = countDict.SelectMany(kv => Enumerable.Repeat(kv.Key, kv.Value)).ToList();
//                ingredients = ingredients.Except(differenceList).ToList();
//                // необходимо удалить из сцены скомбинированные объекты
//                craftableItem = itemData.Prefab;
//                return true;
//            }
//            catch (ArgumentOutOfRangeException)
//            {
//#if UNITY_EDITOR
//                Debug.LogWarning($"Can't craft item {itemData}");
//#endif
//            }
        //}
        //var result = ingredients
        //    .GroupBy(item => item.ObjectName)
        //    .Select(item => new { Name = item.Key, Count = item.Count() });
        //foreach ( var item in result )
        //{
        //    var res = _craftableList.Find(it => it.Element.Ingredient.ObjectName == item.Name && it.Element.Amount <= item.Count);
        //    if (res != null)
        //    {
        //        Dictionary<string, int> match = new();
        //        foreach (var r in result)
        //        {
        //            match[r.Name] = r.Count;
        //        }
        //        foreach (var r in ingredients)
        //        {
        //            if (match.ContainsKey(r.ObjectName) && match[r.ObjectName] > 0)
        //            {
        //                match[r.ObjectName] -= 1;
        //                Destroy(r);
        //            }
        //        }
        //        ingredients = ingredients.Where(i => i != null).ToList();
        //        craftableItem = res.Prefab;
        //        return true;
        //    }
        //}
    }
}
