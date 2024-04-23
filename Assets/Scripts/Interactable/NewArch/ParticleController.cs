using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour, IInitializable
{
    [SerializeField] private ParticleSystem _particleSystem;
    private EventBus _bus;
    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<PlayShortParticleFXSignal>(OnPlayShortParticleFX);
    }
    private void OnPlayShortParticleFX(PlayShortParticleFXSignal signal)
    {

    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<PlayShortParticleFXSignal>(OnPlayShortParticleFX);

    }
}
    