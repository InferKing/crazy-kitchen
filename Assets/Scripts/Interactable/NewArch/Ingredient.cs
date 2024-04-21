using UnityEngine;
using System.Collections.Generic;

public class Ingredient : Grabbable
{
    public override void Interact()
    {
        Rb.isKinematic = true;
        Bus.Invoke(new ItemInteractedSignal(this));
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEItem));
    }

    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }

    //public override bool TryCombine(Interactable interactable, out bool stayInHand)
    //{
    //    stayInHand = false;
    //    if (interactable == null) return false;
    //    if (interactable is Dishes)
    //    {
    //        Dishes dish = interactable as Dishes;
    //        dish.AddIngredient(this);
    //        dish.PlaceIngredient(this);
    //        stayInHand = true;
    //        return true;
    //    }
    //    if (interactable is Knife)
    //    {
    //        stayInHand = true;
    //        //GameObject new_mesh = GetGameObject();
    //        return true;
    //    }
    //    return false;
    //}
}
