using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftItem
{
    [SerializeField] private GameObject _prefabIngredient;
    [field: SerializeField, Min(1)] public int Amount { get; private set; }
    public Ingredient Ingredient { 
        get
        {
            return _prefabIngredient.GetComponent<Ingredient>();
        }
    }
}
