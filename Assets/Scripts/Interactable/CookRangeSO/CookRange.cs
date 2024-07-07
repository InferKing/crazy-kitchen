using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookRange", menuName = "CookRange", order=51)]
public class CookRange : ScriptableObject
{
    [SerializeField] private string _stateName;
    [Header("Range")]
    [SerializeField, Range(0, 600)] private int _minSeconds;
    [SerializeField] private bool _isInfinite = false;
    [SerializeField] private int _maxSeconds;
    [Header("Material")]
    [SerializeField] private Material _material;
    public string StateName { get { return _stateName; } }
    public bool IsInRange(float value)
    {
        return _minSeconds <= value && _maxSeconds >= value;
    }
    public Material Material { get { return _material; } }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_maxSeconds < _minSeconds)
        {
            Debug.LogWarning($"the minimum number of ticks ({_minSeconds}) cannot exceed the maximum number of ticks ({_maxSeconds})");
        }
        if (_isInfinite)
        {
            _maxSeconds = int.MaxValue;
        }
    }
#endif
}
