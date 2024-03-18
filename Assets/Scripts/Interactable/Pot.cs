using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Interactable
{
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
            GameObject new_mesh = GetGameObject();
            ServiceLocator.Instance.Get<EventBus>().Invoke(new DestroyMeDaddySignal(interactable));
            return true;
        }
        return false;
    }
    public override void Drop()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
