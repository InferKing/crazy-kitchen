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
        _bus.Subscribe<EnterNoteSignal>(OnEnterNote);
        _bus.Subscribe<ExitNoteSignal>(OnExitNote);
    }
    private void OnEnterNote(EnterNoteSignal note)
    {
        _playerText.rectTransform.DOShakeScale(0.3f, 0.1f).OnComplete(() => _playerText.rectTransform.localScale = Vector3.one);
        _playerText.text = "Нажмите E, чтобы взять заказ";
    }
    private void OnExitNote(ExitNoteSignal note)
    {
        _playerText.text = "";
    }
}
