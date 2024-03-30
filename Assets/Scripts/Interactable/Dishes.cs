using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dishes : Interactable
{
    [SerializeField] private GameObject _placeToIngredient;
    private List<Ingredient> _ingredients = new();
    public IReadOnlyList<Ingredient> Ingredients { get { return _ingredients; } }
    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }
    public override void Drop()
    {
        transform.DORotate(IgnoreYRotation, 0.1f);
        Rb.isKinematic = false;
    }

    public override void Interact()
    {
        Rb.isKinematic = true;
        Bus.Invoke(new ItemInteractedSignal(this));
    }
    public virtual void PlaceIngredient(Ingredient placeToIngredient)
    {
        placeToIngredient.transform.SetParent(transform);
        placeToIngredient.transform.localPosition = _placeToIngredient.transform.localPosition;
        placeToIngredient.Rb.isKinematic = true;
        placeToIngredient.GetComponent<Collider>().enabled = false;
        placeToIngredient.transform.rotation = Quaternion.Euler(placeToIngredient.InitRotation);
    }
}
