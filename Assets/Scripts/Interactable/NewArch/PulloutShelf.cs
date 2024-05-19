using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

public class PulloutShelf : Interactable
{
    [SerializeField] private Vector3 _fromPosition, _toPosition;
    [SerializeField, Min(0)] private float _timeToMove = 0.3f;
    private StateMachine _fsm;
    private const string _open = "open", _close = "close";
    protected override void Start()
    {
        base.Start();
        transform.localPosition = _fromPosition;

        _fsm = new StateMachine();
        _fsm.AddState(_close, new State(onLogic: state => transform.DOLocalMove(_fromPosition, _timeToMove)));
        _fsm.AddState(_open, new State(onLogic: state => transform.DOLocalMove(_toPosition, _timeToMove)));

        _fsm.AddTransition(_open, _close);
        _fsm.AddTransition(_close, _open);
        _fsm.SetStartState(_close);

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
    public string GetStateName() => _fsm.ActiveStateName;
}
