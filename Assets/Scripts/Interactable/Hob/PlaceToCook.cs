using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceToCook : Interactable
{
    [SerializeField] private HobToggle _toggle;
    private BoxCollider _collider;
    private Dishes _dish;
    public bool IsEmpty { get; private set; } = true;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider>();
    }

    //public void ToStove(Dishes dish)
    //{
    //    if (!IsEmpty) return;
    //    _dish = dish;
    //    _dish.Rb.isKinematic = true;
    //    _dish.transform.position = transform.position;
    //    _collider.enabled = false;
    //}

    //public void OffStove()
    //{
    //    if (IsEmpty) return;
    //    _dish = null;
    //    _dish.Rb.isKinematic = false;
    //    _collider.enabled = true;
    //}
}
