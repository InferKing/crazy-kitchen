using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour, IInitializable
{
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField, Range(0, 7f)] private float _maxDistance;
    private EventBus _bus;

    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.red);
#endif
        if (Physics.Raycast(ray, out hit, _maxDistance, _interactableMask))
        {
            _bus.Invoke(new FindInteractableSignal(hit.collider.GetComponent<Interactable>()));
#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.green);
#endif
        }
        else
        {
            _bus.Invoke(new NoInteractableSignal());
        }
    }
    private void OnDisable()
    {
        if (_bus == null) return;
    }
}
