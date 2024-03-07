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
    }
}
