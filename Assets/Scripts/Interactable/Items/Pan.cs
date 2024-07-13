using DG.Tweening;
using UnityEngine;

public class Pan : Dishes
{
    protected override void Start()
    {
        base.Start();
        _actionKeys.Add(KeyCode.E, () => { });
    }
    private bool HasEmptyPlace()
    {
        return Ingredients.Count != Limits[0].maxCountPerPlace;
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Cookable cookable && HasEmptyPlace())
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
