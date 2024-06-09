using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : SignalReceiver, IObserver
{
    [SerializeField] private List<ToggleButton> _toggles;
    protected override void Start()
    {
        base.Start();
        _toggles.ForEach(toggle => { 
            toggle.AddObserver(this);
        });
        DisableAll();
    }
    public void UpdateInfo(IObservable observable)
    {
        if (CheckAllDisabledBesides(observable))
        {
            (observable as ToggleButton).SetActive(false);
        }
        else
        {
            DisableAll();
            (observable as ToggleButton).SetActive(true);
        }
    }
    private void DisableAll()
    {
        _toggles.ForEach(toggle =>
        {
            toggle.SetActive(false);
        });
    }

    private bool CheckAllDisabledBesides(IObservable observable)
    {
        var data = _toggles.FindAll(toggle => !toggle.GetState());
        if (data.Count + 1 == _toggles.Count)
        {
            var res = data.Find(toggle => toggle == (ToggleButton)observable);
            return res == null;
        }
        return false;
    }
}
