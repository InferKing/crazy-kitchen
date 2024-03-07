using UnityEngine;
using UnityHFSM;
public class KitchenNoteStateIdle : StateBase
{
    private Interactable _item;
    public KitchenNoteStateIdle(Interactable item) : base(needsExitTime: false, isGhostState: false)
    {
        _item = item;
    }
    public override void OnEnter()
    {
        Debug.Log($"KitchenNote {_item} spawned");
    }
}