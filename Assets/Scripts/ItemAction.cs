using System.Data.SqlTypes;
using UnityEngine;

public class ItemAction : MonoBehaviour, IInitializable
{
    private IInteractable _interactable;
    private EventBus _bus;
    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<FindInteractableSignal>(OnFindInteractable);
        _bus.Subscribe<InputDownSignal>(OnInputDown);
        _bus.Subscribe<NoInteractableSignal>(OnNoInteractable);
    }
    private void OnFindInteractable(FindInteractableSignal signal)
    {
        if (_interactable != signal.data) 
        { 
            _interactable?.OnExit();
            _interactable = signal.data;
            _interactable.OnEnter();
        }
    }
    private void OnNoInteractable(NoInteractableSignal signal)
    {
        _interactable?.OnExit();
        _interactable = null;
    }
    private void OnInputDown(InputDownSignal signal)
    {
        if (_interactable == null) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            _interactable.Interact();
        }
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<FindInteractableSignal>(OnFindInteractable);
        _bus.Unsubscribe<InputDownSignal>(OnInputDown);
        _bus.Unsubscribe<NoInteractableSignal>(OnNoInteractable);
    }
}
