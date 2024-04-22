using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Dishes
{
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Ingredient ingredient)
        {
            AddIngredient(ingredient);
            PlaceIngredient(ingredient);
            stayInHand = true;
            return true;
        }
        return false;
    }
}
