using UnityEngine;

public class InteractableHandler : MonoBehaviour
{
    [SerializeField] private GameObject _placeToInteractItem;
    private EventBus _bus;
    private Transform _item;
    private void Start()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<ItemInteractedSignal>(OnItemInteracted);
        _bus.Subscribe<ItemDroppedSignal>(OnItemDropped);
    }
    private void OnItemInteracted(ItemInteractedSignal signal)
    {
        signal.data.transform.SetParent(_placeToInteractItem.transform, true);
        _item = signal.data.transform;
    }
    private void OnItemDropped(ItemDroppedSignal signal)
    {
        _item.parent = null;
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<ItemDroppedSignal>(OnItemDropped);
        _bus.Unsubscribe<ItemInteractedSignal>(OnItemInteracted);
    }
}
