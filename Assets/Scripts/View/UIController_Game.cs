using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;

public class UIController_Game : SignalReceiver
{
    [SerializeField] private GameObject _ingredientParentUI;
    [SerializeField] private TMP_Text _ingredientDish, _ingredientList;
    private Dictionary<Transform, Sequence> _animSequences = new();
    protected override void Start()
    {
        base.Start();
        Bus.Subscribe<ShowDishesUISignal>(OnShowDishesUI);
        Bus.Subscribe<HideDishesUISignal>(OnHideDishesUI);
    }
    private void OnShowDishesUI(ShowDishesUISignal signal)
    {
        _ingredientDish.text = signal.data.ObjectName;
        // ñreate a list of ingredients in the dish and count the amounts. 
        var result = signal.data.Ingredients
            .GroupBy(item => item.ObjectName)
            .Select(item => new { Element = item.Key, Count = item.Count() });
        _ingredientList.text = "";
        foreach (var item in result)
        {
            _ingredientList.text += $"{item.Element} - {item.Count} pcs.\n";
        }
        if (signal.data.Ingredients.Count == 0)
        {
            _ingredientList.text = Constants.dishUINoIngredientText;
        }
        UpdateSequence(signal.data.transform, _ingredientParentUI.transform
            .DOScale(Vector3.one, 0.3f)
            .SetEase(Ease.OutBack));
    }
    private void OnHideDishesUI(HideDishesUISignal signal)
    {
        UpdateSequence(signal.data.transform, _ingredientParentUI.transform
            .DOScale(Vector3.zero, 0.2f)
            .OnComplete(() =>
            {
                _ingredientList.text = "";
                _ingredientDish.text = "";
            }));
    }
    private void UpdateSequence(Transform transform, Tween tween)
    {
        if (_animSequences.TryGetValue(transform, out Sequence result))
        {
            result.Kill();
            _animSequences.Remove(transform);
        }
        Sequence seq = DOTween.Sequence();
        seq.Append(tween);
        _animSequences[transform] = seq;
    }
    private void OnDisable()
    {
        if (Bus == null) return;
        Bus.Unsubscribe<ShowDishesUISignal>(OnShowDishesUI);
        Bus.Unsubscribe<HideDishesUISignal>(OnHideDishesUI);
    }
}
