using UnityEngine;

public class NoteWaitingBehaviour : IInteractable
{
    public void Interact()
    {
        Debug.Log("��� ��� �����");
    }

    public void OnEnter()
    {
        // ServiceLocator.Instance.Get<EventBus>().Invoke();
    }

    public void OnExit()
    {
        
    }
}
