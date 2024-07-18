using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IInitializable
{
    // Подписываюсь на конец инициализации и стартую отсчет таймера
    private PlayerInfo _player;
    private WorldTimer _timer;
    private EventBus _bus;
    public void Initialize()
    {
        // Инициализирую игрока
        // В дальнейшем нужно поменять на подгрузку с файла
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _player = new(new Wallet(1500), new Reputation(0), new Reputation(0), Difficulty.Medium);
        _timer = new WorldTimer(new System.DateTime(2024, 2, 12, 9, 0, 0), _bus);
        _bus.Subscribe<ServicesInitializedSignal>(OnServicesInitialized);
        ServiceLocator.Instance.Register(_player);
    }
    private void OnServicesInitialized(ServicesInitializedSignal signal)
    {
        StartCoroutine(GameTimer());
    }
    private IEnumerator GameTimer()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(1);
            _timer.Update(new System.TimeSpan(0, 0, 30));
        }
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<ServicesInitializedSignal>(OnServicesInitialized);
    }
}
