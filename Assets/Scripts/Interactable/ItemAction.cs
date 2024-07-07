using UnityEngine;

public class ItemAction : MonoBehaviour, IInitializable
{
    private Interactable _interactable, _activeInteractable;
    // _interactable - на кого навелись
    // _activeInteractable - что в руках
    private EventBus _bus;
    private bool _lockedInteract = false;
    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<FindInteractableSignal>(OnFindInteractable);
        _bus.Subscribe<InputDownSignal>(OnInputDown);
        _bus.Subscribe<NoInteractableSignal>(OnNoInteractable);
        _bus.Subscribe<ItemDroppedSignal>(OnItemDropped);
        _bus.Subscribe<ToggleInteractSignal>(OnToggleInteract);
    }
    private void OnToggleInteract(ToggleInteractSignal signal)
    {
        _lockedInteract = signal.data;
    }
    private void OnFindInteractable(FindInteractableSignal signal)
    {
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
        if (_interactable != null)
        {
            _interactable.OnExit();
        }
        _interactable = null;
    }
    private void OnInputDown(InputDownSignal signal)
    {
        if (_lockedInteract) return;
        if (_activeInteractable != null && _interactable != null && Input.GetKeyDown(KeyCode.E))
        {
            if (_activeInteractable.TryCombine(_interactable, out bool stayInHand))
            {
                if (!stayInHand)
                {
                    _activeInteractable = null;
                }
            }
            else if (_interactable is not Grabbable)
            {
                _interactable.Interact();
            }
        }
        else if (_interactable != null && _interactable is Grabbable && Input.GetKeyDown(KeyCode.E) && _activeInteractable == null)
        {
            _interactable.Interact();
            _activeInteractable = _interactable;
            _bus.Invoke(new ShowPossibleInputSignal(_activeInteractable.ActionKeys));
            _bus.Invoke(new GetItemInHandSignal(_activeInteractable));
        }
        else if (_activeInteractable != null)
        {
            // Ќикакого switch, господа
            // Let's celebrate and suck some dicks! Cheers!
            Interactable act = _activeInteractable;
            foreach (var item in Input.inputString)
            {
                if (KeyboardConstants.KeyCodeMatch.TryGetValue(item, out KeyCode value))
                {
                    if (act.ActionKeys.TryGetValue(value, out System.Action action))
                    {
                        action();
                    }

                }
            }
        }
        else if (_interactable != null && Input.GetKeyDown(KeyCode.E))
        {
            _interactable.Interact();
        }
    }
    private void OnItemDropped(ItemDroppedSignal signal)
    {
        _bus.Invoke(new GetItemOutOfHandSignal(_activeInteractable));
        _activeInteractable = null;
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<FindInteractableSignal>(OnFindInteractable);
        _bus.Unsubscribe<InputDownSignal>(OnInputDown);
        _bus.Unsubscribe<NoInteractableSignal>(OnNoInteractable);
        _bus.Unsubscribe<ItemDroppedSignal>(OnItemDropped);
        _bus.Unsubscribe<ToggleInteractSignal>(OnToggleInteract);
    }
}
