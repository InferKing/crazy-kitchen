using DG.Tweening;
using UnityEngine;

public class Pan : Dishes
{
    protected override void Start()
    {
        base.Start();
        _actionKeys.Add(KeyCode.E, () => { });
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Cookable cookable)
        {
            AddIngredient(cookable);
            stayInHand = true;
            return true;
        }
        else if (interactable is PlaceToCook place)
        {
            if (!place.IsEmpty) return false;
            place.ToStove(this);
            return true;
        }
        return false;
    }
    public override void Drop()
    {
        transform.DORotate(IgnoreYZRotation, 0.1f);
        Rb.isKinematic = false;
        Rb.AddForce(Camera.main.transform.forward * 2, ForceMode.Impulse);
        Bus.Invoke(new ItemDroppedSignal());
    }
}
