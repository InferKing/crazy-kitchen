using UnityEngine;

public class ItemAction : MonoBehaviour, IInitializable
{
    private Interactable _interactable, _activeInteractable;
    // _interactable - на кого навелись
    // _activeInteractable - что в руках
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
        if (_activeInteractable != null && _interactable != null && Input.GetKeyDown(KeyCode.E))
        {
            if (_interactable.TryCombine(_activeInteractable, out bool stayInHand))
            {
                if (!stayInHand)
                {
                    _activeInteractable = null;
                }
            }
        }
        else if (_interactable != null && Input.GetKeyDown(KeyCode.E) && _activeInteractable == null)
        {
            _interactable.Interact();
            _activeInteractable = _interactable;
        }
        else if (_activeInteractable != null)
        {
            switch (_activeInteractable)
            {
                case Meat:
                    Debug.Log("Meat");
                    break;
                case SlicedMeat: 
                    Debug.Log("SlicedMeat");
                    break;
                case ChoppedMeat:
                    Debug.Log("ChoppedMeat");
                    break;
            }
            // Я хотел сделать красиво, но пока не понял как эту хуйню сделать нормально
            // Поэтому ловите ебучий switch вместо "гениального" делегата
            //Interactable act = _activeInteractable;
            //foreach (var item in Input.inputString)
            //{
            //    if (KeyboardConstants.KeyCodeMatch.TryGetValue(item, out KeyCode value))
            //    {
            //        if (act.ActionKeys.TryGetValue(value, out System.Action action))
            //        {
            //            action();
            //        }

            //    }
            //}
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
