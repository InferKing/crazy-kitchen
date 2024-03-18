using UnityHFSM;
public class KitchenNoteStateWaiting : StateBase
{
    private Interactable _item;
    public KitchenNoteStateWaiting(Interactable item) : base(needsExitTime: false, isGhostState: false)
    {
        _item = item;
    }
    public override void OnLogic()
    {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new KitchenNoteGetSignal(_item));
    }
}