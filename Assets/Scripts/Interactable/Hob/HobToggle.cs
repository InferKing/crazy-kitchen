using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;


public class HobToggle : Interactable
{
    [SerializeField] private List<Vector3> _rotations;
    private StateMachine<HobToggleState> _fsm;
    private void OnValidate()
    {
        int length = Enum.GetValues(typeof(HobToggleState)).Length;
        if (_rotations.Count != length)
        {
            Debug.LogWarning($"the number of rotations ({_rotations.Count}) must match the number of toggle hob states ({length})");
        }
    }
    protected override void Start()
    {
        base.Start();
        // this problem could be solved by simply looping the list, but we're not looking for easy ways :)
        // let me explain why: I don't know if the conditions of transition between states will be added, so I'll leave it like that.
        transform.localEulerAngles = _rotations[0];
        
        _fsm = new StateMachine<HobToggleState>();

        _fsm.AddState(HobToggleState.Off, new State<HobToggleState>(onLogic: (state) => {
            transform.DOLocalRotate(_rotations[0], 0.2f);
        }));
        _fsm.AddState(HobToggleState.Low, new State<HobToggleState>(onLogic: (state) => {
            transform.DOLocalRotate(_rotations[1], 0.2f);
        }));
        _fsm.AddState(HobToggleState.Medium, new State<HobToggleState>(onLogic: (state) => {
            transform.DOLocalRotate(_rotations[2], 0.2f);
        }));
        _fsm.AddState(HobToggleState.High, new State<HobToggleState>(onLogic: (state) => {
            transform.DOLocalRotate(_rotations[3], 0.3f);
        }));

        _fsm.AddTransition(HobToggleState.Off, HobToggleState.Low);
        _fsm.AddTransition(HobToggleState.Low, HobToggleState.Medium);
        _fsm.AddTransition(HobToggleState.Medium, HobToggleState.High);
        _fsm.AddTransition(HobToggleState.High, HobToggleState.Off);

        _fsm.SetStartState(HobToggleState.Off);

        _fsm.Init();

    }
    
    public void Toggle()
    {
        _fsm.OnLogic();
    }
}
