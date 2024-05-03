using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceiver : MonoBehaviour
{
    protected EventBus Bus { get; private set; }
    protected virtual void Start()
    {
        Bus = ServiceLocator.Instance.Get<EventBus>();
    }
}
