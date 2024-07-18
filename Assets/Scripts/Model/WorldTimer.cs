using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WorldTimer : IService
{
    private EventBus _bus;
    private DateTime _dateTime, _lastDateBeforeSkip;
    public string TimeOnly => _dateTime.ToString("t"); 
    public string DateOnly => _dateTime.ToString("d"); 
    public string FullDateTime => _dateTime.ToString("f"); 
    public WorldTimer(DateTime time, EventBus bus) 
    {
        _dateTime = time;
        _bus = bus;
    }
    public void Update(TimeSpan time)
    {
        _dateTime = _dateTime.Add(time);
        if (IsWorkingHours())
        {
            _lastDateBeforeSkip = _dateTime;
        }
        _bus.Invoke(new UpdatedTimerSignal(this));
    }
    public bool TrySetNextDay()
    {
        if (!IsWorkingHours())
        {
            _dateTime = _lastDateBeforeSkip.Date.AddDays(1).AddHours(9);
            return true;
        }
        return false;
    }
    public bool IsWorkingHours() => _dateTime.Hour >= 10 && _dateTime.Hour < 22;
}
