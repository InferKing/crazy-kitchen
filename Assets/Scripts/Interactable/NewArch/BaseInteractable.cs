using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Interact();
}
