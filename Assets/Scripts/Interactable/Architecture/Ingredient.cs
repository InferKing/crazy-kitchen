using UnityEngine;

public class Ingredient : Grabbable
{
    public override void OnEnter()
    {
        base.OnEnter();
        ServiceLocator.Instance.Get<EventBus>().Invoke(new ShowItemTextSignal(Constants.keyPressEItem));
    }

    public override void OnExit()
    {
        base.OnExit();
        ServiceLocator.Instance.Get<EventBus>().Invoke(new ShowItemTextSignal(string.Empty));
    }
    public override bool TryCombine(Interactable item, out bool stayInHand)
    {
        stayInHand = false;
        if (item == null) return false;
        if (item is RubbishBin)
        {
            Bus.Invoke(new GetItemOutOfHandSignal(this));
            Bus.Invoke(new PlayFXSignal(item.TransformToFX, FXType.PuffSmoke));
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
