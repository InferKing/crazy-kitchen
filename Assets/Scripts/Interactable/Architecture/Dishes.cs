using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dishes : Grabbable
{
    [OnValueChanged("OnAllIngredients")]
    public bool allIngredients;
    [SerializeField] private List<LimitItemsInDishes> _limitItems = new();
    private List<Ingredient> _ingredients = new();
    private Dictionary<GameObject, PlaceIngredientData> _placesBusy = new();
    public IReadOnlyList<Ingredient> Ingredients => _ingredients;
    public IReadOnlyList<LimitItemsInDishes> Limits => _limitItems;
    
    // called when allIngredients toggled
    private void OnAllIngredients()
    {
        _limitItems.Clear();
        if (allIngredients)
        {
            _limitItems.Add(new LimitItemsInDishes(ItemType.AllIngredients, 0, Quaternion.identity));
        }
        else
        {
            _limitItems.Add(new LimitItemsInDishes(ItemType.None, 0, Quaternion.identity));
        }
    }

    protected override void Start()
    {
        base.Start();
        _limitItems.ForEach(limit =>
        {
            limit.wherePlace.ForEach(gObj =>
            {
                _placesBusy.Add(gObj, new PlaceIngredientData(new List<Ingredient>(), limit.maxCountPerPlace, limit.type, limit.rotation));
            });
        });
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
        KeyValuePair<GameObject, PlaceIngredientData> newPosGO;
        if (allIngredients)
        {
            newPosGO = _placesBusy.FirstOrDefault(i => i.Value.type == ItemType.AllIngredients && !i.Value.IsBusy());
        }
        else
        {
            newPosGO = _placesBusy.FirstOrDefault(i => i.Value.type == item.ItemType && !i.Value.IsBusy());
        }
        newPosGO.Value.AddIngredient(item);
        item.transform.localPosition = newPosGO.Key.transform.localPosition;
        item.Rb = item.GetComponent<Rigidbody>();
        item.Rb.isKinematic = true;
        item.GetComponent<Collider>().enabled = false;
        item.transform.localRotation = newPosGO.Value.rotation; 
    }
    public virtual void DropIngredient()
    {
        if (_ingredients.Count > 0)
        {
            Ingredient value = _ingredients[^1];
            PrepareToDrop(value);
            _ingredients.RemoveAt(_ingredients.Count - 1);
        }
    }
    public Ingredient GetIngredientInFire()
    {
        return Ingredients.FirstOrDefault(item => item is Cookable cookable && cookable.IsInFire);
    }
    public virtual void DropAllIngredients()
    {
        _ingredients.ForEach(ingredient => 
        {
            PrepareToDrop(ingredient);
        });
        _ingredients.Clear();
    }
    private void PrepareToDrop(Ingredient item)
    {
        item.transform.parent = null;
        item.transform.localScale = item.InitialWorldScale;
        item.Rb.isKinematic = false;
        item.GetComponent<Collider>().enabled = true;
        if (!allIngredients)
        {
            var result = _placesBusy.FirstOrDefault(kv => kv.Value.type == item.ItemType && !kv.Value.IsEmpty() && kv.Value.ingredients.Contains(item));
            result.Value.RemoveIngredient(item);
        }
        else
        {
            var result = _placesBusy.FirstOrDefault(kv => !kv.Value.IsEmpty());
            result.Value.RemoveIngredient(item);
        }
    }
    private bool HasEmptyPlace(Ingredient cookable)
    {
        return _placesBusy.Any(item => item.Value.type == cookable.ItemType && !item.Value.IsBusy());
    }
    private bool HasAnotherItems(Ingredient cookable)
    {
        return _placesBusy.Any(item => item.Value.type != cookable.ItemType && item.Value.IsBusy());
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Ingredient ingredient)
        {
            if (!HasEmptyPlace(ingredient))
            {
                // notify player that dish don't contain empty place 
                return false;
            }
            if (HasAnotherItems(ingredient) && !allIngredients)
            {
                // notify player that dish contains another items
                return false;
            }
            AddIngredient(ingredient);
            stayInHand = true;
            return true;
        }
        return false;
    }
}
