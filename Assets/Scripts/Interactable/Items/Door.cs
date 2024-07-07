using DG.Tweening;
using UnityEngine;
using UnityHFSM;

public class Door : Interactable
{
    [SerializeField] private Vector3 _fromRotation, _toRotation;
    [SerializeField, Min(0)] private float _timeToRotate = 0.3f;
    protected StateMachine _fsm;
    protected const string _open = "open", _close = "close";
    protected override void Start()
    {
        base.Start();
        transform.localEulerAngles = _fromRotation;
        
        _fsm = new StateMachine();
        _fsm.AddState(_open, new State(onLogic: state => transform.DOLocalRotate(_fromRotation, _timeToRotate)));
        _fsm.AddState(_close, new State(onLogic: state => transform.DOLocalRotate(_toRotation, _timeToRotate)));

        _fsm.AddTransition(_open, _close);
        _fsm.AddTransition(_close, _open);
        _fsm.SetStartState(_open);

        _fsm.Init();
    }
    public override void Interact()
    {
        _fsm.OnLogic();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEItem));
    }
    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }

}
