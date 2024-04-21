using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Interactable
{
    [SerializeField] private Vector3 _initRotation = Vector3.zero;
    [SerializeField] private Vector3 _placeRotation = Vector3.zero;
    private bool _canGetAtMoment = false;
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
            { KeyCode.E, () => Interact() }
        };
    }
    public override void Interact()
    {
        Rb.isKinematic = true;
        Bus.Invoke(new ItemInteractedSignal(this));
    }
    public virtual void Drop()
    {
        transform.DORotate(IgnoreYRotation, 0.1f);
        Rb.isKinematic = false;
        Bus.Invoke(new ItemDroppedSignal());
    }
}
