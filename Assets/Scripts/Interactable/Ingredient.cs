using DG.Tweening;
using UnityEngine;

public class Ingredient : Interactable
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

    public override void Combine(Interactable interactable)
    {
        // wtf is not released
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Knife)
        {
            stayInHand = true;
            GameObject new_mesh = GetGameObject();
            return true;
        }
        return false;
    }

    public override void Drop()
    {
        transform.DORotate(IgnoreYRotation, 0.1f);
        Rb.isKinematic = false;
    }
}
