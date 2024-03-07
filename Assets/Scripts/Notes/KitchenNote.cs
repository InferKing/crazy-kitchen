using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenNote : Interactable
{
    public override void Interact()
    {

    }

    public override void OnEnter()
    {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new ShowItemTextSignal(""));
    }

    public override void OnExit()
    {
        Debug.Log("Exit");

    }
}
