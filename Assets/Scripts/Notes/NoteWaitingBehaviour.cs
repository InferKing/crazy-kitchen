using UnityEngine;

public class NoteWaitingBehaviour : IInteractable
{
    public void Interact()
    {
        Debug.Log("Хуй без масла");
    }

    public void OnEnter()
    {
        // ServiceLocator.Instance.Get<EventBus>().Invoke();
    }

    public void OnExit()
    {
        
    }
}
