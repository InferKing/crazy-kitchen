using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private Order[] _orders;
    [SerializeField] private GameState _gameState = GameState.Game;
    private void Start()
    {
        StartCoroutine(GameTime());
    }
    private IEnumerator GameTime()
    {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new NewOrderSignal(_orders[0]));
        yield return new WaitForSeconds(60);
    }
}
