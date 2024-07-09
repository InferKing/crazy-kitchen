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
    
    public IReadOnlyList<Ingredient> Ingredients { get { return _ingredients; } }
    
    private void OnAllIngredients()
    {
        _limitItems.Clear();
        if (allIngredients)
        {
            _limitItems.Add(new LimitItemsInDishes(ItemType.AllIngredients, 0));
        }
        else
        {
            _limitItems.Add(new LimitItemsInDishes(ItemType.None, 0));
        }
    }

    protected override void Start()
    {
        base.Start();
        _limitItems.ForEach(limit =>
        {
            limit.wherePlace.ForEach(gObj =>
            {
                _placesBusy.Add(gObj, new PlaceIngredientData(new List<Ingredient>(), limit.maxCountPerPlace, limit.type));
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
        var newPosGO = _placesBusy.FirstOrDefault(i => i.Value.type == item.ItemType);
        if (newPosGO.Value != null)
        {
            newPosGO.Value.AddIngredient(item);
            item.transform.localPosition = newPosGO.Key.transform.localPosition;
        }



        //if (newPosGO.Value != null)
        //{
        //    _placesBusy[newPosGO.Key].busy = true;
        //    _placesBusy[newPosGO.Key].ingredients.Add(item);
        //    item.transform.localPosition = newPosGO.Key.transform.localPosition;
        //}
        //else
        //{
        //    //var randomIndex = Random.Range(0, _placeToIngredient.Count);
        //    //_placesBusy[_placeToIngredient[randomIndex]].ingredients.Add(item);
        //    //_placesBusy[_placeToIngredient[randomIndex]].busy = true;
        //    //item.transform.localPosition = _placeToIngredient[randomIndex].transform.localPosition;
        //}
        item.Rb = item.GetComponent<Rigidbody>();
        item.Rb.isKinematic = true;
        item.GetComponent<Collider>().enabled = false;
        item.transform.localRotation = Quaternion.Euler(0, 0, item.transform.rotation.eulerAngles.z);
    }
    // TODO: элементы не выкидываются, но запоминаются
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
        item.Rb.isKinematic = false;
        item.GetComponent<Collider>().enabled = true;
    }
    private bool HasEmptyPlace(Cookable cookable)
    {
        // need to check if the record exists
        var item = _placesBusy.FirstOrDefault(item => item.Value.type == cookable.ItemType);
        if (item.Value != null)
        {
            return !item.Value.IsBusy();
        }
        return false;
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Cookable cookable)
        {
            // need to check to see if there's space in the dish before adding
            if (!HasEmptyPlace(cookable))
            {
                // notify player that dish don't contain empty place 
                return false;
            }
            AddIngredient(cookable);
            stayInHand = true;
            return true;
        }
        return false;
    }
}
