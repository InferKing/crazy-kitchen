using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class InputView : MonoBehaviour
{
    [SerializeField] private GameObject _prefabInputText;
    [SerializeField] private VerticalLayoutGroup _groupInputView;
    private List<InputDescriptionView> _descriptions = new();
    private EventBus _bus;
    private void Start()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<ShowPossibleInputSignal>(OnShowPossibleInput);
        _bus.Subscribe<GetItemOutOfHandSignal>(OnGetItemOutOfHand);
    }
    private void OnShowPossibleInput(ShowPossibleInputSignal signal)
    {
        foreach (var item in signal.data)
        {
            if (KeyboardConstants.KeysDescription.TryGetValue(item.Key, out string description))
            {
                GameObject newView = Instantiate(_prefabInputText);
                newView.name = description;
                AddText(newView, description, item.Key);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"No match in KeysDescription for value {item.Key}");
#endif
            }
        }
    }
    private void AddText(GameObject obj, string text, KeyCode key)
    {
        InputDescriptionView view = obj.GetComponent<InputDescriptionView>();
        if (view != null)
        {
            view.SetText(Enum.GetName(typeof(KeyCode), key), text);
            view.transform.SetParent(_groupInputView.transform);
            _descriptions.Add(view);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Invalid prefab {_prefabInputText.name} (can't find InputDescriptionView)");
#endif
        }
    }
    private void OnGetItemOutOfHand(GetItemOutOfHandSignal signal)
    {
        RemoveAllText();
    }
    private void RemoveAllText()
    {
        _descriptions.ForEach(item =>
        {
            item.transform.SetParent(null);
            Destroy(item.gameObject);
        });
        _descriptions.Clear();
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<GetItemOutOfHandSignal>(OnGetItemOutOfHand);
        _bus.Unsubscribe<ShowPossibleInputSignal>(OnShowPossibleInput);
    }
}
