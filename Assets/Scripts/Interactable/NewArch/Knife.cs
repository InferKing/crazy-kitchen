using DG.Tweening;
using UnityEngine;

public class Knife : Grabbable
{
    protected override void Start()
    {
        base.Start();
        _actionKeys.Add(KeyCode.E, () => { }); // empty because hint should be displayed
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Sliceable item)
        {
            item.ToSlice();
            stayInHand = true;
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
