using UnityHFSM;

public class KitchenNote : Interactable
{
    private StateMachine _fsm;
    private void Start() 
    {
        _fsm = new StateMachine();
        _fsm.AddState("Idle", new KitchenNoteStateIdle(this));
        _fsm.AddState("Waiting", new KitchenNoteStateWaiting(this));
        _fsm.SetStartState("Idle");

        _fsm.AddTransition(new Transition(
            "Idle",
            "Waiting",
            transition => true // "true" can be replaced with integrated order or smth
        ));

        _fsm.Init();
    }
    public override void Interact()
    {
        _fsm.OnLogic();
    }

    public override void OnEnter()
    {
        EventBus bus = ServiceLocator.Instance.Get<EventBus>();
        bus.Invoke(new ShowItemTextSignal(Constants.keyPressENote));
    }

    public override void OnExit()
    {
        EventBus bus = ServiceLocator.Instance.Get<EventBus>();
        bus.Invoke(new ShowItemTextSignal(string.Empty));

    }

    public override void Drop()
    {
        throw new System.NotImplementedException();
    }

    public override void Combine(Interactable interactable)
    {
        throw new System.NotImplementedException();
    }
}
