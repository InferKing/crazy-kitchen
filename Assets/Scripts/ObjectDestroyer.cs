using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObjectDestroyer : MonoBehaviour
{
    private List<MonoBehaviour> _objectsToDestroy;
    private EventBus _bus;
    private Coroutine _coroutine;
    void Start()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<DestroyMeDaddySignal>(OnDestroyMeDaddy);
        _objectsToDestroy = new List<MonoBehaviour>();
    }

    private void OnDestroyMeDaddy(DestroyMeDaddySignal signal)
    {
        if (signal.data is not ObjectDestroyer)
        {
            _objectsToDestroy.Add(signal.data);
            _coroutine ??= StartCoroutine(DestroyInNextFrame());
        }
    }
    private IEnumerator DestroyInNextFrame()
    {
        yield return null;
        _objectsToDestroy.ForEach(obj => { Destroy(obj); });
        _objectsToDestroy.Clear();
        _coroutine = null;

    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<DestroyMeDaddySignal>(OnDestroyMeDaddy);
    }
}
