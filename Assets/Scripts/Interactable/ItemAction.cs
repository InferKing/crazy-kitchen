using UnityEngine;

public class ItemAction : MonoBehaviour, IInitializable
{
    private bool _canInteract = true;
    private Interactable _interactable;
    private EventBus _bus;
    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<FindInteractableSignal>(OnFindInteractable);
        _bus.Subscribe<InputDownSignal>(OnInputDown);
        _bus.Subscribe<NoInteractableSignal>(OnNoInteractable);
        _bus.Subscribe<LockInteractSignal>(OnLockInteract);
    }
    private void OnFindInteractable(FindInteractableSignal signal)
    {
        if (!_canInteract) return;
        if (_interactable != signal.data) 
        { 
            if (_interactable != null)
            {
                _interactable.OnExit();
            }
            _interactable = signal.data;
            _interactable.OnEnter();
        }
    }
    private void OnNoInteractable(NoInteractableSignal signal)
    {
        if (!_canInteract) return;
        if (_interactable != null)
        {
            _interactable.OnExit();
        }
        _interactable = null;
    }
    private void OnInputDown(InputDownSignal signal)
    {
        if (_interactable == null) return;
        if (Input.GetKeyDown(KeyCode.E) && _canInteract)
        {
            _interactable.Interact();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            _bus.Invoke(new ItemDroppedSignal());
            _interactable.Interact();
            _bus.Invoke(new LockInteractSignal(false));
        }
    }
    private void OnLockInteract(LockInteractSignal signal)
    {
        _canInteract = !signal.data;
        if (!_canInteract)
        {
            _bus.Invoke(new ShowItemTextSignal("Нажмите G чтобы бросить"));
        }
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<FindInteractableSignal>(OnFindInteractable);
        _bus.Unsubscribe<InputDownSignal>(OnInputDown);
        _bus.Unsubscribe<NoInteractableSignal>(OnNoInteractable);
        _bus.Unsubscribe<LockInteractSignal>(OnLockInteract);

    }
}
