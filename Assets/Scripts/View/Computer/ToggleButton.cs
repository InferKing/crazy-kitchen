using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour, IObservable
{
    [SerializeField] private GameObject _window;
    private bool _enabled = false;
    private List<IObserver> _observers = new List<IObserver>();

    public void AddObserver(IObserver o)
    {
        if (_observers.Contains(o)) return;
        _observers.Add(o);
    }

    public void NotifyObservers()
    {
        _observers.ForEach(toggle =>
        {
            toggle.UpdateInfo(this);
        });
    }
    public void RemoveObserver(IObserver o)
    {
        if (!_observers.Contains(o)) return;
        _observers.Remove(o);
    }

    public void OnClick()
    {
        _enabled = !_enabled;
        _window.SetActive(_enabled);
    }

    public void SetActive(bool enabled)
    {
        _enabled = enabled;
        _window.SetActive(_enabled);
    }
    public bool GetState() => _enabled;
}
