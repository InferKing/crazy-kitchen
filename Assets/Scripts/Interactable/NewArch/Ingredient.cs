public class Ingredient : Grabbable
{
    public override void OnEnter()
    {
        base.OnEnter();
        ServiceLocator.Instance.Get<EventBus>().Invoke(new ShowItemTextSignal(Constants.keyPressEItem));
    }

    public override void OnExit()
    {
        base.OnExit();
        ServiceLocator.Instance.Get<EventBus>().Invoke(new ShowItemTextSignal(string.Empty));
    }
}
