using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceToCook : Interactable, IObserver
{
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private HobToggle _toggle;
    [SerializeField] private float _maxDistance = 0.2f;
    private BoxCollider _collider;
    private Dishes _dish;
    private HobToggleState _state;
    public bool IsEmpty { get; private set; } = true;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider>();
        _toggle.AddObserver(this);
    }
    protected virtual void Update()
    {
        RaycastHit hit;
        Ray ray = new(transform.position, Vector3.up);
#if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.red);
#endif
        if (Physics.Raycast(ray, out hit, _maxDistance, _interactableMask))
        {
            IsEmpty = false;
#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.green);
#endif
        }
        else
        {
            IsEmpty = true;
            _collider.enabled = true;
            _dish = null;
        }
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Dishes dishes && IsEmpty)
        {
            ToStove(dishes);
            // GameObject new_mesh = GetGameObject();
            // ServiceLocator.Instance.Get<EventBus>().Invoke(new DestroyMeDaddySignal(interactable.gameObject));
            return true;
        }
        return false;
    }
    public void ToStove(Dishes dish)
    {
        _dish = dish;
        _dish.transform.SetParent(transform);
        _dish.transform.eulerAngles = _dish.InitRotation;
        _dish.Rb.isKinematic = true;
        _dish.transform.position = new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z);
        _collider.enabled = false;
    }

    void IObserver.Update()
    {
        _state = _toggle.GetState();

    }
    private IEnumerator Cook()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (_state is not HobToggleState.Off)
            {

            }
        }
    }
}
