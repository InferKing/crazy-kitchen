
using DG.Tweening;
using UnityEngine;

public class Extinguisher : Grabbable
{
    [SerializeField] private GameObject _handle;
    private Sequence _seq;
    private Transform _parent;
    protected override void Start()
    {
        base.Start();
        _actionKeys.Add(KeyCode.E, () => { }); // empty because hint should be displayed
        TransformToFX = _handle.transform;
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Cookable item && item.IsInFire)
        {
            _parent = transform.parent;
            transform.SetParent(item.transform, true);
            _seq = DOTween.Sequence();
            Bus.Invoke(new ToggleMovementSignal(true));
            Bus.Invoke(new ToggleInteractSignal(true));
            GetComponent<Collider>().enabled = false;
            Vector3 pos = transform.position;
            _seq.Append(transform.DOMove(transform.position + Camera.main.transform.forward * 0.3f, 0.4f));
            _seq.Append(_handle.transform.DOLocalRotate(Vector3.down * 15, 0.3f)
                .OnComplete(() =>
                {
                    Bus.Invoke(new PlayFXSignal(TransformToFX, FXType.ExtinguisherSmoke, item.transform));
                }));
            _seq.Append(_handle.transform.DOLocalRotate(Vector3.zero, 0.15f)
                .SetDelay(1.8f)
                .OnComplete(() =>
                {
                    Bus.Invoke(new StopFXSignal(item.transform));
                }));
            _seq.Append(transform.DOMove(pos, 0.3f).OnComplete(() =>
            {
                transform.SetParent(_parent, true);
                transform.SetLocalPositionAndRotation(Vector3.zero, new Quaternion() { eulerAngles = PlaceRoatation });
                Bus.Invoke(new ToggleMovementSignal(false));
                Bus.Invoke(new ToggleInteractSignal(false));
                GetComponent<Collider>().enabled = true;
            }));
            item.IsInFire = false;
            stayInHand = true;
            return true;
        }
        return false;
    }
    public override void Drop()
    {
        transform.DORotate(IgnoreYZRotation, 0.1f);
        Rb.isKinematic = false;
        Rb.AddForce(Camera.main.transform.forward * 2, ForceMode.Impulse);
        Bus.Invoke(new ItemDroppedSignal());
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEExtinguisher));
    }
    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
}
