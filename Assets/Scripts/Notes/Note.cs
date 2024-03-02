using System;
using UnityEngine;

public abstract class Note : MonoBehaviour, IInitializable, IInteractable
{
    private IInteractable _interactable;

    public virtual void Initialize()
    {
        _interactable = new NoteRegularBehaviour();
    }
    public virtual void Interact()
    {
        _interactable.Interact();
    }

    public virtual void OnEnter()
    {
        Debug.Log("������� ������");
    }

    public virtual void OnExit()
    {
        Debug.Log("������ ������");
    }

    public void SetInteractable(IInteractable interactable)
    {
        _interactable = interactable ?? throw new NullReferenceException(nameof(interactable));
    }
}
