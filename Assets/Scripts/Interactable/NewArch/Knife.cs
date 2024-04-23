using DG.Tweening;
using UnityEngine;

public class Knife : Grabbable
{
    private Sequence _seq;
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
            // Не дать игроку двигаться во время анимации
            // item.ToSlice();
            // Починить (отделить от родителя, а также поставить kw)
            _seq.Append(transform.DOMove(item.transform.position, 0.2f));
            _seq.Append(transform.DORotate(new Vector3(0, 360, 0), 1f).OnComplete(() => { transform.localPosition = Vector3.zero; item.ToSlice(); }));
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
