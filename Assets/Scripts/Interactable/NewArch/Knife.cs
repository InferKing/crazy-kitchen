using DG.Tweening;
using UnityEngine;

public class Knife : Grabbable
{
    private Sequence _seq;
    private Transform _parent;
    protected override void Start()
    {
        base.Start();
        _actionKeys.Add(KeyCode.E, () => { }); // empty because hint should be displayed
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Sliceable item)
        {
            _parent = transform.parent;
            transform.SetParent(item.transform, true);
            _seq = DOTween.Sequence();
            Bus.Invoke(new ToggleMovementSignal(true));
            Bus.Invoke(new ToggleInteractSignal(true));
            GetComponent<Collider>().enabled = false;
            _seq.Append(transform.DOMove(item.transform.position + new Vector3(0, 0.07f, 0), 0.3f))
                .Append(transform.DOPunchRotation(Vector3.forward * 25, 0.5f, 5)
                .OnComplete(() =>
                {
                    transform.SetParent(_parent, true);
                    transform.SetLocalPositionAndRotation(Vector3.zero, new Quaternion() { eulerAngles = PlaceRoatation });
                    item.ToSlice(out Transform new_transform);
                    Bus.Invoke(new PlayFXSignal(new_transform, FXType.PuffSmoke));
                    Bus.Invoke(new ToggleMovementSignal(false));
                    Bus.Invoke(new ToggleInteractSignal(false));
                    GetComponent<Collider>().enabled = true;
                })
            );
            stayInHand = true;
            return true;
        }
        return false;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEKnife));
    }

    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
}
