using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cookable : Ingredient
{
    [SerializeField] private List<CookRange> _rangeCookStates;
    private int _totalCookTime = 0;
    private CookRange _activeCookRange;
    public string IngredientCookState { get { return _activeCookRange.StateName; } }
    public void Cook(int cookDeltaTime)
    {
        if (cookDeltaTime <= 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Can't cook {gameObject.name} with cookDeltaTime {cookDeltaTime}");
#endif
            return;
        }
        _totalCookTime += cookDeltaTime;
        CookRange range = _rangeCookStates.FirstOrDefault(range => range.IsInRange(_totalCookTime));
        if (range != null)
        {
            if (_activeCookRange != range)
            {
                GetComponent<MeshRenderer>().material = range.Material;
            }
            _activeCookRange = range;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"No cooking range was found for object {gameObject.name}");
#endif
        }
    }
}
