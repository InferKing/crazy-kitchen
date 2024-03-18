using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Interact();
    public abstract void Drop();
    public abstract void Combine(Interactable interactable);
    public abstract bool TryCombine(Interactable interactable, out bool stayInHand);
}
