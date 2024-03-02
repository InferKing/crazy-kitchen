using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _controllers;
    private List<IInitializable> _inits = new();
    private EventBus _bus;
    public IReadOnlyList<IInitializable> Inits => _inits;
    private void OnValidate()
    {
        ValidateServices();
    }
    private void Awake()
    {
#if !UNITY_EDITOR
        ValidateServices();
#endif
        _bus = new EventBus();
        ServiceLocator.Initialize();
        ServiceLocator.Instance.Register(_bus);
        foreach (IInitializable item in Inits)
        {
            item.Initialize();
        }
        _bus.Invoke(new ServicesInitializedSignal());
    }
    private void ValidateServices()
    {
        _inits.Clear();
        foreach (var controller in _controllers)
        {
            if (controller is IInitializable initialize)
            {
                _inits.Add(initialize);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Object {controller} can't be cast to type IInitialize");
#endif
            }
        }
    }
    private void OnDisable()
    {
        ServiceLocator.Instance.Unregister<EventBus>();
    }
}
