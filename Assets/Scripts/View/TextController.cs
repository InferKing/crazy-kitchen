using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour, IInitializable
{
    [SerializeField] private TMP_Text _playerText;
    private EventBus _bus;
    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<ShowItemTextSignal>(OnShowItemText);
    }
    private void OnShowItemText(ShowItemTextSignal signal)
    {
        _playerText.text = signal.data;
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<ShowItemTextSignal>(OnShowItemText);

    }
}
