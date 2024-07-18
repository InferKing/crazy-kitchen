using UnityEngine;


public struct Reputation
{
    private float _reputation;
    public float Value { 
        get
        {
            return _reputation;
        }
        set
        {
            _reputation = Mathf.Clamp(value, 0, 100);
        }
    }
    public Reputation(float reputation)
    {
        _reputation = reputation;
    }
}
