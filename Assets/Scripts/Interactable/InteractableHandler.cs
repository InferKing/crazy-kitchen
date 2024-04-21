using DG.Tweening;
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
        _bus.Subscribe<PlaceToInteractMovedSignal>(OnPlaceToInteractMoved);
    }
    private void OnItemInteracted(ItemInteractedSignal signal)
    {
        if (signal.data is Grabbable item)
        {
            item.transform.SetParent(_placeToInteractItem.transform, true);
            item.transform.position = _placeToInteractItem.transform.position;
            _item = item.transform;
            Quaternion q = new()
            {
                eulerAngles = item.PlaceRoatation
            };
            signal.data.transform.localRotation = q;
            q.eulerAngles = Vector3.zero;
        }
    }
    private void OnItemDropped(ItemDroppedSignal signal)
    {
        if (_item == null) return;
        _item.parent = null;
    }
    private void OnPlaceToInteractMoved(PlaceToInteractMovedSignal signal)
    {
        
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<ItemDroppedSignal>(OnItemDropped);
        _bus.Unsubscribe<PlaceToInteractMovedSignal>(OnPlaceToInteractMoved);
        _bus.Unsubscribe<ItemInteractedSignal>(OnItemInteracted);
    }
}
