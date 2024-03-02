using UnityEngine;

public class InputHandler : MonoBehaviour, IInitializable
{
    private EventBus _bus;
    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _bus.Invoke(new InputDownSignal());
        }
    }
}
