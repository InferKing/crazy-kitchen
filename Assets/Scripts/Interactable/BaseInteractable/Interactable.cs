using System.Collections.Generic;
using UnityEngine;

public class Interactable : BaseInteractable
{

    [SerializeField] private bool _canGet = true;
    // хз нужно ли в итоге вводить состояния блокировки поворотов или хуй забить и в зависимости от объекта самому прописывать
    // [SerializeField] private LockRotationState _lockRotationState = LockRotationState.XZ;
    [SerializeField] private Vector3 _initRotation = Vector3.zero;
    [SerializeField] private Vector3 _placeRotation = Vector3.zero;
    private EventBus _bus;
    public Rigidbody Rb { get; set; }
    public bool CanSpawn { get; protected set; } = true;
    public Vector3 InitRotation { get { return _initRotation; } }
    public Vector3 PlaceRoatation { get { return _placeRotation; } }
    public Vector3 IgnoreYRotation { get { return new Vector3(InitRotation.x, transform.rotation.eulerAngles.y, InitRotation.z); } }
    public Vector3 IgnoreYZRotation { get { return new Vector3(InitRotation.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); } }
    public bool CanGet { get { return _canGet; } }
    public EventBus Bus { get { return _bus; } }
    // public LockRotationState LockRotation { get { return _lockRotationState; } }
    protected virtual void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
        _bus = ServiceLocator.Instance.Get<EventBus>();
    }

    public void UpdateChildRotation(Quaternion rotation)
    {
        //_curGO.transform.localRotation = rotation;
    }
    public override void Combine(Interactable interactable)
    {
        throw new System.NotImplementedException();
    }

    public override void Drop()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
        if (gameObject.TryGetComponent(out Outline outline))
        {
            outline.enabled = true;
        }
        else
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineWidth = 5;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
    }
    public override void OnExit()
    {
        if (gameObject.TryGetComponent(out Outline outline))
        {
            outline.enabled = false;
        }
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        throw new System.NotImplementedException();
    }
    public virtual GameObject GetGameObject()
    {
        return gameObject;
    }
}
