using DG.Tweening;
using UnityEngine;

public class Knife : Interactable
{
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is ChoppedMeat)
        {
            return false;
        }
        if (interactable is Sliceable item)
        {
            item.Slice();
            return true;
        }
        return false;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEKnife));
    }

    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
}
