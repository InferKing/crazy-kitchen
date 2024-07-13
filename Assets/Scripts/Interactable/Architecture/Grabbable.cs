using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Interactable
{
    [SerializeField] private Vector3 _initRotation = Vector3.zero;
    [SerializeField] private Vector3 _placeRotation = Vector3.zero;
    public bool GetByUser { get; set; } = false;
    public Rigidbody Rb { get; set; }
    public Vector3 InitRotation { get { return _initRotation; } }
    public Vector3 PlaceRoatation { get { return _placeRotation; } }
    public Vector3 IgnoreYRotation { get { return new Vector3(InitRotation.x, transform.rotation.eulerAngles.y, InitRotation.z); } }
    public Vector3 IgnoreYZRotation { get { return new Vector3(InitRotation.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); } }
    
    protected override void Start()
    {
        base.Start();
        Rb = gameObject.GetComponent<Rigidbody>();
        _actionKeys = new Dictionary<KeyCode, Action>()
        {
            { KeyCode.G, () => Drop() },
        };
    }
    public override void Interact()
    {
        Rb.isKinematic = true;
        Bus.Invoke(new ItemInteractedSignal(this));
    }
    public virtual void Drop()
    {
        GetByUser = false;
        transform.DORotate(IgnoreYRotation, 0.15f);
        Rb.isKinematic = false;
        Rb.AddForce(Camera.main.transform.forward * 2, ForceMode.Impulse);
        Bus.Invoke(new ItemDroppedSignal());
    }
}
