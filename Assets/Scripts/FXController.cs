using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : SignalReceiver
{
    [SerializeField] private List<FXData> _fx;
    private Dictionary<Transform, GameObject> _matchFX = new();
    protected override void Start()
    {
        base.Start();
        Bus.Subscribe<PlayFXSignal>(OnPlayFX);
        Bus.Subscribe<StopFXSignal>(OnStopFX);
    }
    private void OnPlayFX(PlayFXSignal signal)
    {
        FXData fx = _fx.Find(item => item.Type == signal.type);
        if (fx == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Can't find {signal.type} FX in {_fx}");
#endif
            return;
        }
        if (_matchFX.TryGetValue(signal.transform, out GameObject temp))
        {
            StopFX(signal.transform);
        }
        GameObject newFX = Instantiate(fx.Prefab);
        newFX.transform.SetParent(signal.transform, true);
        newFX.transform.localPosition = Vector3.zero;
        if (signal.lookAt)
        {
            newFX.transform.LookAt(signal.lookAt);
        }
        newFX.GetComponent<ParticleSystem>().Play();
        _matchFX[signal.transform] = newFX;
    }
    private void OnStopFX(StopFXSignal signal)
    {
        StopFX(signal.transform);
    }
    private void StopFX(Transform tr)
    {
        if (_matchFX.TryGetValue(tr, out GameObject temp))
        {
            ParticleSystem ps = temp.GetComponent<ParticleSystem>();
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (temp != null)
            {
                Destroy(temp, ps.main.startLifetime.constantMax);
            }
            _matchFX.Remove(tr);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Can't find {tr} with enabled fx");
#endif
        }
    }
    protected void OnDisable()
    {
        if (Bus == null) return;
        Bus.Unsubscribe<PlayFXSignal>(OnPlayFX);
        Bus.Unsubscribe<StopFXSignal>(OnStopFX);
    }
}
