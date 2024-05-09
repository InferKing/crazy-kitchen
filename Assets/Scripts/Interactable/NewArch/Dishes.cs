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
        _actionKeys.Add(KeyCode.T, () => DropIngredient());
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Bus.Invoke(new ShowDishesUISignal(this));
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEDishes));
    }
    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new HideDishesUISignal(this));
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
        if (ServiceLocator.Instance.Get<CraftController>().TryCraftItem(ref _ingredients, out GameObject newItem))
        {
            var obj = Instantiate(newItem);
            var ingred = obj.GetComponent<Ingredient>();
            _ingredients.Add(ingred);
            PlaceIngredient(ingred);
        }
        else
        {
            PlaceIngredient(ingredient);
        }
        
    }
    public virtual void PlaceIngredient(Ingredient item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = _placeToIngredient.transform.localPosition;
        item.Rb = item.GetComponent<Rigidbody>();
        item.Rb.isKinematic = true;
        item.GetComponent<Collider>().enabled = false;
        item.transform.localRotation = Quaternion.Euler(0, 0, item.transform.rotation.eulerAngles.z);
    }
    public virtual void DropIngredient()
    {
        if (_ingredients.Count > 0)
        {
            Ingredient value = _ingredients[_ingredients.Count - 1];
            value.transform.parent = null;
            value.Rb.isKinematic = false;
            value.GetComponent<Collider>().enabled = true;
            _ingredients.RemoveAt(_ingredients.Count - 1);
        }
    }
    public virtual void DropAllIngredients()
    {
        _ingredients.ForEach(ingredient => 
        {
            ingredient.transform.parent = null;
            ingredient.Rb.isKinematic = false;
            ingredient.GetComponent<Collider>().enabled = true;
        });
        _ingredients.Clear();
    }
}
