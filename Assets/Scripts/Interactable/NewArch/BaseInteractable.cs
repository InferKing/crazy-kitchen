using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    // ƒл€ _actionKeys не описываетс€ действие Interact, потому что оно может вызыватьс€ ItemAction
    // без €вного указани€ (свои приколы с несколькими предметами и их комбинацией). 
    // ѕри этом, можно биндить клавишу E на какие-нибудь действи€.
    protected Dictionary<KeyCode, Action> _actionKeys;
    public IReadOnlyDictionary<KeyCode, Action> ActionKeys { get { return _actionKeys; } }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Interact();
}
