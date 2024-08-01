using System.Collections.Generic;

public class SiteSession
{
    private Dictionary<SitePageType, List<object>> _data = new();
    public void Add<T>(SitePageType key, T value) where T : class
    {
        if (_data.ContainsKey(key))
        {
            _data[key].Add(value);
        }
        else
        {
            _data[key] = new List<object>() { value };
        }

    }
    public List<object> Get(SitePageType key) 
    {
        return _data.GetValueOrDefault(key);
    }
}