using System.Collections.Generic;

public class PlaceIngredientData
{
    public List<Ingredient> ingredients;
    public int maxAmount;
    public ItemType type;
    public PlaceIngredientData(List<Ingredient> ingredients, int maxAmount, ItemType type)
    {
        this.ingredients = ingredients;
        this.maxAmount = maxAmount;
        this.type = type;
    }
    public bool IsBusy() => ingredients.Count == maxAmount;
    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }
}