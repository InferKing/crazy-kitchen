using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dishes : Grabbable
{
    [SerializeField] private GameObject _placeToIngredient;
    private List<Ingredient> _ingredients = new();
    public IReadOnlyList<Ingredient> Ingredients { get { return _ingredients; } }
    protected override void Start()
    {
        base.Start();
        _actionKeys.Add(KeyCode.F, () => DropAllIngredients());
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEDishes));
    }
    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }
    public virtual void PlaceIngredient(Ingredient item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = _placeToIngredient.transform.localPosition;
        item.Rb.isKinematic = true;
        //item.GetComponent<Collider>().enabled = false;
        item.transform.rotation = Quaternion.Euler(item.InitRotation);
    }
    public void DropAllIngredients()
    {
        _ingredients.ForEach(ingredient => 
        {
            ingredient.transform.parent = null;
            ingredient.Rb.isKinematic = true;
        });
    }
}
