using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceToCook : Interactable, IObserver
{
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private HobToggle _toggle;
    [SerializeField] private float _maxDistance = 0.2f;
    private BoxCollider _collider;
    private Dishes _dish;
    private HobToggleState _state = HobToggleState.Off;
    public bool IsEmpty { get; private set; } = true;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider>();
        _toggle.AddObserver(this);
        StartCoroutine(Cook());
        Bus.Subscribe<GetItemInHandSignal>(OnGetItemInHand);
    }
    private void OnGetItemInHand(GetItemInHandSignal signal)
    {
        if (_dish == null) return;
        if (_dish.gameObject == signal.data.gameObject)
        {
            IsEmpty = true;
            _collider.enabled = true;
            _dish = null;
        }
        //RaycastHit hit;
        //Ray ray = new(transform.position, Vector3.up);
        //if (Physics.Raycast(ray, out hit, _maxDistance, _interactableMask))
        //{
            
        //}
        //else
        //{
            
        //    Debug.Log(_dish);
        //}
    }
    public void ToStove(Dishes dish)
    {
        _dish = dish;
        _dish.transform.SetParent(transform);
        _dish.transform.eulerAngles = _dish.InitRotation;
        //_dish.Rb.isKinematic = true;
        _dish.transform.position = new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z);
        _collider.enabled = false;
        IsEmpty = false;
        Bus.Invoke(new ItemDroppedSignal());
    }

    public void UpdateInfo()
    {
        _state = _toggle.GetState();
    }
    private IEnumerator Cook()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (_dish != null)
            {
                foreach (Cookable item in _dish.Ingredients.Cast<Cookable>())
                {
                    item.Cook((int)_state);
                }
            }
        }
    }
    private void OnDisable()
    {
        if (Bus == null) return;
        Bus.Unsubscribe<GetItemInHandSignal>(OnGetItemInHand);

    }
}
