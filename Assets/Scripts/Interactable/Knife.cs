using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Interactable
{
    private EventBus _bus;
    private void Start()
    {
        Rb = GameObjectsToChange[0].GetComponent<Rigidbody>();
        _bus = ServiceLocator.Instance.Get<EventBus>();
    }
    public override void Combine(Interactable interactable)
    {
        throw new System.NotImplementedException();
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Meat || interactable is SlicedMeat)
        {
            GameObject new_mesh = interactable.GetGameObject();
            //ServiceLocator.Instance.Get<EventBus>().Invoke(new DestroyMeDaddySignal(interactable));
            return true;
        }
        return false;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _bus.Invoke(new ShowItemTextSignal(Constants.keyPressEItem));
    }

    public override void OnExit()
    {
        base.OnExit();
        _bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
    public override void Drop()
    {
        transform.DORotate(IgnoreYRotation, 0.1f);
        Rb.isKinematic = false;
    }

    public override void Interact()
    {
        Rb.isKinematic = true;
        _bus.Invoke(new ItemInteractedSignal(this));
    }
}
