using UnityEngine;

public class PlaceToInteract : MonoBehaviour
{
    [SerializeField] private GameObject _placeToInteract;
    private Vector3 _lastPos;
    private EventBus _bus;
    private void Start()
    {
        _lastPos = _placeToInteract.transform.position;
        _bus = ServiceLocator.Instance.Get<EventBus>();
    }
    private void Update()
    {
        if (_lastPos != _placeToInteract.transform.position)
        {
            _lastPos = _placeToInteract.transform.position;
            _bus.Invoke(new PlaceToInteractMovedSignal(_lastPos));
        }
    }
}
