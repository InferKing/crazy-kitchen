using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ingredient : Interactable
{
    [SerializeField] private string _key = "Ìÿסמ";
    [SerializeField, Range(0, 1000)] private float _value = 100;
    private bool _isActive = false;
    private Rigidbody _rb;
    private EventBus _bus;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _bus = ServiceLocator.Instance.Get<EventBus>();
    }
    public override void Interact()
    {
        if (!_isActive)
        {
            _bus.Invoke(new ItemInteractedSignal(this));
            _bus.Invoke(new LockInteractSignal(true));
        }
        _isActive = !_isActive;
        _rb.isKinematic = _isActive;
    }

    public override void OnEnter()
    {
        EventBus bus = ServiceLocator.Instance.Get<EventBus>();
        bus.Invoke(new ShowItemTextSignal(Constants.keyPressEItem));
    }

    public override void OnExit()
    {
        EventBus bus = ServiceLocator.Instance.Get<EventBus>();
        bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
}
