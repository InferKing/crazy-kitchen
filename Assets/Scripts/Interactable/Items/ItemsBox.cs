using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsBox : Dishes
{
    [Tooltip("NOTE: The _placeToIngredient field does not need to be changed when using this field. I HAVEN'T HIDDEN IT YET, I'LL DO IT LATER ")]
    [SerializeField] private List<ItemsDishesMatch> _authorizedItems = new();
}
