using System.Collections.Generic;
using UnityEngine;


public class PlaceIngredientData
{
    public List<Ingredient> ingredients;
    public int maxAmount;
    public ItemType type;
    public Quaternion rotation;
    public PlaceIngredientData(List<Ingredient> ingredients, int maxAmount, ItemType type, Quaternion rotation)
    {
        this.ingredients = ingredients;
        this.maxAmount = maxAmount;
        this.type = type;
        this.rotation = rotation;
    }
    public bool IsBusy() => ingredients.Count == maxAmount;
    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }
    public void RemoveIngredient(Ingredient ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            ingredients.Remove(ingredient);
        }
    }
}