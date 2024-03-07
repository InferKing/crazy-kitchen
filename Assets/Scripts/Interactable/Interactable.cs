using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Interact();
}
