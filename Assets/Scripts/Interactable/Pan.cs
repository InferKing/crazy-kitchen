using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Dishes
{
    public override void Drop()
    {
        transform.DORotate(IgnoreYZRotation, 0.1f);
        Rb.isKinematic = false;
    }
    public override void Combine(Interactable interactable)
    {
        throw new System.NotImplementedException();
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Ingredient)
        {
            AddIngredient((Ingredient)interactable);
            PlaceIngredient((Ingredient)interactable);
            // GameObject new_mesh = GetGameObject();
            // ServiceLocator.Instance.Get<EventBus>().Invoke(new DestroyMeDaddySignal(interactable.gameObject));
            return true;
        }
        return false;
    }
}
