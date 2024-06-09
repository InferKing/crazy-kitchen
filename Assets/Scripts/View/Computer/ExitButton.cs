using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnClick()
    {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new ExitComputerSignal());
    }
}
