using UnityEngine;

public class ItemAction : MonoBehaviour, IInitializable
{
    private bool _canInteract = true;
    private Interactable _interactable, _activeInteractable;
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
        if (Input.GetKeyDown(KeyCode.E) && _canInteract && _interactable != null)
        {
            if (!_interactable.CanGet)
            {
                _interactable.Interact();
            }
            else if (_activeInteractable == null)
            {
                _interactable.Interact();
                _activeInteractable = _interactable;
            }
            else if(_interactable.TryCombine(_activeInteractable, out bool stayInHand))
            {
                if (!stayInHand)
                {
                    _activeInteractable = null;
                }
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.G) && _activeInteractable != null)
        {
            _bus.Invoke(new ItemDroppedSignal());
            _activeInteractable.Drop();
            _activeInteractable = null;

            // при наведении на ингредиент и при этом нет ничего в руках - используется interact
            // при наведении на ингредиент и при этом есть что-то в руках - используется combine наведенного предмета
            // при нажатии G активный предмет просто бросается
            // активным предметом считается тот, что можно взять в руки, а не просто взаимодействовать
        }
    }
    private void OnLockInteract(LockInteractSignal signal)
    {
        //_canInteract = !signal.data;
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
