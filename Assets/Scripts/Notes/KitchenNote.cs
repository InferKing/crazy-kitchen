public class KitchenNote : Note
{
    public override void Initialize()
    {
        SetInteractable(new NoteRegularBehaviour());
    }
    public override void Interact()
    {
        SetInteractable(new NoteWaitingBehaviour());
        base.Interact();
    }
}
