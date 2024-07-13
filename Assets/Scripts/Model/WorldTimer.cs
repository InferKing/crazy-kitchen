using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WorldTimer
{
    private DateTime _dateTime;
    public string TimeOnly { get => _dateTime.ToString("t"); }
    public string DateOnly { get => _dateTime.ToString("d"); }
    public string FullDateTime { get => _dateTime.ToString("f"); }
    public WorldTimer()
    {
        // idk why that date
        _dateTime = new DateTime(2018, 5, 10, 8, 0, 0);
    }
    public WorldTimer(DateTime time) 
    {
        _dateTime = time;
    }
    public void Update(TimeSpan time)
    {
        _dateTime = _dateTime.Add(time);
    }
    public bool TrySetNextDay()
    {
        // here i should write some genius code
        // установить на следующий день, только если сейчас не день, а нерабочее время
        return false;
    }
    public bool IsWorkingHours() => _dateTime.Hour >= 10 && _dateTime.Hour < 22;
}
